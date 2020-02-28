<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="EditProfile.aspx.cs" Inherits="portal_member_EditProfile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtFullName.ClientID%>").value == "") {
                alert("Kindly Enter Name!");
                document.getElementById("<%=txtFullName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Kindly Enter User Email!");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (!reg.test(document.getElementById("<%=txtEmail.ClientID%>").value)) {
                alert('Invalid Email Address');
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
            }

            if (document.getElementById("<%=ddlCountry.ClientID%>").value == "0") {
                alert("Kindly Select Country!");
                document.getElementById("<%=ddlCountry.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMobileNumber.ClientID%>").value == "") {
                alert("Kindly Enter Mobile Number!");
                document.getElementById("<%=txtMobileNumber.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtCity.ClientID%>").value == "") {
                alert("Kindly Enter City!");
                document.getElementById("<%=txtCity.ClientID%>").focus();
                return false;
            }
            return true;
        }
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function validateNominee() {
            if (document.getElementById("<%=txtNomName.ClientID%>").value == "") {
                alert("Kindly Enter Nominee Name!");
                document.getElementById("<%=txtNomName.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtNomAge.ClientID%>").value == "") {
                alert("Kindly Enter Nominee Age!");
                document.getElementById("<%=txtNomAge.ClientID%>").focus();
                return false;
            }

            if (document.getElementById("<%=txtNomRel.ClientID%>").value == "") {
                alert("Kindly Enter Nominee Relation!");
                document.getElementById("<%=txtNomRel.ClientID%>").focus();
                return false;
            }

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                                            <label for="ccnumber">Password</label>
                                            <asp:TextBox runat="server" ID="txtPassword" Text="123456" CssClass="form-control" Enabled="false"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>

                        <div class="card">
                            <div class="card-header text-theme">
                                <strong>Nominee </strong>
                                <small>Details</small>
                            </div>
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="name">Nominee Name</label>
                                            <asp:TextBox runat="server" ID="txtNomName" Text="" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="ccnumber">Nominee Age</label>
                                            <asp:TextBox runat="server" ID="txtNomAge" Text="user" CssClass="form-control" Enabled="true" onkeypress="return isNumberKey(event);" MaxLength="2"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12">
                                        <div class="form-group">
                                            <label for="name">Nominee Relation</label>
                                            <asp:TextBox runat="server" ID="txtNomRel" Text="" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="btnNominee" runat="server" CssClass=" btn btn-theme btn-sm " OnClientClick="return validateNominee();" OnClick="btnNominee_Click" Text="Update Nominee" />
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
                                    <asp:TextBox runat="server" ID="txtFullName" CssClass="form-control" placeholder="Enter Full Name . . ." Enabled="false"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtFullName">Email ID</label>
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter Email . . ."></asp:TextBox>
                                </div>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">
                                            <label for="ddlCountry">Country</label>
                                            <asp:DropDownList runat="server" ID="ddlCountry" CssClass="form-control" OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="Select">Select Country</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>

                                        <div class="row">

                                            <div class="form-group col-sm-4">
                                                <label for="city">Code</label>
                                                <asp:TextBox runat="server" ID="txtMobileCode" CssClass="form-control" placeholder="+01" Enabled="false"></asp:TextBox>
                                            </div>

                                            <div class="form-group col-sm-8">
                                                <label for="postal-code">Mobile Number</label>
                                                <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control" onkeypress="return isNumberKey(event);" placeholder="Enter Mobile Number" MaxLength="10"></asp:TextBox>
                                            </div>

                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <!--/.row-->

                                <div class="form-group">
                                    <label for="street">City</label>
                                    <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" placeholder="Enter City Name"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="street">Pan</label>
                                    <asp:TextBox runat="server" ID="txtPan" CssClass="form-control" placeholder="Enter Pan" ReadOnly="true"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="street">Aadhar</label>
                                    <asp:TextBox runat="server" ID="txtAadhar" CssClass="form-control" placeholder="Enter Aadhar No." MaxLength="12" onkeypress="return isNumberKey(event);"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="country">Profile Picture</label>
                                    <asp:FileUpload CssClass="form-control" runat="server" ID="fileImage" />
                                </div>
                                <div class="form-group" runat="server" visible="false">
                                    <label for="country">Profile Picture</label>
                                    <asp:Image ID="imgProfile" runat="server" CssClass="img-thumbnail w-25" />
                                </div>


                                <div class="row">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass=" btn btn-theme btn-sm " OnClientClick="return validate();" OnClick="btnSubmit_Click" Text="Update Profile" />
                                    </div>
                                </div>
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


