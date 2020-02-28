<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="PendingWithdrawal.aspx.cs" Inherits="portal_admin_PendingWithdrawal" EnableEventValidation="false" %>

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
    <script type="text/javascript">

        function SelectSingleCheckBox(chkid) {
            var chk = document.getElementById(chkid);
            var chkList = document.getElementsByTagName("input");
            for (i = 0; i < chkList.length; i++) {
                if (chkList[i].type == "checkbox" && chkList[i].id != chk.id) {
                    chkList[i].checked = false;
                }
            }
        }
    </script>
	<script type="text/javascript">
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {
                //If checked change color to Aqua
                row.style.backgroundColor = "aqua";
            }
            else {
                //If not checked change back to original color
                if (row.rowIndex % 2 == 0) {
                    //Alternating Row Color
                    row.style.backgroundColor = "#C2D69B";
                }
                else {
                    row.style.backgroundColor = "white";
                }
            }

            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
    </script>
    <script type="text/javascript">
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows

                        row.style.backgroundColor = "aqua";
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        //and change rowcolor back to original
                        if (row.rowIndex % 2 == 0) {
                            //Alternating Row Color
                            row.style.backgroundColor = "#C2D69B";
                        }
                        else {
                            row.style.backgroundColor = "white";
                        }
                        inputList[i].checked = false;
                    }
                }
            }
        }
    </script>
    <script type = "text/javascript">
        function MouseEvents(objRef, evt) {
            var checkbox = objRef.getElementsByTagName("input")[0];
            if (evt.type == "mouseover") {
                objRef.style.backgroundColor = "orange";
            }
            else {
                if (checkbox.checked) {
                    objRef.style.backgroundColor = "aqua";
                }
                else if (evt.type == "mouseout") {
                    if (objRef.rowIndex % 2 == 0) {
                        //Alternating Row Color
                        objRef.style.backgroundColor = "#C2D69B";
                    }
                    else {
                        objRef.style.backgroundColor = "white";
                    }
                }
            }
        }
</script>
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
                <a href="#">Withdrawal</a>
            </li>
            <li class="breadcrumb-item active">Pending Withdrawals</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Pending Withdrawals</h4>
                                <br />
                                <div class="row mb-2">
                                    <label for="lableTitle" class="m-2"></label>
                                    
                                <img src="../images/download_excel.png" />
                                    <asp:LinkButton ID="lnkbtnExportExcel" runat="server" OnClick="lnkbtnExportExcel_Click">Export 
                            Excel</asp:LinkButton>
                                    &nbsp; |&nbsp; &nbsp; 
                                <img src="../images/printer.png" alt="reset" />
                                    <asp:LinkButton ID="lnkPrint" runat="server" OnClick="lnkPrint_Click">Print</asp:LinkButton>
                                    
                                </div>
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="User ID"></asp:TextBox>
                                    </div>
                                     <div class="col-sm-2">
                                         <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control" placeholder="Start Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtStartDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </div>
                                     <div class="col-sm-2">
                                        <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control " placeholder="End Date"></asp:TextBox>
                            <cc1:CalendarExtender ID="Calendar2" PopupButtonID="imgPopup" runat="server" TargetControlID="txtEndDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-sm-2 ">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success m-0" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div>
                                  
                                </div>
                               <div class="row">

                                   <asp:LinkButton ID="lnkbtnPayPayout" runat="server" OnClick="lnkbtnPayPayout_Click" class="btn btn-primary btn-custom waves-effect waves-light m-b-6"><i class="fa fa-refresh fa fa-spin"></i> &nbsp;Pay Payout</asp:LinkButton>

                            <asp:LinkButton ID="lnkRejectPayout" runat="server" OnClick="lnkRejectPayout_Click" class="btn btn-danger btn-custom waves-effect waves-light m-b-6"><i class="fa fa-refresh fa fa-spin"></i>&nbsp; Reject Payout</asp:LinkButton>
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

                                                    <asp:BoundField DataField="id" HeaderText="ID">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
													<HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSel" runat="server"  />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField> 
													<asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField> 
                                                    <asp:BoundField DataField="wallet1" HeaderText="Other Incomes" SortExpression="wallet1">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="wallet2" HeaderText="Profit Share" SortExpression="wallet2" ItemStyle-ForeColor="Red">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>													
                                                    <asp:BoundField DataField="request_amount" HeaderText="Request Amount" SortExpression="request_amount">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
													<asp:BoundField DataField="tds" HeaderText="TDS" SortExpression="tds">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
													<asp:BoundField DataField="service_charge" HeaderText="Admin Charge" SortExpression="service_charge">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="amount_given" HeaderText="Amount Given" SortExpression="amount_given">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="bank_name" HeaderText="Bank Name" SortExpression="bank_name">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="account_number" HeaderText="Account No." SortExpression="account_number">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ifsc_code" HeaderText="IFSC" SortExpression="ifsc_code">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
													 <asp:BoundField DataField="branch_name" HeaderText="Branch Name" SortExpression="branch_name">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="RequestDate" HeaderText="Request Date" SortExpression="RequestDate">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>
                                                    
                                                    <asp:BoundField DataField="Wstatus" HeaderText="Status" SortExpression="Wstatus">
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
