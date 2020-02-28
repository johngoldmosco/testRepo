<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="SendSms.aspx.cs" Inherits="portal_admin_SendSms" %>

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
                <a href="#">SMS</a>
            </li>
            <li class="breadcrumb-item active">Send SMS</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Send </strong>
                        SMS
                    </div>
                    <div class="card-body text-theme">
                        <div>
                             <div class="form-group">
                                   <label for="nf-password">Total SMS Count used <asp:Label runat="server" ID="lblSMScnt"></asp:Label>/5000 </label>
                                 </div>
                            <div class="form-group">
                                <asp:CheckBox runat="server" ID="chkAll" OnCheckedChanged="chkAll_CheckedChanged" CssClass="checkbox" AutoPostBack="true" />
                                <label for="nf-password">Send To All Members </label>
                                <br />
                                <span class="help-block">Messages will sent to all members (excluding DND Nos.) </span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Mobile No.</label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Mobile" TextMode="Number" required="true"></asp:TextBox> 
                                <span class="help-block">Please enter mobile no</span>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Enter Message</label>
                                <asp:TextBox runat="server" ID="txtMessage" CssClass="form-control" placeholder="Message" Rows="6" TextMode="MultiLine" required="true"></asp:TextBox>
                                <span class="help-block">Please do not include speacial character like:  ` </span>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClick="btnSubmit_Click" OnClientClick="return validate();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

