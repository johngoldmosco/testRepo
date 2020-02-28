<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="WalletDetails.aspx.cs" Inherits="portal_member_WalletDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Reports</a>
            </li>
            <li class="breadcrumb-item active">Wallet Details</li>
        </ol>

      <div class="container-fluid">
            <div class="animated fadeInRightBig">
       
                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-accent-danger">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-danger">
                                                    <asp:Label runat="server" ID="lblWallet1" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Working Balance </strong>
                                            </div>
                                            <div class="h6 text-danger"> </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="card card-accent-danger">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-danger">
                                                    <asp:Label runat="server" ID="lblWallet2" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Profit Share Balance </strong>
                                            </div>
                                            <div class="h6 text-danger"> </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                         <%--   <div class="col-md-4">
                                <div class="card card-accent-success">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-success">
                                                    <asp:Label runat="server" ID="lblWallet2" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Cashback Wallet </strong>
                                            </div>
                                            <div class="h6 text-success"> </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-4">
                                <div class="card card-accent-info">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-info">
                                                    <asp:Label runat="server" ID="lblWallet3" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Total Downline </strong>
                                            </div>
                                            <div class="h6 text-info">Benefit </div>
                                        </div>
                                    </div>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>

                <%--    <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="card card-accent-danger">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-danger">
                                                    <asp:Label runat="server" ID="lblWallet11" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Current Direct </strong>
                                            </div>
                                            <div class="h6 text-danger">Benefit </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="card card-accent-success">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-success">
                                                    <asp:Label runat="server" ID="lblWallet12" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Current Gap </strong>
                                            </div>
                                            <div class="h6 text-success">Benefit </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                             <div class="col-md-4">
                                <div class="card card-accent-info">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-info">
                                                    <asp:Label runat="server" ID="lblWallet13" Text="0"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Current Downline </strong>
                                            </div>
                                            <div class="h6 text-info">Benefit </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>--%>
            </div>
        </div>
    </div>
        <!-- end container-fluid -->
  
</asp:Content>

