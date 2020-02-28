<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="TicketManager.aspx.cs" Inherits="portal_admin_TicketManager" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Support</a>
            </li>
            <li class="breadcrumb-item active">Ticket Manager</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Ticket Manager</h4>
                                <br />
                                <div class="row mb-2" hidden="hidden">
                                    <label for="lableTitle" class="m-2"></label>
                                    <img src="../images/back_btn1.png" alt="generate report" />
                                    <asp:LinkButton ID="lnkbtnBack" runat="server" OnClick="lnkbtnBack_Click">Back 
                            to all reports</asp:LinkButton>
                                    &nbsp; |&nbsp; &nbsp; 
                                <img src="../images/progress_report.png" alt="generate report" />
                                    <asp:LinkButton ID="lnkbtnGenerate" runat="server" OnClick="lnkbtnGenerateReport_Click">Generate 
                            Report</asp:LinkButton>
                                    &nbsp; |&nbsp; &nbsp; 
                                <img src="../images/download_excel.png" />
                                    <asp:LinkButton ID="lnkbtnExportExcel" runat="server" OnClick="lnkbtnExportExcel_Click">Export 
                            Excel</asp:LinkButton>
                                    &nbsp; |&nbsp; &nbsp; 
                                <img src="../images/printer.png" alt="reset" />
                                    <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click">Print</asp:LinkButton>
                                    &nbsp; |&nbsp;  &nbsp;
                                <img src="../images/book_return.png" alt="reset" />
                                    <asp:LinkButton ID="lnkbtnRefresh" runat="server" OnClick="lnkbtnRefresh_Click">Refresh</asp:LinkButton>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                           <asp:DropDownList ID="ddlPriority" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">High</asp:ListItem>
                                    <asp:ListItem Value="2">Medium</asp:ListItem>
                                    <asp:ListItem Value="3">Low</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                    <div class="col-sm-2">
                                         <asp:TextBox ID="txtTicketID" runat="server" CssClass="form-control" placeholder="Ticket ID"></asp:TextBox>
                                    </div>
                                      <div class="col-sm-2">
                                   <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control " placeholder="User ID"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Select Date"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </div>
                                  
                                    <div class="col-sm-2 ">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success m-0" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div>

                                </div>
                            
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" AllowSorting="True"
                                                Width="100%" border="0" CellPadding="0" CellSpacing="0"
                                                CssClass="table table-striped table-bordered" PageIndex="1" PageSize="50" OnRowDataBound="gvReport_RowDataBound" OnSorting="gvReport_Sorting">
                                                <Columns>
                                                    <asp:BoundField DataField="ticket_id" HeaderText="Ticket ID" SortExpression="ticket_id">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="View " SortExpression="id">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="hlnktitle" runat="server" Text="View Ticket" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
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
                                                    <asp:BoundField DataField="TicketID" HeaderText="Ticket ID" SortExpression="TicketID">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="SUBJECT" HeaderText="Title" SortExpression="SUBJECT">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Priority" HeaderText="Priority" SortExpression="Priority">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TicketOn" HeaderText="Ticket On " SortExpression="TicketOn">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                </Columns>
                                            </asp:GridView>
                                    </asp:Panel>
                                    <div style="width: 953px;">
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
    <!-- end main -->
</asp:Content>


