using FamilySearch.Api;
using FamilySearch.Api.Ft;
using Gx.Rs.Api;
using Gx.Rs.Api.Util;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp.Portable;

namespace Gedcomx.Rs.Api.Test
{
    [TestFixture]
    public class OrdinancesTests
    {
        private Uri ordinanceUri = new Uri("https://sandbox.familysearch.org/platform/ordinances/ordinances");

        [Test]
        public void TestReadOrdinancePolicy()
        {
            var request = new RestRequest("/platform/ordinances/policy", HttpMethod.Get).Accept("text/html");
            var client = new FilterableRestClient("https://sandbox.familysearch.org");
            var response = client.Execute(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.IsTrue(response.RawBytes);
        }

        [Test]
        public void TestReadOrdinancePolicyInFrench()
        {
            var request = new RestRequest("/platform/ordinances/policy", HttpMethod.Get).Accept("text/html").AcceptLanguage("fr");
            var client = new FilterableRestClient("https://sandbox.familysearch.org");
            var response = client.Execute(request).Result;

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            //Assert.IsNotNullOrEmpty(response.Content);
        }

        [Test]
        public void TestReadOrdinances()
        {
            var collection = new CollectionState(ordinanceUri);
            var state = collection.AuthenticateViaOAuth2Password(Resources.TestUserName, Resources.TestPassword, Resources.TestClientId).Get();

            Assert.AreEqual(HttpStatusCode.OK, state.Response.StatusCode);
        }

        [Test]
        public void TestReadOrdinancesAccessForbidden()
        {
            var collection = new CollectionState(ordinanceUri);
            var state = collection.AuthenticateViaOAuth2Password(Resources.PublicUserName, Resources.PublicPassword, Resources.TestClientId).Get();

            Assert.AreEqual(HttpStatusCode.Forbidden, state.Response.StatusCode);
        }
    }
}
