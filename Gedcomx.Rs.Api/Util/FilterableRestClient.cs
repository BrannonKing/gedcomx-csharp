using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Gedcomx.File;
using Gedcomx.Support;
using Newtonsoft.Json.Serialization;
using RestSharp.Portable;
using RestSharp.Portable.Deserializers;
using RestSharp.Portable.Encodings;
using RestSharp.Portable.Serializers;

namespace Gx.Rs.Api.Util
{
    /// <summary>
    /// Represents a REST API client that can have <see cref="IFilter"/>s applied just before executing requests. See remarks.
    /// </summary>
    /// <remarks>
    /// It is important to note, however, that in order for the <see cref="IFilter"/>s to be applied, the <see cref="Handle"/> method must be called.
    /// Calling <see cref="O:IRestClient.Execute"/> directly will not result in these filters being applied.
    /// </remarks>
    public class FilterableRestClient : RestClient, IFilterableRestClient
    {
        private List<IFilter> filters;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableRestClient"/> class.
        /// </summary>
        public FilterableRestClient()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FilterableRestClient"/> class.
        /// </summary>
        /// <param name="baseUrl">The base URI for the REST API client.</param>
        public FilterableRestClient(string baseUrl)
            : base(baseUrl)
        {
	        IgnoreResponseStatusCode = true; // don't throw an exception on 401

            filters = new List<IFilter>{new ContentTypeRemover()};

	        var xmlSerializer = new GedcomXmlDeserializer();
	        var jsonSerializer = new GedcomJsonDeserializer();

            AddHandler(MediaTypes.GEDCOMX_JSON_MEDIA_TYPE, jsonSerializer);
            AddHandler(MediaTypes.GEDCOMX_XML_MEDIA_TYPE, xmlSerializer);
			AddHandler(MediaTypes.GEDCOMX_RECORDSET_JSON_MEDIA_TYPE, jsonSerializer);
			AddHandler(MediaTypes.GEDCOMX_RECORDSET_XML_MEDIA_TYPE, xmlSerializer);
			AddHandler(MediaTypes.ATOM_GEDCOMX_JSON_MEDIA_TYPE, jsonSerializer);
			AddHandler("application/x-fs-v1+json", jsonSerializer);
			AddHandler(MediaTypes.ATOM_XML_MEDIA_TYPE, xmlSerializer);
			AddHandler(MediaTypes.APPLICATION_JSON_TYPE, jsonSerializer);
			AddHandler(MediaTypes.APPLICATION_XML_TYPE, xmlSerializer);
	        AddHandler(MediaTypes.TEXT_PLAIN, new TextDeserializer());
        }

        private class ContentTypeRemover : IFilter
        {
            public void Handle(IRestClient client, IRestRequest request)
            {
                //if (request.Method == HttpMethod.Get)
                {
                    for (int i = request.Parameters.Count - 1; i >= 0; i--)
                    {
                        var p = request.Parameters[i];
                        if (p.Name == "Content-Type" && p.Type == ParameterType.HttpHeader)
                            request.Parameters.RemoveAt(i);
                    }
                }
            }
        }

	    private class TextDeserializer : IDeserializer
	    {
		    public T Deserialize<T>(IRestResponse response)
		    {
			    if (typeof(T) == typeof(string))
				    return (T)(object)Encoding.UTF8.GetString(response.RawBytes, 0, response.RawBytes.Length);
				throw new NotImplementedException("Not sure what is using this.");
		    }
	    }

        private class GedcomJsonDeserializer : IDeserializer
        {
            private readonly DefaultJsonSerialization _serializer = new DefaultJsonSerialization();
            public T Deserialize<T>(IRestResponse response)
            {
                using (var ms = new MemoryStream(response.RawBytes, false))
                    return _serializer.Deserialize<T>(ms);
            }
        }

        private class GedcomXmlDeserializer : IDeserializer
        {
            private readonly DefaultXmlSerialization _serializer = new DefaultXmlSerialization();
            public T Deserialize<T>(IRestResponse response)
            {
                using (var ms = new MemoryStream(response.RawBytes, false))
                    return _serializer.Deserialize<T>(ms);
            }
        }

        /// <summary>
        /// Adds a filter to the current REST API client.
        /// </summary>
        /// <param name="filter">The filter to apply to the REST API client. See remarks.</param>
        /// <remarks>
        /// The filter added here will be applied for all subsequent calls to <see cref="Handle"/>. It is important to note, however, that in order for any
        /// <see cref="IFilter"/> to be applied, the <see cref="Handle"/> method must be called. Calling <see cref="O:IRestClient.Execute"/> directly will not
        /// result in these filters being applied.
        /// </remarks>
        public void AddFilter(IFilter filter)
        {
            filters.Add(filter);
        }

        /// <summary>
        /// Handles the specified request by applying all current filters.
        /// </summary>
        /// <param name="request">The REST API request that will be used to instantiate this state instance.</param>
        /// <returns></returns>
        public IRestResponse Handle(IRestRequest request)
        {
            foreach (var filter in filters)
            {
                filter.Handle(this, request);
            }

            return this.Execute(request).Result; // should change this wrapper to async
        }

        /// <summary>
        /// Handles the specified request by applying all current filters.
        /// </summary>
        /// <param name="request">The REST API request that will be used to instantiate this state instance.</param>
        /// <returns></returns>
        public IRestResponse<T> Handle<T>(IRestRequest request)
        {
            foreach (var filter in filters)
            {
                filter.Handle(this, request);
            }

            return this.Execute<T>(request).Result; // should change this wrapper to async
        }

        public bool FollowRedirects { get; set; } // not sure what to do here
    }
}
