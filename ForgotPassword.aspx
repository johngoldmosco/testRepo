<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="ForgotPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="x-ua-compatible" content="ie=edge" />
    <meta name="description" content=" " />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Recover Password - LifeGold</title>
    <link rel="manifest" href="portal/img/favicon/manifest.json" />
    <link rel="mask-icon" href="portal/img/favicon/safari-pinned-tab.svg" color="#5bbad5" />
    <meta name="theme-color" content="#ffffff" />

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/MaterialDesign-Webfont/2.2.43/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <!-- animate css -->
    <link rel="stylesheet" href="portal/libs/animate.css/animate.min.css" />

    <!-- jquery-loading -->
    <link rel="stylesheet" href="portal/libs/jquery-loading/dist/jquery.loading.min.css" />
    <!-- octadmin main style -->
    <link id="pageStyle" rel="stylesheet" href="portal/css/style.css" />
    <noscript>Please enable your javascript for better security</noscript>
</head>
<body>
    <section class="container-pages">
 
        <div class="pages-tag-line text-white"> 
        </div>

        <div class="card pages-card col-lg-4 col-md-6 col-sm-6">
            <div class="card-body ">
				<div class="text-center">
			 		<a class="text-center" href="index.aspx">
					<img alt="LifeGold" src="images/logo.png" class="img" style="width:230px; height:80px;" /> </a>
				</div> <br />
                <div class="h4 text-center text-theme"><strong>Recover Password</strong></div>
                <div class="small text-center text-dark"></div>

                <form runat="server">
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon text-theme"><i class="fa fa-envelope"></i>
                            </span>
                            <asp:TextBox runat="server" ID="txtUserID" CssClass="form-control" placeholder="Customer ID" data-toggle="tooltip" data-placement="top" title="" data-original-title="Your User ID" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="input-group">
                            <span class="input-group-addon text-theme"><i class="fa fa-asterisk"></i></span>
                            <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Mobile" data-toggle="tooltip" data-placement="top" title="" data-original-title="Associated Mobile No." MaxLength="10" required="true"></asp:TextBox>

                        </div>
                    </div>
                    <div class="form-group">
                        <asp:RegularExpressionValidator ID="regExValMobileNo" runat="server" ValidationExpression="^[0-9]{4,10}$" ControlToValidate="txtMobile" ForeColor="Red" ErrorMessage="Enter numeric text only (0-9) !"></asp:RegularExpressionValidator>
                    </div>
                    <div class="form-group form-actions">
                        <asp:Button runat="server" ID="btnGetPassword" Text="Get Password" CssClass="btn btn-primary btn-block btn-lg" OnClick="btnGetPassword_Click" />
                    </div>
                </form>
                <!-- end form -->
                <div class="text-center">
                    <small>I know my password ? Please
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

    <!-- Bootstrap and necessary plugins -->
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
