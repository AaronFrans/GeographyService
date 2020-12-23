using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces.Repositories
{
    /// <summary>
    /// A collection of Continent objects in the database.
    /// </summary>
    public interface IContinentRepository
    {
        /// <summary>
        /// Add a continent to the database.
        /// </summary>
        /// <param name="continent">Continent to add.</param>
        public void AddContinent(Continent continent);

        /// <summary>
        /// Retrieve a continent to the database using the name.
        /// </summary>
        /// <param name="name">Name of the continent.</param>
        /// <returns>The continent found.</returns>
        public Continent GetContinent(string name);

        /// <summary>
        /// Retrieve a continent to the database using the id.
        /// </summary>
        /// <param name="id">Id of the continent.</param>
        /// <returns>The continent found.</returns>
        public Continent GetContinent(int id);

        /// <summary>
        /// Check if a continent is in the database using the name.
        /// </summary>
        /// <param name="name">Name of the continent.</param>
        /// <returns>True if exists</returns>
        public bool IsInDatabase(string name);

        /// <summary>
        /// Check if a continent is in the database using the id.
        /// </summary>
        /// <param name="id">Id of the continent.</param>
        /// <returns>True if exists</returns>
        public bool IsInDatabase(int id);

        /// <summary>
        /// Update a continent in the database.
        /// </summary>
        /// <param name="id">Id of the continent to update.</param>
        /// <param name="continent">New continent</param>
        /// <returns></returns>
        public void UpdateContinent(int id, Continent continent);

        /// <summary>
        /// Remove a country out of tyhe database.
        /// </summary>
        /// <param name="id">Id of the continent to delete.</param>
        public void DeleteContinent(int id);

        /// <summary>
        /// Check if a given continent still has countries.
        /// </summary>
        /// <param name="id">Id of the continent to check.</param>
        /// <returns>True if not empty.</returns>
        public bool HasCountries(int id);
    }
}
