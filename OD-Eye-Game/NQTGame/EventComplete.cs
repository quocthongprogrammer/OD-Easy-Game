using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace NQTGame
{
    public abstract class EventComplete
    {
        private readonly SynchronizationContext SyncContext;
        public event EventHandler Complete = null;
        public event EventHandler InitComplete = null;
        public EventComplete()
        {
            SyncContext = AsyncOperationManager.SynchronizationContext;
        }
        public void SentComplete()
        {
            SyncContext.Post(e => Complete_Callback(), null);
        }
        private void Complete_Callback()
        {
            Complete?.Invoke(this,EventArgs.Empty);
        }
        public void SentInitComplete()
        {
            SyncContext.Post(e => InitComplete_Callback(), null);
        }
        private void InitComplete_Callback()
        {
            InitComplete?.Invoke(this, EventArgs.Empty);
        }
    }
}
