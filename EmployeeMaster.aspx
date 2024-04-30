<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="EmployeeMaster.aspx.cs" Inherits="IssueTrackingSystem.EmployeeMaster" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<script type="text/javascript">

        function validatePassword() {
            var passwordTextbox = document.getElementById('<%= txtEmpPassword.ClientID %>');
            var validationMessage = document.getElementById('<%= regexPassword.ClientID %>');

            var regexPassword = /^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,15}$/;

            var isValid = regexPassword.test(passwordTextbox.value);
            if (isValid) {

                validationMessage.style.display = "none";

                passwordTextbox.classList.remove("alert-light-danger");
                passwordTextbox.classList.remove("color-danger");
            } else {


                validationMessage.style.display = "block";
                //  validationMessage.style.display = "flex";
                //  validationMessage.style.justifyContent = "center";
                //  validationMessage.style.alignItems = "center";
                //  validationMessage.classList.add("alert-light-danger", "color-danger", "text-center");
                passwordTextbox.classList.add("alert-light-danger");
                passwordTextbox.classList.add("color-danger");
            }
        }



        function togglePasswordVisibility() {

         //   var loginID = '<%= Session["EmpID"] %>';
            var passwordField = document.getElementById('<%= txtEmpPassword.ClientID %>');
            var icon = document.querySelector('.password-toggle i');


//            var empIDInput = document.getElementById('<%= txtEmpID.ClientID %>');
//            var empID = empIDInput.value; // Retrieve the value of the input field


            var employeeType = '<%= Session["EmpType"] %>'; // Get the employeeType value from the server

            if ((
             employeeType === "Admin" ||
             employeeType === "Director Strategies" ||
            employeeType === "Managing Director"
            )) 
            {
                if (passwordField.type === 'password') {
                    passwordField.type = 'text';
                    icon.classList.remove('bi-eye');
                    icon.classList.add('bi-eye-slash');
                }
                else {
                    passwordField.type = 'password';
                    icon.classList.remove('bi-eye-slash');
                    icon.classList.add('bi-eye');
                }

                document.getElementById('btnshowpassword').disabled = false;

            }

            else {


                document.getElementById('btnshowpassword').disabled = true;
                passwordField.type = 'password';
                icon.classList.remove('bi-eye-slash');
                icon.classList.add('bi-eye');

            }


        }


</script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Basic Horizontal form layout section start -->
    <asp:UpdatePanel ID="updatepnlEmployee" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnlEmployeeDetails" runat="server">
                <section id="basic-horizontal-layouts">
          <div class = "container">
            <div class="row match-height">
              <div class=" col-12">
                <div class="card">
                  <div class="card-header">
                    <h4 class="card-title">Employee Details</h4>
                  </div>

                  <div class="card-content">
                    <div class="card-body">
                      <form class="form form-horizontal">
                        <div class="form-body">

                          <div class="row">
                            <div class="col-md-2">

                            <asp:Label ID="lblEmpID" runat="server" Text="Emp ID" class="mandatory-field" for="first-name-horizontal"></asp:Label>
                              <%--<label for="first-name-horizontal"  class="mandatory-field">
                                Emp ID</label>--%>

                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtEmpID" runat="server" class="form-control" 
                                    ontextchanged="txtEmpID_TextChanged" AutoPostBack="true" required="" >
                                  </asp:TextBox>
                                    <small class="text-gray-600 badge bg-light-danger" style="font-size: small;">[ eg: PSPL1 ]<i></i></small>
                                  <asp:RequiredFieldValidator ID="valrEmpID" runat="server" class="invalid-feedback"
                                   ErrorMessage="Enter EmpID" ControlToValidate="txtEmpID"></asp:RequiredFieldValidator>
                            </div>
                                                       
                            <div class="col-md-2">
                              <label for="email-horizontal" class="mandatory-field">Emp Name</label>
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtEmpName" runat="server" class="form-control" required=""></asp:TextBox>

                             <asp:RequiredFieldValidator ID="valrEmpName" runat="server" class="invalid-feedback"
                                   ErrorMessage="Enter EmpName" ControlToValidate="txtEmpName"></asp:RequiredFieldValidator>

                            </div>


                             </div>
                            

                             <div class="row">

                            <div class="col-md-2">
                              <label for="contact-info-horizontal" class="mandatory-field"
                                >Designation</label>
                              
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtDesignation" runat="server"  class="form-control"  required=""></asp:TextBox>

                             <asp:RequiredFieldValidator ID="valrDesignation" runat="server" class="invalid-feedback"
                                   ErrorMessage="Enter Designation" ControlToValidate="txtDesignation"></asp:RequiredFieldValidator>
                             
                            </div>

                            <div class="col-md-2">
                              <label for="password-horizontal" class="mandatory-field">Joining Date</label>
                            </div>

                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtJoiningDate" runat="server" class="form-control" TextMode="Date" required=""></asp:TextBox>

                            <asp:RequiredFieldValidator ID="valrJoiningDate" runat="server" class="invalid-feedback"
                                   ErrorMessage="Enter JoiningDate" ControlToValidate="txtJoiningDate"></asp:RequiredFieldValidator>
                             
                            </div>

                            </div>
                           

                         
                         <div class="row">

                            <div class="col-md-2">
                              <label for="contact-info-horizontal" class="mandatory-field"
                                >Emp Type</label>
                              
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:DropDownList ID="ddlEmpType" runat="server" class="choices form-select" AppendDataBoundItems="True" required="">
     
                                </asp:DropDownList>
                         
                             <asp:RequiredFieldValidator ID="valrEmpType" runat="server" class="invalid-feedback" 
                                    ControlToValidate="ddlEmpType"></asp:RequiredFieldValidator>

                            </div>
                            

                            <div class="col-md-2">
                              <label for="password-horizontal" class="mandatory-field" >Emp Password</label>
                            </div>

                            <div class="col-md-4 form-group">
                            <div class="input-group input-group-sm">
                            <asp:TextBox ID="txtEmpPassword" runat="server" class="form-control" required="" onkeyup="validatePassword();"
       ></asp:TextBox>

         <div class="input-group-text">
                    <button id="btnshowpassword" class="btn btn-primary" type="button" >  
                    <span class="password-toggle" onclick="togglePasswordVisibility()" >
                 <i class="bi bi-eye"></i>
                </span>
</button>
</div>

<asp:RegularExpressionValidator ID="regexPassword" runat="server" ControlToValidate="txtEmpPassword" 
    ValidationExpression="^(?=.*[a-zA-Z])(?=.*\d)(?=.*[@#$%^&+=!]).{8,15}$"
    ErrorMessage="Password must be at least 8 characters long upto 15 and contain at least one uppercase, one lowercase, one digit, and one special character."
    class = "alert-light-danger color-danger text-center" Display="Dynamic" style="display:none;"></asp:RegularExpressionValidator>

                                       
<asp:RequiredFieldValidator ID="valrEmpPassword" runat="server" class="invalid-feedback" Display="Static"
  ErrorMessage="Enter EmpPassword" ControlToValidate="txtEmpPassword"></asp:RequiredFieldValidator>
</div>



                            </div>

                           </div> 
                            <div class="row">

                            <div class="col-md-2">
                              <label for="contact-info-horizontal" class="mandatory-field">
                                Mail ID</label>
                           
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtMailID" runat="server"  class="form-control" TextMode="Email" required=""></asp:TextBox>
                            <small class="text-gray-600 badge bg-light-danger" style="font-size: small;">[ eg: empl@psplsoft.com ]<i></i></small>
                             <asp:RequiredFieldValidator ID="valrMailID" runat="server" class="invalid-feedback" 
                                   ErrorMessage="Enter MailID" ControlToValidate="txtEmpPassword"></asp:RequiredFieldValidator>
                             
                            </div>

                            <div class="col-md-2">
                              <label for="password-horizontal" class="mandatory-field">Mobile No.</label>
                            </div>

                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtMobileNo" runat="server" class="form-control" required=""></asp:TextBox>

                              <asp:RequiredFieldValidator ID="valrMobileNo" runat="server" class="invalid-feedback" 
                                   ErrorMessage="Enter MobileNo" ControlToValidate="txtMobileNo"></asp:RequiredFieldValidator>
                             
                            </div>

                            </div>



                            <div class="row">

                            <div class="col-md-2">
                              <label for="contact-info-horizontal" class="mandatory-field"
                                >Address</label
                              >
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtAddress" runat="server"  class="form-control" TextMode="MultiLine" required=""></asp:TextBox>

                             <asp:RequiredFieldValidator ID="valrAddress" runat="server" class="invalid-feedback" 
                                   ErrorMessage="Enter Address" ControlToValidate="txtAddress"></asp:RequiredFieldValidator>
                             
                            </div>

                             <div class="col-md-2">
                              <label for="password-horizontal" >Status</label>
                            </div>

                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtStatus" runat="server" class="form-control"></asp:TextBox>

                           
                            </div>

                            </div>
                                                      
                            <div class="row">

                            <div class="col-md-2">
                              <label for="contact-info-horizontal" >
                                Remark</label>
                              
                            </div>
                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtRemark" runat="server"  class="form-control" TextMode="MultiLine"></asp:TextBox>

                  
                             
                            </div>

                             <div class="col-md-2">
                              <label for="password-horizontal" >Relieving Date</label>
                            </div>

                            <div class="col-md-4 form-group">

                            <asp:TextBox ID="txtRelievingDate" runat="server" class="form-control" TextMode="Date" ></asp:TextBox>

                    
                             
                            </div>

                            </div>

                             </div>
                        </div>
                      </form>
                    </div>


                     <div class="card-footer">
                          
                                <div class="col-sm-12 d-flex justify-content-end">
                            <asp:Button ID="btnSave" runat="server" Text="Save"  
                                    class="btn btn-primary me-1 mb-1" onclick="btnSave_Click"  ></asp:Button>

                             <asp:Button ID="btnCancel" runat="server" Text="Cancel" 
                                    class="btn btn-light-danger me-1 mb-1" onclick="btnCancel_Click"></asp:Button>
                             
                            </div>
                         
                  </div>
                </div>
       </div>
       </div>
       </div>
              </section>
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnCancel" />
            <asp:PostBackTrigger ControlID="ddlEmpType" />
            <asp:PostBackTrigger ControlID="txtEmpID" />
            <asp:AsyncPostBackTrigger ControlID="txtEmpPassword" EventName="TextChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
