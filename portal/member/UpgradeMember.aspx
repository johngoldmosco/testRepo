﻿<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="UpgradeMember.aspx.cs" Inherits="portal_member_UpgradeMember" %>

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
     <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Reports</a>
            </li>
            <li class="breadcrumb-item active">Purchase Package History</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">

                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-header">
                                  <h6 class="text-theme"> Purchase Package History  </h6>                                  
                             </div>
                            <div class="card-body text-theme">                               
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <!-- <table id="datatable-buttons" class="table table-striped table-bordered">-->
                                        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                            Width="100%" border="0" align="center" CellPadding="0" DataKeyNames="id"
                                            OnSorting="gvMembers_Sorting" OnRowDataBound="gvMembers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered" PageIndex="1" PageSize="25">
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="2%">
                                                    <HeaderTemplate>
                                                        SR.No
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSRNO" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:BoundField DataField="amt" HeaderText="Invest Amount" SortExpression="amt">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="package_name" HeaderText="Package Name" SortExpression="package_name">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="invest_by" HeaderText="Purchase By" SortExpression="invest_by">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="investment_no" HeaderText="Invest No" SortExpression="investment_no">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="investment_mode" HeaderText="Payment Mode" SortExpression="investment_mode">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                  
                                                <asp:BoundField DataField="created_on" HeaderText="Invest On" SortExpression="created_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                               
                                            </Columns>
                                        </asp:GridView>
                                        <!--</table>-->
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
