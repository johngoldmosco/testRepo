<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="add_shopee.aspx.cs" Inherits="portal_admin_add_shopee" %>

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
                <a href="#">Shopee</a>
            </li>
            <li class="breadcrumb-item active">Add New Shopee</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Shopee </strong>                        
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Shopee Name</label>
                                <asp:TextBox runat="server" ID="txtName" CssClass="form-control" placeholder="Name of the shopee.."  required="true"></asp:TextBox>
                                
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Address</label>
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" placeholder="Shopee Address.." required="true"></asp:TextBox>                                
                            </div>  
                            
                             <div class="form-group">
                                 <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClick="btnSubmit_Click"  />                               
                            </div>                           
                        </div>
                    </div>                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>

