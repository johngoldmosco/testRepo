<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="overview.aspx.cs" Inherits="portal_member_overview" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item active">Overview </li>
        </ol>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
            PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
        </cc1:ModalPopupExtender>

        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup modal-dialog modal-lg" Style="display: none; width: 30%">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header">
                        <asp:Label runat="server" ID="lblPopupHeader" CssClass="form-control"></asp:Label>
                    </div>
                    <div class="modal-body text-center">
                        <p class="text-dark">
                            <asp:Label runat="server" ID="lblPopupBody" CssClass="form-control"></asp:Label>
                            <asp:Image runat="server" ID="imgPopup" CssClass="img img-thumbnail" />
                        </p>
                        <br />
                        <asp:Button ID="btnHide" runat="server" Text="Close" CssClass="btn btn-round btn-theme" />
                    </div>
                </div>
            </div>
        </asp:Panel>
        <div class="container-fluid">
            <div class="animated fadeInRightBig">
                <div class="breadcrumb bg-success" runat="server" id="divActive">
                    <asp:Label runat="server" ID="lblActivestatus" ForeColor="White" Text="status"></asp:Label>
                </div>
                <div class="breadcrumb bg-success text-white" runat="server" id="div1">
                    Last Binary Time : &nbsp;
                    <asp:Label runat="server" ID="lblBinaryTime" ForeColor="White" Text="Binary Time"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    Last Profit Share Time : &nbsp;
                    <asp:Label runat="server" ID="lblProfitShareTime" ForeColor="Red" Text="Profit Share Time"></asp:Label>

                </div>
                <div class="breadcrumb bg-info" runat="server">
                    <p class="text-white">
                        Left Link: &nbsp;
                        <asp:Label runat="server" ID="lblLeftRefLink" ForeColor="White" Text="status"></asp:Label>
                        &nbsp;<a href="whatsapp://send?text=<%=lblLeftRefLink.Text %>" data-action="share/whatsapp/share"><i class="fa fa-whatsapp m-r-20"></i></a>
                    </p>
                    <div class="clearfix"></div>
                    <p class="text-white">
                        Right Link: &nbsp; 
                        <asp:Label runat="server" ID="lblRightRefLink" ForeColor="White" Text="status"></asp:Label>
                        &nbsp;<a href="whatsapp://send?text=<%=lblRightRefLink.Text %>" data-action="share/whatsapp/share"><i class="fa fa-whatsapp m-r-20"></i></a>
                    </p>
                    <p class="text-white">Share via Whatsapp:  </p>
                </div>

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
                                        <small class="text-white">Your Wallet Summary</small>
                                    </div>
                                    <div class="float-right">
                                        <a href="#" class="btn btn-success">Award:
                                                <asp:Label runat="server" ID="lblAward"></asp:Label>
                                        </a>
                                        <a href="#" class="btn btn-success">Rank:
                                                <asp:Label runat="server" ID="lblRank"></asp:Label>
                                        </a>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-3">
                                        <div class="card-body">
                                            <div class="widget-pm-summary">
                                                <div class="widget-text">
                                                    <div class="h2 text-white">
                                                        <asp:Label runat="server" ID="lblBinary" Text="0.00"></asp:Label>
                                                    </div>
                                                    <small class="text-white">Total Binary Income</small>
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
                                                    <small class="text-white">Referral Income</small>
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
                                                    <small class="text-white">Total Sponsor Income</small>
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
                                                    <small class="text-white">Total Profit share Income </small>
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
                                    Direct
                                        <br />
                                    Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblDirectMember" Text="0"></asp:Label>
                                </div>
                                <div class="text-info ">
                                    <i class="fa fa-arrow-up"></i>Total
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-danger projects-charts-widget">
                            <div class="card-body">
                                <div class="text-danger h3">
                                    Downline
                                        <br />
                                    Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblDownlineMember" Text="0"></asp:Label>
                                </div>
                                <div class="text-danger ">
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
                                    Direct
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblActiveDirect" Text="0"></asp:Label>
                                </div>
                                <div class="text-success ">
                                    <i class="fa fa-arrow-up"></i>Member
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-primary projects-charts-widget">
                            <div class="card-body">
                                <div class="text-primary h3">
                                    Active
                                        <br />
                                    Downline
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblActiveDownline" Text="0"></asp:Label>
                                </div>
                                <div class="text-primary ">
                                    <i class="fa fa-arrow-up"></i>Member
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
                                    Left
                                        <br />
                                    Direct Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTotalLeftDirect" Text="0"></asp:Label>
                                </div>
                                <div class="text-info ">
                                    <i class="fa fa-arrow-up"></i>Total
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-danger projects-charts-widget">
                            <div class="card-body">
                                <div class="text-danger h3">
                                    Right
                                        <br />
                                    Direct Member
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTotalRightDirect" Text="0"></asp:Label>
                                </div>
                                <div class="text-danger ">
                                    <i class="fa fa-arrow-up"></i>Total
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-success projects-charts-widget">
                            <div class="card-body">
                                <div class="text-success h3">
                                    Left
                                        <br />
                                    Downline Members
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTotalLeftDownline" Text="0"></asp:Label>
                                </div>
                                <div class="text-success ">
                                    <i class="fa fa-arrow-up"></i>Member
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-3">
                        <div class="card card-accent-primary projects-charts-widget">
                            <div class="card-body">
                                <div class="text-primary h3">
                                    Right
                                        <br />
                                    Downline Members
                                </div>
                                <div class="text-dark h2">
                                    <span class="text-secondary"></span>
                                    <asp:Label runat="server" ID="lblTotalRightDownline" Text="0"></asp:Label>
                                </div>
                                <div class="text-primary ">
                                    <i class="fa fa-arrow-up"></i>Member
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="row">
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-3" >
                                <div class="card card-accent-danger">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-danger">
                                                    <asp:Label runat="server" ID="lblWallet1" Text="0.00"></asp:Label></div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong>Working </strong>
                                            </div>
                                            <div class="h6 text-danger">Balance </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3" >
                                <div class="card card-accent-success">
                                    <div class="card-body">
                                        <div class="clearfix">
                                            <div class="float-right">
                                                <div class="h2 text-success">
                                                    <asp:Label runat="server" ID="lblWallet2" Text="0.00"></asp:Label></div>
                                            </div>
                                        </div>
                                        <div class="float-left">
                                            <div class="h3 ">
                                                <strong> Profit Share </strong>
                                            </div>
                                            <div class="h6 text-success"> Balance </div>
                                        </div>
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

