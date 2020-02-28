<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true"
    CodeFile="WithdrawProfitShare.aspx.cs" Inherits="portal_member_WithdrawProfitShare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {

            if (document.getElementById("<%=txtRequestAmount.ClientID%>").value == "") {
                alert("Kindly Enter Request Amount!");
                document.getElementById("<%=txtRequestAmount.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtRequestAmount.ClientID%>").value < 0) {
                alert("Kindly Enter Request Amount greater then Rs 0!");
                document.getElementById("<%=txtRequestAmount.ClientID%>").focus();
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

        jQuery('.myClickDisabledElm').bind('dblclick', function (e) {
            e.preventDefault();
        })

        function UserDeleteConfirmation() {
            return confirm("Are you sure you want to Withdraw this amount?");
        }
    </script>
    <script type="text/javascript">
        window.onload = window.history.forward(0);  //calling function on window onload
    </script>
    <noscript>Your browser does not support JavaScript!</noscript>
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
            <li class="breadcrumb-item active">Withdraw Fund (Profit Share)</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <%--    <div class="col-sm-4">
                        <div class="card">
                            <div class="card-header">
                                <strong>Terms </strong>
                                Condition
                            </div>
                            <div class="card-body text-theme">
                                <p class="text-success">- You can only withdraw fund less than Current balance.</p>                                
                                <p class="text-info">- Once you request for withdraw to admin it will be success/ reject in 2-3 working days .</p>
                                <p class="text-danger">- Please make sure amount before withdrawal request .</p>
                            </div>
                        </div>
                    </div>
                    --%>
                    <div class="col-sm-8">
                        <div class="card">
                            <div class="card-header">
                                <strong>Withdraw </strong>
                                Fund (Profit Share)
                            </div>
                            <div class="card-body text-theme">
                                <div>
                                    <div class="form-group">
                                        <label for="nf-password">Current Balance</label>
                                        <asp:TextBox runat="server" ID="txtCurBalance" CssClass="form-control" placeholder="Your Current Balance"
                                            required="true" Enabled="false"></asp:TextBox>
                                        <span class="help-block">Your Current Balance Here . . .</span>
                                    </div>
                                    <div class="form-group">
                                        <label for="nf-password">Withdrwal Amount</label>
                                        <asp:TextBox runat="server" ID="txtRequestAmount" CssClass="form-control" placeholder="Amount.."
                                            required="true" onkeypress="return isNumberKey(this)" MaxLength="8"></asp:TextBox>
                                        <span class="help-block">Please Enter Here Amount you want to Withdrawal . . .</span>
                                    </div>
                                    <div class="form-group">
                                        <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round myClickDisabledElm"
                                     Text="Withdraw Request" OnClick="btnSubmit_Click"
                                    UseSubmitBehavior="false" />
                                <%--OnClientClick=" if ( ! UserDeleteConfirmation()) return false;"
                                    meta:resourcekey="BtnUserDeleteResource1"--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


