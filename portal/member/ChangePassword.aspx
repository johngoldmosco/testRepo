<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="portal_member_ChangePassword" %>

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
            <li class="breadcrumb-item active">Change Password</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Change </strong>
                        Password
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Password</label>
                                <asp:TextBox runat="server" ID="txtOldPassword" CssClass="form-control" placeholder="Old Password.." TextMode="Password"></asp:TextBox>
                                <span class="help-block">Please enter your password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">New Password</label>
                                <asp:TextBox runat="server" ID="txtNewPassword" CssClass="form-control" placeholder="New Password.." TextMode="Password"></asp:TextBox>
                                <span class="help-block">Please enter your New password</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Confirm New Password</label>
                                <asp:TextBox runat="server" ID="txtcPassword" CssClass="form-control" placeholder="New Confirm Password.." TextMode="Password"></asp:TextBox>
                                <span class="help-block">Please enter your New Confirm password</span><br />
                                <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtcPassword"  ErrorMessage="Your passwords do not match up!" Display="Dynamic" ForeColor="Red" />
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



