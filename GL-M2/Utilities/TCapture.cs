using DirectShowLib.DMO;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2.Utilities
{
    public class TCapture
    {
        private OpenCvSharp.VideoCapture _videoCapture;

        public delegate void VideoCaptureError(string messages);
        public event VideoCaptureError OnError;

        public delegate void VideoFrameHeader(Bitmap bitmap);
        public event VideoFrameHeader OnFrameHeader;

        public delegate void VideoFrameEvent();
        public event VideoFrameEvent OnEvent;

        public delegate void VideoFrameHeaderMat(OpenCvSharp.Mat mat);
        public event VideoFrameHeaderMat OnFrameHeaderMat;

        public delegate void VideoCaptureStop();
        public event VideoCaptureStop OnVideoStop;

        public delegate void VideoCaptureStarted();
        public event VideoCaptureStarted OnVideoStarted;

        private bool _onStarted = false;

        public bool _isRunning { get; set; }

        private int _frameRate = 50;

        public int width { get; set; }
        public int height { get; set; }

        public void setSize(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public int frameRate
        {
            get { return _frameRate; }
            set { _frameRate = 1000 / value; }
        }

        public bool IsOpened
        {
            get { return IsOpen(); }
        }
        public bool IsOpen()
        {
            if (_videoCapture != null && _videoCapture.IsOpened())
            {
                return true;
            }
            return false;
        }
        private readonly Stopwatch stopwatchFrame;
        private long frameCount = 0;

        public double rateFPS { get; private set; }

        public TCapture()
        {
            stopwatchFrame = new Stopwatch();
            width = 1920;
            height = 1080;
        }
        private System.Threading.Timer _timer;

        public async Task StartAsync(int device)
        {
            await Task.Run(() => Start(device));
        }
        private void TryInitOrDispose<T>(ref T obj, Func<T> initFunc) where T : IDisposable
        {
            obj?.Dispose();
            obj = initFunc();
        }

        public void Start(int device)
        {
            try
            {
                TryInitOrDispose(ref _videoCapture, () => new OpenCvSharp.VideoCapture(device));

                if (!_videoCapture.Open(device))
                {
                    OnError?.Invoke("Cannot open the video capture device.");
                    return;
                }

                setFrame(width, height);
                _isRunning = true;
                _onStarted = true;

                TryInitOrDispose(ref _timer, () => new System.Threading.Timer(callback: FrameCapture, null, 0, _frameRate));

            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Error while starting video capture: {ex.Message}");
            }
        }

        private void ProcessFrame(OpenCvSharp.Mat frame)
        {
            if (frame.Empty())
            {
                OnError?.Invoke("Frame is empty");
                return;
            }

            using (OpenCvSharp.Mat mat = ReduceNoise(frame))
            {
                using (Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(mat))
                {
                    OnFrameHeader?.Invoke(bitmap);
                }
                OnFrameHeaderMat?.Invoke(mat);
                OnEvent?.Invoke();
            }

            frameCount++;
            if (!stopwatchFrame.IsRunning)
            {
                stopwatchFrame.Start();
            }
            else
            {
                double elapsedSeconds = stopwatchFrame.Elapsed.TotalSeconds;
                if (elapsedSeconds >= 1)
                {
                    rateFPS = frameCount / elapsedSeconds;
                    frameCount = 0;
                    stopwatchFrame.Restart();
                }
            }
        }
        private const float h = 3f;
        private const float hColor = 3f;
        private const int templateWindowSize = 1;
        private const int searchWindowSize = 3;
        // Set the parameters for GaussianBlur
        private const int blurSize = 5; // Increase this for more aggressive noise reduction
        public Mat ReduceNoise(Mat src)
        {
            if(!Properties.Settings.Default.isMedianBlur)
                return src;

            Mat dst = new Mat();
            int blur = Properties.Settings.Default.medianBlur;
            blur = blur % 2==0 ? blur+1 : blur;
            Cv2.MedianBlur(src, dst, blur);
            //int blurSize = 5;
            //Cv2.GaussianBlur(src, dst, new OpenCvSharp.Size(blurSize, blurSize), 0);
            //Cv2.FastNlMeansDenoisingColored(src, dst, h, hColor, templateWindowSize, searchWindowSize);
            return dst;
        }

        private const double sigma = 1.5;
        private const double threshold = 5.0;
        private const double amount = 1.5;

        public Mat Sharpen(Mat src)
        {


         const double sigma = 1.5;
         const double threshold = 5.0;
         const double amount = 1.5;
            // Ensure src is floating point image
            if (src.Type() != MatType.CV_32FC3)
            {
                src.ConvertTo(src, MatType.CV_32FC3);
            }

            // Create the blurred version of the image
            Mat blurred = new Mat();
            Mat dst = new Mat();
            Cv2.GaussianBlur(src, blurred, new OpenCvSharp.Size(), sigma, sigma);

            // Subtract the blurred image from the source image
            Mat lowContrastMask = new Mat();
            Cv2.Absdiff(src, blurred, lowContrastMask);

            // Create a mask for the edges that we want to sharpen
            Mat mask = new Mat();
            Cv2.Compare(lowContrastMask, threshold, mask, CmpType.LE);

            // Scale the source image
            Mat scaledSrc = new Mat();
            src.ConvertTo(scaledSrc, MatType.CV_32FC3, amount);

            // Scale the blurred image
            Mat scaledBlur = new Mat();
            blurred.ConvertTo(scaledBlur, MatType.CV_32FC3, 1.0 - amount);

            // Combine the scaled images
            Cv2.AddWeighted(scaledSrc, 1.0 - amount, scaledBlur, amount, 0, dst);

            // Convert back to 8 bit image before returning
            dst.ConvertTo(dst, MatType.CV_8UC3);

            // Clean up
            blurred.Dispose();
            lowContrastMask.Dispose();
            mask.Dispose();
            scaledSrc.Dispose();
            scaledBlur.Dispose();
            return dst;
        }

        public void Sharpen(Mat src, Mat dst)
        {
            // Create the blurred version of the image
            Mat blurred = new Mat();
            Cv2.GaussianBlur(src, blurred, new OpenCvSharp.Size(), sigma, sigma);

            // Subtract the blurred image from the source image
            Mat lowContrastMask = new Mat();
            Cv2.Absdiff(src, blurred, lowContrastMask);

            // Create a mask for the edges that we want to sharpen
            Mat mask = new Mat();
            Cv2.Compare(lowContrastMask, threshold, mask, CmpType.LE);

            // Scale the source image
            Mat scaledSrc = new Mat();
            src.ConvertTo(scaledSrc, MatType.CV_8UC3, amount);

            // Scale the blurred image
            Mat scaledBlur = new Mat();
            blurred.ConvertTo(scaledBlur, MatType.CV_8UC3, 1.0 - amount);

            // Combine the scaled images
            Cv2.AddWeighted(scaledSrc, 1.0 - amount, scaledBlur, amount, 0, dst);

            // Clean up
            blurred.Dispose();
            lowContrastMask.Dispose();
            mask.Dispose();
            scaledSrc.Dispose();
            scaledBlur.Dispose();
        }

        private void FrameCapture(object state)
        {
            try
            {
                if (!_videoCapture.IsOpened())
                    return;

                if (_onStarted)
                {
                    OnVideoStarted?.Invoke();
                    _onStarted = false;
                }

                using (OpenCvSharp.Mat frame = _videoCapture.RetrieveMat())
                {
 
                    ProcessFrame(frame);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke(ex.Message);
            }
        }

        public void setFrame(int width, int height)
        {
            _videoCapture?.Set(OpenCvSharp.VideoCaptureProperties.FrameWidth, width);
            _videoCapture?.Set(OpenCvSharp.VideoCaptureProperties.FrameHeight, height);
        }

        private void TryDispose<T>(ref T obj) where T : IDisposable
        {
            obj?.Dispose();
            obj = default;
        }

        public async Task StopAsync()
        {
            await Task.Run(() => Stop());
        }

        public void Stop()
        {
            _isRunning = false;
            try
            {
                TryDispose(ref _videoCapture);
                TryDispose(ref _timer);
            }catch(Exception ex)
            {
                OnError?.Invoke("TCapture Stop Error " + ex.Message);
            }

            OnVideoStop?.Invoke();
        }

        public void Dispose()
        {
            _isRunning = false;
            TryDispose(ref _videoCapture);
        }

        // Get Focus
        public int GetFocus()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Focus);
        }
        // Set Focus 
        public void setFocus(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Focus, value);
        }
        // Auto Focus
        public void AutoFocus()
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Focus, -1);
        }

        // Get Zoom
        public int GetZoom()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Zoom);
        }

        // Set Zoom
        public void setZoom(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Zoom, value);
        }
        // Get Exposure
        public int getExposure()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Exposure);
        }

        // Set Exposure
        public void setExposure(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Exposure, value);
        }

        // Get Gain
        public int GetGain()
        {
            return (int)_videoCapture.Get(OpenCvSharp.VideoCaptureProperties.Gain);
        }

        // Set Gain
        public void setGain(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Gain, value);
        }

        // Set Brightness
        public void setBrightness(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Brightness, value);
        }

        // Set Contrast
        public void setContrast(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Contrast, value);
        }

        // Set Saturation
        public void setSaturation(int value)
        {
            _videoCapture.Set(OpenCvSharp.VideoCaptureProperties.Saturation, value);
        }

    }
}
