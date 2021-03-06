﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataLayer.DataModel
{
    /// <summary>
    /// A class representing a continent in the database.
    /// </summary>
    public class DContinent
    {
        #region Properties

        /// <summary>
        /// Id of the continent.
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// Name of the continent.
        /// </summary>
        public string Name { get;  set; }
        /// <summary>
        /// Population of the continent.
        /// </summary>
        public int Population { get;  set; }
        /// <summary>
        /// A collection of countries belonging to the country.
        /// </summary>
        public List<DCountry> Countries { get; set; } = new List<DCountry>();

        #endregion

        #region Constructors

        /// <summary>
        /// An empty constructor for EFCore
        /// </summary>
        public DContinent()
        {

        }

        /// <summary>
        /// A constructor to make a Data Continent object.
        /// </summary>
        /// <param name="name">The name of the continent.</param>
        public DContinent(string name)
        {
            Name = name;
        }

        #endregion

    }
}
