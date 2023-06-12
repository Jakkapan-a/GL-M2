using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
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

            using (Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame))
            {
                OnFrameHeader?.Invoke(bitmap);
            }

            OnFrameHeaderMat?.Invoke(frame);
            OnEvent?.Invoke();
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


        /* private void FrameCapture(object state)
         {
             try
             {
                 if (_videoCapture.IsOpened())
                 {
                     if (_onStarted)
                     {
                         OnVideoStarted?.Invoke();
                         _onStarted = false;
                     }
                     using (OpenCvSharp.Mat frame = _videoCapture.RetrieveMat())
                     {
                         if (frame.Empty())
                         {
                             OnError?.Invoke("Frame is empty");
                         }
                         else
                         {
                             using (Bitmap bitmap = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(frame))
                             {
                                 OnFrameHeader?.Invoke(bitmap);
                             }

                             OnFrameHeaderMat?.Invoke(frame);
                             OnEvent?.Invoke();
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
                     }
                 }
             }
             catch (Exception ex)
             {
                 OnError?.Invoke(ex.Message);
             }
         }

         */

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
            TryDispose(ref _videoCapture);
            TryDispose(ref _timer);
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
