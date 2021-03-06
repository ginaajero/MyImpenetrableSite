﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace MyImpenetrableSite
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            // Create a SQL connection object
            SqlConnection conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["MISConnectionString"].ToString());

            // Create a SQL command object to query username
            // string usernameQuery = "SELECT * FROM Users WHERE Username = '" + txtUsername.Text.Trim() + "'";
            string usernameQuery = "SELECT * FROM Users WHERE Username = @Username";//Gina's fix
            SqlCommand cmd = new SqlCommand(usernameQuery, conn);
            SqlParameter param = new SqlParameter("@Username", txtUsername.Text.Trim());//Gina's fix
            cmd.Parameters.Add(param);//Gina's fix

            conn.Open();
            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            if (!sqlDataReader.HasRows)
            {
               // lblLoginError.Text = "The username you entered does not exist. Please try again.";
                lblLoginError.Text = "Please try again.";
                conn.Close();
            }
            else
            {
                conn.Close();
              //  string strQuery = "SELECT * FROM Users WHERE Username = '" + txtUsername.Text.Trim() + "' AND Password = '" + txtPassword.Text.Trim() + "'";
                string strQuery = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";//Gina's fix
                cmd = new SqlCommand(strQuery, conn);
                SqlParameter[] paramArray = new SqlParameter[2];//Learned online how to parameterize queries
                paramArray[0] = new SqlParameter("@UserID", txtUsername.Text.Trim());
                paramArray[1] = new SqlParameter("@pwd", txtPassword.Text.Trim());
                cmd.Parameters.Add(paramArray[0]);
                cmd.Parameters.Add(paramArray[1]);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows)
                {
                    //lblLoginError.Text = "The password you entered is not correct. Please try again.";
                    lblLoginError.Text = "Please try again.";
                }
                else
                {
                    reader.Read();
                    int roleId = int.Parse(reader["RoleId"].ToString());
                    if (roleId == 1)  // Administrator
                    {
                        reader.Close();
                        conn.Close();
                        Response.Redirect("Admin.aspx");
                    }
                    else
                    {
                        int statusId = int.Parse(reader["StatusId"].ToString());
                        string userId = reader["ID"].ToString();
                        reader.Close();
                        conn.Close();

                        if (statusId == 2)
                        {
                            //lblLoginError.Text = "Your account is inactive. Please contact the administrator to deactivate your account first.";
                            lblLoginError.Text = "Please contact the administrator.";
                        }
                        else
                        {
                            Response.Redirect("Members.aspx?Id=" + userId);
                        }

                    }
                }
            }
        }
    }
}