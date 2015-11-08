using HostRestService.Interfaces;
using HostRestService.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HostRestService
{
    public partial class HostService : ServiceBase
    {
        Dictionary<string, IWindSerOperations> _innerDictionary;

        private CancellationTokenSource _tokenSource = null;
        private CancellationToken? _token = null;

        public HostService()
        {
            InitializeComponent();


            EventLog appLog =new EventLog();
            appLog.Source = "This Application's Name";
            appLog.WriteEntry("An entry to the Application event log.");

            _innerDictionary = new Dictionary<string, IWindSerOperations>();

            _innerDictionary.Add("TestService", new WindSerOperations<TestService, ITestService>("http://localhost:8000"));
            _innerDictionary.Add("TestServiceTwo", new WindSerOperations<TestServiceTwo, ITestServiceTwo>("http://localhost:8001"));
        }

        internal void DebugService(string[] args)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            foreach (KeyValuePair<string, IWindSerOperations> windSerOperationKeyValue in _innerDictionary)
            {
                Task startedTask = Task.Factory.StartNew(_ =>
                {
                    windSerOperationKeyValue.Value.StartOperation();
                }, _token);
            }
        }

        protected override void OnStart(string[] args)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            foreach (KeyValuePair<string, IWindSerOperations> windSerOperationKeyValue in _innerDictionary)
            {
                Task startedTask = Task.Factory.StartNew(_ =>
                {
                    windSerOperationKeyValue.Value.StartOperation();
                }, _token);
            }
        }

        protected override void OnStop()
        {
            foreach (KeyValuePair<string, IWindSerOperations> windSerOperationKeyValue in _innerDictionary)
            {
                windSerOperationKeyValue.Value.StopOperation();
            }

            _tokenSource.Cancel();
        }


    }
}
