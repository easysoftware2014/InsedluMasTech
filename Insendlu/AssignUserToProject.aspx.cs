using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Insendlu.Entities;
using Insendlu.Entities.Connection;

namespace Insendlu
{
    public partial class AssignUserToProject : System.Web.UI.Page
    {
        private readonly Connect _connect;
        private readonly InsendluEntities _insendluEntities;

        public AssignUserToProject()
        {
            _insendluEntities = new InsendluEntities();
            _connect = new Connect();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["ID"].ToString()))
            {
                Response.Redirect("index.aspx");
            }
            if (!IsPostBack)
            {
                var projList = _insendluEntities.Projects.Select(x => x.name).ToList();

                projectList.DataSource = projList;
                projectList.DataBind();

                var loggedUsers = _insendluEntities.Users.ToList();

                selectUsers.DataSource = loggedUsers;
                selectUsers.DataBind();

            }

        }

        protected void Assign_OnClick(object sender, EventArgs e)
        {
            // throw new NotImplementedException();
        }

        protected void selectUsers_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = selectUsers.SelectedIndex.ToString();
        }

        protected void btnChosen_OnClick(object sender, EventArgs e)
        {
            var userSelected = selectUsers.SelectedItem.Text;

            if (string.IsNullOrEmpty(txtUsers.Text))
            {
                txtUsers.Text = userSelected + ",";
            }
            else
            {
                txtUsers.Text += userSelected;
            }

        }

        protected void selectUsers_OnTextChanged(object sender, EventArgs e)
        {
            var s = selectUsers.SelectedItem.Text;
        }
    }
}