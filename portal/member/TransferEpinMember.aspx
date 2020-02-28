<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true"
    CodeFile="TransferEpinMember.aspx.cs" Inherits="portal_member_TransferEpinMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .rgOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0%;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(0,0,0,0.5);
            z-index: 99999999 !important;
        }
    </style>

    <script>
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        function UserDeleteConfirmation() {
            if (document.getElementById("<%=txtUserID.ClientID%>").value == "") {
                alert("Kindly Enter User ID !");
                document.getElementById("<%=txtUserID.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlEpinType.ClientID%>").value == "Select") {
                alert("Kindly Select Pin Type !");
                document.getElementById("<%=ddlEpinType.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEpinNo.ClientID%>").value == "") {
                alert("Kindly Enter number of epins that you want to transfer !");
                document.getElementById("<%=txtEpinNo.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEpinNo.ClientID%>").value < 1) {
                alert("Kindly Enter valid number of epins that you want to transfer !");
                document.getElementById("<%=txtEpinNo.ClientID%>").focus();
                return false;
            }

            var result = confirm("Are you sure to Transfer Epin(s)?");
            if (result) {

                $('#ajaxLoding').css("display", "block");
                return true;
            }
            else {
                return false;
            }
            return true;
        }
    </script>
    <noscript>Your browser does not support JavaScript!</noscript>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server" ID="ScriptManager1"></asp:ScriptManager>
    <div id="ajaxLoding" class="rgOverlay" style="display: none; text-align: center;">
        <img src="../images/pleasewait.gif" class="img-thumbnail" style="margin-top: 10%" />
    </div>
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Epin</a>
            </li>
            <li class="breadcrumb-item active">Transfer Epin</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Transfer </strong>
                        Epin
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="nf-password">User ID</label>
                                        <asp:TextBox ID="txtUserID" runat="server" class="form-control col-6" placeholder="User ID"
                                            required="true" OnTextChanged="txtUserID_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <span class="help-block">
                                            <asp:Label runat="server" ID="lblError" Text="" ForeColor="Red"></asp:Label></span><br />
                                        <span class="help-block">Please enter Receiver's UserID . . . </span>
                                    </div>
                                    <div class="form-group">
                                        <label for="nf-password">User Name</label>
                                        <asp:TextBox runat="server" ID="lblUserName" CssClass="form-control col-6" placeholder="User Name"
                                            Enabled="false" required="true"></asp:TextBox>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="txtUserID" EventName="TextChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel runat="server" ID="UpdatePanel2">
                                <ContentTemplate>
                                    <div class="form-group">
                                        <label for="nf-password">Epin Type</label>
                                        <asp:DropDownList ID="ddlEpinType" runat="server" CssClass="form-control col-6" OnSelectedIndexChanged="ddlEpinType_SelectedIndexChanged"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="nf-password">Available Epins</label>
                                        <asp:TextBox runat="server" ID="txtAvail" CssClass="form-control col-6" placeholder="Available Epins"
                                            Enabled="false" Text="0" required="true"></asp:TextBox>
                                        <span class="help-block">Your Available Epin</span>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ddlEpinType" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="form-group">
                                <label for="nf-password">Transfer Epins Count</label>
                                <asp:TextBox ID="txtEpinNo" runat="server" class="form-control col-6" placeholder="0"
                                    onkeypress="return isNumberKey(event);" required="true"></asp:TextBox>
                                <span class="help-block">Enter number of epins that you want to transfer </span>
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnSubmit" runat="server" class="btn btn-primary" Text="Transfer Epin"
                                    OnClick="btnSubmit_Click" OnClientClick="if ( ! UserDeleteConfirmation()) return false;" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

