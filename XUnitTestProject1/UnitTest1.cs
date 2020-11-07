using AutoMapper;
using RentResidence.Repository;
using RentResidence.WebAPI.Controllers;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using XUnitTestProject1.Fixtures;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        private readonly TestClient _testClient;
        


        public UnitTest1()
        {
            _testClient = new TestClient();
        }

        [Fact]
        public void  TestGetAllSuccess()
        {

            var clientController = new ClientController();
            var lista =  clientController.Get().IsCompleted;
            
            Assert.True(lista); 
        }

    }
}
