using DataLayer;
using DomainLayer;
using DomainLayer.Model;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DomainAndDataLayerTests.DomainTests
{
    [TestClass]
    public class ManagerTests
    {
        [TestMethod]
        public void ManagerFunction_AddContinent_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(false)));

            Continent continent = new Continent("continent 5");

            manager.AddContinent(continent);

            Action act = () => manager.AddContinent(continent);

            act.Should().Throw<DomainException>().WithMessage("Er is al een continent met de gegeven naam.");
        }

        [TestMethod]
        public void ManagerFunction_AddContinent_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Continent continent = new Continent("continent 1");
            Continent continent2 = new Continent("continent 2");
            Continent continent3 = new Continent("continent 4");


            manager.AddContinent(continent);
            manager.AddContinent(continent2);
            manager.AddContinent(continent3);

            manager.GetContinent(2).Name.Should().Be(continent.Name);
            manager.GetContinent(3).Name.Should().Be(continent2.Name);
        }

        [TestMethod]
        public void ManagerFunction_GetContinent_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.GetContinent(55);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");
        }

        [TestMethod]
        public void ManagerFunction_GetContinent_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));
            

            Continent continentFromDB = manager.GetContinent(2);

            continentFromDB.Name.Should().Be("continent 1");

            continentFromDB.Countries.Count.Should().Be(2);

        }

        [TestMethod]
        public void ManagerFunction_UpdateContinent_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Continent continent = new Continent("continent 4");

            Action act = () => manager.UpdateContinent(55, continent);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            act = () => manager.UpdateContinent(4, continent);

            act.Should().Throw<DomainException>().WithMessage("Er is al een continent met de gegeven naam.");
        }

        [TestMethod]
        public void ManagerFunction_UpdateContinent_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            manager.GetContinent(2).Name.Should().Be("continent 1");

            manager.UpdateContinent(2, new Continent("continent 50"));

            manager.GetContinent(2).Name.Should().Be("continent 50");
        }

        [TestMethod]
        public void ManagerFunction_DeleteContinent_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.DeleteContinent(55);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            act = () => manager.DeleteContinent(2);

            act.Should().Throw<DomainException>().WithMessage("Het gegeven continent bevadt nog landen.");
        }

        [TestMethod]
        public void ManagerFunction_DeleteContinent_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            manager.DeleteContinent(3);

            Action act = () => manager.GetContinent(3);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");
        }

        [TestMethod]
        public void ManagerFunction_AddCountry_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.AddCountry(55, "Country 1", 600, 36.66f);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            manager.AddCountry(2, "Country 0", 600, 36.66f);

            act = () => manager.AddCountry(2, "Country 0", 600, 36.66f);

            act.Should().Throw<DomainException>().WithMessage("Er is al een land met de gegeven naam.");
        }

        [TestMethod]
        public void ManagerFunction_AddCountry_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            manager.AddCountry(2, "Country 1", 600, 36.66f);

            manager.AddCountry(2, "Country 2", 666, 66.6f);

            manager.GetCountry(2, 2).Name.Should().Be("Country 1");
            manager.GetCountry(2, 3).Name.Should().Be("Country 2");
        }

        [TestMethod]
        public void ManagerFunction_GetCountry_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.GetCountry(55, 10);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            act = () => manager.GetCountry(2, 10);

            act.Should().Throw<DomainException>().WithMessage("Er is geen land met het gegeven id.");

            act = () => manager.GetCountry(1, 2);

            act.Should().Throw<DomainException>().WithMessage("Het gegeven land is niet deel het gegeven continent.");
        }

        [TestMethod]
        public void ManagerFunction_GetCountry_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Country countryFromDB = manager.GetCountry(2, 1);

            countryFromDB.Name.Should().Be("Country 0");
            countryFromDB.Population.Should().Be(600);
            countryFromDB.Surface.Should().Be(36.66f);
            countryFromDB.Continent.Countries.Count.Should().Be(2);
            countryFromDB.Continent.Name.Should().Be("continent 1");
        }

        [TestMethod]
        public void ManagerFunction_UpdateCountry_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.UpdateCountry(10, 55, "Country 0", 595, 55.0f);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            act = () => manager.UpdateCountry(10, 2, "Country 0", 595, 55.0f);

            act.Should().Throw<DomainException>().WithMessage("Er is geen land met het gegeven id.");

            act = () => manager.UpdateCountry(2, 1, "Country 0", 595, 55.0f);

            act.Should().Throw<DomainException>().WithMessage("Het gegeven land is niet deel het gegeven continent.");

            act = () => manager.UpdateCountry(2, 2, "Country 0", 595, 55.0f);

            act.Should().Throw<DomainException>().WithMessage("Er is al een land met de gegeven naam.");
        }

        [TestMethod]
        public void ManagerFunction_UpdateCountry_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            manager.UpdateCountry(2, 2, "Country 6", 595, 55.0f);

            manager.GetCountry(2, 2).Name.Should().Be("Country 6");
        }


        [TestMethod]
        public void ManagerFunction_DeleteCountry_ExceptionyTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            Action act = () => manager.DeleteCountry(55,10);

            act.Should().Throw<DomainException>().WithMessage("Er is geen continent met het gegeven id.");

            act = () => manager.DeleteCountry(2, 10);

            act.Should().Throw<DomainException>().WithMessage("Er is geen land met het gegeven id.");

            act = () => manager.DeleteCountry(1, 2);

            act.Should().Throw<DomainException>().WithMessage("Het gegeven land is niet deel het gegeven continent.");
        }

        [TestMethod]
        public void ManagerFunction_DeleteCountry_FuncionalityTest()
        {
            Manager manager = new Manager(new UnitOfWork(new GeographyContextTest(true)));

            manager.DeleteCountry(2, 3);

            Action act = () => manager.GetCountry(2, 3);

            act.Should().Throw<DomainException>().WithMessage("Er is geen land met het gegeven id.");
        }
    }
}
