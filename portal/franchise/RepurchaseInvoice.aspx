<%@ Page Title="" Language="C#" MasterPageFile="~/portal/franchise/franchise.master" AutoEventWireup="true" CodeFile="RepurchaseInvoice.aspx.cs" Inherits="portal_franchise_RepurchaseInvoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        @media print {
            .no-print, .no-print * {
                display: none !important;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- start page content -->
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme no-print" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="Dashoard.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Saled Product</a>
            </li>
            <li class="breadcrumb-item active">Product details</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <div class="col-md-1"></div>
                    <div class="col-md-10">
                        <div class="white-box">
                            <h3><b>REPURCHASE INVOICE</b> <span class="pull-right">#<asp:Label ID="lblOrderID" runat="server" Text="1"></asp:Label></span></h3>
                            <hr />
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="pull-left">
                                        <address>
                                            <%--   <img src="http://rsgsgrowrich.com/images/icons/logo-0.png" alt="logo" class="logo-default img-fluid w-25 img-responsive" />--%>
                                            <img src="http://sunraysproducts.com/images/logo/logo11.png" alt="SunRayProduct" class="logo-default img-fluid  img-responsive bg-dark" />
                                            <p class="text-dark m-l-5">
                                                <br />
                                                <%--Office No 107, 1st Floor,<br />
                                                Komal Chambers, Opp Choudhary High School,<br />
                                                Kasturba Road, Rajkot, Gujrat, India - 360001--%>
                                            </p>
                                        </address>
                                    </div>
                                    <div class="pull-right text-right">
                                        <address>
                                            <p class="addr-font-h3">To,</p>
                                            <p class="font-bold addr-font-h4">
                                                <asp:Label ID="lblUserName" runat="server" Text="username"></asp:Label>
                                            </p>
                                            <p class="text-dark m-l-30">
                                                <asp:Label ID="lblUserID" runat="server" Text="userId"></asp:Label>
                                            </p>
                                            <p class="m-t-30">
                                                <b>Invoice Date :</b> <i class="fa fa-calendar"></i>
                                                <asp:Label ID="lblDate" runat="server" Text="date"></asp:Label>
                                            </p>
                                        </address>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="table-responsive m-t-40">
                                        <div class="table table-hover table-scrollable">
                                            <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                            <asp:Panel ID="pnllead" runat="server">
                                                <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                                    DataKeyNames="userid" Width="100%" border="0" align="center" CellPadding="0" PageIndex="1" PageSize="50"
                                                    OnSorting="gvUsers_Sorting" OnRowDataBound="gvUsers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered" OnRowCommand="gvCampaign_RowCommand" style="transform:scale(.87)" >
                                                    <Columns>
                                                        <asp:BoundField DataField="userid" HeaderText="Sr No">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="FID" HeaderText="Franchise">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="username" HeaderText="User Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="product_name" HeaderText="Product Name">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="product_qty" HeaderText="Product Qty">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="price" HeaderText="Price">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="product_bv" HeaderText="Product BV">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="gst" HeaderText="GST (In Percent)">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                         <asp:BoundField DataField="repurchase_on" HeaderText="Date">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                    </Columns>
                                                </asp:GridView>
                                            </asp:Panel>
                                            <div>
                                                <div style="width: 973px;" hidden="hidden">
                                                    <div style="float: left;">
                                                        <div id="ctl00_ContentPlaceHolder1_datapaging_load">
                                                            <table>
                                                                <tbody>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Repeater ID="rptPager" runat="server">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                                                                        Enabled='<%# Eval("Enabled") %>' CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "btn_box" : "current_page" %>'
                                                                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:Repeater>
                                                                        </td>
                                                                    </tr>
                                                                </tbody>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="pull-right m-t-30 text-right">
                                        <p hidden="hidden">
                                            Total BV:
                                            <asp:Label ID="lblTotalAmt" runat="server" Text="Total BV" ></asp:Label>
                                           
                                        </p>
                                      
                                        <hr />
                                        <h3><b>Total :</b> <i class="fa fa-inr"></i>
                                            <asp:Label ID="lblTotalAmt1" runat="server" Text="Total Amount"></asp:Label></h3>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr />
                                     <div class="">
                                        <p class="text-center">ONE YEAR LIMITED WARRANTY</p>
                                        SunRaysProducts warrants its products to be free from defects in the workmanship or materials, under normal use and service, for a period of one year from the date of purchase from any authorized SunRaysProducts distributor.
This warranty does not cover any damage to the product due to lightning, over voltage, under voltage, accident, misuse, abuse, negligence or any damage caused by use of the product by the purchaser or others.
                                    </div>
                                    <div class="text-right">
                                        <%--        <button class="btn btn-danger" type="submit">Proceed to payment </button>--%>
                                        <button onclick="javascript:window.print();" class="btn btn-outline-info no-print" type="button"><span><i class="fa fa-print"></i>Print</span> </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-1"></div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

