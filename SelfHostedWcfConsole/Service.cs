using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Contracts;

namespace SelfHostedWcfConsole
{
    public class Service : IRest
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
