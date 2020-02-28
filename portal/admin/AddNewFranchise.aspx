<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="AddNewFranchise.aspx.cs" Inherits="portal_admin_AddNewFranchise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtName.ClientID%>").value == "") {
                alert("Kindly Enter Name!");
                document.getElementById("<%=txtName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPassword.ClientID%>").value == "") {
                alert("Kindly Enter New Password!");
                document.getElementById("<%=txtPassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtTransPassword.ClientID%>").value == "") {
                alert("Kindly Enter Transaction Password!");
                document.getElementById("<%=txtTransPassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMobile.ClientID%>").value == "") {
                alert("Kindly Enter Mobile Number!");
                document.getElementById("<%=txtMobile.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /(\+\d{1,3}[- ]?)?\d{10}/;
                if (!reg.test(document.getElementById("<%=txtMobile.ClientID%>").value)) {
                    alert('Invalid Mobile Number');
                    document.getElementById("<%=txtMobile.ClientID%>").focus();
                    return false;
                }
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Kindly Enter User Email!");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (!reg.test(document.getElementById("<%=txtEmail.ClientID%>").value)) {
                    alert('Invalid Email Address');
                    document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
            }
            return true;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Account</a>
            </li>
            <li class="breadcrumb-item active">Add New Franchise</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Add </strong>
                        New Franchise
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Name</label>
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Name" required="true"></asp:TextBox>
                                <span class="help-block">Please enter Franchise Name</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Password</label>
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="Password.." TextMode="Password" required="true"></asp:TextBox>
                                <span class="help-block">Please enter your password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Transaction Password</label>
                                <asp:TextBox runat="server" ID="txtTransPassword" CssClass="form-control" placeholder="Transaction Password.." TextMode="Password" required="true"></asp:TextBox>
                                <span class="help-block">Please enter your Transaction password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Mobile</label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Mobile" TextMode="Phone" MaxLength="10" onkeypress="return isNumberKey(event);" required="true"></asp:TextBox>
                                <span class="help-block">Please enter Mobile</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Email</label>
                                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Email" TextMode="Email" required="true"></asp:TextBox>
                                <span class="help-block">Please enter Email</span>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnRegister" CssClass="btn btn-sm btn-primary btn-round" Text="Register" OnClientClick="return validate();" OnClick="btnRegister_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

