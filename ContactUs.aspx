<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMaster.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<style>
.bg_contactus {
  /* The image used */
  background-image: url("images/Building.jpg");

  /* Full height */
  height: 100%; 

  /* Center and scale the image nicely */
  background-position: center;
  background-repeat: no-repeat;
  background-size: cover;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="page-title-main">
        <div class="container">
            <div class="clearfix">
                <div class="title-all text-center">
                    <ul class="breadcrumb">
                        <li class="breadcrumb-item"><a href="#">Home</a></li>
                        <li class="breadcrumb-item active">Contact</li>
                    </ul>
                    <h2>Contact</h2>
                </div>
            </div>
        </div>
    </div>

    <div class="contact-main">
        <div class="container">
            <div class="row">
                <div class="col-lg-6 col-12" >
				
                    <div class="contact-left-slide ">
                        <h3>We Are Here For You </h3>
						<div class="row box-contact-inner">	
							<image src="images/Building.jpg" alt="" style="height:20%; width:100%">
						</div>
                        <div class="row box-contact-inner">							 
                        <!--    <div class="col-sm-12">
                                <h4>Contact Support</h4>
                                <div class="contact-box">
                                    <i class="icon-c flaticon-file"></i>
                                    <div class="contact-description">
                                        <p>
                                            <i class="zmdi zmdi-google-maps"></i>​LIFEGOLD ECOM PVT. LTD.
											ADDRESS : <br /> GOLD WINGS  
											SECOND FLOOR
											COMERCIAL COMPLEX <br / >
											SINHGAD ROAD OPP. ROHAN KIRTIKA BUILDING
											ABOVE DARSHAN TYERS
											SINHGAD ROAD PUNE PIN - 411030<br / >
                                            
                                        </p>
                                        <p><i class="zmdi zmdi-phone"></i>(91) 123-4567</p>
                                    </div>
                                </div>
                            </div> -->
                            <div class="col-sm-12">
                            <!--    <h4>Contact Us</h4>
                                <div class="contact-box">
                                    <i class="icon-c flaticon-shop"></i>
                                    <div class="contact-description">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit.</p>
                                        <p>Nullam ac gravida lorem.</p>
                                    </div>
                                </div>  -->
                            </div>
                        <!--    <div class="col-sm-12">
                                <h4>Profession</h4>
                                <div class="contact-box">
                                    <i class="icon-c flaticon-christmas"></i>
                                    <div class="contact-description">
                                        <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas maximus nulla urna, at suscipit elit varius eget. Nullam imperdiet posuere nunc at molestie.</p>
                                    </div>
                                </div>
                            </div> -->
                        </div>
                    
					 
					</div>
                </div>
                <div class="col-lg-6 col-12">
                    <div class="box-contact-right-form">
                        <h3>Get In Touch</h3>
                        <div class="contact-right-form">
                            <div class="row clearfix">
                                <div class="form-group col-md-12">
                                    <asp:textbox runat="server" id="txtName" placeholder="Your Name" cssclass="form-control" type="text" required="true"></asp:textbox>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:textbox runat="server" id="txtEmail" placeholder="Email Address" cssclass="form-control" textmode="Email" required="true"></asp:textbox>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:textbox runat="server" id="txtMobile" placeholder="Mobile Number" cssclass="form-control" textmode="Number" maxlength="10" required="true"></asp:textbox>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:textbox runat="server" id="txtMessage" placeholder="Message" cssclass="form-control" textmode="MultiLine" type="text" rows="5" required="true"></asp:textbox>
                                </div>
                                <div class="form-group col-md-12">
                                    <asp:button id="btnSubmit" runat="server" cssclass=" " text="Submit" onclick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
					<div class="col-sm-12 form-inline">
					<div class="col-sm-7">
						<h4>Contact Support</h4>
						<div class="contact-box">
							<i class="icon-c flaticon-file"></i>
							<div class="contact-description">
								<p>
									<i class="zmdi zmdi-google-maps"></i>​LIFEGOLD ECOM PVT. LTD.
									ADDRESS : <br /> GOLD WINGS  
									SECOND FLOOR
									COMERCIAL COMPLEX <br / >
									SINHGAD ROAD OPP. ROHAN KIRTIKA BUILDING
									ABOVE DARSHAN TYERS
									SINHGAD ROAD PUNE PIN - 411030<br / >
									
								</p>
								<p><i class="zmdi zmdi-phone"></i>(91) 123-4567</p>
							</div>
						</div>
                    </div>
					<div class="col-sm-5">
						<h4>Bank Details</h4>
						<div class="contact-box">
							<i class="icon-c flaticon-file"></i>
							<div class="contact-description">
								<p>
									<i class="zmdi zmdi-mall"></i>​ 
										Bank Name : <br /> Axis Bank <br />
										Account No. : 918020070735431 <br />
										IFSC : UTIB0000B2B <br />
										Address : Sinhgad road, Pune (MH). 411030 <br /> 
								</p>
								<p><i class="zmdi zmdi-phone"></i>(91) 123-4567</p>
							</div>
						</div>
                    </div>
					</div>
                </div>
            </div>
        <br />
			<div class="row">
                <script src='https://maps.googleapis.com/maps/api/js?v=3.exp&key=AIzaSyAfZ-pWqx7XTJMUwEcgkinVsfY2OG2e1Hk'></script><div style='overflow:hidden;height:400px;width:100%;'><div id='gmap_canvas' style='height:400px;width:100%;'></div><style>#gmap_canvas img{max-width:none!important;background:none!important}</style></div> <a href='https://mapswebsite.net/'>google maps for my website</a> <script type='text/javascript' src='https://embedmaps.com/google-maps-authorization/script.js?id=83ac919e08b10892ebd39711eb39a08be2afde8a'></script><script type='text/javascript'>function init_map(){var myOptions = {zoom:12,center:new google.maps.LatLng(18.4922073,73.83273170000007),mapTypeId: google.maps.MapTypeId.ROADMAP};map = new google.maps.Map(document.getElementById('gmap_canvas'), myOptions);marker = new google.maps.Marker({map: map,position: new google.maps.LatLng(18.4922073,73.83273170000007)});infowindow = new google.maps.InfoWindow({content:'<strong>LIFEGOLD ECOM PVT. LTD.</strong><br>GOLD WINGS SECOND FLOOR COMERCIAL COMPLEX  SINHGAD ROAD OPP. ROHAN KIRTIKA BUILDING ABOVE DARSHAN TYERS SINHGAD ROAD<br>411030 PUNE <br>'});google.maps.event.addListener(marker, 'click', function(){infowindow.open(map,marker);});infowindow.open(map,marker);}google.maps.event.addDomListener(window, 'load', init_map);</script><br /><br />
            </div>
		</div>
    </div>
</asp:Content>

