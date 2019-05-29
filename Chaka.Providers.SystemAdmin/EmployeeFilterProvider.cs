using Chaka.Database.Context.Models;
using Chaka.Utilities;
using Chaka.ViewModels.SystemAdmin.EmployeeFilter;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chaka.Providers.SystemAdmin
{
    public interface IEmployeeFilterService : IDataService<EmployeeListFilter>
    {
        IQueryable<ListEmployeeListFilter> List();
        EmployeeListFilter GetEmployeeListFilter(int id);
        bool IsCodeValid(string code, string id);

        //grade
        int[] GetFilterGradeID(int employeeListFilterID);
        IEnumerable<ListGradeViewModel> ListGradeUnselected(int employeeListFilterID, int[] selectedID);
        IEnumerable<ListGradeViewModel> ListGradeSelected(int employeeListFilterID, int[] unselectedID);
        void UpdateListFilterGrade(int employeeListFilterID, string[] gradeID);

        //level
        int[] GetFilterLevelID(int employeeListFilterID);
        IEnumerable<ListLevelViewModel> ListLevelSelected(int employeeListFilterID, int[] unselectedID);
        IEnumerable<ListLevelViewModel> ListLevelUnselected(int employeeListFilterID, int[] selectedID);
        void UpdateListFilterLevel(int employeeListFilterID, string[] levelID);

        //jobtitle
        IEnumerable<ListJobTitleViewModel> ListJobTitleSelected(int employeeListFilterID, int[] unselectedID);
        IEnumerable<ListJobTitleViewModel> ListJobTitleUnselected(int employeeListFilterID, int[] selectedID);
        int[] GetFilterJobTitleID(int employeeListFilterID);
        void UpdateListFilterJobTitle(int employeeListFilterID, string[] jobtitleID);

        //location
        IEnumerable<ListLocationViewModel> ListLocationSelected(int employeeListFilterID, int[] unselectedID);
        IEnumerable<ListLocationViewModel> ListLocationUnselected(int employeeListFilterID, int[] selectedID);
        int[] GetFilterLocationID(int employeeListFilterID);
        void UpdateListFilterLocation(int employeeListFilterID, string[] locationID);

        //orgunit
        IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitSelected(int employeeListFilterID, int[] unselectedID);
        IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitUnselected(int employeeListFilterID, int[] selectedID);
        int[] GetFilterOrganizationUnitID(int employeeListFilterID);
        void UpdateListFilterOrganizationUnit(int employeeListFilterID, string[] organizationunitID);

        //Employee status
        IEnumerable<ListEmployeeStatusViewModel> ListStatusSelected(int employeeListFilterID, int[] unselectedID);
        IEnumerable<ListEmployeeStatusViewModel> ListStatusUnselected(int employeeListFilterID, int[] selectedID);
        int[] GetFilterStatusID(int employeeListFilterID);
        void UpdateListFilterStatus(int employeeListFilterID, string[] statusID);

        IEnumerable<EmployeeListFilter> GetMenuDetail(int id);

    }

    public class EmployeeFilterProvider : IEmployeeFilterService
    {
        readonly ChakaContext context;
        int CurrentBusinessGroupId = 2;
        int CurrentUserId = 0;

        public EmployeeFilterProvider(ChakaContext context)
        {
            this.context = context;
        }


        #region Employee List Filter
        public IQueryable<EmployeeListFilter> AllEmployeeListFilters
        {
            get
            {
                return context.EmployeeListFilter.Where(e => e.BusinessGroupId == CurrentBusinessGroupId && !e.DelDate.HasValue);
            }
        }

        public EmployeeListFilter GetEmployeeListFilter(int id)
        {
            return AllEmployeeListFilters.SingleOrDefault(e => e.Id == id);
        }


        public CreateEditViewModel GetEmployeeListFilter(string id)
        {
            var decryptedID = Convert.ToInt32(EncryptionHelper.DecryptUrlParam(id));
            var query = from employeeFilter in AllEmployeeListFilters
                        where employeeFilter.Id == decryptedID
                        select new CreateEditViewModel()
                        {
                            ID = id,
                            Code = employeeFilter.Code,
                            Name = employeeFilter.Name,
                            Description = employeeFilter.Description
                        };
            return query.SingleOrDefault();
        }

        public IQueryable<ListEmployeeListFilter> List()
        {

            return from filter in AllEmployeeListFilters
                   orderby filter.Code
                   select new ListEmployeeListFilter
                   {
                       ID = filter.Id.ToString(),
                       Code = filter.Code,
                       Name = filter.Name,
                       Description = filter.Description
                       
                   };
        }

        public IQueryable<ListEmployeeListFilter> GetList(int Id)
        {
            var query = from filter in AllEmployeeListFilters
                        where filter.Id == Id
                        select new ListEmployeeListFilter()
                        {
                            ID = filter.Id.ToString(),
                            Code = filter.Code,
                            Description = filter.Description,
                        };

            return query;
        }

        public int[] GetFilterID(int ID)
        {//id yg selected
            return AllEmployeeListFilters.Where(e => e.Id == ID).Select(s => (int)s.Id).ToArray();
        }

        #endregion

        #region Filter Grade

        public IEnumerable<EmployeeListFilterGrade> AllEmployeeListFilterGrades
        {
            get
            {
                return context.EmployeeListFilterGrade.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<Grade> AllGrades
        {
            get
            {
                return context.Grade.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListGradeViewModel> ListGradeSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from grade in AllGrades
                       where !unselectedID.Contains(grade.Id)
                       select new ListGradeViewModel()
                       {
                           ID = grade.Id,
                           Code = grade.Code,
                           Name = grade.Name
                       };
            }
            else
            {
                return from grade in AllGrades
                       select new ListGradeViewModel()
                       {
                           ID = grade.Id,
                           Code = grade.Code,
                           Name = grade.Name
                       };
            }
        }

        public IEnumerable<ListGradeViewModel> ListGradeUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from grade in AllGrades
                       where !selectedID.Contains(grade.Id)
                       select new ListGradeViewModel()
                       {
                           ID = grade.Id,
                           Code = grade.Code,
                           Name = grade.Name
                       };
            }
            else
            {
                return from grade in AllGrades
                       select new ListGradeViewModel()
                       {
                           ID = grade.Id,
                           Code = grade.Code,
                           Name = grade.Name
                       };
            }
        }

        public int[] GetFilterGradeID(int employeeListFilterID)
        {
            var ids = AllEmployeeListFilterGrades.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.GradeId).ToArray();

            return ids;
        }

        public void UpdateListFilterGrade(int employeeListFilterID, string[] gradeID)
        {
            var filterGrades = AllEmployeeListFilterGrades.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (gradeID != null)
            {
                int[] arrayGradeID = gradeID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterGrades != null)
                {
                    foreach (var grade in filterGrades)
                    {
                        if (!arrayGradeID.Contains(grade.GradeId))
                            context.EmployeeListFilterGrade.Remove(grade);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayGradeID)
                {
                    if (!filterGrades.Any(g => g.GradeId == id))
                    {
                        context.EmployeeListFilterGrade.Add(new EmployeeListFilterGrade()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            GradeId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterGrades != null)
                {
                    foreach (var grade in filterGrades)
                    {
                        context.EmployeeListFilterGrade.Remove(grade);
                    }
                }
            }

            context.SaveChanges();
        }

        #endregion

        # region Filter Level
        public IEnumerable<EmployeeListFilterLevel> AllEmployeeListFilterLevels
        {
            get
            {
                return context.EmployeeListFilterLevel.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<Level> AllLevels
        {
            get
            {
                return context.Level.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListLevelViewModel> ListLevelSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from level in AllLevels
                       where !unselectedID.Contains(level.Id)
                       select new ListLevelViewModel()
                       {
                           ID = level.Id,
                           Code = level.Code,
                           Name = level.Name
                       };
            }
            else
            {
                return from level in AllLevels
                       select new ListLevelViewModel()
                       {
                           ID = level.Id,
                           Code = level.Code,
                           Name = level.Name
                       };
            }
        }

        public IEnumerable<ListLevelViewModel> ListLevelUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from level in AllLevels
                       where !selectedID.Contains(level.Id)
                       select new ListLevelViewModel()
                       {
                           ID = level.Id,
                           Code = level.Code,
                           Name = level.Name
                       };
            }
            else
            {
                return from level in AllLevels
                       select new ListLevelViewModel()
                       {
                           ID = level.Id,
                           Code = level.Code,
                           Name = level.Name
                       };
            }
        }


        public int[] GetFilterLevelID(int employeeListFilterID)
        {
            return AllEmployeeListFilterLevels.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.LevelId).ToArray();
        }

        public void UpdateListFilterLevel(int employeeListFilterID, string[] levelID)
        {
            var filterLevels = AllEmployeeListFilterLevels.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (levelID != null)
            {
                int[] arrayLevelID = levelID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterLevels != null)
                {
                    foreach (var level in filterLevels)
                    {
                        if (!arrayLevelID.Contains(level.LevelId))
                            context.EmployeeListFilterLevel.Remove(level);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayLevelID)
                {
                    if (!filterLevels.Any(g => g.LevelId == id))
                    {
                        context.EmployeeListFilterLevel.Add(new EmployeeListFilterLevel()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            LevelId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterLevels != null)
                {
                    foreach (var level in filterLevels)
                    {
                        context.EmployeeListFilterLevel.Remove(level);
                    }
                }
            }

            context.SaveChanges();
        }

        #endregion

        #region Filter JobTitle -- Position
        public IEnumerable<EmployeeListFilterPosition> AllEmployeeListFilterJobTitles
        {
            get
            {
                return context.EmployeeListFilterPosition.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<JobTitle> AllJobTitles
        {
            get
            {
                return context.JobTitle.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListJobTitleViewModel> ListJobTitleSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from jobtitle in AllJobTitles
                       where !unselectedID.Contains(jobtitle.Id)
                       select new ListJobTitleViewModel()
                       {
                           ID = jobtitle.Id,
                           Code = jobtitle.Code,
                           Name = jobtitle.Name
                       };
            }
            else
            {
                return from jobtitle in AllJobTitles
                       select new ListJobTitleViewModel()
                       {
                           ID = jobtitle.Id,
                           Code = jobtitle.Code,
                           Name = jobtitle.Name
                       };
            }
        }

        public IEnumerable<ListJobTitleViewModel> ListJobTitleUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from jobtitle in AllJobTitles
                       where !selectedID.Contains(jobtitle.Id)
                       select new ListJobTitleViewModel()
                       {
                           ID = jobtitle.Id,
                           Code = jobtitle.Code,
                           Name = jobtitle.Name
                       };
            }
            else
            {
                return from jobtitle in AllJobTitles
                       select new ListJobTitleViewModel()
                       {
                           ID = jobtitle.Id,
                           Code = jobtitle.Code,
                           Name = jobtitle.Name
                       };
            }
        }

        public int[] GetFilterJobTitleID(int employeeListFilterID)
        {
            return AllEmployeeListFilterJobTitles.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.PositionId).ToArray();
        }

        public void UpdateListFilterJobTitle(int employeeListFilterID, string[] jobtitleID)
        {
            var filterJobTitles = AllEmployeeListFilterJobTitles.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (jobtitleID != null)
            {
                int[] arrayJobTitleID = jobtitleID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterJobTitles != null)
                {
                    foreach (var jobtitle in filterJobTitles)
                    {
                        if (!arrayJobTitleID.Contains(jobtitle.PositionId))
                            context.EmployeeListFilterPosition.Remove(jobtitle);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayJobTitleID)
                {
                    if (!filterJobTitles.Any(g => g.PositionId == id))
                    {
                        context.EmployeeListFilterPosition.Add(new EmployeeListFilterPosition()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            PositionId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterJobTitles != null)
                {
                    foreach (var jobtitle in filterJobTitles)
                    {
                        context.EmployeeListFilterPosition.Remove(jobtitle);
                    }
                }
            }

            context.SaveChanges();
        }

        #endregion

        #region Location
        public IEnumerable<EmployeeListFilterLocation> AllEmployeeListFilterLocations
        {
            get
            {
                return context.EmployeeListFilterLocation.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<Location> AllLocations
        {
            get
            {
                return context.Location.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListLocationViewModel> ListLocationSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from location in AllLocations
                       where !unselectedID.Contains(location.Id)
                       select new ListLocationViewModel()
                       {
                           ID = location.Id,
                           Code = location.Code,
                           Name = location.Name
                       };
            }
            else
            {
                return from location in AllLocations
                       select new ListLocationViewModel()
                       {
                           ID = location.Id,
                           Code = location.Code,
                           Name = location.Name
                       };
            }
        }


        public IEnumerable<ListLocationViewModel> ListLocationUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from location in AllLocations
                       where !selectedID.Contains(location.Id)
                       select new ListLocationViewModel()
                       {
                           ID = location.Id,
                           Code = location.Code,
                           Name = location.Name
                       };
            }
            else
            {
                return from location in AllLocations
                       select new ListLocationViewModel()
                       {
                           ID = location.Id,
                           Code = location.Code,
                           Name = location.Name
                       };
            }
        }



        public int[] GetFilterLocationID(int employeeListFilterID)
        {
            return AllEmployeeListFilterLocations.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.LocationId).ToArray();
        }

        public void UpdateListFilterLocation(int employeeListFilterID, string[] locationID)
        {
            var filterLocations = AllEmployeeListFilterLocations.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (locationID != null)
            {
                int[] arrayLocationID = locationID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterLocations != null)
                {
                    foreach (var location in filterLocations)
                    {
                        if (!arrayLocationID.Contains(location.LocationId))
                            context.EmployeeListFilterLocation.Remove(location);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayLocationID)
                {
                    if (!filterLocations.Any(g => g.LocationId == id))
                    {
                        context.EmployeeListFilterLocation.Add(new EmployeeListFilterLocation()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            LocationId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterLocations != null)
                {
                    foreach (var location in filterLocations)
                    {
                        context.EmployeeListFilterLocation.Remove(location);
                    }
                }
            }

            context.SaveChanges();
        }

        #endregion


        #region Organization Unit
        public IEnumerable<EmployeeListFilterOrgUnit> AllEmployeeListFilterOrganizationUnits
        {
            get
            {
                return context.EmployeeListFilterOrgUnit.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<OrgUnit> AllOrganizationUnits
        {
            get
            {
                return context.OrgUnit.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from organizationunit in AllOrganizationUnits
                       where !unselectedID.Contains(organizationunit.Id)
                       select new ListOrganizationUnitViewModel()
                       {
                           ID = organizationunit.Id,
                           Code = organizationunit.Code,
                           Name = organizationunit.Name
                       };
            }
            else
            {
                return from organizationunit in AllOrganizationUnits
                       select new ListOrganizationUnitViewModel()
                       {
                           ID = organizationunit.Id,
                           Code = organizationunit.Code,
                           Name = organizationunit.Name
                       };
            }
        }

        public IEnumerable<ListOrganizationUnitViewModel> ListOrganizationUnitUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from organizationunit in AllOrganizationUnits
                       where !selectedID.Contains(organizationunit.Id)
                       select new ListOrganizationUnitViewModel()
                       {
                           ID = organizationunit.Id,
                           Code = organizationunit.Code,
                           Name = organizationunit.Name
                       };
            }
            else
            {
                return from organizationunit in AllOrganizationUnits
                       select new ListOrganizationUnitViewModel()
                       {
                           ID = organizationunit.Id,
                           Code = organizationunit.Code,
                           Name = organizationunit.Name
                       };
            }
        }

        public int[] GetFilterOrganizationUnitID(int employeeListFilterID)
        {
            return AllEmployeeListFilterOrganizationUnits.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.OrgUnitId).ToArray();
        }

        public void UpdateListFilterOrganizationUnit(int employeeListFilterID, string[] organizationunitID)
        {
            var filterOrganizationUnits = AllEmployeeListFilterOrganizationUnits.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (organizationunitID != null)
            {
                int[] arrayOrganizationUnitID = organizationunitID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterOrganizationUnits != null)
                {
                    foreach (var organizationunit in filterOrganizationUnits)
                    {
                        if (!arrayOrganizationUnitID.Contains(organizationunit.OrgUnitId))
                            context.EmployeeListFilterOrgUnit.Remove(organizationunit);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayOrganizationUnitID)
                {
                    if (!filterOrganizationUnits.Any(g => g.OrgUnitId == id))
                    {
                        context.EmployeeListFilterOrgUnit.Add(new EmployeeListFilterOrgUnit()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            OrgUnitId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterOrganizationUnits != null)
                {
                    foreach (var organizationunit in filterOrganizationUnits)
                    {
                        context.EmployeeListFilterOrgUnit.Remove(organizationunit);
                    }
                }
            }

            context.SaveChanges();
        }
        #endregion


        #region Employee Status
        public IEnumerable<EmployeeListFilterStatus> AllEmployeeListFilterStatus
        {
            get
            {
                return context.EmployeeListFilterStatus.Where(e => e.BusinessGroupId == CurrentBusinessGroupId);
            }
        }

        public IEnumerable<EmployeeStatus> AllEmployeeStatus
        {
            get
            {
                return context.EmployeeStatus.Where(g => g.BusinessGroupId == CurrentBusinessGroupId && !g.DelDate.HasValue);
            }
        }

        public IEnumerable<ListEmployeeStatusViewModel> ListStatusSelected(int employeeListFilterID, int[] unselectedID)
        {
            if (unselectedID != null)
            {
                return from status in AllEmployeeStatus
                       where !unselectedID.Contains(status.Id)
                       select new ListEmployeeStatusViewModel()
                       {
                           ID = status.Id,
                           Code = status.Code,
                           Name = status.Name
                       };
            }
            else
            {
                return from status in AllEmployeeStatus
                       select new ListEmployeeStatusViewModel()
                       {
                           ID = status.Id,
                           Code = status.Code,
                           Name = status.Name
                       };
            }
        }


        public IEnumerable<ListEmployeeStatusViewModel> ListStatusUnselected(int employeeListFilterID, int[] selectedID)
        {
            if (selectedID != null)
            {
                return from status in AllEmployeeStatus
                       where !selectedID.Contains(status.Id)
                       select new ListEmployeeStatusViewModel()
                       {
                           ID = status.Id,
                           Code = status.Code,
                           Name = status.Name
                       };
            }
            else
            {
                return from status in AllEmployeeStatus
                       select new ListEmployeeStatusViewModel()
                       {
                           ID = status.Id,
                           Code = status.Code,
                           Name = status.Name
                       };
            }
        }

        public int[] GetFilterStatusID(int employeeListFilterID)
        {
            return AllEmployeeListFilterStatus.Where(e => e.EmployeeListFilterId == employeeListFilterID).Select(s => s.EmploymentStatusId).ToArray();
        }


        public void UpdateListFilterStatus(int employeeListFilterID, string[] statusID)
        {
            var filterStatus = AllEmployeeListFilterStatus.Where(e => e.EmployeeListFilterId == employeeListFilterID);

            if (statusID != null)
            {
                int[] arrayStatusID = statusID.Select(g => Convert.ToInt32(g)).ToArray();
                if (filterStatus != null)
                {
                    foreach (var status in filterStatus)
                    {
                        if (!arrayStatusID.Contains(status.EmploymentStatusId))
                            context.EmployeeListFilterStatus.Remove(status);
                    }
                    context.SaveChanges();
                }

                foreach (var id in arrayStatusID)
                {
                    if (!filterStatus.Any(g => g.EmploymentStatusId == id))
                    {
                        context.EmployeeListFilterStatus.Add(new EmployeeListFilterStatus()
                        {
                            EmployeeListFilterId = employeeListFilterID,
                            EmploymentStatusId = id,
                            BusinessGroupId = CurrentBusinessGroupId,
                            CreatedBy = CurrentUserId,
                            CreatedDate = DateTime.Now
                        });
                    }
                }
            }
            else
            {
                if (filterStatus != null)
                {
                    foreach (var status in filterStatus)
                    {
                        context.EmployeeListFilterStatus.Remove(status);
                    }
                }
            }

            context.SaveChanges();
        }

        #endregion

        #region Idataservice
        public IQueryable<EmployeeListFilter> Get() => context.EmployeeListFilter.AsNoTracking().Where(x => !x.DelDate.HasValue);


        //public EmployeeListFilter Get(int id) => context.EmployeeListFilter.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == id);
        public EmployeeListFilter Get(int id) => context.EmployeeListFilter.AsNoTracking().SingleOrDefault(x => !x.DelDate.HasValue && x.Id == id);

        public int Add(EmployeeListFilter entity)
        {
            context.SbAdd(entity);
            return context.SaveChanges();
        }

        public int Edit(EmployeeListFilter entity)
        {
            context.SbEdit(entity);
            return context.SaveChanges();
        }

        public void Delete(int Id)
        {
            var menu = Get(Id);
            if (menu != null)
            {
                context.SbDelete(menu);
                context.SaveChanges();
            }
        }

        #endregion


        public bool IsCodeValid(string code, string id)
        {
            var result = AllEmployeeListFilters.Any(filter => filter.Code == code && filter.Id != Convert.ToInt32(id));
            return result;
        }

        //public GetMenuDetail Get(int id)
        public IEnumerable<EmployeeListFilter> GetMenuDetail(int id)
        {
             
            return from filter in AllEmployeeListFilters
                   where id == filter.Id
                   select new EmployeeListFilter()
                   {
                       Id = filter.Id,
                       Code = filter.Code,
                       Name = filter.Name
                   };

        }  


    }
}
