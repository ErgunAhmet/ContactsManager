using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// DTo class that is used as return type for most of countriesservice methods
	/// </summary>
	public class CountryResponse
	{
		public Guid CountryID { get; set; }
		public string? CountryName { get; set; }

	}
	public static class CountryExtensions
	{
		public static CountryResponse ToCountryResponse(this Country country)
		{
			return new CountryResponse()
			{
				CountryID = country.CountryID,
				CountryName = country.CountryName
			};
		}
	}
}
