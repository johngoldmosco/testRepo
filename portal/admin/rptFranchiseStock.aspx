<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="rptFranchiseStock.aspx.cs" Inherits="portal_admin_rptFranchiseStock" %>

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
                <a href="#">Franchise</a>
            </li>
            <li class="breadcrumb-item active">Franchise Stock</li>
        </ol>

        <div class="container-fluid">

            <div class="animated fadeIn">
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-accent-theme">                          
                             <div class="card-body">
                                    <h4 class="text-theme">Franchise Stock</h4>   
                                <br />       
                                 <div class="row">
                                      <div class="   ">
                                        <asp:TextBox ID="txtName" runat="server" class="form-control" placeholder="Product Name"></asp:TextBox>
                                    </div>
                                    <div class=" ">
                                       <asp:TextBox ID="txtUserID" runat="server" class="form-control" placeholder="Franchise ID"></asp:TextBox>
                                    </div>
                                      <div class="col-sm-2">
                                        <asp:LinkButton ID="lnkbtnGenerateReport" runat="server" class="btn btn-success   m-0" OnClick="lnkbtnGenerateReport_Click"><i class="fa fa-pencil"></i>Search</asp:LinkButton>
                                    </div>
                                 </div>
                             </div>
                            <div class="card-body"> 
                                 <div class="row form-inline" hidden="hidden">                                      
                                       <asp:LinkButton ID="lnkActive" runat="server" OnClick="lnkActive_Click" CssClass="m-lg-3">Active</asp:LinkButton>&nbsp;&nbsp;&nbsp;|                                   
                                        <asp:LinkButton ID="lnkDisable" runat="server" OnClick="lnkDisable_Click" CssClass="m-lg-3">Suspend</asp:LinkButton>
                                 </div>                                                    
                                <div class="table-responsive">
                                    <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>
                                    <asp:Panel ID="pnllead" runat="server">
                                        <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowSorting="true"
                                            DataKeyNames="id" Width="100%" border="0" align="center" CellPadding="0" PageIndex="1" PageSize="50"
                                            OnSorting="gvUsers_Sorting" OnRowDataBound="gvUsers_RowDataBound" CellSpacing="0" CssClass="table table-striped table-bordered" >
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
                                                <asp:BoundField DataField="id" HeaderText="Sr No" SortExpression="fransid">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
												  <asp:BoundField DataField="my_sponsar_id" HeaderText="User ID" SortExpression="my_sponsar_id">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>  
                                                <asp:BoundField DataField="prod_name" HeaderText="Product Name" SortExpression="prod_name">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
												 <asp:BoundField DataField="stock" HeaderText="Stock" SortExpression="stock">
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

