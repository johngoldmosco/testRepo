<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="portal_member_Invoice" %>

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
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="Dashoard.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Activation</a>
            </li>
            <li class="breadcrumb-item active">Invoice</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
					<div class="col-md-1"></div>
                    <div class="col-md-10">
                        <div class="white-box">
                            <h3><b>INVOICE</b> <span class="pull-right">#<asp:Label ID="lblOrderID" runat="server" Text="1"></asp:Label></span></h3>
                            <hr />
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="pull-left">
                                        <address>
										<%-- 	<img src="../../images/logo/logo11.png" alt="SunRayProduct" style="width: 60%;"> --%>
                                            <img src="https://lifegoldecom.com/images/logo.png" alt="logo" class=" img-responsive" style="width: 20%;" />                                            
                                            <p class="text-dark m-l-5">
                                                <br />
                                                GOLD WINGS, SECOND FLOOR, COMERCIAL COMPLEX <br />
                                                SINHGAD ROAD OPP. ROHAN KIRTIKA BUILDING <br />
                                                ABOVE DARSHAN TYERS SINHGAD ROAD <br /> PUNE PIN - 411030 <br /> 
												<strong>GST NO :  </strong>
                                            </p>
                                        </address>
                                    </div>
                                    <div class="pull-right text-right">
                                        <address>
                                            <p class="addr-font-h3">To,</p>
                                            <p class="font-bold addr-font-h4">
                                                <asp:Label ID="lblUserName" runat="server" Text="1"></asp:Label>
                                            </p>
                                            <p class="text-dark m-l-30">
                                                <asp:Label ID="lblUserID" runat="server" Text="1"></asp:Label>
                                            </p>
                                            <p class="m-t-30">
                                                <b>Invoice Date :</b> <i class="fa fa-calendar"></i>
                                                <asp:Label ID="lblDate" runat="server" Text="1"></asp:Label>
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
                                                    DataKeyNames="id" Width="100%" border="0" align="center" CellPadding="0" PageIndex="1" PageSize="50"
                                                    OnSorting="gvUsers_Sorting" OnRowDataBound="gvUsers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered" OnRowCommand="gvCampaign_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="Sr No">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UserID" HeaderText="UserID">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="epin" HeaderText="Epin">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                          <asp:BoundField DataField="gst" HeaderText="GST(in Percent)">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="topupamount" HeaderText="Amount">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="pin_type" HeaderText="Product">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Topupby" HeaderText="TopUp by">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TopupOn" HeaderText="Activation Date">
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
                                         <p>
                                            Total GST: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                           <i class="fa fa-inr"></i> <asp:Label ID="lblGst" runat="server"></asp:Label>
                                        </p>
                                        <p>
                                            Total amount (Including GST): <i class="fa fa-inr"></i>
                                            <asp:Label ID="lblTotalAmt" runat="server"></asp:Label>
                                        </p>
                                        <%--<p>Discount : $10 </p>
                                    <p>Tax (10%) : $14 </p>--%>
                                        <hr />
                                        <h3><b>Total :</b> <i class="fa fa-inr"></i>
                                            <asp:Label ID="lblTotalAmt1" runat="server"></asp:Label></h3>
                                    </div>
                                    <div class="clearfix"></div>
                                    <hr />
                                    <div class="" hidden="hidden">
                                        <p class="text-center">ONE YEAR LIMITED WARRANTY</p>
                                        SunRaysProducts warrants its products to be free from defects in the workmanship or materials, under normal use and service, for a period of one year from the date of purchase from any authorized SunRaysProducts distributor.
This warranty does not cover any damage to the product due to lightning, over voltage, under voltage, accident, misuse, abuse, negligence or any damage caused by use of the product by the purchaser or others.
                                    </div>
                                    <div class="text-right no-print" >
                                        <%--        <button class="btn btn-danger" type="submit">Proceed to payment </button>--%>
                                        <button onclick="javascript:window.print();" class="btn btn-outline-info" type="button"><span><i class="fa fa-print"></i>Print</span> </button>
                                    </div>
                                    <hr />
									  <div class="">
                                        <p class="text-left">*Note : GSTIN :   And Total Amount subject to inclusive of GST </p>
                                        
                                    </div>
                                    <asp:Label ID="lbldateOf" runat="server"></asp:Label>
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

