using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    public interface IManager
    {
        /// <summary>
        /// Adds a continent to the database.
        /// </summary>
        /// <param name="continent">Contient to add.</param>
        /// <returns>The new id of the continent.</returns>
        public int AddContinent(Continent continent);

        /// <summary>
        /// Retrieves a continent from the database.
        /// </summary>
        /// <param name="continentId">Id of the continent to retrieve.</param>
        /// <returns>The retrieved continent.</returns>
        public Continent GetContinent(int continentId);

        /// <summary>
        /// Updates a continent in the database.
        /// </summary>
        /// <param name="continentId">Id of the continent to update.</param>
        /// <param name="continent">The updated continent.</param>
        public void UpdateContinent(int continentId, Continent continent);

        /// <summary>
        /// Deletes a continent from the database.
        /// </summary>
        /// <param name="continentId">Id of the continent to delete.</param>
        public void DeleteContinent(int continentId);

        /// <summary>
        /// Adds a country to the database.
        /// </summary>
        /// <param name="continentId">Id of the continent the country belongs to.</param>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="countryPopulation">Population of the country.</param>
        /// <param name="countrySurface">Surface of the country.</param>
        /// <returns>The new id of the continent.</returns>
        public int AddCountry(int continentId, string countryName, int countryPopulation, float countrySurface);

        /// <summary>
        /// Retrieves a country from the database.
        /// </summary>
        /// <param name="continentId">Id of the continent the country belongs to.</param>
        /// <param name="countryId">>Id of the country.</param>
        /// <returns>Retrieved country.</returns>
        public Country GetCountry(int continentId, int countryId);

        /// <summary>
        /// Updates a country in the database.
        /// </summary>
        /// <param name="countryId">Id of the country to update.</param>
        /// <param name="continentId">Id of the continent the country belongs to.</param>
        /// <param name="countryName">Name of the country.</param>
        /// <param name="countryPopulation">Population of the country.</param>
        /// <param name="countrySurface">Surface of the country.</param>
        public void UpdateCountry(int countryId, int continentId, string countryName, int countryPopulation, float countrySurface);

        /// <summary>
        /// Deletes a country from the database.
        /// </summary>
        /// <param name="countryId">Id of the country.</param>
        public void DeleteCountry(int continentId, int countryId);

    }
}
