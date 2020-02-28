<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="Legal.aspx.cs" Inherits="Legal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>
.borderimg {
  border: 10px solid transparent;
  padding: 15px;
  border-image: url(border.png) 30 round;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Start All Pages Title -->
    <div class="page-title-main">
        <div class="container">
            <div class="clearfix">
                <div class="title-all text-center">
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Legal</li>
                    </ul>
                    <h2>Legal</h2>
                </div>
            </div>
        </div>
    </div>
    <!-- End All Pages Title -->
    <div class="our-team-main">
        <div class="container">
            <div class="clearfix">
                <div class="all-title text-center">
                    <h2>Legal</h2>                   
                    <span class="all-title-bar"></span>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="1000">
                        <div class="team-img">
                            <a href="#team-1" class="membername ">
                                <img src="images/COI_LG.jpg" class="borderimg" alt="" />
                                <div class="filter-title">
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div style="display: none">
                    <div class="teamdetail" id="team-1">
                        <div class="innerImg borderimg" style="background-image: url('images/COI_LG.jpg'); height: 1000px;"></div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="2000">
                        <div class="team-img">
                            <a href="#team-2" class="membername">
                                <img src="images/PAN_LG.jpg" class="borderimg" alt="" />
                                <div class="filter-title">
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div style="display: none">
                    <div class="teamdetail" id="team-2">
                        <div class="innerImg borderimg" style="background-image: url('images/PAN_LG.jpg'); height: 1000px;"></div>
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="3000">
                        <div class="team-img">
                            <a href="#team-3" class="membername">
                                <img src="images/TAN_LG.jpg" class="borderimg" alt="" />
                                <div class="filter-title">
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div style="display: none">
                    <div class="teamdetail" id="team-3">
                        <div class="innerImg borderimg" style="background-image: url('images/TAN_LG.jpg'); height: 1000px;"></div>
                    </div>
                </div>
                <div class="col-lg-6 col-md-6 col-sm-6">
                    <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="3000">
                        <div class="team-img">
                            <a href="#team-4" class="membername"> 
                                <img src="images/UdyogAadhar.jpg" class="borderimg" alt="" style="height:685px;" />
                                <div class="filter-title">
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
                <div style="display: none">
                    <div class="teamdetail" id="team-4">
                        <div class="innerImg borderimg" style="background-image: url('images/UdyogAadhar.jpg'); height: 830px;"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

