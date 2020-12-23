using DomainLayer.Interfaces;
using DomainLayer.Model;

namespace DomainLayer
{
    public class Manager : IManager
    {
        private IUnitOfWork uow;


        /// <summary>
        /// Makes a manager to comunicate between the data and domain layers.
        /// </summary>
        /// <param name="uow">Unit of work used to do the commands.</param>
        public Manager(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public int AddContinent(Continent continent)
        {
            if (uow.Continents.IsInDatabase(continent.Name))
                throw new DomainException("Er is al een continent met de gegeven naam.");

            uow.Complete();

            uow.Continents.AddContinent(continent);

            uow.Complete();

            var toReturn = uow.Continents.GetContinent(continent.Name).Id;

            uow.Complete();

            return toReturn;
        }

        public Continent GetContinent(int continentId)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            var toReturn = uow.Continents.GetContinent(continentId);

            uow.Complete();

            return toReturn;
        }

        public void UpdateContinent(int continentId, Continent continent)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (uow.Continents.IsInDatabase(continent.Name))
                throw new DomainException("Er is al een continent met de gegeven naam.");

            uow.Complete();

            uow.Continents.UpdateContinent(continentId, continent);

            uow.Complete();
        }

        public void DeleteContinent(int continentId)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (uow.Continents.HasCountries(continentId))
                throw new DomainException("Het gegeven continent bevadt nog landen.");

            uow.Complete();

            uow.Continents.DeleteContinent(continentId);

            uow.Complete();
        }

        public int AddCountry(int continentId, string countryName, int countryPopulation, float countrySurface)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (uow.Countries.IsInDatabase(countryName))
                throw new DomainException("Er is al een land met de gegeven naam.");

            uow.Complete();

            uow.Countries.AddCountry(continentId, countryName, countryPopulation, countrySurface);

            uow.Complete();

            var toReturn = uow.Countries.GetCountry(continentId, countryName).Id;

            uow.Complete();

            return toReturn;
        }

        public Country GetCountry(int continentId, int countryId)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (!uow.Countries.IsInDatabase(countryId))
                throw new DomainException("Er is geen land met het gegeven id.");

            uow.Complete();

            if (!uow.Countries.BelongsToContinent(continentId, countryId))
                throw new DomainException("Het gegeven land is niet deel het gegeven continent.");


            uow.Complete();

            var toReturn = uow.Countries.GetCountry(continentId, countryId);

            uow.Complete();

            return toReturn;
        }

        public void UpdateCountry(int countryId, int continentId, string countryName, int countryPopulation, float countrySurface)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (!uow.Countries.IsInDatabase(countryId))
                throw new DomainException("Er is geen land met het gegeven id.");

            uow.Complete();

            if(!uow.Countries.BelongsToContinent(continentId, countryId))
                throw new DomainException("Het gegeven land is niet deel het gegeven continent.");

            uow.Complete();

            if (uow.Countries.IsInDatabase(countryName))
                throw new DomainException("Er is al een land met de gegeven naam.");

            uow.Complete();

            uow.Countries.UpdateCountry(countryId, continentId, countryName, countryPopulation, countrySurface);

            uow.Complete();
        }

        public void DeleteCountry(int continentId, int countryId)
        {
            if (!uow.Continents.IsInDatabase(continentId))
                throw new DomainException("Er is geen continent met het gegeven id.");

            uow.Complete();

            if (!uow.Countries.IsInDatabase(countryId))
                throw new DomainException("Er is geen land met het gegeven id.");

            uow.Complete();

            if (!uow.Countries.BelongsToContinent(continentId, countryId))
                throw new DomainException("Het gegeven land is niet deel het gegeven continent.");

            uow.Complete();

            uow.Countries.DeleteCountry(countryId);

            uow.Complete();
        }
    }
}
