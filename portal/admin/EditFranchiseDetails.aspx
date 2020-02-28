<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="EditFranchiseDetails.aspx.cs" Inherits="portal_admin_EditFranchiseDetails" %>

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
                <a href="#">Account</a>
            </li>
            <li class="breadcrumb-item active">Edit Profile</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="row">
                    <div class="col-sm-4">
                        <div class="card">
                            <div class="card-header text-theme">
                                <strong>Login </strong>
                                <small>Details</small>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="name">Franchise ID</label>
                                            <asp:TextBox runat="server" ID="txtFransID" Text="BD123452" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Franchise Name</label>
                                            <asp:TextBox runat="server" ID="txtFransName" Text="user" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Password</label>
                                            <asp:TextBox runat="server" ID="txtPassword" Text="123456" CssClass="form-control" required="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Transaction Password</label>
                                            <asp:TextBox runat="server" ID="txtTransPwd" Text="123456" CssClass="form-control" required="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-8">
                        <div class="card">
                            <div class="card-header text-theme">
                                <strong>Personal </strong>
                                <small>Details</small>
                            </div>
                            <div class="card-body">
                                <div class="form-group">
                                    <label for="txtFullName">Full Name</label>
                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" placeholder="Enter Full Name . . ."  required="true" ></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtFullName">Email ID</label>
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" placeholder="Enter Email . . ."  required="true"></asp:TextBox>
                                </div>
                                <div class="row">
                                    <div class="form-group col-sm-8">
                                        <label for="postal-code">Mobile Number</label>
                                        <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control"  onkeypress="return isNumberKey(event);" placeholder="Enter Mobile Number" MaxLength="10"  required="true"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:Button ID="btnSubmit" runat="server" CssClass=" btn btn-theme btn-sm"  OnClientClick="return validate();" OnClick="btnSubmit_Click" Text="Submit" />

                                <%-- <button type="submit" class="btn btn-theme btn-sm" onclick=""><i class="fa fa-dot-circle-o"></i>Submit</button>--%>
                            </div>
                            <!-- end card-body -->
                        </div>
                        <!-- end card -->
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>

