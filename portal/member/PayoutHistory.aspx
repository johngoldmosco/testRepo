<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="PayoutHistory.aspx.cs" Inherits="portal_member_PayoutHistory" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Withdrawal</a>
            </li>
            <li class="breadcrumb-item active">Payout History</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body" >
                                <h4 class="text-theme">Payout History</h4>
                                <br />
                                <div class="row" hidden="hidden">                                   
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtUserID" runat="server" class="form-control" placeholder="Enter User ID"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success btn-skew m-b-6" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                            DataKeyNames="my_sponsar_id" Width="100%" border="0" align="center" CellPadding="0" PageIndex="1" PageSize="50"
                                            OnSorting="gvUsers_Sorting" OnRowDataBound="gvUsers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="2%">
                                                    <HeaderTemplate>
                                                        SR.No
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="my_sponsar_id" HeaderText="Member ID" SortExpression="my_sponsar_id">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="wallet1" HeaderText="Wallet Balance" SortExpression="wallet1">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="tds" HeaderText="Tds" SortExpression="tds">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="service_charge" HeaderText="Service Charge" SortExpression="service_charge">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="amount_given" HeaderText="Net Amount Given" SortExpression="amount_given">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="request_date" HeaderText="Request Date" SortExpression="request_date">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="confirmed_on" HeaderText="Confirmed On" SortExpression="confirmed_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="reject_on" HeaderText="Reject On" SortExpression="reject_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                            
                                              
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                    <div style="width: 953px;">
                                        <div style="float: right;">
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

