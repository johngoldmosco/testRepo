<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true"
    CodeFile="TransferEpin.aspx.cs" Inherits="portal_admin_TransferEpin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
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
                <a href="#">Epin</a>
            </li>
            <li class="breadcrumb-item active">Transfer Epin</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Transfer </strong>
                        Epin
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">User ID</label>
                                <asp:TextBox ID="txtUserID" runat="server" class="form-control col-6" placeholder="User ID"
                                    required="true" OnTextChanged="txtUserID_TextChanged" AutoPostBack="true"></asp:TextBox>

                                <span class="help-block">Please enter Receiver's UserID . . . </span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">User Name</label>
                                <asp:TextBox runat="server" ID="lblUserName" CssClass="form-control col-6" placeholder="User Name"
                                    Enabled="false" required="true"></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="nf-password">Epin Type</label>
                                <asp:DropDownList ID="ddlEpinType" runat="server" CssClass="form-control col-6" OnSelectedIndexChanged="ddlEpinType_SelectedIndexChanged"
                                    AutoPostBack="true">
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="1">Joining</asp:ListItem>
                                    <asp:ListItem Value="2">Free</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Available Epins</label>
                                <asp:TextBox runat="server" ID="txtAvail" CssClass="form-control col-6" placeholder="Available Epins"
                                    Enabled="false" Text="0" required="true"></asp:TextBox>
                                <span class="help-block">Your Available Epin</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">No. of Epins</label>
                                <asp:TextBox ID="txtEpinNo" runat="server" class="form-control col-6" placeholder="0"
                                    onkeypress="return isNumberKey(event);" required="true"></asp:TextBox>
                                <span class="help-block">Your Available Epin</span>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Transfer Epin"
                                    OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

