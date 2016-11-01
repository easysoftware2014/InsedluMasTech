using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using Insendlu.Entities.Connection;
using Insendu.Services;
using Insendlu.Encryptor;

namespace Insendlu
{
    public partial class HomePage : System.Web.UI.Page
    {
        private readonly InsendluEntities _insendluEntities;
        private readonly ProjectService _projectService;
        private readonly Encryptor.Encryptor _encryptor;

        public HomePage()
        {
            _insendluEntities = new InsendluEntities();
            _projectService = new ProjectService();
            _encryptor = new Encryptor.Encryptor();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var username = txtEmail.Value;
            var password = txtPassword.Value;
            var decryDecrypted = _encryptor.Encrypt(password);

            var user = (from users in _insendluEntities.Users
                        where users.email == username
                        select users).Single();

            if (user != null)
            {
                if (user.password == decryDecrypted)
                {
                    Session["Username"] = user.name;
                    Session["ID"] = user.id;
                    Session["image"] = "assets/images/avatars/user.jpg";

                    if (user.user_type_id == 2)
                    {
                        Response.Redirect("~/UserPages/Index.aspx");
                    }
                    Response.Redirect("AdminPage.aspx");
                }
                else
                {
                    errorMessage.Text = "Incorrect credentials, re-try again";
                    errorMessage.Visible = true;
                }
                

            }
            else
            {
                errorMessage.Text = "No user found for that credentials, please try again later";
                errorMessage.Visible = true;
            }
           
            
        }

        protected void btnRegister_OnClick(object sender, EventArgs e)
        {
            
            var pass = txtNewUserPassword.Value;
            var username = txtNewUserEmail.Value;
            var firstName = name.Value;
            var lastName = surname.Value;

            var id = _projectService.AddUser(firstName, lastName, username, pass);

            
        }
    }
}