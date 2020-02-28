<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="PopupManager.aspx.cs" Inherits="portal_admin_PopupManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
        function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) && k != 32);
        }
    </script>
    <link href="../css/page.css" rel="stylesheet" type="text/css" />
    <link href="../css/jquery-ui%20.css" rel="stylesheet" type="text/css" />

    <script src="http://code.jquery.com/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="http://code.jquery.com/ui/1.10.3/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">
        function printing() {
            window.print();
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
                <a href="#">Popup Manager</a>
            </li>
            <li class="breadcrumb-item active">Popup Manager</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Popup</strong>
                        Manager
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Select Page: </label>
                                    <asp:DropDownList runat="server" ID="ddlSelectType" CssClass="form-control" OnSelectedIndexChanged="ddlSelectType_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Website</asp:ListItem>
                                        <asp:ListItem Value="2">Member dashboard</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
   					<asp:Panel runat="server" ID="pnlShowData" Visible="false">
                    <div class="card-body text-theme">
                        <div>
                            <div class="row form-inline">
                                <asp:LinkButton ID="lnkShow" runat="server" OnClick="lnkShow_Click" CssClass="m-lg-3">Show</asp:LinkButton>&nbsp;&nbsp;&nbsp;|   <asp:LinkButton ID="lnkHide" runat="server" OnClick="lnkHide_Click" CssClass="m-lg-3">Hide</asp:LinkButton>
                            </div>
                            <div class="table-responsive">
                                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                <asp:Panel ID="pnllead" runat="server">
                                    <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" AllowSorting="True"
                                        DataKeyNames="id" Width="100%" border="0" CellPadding="0" CellSpacing="0"
                                        CssClass="table table-striped table-bordered" PageIndex="1" PageSize="50" OnRowDataBound="gvReport_RowDataBound" OnSorting="gvReport_Sorting">
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
                                                    <asp:HyperLink ID="lnkUpdate" runat="server"><asp:Image ImageUrl="~/portal/image/pencil.png" runat="server" /></asp:HyperLink>
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
                                              <asp:BoundField DataField="popup_type" HeaderText="Type" SortExpression="popup_type">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="popup_header" HeaderText="Popup Heading" SortExpression="popup_header">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="content" HeaderText="Content" SortExpression="content">
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:BoundField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    View Image
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HyperLink runat="server" NavigateUrl='<%# Eval("imgUrl") %>' Target="_blank"> View Image  </asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="status" HeaderText="Status" SortExpression="status">
                                                <ItemStyle HorizontalAlign="Center" />
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
                    </div>
					</asp:Panel>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

