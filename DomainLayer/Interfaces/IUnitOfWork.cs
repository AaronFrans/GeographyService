using DomainLayer.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer.Interfaces
{
    /// <summary>
    /// Gives commands to the database.
    /// </summary>
    interface IUnitOfWork
    {
        /// <summary>
        /// Countries in the database.
        /// </summary>
        public ICountryRepository Countries { get; }

        /// <summary>
        /// Continents in the database.
        /// </summary>
        public IContinentRepository Continents { get; }

        /// <summary>
        /// Complete tasks done by the database.
        /// </summary>
        /// <returns>An int signifying whether the task was succsefull.</returns>
        int Complete();

    }
}
