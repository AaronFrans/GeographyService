using DataLayer.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{

    /// <summary>
    /// Connects to the database.
    /// </summary>
    public class GeographyContext: DbContext
    {
        #region Properties

        private string connectionString;

        /// <summary>
        /// Gives acces to the Countries in the database.
        /// </summary>
        public DbSet<DCountry> Countries { get; set; }

        /// <summary>
        /// Gives acces to the Continents in the database.
        /// </summary>
        public DbSet<DContinent> Continents { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public GeographyContext()
        {

        }

        /// <summary>
        /// Constructor used to connect to the database.
        /// </summary>
        /// <param name="db">Which database should be used, default is the production database.</param>
        public GeographyContext(string db = "Production") : base()
        {
            SetConnectionString(db);
        }

        #endregion

        #region Funcionality

        /// <summary>
        /// Gets the connection string from the AppSettings.json file.
        /// </summary>
        /// <param name="db">Determines which database should be used. The default is the production database.</param>
        private void SetConnectionString(string db = "Production")
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile(@"Files\appsettings.json", optional: false);

            var configuration = builder.Build();
            switch (db)
            {
                case "Production":
                    connectionString = configuration.GetConnectionString("ProdSQLconnection").ToString();
                    break;
                case "Test":
                    connectionString = configuration.GetConnectionString("TestSQLconnection").ToString();
                    break;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (connectionString == null)
            {
                SetConnectionString();
            }
            optionsBuilder.UseSqlServer(connectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        #endregion
    }

    /// <summary>
    /// Used to easily test the functionality of the database.
    /// </summary>
    public class GeographyContextTest : GeographyContext
    {
        #region Constructors

        /// <summary>
        /// Empty contructor, it will connect  to the test databse.
        /// </summary>
        public GeographyContextTest() : base("Test")
        {

        }

        /// <summary>
        /// Constructor used to connect to the test database.
        /// </summary>
        /// <param name="keepExistingDB">Determines if the test database should be emptied. It will delete by default.</param>
        public GeographyContextTest(bool keepExistingDB = false) : base("Test")
        {
            if (keepExistingDB)
            {
                Database.EnsureCreated();
            }
            else
            {
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }


        }

        #endregion
    }
}
