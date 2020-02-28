<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="AddPackage.aspx.cs" Inherits="portal_admin_AddPackage" %>

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
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Capping</label>
                                    <asp:textbox id="txtCapping" cssclass="form-control" runat="server" onkeypress="return isNumberKey(event);" placeholder="Capping" required="required"></asp:textbox>
                                    <span class="help-block">Please Enter Capping Value</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Name</label>
                                    <asp:textbox runat="server" id="txtProdName" cssclass="form-control"></asp:textbox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Cost</label>
                                    <asp:textbox id="txtProductCost" cssclass="form-control" runat="server" placeholder="Package Cost" onkeypress="return isNumberKey(event);" required="required"></asp:textbox>
                                    <span class="help-block">Please Enter Package Cost</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Description</label>
                                    <asp:textbox id="txtProdDesc" cssclass="form-control" runat="server" textmode="MultiLine" rows="5" placeholder="Description" required="required"></asp:textbox>
                                    <span class="help-block">Please enter Package description</span>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:button id="btnAddProduct" runat="server" cssclass="btn btn-flickr" text="Add Package" onclick="btnAddProduct_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

