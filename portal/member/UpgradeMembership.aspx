<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="UpgradeMembership.aspx.cs" Inherits="portal_member_UpgradeMembership" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtUserID.ClientID%>").value == "") {
                alert("Kindly Enter UserID !");
                document.getElementById("<%=txtUserID.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPackage.ClientID%>").value == "Select") {
                alert("Kindly Select Package!");
                document.getElementById("<%=ddlPackage.ClientID%>").focus();
                return false;
            } 
            if (document.getElementById("<%=txtEpin.ClientID%>").value == "") {
                alert("Kindly Enter Epin!");
                document.getElementById("<%=txtEpin.ClientID%>").focus();
                return false;
            }
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Account</a>
            </li>
            <li class="breadcrumb-item active">Upgrade Membership</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Upgrade </strong>
                        Membership
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">My User ID</label>
                                    <asp:TextBox runat="server" ID="txtUserID" Placeholder="Enter User ID" CssClass="form-control" Enabled="false" required="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">User Name</label>
                                    <asp:TextBox runat="server" ID="txtUserName" Text=" " CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Select Package</label>
                                    <asp:DropDownList ID="ddlPackage" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPackage_SelectedIndexChanged" AutoPostBack="true">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Amount</label>
                                    <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                 <div class="form-group col-sm-6">
                                    <label for="nf-password">Enter Epin</label>
                                    <asp:TextBox ID="txtEpin" runat="server" CssClass="form-control" OnTextChanged="txtEpin_TextChanged" AutoPostBack="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Epin Amount</label>
                                    <asp:TextBox runat="server" ID="txtEpinAmt" CssClass="form-control" placeholder="Amount" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnUpgrade" CssClass="btn btn-sm btn-primary btn-round" Text="Upgrade" OnClientClick="return validate();" OnClick="btnUpgrade_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

