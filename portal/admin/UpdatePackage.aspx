<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="UpdatePackage.aspx.cs" Inherits="portal_admin_UpdatePackage" %>

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
            <li class="breadcrumb-item active">View/Edit Package</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>View/Edit</strong>
                        Package
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Name</label>
                                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server" placeholder="Package Name" required="true"></asp:TextBox>
                                    <span class="help-block">Please enter Package name</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Price</label>
                                    <asp:TextBox ID="txtPrice" CssClass=" form-control " runat="server" placeholder="0.00" onkeypress="return isNumberKey(event);" required="true"></asp:TextBox>
                                    <span class="help-block">Please enter Package price</span>
                                </div>
                            </div>
                            <div class="form-group  ">
                                <label for="nf-password">Package Description</label>
                                <asp:TextBox ID="txtProdDesc" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Description" required="true"></asp:TextBox>
                                <span class="help-block">Please enter Package Description</span>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Capping</label>
                                    <asp:TextBox ID="txtCapping" CssClass="form-control" runat="server" placeholder="Capping" required="true"></asp:TextBox>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Package Status</label>
                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                        <asp:ListItem Value="0">Select Status</asp:ListItem>
                                        <asp:ListItem Value="1">Enabled</asp:ListItem>
                                        <asp:ListItem Value="2">Disabled</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnUpdateProduct" runat="server" OnClick="btnUpdateProduct_Click" CssClass="btn btn-dribbble" Text="Update Package" />
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

