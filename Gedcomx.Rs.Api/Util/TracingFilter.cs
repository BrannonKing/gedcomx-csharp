using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace Gx.Rs.Api.Util
{
    /// <summary>
    /// Enables log4net logging of REST API requests before the requests are executed. See remarks.
    /// </summary>
    /// <remarks>
    /// Log4net is used to log information about requests. The information is output as a DEBUG string and the logger is of
    /// type <c>Gx.Rs.Api.Util.Log4NetLoggingFilter</c>.
    /// </remarks>
    [EventSource(Name = "Request-EventLog")]
    public class TracingFilter : EventSource, IFilter
    {
        /// <summary>
        /// This method uses log4net to output a DEBUG string containing the HTTP method and fully qualified URI that will be executed.
        /// </summary>
        /// <param name="client">The REST API client that will execute the specified request.</param>
        /// <param name="request">The REST API request that will be executed by the specified client.</param>
        [NonEvent]
        public void Handle(IRestClient client, IRestRequest request)
        {
            Handle(request.Method.ToString(), client.BaseUrl.ToString(), request.Resource);
        }

        [Event(1, Message = "{0} {1}{2}", Level = EventLevel.Informational)]
        private void Handle(string method, string baseUrl, string path)
        {
            WriteEvent(1, method, baseUrl, path);
        }
    }
}
