<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="KYCuploads.aspx.cs" Inherits="portal_member_KYCuploads" %>

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
            <li class="breadcrumb-item active">KYC Uploads</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>KYC </strong>
                        Uploads
                    </div>
                    <div class="card-body text-theme">
                        <div>
                            <div class="row">
                                <div class="form-group col-sm-8">
                                    <label class="col-sm-3" for=""><strong>Pan Card :</strong> </label>
                                    <asp:FileUpload ID="fu_pan_card" runat="server" CssClass="text" Width="320px"></asp:FileUpload>
                                </div>
                                <div class="form-group col-sm-4">

                                    <asp:Image runat="server" ID="imgPAN" CssClass="img-thumbnail" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-sm-8">
                                    <label class="col-sm-3" for=""><strong>Aadhar Card :</strong> </label>
                                    <asp:FileUpload ID="fu_aadhar_card" runat="server" CssClass="text" Width="320px"></asp:FileUpload>
                                </div>
                                <div class="form-group col-sm-4">
                                    <asp:Image runat="server" ID="imgAdhar" CssClass="img-thumbnail" />
                                </div>
                            </div>
							<%--<div class="row">
                                <div class="form-group col-sm-8">
                                    <label class="col-sm-3" for=""><strong>Aadhar Card (Back Side - optional) :</strong> </label>
                                    <asp:FileUpload ID="fu_aadhar_cardBack" runat="server" CssClass="text" Width="320px"></asp:FileUpload>
                                </div>
                                <div class="form-group col-sm-4">
                                    <asp:Image runat="server" ID="imgAdharBack" CssClass="img-thumbnail" />
                                </div>
                            </div>--%>
                            <div class="row">
                                <div class="form-group col-sm-8">
                                    <label class="col-sm-3" for=""><strong>Photo :</strong> </label>
                                    <asp:FileUpload ID="fu_photo" runat="server" CssClass="text" Width="320px"></asp:FileUpload>
                                </div>
                                <div class="form-group col-sm-4">
                                    <asp:Image runat="server" ID="imgPhoto" CssClass="img-thumbnail" />
                                </div>
                            </div>
                              <div class="row">
                                <div class="form-group col-sm-8">
                                    <label class="col-sm-3" for=""><strong>Cancel Cheque/ Passbook :</strong> </label>
                                     <asp:FileUpload ID="fileCheque" runat="server" CssClass="text" Width="320px" ></asp:FileUpload>	
                                </div>
                                <div class="form-group col-sm-4">
                                    <asp:Image runat="server" ID="imgCheque" CssClass="img-thumbnail"/>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card-footer">
                        <asp:Button runat="server" ID="btnSubmit" CssClass="btn btn-sm btn-primary btn-round" Text="Submit" OnClientClick="return validate();" OnClick="btnAdd_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

