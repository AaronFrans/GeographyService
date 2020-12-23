using DataLayer;
using DataLayer.Repositories;
using DomainLayer.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainAndDataLayerTests.DataTests
{
    [TestClass]
    public class CountryRepoTests
    {
        [TestMethod]
        public void CountryRepositoryFunction_AddCountry_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);
            ContinentRepository continentRepo = new ContinentRepository(context);

            Continent continent = new Continent("Continent60");
            context.SaveChanges();

            continentRepo.AddContinent(continent);
            context.SaveChanges();

            countryRepo.AddCountry(4, "Country60", 20, 30.0f);
            context.SaveChanges();

            var countryFromRepo = countryRepo.GetCountry(4, 2);

            continentRepo.HasCountries(4).Should().BeTrue();

            countryFromRepo.Name.Should().Be("Country60");
            countryFromRepo.Population.Should().Be(20);
            countryFromRepo.Surface.Should().Be(30.0f);
            countryFromRepo.Continent.Name.Should().Be("Continent60");

        }

        [TestMethod]
        public void CountryRepositoryFunction_GetCountryWithName_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            Country country = countryRepo.GetCountry(4, "Country60");

            country.Name.Should().Be("Country60");
            country.Population.Should().Be(20);
            country.Surface.Should().Be(30.0f);
            country.Continent.Name.Should().Be("Continent60");

        }

        [TestMethod]
        public void CountryRepositoryFunction_GetCountryWithId_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            Country country = countryRepo.GetCountry(4, 2);

            country.Name.Should().Be("Country60");
            country.Population.Should().Be(20);
            country.Surface.Should().Be(30.0f);
            country.Continent.Name.Should().Be("Continent60");
        }

        [TestMethod]
        public void CountryRepositoryFunction_IsInDatabaseWithId_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            countryRepo.IsInDatabase(2).Should().BeTrue();
            countryRepo.IsInDatabase(10).Should().BeFalse();
        }

        [TestMethod]
        public void CountryRepositoryFunction_IsInDatabaseWithName_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);


            countryRepo.IsInDatabase("Country60").Should().BeTrue();
            countryRepo.IsInDatabase("Country 60").Should().BeFalse();
        }

        [TestMethod]
        public void CountryRepositoryFunction_UpdateCountry_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            Country country = countryRepo.GetCountry(4, 2);

            country.Name.Should().Be("Country60");
            country.Population.Should().Be(20);
            country.Surface.Should().Be(30.0f);
            country.Continent.Name.Should().Be("Continent60");

            countryRepo.UpdateCountry(2, 4, "NewCountry60", 99, 55.5f);
            context.SaveChanges();

            country = countryRepo.GetCountry(4, 2);

            country.Name.Should().Be("NewCountry60");
            country.Population.Should().Be(99);
            country.Surface.Should().Be(55.5f);
            country.Continent.Name.Should().Be("Continent60");

        }


        [TestMethod]
        public void CountryRepositoryFunction_DeleteCountry_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            Country country = countryRepo.GetCountry(4, 2);

            country.Name.Should().Be("Country60");
            country.Population.Should().Be(20);
            country.Surface.Should().Be(30.0f);
            country.Continent.Name.Should().Be("Continent60");

            countryRepo.DeleteCountry(1);
            context.SaveChanges();

            countryRepo.IsInDatabase(1).Should().BeFalse();
            countryRepo.IsInDatabase(2).Should().BeTrue();
        }

        [TestMethod]
        public void CountryRepositoryFunction_BelongsToContinent_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            CountryRepository countryRepo = new CountryRepository(context);

            Country country = countryRepo.GetCountry(4, 2);

            country.Name.Should().Be("Country60");
            country.Population.Should().Be(20);
            country.Surface.Should().Be(30.0f);
            country.Continent.Name.Should().Be("Continent60");

            countryRepo.BelongsToContinent(4, 2).Should().BeTrue();
            countryRepo.BelongsToContinent(1, 2).Should().BeFalse();
        }

    }
}
