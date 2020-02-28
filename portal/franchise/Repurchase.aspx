<%@ Page Title="" Language="C#" MasterPageFile="~/portal/franchise/franchise.master" AutoEventWireup="true" CodeFile="Repurchase.aspx.cs" Inherits="portal_franchise_Repurchase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function validate() {

            if (document.getElementById("<%=CheckBoxList1.ClientID%>").value == "0") {
                alert("Kindly Select Product!");
                document.getElementById("<%=CheckBoxList1.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtTotalAmount.ClientID%>").value == "0") {
                alert("Kindly Select Product and Enter Valid Quantity!");
                document.getElementById("<%=txtTotalAmount.ClientID%>").focus();
                return false;
            }
            return true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
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
                <a href="#">Repurchase</a>
            </li>
            <li class="breadcrumb-item active">Repurchase Product</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Repurchase </strong>
                        Product
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User ID:</label>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" OnTextChanged="txtUserID_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User Name:</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                             
                            <div class="form-group">
                                <label for="nf-password">Repurchase Product</label>
                                <asp:CheckBoxList ID="CheckBoxList1" runat="server" AutoPostBack="True" CssClass="form-control col-md-8 " RepeatLayout="OrderedList" OnSelectedIndexChanged="CheckBoxList1_SelectedIndexChanged">
                                </asp:CheckBoxList>
                                <span class="help-block">Please Select productes From Here . . .</span>
                            </div>

                            <div class="form-group col-md-12">
                                <asp:Label ID="lblError" runat="server" Text="" Font-Bold="true" Font-Size="Medium" ForeColor="Red"></asp:Label>

                                <asp:Panel ID="pnllead" runat="server">
                                    <!-- DONT CHANGE THE POSITION 6  -->

                                    <asp:GridView ID="gvProducts" runat="server" AutoGenerateColumns="false" Width="100%" border="0" CellPadding="0" CellSpacing="0" CssClass="table table-striped table-bordered" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" OnRowDataBound="gvProducts_RowDataBound">
                                        <Columns>
                                            <asp:BoundField DataField="id" HeaderText="Product Code" />
                                            <asp:BoundField DataField="prod_name" HeaderText="Name" />
                                            <asp:BoundField DataField="prod_price" ItemStyle-CssClass="prod_price" HeaderText="Price" />
                                            <asp:BoundField DataField="pv" ItemStyle-CssClass="pv" HeaderText="BV" />
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextBoxQty" CssClass="txtQty" AutoPostBack="true" runat="server" Text='<%# Eval("SelQty") %>' MaxLength="2" Width="45" OnTextChanged="TextBoxQty_TextChanged" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxQty" Display="Dynamic" ForeColor="Red" ErrorMessage="RequiredFieldValidator">*</asp:RequiredFieldValidator>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbltotalPrice" runat="server" Text='<%# Eval("prod_price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="id" HeaderText="ID" Visible="true" />
                                        </Columns>
                                    </asp:GridView>

                                </asp:Panel>
                            </div>
                            <div class="form-group col-md-8">
                                <label for="nf-password">Total Amount </label>
                                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control" placeholder="0.00" Enabled="false" Text="0.00"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-8">
                                <label for="nf-password">Total PV </label>
                                <asp:TextBox ID="txtTotalBV" runat="server" CssClass="form-control" placeholder="0.00" Enabled="false" Text="0.00"></asp:TextBox>
                            </div>
                            <div class="form-group col-md-8" hidden="hidden">
                                <label for="nf-password">Your Balance</label>
                                <asp:TextBox ID="txtFranchiseBalance" runat="server" Enabled="false" CssClass="form-control"  placeholder="0.00" Text="0.00"></asp:TextBox>
                            </div>
                             <div class="form-group col-md-8">
                                 <asp:Label runat="server" ID="lblError1" ForeColor="Red" ></asp:Label>
                                </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnRepurchase" runat="server" class="btn btn-success margin-top-10" Text="Repurchase" OnClientClick="return validate();" OnClick="btnRepurchase_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

