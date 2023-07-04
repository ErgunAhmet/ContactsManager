using Entities;
using Microsoft.EntityFrameworkCore;
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

		//constructor
		public CountriesServiceTest()
		{
			_countryService = new CountriesService
				(new PersonsDbContext
				(new DbContextOptionsBuilder<PersonsDbContext>().Options));
		}

		#region AddCountry
		[Fact]
		public async Task AddCountry_NullCountry()
		{
			//Arrange
			CountryAddRequest? request = null;
			//Assert 
			await Assert.ThrowsAsync<ArgumentNullException>(async () =>
			{
				await _countryService.AddCountry(request);
			});
		}
		[Fact]
		public async Task AddCountry_CountryNameIsNull()
		{
			//Arrange
			CountryAddRequest? request = new CountryAddRequest()
			{
				CountryName = null
			};
			//Assert 
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				await _countryService.AddCountry(request);
			});
		}
		[Fact]
		public async Task AddCountry_DuplicateCountryName()
		{
			//Arrange
			CountryAddRequest? request1 = new CountryAddRequest()
			{ CountryName = "USA" };
			CountryAddRequest? request2 = new CountryAddRequest()
			{ CountryName = "USA" };
			//Assert 
			await Assert.ThrowsAsync<ArgumentException>(async() =>
			{
				await _countryService.AddCountry(request1);
				await _countryService.AddCountry(request2);
			});
		}

		[Fact]
		public async Task AddCountry_ProperCountryDetails()
		{
			//Arrange
			CountryAddRequest? request = new CountryAddRequest()
			{
				CountryName = "Japan"
			};
			//Act
			CountryResponse response= await _countryService.AddCountry(request);
			List<CountryResponse> countries_from_GetAllCountries =
				await _countryService.GetAllCountries();
			//Assert 
			Assert.True(response.CountryID != Guid.Empty);
			Assert.Contains(response, countries_from_GetAllCountries);
		}
		#endregion

		#region GetAllCountries
		[Fact]
		public async Task GetAllCountries_EmptyList()
		{
			//Acts
			List<CountryResponse> actual_country_response_list = await 
				_countryService.GetAllCountries();

			//Assert
			Assert.Empty(actual_country_response_list);
		}

		[Fact]
		public async Task GetAllCountries_AddFewCountries()
		{
			List<CountryAddRequest> country_request_list = new List<CountryAddRequest>()
			{
				new CountryAddRequest() { CountryName="USA"},
				new CountryAddRequest() { CountryName="UK"}
			};

			List<CountryResponse> countries_list_from_add_country = new List<CountryResponse>();
			foreach (CountryAddRequest country_request in country_request_list)
			{
				countries_list_from_add_country.Add
					(await _countryService.AddCountry(country_request));
			}
			
			List<CountryResponse> actualCountryResponseList =await  _countryService.GetAllCountries();

			foreach(CountryResponse expected_country in countries_list_from_add_country)
			{
				Assert.Contains(expected_country, actualCountryResponseList);
			}
		}

		#endregion

		#region GetCountryByCountryID
		[Fact]
		public async Task GetCoutryByCountryID_NullCountryID()
		{
			Guid? countryID = null;

			CountryResponse country_response_from_get_method = await
				_countryService.GetCountryByCountryID(countryID);

			Assert.Null(country_response_from_get_method);
		}

		[Fact]
		public async Task GetCountryByCountryID_ValidCountryID()
		{
			CountryAddRequest? country_add_request = new
				CountryAddRequest()
			{ CountryName = "China" };
			CountryResponse country_response_from_add = await
				_countryService.AddCountry(country_add_request);

			CountryResponse? country_response_from_get = await
				_countryService.GetCountryByCountryID(country_response_from_add.CountryID);

			Assert.Equal(country_response_from_add, country_response_from_get);
		}
		#endregion
	}
}
