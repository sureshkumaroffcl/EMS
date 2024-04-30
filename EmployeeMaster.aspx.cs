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
    public partial class EmployeeMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string employeeType = Session["EmpType"] as string;
                if (employeeType.Equals("Admin", StringComparison.OrdinalIgnoreCase) ||

                    employeeType.Equals("Director Strategies", StringComparison.OrdinalIgnoreCase) ||

                employeeType.Equals("Managing Director", StringComparison.OrdinalIgnoreCase))
                {
                    txtStatus.Enabled = true;
                    txtRemark.Enabled = true;
                    txtRelievingDate.Enabled = true;
                    txtStatus.CssClass = "form-control";
                    txtRemark.CssClass = "form-control";
                    txtRelievingDate.CssClass = "form-control";


                }
                else
                {
                    txtStatus.Enabled = false;
                    txtRemark.Enabled = false;
                    txtRelievingDate.Enabled = false;
                    txtStatus.CssClass = "form-control disabled-cursor";
                    txtRemark.CssClass = "form-control disabled-cursor";
                    txtRelievingDate.CssClass = "form-control disabled-cursor";

                }

                txtJoiningDate.Attributes["max"] = DateTime.Now.ToString("yyyy-MM-dd");
                txtEmpPassword.Attributes["type"] = "password";
                ddlEmpTypeBindGrid();
                ResetRecord();
                
              }

        }

        protected void txtEmpID_TextChanged(object sender, EventArgs e)
        {
            string employeeID = txtEmpID.Text;

            if (string.IsNullOrEmpty(employeeID))
            {
                ResetRecord();
                btnSave.Text = "Save";

                // Show error message
              
                return;
            }
            
            DataTable employeeTable = GetEmployeeDetails(employeeID);
            string loggedInEmpID = Session["EmpID"].ToString();

            string employeeType = Session["EmpType"] as string;
        
            if (employeeTable.Rows.Count > 0)
            {

                ddlEmpType.Items[0].Attributes.Add("disabled", "disabled");
                ddlEmpType.Items[0].Attributes.Add("class", "disabled-cursor");
                ddlEmpType.CssClass = "choices form-select";

                DataRow employeeRow = employeeTable.Rows[0];
                string empID = employeeRow["EmpID"].ToString();
               
                txtEmpID.Enabled = false;
                txtEmpID.CssClass = "form-control disabled-cursor";

                txtEmpName.Text = employeeRow["EmpName"].ToString();
                txtDesignation.Text = employeeRow["Designation"].ToString();

                DateTime JoiningDate = Convert.ToDateTime(employeeRow["JoiningDate"]);

                txtJoiningDate.Text = JoiningDate.ToString("yyyy-MM-dd");

                string selectedValue = employeeRow["EmpType"].ToString();

                ddlEmpType.ClearSelection();
                ddlEmpType.Items.FindByText(selectedValue).Selected = true;

               // txtEmpPassword.Attributes["type"] = "text";
               // txtEmpPassword.Attributes["value"] = employeeRow["EmpPassword"].ToString();
                txtEmpPassword.Text = employeeRow["EmpPassword"].ToString();
                
                txtMailID.Text = employeeRow["MailID"].ToString();
                txtMobileNo.Text = employeeRow["MobileNo"].ToString();
                txtAddress.Text = employeeRow["Address"].ToString();
                txtStatus.Text = employeeRow["Status"].ToString();
                txtRemark.Text = employeeRow["Remark"].ToString();

                DateTime? endDate = employeeRow["RelievingDate"] as DateTime?;

                if (endDate.HasValue)
                {
                    txtRelievingDate.Text = endDate.Value.ToString("yyyy-MM-dd");
                }
                else
                {
                    txtRelievingDate.Text = string.Empty; 
                }

                //empID == loggedInEmpID

                if ((employeeType.Equals("Admin", StringComparison.OrdinalIgnoreCase)) || employeeType.Equals("Director Strategies", StringComparison.OrdinalIgnoreCase) ||
                    
                employeeType.Equals("Managing Director", StringComparison.OrdinalIgnoreCase))
                {
                    
                    EnableAllTextBoxes(true);
                    ApplyEnabledStyles();
                   
                }
                else
                {
                   
                    EnableAllTextBoxes(false);
                    ApplyDisabledStyles();
                   
                }


                btnSave.Text = "Update";
           
            }
            else
            {
                EnableAllTextBoxes(true);
                ApplyEnabledStyles();

                txtEmpID.Enabled = true;

                txtEmpID.CssClass = "form-control";
                txtEmpName.Text = string.Empty;
                txtDesignation.Text = string.Empty;
                txtJoiningDate.Text = string.Empty;

                ddlEmpType.SelectedIndex = 0;
                ddlEmpType.Items[0].Attributes.Add("disabled", "disabled");
                ddlEmpType.Items[0].Attributes.Add("class", "disabled-cursor");
                ddlEmpType.CssClass = "choices form-select";

                txtEmpPassword.Text = string.Empty;
            
                txtMailID.Text = string.Empty;
                txtMobileNo.Text = string.Empty;
                txtAddress.Text = string.Empty;
                txtStatus.Text = string.Empty;
                txtRemark.Text = string.Empty;
                txtRelievingDate.Text = string.Empty; 

          
                btnSave.Text = "Save";
            }

        }

        private void EnableAllTextBoxes(bool enable)
        {
            txtEmpName.Enabled = enable;
            txtDesignation.Enabled = enable;
            txtJoiningDate.Enabled = enable;
            ddlEmpType.Enabled = enable;
            txtEmpPassword.Enabled = enable;
            txtMailID.Enabled = enable;
            txtMobileNo.Enabled = enable;
            txtAddress.Enabled = enable;
            btnSave.Text = "Update";
            btnSave.Enabled = enable;
            
        
        }

        void ApplyEnabledStyles()
        {
            txtEmpName.CssClass = "form-control ";
            txtDesignation.CssClass = "form-control ";
            txtJoiningDate.CssClass = "form-control ";
            ddlEmpType.CssClass = "choices form-select ";
            txtEmpPassword.CssClass = "form-control ";
            txtMailID.CssClass = "form-control ";
            txtMobileNo.CssClass = "form-control ";
            txtAddress.CssClass = "form-control ";
            btnSave.CssClass = "btn btn-primary me-1 mb-1";

           
        }

       
        void ApplyDisabledStyles()
        {
            txtEmpName.CssClass = "form-control disabled-cursor";
            txtDesignation.CssClass = "form-control disabled-cursor";
            txtJoiningDate.CssClass = "form-control disabled-cursor";
            ddlEmpType.CssClass = "choices form-select disabled-cursor";
            txtEmpPassword.CssClass = "form-control disabled-cursor";
            txtMailID.CssClass = "form-control disabled-cursor";
            txtMobileNo.CssClass = "form-control disabled-cursor";
            txtAddress.CssClass = "form-control disabled-cursor";
            btnSave.CssClass = "btn btn-primary me-1 mb-1 btn disabled-cursor";
 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string createdBy = Session["EmpName"].ToString();
            string modifiedBy = Session["EmpName"].ToString();
        
            SqlConnection con = Connection.GetConnectionString();
            con.Open();
            SqlCommand cmd = new SqlCommand("spInsertOrUpdateEmployeeDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            DateTime endDate;

            if (DateTime.TryParse(txtRelievingDate.Text, out endDate))
            {
                cmd.Parameters.AddWithValue("@RelievingDate", endDate.ToString("yyyy-MM-dd"));
            }
            else
            {
                cmd.Parameters.AddWithValue("@RelievingDate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@EmpID", txtEmpID.Text);
            cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
            cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
            cmd.Parameters.AddWithValue("@JoiningDate", string.Format("{0:yyyy-MM-dd}", txtJoiningDate.Text));
            cmd.Parameters.AddWithValue("@EmpType", ddlEmpType.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@EmpPassword", txtEmpPassword.Text);
            cmd.Parameters.AddWithValue("@MailID", txtMailID.Text);
            cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
            cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
            cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
            cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
       
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
            cmd.Parameters.AddWithValue("@ModifiedBy", createdBy);

            cmd.Parameters.Add("@Result", System.Data.SqlDbType.VarChar, 50).Direction = System.Data.ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            string result = cmd.Parameters["@Result"].Value.ToString();

            if (result == "updated")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "recordUpdated", "Swal.fire('Record Updated', 'The record has been Updated successfully!', 'success');", true);

            }
            else if (result == "inserted")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "recordSaved", "Swal.fire('Record Inserted', 'The record has been Saved successfully!', 'success');", true);

            }

            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "recordError", "Swal.fire('Error', 'Failed to save or update the record. Please check requied data.', 'error');", true);
            }


            con.Close();
            ResetRecord();

        }

        private void ResetRecord()
        {

            EnableAllTextBoxes(true);
            ApplyEnabledStyles();
            txtEmpID.Enabled = true;
            txtEmpID.CssClass = "form-control";
            txtEmpID.Text = string.Empty;

            txtEmpName.Text = string.Empty;
            txtDesignation.Text = string.Empty;
            txtJoiningDate.Text = string.Empty;

            ddlEmpType.SelectedIndex = 0;
            ddlEmpType.Items[0].Attributes.Add("disabled", "disabled");
            ddlEmpType.Items[0].Attributes.Add("class", "disabled-cursor");
            ddlEmpType.CssClass = "choices form-select";

           // txtEmpPassword.Attributes["type"] = "password";
            txtEmpPassword.Text = string.Empty;
           
            txtMailID.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtStatus.Text = string.Empty;
            txtRemark.Text = string.Empty;
            txtRelievingDate.Text = string.Empty; 

            btnSave.Text = "Save";

        }

        private void ddlEmpTypeBindGrid()
        {
            ddlEmpType.Items.Clear();
            ddlEmpType.Items.Insert(0, new ListItem("--- Select EmpType ---", ""));
            ddlEmpType.SelectedIndex = 0;
            ddlEmpType.Items[0].Attributes.Add("disabled", "disabled");
            ddlEmpType.Items[0].Attributes.Add("selected", "selected");

            // Set the data-hidden attribute to true for the "--Select--" option.
            ddlEmpType.Items[0].Attributes.Add("data-hidden", "true");
            ddlEmpType.Items[0].Attributes.Add("class", "disabled-cursor");

            // Set the CSS class for the DropDownList.
            ddlEmpType.CssClass = "choices form-select";

            ddlEmpType.Items.Insert(1, new ListItem("Developer", "1"));
            ddlEmpType.Items.Insert(2, new ListItem("Director Strategies", "2"));
            ddlEmpType.Items.Insert(3, new ListItem("Functional", "3"));
            ddlEmpType.Items.Insert(4, new ListItem("Managing Director", "4"));
            ddlEmpType.Items.Insert(5, new ListItem("Tester", "5"));

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetRecord();
            
            
        }

        public DataTable GetEmployeeDetails(string employeeID)
        {

            DataTable employeeTable = new DataTable();

            SqlConnection con = Connection.GetConnectionString();
            con.Open();

            SqlCommand getcmd = new SqlCommand("spGetEmployeeDetails", con);
            getcmd.CommandType = CommandType.StoredProcedure;
            getcmd.Parameters.AddWithValue("@EmpID", employeeID);

            SqlDataAdapter adapter = new SqlDataAdapter(getcmd);
            adapter.Fill(employeeTable);

            return employeeTable;
        }

    }
}




#region Command

//SqlDataReader reader = getcmd.ExecuteReader();

 //               if (reader.Read())
 //               {
 //                   // Employee ID exists, populate textboxes and change button name
 //                   txtEmpName.Text = reader["EmpName"].ToString();
 //                   txtDesignation.Text = reader["Designation"].ToString();
 //                   txtJoiningDate.Text = reader["JoiningDate"].ToString();
 //                   ddlEmpType.SelectedValue = reader["EmpType"].ToString();
 //                   txtEmpPassword.Text = reader["EmpPassword"].ToString();
 //                   txtMailID.Text = reader["MailID"].ToString();
 //                   txtMobileNo.Text = reader["MobileNo"].ToString();
 //                   txtAddress.Text = reader["Address"].ToString();
 //                   txtStatus.Text = reader["Status"].ToString();
 //                   txtRemark.Text = reader["Remark"].ToString();



 //                   btnSave.Text = "Update";
 //               }
 //               else
 //               {
 //                   // Employee ID does not exist, clear textboxes and reset button name
 //                   ResetRecord();
 //                   btnSave.Text = "Save";
 //               }
 //           }


 //           else
 //           {
 //               // Invalid Employee ID, handle accordingly
 //               // For example, display an error label
 //               //lblError.Text = "Invalid Employee ID.";

 //               // Clear TextBox controls
 //               // txtName.Text = string.Empty;
 //               //   txtAge.Text = string.Empty;
 //               // Clear other TextBox controls

 //               // Change button name to "Save"
 //               btnSave.Text = "Save";
 //           }

 //       }



//private void InsertOrUpdateEmployee(string createdBy, string modifyBy)
        //{
        //    string employeeID = txtEmpID.Text.Trim();
        //    string newStatus = txtStatus.Text.Trim();


         

        //    SqlConnection con = Connection.GetConnectionString();
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("InsertOrUpdateEmployee", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@EmpID", employeeID);
        //    cmd.Parameters.AddWithValue("@EmpName", txtEmpName.Text);
        //    cmd.Parameters.AddWithValue("@Designation", txtDesignation.Text);
        //    cmd.Parameters.AddWithValue("@JoiningDate", txtJoiningDate.Text);
        //    cmd.Parameters.AddWithValue("@EmpType", ddlEmpType.SelectedIndex.ToString());
        //    cmd.Parameters.AddWithValue("@EmpPassword", txtEmpPassword.Text);
        //    cmd.Parameters.AddWithValue("@MailID", txtMailID.Text);
        //    cmd.Parameters.AddWithValue("@MobileNo", txtMobileNo.Text);
        //    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
        //    cmd.Parameters.AddWithValue("@Status", newStatus);
        //    cmd.Parameters.AddWithValue("@Remark", txtRemark.Text);
        //    cmd.Parameters.AddWithValue("@Createdby", createdBy);


        //    cmd.Parameters.AddWithValue("@Modifyby", modifyBy);
           
        //    cmd.ExecuteNonQuery();



        //    ScriptManager.RegisterStartupScript(this, GetType(), "Record Saved Successfully", "$('.toast.success').toast('show');", true);
        //    con.Close();
        //}


        //private string GetEmployeeType(int employeeID)
        //{
        //    string employeeType = "";

        //    // Retrieve the employee type from the database based on the employee ID
        //   SqlConnection con = Connection.GetConnectionString();
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("SELECT EmpType FROM tblEmployeeMst WHERE EmpID = @EmpID", con);
               
        //            cmd.Parameters.AddWithValue("@EmpID", employeeID);
                 
        //            object result = cmd.ExecuteScalar();
        //            if (result != null)
        //            {
        //                employeeType = result.ToString();
        //            }
              

        //    return employeeType;
        //}

        //protected void txtEmpID_TextChanged(object sender, EventArgs e)
        //{
        //       string employeeID = txtEmpID.Text.Trim();

        //         // Retrieve employee details from the database
        //          Employee employee = GetEmployeeDetails(employeeID);

        //         if (employee != null)
        //         {
        //          txtEmpName.Text = employee.Name;
        //          ddlEmpType.SelectedValue = employee.Type;

        //            // Enable/disable status TextBox based on employee type
        //         if (employee.Type == "Director")
        //            {
        //            txtStatus.Enabled = true;
        //            }
        //         else
        //            {
        //            txtStatus.Enabled = false;
        //             }
        //          }
        //        else
        //            {
        //             // Handle case where employee ID is not found
        //             // Clear textboxes and disable the status TextBox
        //             ResetRecord();
        //             txtStatus.Enabled = false;
        //         }
        //    }


        //private Employee GetEmployeeDetails(string employeeID)
        //{
        //    // Implement the code to retrieve employee details based on the ID
        //    // Use the SqlConnection and SqlCommand classes to execute a SELECT query and populate an Employee object
        //    // Return the Employee object if found, or null if not found

        //    SqlConnection con = Connection.GetConnectionString();


        //    SqlCommand cmd = new SqlCommand("spSelectEmployeeDetails", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
                
        //    cmd.Parameters.AddWithValue("@EmpID", employeeID);
          

        //            con.Open();

        //           SqlDataReader reader = cmd.ExecuteReader();
                    
        //                if (reader.Read())
        //                {
        //                    // Create and populate the Employee object
        //                    Employee employee = new Employee();
        //                    employee.Name = reader["EmpName"].ToString();
        //                    employee.Type = reader["EmpType"].ToString();
                            
        //                    return employee;
        //                }
                    
                
            

        //    return null; // Employee not found
        //}
#endregion