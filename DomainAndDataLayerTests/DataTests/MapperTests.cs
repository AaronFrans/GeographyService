using DataLayer.DataModel;
using DomainLayer.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainAndDataLayerTests.DataTests
{
    [TestClass]
    public class MapperTests
    {
        [TestMethod]
        public void MapperFunction_ToDCountry_FunctionalityTest()
        {

            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            DCountry mappedDCountry = Mapper.ToDCountry(country);

            mappedDCountry.Id.Should().Be(0);
            mappedDCountry.Name.Should().Be(countryName);
            mappedDCountry.Population.Should().Be(countryPopulation);
            mappedDCountry.Surface.Should().Be(countrySurface);
        }

        [TestMethod]
        public void MapperFunction_ToCountry_FunctionalityTest()
        {

            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);


            DCountry dCountry = Mapper.ToDCountry(country);
            int newCountryId = 10;
            dCountry.Id = newCountryId;


            Country mappedCountry = Mapper.ToCountry(dCountry, continent);

            mappedCountry.Id.Should().Be(newCountryId);
            mappedCountry.Name.Should().Be(countryName);
            mappedCountry.Surface.Should().Be(countrySurface);
            mappedCountry.Population.Should().Be(countryPopulation);
            mappedCountry.Continent.Should().Be(continent);

        }

        [TestMethod]
        public void MapperFunction_ToDContinent_FunctionalityTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            string countryName2 = "Country 2";
            int countryPopulation2 = 18000;
            float countrySurface2 = 7500.50f;
            Country country2 = new Country(countryPopulation2, countryName2, countrySurface2, continent);

            continent.AddCountry(country);
            continent.AddCountry(country2);

            DContinent mappedDContinent = Mapper.ToDContinent(continent);

            mappedDContinent.Name.Should().Be(continentName);
            mappedDContinent.Population.Should().Be(countryPopulation + countryPopulation2);

            mappedDContinent.Countries.Count.Should().Be(2);
            mappedDContinent.Countries[0].Name.Should().Be(countryName);
            mappedDContinent.Countries[0].Surface.Should().Be(countrySurface);
            mappedDContinent.Countries[0].Population.Should().Be(countryPopulation);
            mappedDContinent.Countries[1].Name.Should().Be(countryName2);
            mappedDContinent.Countries[1].Surface.Should().Be(countrySurface2);
            mappedDContinent.Countries[1].Population.Should().Be(countryPopulation2);

        }

        [TestMethod]
        public void MapperFunction_ToContinent_FunctionalityTest()
        {
            string continentName = "Continent";
            Continent continent = new Continent(continentName);

            string countryName = "Country";
            int countryPopulation = 14000;
            float countrySurface = 7500.50f;
            Country country = new Country(countryPopulation, countryName, countrySurface, continent);

            string countryName2 = "Country 2";
            int countryPopulation2 = 18000;
            float countrySurface2 = 7500.50f;
            Country country2 = new Country(countryPopulation2, countryName2, countrySurface2, continent);

            continent.AddCountry(country);
            continent.AddCountry(country2);

            DContinent dContinent = Mapper.ToDContinent(continent);
            int newContinentId = 1;
            int newCountryId = 2;
            int newCountryId2 = 5;
            dContinent.Id = newContinentId;
            dContinent.Countries[0].Id = newCountryId;
            dContinent.Countries[1].Id = newCountryId2;

            Continent mappedContinent = Mapper.ToContinent(dContinent);

            mappedContinent.Id.Should().Be(newContinentId);
            mappedContinent.Name.Should().Be(continentName);
            mappedContinent.Population.Should().Be(countryPopulation + countryPopulation2);

            mappedContinent.Countries.Count.Should().Be(2);
            mappedContinent.Countries[0].Id.Should().Be(newCountryId);
            mappedContinent.Countries[0].Name.Should().Be(countryName);
            mappedContinent.Countries[0].Surface.Should().Be(countrySurface);
            mappedContinent.Countries[0].Population.Should().Be(countryPopulation);
            mappedContinent.Countries[1].Id.Should().Be(newCountryId2);
            mappedContinent.Countries[1].Name.Should().Be(countryName2);
            mappedContinent.Countries[1].Surface.Should().Be(countrySurface2);
            mappedContinent.Countries[1].Population.Should().Be(countryPopulation2);
        }
    }

}
