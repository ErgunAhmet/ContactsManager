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
			_countryService = new CountriesService();
		}

		#region AddCountry
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
			List<CountryResponse> countries_from_GetAllCountries =
				_countryService.GetAllCountries();
			//Assert 
			Assert.True(response.CountryID != Guid.Empty);
			Assert.Contains(response, countries_from_GetAllCountries);
		}
		#endregion

		#region GetAllCountries
		[Fact]
		public void GetAllCountries_EmptyList()
		{
			//Acts
			List<CountryResponse> actual_country_response_list =
				_countryService.GetAllCountries();

			//Assert
			Assert.Empty(actual_country_response_list);
		}

		[Fact]
		public void GetAllCountries_AddFewCountries()
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
					(_countryService.AddCountry(country_request));
			}
			
			List<CountryResponse> actualCountryResponseList = _countryService.GetAllCountries();

			foreach(CountryResponse expected_country in countries_list_from_add_country)
			{
				Assert.Contains(expected_country, actualCountryResponseList);
			}
		}

		#endregion

		#region GetCountryByCountryID
		[Fact]
		public void GetCoutryByCountryID_NullCountryID()
		{
			Guid? countryID = null;

			CountryResponse country_response_from_get_method =
				_countryService.GetCountryByCountryID(countryID);

			Assert.Null(country_response_from_get_method);
		}

		[Fact]
		public void GetCountryByCountryID_ValidCountryID()
		{
			CountryAddRequest? country_add_request = new
				CountryAddRequest()
			{ CountryName = "China" };
			CountryResponse country_response_from_add =
				_countryService.AddCountry(country_add_request);

			CountryResponse? country_response_from_get =
				_countryService.GetCountryByCountryID(country_response_from_add.CountryID);

			Assert.Equal(country_response_from_add, country_response_from_get);
		}
		#endregion
	}
}
