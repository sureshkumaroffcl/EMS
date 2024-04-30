using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace IssueTrackingSystem
{
    public partial class UILogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string UserType = string.Empty;

            if (rdoEmployee.Checked)
            {
                UserType = "Employee";
            }
            //else if (rdoClient.Checked)
            //{
            //    UserType = "Client";
            //}

            SqlConnection con = Connection.GetConnectionString();
            con.Open();
            SqlCommand cmd = new SqlCommand("spProcessUserType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmpIdentifier", txtUsername.Text);
            cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
            cmd.Parameters.AddWithValue("@UserType", UserType);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                //string employeeID = reader["EmpID"].ToString();
                //string clientID = reader["ClientID"].ToString();


                if (reader.Read())
                {

                    string response = reader["Response"].ToString();


                    if (response == "Employee")
                    {
                        string employeeName = GetSafeStringValue(reader, "EmpName");
                        string employeeType = GetSafeStringValue(reader, "EmpType");
                        string employeeID = GetSafeStringValue(reader, "EmpID");

                        Session["EmpID"] = employeeID;
                        Session["EmpName"] = employeeName;
                        Session["EmpType"] = employeeType;
                        reader.Close();
                        con.Close();
                        Response.Redirect("~/EmployeeMaster.aspx?Username=" + employeeName);
                    }
                    else if (response == "Client")
                    {
                        string clientName = GetSafeStringValue(reader, "ClientName");
                        reader.Close();
                        con.Close();
                        Response.Redirect("Home.aspx");
                    }

                }


                else
                {

                    string alertMessage = "<div class= 'alert alert-danger color-danger'> <i class= 'bi bi-exclamation-circle'></i>  Username or Password is invalid. </div>";
                    alertPlaceholder.Controls.Add(new LiteralControl(alertMessage));

                    txtUsername.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                }

                reader.Close();
                con.Close();
            }
        }

        private string GetSafeStringValue(SqlDataReader reader, string columnName)
        {
            //int columnIndex = reader.GetOrdinal(columnName);
            //return columnIndex != -1 && !reader.IsDBNull(columnIndex) ? reader.GetString(columnIndex) : string.Empty;

            return reader[columnName] is DBNull ? string.Empty : reader[columnName].ToString();
        }

       

    }
}