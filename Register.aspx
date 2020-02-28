<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="description" content=" " />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>LifeGold | Register</title>

	<!-- Site Icons -->
    <link rel="shortcut icon" href="images/android-icon-48x48.png" type="image/x-icon" />
    <link rel="apple-touch-icon" href="images/apple-touch-icon.html" /> 
	
    <%--<link rel="apple-touch-icon" sizes="180x180" href="portal/img/favicon/apple-touch-icon.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="portal/img/favicon/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="portal/img/favicon/favicon-16x16.png" />
    <link rel="manifest" href="portal/img/favicon/manifest.json" />--%>
    <link rel="mask-icon" href="portal/img/favicon/safari-pinned-tab.svg" color="#5bbad5" />
    <meta name="theme-color" content="#ffffff" />

    <!-- fonts -->
    <link rel="stylesheet" href="portal/fonts/md-fonts/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="portal/fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />

    <!-- animate css -->
    <link rel="stylesheet" href="portal/libs/animate.css/animate.min.css" />

    <!-- jquery-loading -->
    <link rel="stylesheet" href="portal/libs/jquery-loading/dist/jquery.loading.min.css" />
    <!-- octadmin main style -->
    <link id="pageStyle" rel="stylesheet" href="portal/css/style.css" />

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
    <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
    <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
	
<![endif]-->

    <script type="text/javascript" lang="javascript">
		 
        function changeText(button) {
		//	$('#btnRegister').hide();
        //    button.value = "processing";
        }
     
        function validate() {
            if (document.getElementById("<%=txtRefID.ClientID%>").value == "") {
                alert("Kindly Enter Referral ID!");
                document.getElementById("<%=txtRefID.ClientID%>").focus();
                return false;
            }             
            if (document.getElementById("<%=ddlPosition.ClientID%>").value == "0") {
                alert("Kindly Select Position!");
                document.getElementById("<%=ddlPosition.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtUserName.ClientID%>").value == "") {
                alert("Kindly enter the name");
                document.getElementById("<%=txtUserName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Kindly Enter User Email!");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (!reg.test(document.getElementById("<%=txtEmail.ClientID%>").value)) {
                      alert('Invalid Email Address');
                      document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtMobileNo.ClientID%>").value == "") {
                alert("Kindly Enter Mobile Number!");
                document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /(\+\d{1,3}[- ]?)?\d{10}/;
                if (!reg.test(document.getElementById("<%=txtMobileNo.ClientID%>").value)) {
                      alert('Invalid Mobile Number');
                      document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }
            }

            if (document.getElementById("<%=txtPAN.ClientID%>").value == "") {
                alert("Kindly Enter PAN Number!");
                document.getElementById("<%=txtPAN.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /[A-Za-z]{5}\d{4}[A-Za-z]{1}/;
                if (!reg.test(document.getElementById("<%=txtPAN.ClientID%>").value)) {
                    alert('Invalid PAN Number');
                    document.getElementById("<%=txtPAN.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtEpin.ClientID%>").value == "") {
                alert("Kindly Enter Valid Epin!");
                document.getElementById("<%=txtEpin.ClientID%>").focus();
                return false;
            }
			else {
                $('#btnRegister').hide();
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }

        window.onload = window.history.forward(0);  //calling function on window onload
    </script>
    <style>
        .rgOverlay {
            position: fixed;
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(0,0,0,0.5);
            z-index: 99999999 !important;
        }
    </style>

    <script>
        function UserDeleteConfirmation() {
            if (document.getElementById("<%=txtRefID.ClientID%>").value == "") {
                alert("Kindly Enter Referral ID!");
                document.getElementById("<%=txtRefID.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlPosition.ClientID%>").value == "0") {
                alert("Kindly Select Position!");
                document.getElementById("<%=ddlPosition.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtUserName.ClientID%>").value == "") {
                alert("Kindly enter the name");
                document.getElementById("<%=txtUserName.ClientID%>").focus();
                return false;
            }
            if (document.getElementById("<%=txtEmail.ClientID%>").value == "") {
                alert("Kindly Enter User Email!");
                document.getElementById("<%=txtEmail.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                if (!reg.test(document.getElementById("<%=txtEmail.ClientID%>").value)) {
                    alert('Invalid Email Address');
                    document.getElementById("<%=txtEmail.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtMobileNo.ClientID%>").value == "") {
                alert("Kindly Enter Mobile Number!");
                document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /(\+\d{1,3}[- ]?)?\d{10}/;
                if (!reg.test(document.getElementById("<%=txtMobileNo.ClientID%>").value)) {
                    alert('Invalid Mobile Number');
                    document.getElementById("<%=txtMobileNo.ClientID%>").focus();
                    return false;
                }
            }

            if (document.getElementById("<%=txtPAN.ClientID%>").value == "") {
                alert("Kindly Enter PAN Number!");
                document.getElementById("<%=txtPAN.ClientID%>").focus();
                return false;
            }
            else {
                var reg = /[A-Za-z]{5}\d{4}[A-Za-z]{1}/;
                if (!reg.test(document.getElementById("<%=txtPAN.ClientID%>").value)) {
                    alert('Invalid PAN Number');
                    document.getElementById("<%=txtPAN.ClientID%>").focus();
                    return false;
                }
            }
            if (document.getElementById("<%=txtEpin.ClientID%>").value == "") {
                alert("Kindly Enter Valid Epin!");
                document.getElementById("<%=txtEpin.ClientID%>").focus();
                return false;
            }
            
            var result = confirm("Are you sure you want to Register With LifeGoldEcom ?");
            if (result) {
                $('#ajaxLoding').css("display", "block");
                return true;
            }
            else
                return false;

            //else {
            //    $('#btnRegister').hide();
            //}
            return true;
        }
    </script>

    <noscript> Please enable your browser javascript for more security and safety! </noscript> 
</head>
<body>
     <div id="ajaxLoding" class="rgOverlay" style="display:none; text-align:center;"><img src="pleasewait.gif" class="img-thumbnail" style="margin-top:10%" /></div>
    <section class="container-pages">
         
        <div class="pages-tag-line text-white">
            <div class="h4">Let's Get Started .!</div>
        </div>
        <div class="card pages-card col-lg-8 col-md-10 col-sm-10" style="max-width: none">
            <div class="card-body ">
				<div class="text-center">
			 		<a class="text-center" href="index.aspx">
					<img alt="LifeGold" src="images/logo.png" class="img" style="width:300px; height:120px;" /> </a>
				</div> <br />
                <div class="h4 text-center text-theme"><strong>Register</strong></div>
                <form id="form1" runat="server">
                    <div>
                        <div class="row">
                            <div class="col-md-6 form-group">
                                <label>Referral ID *</label>
                                <asp:TextBox runat="server" ID="txtRefID" CssClass="form-control input-lg" required="required" Placeholder="Referral ID" OnTextChanged="txtRefID_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Referral Name</label>
                                <asp:TextBox ID="txtRefName" CssClass="form-control input-lg" runat="server" Enabled="false" Placeholder="Referral Name"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>

                            <div class="col-md-6 form-group">
                                <label>Select Sponser Group *</label>
                                <asp:DropDownList runat="server" ID="ddlPosition" CssClass="form-control">
                                    <asp:ListItem Value="0">Select Position</asp:ListItem>
                                    <asp:ListItem Value="1">Left</asp:ListItem>
                                    <asp:ListItem Value="2">Right</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Applicant Name *</label>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control input-lg" required="required" placeholder="Enter Applicant Name"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 form-group">
                                <label>Mobile No *</label>
                                <asp:TextBox ID="txtMobileNo" CssClass="form-control input-lg" placeholder="Mobile Number" runat="server" required="required" onkeypress="return isNumberKey(event);" MaxLength="10"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Email Id *</label>
                                <asp:TextBox ID="txtEmail" CssClass="form-control input-lg" placeholder="Email ID" runat="server" TextMode="Email" required="required"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 form-group">
                                <label>PAN No *</label>
                                <asp:TextBox ID="txtPAN" CssClass="form-control input-lg" placeholder="PAN Number" MaxLength="10" runat="server" required="required"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                 <label>Epin *</label>
                                <asp:TextBox ID="txtEpin" CssClass="form-control input-lg" placeholder="Epin" runat="server" required="required" OnTextChanged="txtEpin_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </div>
                            <hr />
                            <div class="clearfix"></div>
                            <%--  <div class="col-md-6 form-group">
                                <label>Bank Account Number</label>
                                <asp:TextBox ID="txtBankAcNo" CssClass="form-control input-lg" placeholder="Bank Account Number" runat="server" onkeypress="return isNumberKey(event);" MaxLength="15"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>IFSC CODE *</label>
                                <asp:TextBox ID="txtIFSC" CssClass="form-control input-lg" placeholder="Bank IFSC Code" runat="server"></asp:TextBox>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 form-group">
                                <label>Branch Name *</label>
                                <asp:TextBox ID="txtBranchName" CssClass="form-control input-lg" placeholder="Branch Name" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-6 form-group">
                                <label>Bank Name *</label>
                                <asp:TextBox ID="txtBankName" CssClass="form-control input-lg" placeholder="Bank Name" runat="server"></asp:TextBox>
                            </div>--%>
                            <div class="clearfix"></div>
                            <div class="col-md-6 form-group">
                                <div class="form-check">
                                    <label class="form-check-label" for="defaultCheck1">
                                        By continuing, you are agree to terms and conditions
                                    </label>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="col-md-6 form-group">
                                <asp:Button Text="Continue" runat="server" CssClass="btn btn-info" ID="btnRegister" OnClick="btnRegister_Click" OnClientClick="  if ( ! UserDeleteConfirmation()) return false;" Enabled="false" />
                                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
                                <%--changeText(this);  return validate(); --%>
                            </div>
                        </div>
                    </div>
                </form>
                <!-- end form -->
                <div class="text-center">
                    <small>already a member ? Please
                            <a href="Login.aspx" class="text-theme">Login</a>
                    </small>
                </div>
            </div>
            <!-- end card-body -->
        </div>
        <!-- end card -->
    </section>
    <!-- end section container -->

    <div class="half-circle"></div>
    <div class="small-circle"></div>

    <div id="mybutton">
        <div class="btn-group dropup">
            <button type="button" class="btn btn-round btn-theme " data-toggle="dropdown" aria-haspopup="true" aria-expanded="false" hidden="hidden">
                <i class="fa fa-circle fa-1"></i>
            </button>
            <div class="dropdown-menu">
                <h6 class="text-theme">Color Themes</h6>
                <ul class="theme-colors">
                    <li class="theme-blue" onclick="appSwapStyleSheet('portal/css/style-blue.css')"></li>
                    <li class="theme-green" onclick="appSwapStyleSheet('portal/css/style-green.css')"></li>
                    <li class="theme-red" onclick="appSwapStyleSheet('portal/css/style-red.css')"></li>
                    <li class="theme-yellow" onclick="appSwapStyleSheet('portal/css/style-yellow.css')"></li>
                    <li class="theme-orange" onclick="appSwapStyleSheet('portal/css/style-orange.css')"></li>
                    <li class="theme-teal" onclick="appSwapStyleSheet('portal/css/style-teal.css')"></li>
                    <li class="theme-cyan" onclick="appSwapStyleSheet('portal/css/style-cyan.css')"></li>
                    <li class="theme-purple" onclick="appSwapStyleSheet('portal/css/style-purple.css')"></li>
                    <li class="theme-indigo" onclick="appSwapStyleSheet('portal/css/style-indigo.css')"></li>
                    <li class="theme-pink" onclick="appSwapStyleSheet('portal/css/style-pink.css')"></li>
                </ul>

                <ul class="theme-colors">
                    <li class="theme-facebook" onclick="appSwapStyleSheet('portal/css/style-facebook.css')"></li>
                    <li class="theme-twitter" onclick="appSwapStyleSheet('portal/css/style-twitter.css')"></li>
                    <li class="theme-linkedin" onclick="appSwapStyleSheet('portal/css/style-linkedin.css')"></li>
                    <li class="theme-google-plus" onclick="appSwapStyleSheet('portal/css/style-google-plus.css')"></li>
                    <li class="theme-flickr" onclick="appSwapStyleSheet('portal/css/style-flickr.css')"></li>
                    <li class="theme-tumblr" onclick="appSwapStyleSheet('portal/css/style-tumblr.css')"></li>
                    <li class="theme-xing" onclick="appSwapStyleSheet('portal/css/style-xing.css')"></li>
                    <li class="theme-github" onclick="appSwapStyleSheet('portal/css/style-github.css')"></li>
                    <li class="theme-html5" onclick="appSwapStyleSheet('portal/css/style-html5.css')"></li>
                    <li class="theme-openid" onclick="appSwapStyleSheet('portal/css/style-openid.css')"></li>
                    <li class="theme-stack-overflow" onclick="appSwapStyleSheet('portal/css/style-stack-overflow.css')"></li>
                    <li class="theme-css3" onclick="appSwapStyleSheet('portal/css/style-css3.css')"></li>
                    <li class="theme-dribbble" onclick="appSwapStyleSheet('portal/css/style-dribbble.css')"></li>
                    <li class="theme-instagram" onclick="appSwapStyleSheet('portal/css/style-instagram.css')"></li>
                    <li class="theme-pinterest" onclick="appSwapStyleSheet('portal/css/style-pinterest.css')"></li>
                    <li class="theme-vk" onclick="appSwapStyleSheet('portal/css/style-vk.css')"></li>
                    <li class="theme-yahoo" onclick="appSwapStyleSheet('portal/css/style-yahoo.css')"></li>
                    <li class="theme-behance" onclick="appSwapStyleSheet('portal/css/style-behance.css')"></li>
                    <li class="theme-dropbox" onclick="appSwapStyleSheet('portal/css/style-dropbox.css')"></li>
                    <li class="theme-reddit" onclick="appSwapStyleSheet('portal/css/style-reddit.css')"></li>
                    <li class="theme-spotify" onclick="appSwapStyleSheet('portal/css/style-spotify.css')"></li>
                    <li class="theme-vine" onclick="appSwapStyleSheet('portal/css/style-vine.css')"></li>
                    <li class="theme-foursquare" onclick="appSwapStyleSheet('portal/css/style-foursquare.css')"></li>
                    <li class="theme-vimeo" onclick="appSwapStyleSheet('portal/css/style-vimeo.css')"></li>
                </ul>
            </div>
        </div>
    </div>
    <!-- end mybutton -->

    <script src="portal/libs/jquery/dist/jquery.min.js"></script>
    <script src="portal/libs/popper.js/dist/umd/popper.min.js"></script>
    <script src="portal/libs/bootstrap/bootstrap.min.js"></script>
    <script src="portal/libs/PACE/pace.min.js"></script>
    <script src="portal/libs/chart.js/dist/Chart.min.js"></script>
    <script src="portal/libs/nicescroll/jquery.nicescroll.min.js"></script>
    <script src="portal/libs/jquery-knob/dist/jquery.knob.min.js"></script>

    <!-- jquery-loading -->
    <script src="portal/libs/jquery-loading/dist/jquery.loading.min.js"></script>
    <!-- octadmin Main Script -->
    <script src="portal/js/app.js"></script>
</body>
</html>
