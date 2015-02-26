using Gx.Rs.Api;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Gx.Rs.Api.Util;
using NUnit.Framework;
using Gx.Links;
using System.Net;
using System.Net.Http;
using CsQuery;
using Gedcomx.Support;
using RestSharp.Portable;

namespace Gedcomx.Rs.Api.Test
{
    [TestFixture]
    public class AuthenticationTests
    {
        private static readonly String SANDBOX_URI = "https://sandbox.familysearch.org/platform/collections/tree";

        [Test]
        public void TestDeleteAccessToken()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            collection.AuthenticateViaOAuth2Password(Resources.TestUserName, Resources.TestPassword, Resources.TestClientId);
            Assert.IsTrue(collection.IsAuthenticated);
            Link link = collection.GetLink(Rel.OAUTH2_TOKEN);
            IRestRequest request = new RestRequest()
                .Accept(MediaTypes.APPLICATION_JSON_TYPE)
                .Build(link.Href + "?access_token=" + collection.CurrentAccessToken, HttpMethod.Delete);
            IRestResponse response = collection.Client.Handle(request);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public void TestObtainAccessTokenBadParameters()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            Link tokenLink = collection.GetLink(Rel.OAUTH2_TOKEN);
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("grant_type", "authorization_code");
            formData.Add("code", "tGzv3JOkF0XG5Qx2TlKWIA");
            formData.Add("client_id", "WCQY-7J1Q-GKVV-7DNM-SQ5M-9Q5H-JX3H-CMJK");
            IRestRequest request = new RestRequest()
                .Accept(MediaTypes.APPLICATION_JSON_TYPE)
                .ContentType(MediaTypes.APPLICATION_FORM_URLENCODED_TYPE)
                .SetEntity(formData)
                .Build(tokenLink.Href, HttpMethod.Post);
            IRestResponse response = collection.Client.Handle(request);

            Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Test]
        public void TestObtainAccessTokenWithUsernameAndPassword()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            var state = collection.AuthenticateViaOAuth2Password(Resources.TestUserName, Resources.TestPassword, Resources.TestClientId);
            Assert.AreEqual(HttpStatusCode.OK, state.Response.StatusCode);
            Assert.IsNotNullOrEmpty(state.CurrentAccessToken);
        }

        [Test]
        public void TestObtainAccessTokenWithoutAuthenticating()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            var response = new WebClient().DownloadString("http://checkip.dyndns.com/");
            var ip = new CQ(response).Select("body").Text().Split(new string[] { ": " }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
            var state = collection.UnauthenticatedAccess(ip, "WCQY-7J1Q-GKVV-7DNM-SQ5M-9Q5H-JX3H-CMJK");
            Assert.AreEqual(HttpStatusCode.OK, state.Response.StatusCode);
            Assert.IsNotNullOrEmpty(state.CurrentAccessToken);
        }

        [Test]
        public void TestInitiateAuthorizationGet()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            Link tokenLink = collection.GetLink(Rel.OAUTH2_AUTHORIZE);
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("response_type", "code");
            formData.Add("client_id", "ABCD-EFGH-JKLM-NOPQ-RSTU-VWXY-0123-4567");
            formData.Add("redirect_uri", "https://familysearch.org/developers/sandbox-oauth2-redirect");
            IRestRequest request = new RestRequest()
                .SetEntity(formData)
                .Build(tokenLink.Href, HttpMethod.Get);
            IRestResponse response = collection.Client.Handle(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var cq = CQ.Create(new MemoryStream(response.RawBytes));
            Assert.AreEqual(1, cq.Select("input#userName").Length);
            Assert.AreEqual(1, cq.Select("input#password").Length);
        }

        [Test]
        public void TestInitiateAuthorizationInvalidParameter()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            Link tokenLink = collection.GetLink(Rel.OAUTH2_AUTHORIZE);
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("response_type", "authorize_me");
            formData.Add("client_id", "ABCD-EFGH-JKLM-NOPQ-RSTU-VWXY-0123-4567");
            formData.Add("redirect_uri", "https://familysearch.org/developers/sandbox-oauth2-redirect");
            IRestRequest request = new RestRequest()
                .ContentType(MediaTypes.APPLICATION_FORM_URLENCODED_TYPE)
                .SetEntity(formData)
                .Build(tokenLink.Href, HttpMethod.Post);
            IRestResponse response = collection.Client.Handle(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var cq = CQ.Create(new MemoryStream(response.RawBytes));
            Assert.AreEqual(1, cq.Select("p#error").Length);
        }

        [Test]
        public void TestInitiateAuthorizationPost()
        {
            var collection = new CollectionState(new Uri(SANDBOX_URI));
            Link tokenLink = collection.GetLink(Rel.OAUTH2_AUTHORIZE);
            IDictionary<String, String> formData = new Dictionary<String, String>();
            formData.Add("response_type", "code");
            formData.Add("client_id", "ABCD-EFGH-JKLM-NOPQ-RSTU-VWXY-0123-4567");
            formData.Add("redirect_uri", "https://familysearch.org/developers/sandbox-oauth2-redirect");
            IRestRequest request = new RestRequest()
                .SetEntity(formData)
                .Build(tokenLink.Href, HttpMethod.Post);
            IRestResponse response = collection.Client.Handle(request);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var cq = CQ.Create(new MemoryStream(response.RawBytes));
            Assert.AreEqual(1, cq.Select("input#userName").Length);
            Assert.AreEqual(1, cq.Select("input#password").Length);
        }
    }
}
