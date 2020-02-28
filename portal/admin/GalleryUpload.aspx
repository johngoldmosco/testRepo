<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="GalleryUpload.aspx.cs" Inherits="portal_admin_GalleryUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <script lang="javascript" type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
        function blockSpecialChar(e) {
            var k = e.keyCode;
            return ((k > 64 && k < 91) || (k > 96 && k < 123) || k == 8 || (k >= 48 && k <= 57) && k != 32);
        }
        function blockSpecialChar1(ex) {
            var k = ex.keyCode;
            return (k == 8 || k != 32);
        }
        function pulsar(obj) {
            obj.value = obj.value.toUpperCase();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="Dashboard.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Admin Settings</a>
            </li>
            <li class="breadcrumb-item active">Add Gallery Photos</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Add  </strong>
                        Gallery Photos
                    </div>
                    <div class="card-body">
                        <div>
                            <div class="row ">
                                <div class="form-group col-12">
                                    <label for="nf-password">Photo:</label>
                                    <asp:FileUpload runat="server" ID="flupPhoto" CssClass="form-control" />
									<span> <asp:RequiredFieldValidator runat="server" Display="Dynamic" ErrorMessage="* Required Field" ControlToValidate="flupPhoto" Forecolor="Red">
										</asp:RequiredFieldValidator></span> </br>
                                    <span>For better visual effect upload image of size([width * hight] [316 * 279]) And less than 3 MB </span>
                                </div>
                            </div>
                         
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button ID="btnSave" runat="server" CssClass="btn btn-info" Text="Add Photo" OnClick="btnSave_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

