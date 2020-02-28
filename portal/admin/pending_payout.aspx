<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="pending_payout.aspx.cs" Inherits="portal_admin_pending_payout" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Withdrawal</a>
            </li>
            <li class="breadcrumb-item active">Pending Requests</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Payout Requests</h4>
                                <br />
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnPayPayout" runat="server" OnClick="lnkbtnPayPayout_Click" class="btn btn-danger m-l-20"><i class="fa fa-refresh fa-spin"></i> Pay Payout</asp:LinkButton>


                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkRejectPayout" runat="server" OnClick="lnkRejectPayout_Click" class="btn btn-success m-l-20"><i class="fa fa-refresh fa-spin"></i> Reject Payout</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <hr />
                            <div class="card-body">
                                <div class="table-responsive">
                                    <div class="table-scrollable">
                                        <asp:Label runat="server" ID="lblError"></asp:Label>
                                        <asp:Panel ID="pnllead" runat="server">
                                            <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                                DataKeyNames="id" Width="100%" border="0" CellPadding="0" CellSpacing="0" CssClass="dom-jqry table table-striped table-bordered nowrap" PageIndex="1"
                                                PageSize="25" OnRowDataBound="gvMembers_RowDataBound" OnSorting="gvMembers_Sorting">
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
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSel" runat="server" OnClick="javascript:SelectSingleCheckBox(this.id)" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:BoundField DataField="Beneficiary_Name" HeaderText="Beneficiary Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>  

                                            <asp:BoundField DataField="Beneficiary_Bank_Account_Number" HeaderText="Beneficiary Bank Account Number" SortExpression="Beneficiary_Bank_Account_Number">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Bank_Name" HeaderText="Bank_Name" SortExpression="Bank_Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>

                                            <asp:BoundField DataField="Branch_Name" HeaderText="Bank Branch" SortExpression="Branch_Name">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>                                          

                                            <asp:BoundField DataField="IFSC_Code" HeaderText="IFSC Code" SortExpression="IFSC_Code">
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                                                                     
                                        </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                        <div>
                                            <div style="width: 973px;">
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
  
   
</asp:Content>

