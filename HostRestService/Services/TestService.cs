using HostRestService.Interfaces;
using HostRestService.LogEvent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Web;

namespace HostRestService.Services
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class TestService : ITestService
    {
        public string SetMessage(string inputMessage)
        {
            try
            {
                HttpContext.Current.Session.Add("123", inputMessage);
            }
            catch (Exception ex)
            {
                ILogEventViewer logEvent = new LogEventViewer("TestService");
                logEvent.LogEvent(ex.Message, System.Diagnostics.EventLogEntryType.Error, Thread.CurrentThread.ManagedThreadId);
            }

            return "SetMessage:" + inputMessage;
        }

        public string GetMessage(string sessionId)
        {
            var session = HttpContext.Current.Session[sessionId];

            return "GetMessage:" + session.ToString();
        }

        public string PostMessage(string inputMessage)
        {
            return "PostMessage:" + inputMessage;
        }
    }
}
