using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.UI;
using System.Web.UI.WebControls;
using SignalRChat;

namespace Insendlu
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["ID"].ToString()))
            {
                Response.Redirect("index.aspx");
            }

            if (!IsPostBack)
            {
                var username = Session["Username"];
                userLabel.Text = username.ToString();

                var image = Session["image"];
                userImage.ImageUrl = image.ToString();
            }
        }

        protected void Newproj_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/NewProject.aspx", false);
        }

        protected void Chat_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("http://localhost:13460/index.html");
        }

        protected void logout_OnClick(object sender, EventArgs e)
        {
            Session.Clear();

            Response.Redirect("~/Account/Login.aspx", false);
        }
     
        private void Logout()
        {
            Session.Clear();

            Response.Redirect("~/Account/Login.aspx", false);
        }
    }
}