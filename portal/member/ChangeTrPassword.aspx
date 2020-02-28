<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="ChangeTrPassword.aspx.cs" Inherits="portal_member_ChangeTrPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                                    <asp:CompareValidator ID="comparePasswords" runat="server" ControlToCompare="txtNewPassword" ControlToValidate="txtcPassword"  ErrorMessage="Your passwords do not match up!" Display="Dynamic" ForeColor="Red" /> <br />
                                <span class="help-block">Please enter your New Confirm Transaction password</span>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" OnClick="btnSubmit_Click" OnClientClick="return validate();" Text="Submit"/>                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



