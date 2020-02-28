<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="Business_plan.aspx.cs" Inherits="portal_member_Business_plan" %>

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
            <li class="breadcrumb-item active">Business Plan</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Business  </strong>
                        Plan
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <object data="LifeGoldVer1.pdf" type="application/pdf" width="100%" height="700px">
                                <a href="LifeGoldVer1.pdf" target="_blank">Lifegold</a>
                            </object>
                        </div>
                    </div>
                    <div class="card-footer">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

