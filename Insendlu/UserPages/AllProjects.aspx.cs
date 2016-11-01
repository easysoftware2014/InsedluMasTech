using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Insendlu.Entities;
using Insendlu.Entities.Connection;

namespace Insendlu.UserPages
{
    public partial class AllProjects : System.Web.UI.Page
    {
        private readonly InsendluEntities _insendluEntities;

        public AllProjects()
        {
            _insendluEntities = new InsendluEntities();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBindBind();
            }
        }

        private void DataBindBind()
        {
            var projects = (from proj in _insendluEntities.Projects
                           select proj).ToList();

            if (projects.Count != 0)
            {
                datagridviews.DataSource = projects.ToList();
                datagridviews.DataBind();

            }
            else
            {
                Response.Redirect("AdminPage.aspx");
            }
        }

        protected void datagridviews_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewDetails")
            {

                var rowno = int.Parse(e.CommandArgument.ToString());  // It is the rowno of which the user as clicked

                var row = datagridviews.Rows[rowno];  // logical 0,1,2,3,4,5
                var label = (Label)row.FindControl("lblId");
                var id = Convert.ToInt32(label.Text);

                var projects = (from proj in _insendluEntities.Projects
                                where proj.id == id
                                select proj).Single();

                lblnames.Text = "Project name : " + projects.name;
                if (projects.created_at != null) lbldescs.Text = "Date Created : " + projects.created_at.Value.Date.ToShortDateString();
                lblstores.Text = "Project Status : " + projects.status;

                //var productPrice = datagridview.Rows[rowno].Cells[3].Text.ToString();
                //lblprice.Text = "Product Price : " + productPrice;

            }
            if (e.CommandName == "Download")
            {
                Download(sender, e);
            }
        }
        private void Download(object sender, GridViewCommandEventArgs e)
        {
            var rowno = int.Parse(e.CommandArgument.ToString());  // It is the rowno of which the user as clicked

            var row = datagridviews.Rows[rowno];
            var label = (Label)row.FindControl("lblId");
            var id = Convert.ToInt32(label.Text);

            var projects = (from proj in _insendluEntities.Projects
                            where proj.id == id
                            select proj).Single();

            //viewModal.Visible = true;
        }
        protected void datagridviews_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            datagridviews.PageIndex = e.NewPageIndex;
            DataBindBind();
        }

    }
}