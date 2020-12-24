using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DataModel
{
    /// <summary>
    /// Maps Data classes to the Domain equivalent and reverse.
    /// </summary>
    public class Mapper
    {

        /// <summary>
        /// Maps a Country object to a DCountry object.
        /// </summary>
        /// <param name="country">Country to map.</param>
        /// <returns>The mapped DCountry object.</returns>
        public static DCountry ToDCountry(Country country)
        {
          return new DCountry(country.Population, country.Name, country.Surface) 
          {
              Id = country.Id 
          };
        }

        /// <summary>
        /// Maps a DCountry object to a Country object.
        /// </summary>
        /// <param name="dCountry">DCountry to map.</param>
        /// <param name="continent">Continent the country belongs to.</param>
        /// <returns>The mapped Country object.</returns>
        public static Country ToCountry(DCountry dCountry, Continent continent)
        {
            return new Country(dCountry.Population, dCountry.Name, dCountry.Surface, continent) 
            { 
                Id = dCountry.Id 
            };
        }


        /// <summary>
        /// Maps a Continent object to a DContinent object.
        /// </summary>
        /// <param name="continent">Continent to map.</param>
        /// <returns>The mapped DContinent object.</returns>
        public static DContinent ToDContinent(Continent continent)
        {
            List<DCountry> countries = new List<DCountry>();

            foreach(Country country in continent.Countries)
            {
                countries.Add(ToDCountry(country));
            }

            return new DContinent(continent.Name)
            {
                Id = continent.Id,
                Countries = countries,
                Population = continent.Population

            };
        }

        /// <summary>
        /// Maps a Continent object to a Continent object.
        /// </summary>
        /// <param name="continent">Continent to map.</param>
        /// <returns>The mapped Continent object.</returns>
        public static Continent ToContinent(DContinent dContinent)
        {
            Continent continent = new Continent(dContinent.Name)
            {
                Id = dContinent.Id
            };

            foreach (DCountry country in dContinent.Countries)
            {
                continent.AddCountry(ToCountry(country, continent));
            }


            return continent;


        }

    }
}
