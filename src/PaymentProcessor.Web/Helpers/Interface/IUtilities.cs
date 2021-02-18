using PaymentProcessor.Service.Helpers;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PaymentProcessor.Web.Helpers.Interfaces
{
    public interface IUtilities
    {
        Task<HttpResponseMessage> MakeHttpRequest(object request, string baseAddress, string requestUri, HttpMethod method, Dictionary<string, string> headers = null);
    }
}
