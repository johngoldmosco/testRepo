<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="ViewTicket.aspx.cs" Inherits="portal_admin_ViewTicket" %>

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
                <a href="#">Support</a>
            </li>
            <li class="breadcrumb-item active">View Ticket</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">               
                <div class="card-body">
                    <div id="accordion" role="tablist">
                        <div class="card">
                            <div class="card-header" role="tab" id="headingOne">
                                <h6 class="mb-0">
                                    <a data-toggle="collapse" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">Reply  <i class="fa fa-minus pull-right"></i>
                                    </a>
                                </h6>
                            </div>

                            <div id="collapseOne" class="collapse show" role="tabpanel" aria-labelledby="headingOne" data-parent="#accordion">
                                <div class="card-body">
                                    <div class="card-body text-theme">
                                        <div>
                                           
                                            <div class="form-group">
                                                <label for="nf-password">Message</label>
                                                <asp:TextBox runat="server" ID="txtMessage" CssClass="form-control" placeholder="Message" TextMode="MultiLine" Rows="5" required="true"></asp:TextBox>
                                            </div>
                                            <div class="form-group col-sm-6">
                                                <label for="nf-password">Attachment</label>
                                                <asp:FileUpload runat="server" ID="flupAttach" CssClass="form-control" placeholder="Attachment" TextMode="MultiLine" Rows="5"></asp:FileUpload>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Answered" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                                          <asp:Button runat="server" ID="btnHold" CssClass="btn  btn-sm btn-info" Text="Hold" OnClick="btnHold_Click" />
                                                            <asp:Button runat="server" ID="btnClosed" CssClass="btn btn-sm btn-danger" Text="Close" OnClick="btnClosed_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <br />
                <asp:Literal ID="litReply" runat="server"></asp:Literal>
                <br />
                <div class="card card-accent-secondary mb-3">
                    <div class="card-header bg-info text-white">
                        <i class="fa fa-user"></i>
                        <asp:Label runat="server" ID="lblUserName"></asp:Label>
                        <span class="pull-right">
                            <asp:Label runat="server" ID="lblDateTime"></asp:Label>
                        </span>
                    </div>
                    <div class="card-body">
                        <p class="card-text">
                            <asp:Label runat="server" ID="lblTicketMessage"></asp:Label>
                        </p>
                        <p><a href="" runat="server" id="lnkAttachment" target="_blank">Attachment</a> </p>
                    </div>
                </div>

            </div>
        </div>
    </div>
</asp:Content>


