using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HostRestService.Interfaces
{
    [ServiceContract]
    public interface ITestServiceTwo
    {
        [OperationContract]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetName/{inputMessage}")]
        string GetName(string inputMessage);

    }
}
