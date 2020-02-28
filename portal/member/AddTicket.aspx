<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="AddTicket.aspx.cs" Inherits="portal_member_AddTicket" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#"></a>
            </li>
            <li class="breadcrumb-item active">Add Ticket</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Add   </strong>
                        Ticket
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Department</label>
                                <asp:DropDownList runat="server" ID="ddlCategory" CssClass="form-control form-control-variant form-control-round ">
                                    <asp:ListItem Value="Select">Select Department Type</asp:ListItem>
                                    <asp:ListItem Value="1">Support</asp:ListItem>
                                    <asp:ListItem Value="2">Admin</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Priority</label>
                                <asp:DropDownList runat="server" ID="ddlPriority" CssClass="form-control form-control-variant form-control-round ">
                                    <asp:ListItem Value="Select">Select Priority</asp:ListItem>
                                    <asp:ListItem Value="1">High</asp:ListItem>
                                    <asp:ListItem Value="2">Medium</asp:ListItem>
                                    <asp:ListItem Value="3">Low</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Subject </label>
                                <asp:TextBox ID="txtSubject" runat="server" class="form-control form-control-variant form-control-round " Placeholder="Enter your Subject Here . . ."></asp:TextBox>
                            </div>

                            <div class="form-group">
                                <label for="nf-password">Message</label>
                                <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control form-control-variant form-control-round " placeholder="Describe your Subject . . ." TextMode="MultiLine" Rows="5"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="nf-password">Attacchment</label>
                                <asp:FileUpload runat="server" ID="flupAttach" CssClass="form-control" />
                            </div>
                            <div class="form-group">
                                <asp:Label ID="lblError" runat="server" Font-Bold="true" Font-Size="Medium"
                                    ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnUpdate" runat="server" Text="Request Ticket" CssClass="btn btn-primary btn-round" OnClientClick="return validate();" OnClick="btnUpdate_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


