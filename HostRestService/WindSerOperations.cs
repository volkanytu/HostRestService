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

namespace HostRestService
{
    public class WindSerOperations<TService, TServiceInterface> : IWindSerOperations
    {
        WebServiceHost _host;
        ServiceEndpoint _ep;
        bool _stopTriggerred = false;

        public WindSerOperations(string uri)
        {
            _host = new WebServiceHost(typeof(TService), new Uri(uri));
            _ep = _host.AddServiceEndpoint(typeof(TServiceInterface), new WebHttpBinding(), "");
        }

        public void StartOperation()
        {
            ServiceDebugBehavior stp = _host.Description.Behaviors.Find<ServiceDebugBehavior>();
            stp.HttpHelpPageEnabled = false;
            _host.Open();

            using (StreamWriter streamWriter = new StreamWriter(@"C:\LogFolder\restservicelog.txt"))
            {
                streamWriter.WriteLine("Service is up and running");
            }

            while (!_stopTriggerred)
            {
                Thread.Sleep(1000 * 5);
            }
        }

        public void StopOperation()
        {
            _host.Close();

            _stopTriggerred = true;
        }
    }
}
