<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="ReviewManager.aspx.cs" Inherits="portal_admin_ReviewManager" EnableEventValidation="false" %>

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
                <a href="#">Admin Settings</a>
            </li>
            <li class="breadcrumb-item active">Review Manager</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Review Manager</h4>
                                <br />
                                <div class="row mb-2">
                                    <label for="lableTitle" class="m-2"></label>
                                 &nbsp; &nbsp; 
                                <img src="../images/download_excel.png" />
                                    <asp:LinkButton ID="lnkbtnExportExcel" runat="server" OnClick="lnkbtnExportExcel_Click">Export 
                            Excel</asp:LinkButton>
                                    &nbsp; |&nbsp; &nbsp; 
                                <img src="../images/printer.png" alt="reset" />
                                    <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click">Print</asp:LinkButton>
                                    &nbsp; 
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Select Date"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control" placeholder="End Date"></asp:TextBox>
                                        <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </div>

                                    <div class="col-sm-2 ">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success m-0" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div> 
                                </div>
                                <div class="row">
                                    <asp:LinkButton ID="lnkActive" runat="server" class="btn btn-info" OnClick="lnkActive_Click">Show Review</asp:LinkButton>&nbsp;&nbsp; | &nbsp;
                                    <asp:LinkButton ID="lnkDisable" runat="server" class="btn btn-danger" OnClick="lnkDisable_Click"> Hide Review </asp:LinkButton>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" AllowSorting="True"
                                                DataKeyNames="id" Width="100%" border="0" CellPadding="0" CellSpacing="0"
                                                CssClass="table table-striped table-bordered" PageIndex="1" PageSize="50" OnRowDataBound="gvReport_RowDataBound" OnSorting="gvReport_Sorting">
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="2%">
                                                        <HeaderTemplate>
                                                            SR.No
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSRNO" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSel" runat="server" OnClick="javascript:SelectSingleCheckBox(this.id)" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>Select</HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkUpdate" runat="server"><asp:Image ImageUrl="~/portal/images/pencil.png" runat="server" /></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <HeaderTemplate>Edit</HeaderTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="id" HeaderText="id" SortExpression="id">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="comment" HeaderText="Comment" SortExpression="comment" ItemStyle-Width="45%">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CreatedOn" HeaderText="Created On" SortExpression="CreatedOn">
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



