<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="description" content="Admin, Dashboard, Bootstrap" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title>LifeGold | Login</title>

   <%-- <link rel="apple-touch-icon" sizes="180x180" href="portal/img/favicon/apple-touch-icon.png" />
    <link rel="icon" type="image/png" sizes="32x32" href="portal/img/favicon/favicon-32x32.png" />
    <link rel="icon" type="image/png" sizes="16x16" href="portal/img/favicon/favicon-16x16.png" />
    <link rel="manifest" href="portal/img/favicon/manifest.json" />--%>
    <link rel="mask-icon" href="portal/img/favicon/safari-pinned-tab.svg" color="#5bbad5" />
    <meta name="theme-color" content="#ffffff" />

    <!-- fonts -->
    <link rel="stylesheet" href="portal/fonts/md-fonts/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="portal/fonts/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.css" />
    <link rel="stylesheet" href="http://cdn.materialdesignicons.com/2.5.94/css/materialdesignicons.min.css" />
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
</head>
<body>
    <section class="container-pages">         
        <div class="pages-tag-line text-white">            
        </div>

        <div class="card pages-card col-lg-4 col-md-6 col-sm-6">
            <div class="card-body ">
				<div class="text-center">
			 		<a class="text-center" href="index.aspx">
					<img alt="LifeGold" src="images/logo.png" class="img" style="width:230px; height:100px;" /> </a>
				</div> <br/>
                <div class="h4 text-center text-theme"><strong>Login</strong></div>
                <div class="small text-center text-dark"> </div>
                <form id="form1" runat="server">
                    <div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon text-theme"><i class="fa fa-user"></i>
                                </span>
                                <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" placeholder="User ID" required="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <span class="input-group-addon text-theme"><i class="fa fa-envelope"></i>
                                </span>
                                <asp:TextBox runat="server" ID="txtPwd" CssClass="form-control" placeholder="Password" required="true" TextMode="Password"></asp:TextBox>
                            </div>
                        </div>

                        <div class="form-group form-actions text-center">
                            <asp:Button runat="server" ID="btnLogin" CssClass="btn btn-info " Text="LOG IN" OnClick="btnLogin_Click" />
                        </div>
                    </div>

                </form>
                <!-- end form -->
                <div class="text-center">
                    <small> <a href="ForgotPassword.aspx" class="text-theme">Forgot Password</a> | 
Yet not registerd ? Please
                            <a href="register.aspx" class="text-theme">Register</a>
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

    <div id="mybutton" hidden="hidden">
        <div class="btn-group dropup">
            <button type="button" class="btn btn-round btn-theme" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                <i class="mdi mdi-palette"></i>
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
