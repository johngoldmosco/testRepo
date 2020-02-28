<%@ Page Title="" Language="C#" MasterPageFile="~/portal/admin/Master.master" AutoEventWireup="true" CodeFile="Tree.aspx.cs" Inherits="portal_admin_Tree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript" src="../tree/jquery.js"></script>
   
    <script type="text/javascript">
        var minMargin = 15; // set how much minimal space there should be (in pixels)
        // between the popup and everything else (borders, mouse)
        var ready = false;  // we are ready when the mouse event is set up
        var default_width = 200; // will be set to width from css in document.ready

        /* Prepare popup and define the mouseover callback */
        jQuery(document).ready(function () {
            $('body').append('<div id="pup" style="position:abolute; display:none; z-index:200;"></div>');
            css_width = $('#pup').width();
            if (css_width != 0) default_width = css_width;
            // set dynamic coords when the mouse moves
            $(document).mousemove(function (e) {
                var x, y;

                x = $(document).scrollLeft() + e.clientX;
                y = $(document).scrollTop() + e.clientY;

                x -= 50; // important: if the popup is where the mouse is, the hoverOver/hoverOut events flicker

                var x_y = nudge(x, y); // avoids edge overflow

                // remember: the popup is still hidden
                $('#pup').css('top', x_y[1] + 'px');
                $('#pup').css('left', x_y[0] + 'px');
            });
            ready = true;
        });

        /*
         The actual callback:
         Write message, show popup w/ custom width if necessary,
         make sure it disappears on mouseout
        */
        function popup(msg, width) {
            if (ready) {
                // use default width if not customized here
                if (typeof width === "undefined") {
                    width = default_width;
                }
                // write content and display
                $('#pup').html(msg).width(width).show();
                // make sure popup goes away on mouse out
                // the event obj needs to be gotten from the virtual 
                //   caller, since we use onmouseover='popup(msg)' 
                var t = getTarget(arguments.callee.caller.arguments[0]);
                $(t).unbind('mouseout').bind('mouseout',
                    function (e) {
                        $('#pup').hide().width(default_width);
                    }
                );
            }
        }

        /* Avoid edge overflow */
        function nudge(x, y) {
            var win = $(window);

            // When the mouse is too far on the right, put window to the left
            var xtreme = $(document).scrollLeft() + win.width() - $('#pup').width() - minMargin;
            if (x > xtreme) {
                x -= $('#pup').width() + 2 * minMargin;
            }
            x = max(x, 0);

            // When the mouse is too far down, move window up
            if ((y + $('#pup').height()) > (win.height() + $(document).scrollTop())) {
                y -= $('#pup').height() + minMargin;
            }

            return [x, y];
        }

        /* custom max */
        function max(a, b) {
            if (a > b) return a;
            else return b;
        }

        /*
         Get the target (element) of an event.
         Inspired by quirksmode
        */
        function getTarget(e) {
            var targ;
            if (!e) var e = window.event;
            if (e.target) targ = e.target;
            else if (e.srcElement) targ = e.srcElement;
            if (targ.nodeType == 3) // defeat Safari bug
                targ = targ.parentNode;
            return targ;
        }
    </script>
    <style>
        .line-vertical {
            width: 4px;
            height: 25px;
            background: #333;
            margin: 0 auto;
        }

        .line-horizontal {
            width: 100%;
            height: 4px;
            background: #333;
        }

        .line-horizontal-left {
            width: 50.8%;
            height: 4px;
            background: #333;
            float: left;
        }

        .line-horizontal-right {
            width: 50.5%;
            height: 4px;
            background: #333;
            float: right;
        }

        .table thead > tr > th, .table tbody > tr > th,
        .table tfoot > tr > th, .table thead > tr > td,
        .table tbody > tr > td, .table tfoot > tr > td {
            border-top: 0;
            padding: 0;
        }

        .profilePaid {
            border: 1px solid #000;
            background-color: green;
        }

        .profile img {
            width: 52px;
            height: 52px;
            margin: 5px 0;
            border: none;
        }

        td img {
            width: 48px;
            height: 48px;
            margin: 5px 0;
        }

        @media(max-width:767px) {
            .profile img {
                width: 32px;
                height: 32px;
                margin: 5px 0;
            }

            td img {
                width: 32px;
                height: 32px;
                margin: 5px 0;
            }
        }

        @media(max-width:480px) {
            .profile img {
                width: 16px;
                height: 16px;
                margin: 5px 0;
            }

            td img {
                width: 16px;
                height: 16px;
                margin: 5px 0;
            }
        }

        .profile .name {
            margin-top: 10px;
            font-size: 13px;
        }
        /* HOVER STYLES */
        /*div.pop-up {
		  display: none;
		  text-align: left;
		  position: absolute;
		  /*margin-top: 100px;*/
        /*width: 250px;
		  padding: 5px 13px;
		  background: #333;
		  color: #fff;
		  border: 1px solid #1a1a1a;
		  font-size: 90%;
		  z-index:999;
		}
		div.pop-up table{width:100%;text-align:left;}
		div.pop-up tr{border-top:1px solid #ccc;}
		div.pop-up tr:first-child{border-top:none;}
		div.pop-up th{text-align:center;padding:5px;}
		div.pop-up td{text-align:center;padding:5px;}*/
        #pup {
            position: absolute;
            z-index: 200; /* aaaalways on top*/
            padding: 3px;
            margin-left: 10px;
            margin-top: 5px;
            color: #fff;
            font-size: 0.95em;
        }


        .hidden-table {
            padding: 3px;
            margin-left: 10px;
            margin-top: 5px;
            border: 1px solid black;
            background: #026466;
            color: #fff;
            width: 360px;
        }

            .hidden-table tr {
                border-bottom: 1px solid #777;
            }

                .hidden-table tr:last-child {
                    border-bottom-color: transparent;
                }

            .hidden-table td {
                padding: 5px 10px;
                margin: 0;
                border-bottom: 1px solid #999;
            }

        a.red {
            padding: 15px 10px;
            background: rgb(255,26,0); /* Old browsers */
            background: -moz-linear-gradient(top, rgba(255,26,0,1) 0%, rgba(255,26,0,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(255,26,0,1)), color-stop(100%,rgba(255,26,0,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, rgba(255,26,0,1) 0%,rgba(255,26,0,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, rgba(255,26,0,1) 0%,rgba(255,26,0,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top, rgba(255,26,0,1) 0%,rgba(255,26,0,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom, rgba(255,26,0,1) 0%,rgba(255,26,0,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#ff1a00', endColorstr='#ff1a00',GradientType=0 ); /* IE6-9 */
        }

        a.green {
            padding: 15px 10px;
            background: rgb(100,209,41); /* Old browsers */
            background: -moz-linear-gradient(top, rgba(100,209,41,1) 0%, rgba(22,191,0,1) 100%); /* FF3.6+ */
            background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,rgba(100,209,41,1)), color-stop(100%,rgba(22,191,0,1))); /* Chrome,Safari4+ */
            background: -webkit-linear-gradient(top, rgba(100,209,41,1) 0%,rgba(22,191,0,1) 100%); /* Chrome10+,Safari5.1+ */
            background: -o-linear-gradient(top, rgba(100,209,41,1) 0%,rgba(22,191,0,1) 100%); /* Opera 11.10+ */
            background: -ms-linear-gradient(top, rgba(100,209,41,1) 0%,rgba(22,191,0,1) 100%); /* IE10+ */
            background: linear-gradient(to bottom, rgba(100,209,41,1) 0%,rgba(22,191,0,1) 100%); /* W3C */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#64d129', endColorstr='#16bf00',GradientType=0 ); /* IE6-9 */
        }

        a.black {
            padding: 40px 10px 5px 10px;
            background-color: #FADC1A;
        }

        a.orange {
            padding: 40px 10px 5px 10px;
            background-color: #FF9933;
        }

        a.blue {
            padding: 40px 10px 5px 10px;
            background-color: Aqua;
        }
        .table-primary, .table-primary>td, .table-primary>th
        {
            background-color:#fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="main">
        <!-- Breadcrumb -->
        <ol class="breadcrumb bc-colored bg-theme" id="breadcrumb">
            <li class="breadcrumb-item ">
                <a href="overview.aspx">Home</a>
            </li>
            <li class="breadcrumb-item">
                <a href="#">Network</a>
            </li>
            <li class="breadcrumb-item active">Geneology Tree</li>
        </ol>

        <div class="container-fluid">
            <div class="animated fadeIn">
                <div class="card col-sm-12">
                    <div class="card-header">
                        <strong>Geneology </strong>
                        Tree
                    </div>
                    <div class="card-body text-theme" style="height:500px;">
						<div class="row form-inline">
                           <div class="form-group">
                               <img alt="Plan 3000" src="../tree/Geneology/plan1.png" style="width: 52px;height: 52px;"/>
							   <br />
							   <b>Pack-3000</b>
                            </div>
							 <div class="form-group">
                                <img alt="Plan 5000" src="../tree/Geneology/plan2.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-5000</b>
                            </div>
							
							 <div class="form-group">
                                <img alt="Plan 25000" src="../tree/Geneology/plan3.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-25000</b>
                            </div>
							
							 <div class="form-group">
                                <img alt="Plan 50000" src="../tree/Geneology/plan4.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-50000</b>
                            </div>
							
							 <div class="form-group">
                                <img alt="Plan 100000" src="../tree/Geneology/plan5.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-100000</b>
                            </div>
							
							 <div class="form-group">
                                <img alt="Plan 200000" src="../tree/Geneology/plan6.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-200000</b>
                            </div>
							
							 <div class="form-group">
                                <img alt="Plan 500000" src="../tree/Geneology/plan7.png" style="width: 52px;height: 52px;"/><br />
								<b>Pack-500000</b>
                            </div>
                        </div>
						<br /><br />
						 <div class="form-inline">
                       <div class="">
                       		<asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" placeholder="UserID"></asp:TextBox>
                       </div>
                       <div class="">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-sm btn-primary btn-round" OnClick="btnSearch_Click" />
                       </div></div>
                       <asp:Literal ID="litPopup" runat="server"></asp:Literal>

                    </div>
                   
                </div>
            </div>
        </div>
    </div>
</asp:Content>

