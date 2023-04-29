using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
	public class CountriesServiceTest
	{
		private readonly ICountriesService _countryService;

		public CountriesServiceTest()
		{
			_countryService = new CountriesService();
		}
		[Fact]
		public void AddCountry_NullCountry()
		{
			//Arrange
			CountryAddRequest? request = null;
			//Assert 
			Assert.Throws<ArgumentNullException>(() =>
			{
				_countryService.AddCountry(request);
			});
		}
		[Fact]
		public void AddCountry_CountryNameIsNull()
		{
			//Arrange
			CountryAddRequest? request = new CountryAddRequest()
			{
				CountryName = null
			};
			//Assert 
			Assert.Throws<ArgumentException>(() =>
			{
				_countryService.AddCountry(request);
			});
		}
		[Fact]
		public void AddCountry_DuplicateCountryName()
		{
			//Arrange
			CountryAddRequest? request1 = new CountryAddRequest()
			{ CountryName = "USA" };
			CountryAddRequest? request2 = new CountryAddRequest()
			{ CountryName = "USA" };
			//Assert 
			Assert.Throws<ArgumentException>(() =>
			{
				_countryService.AddCountry(request1);
				_countryService.AddCountry(request2);
			});
		}

		[Fact]
		public void AddCountry_ProperCountryDetails()
		{
			//Arrange
			CountryAddRequest? request = new CountryAddRequest()
			{
				CountryName = "Japan"
			};
			//Act
			CountryResponse response= _countryService.AddCountry(request);
			//Assert 
			Assert.True(response.CountryID != Guid.Empty);
		}
	}
}
