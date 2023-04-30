using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
	/// <summary>
	/// Represents business logic for manipulating person entity
	/// </summary>
	public interface IPersonsService
	{
		/// <summary>
		/// Adds a new person into the list of person
		/// </summary>
		/// <param name="personAddRequest">Person To Add</param>
		/// <returns>Returns the same person details,
		/// along with newly generated PersonID</returns>
		PersonResponse AddPerson(PersonAddRequest? personAddRequest);
		/// <summary>
		/// Returns all person
		/// </summary>
		/// <returns>Returns a list of objects of PersonResponse type</returns>
		List<PersonResponse> GetAllPersons();
		/// <summary>
		/// Get person by person Id
		/// </summary>
		/// <param name="PersonID">Person id to search</param>
		/// <returns>Returns matching person object</returns>
		PersonResponse? GetPersonByPersonID(Guid? PersonID);
		/// <summary>
		/// Returns all persons that matches with the given search field and search string
		/// </summary>
		/// <param name="searchBy">Search field to search</param>
		/// <param name="searchString">Search string to search</param>
		/// <returns>Returns all matching persons based on the given search field
		/// and search string</returns>
		List<PersonResponse>? GetFilteredPersons(string searchBy, string? searchString);
		/// <summary>
		/// Return sorted list of persons
		/// </summary>
		/// <param name="allPersons">Represents list of persons to sort</param>
		/// <param name="sortBy">Name of the property (key),
		/// based on which the persons should be sorted</param>
		/// <param name="sortOrder">ASC or DESC</param>
		/// <returns>Returns sorted persons as PersonResponse list</returns>
		List<PersonResponse>? GetSortedPersons
			(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder);
		/// <summary>
		/// Updates the specified person details based on the given person ID
		/// </summary>
		/// <param name="personUpdateRequest">Person details to update, including person id</param>
		/// <returns>Returns the person response object after update</returns>
		PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest);
		/// <summary>
		/// Deletes the specified person based on the given person ID
		/// </summary>
		/// <param name="personID">PersonID to delete</param>
		/// <returns>returns true if the deletion is successfull, false if not</returns>
		bool DeletePerson(Guid? personID);
	}
}
