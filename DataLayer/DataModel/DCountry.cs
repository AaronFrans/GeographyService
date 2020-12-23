using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DataModel
{
    /// <summary>
    /// A class representing a country in the database.
    /// </summary>
    public class DCountry
    {
        #region Properties

        /// <summary>
        /// Id of the country.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Population of the country.
        /// </summary>
        public int Population { get; set; }
        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Surface of the country.
        /// </summary>
        public float Surface { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// An empty constructor for EFCore
        /// </summary>
        public DCountry()
        {

        }

        /// <summary>
        /// A constructor to make a Data Country object and add it to a continent.
        /// </summary>
        /// <param name="population">The population of the country.</param>
        /// <param name="name">The name of the country.</param>
        /// <param name="surface">The surface of the country.</param>
        public DCountry(int population, string name, float surface)
        {
            Population = population;
            Name = name;
            Surface = surface;
        }

        #endregion
    }
}
