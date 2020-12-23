using DomainLayer;
using DomainLayer.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainAndDataLayerTests.DomainTests
{
    [TestClass]
    public class ModelTests
    {
        [TestMethod]
        public void ContinentConstructor_ExceptionsTest()
        {
            string name = "";
            string name2 = null;
            string name3 = "    ";
            string name4 = "Actual Name";

            Action act = () => new Continent(name);

            act.Should().Throw<DomainException>().WithMessage("De naam van een continent mag nie leeg of null zijn.");

            act = () => new Continent(name2);

            act.Should().Throw<DomainException>().WithMessage("De naam van een continent mag nie leeg of null zijn.");

            act = () => new Continent(name3);

            act.Should().Throw<DomainException>().WithMessage("De naam van een continent mag nie leeg of null zijn.");

            act = () => new Continent(name4);

            act.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void CountryConstructor_ExeptionTest()
        {
            string name = "";
            string name2 = null;
            string name3 = "    ";
            string name4 = "Actual Name";

            int population = 0;
            int population1 = -5;
            int population2 = 10;

            float surface = 0.0f;
            float surface1 = -10.0f;
            float surface2 = 10.0f;

            Continent continent = new Continent("Continent name");

            Action act = () => new Country(population, name, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De populatie van een land moet groter zijn dan 0.");

            act = () => new Country(population1, name, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De populatie van een land moet groter zijn dan 0.");

            act = () => new Country(population2, name, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De naam van een land mag nie leeg of null zijn.");

            act = () => new Country(population2, name2, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De naam van een land mag nie leeg of null zijn.");

            act = () => new Country(population2, name3, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De naam van een land mag nie leeg of null zijn.");

            act = () => new Country(population2, name4, surface, continent);

            act.Should().Throw<DomainException>().WithMessage("De oppervlakte van een land moet groter zijn dan 0.");

            act = () => new Country(population2, name4, surface1, continent);

            act.Should().Throw<DomainException>().WithMessage("De oppervlakte van een land moet groter zijn dan 0.");

            act = () => new Country(population2, name4, surface2, continent);

            act.Should().NotThrow<Exception>();
        }

        [TestMethod]
        public void ContinentFunction_AddCountry_FuncionalityTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            continent.AddCountry(country);

            continent.Population.Should().Be(countryPopulation);

            continent.Countries.Count.Should().Be(1);

            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);


            continent.Population.Should().Be(countryPopulation);

            continent.AddCountry(country);


            continent.Countries.Count.Should().Be(1);

            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);



            string countryName2 = "Country2";
            int countryPopulation2 = 77000;
            float countrySurface2 = 10000.50f;
            Country country2 = new Country(countryPopulation2, countryName2, countrySurface2, continent);


            continent.AddCountry(country2);

            continent.Population.Should().Be(countryPopulation + countryPopulation2);

            continent.Countries.Count.Should().Be(2);

            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[0].Name.Should().Be(countryName);
            continent.Countries[1].Name.Should().Be(countryName2);
            continent.Countries[1].Name.Should().Be(countryName2);
            continent.Countries[1].Name.Should().Be(countryName2);
        }

        [TestMethod]
        public void ContinentFunction_AddCountry_ExceptionsTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            Action act = () => continent.AddCountry(null);

            act.Should().Throw<DomainException>().WithMessage("Een land mag niet null zijn");
        }

        [TestMethod]
        public void ContinentFunction_RemoveCountry_FuncionalityTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            continent.AddCountry(country);

            continent.RemoveCountry(country);

            continent.Population.Should().Be(0);

            continent.Countries.Count.Should().Be(0);

        }

        [TestMethod]
        public void ContinentFunction_RemoveCountry_ExceptionsTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);


            Action act = () => continent.RemoveCountry(null);

            act.Should().Throw<DomainException>().WithMessage("Er zijn geen landen in dit continent");

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            continent.AddCountry(country);

            act = () => continent.RemoveCountry(null);

            act.Should().Throw<DomainException>().WithMessage("Een land mag niet null zijn");

            string continentName2 = "Continent 2";
            Continent continent2 = new Continent(continentName2);

            string countryName2 = "Country 2";
            int countryPopulation2 = 14000;
            float countrySurface2 = 7500.50f;
            Country country2 = new Country(countryPopulation2, countryName2, countrySurface2, continent2);

            act = () => continent.RemoveCountry(country2);

            act.Should().Throw<DomainException>().WithMessage("Het gegeven land is niet in dit continent.");
        }

        [TestMethod]
        public void CountryFunction_SetContinent_FuncionalityTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string continentName2 = "Continent 2";
            Continent continent2 = new Continent(continentName2);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            continent.Countries.Count.Should().Be(1);
            continent2.Countries.Count.Should().Be(0);

            country.Continent.Name.Should().Be(continentName);


            country.SetContinent(continent2);

            continent.Countries.Count.Should().Be(0);
            continent2.Countries.Count.Should().Be(1);

            country.Continent.Name.Should().Be(continentName2);
        }

        [TestMethod]
        public void CountryFunction_SetContinent_ExceptionsTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);


            Action act =()=> country.SetContinent(null);

            act.Should().Throw<DomainException>().WithMessage("Een continent mag niet null zijn.");
        }

    }
}
