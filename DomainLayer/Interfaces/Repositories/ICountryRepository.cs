using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces.Repositories
{
    /// <summary>
    /// A collection of Country objects in the database.
    /// </summary>
    public interface ICountryRepository
    {
        /// <summary>
        /// Add a country to the database
        /// </summary>
        /// <param name="continentId">Id of the continent the country belongs to.</param>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="countryPopulation">Population of the country.</param>
        /// <param name="countrySurface">Surface of the country.</param>
        public void AddCountry(int continentId, string countryName, int countryPopulation, float countrySurface);

        /// <summary>
        /// Retireve a country from the database using the name.
        /// </summary>
        /// <param name="name">Name of the country</param>
        /// <returns>The found country</returns>
        public Country GetCountry(string name);

        /// <summary>
        /// Retireve a country from the database using the name.
        /// </summary>
        /// <param name="id">Id of the country</param>
        /// <returns>The found country</returns>
        public Country GetCountry(int id);

        /// <summary>
        /// Check if a continent is in the database using the name.
        /// </summary>
        /// <param name="name">Name of the country.</param>
        /// <returns>True if exists.</returns>
        public bool IsInDatabaset(string name);

        /// <summary>
        /// Check if a continent is in the database using the name.
        /// </summary>
        /// <param name="id">Id of the country.</param>
        /// <returns>True if exists.</returns>
        public bool IsInDatabaset(int id);

        /// <summary>
        /// Update a country in the database.
        /// </summary>
        /// <param name="id">Id of the country to update</param>
        /// <param name="countryName">The new name of the country.</param>
        /// <param name="countryPopulation">The new population of the country.</param>
        /// <param name="countrySurface">The new surface of the country.</param>
        public void UpdateCountry(int id, string countryName, int countryPopulation, float countrySurface);

        /// <summary>
        /// Deletes a country from the database.
        /// </summary>
        /// <param name="id">Country to delete</param>
        public void DeleteCountry(int id);

        /// <summary>
        /// Checks if a counbtry belongs to a continent.
        /// </summary>
        /// <param name="ContinentId">Id of the continent.</param>
        ///  <param name="countryId">Id of the country.</param>
        public void BelongsToContinent(int ContinentId, int countryId);
    }
}
