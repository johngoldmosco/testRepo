<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="CreditDebitSummary.aspx.cs" Inherits="portal_admin_CreditDebitSummary" %>

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

    <%--<script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtReferralID.ClientID%>").value == "") {
                alert("Kindly enter the Sponsor ID");
                document.getElementById("<%=txtReferralID.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPosition.ClientID%>").value == "0") {
                alert("Kindly Select Position!");
                document.getElementById("<%=ddlPosition.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtUserName.ClientID%>").value == "") {
                alert("Kindly enter the name");
                document.getElementById("<%=txtUserName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Kindly Enter User Email!");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (!reg.test(document.getElementById("<%=txtEmail.ClientID%>").value)) {
                    alert('Invalid Email Address');
                    document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtMobileNo.ClientID%>").value == "") {
                alert("Kindly Enter Mobile Number!");
                document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /(\+\d{1,3}[- ]?)?\d{10}/;
                if (!reg.test(document.getElementById("<%=txtMobileNo.ClientID%>").value)) {
                    alert('Invalid Mobile Number');
                    document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtPAN.ClientID%>").value == "") {
                alert("Kindly Enter PAN!");
                document.getElementById("<%=txtPAN.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPAN.ClientID%>").value != "") {
                var reg = /[A-Za-z]{5}\d{4}[A-Za-z]{1}/;
                if (!reg.test(document.getElementById("<%=txtPAN.ClientID%>").value)) {
                    alert('Invalid PAN Number');
                    document.getElementById("<%=txtPAN.ClientID%>").focus();
                    return false;
                }
            }

            return true;
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        } 
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">E-Wallet</a>
            </li>
            <li class="breadcrumb-item active">Credit Debit Summary</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Credit Debit Summary</h4>
                                <br />
                                <div class="row">
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Enter User Name"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:TextBox ID="txtUserID" runat="server" class="form-control" placeholder="Enter User ID"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success btn-skew m-b-6" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">                                
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
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

                                                <asp:BoundField DataField="id" HeaderText="User ID">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id ">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="username" HeaderText="User Name" SortExpression="username ">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="transaction_type" HeaderText="ADD/Deduct" SortExpression="transaction_type ">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="wallet_type" HeaderText="Wallet Type" SortExpression="wallet_type">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="amount" HeaderText="Amount" SortExpression="amount ">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>

                                                <asp:BoundField DataField="created_on" HeaderText="Transfer On" SortExpression="created_on">
                                                    <ItemStyle HorizontalAlign="Left" />
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

