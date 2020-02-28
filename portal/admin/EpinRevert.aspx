<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="EpinRevert.aspx.cs" Inherits="portal_admin_EpinRevert" %>

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
                <a href="#">Account</a>
            </li>
            <li class="breadcrumb-item active">Epin Revert</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Epin  </strong>
                        Revert
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">User ID</label>
                                <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="User ID" required="true"></asp:TextBox>
                            </div> 
                            <div class="form-group">
                                <label for="nf-password">Epin Type</label>
                                <asp:DropDownList ID="ddlEpinType" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEpinType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Epin Amount </label>
                                <asp:TextBox ID="txtEpinAmt" runat="server" CssClass="form-control" placeholder="Epin Amount" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Total Available Epins </label>
                                <asp:TextBox ID="txtAvlEpins" runat="server" CssClass="form-control" placeholder="Epin Amount" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="nf-password">Epin Nos. (To be Revert) </label>
                                <asp:TextBox ID="txtEpinNos" runat="server" CssClass="form-control" onkeypress="return isNumberKey(event);" placeholder="Epin Nos." required="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Revert Epin" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

