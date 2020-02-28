<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="PackageManager.aspx.cs" Inherits="portal_admin_PackageManager" %>
<%--  This is packge manager epin type not a product manager only for this project  --%>
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

        .card .btn {
            margin-top: 0px;
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
    <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item active">
                <a href="#">Package Manager</a>
            </li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">
                            <div class="card-body">
                                <h4 class="text-theme">Package Manager</h4>
                                <div class="row">
                                    <div class="col-sm-3">
                                        <label class="sr-only" for="exampleInputEmail2">Package Name </label>
                                        <asp:TextBox ID="txtProdName" runat="server" class="form-control col-sm-12 " placeholder="Enter Package Name"></asp:TextBox>
                                    </div>

                                    <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-search fa-spin"></i>Search</asp:LinkButton>
                                    </div>
                                    <div class="col-sm-2">
                                        <asp:Button ID="btnAddProduct" runat="server" CssClass="btn btn-flickr" Text="Add New Package" OnClick="btnAddProduct_Click" />
                                    </div>
                                </div>
                                <div class="row" hidden="hidden">
                                    <div class="col-sm-1 ">
                                        <label class="sr-only" for="exampleInputEmail2">Product ID </label>
                                        <asp:TextBox ID="txtProdID" runat="server" class="form-control col-sm-12 " placeholder="Enter Product ID"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="card-body">
                                <div class="table-scrollable">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                            DataKeyNames="id" Width="100%" border="0" align="center" CellPadding="0" PageIndex="1" PageSize="50" OnSorting="gvUsers_Sorting" OnRowDataBound="gvUsers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered" OnRowCommand="gvCampaign_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="id" HeaderText="ID">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSel" runat="server" OnClick="javascript:SelectSingleCheckBox(this.id)" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkUpdate" runat="server"><asp:Image ImageUrl="~/portal/images/pencil.png" runat="server" /> Edit</asp:HyperLink>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="id" HeaderText="Sr No" SortExpression="id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="capping" HeaderText="Capping" SortExpression="capping">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="pin_type" HeaderText="Package Name" SortExpression="pin_type">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="epin_cost" HeaderText="Package Price" SortExpression="epin_cost">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField> 
                                                <asp:BoundField DataField="description" HeaderText="Description" SortExpression="description">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
												<asp:BoundField DataField="Active" HeaderText="Status" SortExpression="Active">
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

