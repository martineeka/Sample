using Chaka.Database.Context.Models;
using Chaka.ViewModels.Organization.LocationClassification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.Organization
{
    public interface ILocationClassificationService : IDataService<LocationClassification>
    {
        IQueryable<ListLocationClassificationViewModel> GetList();
        int GetMaxSequenceLocclas();
        bool IsLocClassCodeValid(string code, int id);

    }
    public class LocationClassificationProvider :ILocationClassificationService
    {
        readonly ChakaContext context;

        public LocationClassificationProvider(ChakaContext context)
        {
            this.context = context;
        }

        public IQueryable<LocationClassification> AllLocationClassifications
        {
            get { return context.LocationClassification.Where(x => !x.DelDate.HasValue); }
        }

        public IQueryable<LocationClassification> Get() => context.LocationClassification.AsNoTracking().Where(x => !x.DelDate.HasValue);

        public LocationClassification Get(int Id) => context.LocationClassification.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == Id);

        public IQueryable<ListLocationClassificationViewModel> GetList()
        {
            var query = from locclas in AllLocationClassifications
                        select new ListLocationClassificationViewModel()
                        {
                            ID = locclas.Id.ToString(),
                            Code = locclas.Code,
                            Name = locclas.Name,
                            Description = locclas.Name,
                            //Sequence = locclas.Sequence,
                            BusinessGroupId = locclas.BusinessGroupId,
                        };

            return query;
        }

        public int Add(LocationClassification entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(LocationClassification entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var locclass = Get(Id);
            if(locclass != null)
            {
                context.SbDelete(locclass);
                context.SaveChanges();
            }
        }


        public int GetMaxSequenceLocclas()
        {
            int maxSequenceLocclas = 0;
            //var sequence = AllLocationClassifications.OrderByDescending(Locclas => Locclas.Sequence).FirstOrDefault();
            //if (sequence != null)
            //    //maxSequenceLocclas = sequence.Sequence.GetValueOrDefault();
            //    maxSequenceLocclas = (int)sequence.Sequence + 1;
            ////maxSequenceLevel = (int)sequence.Sequence;
            ////maxSequenceLocclas = 0;
            return maxSequenceLocclas;
        }

        public bool IsLocClassCodeValid(string code, int id)
        {
            var result = AllLocationClassifications.Any(locclas => locclas.Code == code && locclas.Id != id);
            return result;
        }





    }
}
