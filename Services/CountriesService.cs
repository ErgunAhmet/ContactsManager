using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using System.Collections.Generic;

namespace Services
{
	public class CountriesService : ICountriesService
	{
		//private field
		private readonly List<Country> _countries;

		public CountriesService()
		{
			_countries = new List<Country>();
		}

		public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
		{
			//validation
			if (countryAddRequest == null) 
				throw new ArgumentNullException(nameof(countryAddRequest));
			if (countryAddRequest.CountryName == null)
				throw new ArgumentException(nameof(countryAddRequest.CountryName));
			if (_countries.Where(x => x.CountryName == countryAddRequest.CountryName).Count() > 0)
				throw new ArgumentException("Given country name already exists");
			//Convert object from countryAddRequest to Country type
			Country country = countryAddRequest.ToCountry();

			//generate CountryID
			country.CountryID = Guid.NewGuid();

			//add country object into _countries
			_countries.Add(country);
			return country.ToCountryResponse();
		}

		public List<CountryResponse> GetAllCountries()
		{

			return _countries.Select(country => country.ToCountryResponse()).ToList();
		}

		public CountryResponse? GetCountryByCountryID(Guid? countryID)
		{
			if (countryID == null)
				return null;

			Country? country_response_from_list =
				_countries.FirstOrDefault(country => country.CountryID == countryID);

			if (country_response_from_list == null)
				return null;
			return country_response_from_list.ToCountryResponse();
		}
	}
}