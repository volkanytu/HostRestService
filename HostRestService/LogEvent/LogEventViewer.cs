using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace HostRestService.LogEvent
{
    public class LogEventViewer : ILogEventViewer
    {
        private string _applicationName;
        private EventLog _appLog;

        public LogEventViewer(string applicatioName)
        {
            _applicationName = applicatioName;

            _appLog = new EventLog();
        }

        public void LogEvent(string message, EventLogEntryType eventType, int id)
        {
            _appLog.Source = _applicationName;
            _appLog.WriteEntry(message, eventType, Thread.CurrentThread.ManagedThreadId);
        }
    }
}
