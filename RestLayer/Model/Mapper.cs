using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestLayer.Model
{
    /// <summary>
    /// Maps Rest classes to the Domain equivalent and reverse.
    /// </summary>
    public class Mapper
    {

        /// <summary>
        /// Maps a RContinentInput object to a Continent object.
        /// </summary>
        /// <param name="input">The RContinentInput object to map.</param>
        /// <returns>The mapped Continent object.</returns>
        public static Continent ToContinent(RContinentInput input)
        {
            return new Continent(input.Name) { Id = input.Id };
        }

        /// <summary>
        /// Maps a Continent object to a RContinentOutput object.
        /// </summary>
        /// <param name="continent">The Continent object to map.</param>
        /// <param name="url">The base url.</param>
        /// <returns>The mapped RContinentOutput object.</returns>
        public static RContinentOutput ToRContinentOutput(Continent continent, string url)
        {

            string continentURL = url + "/api/continent/" + continent.Id;
            List<string> countrieURLS = new List<string>();

            foreach(Country c in continent.Countries)
            {
                countrieURLS.Add(continentURL + "/country/" + c.Id );
            }

            return new RContinentOutput()
            {
                Id = continentURL,
                Name = continent.Name,
                Population = continent.Population,
                Countries = countrieURLS

            };

        }

        /// <summary>
        /// Maps a Country object to a RCountryOutput object.
        /// </summary>
        /// <param name="country">The Country object to map.</param>
        /// <param name="url">The base url.</param>
        /// <returns>The mapped RCountryOutput object.</returns>
        public static RCountryOutput ToRCountryOutput(Country country, string url)
        {
            string continentURL = url + "/api/continent/" + country.Continent.Id;

            return new RCountryOutput()
            {
                Id = continentURL + "/country/" + country.Id,
                Population = country.Population,
                Name = country.Name,
                Surface = country.Surface,
                Continent = continentURL

            };
        }
    }
}
