using HostRestService.Interfaces;
using HostRestService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using System.Text;
using System.Collections;
using System.IO;
using System.Threading;
using HostRestService.LogEvent;
using System.Diagnostics;

namespace HostRestService
{
    public class WindSerOperations<TService, TServiceInterface> : IWindSerOperations
    {
        private WebServiceHost _host;
        private ServiceEndpoint _ep;
        private bool _stopTriggerred = false;
        private ILogEventViewer _logEventviewer;
        private string _serviceName;

        public WindSerOperations(string serviceName, string uri, ILogEventViewer logEventviewer)
        {
            _logEventviewer = logEventviewer;
            _serviceName = serviceName;
            _host = new WebServiceHost(typeof(TService), new Uri(uri));
            _ep = _host.AddServiceEndpoint(typeof(TServiceInterface), new WebHttpBinding(), "");
        }

        public void StartOperation()
        {
            ServiceDebugBehavior stp = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _host.Open();

            _logEventviewer.LogEvent(string.Format("{0} service is up and running.", _serviceName), EventLogEntryType.Information, Thread.CurrentThread.ManagedThreadId);

            while (!_stopTriggerred)
            {
                Thread.Sleep(1000 * 5);
            }
        }

        public void StopOperation()
        {
            _host.Close();
            _logEventviewer.LogEvent(string.Format("{0} service stopped.", _serviceName), EventLogEntryType.Information, Thread.CurrentThread.ManagedThreadId);
            _stopTriggerred = true;
        }
    }
}
