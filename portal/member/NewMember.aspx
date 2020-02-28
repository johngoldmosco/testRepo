<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="NewMember.aspx.cs" Inherits="portal_member_NewMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function validate() {
            if (document.getElementById("<%=txtReferralID.ClientID%>").value == "") {
                alert("Kindly Enter Sponsor ID!");
                document.getElementById("<%=txtReferralID.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPosition.ClientID%>").value == "0") {
                alert("Kindly Select Position!");
                document.getElementById("<%=ddlPosition.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtFullName.ClientID%>").value == "") {
                alert("Kindly enter the name");
                document.getElementById("<%=txtFullName.ClientID%>").focus();
                return false;
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
            if (document.getElementById("<%=txtCity.ClientID%>").value == "") {
                alert("Kindly Enter City!");
                document.getElementById("<%=txtCity.ClientID%>").focus();
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
            <li class="breadcrumb-item active">Add New Member</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="=row">
                    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
                </div>
                <div class="row">
                    <div class="col-sm-4">
                        <div class="card">
                            <div class="card-header text-theme">
                                <strong>Login </strong>
                                <small>Details</small>
                            </div>
                            <div class="card-body">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="name">Referral ID</label>
                                                    <asp:TextBox runat="server" ID="txtReferralID" placeholder="Enter referral User ID" CssClass="form-control" OnTextChanged="txtReferralID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="ccnumber">Referral Name</label>
                                                    <asp:TextBox runat="server" ID="txtReferralName" Text=" " CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtReferralID" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="name">Parent ID</label>
                                                    <asp:TextBox runat="server" ID="txtParentID" placeholder="Enter Parent User ID" CssClass="form-control" OnTextChanged="txtParentID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group">
                                                    <label for="ccnumber">Parent Name</label>
                                                    <asp:TextBox runat="server" ID="txtParentName" Text=" " CssClass="form-control" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtReferralID" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <div class="form-group row">
                                    <label class="col-md-4 form-control-label">Position</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlPosition" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPosition_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Value="0">Select Position</asp:ListItem>
                                            <asp:ListItem Value="1">Left Side</asp:ListItem>
                                            <asp:ListItem Value="2">Right Side</asp:ListItem>
                                        </asp:DropDownList>
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
                                                <asp:TextBox runat="server" ID="txtMobileNumber" CssClass="form-control" placeholder="Enter Mobile Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                <div class="form-group">
                                    <label for="street">Email ID</label>
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Enter Email ID"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="street">City</label>
                                    <asp:TextBox runat="server" ID="txtCity" CssClass="form-control" placeholder="Enter City Name"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-theme btn-sm" Text="Add New Member" OnClientClick="return validate();" OnClick="btnSubmit_Click" />

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



