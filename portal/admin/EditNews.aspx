<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="EditNews.aspx.cs" Inherits="portal_admin_EditNews" %>

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
                <a href="#">Admin Settings</a>
            </li>
            <li class="breadcrumb-item active">Edit  News</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="card">
                            <div class="card-header text-theme">
                                <strong>Edit   </strong>
                                <small>News</small>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="name">News Title </label>
                                            <asp:TextBox ID="txtNewsTitle" runat="server" CssClass="form-control" placeholder="News Title" required="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">News Description</label>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control border-dark" placeholder="News Description" TextMode="MultiLine" Rows="6" required="true" MaxLength="500"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Edit News" OnClick="btnSave_Click" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

