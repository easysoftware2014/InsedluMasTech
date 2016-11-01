using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Insendlu.Entities;
using Insendlu.Entities.Connection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Insendu.Services;
using ListItem = System.Web.UI.WebControls.ListItem;

namespace Insendlu
{
    public partial class NewProject : System.Web.UI.Page
    {
        private readonly InsendluEntities _insendluEntities;
        private readonly ProjectService _projectService;
        public NewProject()
        {
            _insendluEntities = new InsendluEntities();
            _projectService = new ProjectService();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //var users = from user in _insendluEntities.Users
                //            select new { ID = user.id, Name = user.name, Surname = user.surname };

                //ddlUsers.DataSource = users.ToList();
                //ddlUsers.DataTextField = "name";
                //ddlUsers.DataValueField = "id";
                //ddlUsers.DataBind();

                //ddlUsers.Items.Insert(0, new ListItem("--select user--", "0"));
            }
        }
        protected void SubmitButton_OnClick(object sender, EventArgs e)
        {

            var createdDate = DateTime.Now;
            var modifiedAt = DateTime.Now;
            var conclution = conclusion.Content;

            var newProj = new Project
            {
                created_at = createdDate,
                modified_at = modifiedAt,
                description = descriptionPro.Text,
                name = ProjName.Value,
                status = EntityStatus.Active.GetHashCode(),
                conclusion = conclution
            };

            _insendluEntities.Projects.Add(newProj);
            var projId = _insendluEntities.SaveChanges();

            if (projId == 1)
            {
                var newId = newProj.id;

                var executiveSummary = execSummary.Content;
                var id = Executive(executiveSummary, newId);

                var company = companyBackground.Content;
                var companyId = GetCompany(company, newId);

                //var dpwProperty = dpw.Content;
                //var methId = GetDpw(dpwProperty, newId);

                var methodology = propMethodology.Content;
                var methodoId = GetMethodology(methodology, newId);
                

                var costPlanV = costPlan.Content;
                var costPlanId = GetProjectCostPlan(costPlanV, newId);

                //var bee = whyChoose.Content;
                //var companyId = GetCompanyDetails(bee, newId);

                var projAnalysis = riskanalysis.Content;
                var analysisId = GetProjectAnalysis(projAnalysis, newId);

                var resources = resource.Content;

                var updateProject = (from pr in _insendluEntities.Projects
                                    where pr.id == newId
                                    select pr).Single();

                if (updateProject != null)
                {
                    updateProject.cost_plan_id = costPlanId;
                    updateProject.company_id = companyId;
                    updateProject.exec_summary_id = id;
                    updateProject.methodology_id = methodoId;
                    updateProject.policy_id = analysisId;

                    _insendluEntities.SaveChangesAsync();

                    SaveProject(updateProject);
                }
            }

        }
        private void SaveProject(Project projects)
        {
            var id = projects.id;

            var execSumarry = _projectService.GetExecSummary(id);
            var company = _projectService.GetCompanyById(id);
            var dpwProperty = _projectService.GetProjectPolicy(id);
            var methodology = _projectService.GetProjMethodolgy(id);
            var cosPlan = _projectService.GetCostPlan(id);

            var r = new Rectangle(400, 300);
            var doc1 = new Document(r);
            var path = Server.MapPath("PDF's");

            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(path + "/" + projects.name + projects.id + ".pdf", FileMode.Create));
                doc1.Open();
                var text = projects.name + " Project";

                text = text.Replace(Environment.NewLine, String.Empty).Replace("  ", String.Empty);
                var brown = new Font(Font.FontFamily.COURIER, 9f, Font.NORMAL, new BaseColor(163, 21, 21));
                var lightblue = new Font(Font.FontFamily.COURIER, 9f, Font.NORMAL, new BaseColor(43, 145, 175));
                var courier = new Font(Font.FontFamily.TIMES_ROMAN, 4f);
                var georgia = FontFactory.GetFont("georgia", 10f);
                //var header = new Font(Font.FontFamily.TIMES_ROMAN, 9f, 1, BaseColor.LIGHT_GRAY);

                var beginning = new Chunk(text, brown);
                var p1 = new Phrase(beginning);
                var par = new Paragraph(p1);

                var c1 = new Chunk("Executive Summary", lightblue);
                var heading1 = new Phrase(c1);
                var para = new Paragraph(heading1);

                var c4 = new Chunk(execSumarry.content, courier);
                var ph = new Phrase(c4);
                var phr = new Paragraph(ph);

                var dpw = new Chunk("DPW Property", lightblue);
                var heading2 = new Phrase(dpw);
                var property = new Paragraph(heading2);

                var c5 = new Chunk(dpwProperty.name, courier);
                var ph1 = new Phrase(c5);
                var compInfo = new Paragraph(ph1);

                var compHeading = new Chunk("Company Information", lightblue);
                var heading3 = new Phrase(compHeading);
                var compa = new Paragraph(heading3);

                var c6 = new Chunk(company.mission_statement, courier);
                var ph2 = new Phrase(c6);
                var comp = new Paragraph(ph2);

                var meth = new Chunk("Methodology", lightblue);
                var heading4 = new Phrase(meth);
                var methology = new Paragraph(heading4);

                var c7 = new Chunk(methodology.content, courier);
                var ph3 = new Phrase(c7);
                var metho = new Paragraph(ph3);

                var cost = new Chunk("Cost Plan Summary", lightblue);
                var heading5 = new Phrase(cost);
                var planCost = new Paragraph(heading5);

                var c8 = new Chunk(cosPlan.details, courier);
                var ph4 = new Phrase(c8);
                var costPla = new Paragraph(ph4);

                var conclu = new Chunk("Conclusion", lightblue);
                var con = new Phrase(conclu);
                var lusion = new Paragraph(con);

                var c9 = new Chunk(projects.conclusion, courier);
                var ph5 = new Phrase(c9);
                var conclusion = new Paragraph(ph5);

                doc1.Add(par);

                doc1.Add(para);
                doc1.Add(phr);

                doc1.Add(property);
                doc1.Add(compInfo);

                doc1.Add(methology);
                doc1.Add(metho);

                doc1.Add(compa);
                doc1.Add(comp);

                doc1.Add(planCost);
                doc1.Add(costPla);

                doc1.Add(lusion);
                doc1.Add(conclusion);
            }
            catch (DocumentException dex)
            {
                throw (dex);
            }
            catch (IOException ioex)
            {
                throw (ioex);
            }
            finally
            {
                doc1.Close();

                //lblDownload.Text = "Project saved successfuly " + projects.name;
                //lblDownload.Visible = true;
            }

        }

        private int GetProjectAnalysis(string projAnalysis, long newId)
        {
            var projectAnalysis = new ProjectPolicy()
            {
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                policy_number = "",
                name = "Policy Name Here",
                proj_id = (int) newId
            };

            _insendluEntities.ProjectPolicies.Add(projectAnalysis);

            try
            {
                _insendluEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return (int) projectAnalysis.id;
        }

        private int GetProjectCostPlan(string projRef, long newId)
        {
            var proReference = new ProjectCostPlan()
            {
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                details = projRef,
                proj_id = (int) newId
            };

            _insendluEntities.ProjectCostPlans.Add(proReference);

            try
            {
                _insendluEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return (int) proReference.id;
        }

        private int Executive(string execsum, long projId)
        {
            var exec = new ExecutiveSummary()
            {
                content = execsum,
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                proj_id = (int)projId

            };

            _insendluEntities.ExecutiveSummaries.Add(exec);

            try
            {
                _insendluEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return (int) exec.id;

        }

        private int GetCompany(string content, long projId)
        {
            var company = new Company()
            {
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                mission_statement = content,
                proj_id = (int) projId

            };

            _insendluEntities.Companies.Add(company);

            try
            {
                _insendluEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }

            return (int) company.id;
        }

        private int GetDpw(string content, long projId)
        {
            var dpw = new ProjectPolicy()
            {
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                policy_number = content,
                proj_id = (int)projId
            };

            _insendluEntities.ProjectPolicies.Add(dpw);

            return _insendluEntities.SaveChanges();
        }

        private int GetMethodology(string content, long projId)
        {
            var methodology = new ProjectMethodology()
            {
                created_at = DateTime.Now,
                modified_at = DateTime.Now,
                content = content,
                proj_id = (int) projId
            };

            _insendluEntities.ProjectMethodologies.Add(methodology);

            try
            {
                _insendluEntities.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                // Throw a new DbEntityValidationException with the improved exception message.
                throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
            }
            return (int) methodology.id;
        }
       

        
    }
}