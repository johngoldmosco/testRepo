<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="AccountDetails.aspx.cs" Inherits="portal_member_AccountDetails" %>

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
            <li class="breadcrumb-item active">Bank Details</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Bank </strong>
                        Details
                    </div>
                    <div class="card-body text-theme">
                        <label class="text-danger"> Note:  User/ Member can fill bank details only once.</label>
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User ID</label>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" required="true" Enabled="false" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">User Name</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" required="true" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Pan Number</label>
                                <asp:TextBox runat="server" ID="txtPan" CssClass="form-control" required="true" onkeypress="return blockSpecialChar(event);" onkeyup=" return pulsar(this);" ReadOnly="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Mobile Number</label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" required="true" MaxLength="10" onkeypress="return isNumberKey(event);" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Name(AS on bank passbook)</label>
                                <asp:TextBox runat="server" ID="txtPayeeName" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Account Number</label>
                                <asp:TextBox runat="server" ID="txtAccount" CssClass="form-control" required="true" onkeypress="return isNumberKey(event);" ></asp:TextBox>
                            </div>
                        </div>                        
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Bank Name</label>
                                <asp:TextBox runat="server" ID="txtBankName" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Branch Name</label>
                                <asp:TextBox runat="server" ID="txtBranchName" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">IFSC Code</label>
                                <asp:TextBox runat="server" ID="txtIFSC" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Branch Code</label>
                                <asp:TextBox runat="server" ID="txtBranchCode" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-sm-6">
                                <label for="nf-password">MICR Code</label>
                                <asp:TextBox runat="server" ID="txtMICR" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                            <div class="form-group col-sm-6">
                                <label for="nf-password">Address</label>
                                <asp:TextBox runat="server" ID="txtAddress" CssClass="form-control" required="true" ></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" OnClick="btnSubmit_Click" Text="Update" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

