<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Basic -->
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!-- Mobile Metas -->
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <!-- Site Metas -->
    <title>LifeGoldEcom </title>
    <meta name="keywords" content="LifeGoldEcom, LifeGold, Ecom, Life, Gold, Life Gold" />
    <meta name="description" content="LifeGoldEcom, LifeGold, Ecom, Life, Gold, Life Gold" />
    <meta name="author" content="LifeGoldEcom, LifeGold, Ecom, Life, Gold, Life Gold" />

    <!-- Site Icons -->
    <link rel="shortcut icon" href="images/android-icon-48x48.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="images/apple-touch-icon.html" />

    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="css/bootstrap.min.css" />

    <!-- Revolution Loaling Fonts and Icons  -->
    <link rel="stylesheet" href="js/revolution/fonts/pe-icon-7-stroke/css/pe-icon-7-stroke.css" />
    <!-- Revolution style Sheets  -->
    <link rel="stylesheet" href="js/revolution/css/settings.css" />
    <link rel="stylesheet" href="js/revolution/css/layers.css" />
    <link rel="stylesheet" href="js/revolution/css/navigation.css" />

    <!-- Site CSS -->
    <link rel="stylesheet" href="css/style.css" />

    <!-- Responsive CSS -->
    <link rel="stylesheet" href="css/responsive.css" />

    <!-- Modernizer -->
    <script src="js/modernizer.js"></script>

    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
	<style>
		.ace-responsive-menu > li > a {     
    		margin: 20px 0px;
			padding: 5px 15px;
		}
		.ace-responsive-menu > ul > a {     
    		 
			padding-top: 22px;
		}
	</style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
		<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></cc1:ToolkitScriptManager>
            <!-- Start Preloader  -->
            <!-- Start Top Bar -->
			<!--
            <div class="top-bar d-none d-md-block">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-8">
                            <div class="top-box-icon clearfix">
                                <ul>
                                    <li><i class="zmdi zmdi-email-open"></i><a href="mailto:info@progkiconsulting.com">info@progkiconsulting.com</a></li>
                                    <li><i class="zmdi zmdi-phone-in-talk"></i><a href="#">(855) 999-5597</a></li>
                                    <li><i class="zmdi zmdi-google-maps"></i>
                                        <p>9090 Avenue of the Moon, New York, NY 2218 US.</p>
                                    </li>
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4">
                            <div class="top-box-icon social">
                                <a href="#"><i class="zmdi zmdi-facebook"></i></a>
                                <a href="#"><i class="zmdi zmdi-twitter"></i></a>
                                <a href="#"><i class="zmdi zmdi-google-plus"></i></a>
                                <a href="#"><i class="zmdi zmdi-pinterest"></i></a>
                                <a href="#"><i class="zmdi zmdi-skype"></i></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
			-->
            <!-- End Top Bar -->

            <!-- Start Header -->
            <div class="header top-head">
                <!-- Start Header Area -->
                <div id="nav-bar" class="header-area">
                    <div class="container">
                        <div class="head-nav-bar clearfix">
                            <div class="float-left">
                                <div class="logo">
                                    <a href="index.aspx">
                                        <img src="images/logo.png" alt="" style="width:230px; height:80px;" /></a>
                                </div>
                            </div>
                            <div class="float-right">
                                <div class="float-right v-align">
                                    <%--<span class="open-search sb-icon-search">
                                        <a href="#search"><i class="zmdi zmdi-search"></i></a>
                                    </span>--%>
                                </div>
                                <%--<div id="search">
                                    <button type="button" class="close"><i class="zmdi zmdi-close"></i></button>
                                    <div>
                                        <input type="search" value="" placeholder="type keyword(s) here" />
                                        <button type="submit" class="btn btn-primary">Search</button>
                                    </div>
                                </div>--%>
                                <div class="nav-box-right">
                                    <nav>
                                        <div class="menu-toggle">
                                            <h3>Menu</h3>
                                            <button class="" type="button" id="menu-btn">
                                                <span class="icon-bar"></span>
                                                <span class="icon-bar"></span>
                                                <span class="icon-bar"></span>
                                            </button>
                                        </div>
                                        <ul id="respMenu" class="ace-responsive-menu" data-menu-style="horizontal" style="padding-top:22px !important; ">
                                            <li class=""><a href="index.aspx"><span class="title">Home</span></a></li>
                                            <li class=""><a href="Legal.aspx"><span class="title">Legal</span></a></li>
											<li class=""><a href="Gallery.aspx"><span class="title">Gallery</span></a></li>
											
                                            <%--<li class="active"><a href="index.html"><span class="title">Home</span></a>
                                                <ul>
                                                    <li><a href="index.html">Home 1</a></li>
                                                    <li><a href="index-2.html">Home 2</a></li>
                                                    <li><a href="index-3.html">Home 3</a></li>
                                                </ul>
                                            </li>
                                            <li><a href="about.html"><span class="title">About Us</span></a></li>
                                            <li><a href="#"><span class="title">Services</span></a>
                                                <ul>
                                                    <li><a href="services.html">Services</a></li>
                                                    <li><a href="single-service.html">Single Services</a></li>
                                                </ul>
                                            </li>
                                            <li><a href="#"><span class="title">Features</span></a>
                                                <ul>
                                                    <li><a href="project-1.html">Project 4 Column</a></li>
                                                    <li><a href="project-2.html">Project 3 Column</a></li>
                                                    <li><a href="project-3.html">Project 2 Column</a></li>
                                                    <li><a href="single-project.html">Single Project</a></li>
                                                    <li><a href="gallery.html">Gallery</a></li>
                                                    <li><a href="team.html">Team</a></li>
                                                    <li><a href="review.html">Review</a></li>
                                                    <li><a href="faq.html">FAQ</a></li>
                                                </ul>
                                            </li>
                                            <li><a href="#"><span class="title">Blog</span></a>
                                                <ul>
                                                    <li><a href="blog-grid-2.html">Blog 2 grid</a></li>
                                                    <li><a href="blog-grid-3.html">Blog 3 column</a></li>
                                                    <li><a href="blog-side.html">Blog Sidebar</a></li>
                                                    <li><a href="blog-details.html">Blog Details</a></li>
                                                </ul>
                                            </li>--%>
                                            <li class="last"><a href="contactus.aspx"><span class="title">Contact Us</span></a></li>
                                            <li class="last"><a href="Login.aspx"><span class="title">Login</span></a></li>
                                            <li class="last"><a href="Register.aspx"><span class="title">Register</span></a></li>
                                        </ul>
                                    </nav>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End Header Area -->

                <!-- Start Banner Slider -->
                <div class="banner-slider" style="padding-top: 9%;">
                    <div class="slider-section">
                        <div id="rev_slider_486_1_wrapper" class="rev_slider_wrapper fullscreen-container" data-alias="news-gallery36" data-source="gallery" style="margin: 0px auto; background-color: #ffffff; padding: 0px; margin-top: 0px; margin-bottom: 0px;">
                            <!-- Start Revolution Slider -->
                            <div id="rev_slider_486_1" class="rev_slider fullscreenbanner" style="display: none;" data-version="5.3.0.2">
                                 <ul>
                                    <!-- Slide 1 -->
                                    <li data-transition="random">
                                        <img src="images/Slide1.jpg" alt="" data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat" class="rev-slidebg" />
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-798-layer-1" data-x="left" data-hoffset="" data-y="center" data-voffset="-50" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-start="1000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 5; white-space: normal; font-size: 16px; line-height: 24px; margin-bottom: 20px; font-weight: normal;">
                                            <div class="smooth-textbox">
                                            </div>
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-899-layer-4" data-x="left" data-hoffset="" data-y="center" data-voffset="70" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="z:0;rX:0deg;rY:0;rZ:0;sX:2;sY:2;skX:0;skY:0;opacity:0;s:1000;e:Power2.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:0px;y:0px;s:inherit;e:inherit;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 8; white-space: normal; font-size: 20px; line-height: 30px; font-weight: 400;">
                                            <div class="smooth-textbox">
                                            </div>
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-998-layer-3" data-x="left" data-hoffset="" data-y="center" data-voffset="180" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="y:[100%];z:0;rX:0deg;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0;s:2000;e:Power4.easeInOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 7; white-space: nowrap; font-size: 16px; line-height: 50px; font-weight: 500;">
                                            <div class="slidebtns smooth-textbox">
                                            </div>
                                        </div>
                                    </li>
                                    <!-- Slide 2 -->
                                    <li data-transition="random">
                                        <img src="images/Slide2.jpg" alt="" data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat" class="rev-slidebg" />
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-198-layer-1" data-x="right" data-hoffset="" data-y="center" data-voffset="-50" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-start="1000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 5; white-space: normal; font-size: 16px; line-height: 24px; margin-bottom: 20px; font-weight: normal;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-299-layer-4" data-x="right" data-hoffset="" data-y="center" data-voffset="70" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="z:0;rX:0deg;rY:0;rZ:0;sX:2;sY:2;skX:0;skY:0;opacity:0;s:1000;e:Power2.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:0px;y:0px;s:inherit;e:inherit;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 8; white-space: normal; font-size: 20px; line-height: 30px; font-weight: 400;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-398-layer-3" data-x="right" data-hoffset="" data-y="center" data-voffset="180" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="y:[100%];z:0;rX:0deg;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0;s:2000;e:Power4.easeInOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 7; white-space: nowrap; font-size: 16px; line-height: 50px; font-weight: 500;">
                                        </div>
                                    </li>
                                    <!-- Slide 3 -->
                                    <li data-transition="random">
                                        <img src="images/Slide3.jpg" alt="" data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat" class="rev-slidebg" />
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-798-layer-13" data-x="left" data-hoffset="" data-y="center" data-voffset="-50" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-start="1000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 5; white-space: normal; font-size: 16px; line-height: 24px; margin-bottom: 20px; font-weight: normal;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-899-layer-43" data-x="left" data-hoffset="" data-y="center" data-voffset="70" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="z:0;rX:0deg;rY:0;rZ:0;sX:2;sY:2;skX:0;skY:0;opacity:0;s:1000;e:Power2.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:0px;y:0px;s:inherit;e:inherit;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 8; white-space: normal; font-size: 20px; line-height: 30px; font-weight: 400;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-998-layer-33" data-x="left" data-hoffset="" data-y="center" data-voffset="180" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="y:[100%];z:0;rX:0deg;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0;s:2000;e:Power4.easeInOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 7; white-space: nowrap; font-size: 16px; line-height: 50px; font-weight: 500;">
                                        </div>
                                    </li>
                                    <!-- Slide 4 -->
                                    <li data-transition="random">
                                        <img src="images/Slide4.jpg" alt="" data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat" class="rev-slidebg" />
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-198-layer-14" data-x="right" data-hoffset="" data-y="center" data-voffset="-50" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-start="1000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 5; white-space: normal; font-size: 16px; line-height: 24px; margin-bottom: 20px; font-weight: normal;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-299-layer-44" data-x="right" data-hoffset="" data-y="center" data-voffset="70" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="z:0;rX:0deg;rY:0;rZ:0;sX:2;sY:2;skX:0;skY:0;opacity:0;s:1000;e:Power2.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:0px;y:0px;s:inherit;e:inherit;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 8; white-space: normal; font-size: 20px; line-height: 30px; font-weight: 400;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-398-layer-34" data-x="right" data-hoffset="" data-y="center" data-voffset="180" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="y:[100%];z:0;rX:0deg;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0;s:2000;e:Power4.easeInOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 7; white-space: nowrap; font-size: 16px; line-height: 50px; font-weight: 500;">
                                        </div>
                                    </li>
                                    <!-- Slide 5 -->
                                    <li data-transition="random">
                                        <img src="images/Slide5.jpg" alt="" data-bgposition="center center" data-bgfit="cover" data-bgrepeat="no-repeat" class="rev-slidebg" />
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-798-layer-15" data-x="left" data-hoffset="" data-y="center" data-voffset="-50" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="x:[-175%];y:0px;z:0;rX:0;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0.01;s:3000;e:Power3.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:[100%];y:0;s:inherit;e:inherit;" data-start="1000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 5; white-space: normal; font-size: 16px; line-height: 24px; margin-bottom: 20px; font-weight: normal;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-899-layer-45" data-x="left" data-hoffset="" data-y="center" data-voffset="70" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="z:0;rX:0deg;rY:0;rZ:0;sX:2;sY:2;skX:0;skY:0;opacity:0;s:1000;e:Power2.easeOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-mask_in="x:0px;y:0px;s:inherit;e:inherit;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 8; white-space: normal; font-size: 20px; line-height: 30px; font-weight: 400;">
                                        </div>
                                        <div class="tp-caption NotGeneric-Title   tp-resizeme" id="slide-998-layer-35" data-x="left" data-hoffset="" data-y="center" data-voffset="180" data-width="['auto','auto','auto','auto']" data-height="['auto','auto','auto','auto']" data-transform_idle="o:1;" data-transform_in="y:[100%];z:0;rX:0deg;rY:0;rZ:0;sX:1;sY:1;skX:0;skY:0;opacity:0;s:2000;e:Power4.easeInOut;" data-transform_out="s:1000;e:Power3.easeInOut;s:1000;e:Power3.easeInOut;" data-start="3000" data-splitin="none" data-splitout="none" data-responsive_offset="on" style="z-index: 7; white-space: nowrap; font-size: 16px; line-height: 50px; font-weight: 500;">
                                        </div>
                                    </li>

                                </ul>
                                <div class="tp-bannertimer" style="height: 5px; background-color: rgba(0, 0, 0, .8);"></div>
                            </div>
                        </div>
                        <!-- End Revolution Slider -->
                    </div>
                </div>
                <!-- End Banner Slider -->
            </div>
            <!-- End Header -->

			<asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
			<cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
				PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground" CancelControlID="btnHide">
			</cc1:ModalPopupExtender>
					
			<asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup modal-dialog modal-lg modal-sm" Style="display: none; width:30%">
				<div class="modal-dialog modal-lg  modal-sm " >
					<div class="modal-content">
						<div class="modal-header">
							<asp:Label runat="server" ID="lblPopupHeader" CssClass="form-control"></asp:Label>
						</div>
						<div class="modal-body text-center">

							<p class="text-dark">
								<asp:Label runat="server" ID="lblPopupBody" CssClass="form-control"></asp:Label>
								<asp:Image runat="server" ID="imgPopup" CssClass="img img-thumbnail" />
							</p>

							<br />
							<asp:Button ID="btnHide" runat="server" Text="Close" CssClass="btn btn-round btn-theme" />
						</div>
					</div>
				</div>
			</asp:Panel>
	
            <!-- Start Welcome Strip -->
            <div class="wel-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="wel-info-box">
                            <h3>Welcome To Our Lifegold Ecom Pvt. Ltd.</h3>
                            
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Welcome Strip -->

            <!-- Start How Help -->
            <div class="how-help-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2> WHAT WE DO? </h2>
                            <p>We are into Digital marketing, E-commerce IT- services, Mining projects, and organics framings’.</p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <div class="helpbox aos-item" data-aos="fade-up" data-aos-duration="1000">
                                <div class="icon-help">
                                    <img class="hover-icon" src="images/employees-h.png" alt="" />
                                    <img class="hover-icon-h" src="images/employees-h.png" alt="" />
                                </div>
                                <div class="helpdit">
                                    <h3> OUR VISION </h3>
                                    <p>Vision of LGC Flow is to become a leading infrastructure solution provider to multiple various industries by leveraging our well- researched technology developed under careful supervision of reputed and tenured domain experts. <br /> <br /> <br /><br /> </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-md-6 col-sm-6">
                            <div class="helpbox aos-item" data-aos="fade-up" data-aos-duration="1500">
                                <div class="icon-help">
                                    <img class="hover-icon" src="images/hierarchical-structure-h.png" alt="" />
                                    <img class="hover-icon-h" src="images/hierarchical-structure-h.png" alt="" />
                                </div>
                                <div class="helpdit">
                                    <h3> OUR MISSION </h3>
                                    <p> Our mission is to empower modern age entrepreneurs to lead a profitable business and peaceful life. Our Business which focuses on maintaining a perfect work-life balance. Our focus is on helping millions of young entrepreneurs to turn their spark in a flame and use it for enlightenment of a society as a whole, and Create multiples jobs opportunity to society. Create multiples entrepreneurs in society.’  </p>
                                </div>
                            </div>
                        </div>
					<!--
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="helpbox aos-item" data-aos="fade-up" data-aos-duration="2000">
                                <div class="icon-help">
                                    <img class="hover-icon" src="images/networking.png" alt="" />
                                    <img class="hover-icon-h" src="images/networking-h.png" alt="" />
                                </div>
                                <div class="helpdit">
                                    <h3>Logistics Consulting </h3>
                                    <p>Lorem Ipsum has been the industry’s standard dummy text ever since the 1500s </p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-md-3 col-sm-6">
                            <div class="helpbox aos-item" data-aos="fade-up" data-aos-duration="2500">
                                <div class="icon-help">
                                    <img class="hover-icon" src="images/stats.png" alt="" />
                                    <img class="hover-icon-h" src="images/stats-h.png" alt="" />
                                </div>
                                <div class="helpdit">
                                    <h3>Analysis of Business </h3>
                                    <p>Lorem Ipsum has been the industry’s standard dummy text ever since the 1500s </p>
                                </div>
                            </div>
                        </div>
                    -->
					</div>
                </div>
            </div>
            <!-- End How Help -->

            <!-- Start About Us -->
            <div class="about-main left-color-a">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-6 col-md-6">
                            <div class="left-about-b aos-item" data-aos="fade-up">
                                <div class="all-title">
                                    <h2>See Facts In Numbers</h2>
                                    <span class="all-title-bar"></span>
                                </div>
                                <h4>Customers are grateful,
                                    <br />
                                    for our services.</h4>
                                <p>  </p>
                                <ul>
                                    <li>
                                        <h5 class="counter">555</h5>
                                        <span>Happy clients</span>
                                    </li>
                                    <li>
                                        <h5 class="counter">77</h5>
                                        <span>Confident consultants</span>
                                    </li>
                                    <li>
                                        <h5 class="counter">99</h5>
                                        <span>Awards winning</span>
                                    </li>
                                    <li>
                                        <h5 class="counter">888</h5>
                                        <span>Project completed</span>
                                    </li>
                                </ul>

                                <a class="button-wayra-b" href="#">Learn more</a>
                            </div>
                        </div>
                        <div class="col-sm-6 col-md-6">
                            <div class="right-about-b aos-item" data-aos="fade-up">
                                <div class="all-title">
                                    <h2>SOMETHING ABOUT US</h2>
                                    <span class="all-title-bar"></span>
                                </div>
                                <p>Lifegold Ecom Pvt. Ltd. is one of the best direct affiliate programs selling companies founded In Maharashtra in India. Lifegold Ecom Pvt. Ltd. deals in Holiday Vouchers, Domestic Tour, International Tour, and Online Shopping Portal. Company offers a high quality life changing products and an unparalleled business opportunity that empowers people to achieve their dreams, financial freedom and rewards by helping others. </p>
								<p>
								We also in Digital Marketing, IT- services,Organic framings, stone Mining and E-commerce business. </p>
								<p>  
								We also work on government projects start-up India, Make In India and Digital India projects.
								</p>                                 
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <!-- End About Us -->

            <!-- Start Services -->
            <div class="services-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>OUR SPECIALIZATION</h2>
                            <p>Within a short span of time we have established a name for our services that are in cognizance to all the authorized standard quality of products. Our premium services have made us won a wide clientele across the globe. We work on a customer centric approach that helps us achieve total customer satisfaction and retain them with us.</p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6 col-sm-6">
                            <div class="services-box aos-item" data-aos="fade-up" data-aos-duration="1000">
                                <div class="icon-ser ">
                                    <img src="images/analytics.png" alt="" />
                                </div>
                                <div class="services-dit">
                                    <h4> Company Overview</h4>
                                    <p> We present an end-to-end approach in entrepreneurship by introducing simple reward system for our distributor's Ecommerce, digital products , IT solutions holidays and daily use products. We understand optimizing opportunity value is as important as seizing the opportunity.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6 col-sm-6">
                            <div class="services-box aos-item" data-aos="fade-up" data-aos-duration="1200">
                                <div class="icon-ser">
                                    <img src="images/applications.png" alt="" />
                                </div>
                                <div class="services-dit">
                                    <h4>	Our Services</h4>
                                    <p>We are into Digital marketing, E-commerce IT- services, Mining projects, and organics framings’. Our company provides an extensive (wide) range of services to the customers, with global markets so here is a quick summary of what we provide. We help people in putting life to their ideas. If you have been in any dilemma (confusion) to how to mould your ideas, we are the best solution. We are readily available with our toolkit at your disposal to give shape to your ideas and add a spark to your ideas as well as to your life.</p>
                                </div>
                            </div>
                        </div> 
                    </div>
                </div>
            </div>
            <!-- End Services -->

            <!-- Start Portfolio -->
				<div class="portfolio-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>Our Portfolio</h2>
                            <p></p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                        <!--     <div class="project-menu text-center">
                                <span data-filter="*" class="active">ALL</span>
                                <span data-filter=".consulting">Consulting</span>
                                <span data-filter=".finance">Finance</span>
                                <span data-filter=".marketing">Marketing</span>
                            </div>    -->
                            <div class="row project-gird">
                                <div class="col-lg-4 col-md-6 finance consulting" data-category="consulting">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/01.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/01.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                            <h4> </h4>
                                        <!--    <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p> -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/02.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/02.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--    <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>  -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 finance consulting" data-category="finance">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/03.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/03.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!-- <h4>Ecommerce</h4>
                                          <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p> -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 finance consulting" data-category="consulting">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/04.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/04.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--     <h4>Consulting Business</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>   -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 finance consulting" data-category="finance">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/05.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/05.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--    <h4>Finance</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>   -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/06.jpeg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/06.jpeg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--     <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>    -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/07.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/07.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
											<h4>Mining Project</h4>
                                        <!--  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>    -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/08.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/08.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--     <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>   -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/09.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/09.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <h4>Mining Project</h4>
                                        <!--  <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>    -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/10.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/10.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--     <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>    -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/11.jpg" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/11.jpg" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--    <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>   -->
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-6 marketing consulting" data-category="marketing">
                                    <div class="hover-effect">
                                        <img class="thumb_gallery" src="images/12.png" alt="" />
                                        <div class="overlay-gallery"></div>
                                        <div class="hover-text">
                                            <div class="up-icon">
                                                <a href="images/12.png" data-fancybox="images" title="Consulting">
                                                    <i class="zmdi zmdi-camera-alt"></i>
                                                </a>
                                            </div>
                                        <!--     <h4>Marketing</h4>
                                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. </p>   -->
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Portfolio -->

            <!-- Start Our Team -->
        <%--    <div class="our-team-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>Meet Our Team</h2>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="1000">
                                <div class="team-img">
                                    <a href="#team-1" class="membername">
                                        <img src="images/img-01.jpg" alt="" />
                                        <div class="filter-title">
                                            <h3>Clark Roberts</h3>
                                            <p>Founder & CEO</p>
                                        </div>
                                    </a>
                                    <div class="filter-social">
                                        <ul>
                                            <li><a href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-google-plus"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-pinterest"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-skype"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="display: none">
                            <div class="teamdetail" id="team-1">
                                <div class="innerImg" style="background-image: url('images/team-big.jpg')"></div>
                                <div class="innertext ">
                                    <h3>Clark Roberts</h3>
                                    <p class="subtitle">Founder & CEO</p>
                                    <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce tempus odio erat, eget feugiat libero sollicitudin at.  Fusce tempus odio erat, eget feugiat libero sollicitudin at.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="2000">
                                <div class="team-img">
                                    <a href="#team-2" class="membername">
                                        <img src="images/img-01.jpg" alt="" />
                                        <div class="filter-title">
                                            <h3>Dave John</h3>
                                            <p>Finance & Commerce</p>
                                        </div>
                                    </a>
                                    <div class="filter-social">
                                        <ul>
                                            <li><a href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-google-plus"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-pinterest"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-skype"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="display: none">
                            <div class="teamdetail" id="team-2">
                                <div class="innerImg" style="background-image: url('images/team-big.jpg')"></div>
                                <div class="innertext ">
                                    <h3>Clark Roberts</h3>
                                    <p class="subtitle">Founder & CEO</p>
                                    <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce tempus odio erat, eget feugiat libero sollicitudin at.  Fusce tempus odio erat, eget feugiat libero sollicitudin at.</p>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <div class="our-team-box aos-item" data-aos="fade-up" data-aos-duration="3000">
                                <div class="team-img">
                                    <a href="#team-3" class="membername">
                                        <img src="images/img-01.jpg" alt="" />
                                        <div class="filter-title">
                                            <h3>Michael King</h3>
                                            <p>Team Leader</p>
                                        </div>
                                    </a>
                                    <div class="filter-social">
                                        <ul>
                                            <li><a href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-google-plus"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-pinterest"></i></a></li>
                                            <li><a href="#"><i class="zmdi zmdi-skype"></i></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div style="display: none">
                            <div class="teamdetail" id="team-3">
                                <div class="innerImg" style="background-image: url('images/team-big.jpg')"></div>
                                <div class="innertext ">
                                    <h3>Clark Roberts</h3>
                                    <p class="subtitle">Founder & CEO</p>
                                    <p class="desc">Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce tempus odio erat, eget feugiat libero sollicitudin at.  Fusce tempus odio erat, eget feugiat libero sollicitudin at.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Our Team -->

            <!-- Start Testimonials -->
            <div class="testimonials-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>Our Testimonials</h2>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="slider-testimonial">
                            <div id="testimonial-slider" class="owl-carousel">
                                <div class="testimonial">
                                    <i class="icon left">“</i>
                                    <p class="description">
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus ac condimentum mi, vitae iaculis ante. Suspendisse viverra urna quis diam sodales, convallis auctor nibh.
                                    </p>
                                    <i class="icon right">”</i>
                                    <div class="pic">
                                        <img src="images/img-1.jpg" alt="" />
                                    </div>
                                    <h3 class="testimonial-title">williamson</h3>
                                    <span class="post">Web Developer</span>
                                </div>

                                <div class="testimonial">
                                    <i class="icon">“</i>
                                    <p class="description">
                                        Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vivamus ac condimentum mi, vitae iaculis ante. Suspendisse viverra urna quis diam sodales, convallis auctor nibh.
                                    </p>
                                    <i class="icon">”</i>
                                    <div class="pic">
                                        <img src="images/img-2.jpg" alt="" />
                                    </div>
                                    <h3 class="testimonial-title">kristiana</h3>
                                    <span class="post">Web Designer</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Testimonials -->

            <!-- Start latest news -->
            <div class="latest-news-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>Latest News</h2>
                            <p>Lorem Ipsum is simply dummy text of the printing and typesetting industry.</p>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <article class="post aos-item" data-aos="fade-up" data-aos-duration="1000">
                                <div class="post-thumb">
                                    <img src="images/new-01.jpg" alt="" />
                                    <div class="date-box">
                                        <b>30</b>
                                        Jan
                                    </div>
                                </div>
                                <div class="post-description">
                                    <h4>Business Consulting</h4>
                                    <ul>
                                        <li>Post by Admin</li>
                                        <li>|</li>
                                        <li>30 January 2018</li>
                                    </ul>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam laoreet odio id lectus tristique luctus. Morbi id sodales orci. Nunc et nunc sodales, convallis nibh vitae, dictum elit.</p>
                                    <div class="list-inline">
                                        <ul>
                                            <li>880 Like</li>
                                            <li>402 Share</li>
                                            <li>90 Comments</li>
                                        </ul>
                                    </div>
                                </div>
                            </article>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <article class="post aos-item" data-aos="fade-up" data-aos-duration="2000">
                                <div class="post-thumb ">
                                    <img src="images/new-02.jpg" alt="" />
                                    <div class="date-box">
                                        <b>10</b>
                                        Jan
                                    </div>
                                </div>
                                <div class="post-description">
                                    <h4>Business Consulting</h4>
                                    <ul>
                                        <li>Post by Admin</li>
                                        <li>|</li>
                                        <li>10 January 2018</li>
                                    </ul>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam laoreet odio id lectus tristique luctus. Morbi id sodales orci. Nunc et nunc sodales, convallis nibh vitae, dictum elit.</p>
                                    <div class="list-inline">
                                        <ul>
                                            <li>880 Like</li>
                                            <li>402 Share</li>
                                            <li>90 Comments</li>
                                        </ul>
                                    </div>
                                </div>
                            </article>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-6">
                            <article class="post aos-item" data-aos="fade-up" data-aos-duration="3000">
                                <div class="post-thumb ">
                                    <img src="images/new-03.jpg" alt="" />
                                    <div class="date-box">
                                        <b>20</b>
                                        Jan
                                    </div>
                                </div>
                                <div class="post-description">
                                    <h4>Business Consulting</h4>
                                    <ul>
                                        <li>Post by Admin</li>
                                        <li>|</li>
                                        <li>20 January 2018</li>
                                    </ul>
                                    <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam laoreet odio id lectus tristique luctus. Morbi id sodales orci. Nunc et nunc sodales, convallis nibh vitae, dictum elit.</p>
                                    <div class="list-inline">
                                        <ul>
                                            <li>880 Like</li>
                                            <li>402 Share</li>
                                            <li>90 Comments</li>
                                        </ul>
                                    </div>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>

            <div class="our-partner-main">
                <div class="container">
                    <div class="clearfix">
                        <div class="all-title text-center">
                            <h2>Our Partner</h2>
                            <span class="all-title-bar"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div id="our-partner-slider" class="owl-carousel">
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/01.png" alt="" />
                            </div>
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/02.png" alt="" />
                            </div>
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/03.png" alt="" />
                            </div>
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/04.png" alt="" />
                            </div>
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/05.png" alt="" />
                            </div>
                            <div class="partner-img" data-toggle="tooltip" data-placement="top" title="Our Partner">
                                <img src="images/06.png" alt="" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Our Partner -->
		--%>
            <!-- Start Time -->
            <div class="top-box-time">
                <i class="zmdi zmdi-time"></i>
                <p>Mon - Sat 9.30 - 6.30. Sunday CLOSED</p>
            </div>
            <!-- End Our Partner -->

            <!-- Start Time -->
            <div class="footer-main">
                <div class="container">
                    <div class="row">
                        <div class="col-lg-3 col-sm-6">
                            <div class="widget-footer">
                                <div class="footer-logo">
                                    <img src="images/footer-logo.png" alt="" style="background-color: white; width:230px; height:100px; " />
                                </div>
                                <div class="footer-description">
                                    <p>Lifegold Ecom Pvt. Ltd. is one of the best direct affiliate programs selling companies founded In Maharashtra in India. Lifegold Ecom Pvt. Ltd. deals in Holiday Vouchers, Domestic Tour, International Tour, and Online Shopping Portal.</p>
                                </div>
                                <div class="footer-socials">
                                    <ul>
                                        <li><a href="#"><i class="zmdi zmdi-facebook"></i></a></li>
                                        <li><a href="#"><i class="zmdi zmdi-twitter"></i></a></li>
                                        <li><a href="#"><i class="zmdi zmdi-google-plus"></i></a></li>
                                        <li><a href="#"><i class="zmdi zmdi-pinterest"></i></a></li>
                                        <li><a href="#"><i class="zmdi zmdi-skype"></i></a></li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm-6">
                            <div class="widget-footer-nav">
                                <h3>Important Link</h3>
                                <ul>
                                    <li><a href="index.aspx">Home</a></li>
                                    <li><a href="Legal.aspx">Legal</a></li>
                                    <li><a href="Gallery.aspx">Gallery</a></li>
                                    <li><a href="ContactUs.aspx">Contact Us</a></li>
                                    <li><a href="Login.aspx">Login</a></li>
                                    <li><a href="Register.aspx">Register</a></li>                                    
                                </ul>
                            </div>
                        </div>
                        <div class="col-lg-3 col-sm-12">
							<div class="widget-footer">
                                <div class="widget-footer-nav text-white">
                                    <h3 class="text-white">Contact Us</h3>
                                </div>
                                <div class="footer-description">
                                    <p>LIFEGOLD ECOM PVT. LTD.
										ADDRESS : GOLD WINGS
										SECOND FLOOR
										COMERCIAL COMPLEX <br />
										SINHGAD ROAD OPP. ROHAN KIRTIKA BUILDING
										ABOVE DARSHAN TYERS <br />
										SINHGAD ROAD PUNE PIN - 411030
									</p>
                                </div>                                 
                            </div>                            
                        </div>
						<div class="col-lg-3 col-sm-6">
                            <div class="widget-footer"> 
                                <div class="widget-footer-nav text-white">
                                    <h3 class="text-white">Bank Details</h3>
                                </div>
                                <div class="footer-description">
                                    <p>
										Bank Name : Axis Bank <br />
										Account No. : 918020070735431 <br />
										IFSC : UTIB0000B2B <br />
										Address : Sinhgad road, Pune (MH). 411030 <br />
									</p>
                                </div> 
                            </div>
                        </div>
                    </div>
                </div>
                <div class="copyright-main">
                    <div class="container">
                        <div class="copyright">
                            <p><a href="#" target="_blank" rel="noopener noreferrer"> LifeGold Ecom Pvt. Ltd. </a> &copy; 2019. All Rights Reserved.</p>
                        </div>
                    </div>
                </div>
            </div>
            <!-- End Footer -->

            <a href="#" id="scroll-to-top" title="Scroll to top"><i class="zmdi zmdi-chevron-up"></i></a>
        </div>
    </form>
    <!-- All Js Files -->
    <script src="js/all.js"></script>

    <!-- Revolution Js Files -->
    <script src="js/revolution/js/jquery.themepunch.tools.min.js"></script>
    <script src="js/revolution/js/jquery.themepunch.revolution.min.js"></script>
    <script src="js/slider-setting.js"></script>
    <!-- Slider Revolution 5.0 Extensions -->
    <script src="js/revolution/js/extensions/revolution.extension.actions.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.carousel.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.kenburn.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.layeranimation.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.migration.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.navigation.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.parallax.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.slideanims.min.js"></script>
    <script src="js/revolution/js/extensions/revolution.extension.video.min.js"></script>
    <!-- End Revolution Slider Extensions -->
    <!-- All Plugins -->
    <script src="js/custom.js"></script>
</body>
</html>
