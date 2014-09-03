using System;
using RestSharp;
using System.Text;
using System.Collections.Generic;
using Gx.Conclusion;
using Gx.Rs.Api.Util;
using Gx.Links;
using System.Net;
using System.Linq;
using Newtonsoft.Json;
using Gx.Records;
using Gx.Common;
using Gedcomx.Model;

namespace Gx.Rs.Api
{
    public abstract class GedcomxApplicationState : HypermediaEnabledData
    {
    }

    public abstract class GedcomxApplicationState<T> : GedcomxApplicationState where T : class, new()
    {
        protected static readonly EmbeddedLinkLoader DEFAULT_EMBEDDED_LINK_LOADER = new EmbeddedLinkLoader();

        internal readonly StateFactory stateFactory;
        public IRestClient Client { get; private set; }
        public String CurrentAccessToken { get; set; }
        private Tavis.LinkFactory linkFactory;
        private Tavis.LinkHeaderParser linkHeaderParser;
        public bool IsAuthenticated { get { return CurrentAccessToken != null; } }
        public IRestRequest Request { get; private set; }
        public IRestResponse Response { get; private set; }
        public T Entity { get; private set; }
        protected abstract GedcomxApplicationState Clone(IRestRequest request, IRestResponse response, IRestClient client);
        protected abstract T LoadEntity(IRestResponse response);
        protected abstract SupportsLinks MainDataElement { get; }
        private IRestRequest lastEmbeddedRequest;
        private IRestResponse lastEmbeddedResponse;


        protected GedcomxApplicationState()
        {
            linkFactory = new Tavis.LinkFactory();
            linkHeaderParser = new Tavis.LinkHeaderParser(linkFactory);
        }

        protected GedcomxApplicationState(IRestRequest request, IRestResponse response, IRestClient client, String accessToken, StateFactory stateFactory)
            : this()
        {
            this.Request = request;
            this.Response = response;
            this.Client = client;
            this.CurrentAccessToken = accessToken;
            this.stateFactory = stateFactory;
            this.Entity = LoadEntityConditionally(this.Response);
            List<Link> links = LoadLinks(this.Response, this.Entity, this.Request.RequestFormat);
            this.Links = new List<Link>();
            this.Links.AddRange(links);
        }

        protected T LoadEntityConditionally(IRestResponse response)
        {
            if (Request.Method != Method.HEAD && Request.Method != Method.OPTIONS && response.StatusCode == HttpStatusCode.OK)
            {
                return LoadEntity(response);
            }
            else
            {
                return null;
            }
        }

        public GedcomxApplicationState Inject(IRestRequest request)
        {
            return Clone(request, Invoke(request), this.Client);
        }

        protected List<Link> LoadLinks(IRestResponse response, T entity, DataFormat contentFormat)
        {
            List<Link> links = new List<Link>();

            //if there's a location, we'll consider it a "self" link.
            if (response.ResponseUri != null)
            {
                links.Add(new Link() { Rel = Rel.SELF, Href = response.ResponseUri.ToString() });
            }

            //initialize links with link headers
            foreach (var header in response.Headers.Where(x => x.Name == "Link" && x.Value != null).SelectMany(x => linkHeaderParser.Parse(response.ResponseUri, x.Value.ToString())))
            {
                Link link = new Link() { Rel = header.Relation, Href = header.Target.ToString() };
                link.Template = header.GetLinkExtensionSafe("template");
                link.Title = header.Title;
                link.Accept = header.GetLinkExtensionSafe("accept");
                link.Allow = header.GetLinkExtensionSafe("allow");
                link.Hreflang = header.HrefLang.Select(x => x.Name).FirstOrDefault();
                link.Type = header.GetLinkExtensionSafe("type");
                links.Add(link);
            }

            //load the links from the main data element
            SupportsLinks mainElement = MainDataElement;
            if (mainElement != null && mainElement.Links != null)
            {
                links.AddRange(mainElement.Links);
            }

            //load links at the document level
            var collection = entity as Collection;
            if (entity != mainElement && collection != null && collection.Links != null)
            {
                links.AddRange(collection.Links);
            }


            return links;
        }

        public string GetUri()
        {
            return this.Client.BaseUrl + this.Request.Resource;
        }

        public bool HasError()
        {
            return this.Response.HasClientError() || this.Response.HasServerError();
        }

        public bool HasStatus(ResponseStatus status)
        {
            return this.Response.ResponseStatus == status;
        }

        internal IRestResponse Invoke(IRestRequest request, params StateTransitionOption[] options)
        {
            string originalBaseUrl = this.Client.BaseUrl;
            string originalResource = request.Resource;
            bool restore = false;
            IRestResponse result;

            foreach (StateTransitionOption option in options)
            {
                option.Apply(request);
            }

            Uri uri;
            if (Uri.TryCreate(request.Resource, UriKind.RelativeOrAbsolute, out uri))
            {
                if (uri.IsAbsoluteUri)
                {
                    restore = true;
                    this.Client.BaseUrl = uri.GetLeftPart(UriPartial.Authority);
                    request.Resource = uri.GetComponents(UriComponents.PathAndQuery, UriFormat.UriEscaped);
                }
            }

            result = this.Client.Execute(request);

            if (restore)
            {
                this.Client.BaseUrl = originalBaseUrl;
                request.Resource = originalResource;
            }

            return result;
        }

        public virtual GedcomxApplicationState IfSuccessful()
        {
            if (HasError())
            {
                throw new GedcomxApplicationException(String.Format("Unsuccessful {0} to {1}", this.Request.Method, GetUri()), this.Response);
            }
            return this;
        }

        public virtual GedcomxApplicationState Head(params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();

            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            if (accept != null)
            {
                request.AddParameter(accept);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.HEAD;

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState Options(params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            if (accept != null)
            {
                request.AddParameter(accept);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.OPTIONS;

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState Get(params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            if (accept != null)
            {
                request.AddParameter(accept);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.GET;

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState Delete(params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            if (accept != null)
            {
                request.AddParameter(accept);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.DELETE;

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState Put(T entity, params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            Parameter contentType = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Content-Type");

            if (accept != null)
            {
                request.AddParameter(accept);
            }

            if (contentType != null)
            {
                request.AddParameter(contentType);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.PUT;
#warning Need to resolve "builder.entity(entity)" pattern
            request.AddObject(entity);

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState Post(T entity, params StateTransitionOption[] options)
        {
            IRestRequest request = CreateAuthenticatedRequest();
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            Parameter contentType = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Content-Type");

            if (accept != null)
            {
                request.AddParameter(accept);
            }

            if (contentType != null)
            {
                request.AddParameter(contentType);
            }

            request.Resource = GetSelfUri().ToString();
            request.Method = Method.POST;
#warning Need to resolve "builder.entity(entity)" pattern
            request.AddObject(entity);

            return Clone(request, Invoke(request, options), this.Client);
        }

        public virtual GedcomxApplicationState AuthenticateViaOAuth2Password(String username, String password, String clientId)
        {
            return AuthenticateViaOAuth2Password(username, password, clientId, null);
        }

        public virtual GedcomxApplicationState AuthenticateViaOAuth2Password(String username, String password, String clientId, String clientSecret)
        {
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("grant_type", "password");
            formData.Add("username", username);
            formData.Add("password", password);
            formData.Add("client_id", clientId);
            if (clientSecret != null)
            {
                formData.Add("client_secret", clientSecret);
            }
            return AuthenticateViaOAuth2(formData);
        }

        public GedcomxApplicationState AuthenticateViaOAuth2AuthCode(String authCode, String redirect, String clientId)
        {
            return AuthenticateViaOAuth2Password(authCode, authCode, clientId, null);
        }

        public GedcomxApplicationState AuthenticateViaOAuth2AuthCode(String authCode, String redirect, String clientId, String clientSecret)
        {
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("grant_type", "authorization_code");
            formData.Add("code", authCode);
            formData.Add("redirect_uri", redirect);
            formData.Add("client_id", clientId);
            if (clientSecret != null)
            {
                formData.Add("client_secret", clientSecret);
            }
            return AuthenticateViaOAuth2(formData);
        }

        public GedcomxApplicationState AuthenticateViaOAuth2ClientCredentials(String clientId, String clientSecret)
        {
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("grant_type", "client_credentials");
            formData.Add("client_id", clientId);
            if (clientSecret != null)
            {
                formData.Add("client_secret", clientSecret);
            }
            return AuthenticateViaOAuth2(formData);
        }

        public GedcomxApplicationState AuthenticateWithAccessToken(String accessToken)
        {
            this.CurrentAccessToken = accessToken;
            return this;
        }

        public GedcomxApplicationState AuthenticateViaOAuth2(IDictionary<String, String> formData, params StateTransitionOption[] options)
        {
            Link tokenLink = this.GetLink(Rel.OAUTH2_TOKEN);
            if (tokenLink == null || tokenLink.Href == null)
            {
                throw new GedcomxApplicationException(String.Format("No OAuth2 token URI supplied for resource at {0}.", GetUri()));
            }

            IRestRequest request = CreateRequest();

            request.SetDataFormat(DataFormat.Json);
            request.AddHeader("Content-Type", MediaTypes.APPLICATION_FORM_URLENCODED_TYPE);
#warning Need to resolve ".entity(formData)" pattern
            foreach (var key in formData.Keys)
            {
                request.AddParameter(key, formData[key]);
            }

            request.Resource = tokenLink.Href.ToString();
            request.Method = Method.POST;

            IRestResponse response = Invoke(request, options);

            // TODO: Confirm response status SUCCESS = ResponseStatus.Completed
            if (response.ResponseStatus == ResponseStatus.Completed)
            {
#warning Once entity is encoded correctly, need to confirm retrieval pattern
                var accessToken = JsonConvert.DeserializeObject<IDictionary<string, object>>(response.Content);
                String access_token = null;

                if (accessToken.ContainsKey("access_token"))
                {
                    access_token = accessToken["access_token"] as string;
                }
                if (access_token == null && accessToken.ContainsKey("token"))
                {
                    //workaround to accommodate providers that were built on an older version of the oauth2 specification.
                    access_token = accessToken["token"] as string;
                }

                if (access_token == null)
                {
                    throw new GedcomxApplicationException("Illegal access token response: no access_token provided.", response);
                }

                return AuthenticateWithAccessToken(access_token);
            }
            else
            {
                throw new GedcomxApplicationException("Unable to obtain an access token.", response);
            }
        }

        protected GedcomxApplicationState ReadPage(String rel, params StateTransitionOption[] options)
        {
            Link link = this.GetLink(rel);
            if (link == null || link.Href == null)
            {
                return null;
            }

            IRestRequest request = CreateAuthenticatedRequest();
            request.Resource = link.Href.ToString();
            request.Method = Method.GET;
            Parameter accept = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Accept");
            Parameter contentType = this.Request.Parameters.FirstOrDefault(x => x.Type == ParameterType.HttpHeader && x.Name == "Content-Type");
            if (accept != null)
            {
                request.AddHeader(accept.Name, accept.Value as string);
            }
            if (contentType != null)
            {
                request.AddHeader(contentType.Name, contentType.Value as string);
            }
            return Clone(request, Invoke(request, options), this.Client);
        }

        protected GedcomxApplicationState ReadNextPage(params StateTransitionOption[] options)
        {
            return ReadPage(Rel.NEXT);
        }

        protected GedcomxApplicationState ReadPreviousPage(params StateTransitionOption[] options)
        {
            return ReadPage(Rel.PREVIOUS);
        }

        protected GedcomxApplicationState ReadFirstPage(params StateTransitionOption[] options)
        {
            return ReadPage(Rel.FIRST);
        }

        protected GedcomxApplicationState readLastPage(params StateTransitionOption[] options)
        {
            return ReadPage(Rel.LAST);
        }

        protected IRestRequest CreateAuthenticatedFeedRequest()
        {
            return CreateAuthenticatedRequest().SetDataFormat(DataFormat.Json);
        }

        protected void IncludeEmbeddedResources(Gedcomx entity, params StateTransitionOption[] options)
        {
            Embed(EmbeddedLinkLoader.LoadEmbeddedLinks(entity), entity, options);
        }

        protected void Embed(IEnumerable<Link> links, Gedcomx entity, params StateTransitionOption[] options)
        {
            foreach (Link link in links)
            {
                Embed(link, entity, options);
            }
        }

        protected EmbeddedLinkLoader EmbeddedLinkLoader
        {
            get
            {
                return DEFAULT_EMBEDDED_LINK_LOADER;
            }
        }

        protected void Embed(Link link, Gedcomx entity, params StateTransitionOption[] options)
        {
            if (link.Href != null)
            {
                lastEmbeddedRequest = CreateRequestForEmbeddedResource(link.Rel).Build(link.Href, Method.GET);
                lastEmbeddedResponse = Invoke(lastEmbeddedRequest, options);
                if (lastEmbeddedResponse.StatusCode == HttpStatusCode.OK)
                {
                    //entity.Embed(lastEmbeddedResponse.Data as Gedcomx);
                }
                else if (lastEmbeddedResponse.HasServerError())
                {
                    // TODO: Confirm lastEmbeddedResponse.StatusDescription gives good human readable status; otherwise, revert to lastEmbeddedResponse.StatusCode.ToString() (the enum name)
                    throw new GedcomxApplicationException(String.Format("Unable to load embedded resources: server says \"{0}\" at {1}.", lastEmbeddedResponse.StatusDescription, lastEmbeddedRequest.Resource), lastEmbeddedResponse);
                }
                else
                {
                    //todo: log a warning? throw an error?
                }
            }
        }

        protected IRestRequest CreateRequestForEmbeddedResource(String rel)
        {
            return CreateAuthenticatedGedcomxRequest();
        }

        public AgentState ReadContributor(params StateTransitionOption[] options)
        {
            var scope = MainDataElement;
            if (scope is Attributable)
            {
                return ReadContributor((Attributable)scope, options);
            }
            else
            {
                return null;
            }
        }

        public AgentState ReadContributor(Attributable attributable, params StateTransitionOption[] options)
        {
            Attribution attribution = attributable.Attribution;
            if (attribution == null)
            {
                return null;
            }

            return ReadContributor(attribution.Contributor, options);
        }

        public AgentState ReadContributor(ResourceReference contributor, params StateTransitionOption[] options)
        {
            if (contributor == null || contributor.Resource == null)
            {
                return null;
            }

            IRestRequest request = CreateAuthenticatedGedcomxRequest().Build(contributor.Resource, Method.GET);
            return this.stateFactory.NewAgentState(request, Invoke(request, options), this.Client, this.CurrentAccessToken);
        }

        protected IRestRequest CreateAuthenticatedRequest()
        {
            IRestRequest request = CreateRequest();
            if (this.CurrentAccessToken != null)
            {
                request = request.AddHeader("Authorization", "Bearer " + this.CurrentAccessToken);
            }
            return request;
        }

        internal IRestRequest CreateAuthenticatedGedcomxRequest()
        {
            IRestRequest result = CreateAuthenticatedRequest();

            result.SetDataFormat(DataFormat.Json);
            result.AddHeader("Content-Type", MediaTypes.GEDCOMX_JSON_MEDIA_TYPE);

            return result;
        }

        protected IRestRequest CreateRequest()
        {
            return new RestRequest();
        }

        public string GetSelfUri()
        {
            String selfRel = SelfRel;
            Link link = null;
            if (selfRel != null)
            {
                link = this.GetLink(selfRel);
            }
            link = link == null ? this.GetLink(Rel.SELF) : link;
            String self = link == null ? null : link.Href == null ? null : link.Href;
            return self == null ? GetUri() : self;
        }

        public virtual String SelfRel
        {
            get
            {
                return null;
            }
        }
    }
}
