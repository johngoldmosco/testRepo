<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="Review.aspx.cs" Inherits="portal_member_Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script lang="javascript" type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtReview.ClientID%>").value == "") {
                alert("Kindly Fill review section!");
                document.getElementById("<%=txtReview.ClientID%>").focus();
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
                <a href="#">Support</a>
            </li>
            <li class="breadcrumb-item active">Review </li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-8">
                    <div class="card-header">
                        <strong>Member </strong>
                        Review
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="form-group">
                                <label for="nf-password">Write your review</label>
                                <asp:TextBox runat="server" ID="txtReview" CssClass="form-control" placeholder="Your review" TextMode="MultiLine" Rows="6"></asp:TextBox>
                                <span class="help-block">Please enter your review. Please Do not use ` (at symbol) </span>
                            </div>

                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClientClick="return validate();" OnClick="btnSubmit_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>



