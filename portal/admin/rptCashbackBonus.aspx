<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="rptCashbackBonus.aspx.cs" Inherits="portal_admin_rptRoyaltyBonus" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/page.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui%20.css" rel="stylesheet" type="text/css" />

    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function printing() {
            window.print();
        }
    </script>

    <style type="text/css">
        @media print {
            .noPrint {
                display: none;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Reports</a>
            </li>
            <li class="breadcrumb-item active">Cashback Summary</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <div class="row">

                    <div class="form-inline">
                        <label for="lableTitle" class="m-2"></label>
                        <div class="">
                            <img src="../images/back_btn1.png" alt="generate report" />
                            <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click">Back 
                            to all reports</asp:LinkButton>
                            &nbsp; |&nbsp; &nbsp; 
                        </div>
                        <div class="">

                            <img src="../images/progress_report.png" alt="generate report" />
                            <asp:LinkButton ID="lnkbtnGenerate" runat="server" OnClick="lnkbtnGenerate_Click">Generate 
                            Report</asp:LinkButton>
                            &nbsp; |&nbsp; &nbsp; 
                        </div>
                        <div class="">
                            <img src="../images/download_excel.png" />
                            <asp:LinkButton ID="lnkbtnExportExcel" runat="server" OnClick="lnkbtnExportExcel_Click">Export 
                            Excel</asp:LinkButton>
                            &nbsp; |&nbsp; &nbsp;  
                        </div>
                        <div class="">
                            <img src="../images/printer.png" alt="reset" />
                            <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click">Print</asp:LinkButton>
                            &nbsp; |&nbsp;  &nbsp; 
                        </div>
                        <div class="">
                            <img src="../images/book_return.png" alt="reset" />
                            <asp:LinkButton ID="lnkbtnRefresh" runat="server" OnClick="lnkbtnRefresh_Click">Refresh</asp:LinkButton>
                        </div>
                    </div>
                    <div class="form-inline">
                        <label for="lableTitle" class="m-2"></label>
                         <div class="">
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control " placeholder="User ID"></asp:TextBox>
                        </div>
                        <div class="">
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                        </div>
                        <div class="">
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Start Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                        </div>
                        <div class="">
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="End Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                        </div>
                        <div class="">
                            <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-search"></i>Search</asp:LinkButton>
                        </div>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-header">
                                <h6 class="text-theme">Cashback Bonus Details</h6>
                            </div>
                            <div class="card-body text-theme">
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvBinaryIncome" runat="server" AutoGenerateColumns="false" AllowSorting="True"
                                            DataKeyNames="userid" Width="100%" border="0" CellPadding="0" CellSpacing="0"
                                            CssClass="table table-striped table-bordered" PageIndex="1" PageSize="50" OnRowDataBound="gvBinaryIncome_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="2%">
                                                    <HeaderTemplate>
                                                        SR.No
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="amount" HeaderText="Cashback" SortExpression="amount">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="trans_month" HeaderText="Transaction Month" SortExpression="trans_month">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                           
                                                <asp:BoundField DataField="Received_on" HeaderText="Received on" SortExpression="Received_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="status" HeaderText="Transaction Status" SortExpression="status">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
                                            </Columns>
                                        </asp:GridView>
                                        <!--</table>-->
                                    </asp:Panel>
                                    <div style="width: 973px;">
                                        <div style="float: Left;">
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
                                    <br />
                                </div>
                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card -->
                    </div>
                    <!-- end col -->

                </div>
                <!-- end row -->

            </div>
            <!-- end animated fadeIn -->
        </div>
        <!-- end container-fluid -->
    </div>
</asp:Content>

