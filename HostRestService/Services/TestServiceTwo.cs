using HostRestService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostRestService.Services
{
    public class TestServiceTwo : ITestServiceTwo
    {

        public string GetName(string inputMessage)
        {
            return "GetMessage:" + inputMessage;
        }
    }
}
