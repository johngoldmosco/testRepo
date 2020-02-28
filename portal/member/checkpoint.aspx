<%@ Page Title="" Language="VB" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="false" CodeFile="checkpoint.aspx.vb" Inherits="portal_member_checkpoint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtPwd.ClientID%>").value == "") {
        alert("Kindly Enter Password!");
        document.getElementById("<%=txtPwd.ClientID%>").focus();
        return false;
    }

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
                <a href="#">Security code</a>
            </li>
            <li class="breadcrumb-item active">Security code (M-Pin) for security Purpose . . .</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Security  </strong>
                        code
                    </div>
                    <div class="card-body text-theme">
                        <div class="form-group row">
                            <label class="col-sm-2 col-form-label">Enter Transaction Password</label>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtPwd" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>
						 <div class="form-group row">
                              <asp:Label runat="server" ID="lblError" ForeColor="Red" CssClass="text-danger"></asp:Label>
                          </div>
                        <div class="form-group row">
                            <div class="col-sm-4"></div>
                            <div class="col-sm-4">
                                <asp:Button ID="btnSave" runat="server" Text="Validate" CssClass="btn btn-info btn-round" OnClick="btnSave_Click"/>
                                <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-primary btn-round m-l-20" />
                            </div>
                            <div class="col-sm-4"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

