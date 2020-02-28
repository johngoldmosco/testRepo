<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="CreditDebitWallet.aspx.cs" Inherits="portal_admin_CreditDebitWallet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script lang="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function validate() {
            if (document.getElementById("<%=ddlWalletType.ClientID%>").value == "0") {
                alert("Kindly Select Wallet Type!");
                document.getElementById("<%=ddlWalletType.ClientID%>").focus();
                return false;
            } 
            if (document.getElementById("<%=ddlTransactionType.ClientID%>").value == "0") {
                alert("Kindly Select Transaction Type!");
                document.getElementById("<%=ddlTransactionType.ClientID%>").focus();
                return false;
            } 
            return true;
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
                <a href="#">E-Wallet</a>
            </li>
            <li class="breadcrumb-item active">Credit Debit Wallet</li>
        </ol>

     <div class="container-fluid">
         <div class="animated fadeIn">
             <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <ContentTemplate>
                     <div class="card col-sm-8">
                         <div class="card-header">
                             <strong>Credit Debit </strong>
                             Wallet
                         </div>
                         <div class="card-body text-theme">
                             <div>
                                  <div class="form-group">
                                     <label for="nf-password">Wallet Type</label>
                                     <asp:DropDownList runat="server" ID="ddlWalletType" CssClass="form-control" required="true">
                                         <asp:ListItem Value="Select">Select Wallet Type</asp:ListItem>
                                         <asp:ListItem Value="1">E Wallet</asp:ListItem>
                                         <asp:ListItem Value="2">Cashback Wallet</asp:ListItem>
                                     </asp:DropDownList>
                                     <span class="help-block">Select Wallet type</span>
                                 </div>
                                 <div class="form-group">
                                     <label for="nf-password">Member ID</label>
                                     <asp:TextBox runat="server" ID="txtMemberID" CssClass="form-control" placeholder="UserID" required="true" AutoPostBack="true" OnTextChanged="txtMemberID_TextChanged"></asp:TextBox>
                                     <span class="help-block">Please enter Member ID for Credit / Debit Wallet</span>
                                 </div>
                                 <div class="form-group">
                                     <label for="nf-password">UserName</label>
                                     <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="Member User Name" Enabled="false" required="true"></asp:TextBox>                                     
                                 </div>
                                   <div class="form-group">
                                     <label for="nf-password">Current Balance</label>
                                     <asp:TextBox runat="server" ID="txtCurrentBalance" CssClass="form-control" placeholder="Current Balance" Enabled="false" required="true"></asp:TextBox>                                      
                                 </div>
                                 <div class="form-group">
                                     <label for="nf-password">Transaction Type</label>
                                     <asp:DropDownList runat="server" ID="ddlTransactionType" CssClass="form-control" required="true">
                                         <asp:ListItem Value="Select">Select Transaction Type</asp:ListItem>
                                         <asp:ListItem Value="1">Credit Wallet (Add) </asp:ListItem>
                                         <asp:ListItem Value="2">Debit Wallet (Deduct) </asp:ListItem>
                                     </asp:DropDownList>
                                     <span class="help-block">Select Transaction type</span>
                                 </div>
                                 <div class="form-group">
                                     <label for="nf-password">Amount </label>
                                     <asp:TextBox runat="server" ID="txtAmount" CssClass="form-control" placeholder="Amount" required="true" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                     <span class="help-block">Enter Amount for Transaction</span>
                                 </div>
                             </div>
                         </div>
                         <div class="card-footer">
                             <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                         </div>
                     </div>
                 </ContentTemplate>
                 <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="txtMemberID" EventName="TextChanged" />
                     <asp:PostBackTrigger ControlID="btnSubmit" />                     
                 </Triggers>
             </asp:UpdatePanel>
         </div>
     </div>
 </div>
</asp:Content>



