using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.Location;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ILocationService : IDataService<Location>
    {
        IQueryable<ListLocationViewModel> GetList();
        bool IsLocationCodeValid(string code, int id);
        IQueryable<Country> GetListCountry();
        IQueryable<State> GetListState(int CountryID);
        IQueryable<City> GetListCity(int StateID);
        IQueryable<LocationClassification> GetListClassification();
    }
    public class LocationProvider : ILocationService
    {
        readonly ChakaContext context;

        public LocationProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<Location> AllLocations
        {
            get { return context.Location.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<Country> AllCountries
        {
            get { return context.Country.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<City> AllCities
        {
            get { return context.City.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<State> AllStates
        {
            get { return context.State.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<LocationClassification> AllClassifications
        {
            get { return context.LocationClassification.Where(x => !x.DelDate.HasValue); }
        }


        public IQueryable<Location> Get() => context.Location.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public Location Get(int Id) => context.Location.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListLocationViewModel> GetList()
        {
            var query = from location in AllLocations
                        join country in AllCountries on location.CountryId equals country.Id into ct
                        from country in ct.DefaultIfEmpty()
                        join city in AllCities on location.CityId equals city.Id into cy
                        from city in cy.DefaultIfEmpty()
                        join state in AllStates on location.ProvinceId equals state.Id into cp
                        from state in cp.DefaultIfEmpty()
                        join classification in AllClassifications on location.ClassificationId equals classification.Id into cc
                        from classification in cc.DefaultIfEmpty()
                        select new ListLocationViewModel()
                        {
                            ID = location.Id.ToString(),
                            Code = location.Code,
                            Name = location.Name,
                            Address = location.Address,
                            Country = country.Name,
                            State = state.Name,
                            City = city.Name,
                            Postalcode = location.Postalcode,
                            BeginEff = location.BeginEff,
                            LastEff = location.LastEff,
                            Classification = classification.Name,
                            Description = location.Name,
                        };

            return query;
        }

        public bool IsLocationCodeValid(string code, int id)
        {
            var result = AllLocations.Any(x => x.Code == code && x.Id != id);
            return result;
        }

        public int Add(Location entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(Location entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var locclass = Get(Id);
            if (locclass != null)
            {
                context.SbDelete(locclass);
                context.SaveChanges();
            }
        }


        public IQueryable<Country> GetListCountry()
        {
            var query = from country in AllCountries
                        select new Country()
                        {
                            Id = country.Id,
                            Name = country.Name,
                        };

            return query;
        }

        public IQueryable<State> GetListState(int CountryID)
        {
            var query = from state in AllStates
                        where state.CountryId == CountryID
                        select new State()
                        {
                            Id = state.Id,
                            Name = state.Name,
                        };

            return query;
        }

        public IQueryable<City> GetListCity(int StateID)
        {
            var query = from city in AllCities
                        //where city.StateId == StateID
                        select new City()
                        {
                            Id = city.Id,
                            Name = city.Name,
                        };

            return query;
        }

        public IQueryable<LocationClassification> GetListClassification()
        {
            var query = from clf in AllClassifications
                        select new LocationClassification
                        {
                            Id = clf.Id,
                            Name = clf.Name,
                        };

            return query;
        }

    }
}
