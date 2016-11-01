using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insendlu.Encryptor;
using Insendlu.Entities;
using Insendlu.Entities.Connection;


namespace Insendu.Services
{
    public class ProjectService
    {
        private readonly Connect _connect;
        private readonly InsendluEntities _insendluEntities;
        private readonly Encryptor _encryptor;

        public ProjectService()
        {
            _connect = new Connect();
            _insendluEntities = _connect.GetConnection();
            _encryptor = new Encryptor();
        }

        public Connect Connect
        {
            get
            {
                return _connect;
            }
        }

        public int AddNewProject(Project proj)
        {
            var project = new Project()
            {
                created_at = proj.created_at,
                modified_at = proj.modified_at,
                description = proj.description,
                status = proj.status,
                proj_cat_id = proj.proj_cat_id,
                name = proj.name
            };

            _insendluEntities.Projects.Add(project);

            return _insendluEntities.SaveChanges();
        }

        public int AddProjectCategory(ProjectCategory projectCategory)
        {
            var cat = _insendluEntities.ProjectCategories.Add(projectCategory);
            var id = _insendluEntities.SaveChanges();
            return id;
        }
        public void RemoveProject(long id)
        {
            var projectToRemove = (from pro in _insendluEntities.Projects
                                   where pro.id == id
                                   select pro).SingleOrDefault();

            _insendluEntities.Projects.Remove(projectToRemove);

        }

        public void UpdateProject(Project project)
        {
            var projectToUpdate = (from pro in _insendluEntities.Projects
                                   where pro.id == project.id
                                   select pro).SingleOrDefault();

            if (projectToUpdate != null)
            {
                projectToUpdate.description = project.description;
                projectToUpdate.modified_at = DateTime.Now;
                projectToUpdate.proj_cat_id = project.proj_cat_id;
                projectToUpdate.name = project.name;

            }

            _insendluEntities.SaveChanges();
        }

        public Company GetCompanyById(long id)
        {
            var company = (from comp in _insendluEntities.Companies
                where comp.proj_id == id
                select comp).Single();

            return company;
        }
        public ExecutiveSummary GetExecSummary(long id)
        {
            var exec = (from p in _insendluEntities.ExecutiveSummaries
                        where p.proj_id == id
                        select p).SingleOrDefault();
            return exec;
        }
        public ProjectPolicy GetProjectPolicy(long id)
        {
            var projPolicy = (from p in _insendluEntities.ProjectPolicies
                              where p.proj_id == id
                        select p).SingleOrDefault();
            return projPolicy;
        }

        public ProjectMethodology GetProjMethodolgy(long id)
        {
            var projMeth = (from proj in _insendluEntities.ProjectMethodologies
                            where proj.proj_id == id
                select proj).SingleOrDefault();

            return projMeth;
        }

        public ProjectCostPlan GetCostPlan(long id)
        {
            var costPlan = (from cost in _insendluEntities.ProjectCostPlans
                            where cost.proj_id == id
                            select cost).SingleOrDefault();

            return costPlan;
        }

        public Project GetProject(long id)
        {
            var project = (from proj in _insendluEntities.Projects
                            where proj.id == id
                            select proj).SingleOrDefault();

            return project;
        }

        public int AddUser(string name, string surname, string email, string password)
        {
            var encryptedPass = _encryptor.Encrypt(password);
            var usertype = new UserType()
            {
                name = "User",
                status = EntityStatus.Active.GetHashCode()
            };
            _insendluEntities.UserTypes.Add(usertype);
            var userTID = _insendluEntities.SaveChanges();

            if (userTID == 1)
            {
                var user = new User()
                {
                    name = name,
                    email = email,
                    password = encryptedPass,
                    created_at = DateTime.Now,
                    modified_at = DateTime.Now,
                    user_type_id = 1

                };

                _insendluEntities.Users.Add(user);

                return _insendluEntities.SaveChanges();
            }

            return 0;
        }

        
    }
}
