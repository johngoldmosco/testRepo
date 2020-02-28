<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="Topup.aspx.cs" Inherits="portal_member_Topup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function validate() {
            if (document.getElementById('<%=ddlEpinType.ClientID %>').value == "") {
            alert("Please select epin type!");
            document.getElementById('<%=ddlEpinType.ClientID %>').focus();
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
            <li class="breadcrumb-item active">Top Up Member</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>TopUP </strong>
                        Member
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Member ID</label>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" required="true" AutoPostBack="true" OnTextChanged="txtUserID_TextChanged"></asp:TextBox>
                                <span class="help-block">Please enter Member ID</span>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User Name</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" required="true" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Epin Type</label>
                                <asp:DropDownList runat="server" ID="ddlEpinType" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Epin</label>
                                <asp:TextBox runat="server" ID="txtEpin" CssClass="form-control" required="true" OnTextChanged="txtEpin_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Top UP Member" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

