using HostRestService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostRestService.Services
{
    public class TestService : ITestService
    {
        public string GetMessage(string inputMessage)
        {
            return "GetMessage:" + inputMessage;
        }

        public string PostMessage(string inputMessage)
        {
            return "PostMessage:" + inputMessage;
        }
    }
}
