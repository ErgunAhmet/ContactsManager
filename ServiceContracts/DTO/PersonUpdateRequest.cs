using Entities;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts.DTO
{
	/// <summary>
	/// Represents the dto class that contains the person details to update
	/// </summary>
	public class PersonUpdateRequest
	{
		[Required(ErrorMessage = "Person ID can't be blank")]
		public Guid PersonID { get; set; }
		[Required(ErrorMessage = "Person Name can't be blank")]
		public string? PersonName { get; set; }
		[Required(ErrorMessage = "Email can't be blank")]
		[EmailAddress(ErrorMessage = "Email value should ba valid email")]
		public string? Email { get; set; }
		public DateTime? DateOfBirth { get; set; }
		public GenderOptions? Gender { get; set; }
		public Guid? CountryID { get; set; }
		public string? Address { get; set; }
		public bool ReceiveNewsLetters { get; set; }
		/// <summary>
		/// Converts the current object of PersonUpdateRequest into a new object of Person
		/// </summary>
		/// <returns>Returns person object</returns>
		public Person ToPerson()
		{
			return new Person()
			{
				PersonID= PersonID,
				PersonName = PersonName,
				Email = Email,
				DateOfBirth = DateOfBirth,
				Gender = Gender.ToString(),
				Address = Address,
				CountryID = CountryID,
				ReceiveNewsLetters = ReceiveNewsLetters
			};
		}
	}
}
