<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="portal_admin_Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item active">Overview </li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeInRightBig">
                <div class="row">
                    <div class="col-md-12">
                        <div class="card card-pm-summary bg-theme">
                            <div class="card-body">

                                <div class="clearfix">
                                    <div class="float-left">
                                        <div class="h3 text-white">
                                            <strong>Welcome, &nbsp
                                                <asp:Label runat="server" ID="lblFullName" Text="Guest"></asp:Label></strong>
                                        </div>
                                        <small class="text-white">Total Income Summary</small>
                                    </div>

                                    <!--<div class="float-right">
                                            <a href="ActivateMembership.aspx" class="btn btn-success">Activate MemberShip</a>                                           
                                        </div>-->
                                </div>

                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="card-body">
                                            <div class="widget-pm-summary">

                                                <div class="widget-text">
                                                    <div class="h2 text-white">
                                                        <asp:Label runat="server" ID="lblBinary" Text="0.00"></asp:Label>
                                                    </div>
                                                    <small class=" text-white">Binary </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card-body">
                                            <div class="widget-pm-summary">

                                                <div class="widget-text">
                                                    <div class="h2 text-white">
                                                        <asp:Label runat="server" ID="lblReferral" Text="0.00"></asp:Label>
                                                    </div>
                                                    <small class=" text-white">Referral </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card-body">
                                            <div class="widget-pm-summary">

                                                <div class="widget-text">
                                                    <div class="h2 text-white">
                                                        <asp:Label runat="server" ID="lblSponsorInc" Text="0.00"></asp:Label>
                                                    </div>
                                                    <small class=" text-white">Total Sponsor Income </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="card-body">
                                            <div class="widget-pm-summary">
                                                <div class="widget-text">
                                                    <div class="h2 text-white">
                                                        <asp:Label runat="server" ID="lblProfitShare" Text="0.00"></asp:Label>
                                                    </div>
                                                    <small class=" text-white">Total Profit Share </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="card card-accent-danger">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-danger">
                                                    <asp:Label runat="server" ID="lblTotalBinary" Text="0.00"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Total</strong>
                                            </div>
                                            <div class="h6 text-danger">Binary </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="card card-accent-success">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-success">
                                                    <asp:Label runat="server" ID="lblTodaysBinary" Text="0.00"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Todays</strong>
                                            </div>
                                            <div class="h6 text-success">Binary </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="card card-accent-primary">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-primary">
                                                    <asp:Label runat="server" ID="lblRequestAmount" Text="0.00"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Withdrawal</strong>
                                            </div>
                                            <div class="h6 text-primary">Request Amount </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="card card-accent-warning">
                                    <div class="card-body ">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-warning">
                                                    <asp:Label runat="server" ID="lblReceivedAmount" Text="0.00"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Received</strong>
                                            </div>
                                            <div class="h6 text-warning">Amount </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-3">
                        <div class="card card-accent-info projects-charts-widget">
                            <div class="card-body">
                                <div class="text-info h3">
                                    Total
                                        <br />
                                    Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTotalMember" Text="0"></asp:Label>
                                </div>
                                <div class="text-info ">
                                    <i class="fa fa-arrow-up"></i>Total
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-success projects-charts-widget">
                            <div class="card-body">
                                <div class="text-success h3">
                                    Active
                                        <br />
                                    Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblActiveMembres" Text="0"></asp:Label>
                                </div>
                                <div class="text-success ">
                                    <i class="fa fa-arrow-up"></i>Total
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-danger projects-charts-widget">
                            <div class="card-body">
                                <div class="text-danger h3">
                                    Pending
                                        <br />
                                    Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblPendingMember" Text="0"></asp:Label>
                                </div>
                                <div class="text-danger ">
                                    <i class="fa fa-arrow-up"></i>Member
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-primary projects-charts-widget">
                            <div class="card-body">
                                <div class="text-primary h3">
                                    Todays
                                        <br />
                                    Joinging
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTodaysJoining" Text="0"></asp:Label>
                                </div>
                                <div class="text-primary ">
                                    <i class="fa fa-arrow-up"></i>Member
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

