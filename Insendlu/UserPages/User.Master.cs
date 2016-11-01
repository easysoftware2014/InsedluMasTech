using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Insendlu.UserPages
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var username = Session["Username"];
                userLabel.Text = username.ToString();

                var image = Session["image"];
                userImage.ImageUrl = image.ToString();

                
            }
        }
    }
}