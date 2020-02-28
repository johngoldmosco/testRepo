<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="Holidays.aspx.cs" Inherits="portal_admin_Holidays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server"></cc1:ToolkitScriptManager>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Profit Share</a>
            </li>
            <li class="breadcrumb-item active">Add Holiday</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Profit Share </strong>
                        Holiday
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Select Date</label>
                                <asp:TextBox runat="server" ID="txtDate" CssClass="form-control" placeholder="Select Date" required="true"></asp:TextBox>
                               <cc1:CalendarExtender ID="Calendar1" PopupButtonID="imgPopup" runat="server" TargetControlID="txtDate" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                            </div>                            
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Add Holiday For Profit Share" OnClick="btnSubmit_Click" OnClientClick="return validate();" />                        
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

