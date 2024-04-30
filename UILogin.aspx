<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UILogin.aspx.cs" Inherits="IssueTrackingSystem.UILogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Management System</title>
    <%--<link rel="shortcut icon" type="image/x-icon" href="assets/img/pspl_s.png"  />--%>
    <link rel="stylesheet" type="text/css" href="assets/compiled/css/app-dark.css" />
    <link rel="stylesheet" type="text/css" href="assets/compiled/css/app.css" />
    <style type="text/css">
        .back
        {
            width: 100%;
            position: absolute;
            top: 0;
            bottom: 0;
        }
        
        .div-center
        {
            width: 400px;
            height: 400px;
            background-color: #fff;
            position: absolute;
            left: 0;
            right: 0;
            top: 0;
            bottom: 0;
            margin: auto;
            max-width: 100%;
            max-height: 100%;
            overflow: auto;
            padding: 1em 2em;
            border-left: 2px #007bff solid;
            display: table;
        }
        
        div.content
        {
            display: table-cell;
            vertical-align: middle;
        }
        
        
        .custom-radio
        {
            width: 20px;
            height: 20px;
            border-radius: 50%;
            background-color: #007bff;
        }
        .custom-radio:checked
        {
            background-color: #17a2b8;
        }
        
        
        .form-check-label
        {
            font-weight: bold;
        }
        
        .mandatory-field::after {
        content: " *";
        color: red;
        }


    
    </style>


    <script type="text/javascript">
        function togglePasswordVisibility() {
            var passwordField = document.getElementById('<%= txtPassword.ClientID %>');
            var icon = document.querySelector('.password-toggle i');

            if (passwordField.type === 'password') {
                passwordField.type = 'text';
                icon.classList.remove('bi-eye');
                icon.classList.add('bi-eye-slash');
            } else {
                passwordField.type = 'password';
                icon.classList.remove('bi-eye-slash');
                icon.classList.add('bi-eye');
            }
        }
</script>





</head>
<body>
    <form id="form1" runat="server" class="needs-validation" novalidate="">
    <div class="logo">
        <%--<center>
            <img src="assets/img/pspl_logo4.png" alt="Logo" class="rounded-3 img-fluid  center-block bg-white mt-2 d-block" />
        </center>--%>
    </div>
    <div class="div-center">
        <div class="content">
            <h3 class="text-primary text-center">
                Login</h3>
            <hr style="color: #007bff" />
            <form>
            <div class="form-group">
                <label for="" class="mandatory-field">
                    User Name</label>
                <asp:TextBox ID="txtUsername" runat="server" class="form-control" required="" Text=""></asp:TextBox>
                <%-- <div class="invalid-feedback">
      Enter Username
    </div>--%>
                <asp:RequiredFieldValidator ID="valrUsername" runat="server" class="invalid-feedback"
                    ErrorMessage="Enter Username" ControlToValidate="txtUsername"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <label for="" class="mandatory-field">
                    Password</label>
                     <%--<div class="form-group position-relative has-icon-right">--%>
              <div class="input-group input-group-sm ">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" class="form-control"
                    required="" ></asp:TextBox>
                     <div class="input-group-text">
                    <button id="show_password" class="btn btn-primary" type="button">  
                   <span class="password-toggle" onclick="togglePasswordVisibility()">
                <i class="bi bi-eye"></i>
        </span>
    </button>
</div>

<asp:RequiredFieldValidator ID="valrPassword" runat="server" class="invalid-feedback"
                    ErrorMessage="Enter Password" ControlToValidate="txtPassword" Display="Static"></asp:RequiredFieldValidator>

</div>

                <%-- <div class="invalid-feedback">
      Enter Password
    </div>--%>
                
            </div>
            
            <div class="form-group row text-center">
                <div class="col-sm-10 ">
                    <div class="form-check form-check-inline">
                        <asp:RadioButton ID="rdoEmployee" runat="server" class="form-check-input custom-radio"
                            GroupName="Role" value="option1" Checked="True" />
                        <label class="form-check-label" for="option1">
                            Employee</label>
                    </div>
                    <div class="form-check form-check-inline">
                      <%--  <asp:RadioButton ID="rdoClient" runat="server" class="form-check-input custom-radio"
                            GroupName="Role" value="option2" />
                        <label class="form-check-label" for="option2">
                            Client</label>--%>
                    </div>
                </div>
            </div>
            <%--<div class="form-check">
           
               <asp:RadioButton ID="rdoEmployee" runat="server" class="form-check-input" GroupName="Role" />         
                      <label class="form-check-label" for="flexRadioDefault1">
                        Employee
                      </label>

                       <asp:RadioButton ID="rdoClient" runat="server" class="form-check-input" GroupName="Role" />         
                      <label class="form-check-label" for="flexRadioDefault1">
                        Client
                      </label>


                      </div>--%>
            <div class="form-group text-center p-2">
                <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-primary" OnClick="btnLogin_Click" />
                <hr style="color: #007bff" />
                <%--<button type="button" class="btn btn-link">Signup</button>
        <button type="button" class="btn btn-link">Reset Password</button>--%>
            </div>
            <%--<div id="alertPlaceholder"></div>--%>
            </form>
        </div>
    </div>
    <div class="container fixed-bottom position-absolute" style="width: 400px">
        <center>
            <asp:PlaceHolder runat="server" ID="alertPlaceholder"></asp:PlaceHolder>
        </center>
    </div>
    <script type="text/javascript">
        if (window.history.replaceState) {
            window.history.replaceState(null, null, window.location.href);
        }
    </script>
    <script type="text/javascript">
        // Example using JavaScript
        (function () {
            'use strict';

            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.querySelectorAll('.needs-validation');

            // Loop over them and prevent submission
            Array.prototype.slice.call(forms)
    .forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }

            form.classList.add('was-validated');
        }, false);
    });
        })();
    </script>
    </form>
</body>
</html>
