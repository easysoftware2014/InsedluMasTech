using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Insendlu.Entities.Connection;
using Insendu.Services;

namespace Insendlu.UserPages
{
    public partial class EditProject : System.Web.UI.Page
    {
        private readonly ProjectService _projectService;
        private readonly InsendluEntities _insendluEntities;

        public EditProject()
        {
            _projectService = new ProjectService();
            _insendluEntities = new InsendluEntities();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var exec    = Session["execSummary"];
                var company = Session["company"];
                var projName = Session["projectName"];
                var description = Session["ProjDescription"];
                var conclu = Session["conclusion"];
                var costplan = Session["costPlan"];
                var methodology = Session["methodology"];

                execSummary.Content = exec.ToString();
                companyBackground.Content = company.ToString();
                conclusion.Content = conclu.ToString();
                descriptionPro.Text = description.ToString();
                projectName.Text = projName.ToString();
                costPlan.Content = costplan.ToString();
                propMethodology.Content = methodology.ToString();

            }
        }

        protected void Update_OnClick(object sender, EventArgs e)
        {
            var id = Convert.ToInt64(Session["ID"]);
            var modification = DateTime.Now;

            var comp = companyBackground.Content;
            var execSumm = execSummary.Content;
            var costP = costPlan.Content;
            var methodol = propMethodology.Content;
            var poli = riskanalysis.Content;
            var projDesc = descriptionPro.Text;
            var projName = projectName.Text;
            var conlusion = conclusion.Content;

            var proj = _projectService.GetProject(id);
            proj.description = projDesc;
            proj.name = projName;
            proj.conclusion = conlusion;
            proj.modified_at = modification;

            _insendluEntities.Entry(proj).State = System.Data.Entity.EntityState.Modified;
            _insendluEntities.SaveChanges();

            var execSummar = (from exec in _insendluEntities.ExecutiveSummaries
                                where exec.proj_id == id
                                select exec).SingleOrDefault();

            UpdateExecSummary(execSumm, execSummar, modification);

            var plan = (from cost in _insendluEntities.ProjectCostPlans
                            where cost.proj_id == id
                            select cost).SingleOrDefault();

            UpdateCostPlan(costP, plan, modification);

            var company = (from compan in _insendluEntities.Companies
                             where compan.proj_id == id
                             select compan).SingleOrDefault();

            UpdateCompany(comp, company, modification);

            var methodology = (from meth in _insendluEntities.ProjectMethodologies
                                where meth.proj_id == id
                                select meth).SingleOrDefault();

            UpdateProjectMethodology(methodol, methodology, modification);

            var policy = (from polic in _insendluEntities.ProjectPolicies
                            where polic.proj_id == id
                            select polic).SingleOrDefault();

            UpdateprojectPolicy(poli, policy, modification);


            SaveProject(proj);

            Response.Redirect("~/ActiveProjects.aspx");
        }

        private void UpdateprojectPolicy(string poli, ProjectPolicy policy, DateTime date)
        {
            policy.content = poli;
            policy.modified_at = date;

            _insendluEntities.Entry(policy).State = System.Data.Entity.EntityState.Modified; 
            _insendluEntities.SaveChanges();
        }

        private void UpdateProjectMethodology(string methodol, ProjectMethodology methodology, DateTime date)
        {
            methodology.content = methodol;
            methodology.modified_at = date;

            _insendluEntities.Entry(methodology).State = System.Data.Entity.EntityState.Modified;
            _insendluEntities.SaveChanges();
        }

        private void UpdateCompany(string comp, Company company, DateTime date)
        {
            company.mission_statement = comp;
            company.modified_at = date;

            _insendluEntities.Entry(company).State = System.Data.Entity.EntityState.Modified;
            _insendluEntities.SaveChanges();
        }

        private void UpdateCostPlan(string costP, ProjectCostPlan plan, DateTime date)
        {
            plan.details = costP;
            plan.modified_at = date;

            _insendluEntities.Entry(plan).State = System.Data.Entity.EntityState.Modified;
            _insendluEntities.SaveChanges();

        }

        private void UpdateExecSummary(string execSumm, ExecutiveSummary execSummar, DateTime date)
        {
            execSummar.content = execSumm;
            execSummar.modified_at = date;

            _insendluEntities.Entry(execSummar).State = System.Data.Entity.EntityState.Modified;
            _insendluEntities.SaveChanges();
        }
        private void SaveProject(Project project)
        {
            var id = project.id;

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
                PdfWriter.GetInstance(doc1, new FileStream(path + "/" + project.name + project.id + ".pdf", FileMode.Create));
                doc1.Open();
                var text = project.name + " Project";

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

                var c6 = new Chunk(company.name, courier);
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

                var c9 = new Chunk(project.conclusion, courier);
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
    }
}