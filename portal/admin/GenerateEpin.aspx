<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="GenerateEpin.aspx.cs" Inherits="portal_admin_GenerateEpin" %>

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
        function blockSpecialChar1(ex) {
            var k = ex.keyCode;
            return (k == 8 || k != 32);
        }
        function pulsar(obj) {
            obj.value = obj.value.toUpperCase();
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
                <a href="#">E-Pin</a>
            </li>
            <li class="breadcrumb-item active">Generate Epin</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Generate </strong>
                        Epin
                    </div>
                    <div class="card-body text-theme">
                        <div>

                            <div class="form-group">
                                <label for="simpleFormPassword" class=" form-group col-4">E-Pin Type: </label>
                                <asp:DropDownList ID="ddlEpinType" runat="server" CssClass="form-control form-group m-r-20" required>
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Joining</asp:ListItem>
                                    <asp:ListItem Value="2">Free Epin</asp:ListItem>
                                </asp:DropDownList>
                                <span class="help-block">Please select the type of Epin</span>
                                <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlEpinType"
                                    InitialValue="0" ErrorMessage="Select Epin Type!" SetFocusOnError="true" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label for="simpleFormPassword" class="form-group col-4">No. of Epins: </label>

                                <asp:TextBox ID="txtEpinNo" runat="server" class="form-control form-group " placeholder="0" onkeypress="return isNumberKey(event);" required="true"></asp:TextBox>
                                <span class="help-block">Please enter how much of epins you want to create</span>
                            </div>
                            <div class="form-group text-center">
                                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Generate Epin" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

