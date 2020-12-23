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
    public class ContinentRepoTests
    {
        [TestMethod]
        public void ContinentRepositoryFunction_AddContinent_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(false);
            ContinentRepository repo = new ContinentRepository(context);

            Continent continent = new Continent("Continent");
            Continent continent2 = new Continent("Continent2");

            Continent continent3 = new Continent("Continent4");

            continent3.AddCountry(new Country(12, "Country1", 20.0f, continent3));

            repo.AddContinent(continent);
            context.SaveChanges();

            repo.AddContinent(continent2);
            context.SaveChanges();

            repo.AddContinent(continent3);
            context.SaveChanges();


            repo.GetContinent(1).Name.Should().Be(continent.Name);
            repo.GetContinent(2).Name.Should().Be(continent2.Name);

            var continentWithCountries = repo.GetContinent(3);

            continentWithCountries.Name.Should().Be(continent3.Name);

            continentWithCountries.Countries.Count.Should().Be(1);
            continentWithCountries.Countries[0].Name.Should().Be("Country1");
            continentWithCountries.Countries[0].Population.Should().Be(12);
            continentWithCountries.Countries[0].Surface.Should().Be(20.0f);
        }

        [TestMethod]
        public void ContinentRepositoryFunction_IsInDatabaseWithId_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            repo.IsInDatabase("Continent").Should().BeTrue();

            repo.IsInDatabase("Continent55").Should().BeFalse();
        }

        [TestMethod]
        public void ContinentRepositoryFunction_IsInDatabaseWithName_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            repo.IsInDatabase(1).Should().BeTrue();

            repo.IsInDatabase(29).Should().BeFalse();
        }

        [TestMethod]
        public void ContinentRepositoryFunction_GetContinentWithName_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            Continent continent = repo.GetContinent("Continent");

            continent.Name.Should().Be("Continent");


        }

        [TestMethod]
        public void ContinentRepositoryFunction_GetContinentWithId_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            Continent continent = repo.GetContinent(1);

            continent.Name.Should().Be("Continent");

            var continentWithCountries = repo.GetContinent(3);

            continentWithCountries.Name.Should().Be("Continent4");

            continentWithCountries.Countries.Count.Should().Be(1);
            continentWithCountries.Countries[0].Name.Should().Be("Country1");
            continentWithCountries.Countries[0].Population.Should().Be(12);
            continentWithCountries.Countries[0].Surface.Should().Be(20.0f);

        }

        [TestMethod]
        public void ContinentRepositoryFunction_UpdateContinent_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            

            Continent continent = repo.GetContinent(1);

            continent.Name.Should().Be("Continent");

            repo.UpdateContinent(1, new Continent("Continent3"));
            context.SaveChanges();

            continent = repo.GetContinent(1);

            continent.Name.Should().Be("Continent3");


        }

        [TestMethod]
        public void ContinentRepositoryFunction_DeleteContinent_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            repo.DeleteContinent(2);

            context.SaveChanges();

            repo.IsInDatabase(1).Should().BeTrue();
            repo.IsInDatabase(2).Should().BeFalse();

        }

        [TestMethod]
        public void ContinentRepositoryFunction_HasCountries_FunctionalityTest()
        {
            GeographyContextTest context = new GeographyContextTest(true);
            ContinentRepository repo = new ContinentRepository(context);

            repo.HasCountries(1).Should().BeFalse();
            repo.HasCountries(3).Should().BeTrue();

        }
    }
}
