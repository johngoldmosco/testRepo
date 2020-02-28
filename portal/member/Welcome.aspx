<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="portal_member_Welcome" %>

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
                <a href="#"></a>
            </li>
            <li class="breadcrumb-item active">Welcome</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Welcome </strong>
                        Credentials
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">User ID</label>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" placeholder="" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">User Name</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Password</label>
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Transaction Password</label>
                                <asp:TextBox runat="server" ID="txtTransactionPwd" CssClass="form-control" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn btn-sm btn-primary btn-round" OnClick="lnkbtnBack_Click">Click here to register new member</asp:LinkButton>
                      
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

