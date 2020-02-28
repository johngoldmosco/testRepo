<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="UpdateProductOnPage.aspx.cs" Inherits="portal_admin_UpdateProductOnPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

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
                <a href="#">Product Manager(On Site)</a>
            </li>
            <li class="breadcrumb-item active">Update Product </li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Update</strong>
                        Product
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Name</label>
                                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server" placeholder="Product Name" required="required"></asp:TextBox>
                                    <span class="help-block">Please Enter Product Name</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Cost</label>
                                    <asp:TextBox ID="txtProductCost" CssClass="form-control" runat="server" placeholder="Product Cost" onkeypress="return isNumberKey(event);" required="required"></asp:TextBox>
                                    <span class="help-block">Please Enter Product Cost</span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Description</label>
                                    <asp:TextBox ID="txtProdDesc" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="5" placeholder="Description" required="required"></asp:TextBox>
                                    <span class="help-block">Please enter product description</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product GST(In Percent)</label>
                                    <asp:TextBox runat="server" ID="txtGst" CssClass="form-control"></asp:TextBox>
                                    <span class="help-block">Please Enter Product GST in Percent</span>
                                </div>
                                 <div class="form-group col-sm-6">
                                     <asp:Label runat="server" ID="lblEpinID" hidden="hidden" ></asp:Label>
                                     </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Image</label>
                                    <asp:FileUpload runat="server" ID="flupProdImg" CssClass="form-control" />
                                    <span class="help-block">Please Upload product Image</span>
                                </div>
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Product Status</label>
                                    <asp:DropDownList runat="server" ID="ddlStatus" CssClass="form-control">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Enabled</asp:ListItem>
                                        <asp:ListItem Value="2">Disabled</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="help-block">Please Select product show/hide status</span>
                                </div>
                            </div>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnUpdateProduct" runat="server" CssClass="btn btn-flickr" Text="Update Product" OnClick="btnUpdateProduct_Click" />
                        </div>
                        <br />
                        <div class="row">
                            <asp:Image ID="imgProd" runat="server" CssClass=" w-25 h-25" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

