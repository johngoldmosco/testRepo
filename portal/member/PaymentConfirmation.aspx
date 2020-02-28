<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true"
    CodeFile="PaymentConfirmation.aspx.cs" Inherits="portal_member_PaymentConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rgOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(0,0,0,0.5);
            z-index: 99999;
        }
    </style>

    <script>
        function UserDeleteConfirmation() {
            var result = confirm("Are you sure you want to withdraw requested amount?");
            if (result) {

                $('#ajaxLoding').css("display", "block");
                return true;
            }
            else
                return false;
        }
    </script>
    <script type="text/javascript">
        window.onload = window.history.forward(0);  //calling function on window onload
    </script>
    <noscript>Your browser does not support JavaScript!</noscript>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="ajaxLoding" class="rgOverlay" style="display: none">
        <img src="PleaseWait.jpg" class="center" /></div>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Secure Payment</a>
            </li>
            <li class="breadcrumb-item active">PaymentConfirmation</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <div class="col-sm-8">
                        <div class="card">
                            <div class="card-header">
                                <strong>Payment  </strong>
                                Confirmation
                            </div>
                            <div class="card-body text-theme">
                                <div class="form-inline">
                                    <div class="form-group col-sm-6">
                                        <label>Wallet Balance :</label>
                                        <asp:Label runat="server" ID="lblWalletBalance" Text=""></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <label>Withdraw amount :</label>
                                        <asp:Label runat="server" ID="lblReqAmt" Text=""></asp:Label>
                                    </div>
                                     <div class="form-group col-sm-12">
                                         
                                        <asp:Label runat="server" ID="lblError" Text="" ForeColor="Red"></asp:Label>
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <asp:Button runat="server" ID="btnCnf" CssClass="btn btn-sm btn-primary" Text="Confirm"
                                            OnClientClick="if ( ! UserDeleteConfirmation()) return false;" OnClick="btnCnf_Click" />
                                    </div>
                                    <div class="form-group col-sm-6">
                                        <asp:Button runat="server" ID="btnReject" CssClass="btn btn-sm btn-primary" Text="Cancel"
                                            OnClick="btnReject_Click" />
                                    </div>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

