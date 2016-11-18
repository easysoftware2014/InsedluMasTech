using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Insendlu
{
    public partial class AdminPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["ID"].ToString()))
            {
                Response.Redirect("index.aspx");
            }

            if (!IsPostBack)
            {
                
                
            }
        }
    }
}