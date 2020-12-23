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
    public class ContinentRepository : IContinentRepository
    {

        #region Properties

        private GeographyContext context;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a repository with acces to the database.
        /// </summary>
        /// <param name="context">Context to use.</param>
        public ContinentRepository(GeographyContext context)
        {
            this.context = context;
        }

        #endregion

        #region Functionality

        public void AddContinent(Continent continent)
        {

            context.Continents.Add(Mapper.ToDContinent(continent));

        }

        public Continent GetContinent(string name)
        {


            return Mapper.ToContinent(context.Continents.Include(c => c.Countries).Single(c => c.Name == name));

        }

        public Continent GetContinent(int id)
        {
            return Mapper.ToContinent(context.Continents.Include(c => c.Countries).Single(c => c.Id == id));
        }

        public bool IsInDatabase(string name)
        {
            return context.Continents.Any(c => c.Name == name);
        }

        public bool IsInDatabase(int id)
        {
            return context.Continents.Any(c => c.Id == id);
        }

        public void UpdateContinent(int id, Continent continent)
        {
            if (!IsInDatabase(id))
                throw new DataException("Er is geen continent met het gegeven id.");

            context.Continents.Single(c => c.Id == id).Name = continent.Name;

        }

        public void DeleteContinent(int id)
        {

            context.Continents.Remove(context.Continents.Single(c => c.Id == id));

        }

        public bool HasCountries(int id)
        {
            return context.Continents.Include(c => c.Countries).Single(c => c.Id == id).Countries.Count != 0;
        }

        #endregion

    }
}
