<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="AddProduct.aspx.cs" Inherits="portal_admin_AddProduct" %>
<%--  THis is nothing but the add package only for this project  --%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script lang="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) && k != 32);
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
                <a href="#">Package Manager</a>
            </li>
            <li class="breadcrumb-item active">Add Package</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Add</strong>
                        Package
                    </div>
                    <div class="card-body text-theme">
                        <div>
							 <div class="row">
                                <div class="form-group">
                                    <label for="nf-password">Select Category</label>
                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Select Category</asp:ListItem>
                                        <asp:ListItem Value="1">Joining Package</asp:ListItem>
                                        <asp:ListItem Value="2">Repurchase Package</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Capping</label>
                                    <asp:TextBox ID="txtCapping" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event);" placeholder="Capping" required="required"></asp:TextBox>
                                    <span class="help-block">Please Enter Capping Value</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Name</label>
                                  <asp:TextBox runat="server" ID="txtProdName" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Cost</label>
                                    <asp:TextBox ID="txtProductCost" CssClass="form-control" runat="server" placeholder="Product Cost" onkeypress="return isNumberKey(event);"  required="required"></asp:TextBox>
                                    <span class="help-block">Please Enter Package Cost</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product BV</label>
                                    <asp:TextBox runat="server" ID="txtBV" CssClass="form-control" required="true" onkeypress="return isNumberKey(event);" ></asp:TextBox>
                                </div>
                            </div>

                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Description</label>
                                    <asp:TextBox ID="txtProdDesc" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Description" required="required"></asp:TextBox>
                                    <span class="help-block">Please enter Package description</span>
                                </div>
                                 <div class="form-group col-sm-6">
                                    <label for="nf-password">Cashback Amount</label>
                                    <asp:TextBox ID="txtCashback" CssClass="form-control" runat="server" onkeypress="return isNumberKey(event);"  required="required"></asp:TextBox>
                                    <span class="help-block">Please enter Package description</span>
                                </div>
                            </div>                          
                       
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-flickr" Text="Add Package" OnClick="btnAddProduct_Click" />
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

