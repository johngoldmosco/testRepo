<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="EditPopup.aspx.cs" Inherits="portal_admin_EditPopup" %>

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
                <a href="#">Popup Manager</a>
            </li>
            <li class="breadcrumb-item active">Edit Popup</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Edit</strong>
                        Popup
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">Display on Dashboard: </label>
                                    <asp:DropDownList runat="server" ID="ddlShowPopup" CssClass="form-control">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Yes</asp:ListItem>
                                        <asp:ListItem Value="2">No</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-6">
                                    <label for="nf-password">PopUp Type</label>
                                    <asp:DropDownList runat="server" ID="ddlPopupType" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPopupType_SelectedIndexChanged">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        <asp:ListItem Value="1">Content</asp:ListItem>
                                        <asp:ListItem Value="2">Image</asp:ListItem>
                                    </asp:DropDownList>
                                    <span class="help-block">Please enter popup type</span>
                                </div>
                            </div>

                            <div class="row">
                                <label for="nf-password">Popup Heading</label>
                                <asp:TextBox runat="server" ID="txtHeader" CssClass="form-control"></asp:TextBox>
                            </div>

                            <asp:Panel runat="server" ID="pnlContent" Visible="false">
                                <div class="row">
                                    <label for="nf-password">Popup Text</label>
                                    <asp:TextBox runat="server" ID="txtContent" CssClass="form-control" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                </div>
                                <span class="help-block">Type Popup Text Here</span>
                            </asp:Panel>
                            <asp:Panel runat="server" ID="pnlImage" Visible="false">
                                <div class="row">
                                    <label for="nf-password">PopUp Image</label>
                                    <asp:FileUpload runat="server" ID="flupImage" CssClass="form-control" />
                                </div>
                                <span class="help-block">Upload PopUp Image here</span>
                                <div class="row">
                                    <label for="nf-password">Current Image: </label>
                                    <asp:Image runat="server" ID="imgPopup" CssClass="img img-thumbnail user-img" />
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="card-footer">
                            <asp:Button ID="btnEditPopUp" runat="server" CssClass="btn btn-flickr" Text="Edit Popup" OnClick="btnEditPopUp_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

