using System;
using System.Diagnostics;
namespace HostRestService.LogEvent
{
    public interface ILogEventViewer
    {
        void LogEvent(string message, EventLogEntryType eventType, int id);
    }
}
