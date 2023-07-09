using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class PersonsControllerIntegrationsTest: 
        IClassFixture<CustomWebApplicationFactory>
    {

        private readonly HttpClient _client;

        public PersonsControllerIntegrationsTest
            (CustomWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }
        #region Index

        [Fact]
        public async void Index_ToReturnView()
        {
            HttpResponseMessage response = await
                _client.GetAsync("/Persons/Index");

            response.Should().BeSuccessful();


        }
        #endregion
    }
}
