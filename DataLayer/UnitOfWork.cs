using DataLayer.Repositories;
using DomainLayer.Interfaces;
using DomainLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private GeographyContext context;


        public ICountryRepository Countries { get; private set; }

        public IContinentRepository Continents { get; private set; }

        public UnitOfWork(GeographyContext context)
        {
            this.context = context;
            Continents = new ContinentRepository(context);
            Countries = new CountryRepository(context);
        }

        public int Complete()
        {
            try
            {
                return context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Disposes of the context.
        /// </summary>
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
