using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using RentResidence.WebAPI;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace XUnitTestProject1.Fixtures
{
    class TestClient 
    {
        public HttpClient Client { get;  set;}
        private TestServer _server;

        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
      
    }
}
