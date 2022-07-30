using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NQTGame
{
    public class TimeOutResponse
    {
        public int id { get; set; }
        public double time { get; set; }
        public TimeOutResponse(int id, double time)
        {
            this.id = id;
            this.time = time;
        }
    }
    public class TimeOut
    {
        public bool isStop { get; set; } = false;
        private Thread thread;
        private readonly SynchronizationContext SyncContext;
        private TimeOutResponse response = new TimeOutResponse(0, 0);
        private int num = 0;
        public int time;
        public TimeOut() : this(1000) { }
        public TimeOut(int time, int num = -1)
        {
            SyncContext = AsyncOperationManager.SynchronizationContext;
            this.time = time;
            this.num = num;
        }
        public event EventHandler<TimeOutResponse> Handle;
        public event EventHandler<TimeOutResponse> End;
        public void Start()
        {
            thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }
        public void Stop()
        {
            isStop = true;
        }
        private void Run()
        {
            while (!isStop)
            {
                if (response.id >= num && num >= 0)
                {
                    SyncContext.Post(e => EndCallback(), null);
                    Stop();
                }
                else
                {
                    Thread.Sleep((int)time);
                    response.id++;
                    response.time += time;
                    SyncContext.Post(e => HandleCallback(), null);
                }
            }
        }
        private void HandleCallback()
        {
            Handle?.Invoke(this, response);
        }
        private void EndCallback()
        {
            End?.Invoke(this, response);
        }
    }
}
