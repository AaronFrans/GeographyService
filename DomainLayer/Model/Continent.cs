using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainLayer.Model
{
    /// <summary>
    /// A class representing a continent.
    /// </summary>
    public class Continent
    {

        #region Properties
        /// <summary>
        /// Id of the continent.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name of the continent.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Population of the continent.
        /// </summary>
        public int Population { get; private set; }
        /// <summary>
        /// A collection of countries belonging to the continent.
        /// </summary>
        private List<Country> countries = new List<Country>();
        /// <summary>
        /// A collection of countries belonging to the continent.
        /// </summary>
        public IReadOnlyList<Country> Countries => countries.AsReadOnly();
        #endregion

        #region Constructors

        /// <summary>
        /// A constructor to make a Continent object.
        /// </summary>
        /// <param name="name">The name of the continent.</param>
        public Continent(string name)
        {
            SetName(name);
            Population = 0;
        }

        #endregion

        #region Setters

        /// <summary>
        /// Setter for the name.
        /// </summary>
        /// <param name="name">The new name.</param>
        public void SetName(string name)
        {
            if (!IsValidName(name))
                throw new DomainException("De naam van een continent mag nie leeg of null zijn.");
            Name = name;
        }

        #endregion

        #region Validation

        /// <summary>
        /// Checks if the name is valid.
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>True if valid.</returns>
        private bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Continent continent &&
                   Name == continent.Name &&
                   Population == continent.Population &&
                   EqualityComparer<List<Country>>.Default.Equals(countries, continent.countries);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Population, countries);
        }

        #endregion

        #region Funcionality

        /// <summary>
        /// Add a country to the continent.
        /// </summary>
        /// <param name="country">Country to add.</param>
        public void AddCountry(Country country)
        {
            if (country == null)
                throw new DomainException("Een land mag niet null zijn");

            if (country.IsInContinent(this))
            {
                if (!countries.Contains(country))
                {
                    countries.Add(country);
                    Population += country.Population;
                }

            }
            else
            {
                if (!countries.Contains(country))
                {
                    countries.Add(country);
                    Population += country.Population;
                }
                country.SetContinent(this);

            }


        }

        /// <summary>
        /// Remove a country to the continent.
        /// </summary>
        /// <param name="country">Country to remove.</param>
        public void RemoveCountry(Country country)
        {
            if (countries.Count == 0)
                throw new DomainException("Er zijn geen landen in dit continent");

            if (country == null)
                throw new DomainException("Een land mag niet null zijn");


            if (countries.Remove(country))
                Population -= country.Population;
            else
                throw new DomainException("Het gegeven land is niet in dit continent.");


        }

        #endregion
    }
}
