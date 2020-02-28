
<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Gallery.aspx.cs" Inherits="Gallery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Start All Pages Title -->
    <div class="page-title-main">
        <div class="container">
            <div class="clearfix">
                <div class="title-all text-center">
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Gallery</li>
                    </ul>
                    <h2>Gallery</h2>
                </div>
            </div>
        </div>
    </div>
    <!-- End All Pages Title -->
    <div class="our-team-main">
        <div class="container">
            <div class="clearfix">
                <div class="all-title text-center">
                    <h2>Gallery</h2>                   
                    <span class="all-title-bar"></span>
                </div>
            </div>
            <div class="row">
                 <asp:literal runat="server" id="lit1" ></asp:literal>
            </div>
        </div>
    </div>
</asp:Content>

