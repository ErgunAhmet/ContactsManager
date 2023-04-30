using ServiceContracts.DTO;

namespace ServiceContracts
{
	/// <summary>
	/// Represents business logic for manipulating country entity
	/// </summary>
	public interface ICountriesService
	{
		/// <summary>
		/// Adds a country object to the list of countries
		/// </summary>
		/// <param name="countryAddRequest"></param>
		/// <returns>Returns the country object after adding it 
		/// (including newly generated country id )</returns>
		CountryResponse AddCountry(CountryAddRequest? countryAddRequest);
		/// <summary>
		/// Return all countries from the list
		/// </summary>
		/// <returns>Return all countries from the list as List<CountryResponse></returns>
		List<CountryResponse> GetAllCountries();
		/// <summary>
		/// Return country by id
		/// </summary>
		/// <param name="countryID">Country (guid) to search</param>
		/// <returns></returns>
		CountryResponse GetCountryByCountryID(Guid? countryID);
	}
}
