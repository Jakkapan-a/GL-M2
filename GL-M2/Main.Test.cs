using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL_M2
{
    partial class Main
    {
        private Task taskProcessTest;
        private void timerTest_Tick(object sender, EventArgs e)
        {
            if (taskProcessTest != null && taskProcessTest.Status == TaskStatus.Running)
            {
                return;
            }

            taskProcessTest = Task.Run(() => ProcessTest()).ContinueWith(task =>{
           if (task.Exception != null)
           {
               // Log error
               Console.WriteLine(task.Exception.InnerException.Message);
           }});
        }
        
        private void ProcessTest()
        {
            rectangles = SQliteDataAccess.Rectangles.GetByModelId(model_id);
            // Check if there is any rectangle
            if(rectangles.Count == 0) return;

            
        }
        
    }
}
