using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.UI.WebControls;
using Insendlu.Entities;
using Insendlu.Entities.Connection;

using Insendu.Services;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace Insendlu
{
    public partial class ActiveProjects : Page
    {
        private readonly InsendluEntities _insendluEntities;
        private readonly ProjectService _projectService;
      
        public ActiveProjects()
        {
            _insendluEntities = new InsendluEntities();
            _projectService = new ProjectService();
           
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataGridBind();
            }
        }

        private void DataGridBind()
        {
            var projects = (from proj in _insendluEntities.Projects
                           where proj.status == (int)EntityStatus.Active
                           select proj).ToList();

            if (projects.Count != 0)
            {
                datagridview.DataSource = projects.ToList();
                datagridview.DataBind();
                
            }
            else
            {
                Response.Redirect("AdminPage.aspx");
            }
        }

        protected void activeProjects_OnRowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void activeProjects_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drv = e.Row.DataItem as DataRowView;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((e.Row.RowState & DataControlRowState.Edit) > 0)
                {
                    DropDownList dropDown = (DropDownList)e.Row.FindControl("drpAvailableSlots");
                }
            }
        }

        protected void activeProjects_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //var id = int.Parse(activeProjects.DataKeys[e.RowIndex].Value.ToString());
        }

        protected void datagridview_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            //activeProjects.EditIndex = e.NewEditIndex;
            DataGridBind();
        }
       
        protected void activeProjects_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //var id = int.Parse(activeProjects.DataKeys[e.RowIndex].Value.ToString());

        }

        protected void activeProjects_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {
                int rowno = Int32.Parse(e.CommandArgument.ToString());
            }
        }

        protected void datagridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Here we will be passing the command name which we assigned to the button
            if (e.CommandName == "ViewDetails")
            {
                
                var rowno = int.Parse(e.CommandArgument.ToString());  // It is the rowno of which the user as clicked

                var row = datagridview.Rows[rowno];  // logical 0,1,2,3,4,5
                var label = (Label)row.FindControl("lblId");
                var id = Convert.ToInt32(label.Text);

                var projects = (from proj in _insendluEntities.Projects
                                where proj.id == id
                                select proj).Single();

                var exec = (from execs in _insendluEntities.ExecutiveSummaries
                    where execs.proj_id == projects.id
                    select execs).FirstOrDefault();

                //execSummary.Content = exec.content;

                lblname.Text = "Project name : " + projects.name;
                if (projects.created_at != null) lbldesc.Text = "Date Created : " + projects.created_at.Value.Date.ToShortDateString();
                lblstore.Text = "Project Status : " + projects.status;

                //var productPrice = datagridview.Rows[rowno].Cells[3].Text.ToString();
                //lblprice.Text = "Product Price : " + productPrice;
                lblDownload.Visible = false;
            }
            if (e.CommandName == "Download")
            {
                Download(sender, e);
            }
            if (e.CommandName == "Edit")
            {
                EditProject(sender, e);
            }
        }

        private void EditProject(object sender, GridViewCommandEventArgs e)
        {
            var rowno = int.Parse(e.CommandArgument.ToString());

            var row = datagridview.Rows[rowno];
            var label = (Label)row.FindControl("lblId");
            var id = Convert.ToInt32(label.Text);

            var execSumarry = _projectService.GetExecSummary(id).content;
            var company = _projectService.GetCompanyById(id).mission_statement;
            var dpwProperty = _projectService.GetProjectPolicy(id).name;
            var methodology = _projectService.GetProjMethodolgy(id).content;
            var costPlan = _projectService.GetCostPlan(id).details;
            var project = _projectService.GetProject(id);

            Session["execSummary"] = execSumarry;
            Session["projectName"] = project.name;
            Session["ProjDescription"] = project.description;
            Session["conclusion"] = project.conclusion;
            Session["company"] = company;
            Session["policy"] = dpwProperty;
            Session["methodology"] = methodology;
            Session["costPlan"] = costPlan;
            Session["ID"] = id;

            Response.Redirect("~/EditProject.aspx", false);

        }

        private void Download(object sender, GridViewCommandEventArgs e)
        {
            var rowno = int.Parse(e.CommandArgument.ToString());  // It is the rowno of which the user as clicked

            var row = datagridview.Rows[rowno];
            var label = (Label)row.FindControl("lblId");
            var id = Convert.ToInt32(label.Text);

            var projects = (from proj in _insendluEntities.Projects
                            where proj.id == id
                            select proj).Single();

            var projName = projects.name + projects.id;

            ReadIT(projName);

            //ProvideContent(projects);
            //lblDownload.Text = "This functionality of downloading still to be added :)" + projects.name;
            //lblDownload.Visible = true;
        }

        private void ReadIT(string projName)
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename="+projName+".pdf");
            Response.TransmitFile(Server.MapPath("~/PDF's/"+projName+".pdf"));
            Response.End();
        }

        private void ProvideContent(Project projects)
        {
            var id = projects.id;

            var execSumarry = _projectService.GetExecSummary(id);
            var company = _projectService.GetCompanyById(id);
            var dpwProperty = _projectService.GetProjectPolicy(id);
            var methodology = _projectService.GetProjMethodolgy(id);
            var costPlan = _projectService.GetCostPlan(id);

            var r = new Rectangle(400, 300);
            var doc1 = new Document(r);
            var path = Server.MapPath("PDF's");
           
            try
            {
                PdfWriter.GetInstance(doc1, new FileStream(path + "/" + projects.name + projects.id + ".pdf", FileMode.Create));
                doc1.Open();
                var text = projects.name + "Project";

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

                var c8 = new Chunk(costPlan.details, courier);
                var ph4 = new Phrase(c8);
                var costPla = new Paragraph(ph4);

                var conclu = new Chunk("Conclusion", lightblue);
                var con = new Phrase(conclu);
                var lusion = new Paragraph(con);

                var c9 = new Chunk(projects.conclusion,courier);
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

                lblDownload.Text = "Project saved successfuly " + projects.name;
                lblDownload.Visible = true;
            }

        }
        protected void datagridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            datagridview.PageIndex = e.NewPageIndex;
            DataGridBind();

        }

      
    }
}