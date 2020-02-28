<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="DepositFund.aspx.cs" Inherits="portal_member_DepositFund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
            <li class="breadcrumb-item active">Deposit Fund</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <div class="col-sm-8">
                        <div class="card ">
                            <div class="card-header">
                                <strong>Deposit </strong>
                                Fund
                            </div>
                            <div class="card-body text-theme">
                                <div>
                                    <div class="form-group">
                                        <label for="nf-password">Wallet Type</label>
                                        <asp:DropDownList runat="server" ID="ddlWallet" CssClass="form-control" required="true">
                                            <asp:ListItem Value="Select">Select Wallet</asp:ListItem>
                                        </asp:DropDownList>
                                        <span class="help-block">Select First Your Wallet . . .</span>
                                    </div>

                                    <div class="form-group">
                                        <label for="nf-password">Current Balance</label>
                                        <asp:TextBox runat="server" ID="txtCurBalance" CssClass="form-control" placeholder="Your Current Balance" required="true"></asp:TextBox>
                                        <span class="help-block">Your Current Balance Here . . .</span>
                                    </div>   
                                                                    
                                    <div class="form-group">
                                        <label for="nf-password">Deposit Amount</label>
                                        <asp:TextBox runat="server" ID="txtAmt" CssClass="form-control" placeholder="Amount.." required="true"></asp:TextBox>
                                        <span class="help-block">Please Enter Here Amount you want to Withdrawal . . .</span>
                                    </div>

                                    <div class="form-group">
                                        <label for="nf-password">Balance will be</label>
                                        <asp:TextBox runat="server" ID="txtNextbalance" CssClass="form-control" placeholder="New Balance.." required="true"></asp:TextBox>
                                        <span class="help-block">After Withdraw you Balance will be . . .</span>
                                    </div>
                                </div>
                            </div>
                            <div class="card-footer">
                                <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Deposit Fund" />
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-4">
                        <div class="card">
                            <div class="card-header">
                                <strong>Terms </strong>
                                Condition
                            </div>
                            <div class="card-body text-theme">
                                <p class="text-success">- You can only send from your E-wallet to E-wallet of another member.</p>
                                <p class="text-warning">- Receiver ID should have your downline member.</p>
                                <p class="text-info">- You are totally responsible for this transaction .</p>
                                <p class="text-danger">- Please make sure amount before transfer fund .</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

