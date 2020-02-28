<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="portal_admin_EditProfile" %>

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
                                            <label for="name">User ID</label>
                                            <asp:TextBox runat="server" ID="txtUserID" Text="BD123452" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">User Name</label>
                                            <asp:TextBox runat="server" ID="txtUserName" Text="user" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="name">Referral ID</label>
                                            <asp:TextBox runat="server" ID="txtReferralID" Text="BD123451" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Referral Name</label>
                                            <asp:TextBox runat="server" ID="txtReferralName" Text="admin" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Password</label>
                                            <asp:TextBox runat="server" ID="txtPassword" Text="123456" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <button type="submit" class="btn btn-theme btn-sm"><i class="fa fa-dot-circle-o"></i>Submit</button>
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
                                     <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" placeholder="Enter Full Name . . ."></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label for="ddlCountry">Country</label>
                                   <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control">
                                       <asp:ListItem Value="Select">Select Country</asp:ListItem>
                                   </asp:DropDownList>
                                </div>                              

                                <div class="row">

                                    <div class="form-group col-sm-4">
                                        <label for="city">Code</label>
                                        <asp:TextBox runat="server" ID="txtMobileCode" CssClass="form-control" placeholder="+01" Enabled="false" ></asp:TextBox>
                                    </div>

                                    <div class="form-group col-sm-8">
                                        <label for="postal-code">Mobile Number</label>
                                        <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control" placeholder="Enter Mobile Number" ></asp:TextBox>
                                    </div>

                                </div>
                                <!--/.row-->

                                  <div class="form-group">
                                    <label for="street">City</label>
                                   <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" placeholder="Enter City Name" ></asp:TextBox>
                                </div>

                                <div class="form-group">
                                    <label for="country">Profile Picture</label>
                                    <asp:FileUpload CssClass="form-control" runat="server" ID="fileImage" />
                                </div>

                                <button type="submit" class="btn btn-theme btn-sm"><i class="fa fa-dot-circle-o"></i>Submit</button>
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

