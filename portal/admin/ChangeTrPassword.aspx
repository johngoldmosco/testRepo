<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="ChangeTrPassword.aspx.cs" Inherits="portal_admin_ChangeTrPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtOldPassword.ClientID%>").value == "") {
                alert("Kindly Enter Old Password!");
                document.getElementById("<%=txtOldPassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtNewPassword.ClientID%>").value == "") {
                alert("Kindly Enter New Password!");
                document.getElementById("<%=txtNewPassword.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtcPassword.ClientID%>").value == "") {
                alert("Kindly Enter Confirm Password!");
                document.getElementById("<%=txtcPassword.ClientID%>").focus();
                return false;
            }
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
            <li class="breadcrumb-item active">Change Transaction Password</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Change </strong>
                        Transaction Password
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">OLD Transaction Password</label>
                                <asp:TextBox runat="server" ID="txtOldPassword" CssClass="form-control" placeholder="Old Password.." TextMode="Password" required="true"></asp:TextBox>
                                <span class="help-block">Please enter your Transaction password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">New Transaction Password</label>
                                <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" placeholder="New Password.." TextMode="Password" required="true"></asp:TextBox>
                                <span class="help-block">Please enter your New Transaction password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Confirm New Password</label>
                                <asp:TextBox runat="server" ID="txtcPassword" CssClass="form-control" placeholder="New Confirm Password.." TextMode="Password" required="true"></asp:TextBox>
                                <span class="help-block">Please enter your New Confirm Transaction password</span>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate();"/>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

