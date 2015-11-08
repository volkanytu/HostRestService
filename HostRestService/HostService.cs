using HostRestService.Interfaces;
using HostRestService.LogEvent;
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
        ILogEventViewer _logEvent;
        Dictionary<string, IWindSerOperations> _innerDictionary;

        private CancellationTokenSource _tokenSource = null;
        private CancellationToken? _token = null;

        public HostService()
        {
            InitializeComponent();

            _logEvent = new LogEventViewer(this.ServiceName);
            _innerDictionary = new Dictionary<string, IWindSerOperations>();

            _innerDictionary.Add("TestService", new WindSerOperations<TestService, ITestService>("TestService", "http://localhost:8000", _logEvent));
            _innerDictionary.Add("TestServiceTwo", new WindSerOperations<TestServiceTwo, ITestServiceTwo>("TestServiceTwo", "http://localhost:8001", _logEvent));
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
