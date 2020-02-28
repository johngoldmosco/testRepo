<%@ Page Title="" Language="C#" MasterPageFile="~/portal/member/Master.master" AutoEventWireup="true" CodeFile="WelcomeLetter.aspx.cs" Inherits="portal_member_WelcomeLetter" %>

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
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Account</a>
            </li>
            <li class="breadcrumb-item active">Welcome Letter</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Welcome </strong>
                        Letter
                    </div>
                    <div class="card-body text-theme">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="card borderimg">
                                    <div class="card-body">
                                        <div class="mail-view">
                                            <div class="mail-toolbar" style="float: right;"> 
                                                 <img src="WelcomeLogo.png" class="user-img img-avatar" alt="LifeGold" height="80" width="260"  />  
                                            </div>
                                            <!-- end mail-toolbar -->
                                            <div class="mail-view-header">
                                                <img src="../../images/avatar-01.jpg" alt="" class="user-avatar" runat="server" id="imgProfile"/>
                                               <br />
                                                <br />
                                                    <p>
                                                      Name: &nbsp&nbsp <asp:Label runat="server" ID="lblUserName" Text="User"></asp:Label></p>
                                                    <p>
                                                      UserID: &nbsp&nbsp <asp:Label runat="server" ID="lblUserID" Text="GR20182"></asp:Label></p>
                                                    <p>
                                                      Email ID : &nbsp&nbsp  <asp:Label runat="server" ID="lblEmailID" Text="User@gmail.com"></asp:Label></p>
                                               
                                            </div>
                                            <!-- end mail-view-header -->

                                            <div class="mail-body">
                                                <div class="mail-heading text-dark">Welcome TO LifeGold Ecommerce.</div>
                                                <br />
                                               
                                                <p class="mail-message">
                                                    Dear Sir/Madam &nbsp
                                                    <asp:Label ID="lblUserName1" runat="server" Text="user"></asp:Label>,
                                                <br />
                                                    <br />
                                                    I'd like to welcome you to <i><b>"LifeGold Ecommerce"</b></i>. &nbsp We are excited that you have become a part of our Family.<br />

                                                    <br />
                                                    <br />

                                                    A business that has the potential to turn your dreams into reality, as you build your business, you will establish lifelong friendships and develop support systems unparalleled in any other business.<br />
                                                    <br />
                                                    We pledge our best efforts to provide the levels of continuing support necessary to ensure that your business is a total success. You are in this business for yourself, not by yourself.<br />
                                                    <br />
                                                    We have developed an effective and proven progress product & plan to help you launch a profitable business of your own. With YOU in control, you determine your own level of commitment in order to adapt and benefit your lifestyle and personal goals. Profit share will be depend on company performance.	
													<br />
                                                    <br />
                                                    The rewards are tremendous for those who endure the efforts required to develop a stable organization, one from which you can potentially benefit from eternally. We are confident that you will receive gratification from your involvement with Company Name and we wish you every Success!<br />
                                                    <br />
                                                    Please note we are providing you an opportunity to earn money which is optional, your earnings will depend directly in the amount of efforts you put to develop your business team....<br />
                                                    <br />
                                                </p>
                                                                                        
                                                <br />
                                                Looking Forward from you
                                                <br /> <br />
                                                Have a Great Day !!!
                                                <br /><br />
                                                <span class="text-theme text-dark">Administrator <br /> LifeGold Ecommerce.</span>
                                            </div>

                                        </div>
                                        <!-- end mail view -->
                                    </div>
                                    <!-- end card-body -->
                                </div>
                                <!-- end card -->
                            </div>
                            <!-- end col -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

