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
        IWindSerOperations _serviceOperations;

        private CancellationTokenSource _tokenSource = null;
        private CancellationToken? _token = null;

        public HostService()
        {
            InitializeComponent();

            _serviceOperations = new WindSerOperations<TestService, ITestService>("http://localhost:8000");
        }

        internal void DebugService(string[] args)
        {
            _serviceOperations.StartOperation();
        }

        protected override void OnStart(string[] args)
        {
            _tokenSource = new CancellationTokenSource();
            _token = _tokenSource.Token;

            IWindSerOperations serviceOperations = new WindSerOperations<TestService, ITestService>("http://localhost:8000");
            Task startedTask = Task.Factory.StartNew(_ =>
            {
                serviceOperations.StartOperation();
            }
           , _token);

            IWindSerOperations serviceOperationsTwo = new WindSerOperations<TestServiceTwo, ITestServiceTwo>("http://localhost:8001");
            Task startedTaskTwo = Task.Factory.StartNew(_ =>
            {
                serviceOperationsTwo.StartOperation();
            }
           , _token);
        }

        protected override void OnStop()
        {
            _tokenSource.Cancel();
            _serviceOperations.StopOperation();
        }


    }
}
