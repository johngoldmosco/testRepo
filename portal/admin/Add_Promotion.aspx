<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="Add_Promotion.aspx.cs" Inherits="portal_admin_Add_Promotion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="Dashboard.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Admin Settings</a>
            </li>
            <li class="breadcrumb-item active">Add Promotion Materail</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Add  </strong>
                        Promotion Materail
                    </div>
                    <div class="card-body">
                        <div>
                            <div class="row ">
                                <div class="form-group col-12">
                                    <label for="nf-password"> Title:</label>
                                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Heading" required="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group  col-12">
                                    <label for="nf-password">Promotion Type:</label>
                                    <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control" AutoPostBack ="true" OnSelectedIndexChanged="ddlType_SelectedIndexChanged">
                                        <asp:ListItem Value="Select">Select Type</asp:ListItem>
                                         <asp:ListItem Value="1">Image</asp:ListItem>
                                         <asp:ListItem Value="2">Youtube Link</asp:ListItem>
                                         <asp:ListItem Value="3">Document</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divImage" visible="false">
                                <div class="form-group col-12">
                                    <label for="nf-password"> Image:</label>
                                    <asp:FileUpload ID="fileImage" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="row" runat="server" id="divYoutube" visible="false">
                                <div class="form-group col-12">
                                    <label for="nf-password"> Youtube Link:</label>
                                    <asp:TextBox ID="txtYoutubeLink" runat="server" CssClass="form-control" placeholder="Enter here Youtube Link" required="true"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" runat="server" id="divDocument" visible="false">
                                <div class="form-group col-12">
                                    <label for="nf-password"> Document:</label>
                                    <asp:FileUpload ID="fileDoc" runat="server" CssClass="form-control" placeholder="Like PDF, Word, Excel, PPT etc"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Submit" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

