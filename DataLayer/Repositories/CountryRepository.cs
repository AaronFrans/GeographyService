using DataLayer.DataModel;
using DomainLayer.Interfaces.Repositories;
using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repositories
{
    public class CountryRepository : ICountryRepository
    {

        #region Properties

        private GeographyContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a repository with acces to the database.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public CountryRepository(GeographyContext context)
        {
            this.context = context;
        }

        #endregion

        #region Functionality

        public void AddCountry(int continentId, string countryName, int countryPopulation, float countrySurface)
        {
            DContinent dContinent = context.Continents.Include(c => c.Countries).Single(c => c.Id == continentId);

            Continent continent = Mapper.ToContinent(dContinent);

            continent.AddCountry(new Country(countryPopulation, countryName, countrySurface, continent));

            DCountry dCountry = Mapper.ToDCountry(continent.Countries.Last());

            dContinent.Countries.Add(dCountry);
            dContinent.Population = continent.Population;

        }

        public Country GetCountry(int continentId, string countryName)
        {
            Continent continent = Mapper.ToContinent(context.Continents.Include(c => c.Countries).Single(c => c.Id == continentId));
            return Mapper.ToCountry(context.Countries.Single(c => c.Name == countryName), continent);
        }

        public Country GetCountry(int continentId, int countryId)
        {
            Continent continent = Mapper.ToContinent(context.Continents.Include(c => c.Countries).Single(c => c.Id == continentId));
            return Mapper.ToCountry(context.Countries.Single(c => c.Id == countryId), continent);
        }

        public bool IsInDatabase(string name)
        {
            return context.Countries.Any(c => c.Name == name);
        }

        public bool IsInDatabase(int id)
        {
            return context.Countries.Any(c => c.Id == id);
        }

        public void UpdateCountry(int countryId, int continentId, string countryName, int countryPopulation, float countrySurface)
        {
            DCountry dCountry = context.Countries.Single(c => c.Id == countryId);

            Continent continent = Mapper.ToContinent(context.Continents.Single(c => c.Id == continentId));

            Country country = Mapper.ToCountry(dCountry, Mapper.ToContinent(context.Continents.Single(c => c.Id == continentId)));

            country.SetName(countryName);
            country.SetPopulation(countryPopulation);
            country.SetSurface(countrySurface);

            dCountry.Name = country.Name;
            dCountry.Population = country.Population;
            dCountry.Surface = country.Surface;
        }

        public void DeleteCountry(int id)
        {
            context.Countries.Remove(context.Countries.Single(c => c.Id == id));
        }

        public bool BelongsToContinent(int continentId, int countryId)
        {
            return context.Continents.Include(c => c.Countries).Single(c => c.Id == continentId)
                                     .Countries.Any(country => country.Id == countryId);
        }

        #endregion

    }
}
