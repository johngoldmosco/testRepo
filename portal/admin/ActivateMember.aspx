<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="ActivateMember.aspx.cs" Inherits="portal_admin_ActivateMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
                        <strong>Top Up </strong>
                        Member
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Member ID</label>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" required="true" AutoPostBack="true" OnTextChanged="txtUserID_TextChanged"></asp:TextBox>
                                <span class="help-block">Please enter mamber ID</span>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User Name</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" required="true" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="simpleFormPassword" class=" form-group col-4">E-Pin Type: </label>
                                <asp:DropDownList ID="ddlEpinType" runat="server" CssClass="form-control form-group m-r-20" required="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                </asp:DropDownList>
                                <span class="help-block">Please select the type of Epin</span>
                                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlEpinType"
                                    InitialValue="0" ErrorMessage="Select Epin Type!" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Epin</label>
                                <asp:TextBox runat="server" ID="txtEpin" CssClass="form-control" required="true" Enabled="true" OnTextChanged="txtEpin_TextChanged" AutoPostBack="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="form-group col-sm-6">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Activate Member" OnClick="btnSubmit_Click" Enabled="false" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

