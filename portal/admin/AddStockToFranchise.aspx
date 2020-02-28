<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="AddStockToFranchise.aspx.cs" Inherits="portal_admin_AddStockToFranchise" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Add Stock</a>
            </li>
            <li class="breadcrumb-item active">Add Stock To Franchise</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Add Stock </strong>
                        To Franchise
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                               <label for="nf-password">Select Franchise</label>
                                <asp:DropDownList runat="server" ID="ddlFranchiese" CssClass="form-control"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Select Product</label>
                                <asp:DropDownList runat="server" ID="ddlProducts" CssClass="form-control" OnSelectedIndexChanged="ddlProducts_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Current Stock</label>
                                <asp:TextBox runat="server" ID="txtCurStock" CssClass="form-control" placeholder="Current Stock"  ReadOnly="true"></asp:TextBox>                               
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Add Stock</label>
                                <asp:TextBox runat="server" ID="txtStock" CssClass="form-control" placeholder="Add Stock" TextMode="Number" required="true"></asp:TextBox>                               
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Add Stock" OnClick="btnSubmit_Click" OnClientClick="return validate();" />                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

