<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="RequestEpin.aspx.cs" Inherits="portal_member_RequestEpin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById('<%=txtEpinCount.ClientID %>').value == "") {
                document.getElementById('<%=txtEpinCount.ClientID %>').innerHTML = 'Enter Epin Count!';
                document.getElementById('<%=txtEpinCount.ClientID %>').focus();
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

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Epin</a>
            </li>
            <li class="breadcrumb-item active">Request Epin</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Request  </strong>
                        Epin
                    </div>
                    <div class="card-body text-theme">
                        <div>
                              <div class="form-group">
                                <label for="simpleFormEmail" class="form-group col-4">Epin Type: </label>
                               <asp:DropDownList runat="server" ID="ddlEpinType" CssClass="form-control" OnSelectedIndexChanged="ddlEpinType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label for="simpleFormEmail" class="form-group col-4">Epin Count: </label>
                                <asp:TextBox ID="txtEpinCount" runat="server" class="form-control form-group" placeholder="Enter Epin Count" AutoPostBack="true" OnTextChanged="txtEpinCount_TextChanged" onkeypress="return isNumberKey(event);" MaxLength="2" Text="1"></asp:TextBox>                              
                            </div>
                            <div class="form-group" hidden="hidden">
                                <label for="simpleFormPassword" class="form-group col-4">Available Balance: </label>
                                <asp:TextBox ID="txtBalance" runat="server" class="form-control form-group " placeholder="Available Balance" ReadOnly="true"></asp:TextBox>                                
                            </div>
                            <div class="form-group">
                                <label for="simpleFormPassword" class="form-group col-4">Total Cost: </label>
                                <asp:TextBox ID="txtTotCost" runat="server" class="form-control form-group" placeholder="Total Cost of Epins" ReadOnly="true"></asp:TextBox>
                            </div>
                             <div class="form-group">
                                <label for="simpleFormPassword" class="form-group col-4">Upload receipt</label>
                                <asp:FileUpload ID="flupReceipt" runat="server" CssClass="form-control" />
                            </div>
                             <div class="form-group">
                                <label for="simpleFormPassword" class="form-group col-4">Enter Reference No</label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="simpleFormPassword" class="form-group col-4"></label>
                                <asp:Label ID="lblError" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <label for="simpleFormPassword" class="form-group col-4"></label>
                        <asp:Button ID="btnSubmit" runat="server" class="btn btn-html5" Text="Request Epin" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>