using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainLayer.Model
{
    /// <summary>
    /// A class representing a country.
    /// </summary>
    public class Country
    {

        #region Properties

        /// <summary>
        /// Id of the country.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Population of the country.
        /// </summary>
        public int Population { get; private set; }
        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Surface of the country.
        /// </summary>
        public float Surface { get; private set; }
        /// <summary>
        /// Continent the country is part of.
        /// </summary>
        public Continent Continent { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// A constructor to make a Country object and add it to a continent.
        /// </summary>
        /// <param name="population">The population of the country.</param>
        /// <param name="name">The name of the country.</param>
        /// <param name="surface">The surface of the country.</param>
        /// <param name="continent">The continent the country is part of.</param>
        public Country(int population, string name, float surface, Continent continent)
        {
            SetPopulation(population);
            SetName(name);
            SetSurface(surface);
            SetContinent(continent);

        }
        #endregion

        #region Setters

        /// <summary>
        /// Setter for the population.
        /// </summary>
        /// <param name="population">The new population.</param>
        public void SetPopulation(int population)
        {
            if (!IsValidPopulation(population))
                throw new DomainException("De populatie van een land moet groter zijn dan 0.");
            Population = population;
        }

        /// <summary>
        /// Setter for the name.
        /// </summary>
        /// <param name="name">The new name.</param>
        public void SetName(string name)
        {
            if (!IsValidName(name))
                throw new DomainException("De naam van een land mag nie leeg of null zijn.");
            Name = name;
        }

        /// <summary>
        /// Setter for the surface.
        /// </summary>
        /// <param name="surface">The new surface.</param>
        public void SetSurface(float surface)
        {
            if (!IsValidSurface(surface))
                throw new DomainException("De oppervlakte van een land moet groter zijn dan 0.");
            Surface = surface;
        }

        /// <summary>
        /// Setter for the continent.
        /// </summary>
        /// <param name="continent">The new continent.</param>
        public void SetContinent(Continent continent)
        {
            if (continent == null)
                throw new DomainException("Een continent mag niet null zijn.");
            if (Continent != continent)
            {
                if (Continent != null)
                    Continent.RemoveCountry(this);
                Continent = continent;
                if (!IsInContinent(continent))
                    continent.AddCountry(this);
            }

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

        /// <summary>
        /// Checks if the continent is valid.
        /// </summary>
        /// <param name="continent">The continent to check.</param>
        /// <returns>True if valid.</returns>
        public bool IsInContinent(Continent continent)
        {
            if (continent == Continent)
            {
                if (Continent.Countries.Contains(this))
                    return true;

                return false;

            }

            return false;

        }

        /// <summary>
        /// Checks if the surface is valid.
        /// </summary>
        /// <param name="surface">The surface to check.</param>
        /// <returns>True if valid.</returns>
        private bool IsValidSurface(float surface)
        {
            if (surface <= 0)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the population is valid.
        /// </summary>
        /// <param name="population">The population to check.</param>
        /// <returns>True if valid.</returns>
        private bool IsValidPopulation(int population)
        {
            if (population <= 0)
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is Country country &&
                   Name == country.Name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Population, Name, Surface);
        }

        #endregion

    }
}
