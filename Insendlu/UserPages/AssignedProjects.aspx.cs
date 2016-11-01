using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Insendlu.Entities;
using Insendlu.Entities.Connection;
using Insendu.Services;

namespace Insendlu.UserPages
{
    public partial class AssignedProjects : System.Web.UI.Page
    {
        private readonly InsendluEntities _insendluEntities;
        private readonly ProjectService _projectService;

        public AssignedProjects()
        {
            _projectService = new ProjectService();
            _insendluEntities = new InsendluEntities();
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
            var id = Convert.ToInt32(Session["ID"]); // user id

            var proIds = (from proj in _insendluEntities.User_Project
                            where proj.user_id ==  id
                            select new {ID = proj.proj_id}).ToList();

            if (proIds.Count != 0)
            {
                var projects = new List<Project>(proIds.Count);

                foreach (var pro in proIds)
                {
                    var nje = (from proj in _insendluEntities.Projects
                        where proj.id == pro.ID
                        select proj).Single();

                   projects.Add(nje);
                }
                 
                datagridview.DataSource = projects.ToList();
                datagridview.DataBind();

            }
            else
            {
                Response.Redirect("~/AdminPage.aspx");
            }
        }
        protected void datagridview_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            datagridview.PageIndex = e.NewPageIndex;
            DataGridBind();
        }

        protected void datagridview_RowCommand(object sender, GridViewCommandEventArgs e)
        {
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
            Session["ProjectID"] = id;

            Response.Redirect("~/UserPages/EditProject.aspx", false);

        }

        private void ReadIT(string projName)
        {
            Response.ContentType = "Application/pdf";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + projName + ".pdf");
            Response.TransmitFile(Server.MapPath("~/PDF's/" + projName + ".pdf"));
            Response.End();
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

        protected void datagridview_OnRowEditing(object sender, GridViewEditEventArgs e)
        {
            DataGridBind();
            //DataGridBindEdit();
        }

        private void DataGridBindEdit()
        {
            var projId = Convert.ToInt32(Session["ProjectID"]);

        }
    }
}