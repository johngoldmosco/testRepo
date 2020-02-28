<%@ Page Title="" Language="VB" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="false" CodeFile="reportKYCVerification.aspx.vb" Inherits="portal_admin_reportKYCVerification" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../css/page.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui%20.css" rel="stylesheet" type="text/css" />

    <script src="https://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>

    <script src="https://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Reports</a>
            </li>
            <li class="breadcrumb-item active">KYC Manager</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">KYC Manager</h4>
                                <br />
                                <div class="row">
                                    <div class="form-inline">
                                        <div class="">
                                            <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control form-control-round" placeholder="User ID"></asp:TextBox>
                                        </div>
                                        <div class="">
                                            <asp:Button ID="btnadd" runat="server" Text="Search" CssClass="btn btn-primary btn-round" OnClick="btnadd_Click" />
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="card-block">
                                    <div class="table-responsive dt-responsive">
                                        <asp:Panel ID="pnllead" runat="server">
                                            <asp:GridView ID="gvPINRequest" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                                DataKeyNames="userid" class="table table-striped table-bordered" PageIndex="1" PageSize="50">
                                                <Columns>
                                                    <asp:BoundField DataField="userid" HeaderText="ID">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:BoundField>

                                                    <asp:TemplateField HeaderText="Approve">
                                                        <ItemTemplate>
                                                            <img src="../images/checkmark2.png" alt="" />
                                                            <asp:LinkButton runat="server" ID="lnkapprove" CommandArgument='<%# Eval("userid") %>' CommandName="APPROVE">Approve</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reject">
                                                        <ItemTemplate>
                                                            <img src="../images/cross.png" alt="" />
                                                            <asp:LinkButton runat="server" ID="lnkreject" CommandArgument='<%# Eval("userid") %>' CommandName="REJECT">Reject</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="username" HeaderText="Name" SortExpression="username">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="my_sponsar_id" HeaderText="UserID">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="pan_card" HeaderText=" Pan Card" SortExpression="pan_card">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="aadhar_card" HeaderText="Aadhar Card" SortExpression="aadhar_card">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="photo" HeaderText="Photo" SortExpression="photo">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Pan Card">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkviewpancard" runat="server" Target="_blank">PAN</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Aadhar Card ">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkViewAadharCard" runat="server" Target="_blank"> adhar</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Photo">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkviewpho" runat="server" Target="_blank"> Photo</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cheque">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkviewchque" runat="server" Target="_blank">Cheque</asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="kyc_status" HeaderText="Kyc Status">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="upload_date" HeaderText="Upload On">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="cheque" HeaderText="Cheque" SortExpression="cheque">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:BoundField>

                                                </Columns>
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
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
                                                                                Enabled='<%# Eval("Enabled") %>' CssClass='<%# process(Eval("Enabled")) %>' OnClick="Page_Changed"
                                                                                OnClientClick='<%# process1(Eval("Enabled")) %>'></asp:LinkButton>
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

