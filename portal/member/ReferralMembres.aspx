<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="ReferralMembres.aspx.cs" Inherits="portal_member_ReferralMembres" %>

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
            <li class="breadcrumb-item active">Referral Members</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">                          
                             <div class="card-body">
                                    <h4 class="text-theme">Referral Members</h4>   
                                <br />       
                                 <div class="row">
                                      <div class="col-sm-2">
                                        <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="UserID"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="User Name"></asp:TextBox>
                                    </div>
                                      <div class="col-sm-2">
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-sm btn-primary btn-round" OnClick="btnSearch_Click" />
                                    </div>
                                 </div>
                             </div>
                            <div class="card-body">                                                     
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">                                       
                                        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                            DataKeyNames="my_sponsar_id" Width="100%" border="0" CellPadding="0" CellSpacing="0" CssClass="table table-striped table-bordered" PageIndex="1" PageSize="25" OnRowDataBound="gvMembers_RowDataBound" OnSorting="gvMembers_Sorting">
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
                                                <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
												  <asp:BoundField DataField="emailid" HeaderText="Email ID" SortExpression="emailid">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                  <asp:BoundField DataField="mobile_number" HeaderText="Mobile No." SortExpression="mobile_number">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="created_on" HeaderText="Join Date" SortExpression="created_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="active_on" HeaderText="Activation Date" SortExpression="active_on">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="total_direct_members" HeaderText="Total Downline Members" SortExpression="total_direct_members">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>                                              
                                                <asp:BoundField DataField="l_members" HeaderText="Left Downline" SortExpression="l_members">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="r_members" HeaderText="Right Downline" SortExpression="r_members">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="direct_left" HeaderText="Left Direct" SortExpression="direct_left">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="direct_right" HeaderText="Right Direct" SortExpression="direct_right">
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

