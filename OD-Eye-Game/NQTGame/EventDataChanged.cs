using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.ComponentModel;

namespace NQTGame
{
    public abstract class EventDataChanged
    {
        private readonly SynchronizationContext SyncContext;
        public event EventHandler DataChanged = null;
        public EventDataChanged()
        {
            SyncContext = AsyncOperationManager.SynchronizationContext;
        }
        private protected void DataChange()
        {
            SyncContext.Post(e => DataChanged_Callback(), null);
        }
        private protected void DataChanged_Callback()
        {
            DataChanged?.Invoke(this,EventArgs.Empty);
        }
    }
}
