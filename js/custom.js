/*=========/=========/=========/=========/=========/=========/=========
    File Name: custom.js
    Template Name: Progki Consulting
    Created By: JollyThemes
    Envato Profile: http://themeforest.net/user/jollythemes
    Website: http://www.jollythemes.com
    Version: 1.0
=========/=========/=========/=========/=========/=========/========= */

/* =========/=========/=========/=========/=========/=========/=========
	
	* 1.  // Preloader *
	* 2.  // Responsive Menu *
	* 3.  // AddClass (current) *
	* 4.  // Search *
	* 5.  // Fixed menu *
	* 6.  // AOS  *
	* 7.  // Counter Up *
	* 8.  // Testimonial Slider *
	* 9.  // Testimonial Slider-2 *
	* 10. // Partner Carousel *
	* 11. // Tooltip *
	* 12. // Maps *
	* 13. // Scroll to top *
	* 14. // Contact Form *
	* 15. // Colorbox popup *
	* 16. // Colorbox popup Reply form *
	* 17. // Add class Active *
	* 18. // Placeholder on focus *
	
=========/=========/=========/=========/=========/=========/========= */


(function($) {
    
	"use strict";
		
		/*=========/=========/=========/=========/=========/=========/=========
			Preloader
		=========/=========/=========/=========/=========/=========/=========*/
		
		$(window).on('load', function() { 
			$('#loader').fadeOut(); 
			$('#preloader').delay(550).fadeOut('slow'); 
			$('body').delay(450).css({'overflow':'visible'});
		})
		
		/* =========/=========/=========/=========/=========/=========/=========
			Responsive Menu
		=========/=========/=========/=========/=========/=========/========= */
		
		$(document).ready(function () {
             $("#respMenu").aceResponsiveMenu({
				resizeWidth: '768',        
				animationSpeed: 'fast', 
				accoridonExpAll: false 
             });
         });
		
		/* =========/=========/=========/=========/=========/=========/=========
			AddClass (current)
		=========/=========/=========/=========/=========/=========/========= */
		
		 $('.menu-toggle button').on('click',function(){
			if ( $(this).hasClass('current') ) {
				$(this).removeClass('current');
			} else {
				$('button.current').removeClass('current');
				$(this).addClass('current');    
			}
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Search
		=========/=========/=========/=========/=========/=========/========= *
		
		$(function () {
			$('a[href="#search"]').on('click', function(event) {
				event.preventDefault();
				$('#search').addClass('open');
				$('#search > form > input[type="search"]').focus();
			});
			$('#search, #search button.close').on('click keyup', function(event) {
				if (event.target == this || event.target.className == 'close' || event.keyCode == 27) {
					$(this).removeClass('open');
				}
			});
			$('form').submit(function(event) {
				event.preventDefault();
				return false;
			})
		});
		
		
		/* =========/=========/=========/=========/=========/=========/=========
			Fixed menu
		=========/=========/=========/=========/=========/=========/========= */
		
		$(window).on('scroll', function () {
			if ($(window).scrollTop() > 50) {
				$('.top-head').addClass('fixed-menu');
			} else {
				$('.top-head').removeClass('fixed-menu');
			}
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			AOS 
		=========/=========/=========/=========/=========/=========/========= */
		
		AOS.init({
			duration: 1200
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Counter Up
		=========/=========/=========/=========/=========/=========/========= */
		
		$('.counter').counterUp({
			delay: 10,
			time: 1000
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Portfolio filters
		=========/=========/=========/=========/=========/=========/========= */
		
        $(".project-menu span").on('click', function () {
            $(".project-menu span").removeClass('active');
            $(this).addClass('active');
            var filterValue = $(this).attr('data-filter');
            $(".project-gird").isotope({
                filter: filterValue
            });
        }); 
		
		/* =========/=========/=========/=========/=========/=========/=========
			Testimonial Slider
		=========/=========/=========/=========/=========/=========/========= */
		
        $("#testimonial-slider").owlCarousel({
            animateOut: 'slideOutUp',
			animateIn: 'fadeUp',
			autoplay: true,
            autoplaySpeed: 4000,
            autoplayTimeout: 4000,
            autoplayHoverPause: true,
			items:1,
			margin:30,
			stagePadding:30,
			smartSpeed:450
        }); 		
		
		/* =========/=========/=========/=========/=========/=========/=========
			Testimonial Slider 2
		=========/=========/=========/=========/=========/=========/========= */
		
        $("#testimonial-slider-2").owlCarousel({
			autoplay: true,
			items:2,
			navText : ["<i class='zmdi zmdi-chevron-left'></i>","<i class='zmdi zmdi-chevron-right'></i>"],
			nav: true,
			dots: false,
			loop:true,
			responsiveClass:true,
			responsive: {
                0: {
                    items: 1
                },
                576: {
                    items: 1
                },
                768: {
                    items: 2
                },
                1200: {
                    items: 2
                }
            }
			
        }); 		
		
		/* =========/=========/=========/=========/=========/=========/=========
			Partner Carousel
		=========/=========/=========/=========/=========/=========/========= */
		
		$("#our-partner-slider").owlCarousel({
			items: 6,
            loop: true,
            autoplay: true,
            smartSpeed: 500,
            margin: 30,
            center: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                576: {
                    items: 2
                },
                768: {
                    items: 4
                },
                1200: {
                    items: 6
                }
            }
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Tooltip
		=========/=========/=========/=========/=========/=========/========= */
		
		$('[data-toggle="tooltip"]').tooltip();   
		
		/* =========/=========/=========/=========/=========/=========/=========
			Scroll to top  
		=========/=========/=========/=========/=========/=========/========= */
		
		if ($('#scroll-to-top').length) {
			var scrollTrigger = 100, // px
				backToTop = function () {
					var scrollTop = $(window).scrollTop();
					if (scrollTop > scrollTrigger) {
						$('#scroll-to-top').addClass('show');
					} else {
						$('#scroll-to-top').removeClass('show');
					}
				};
			backToTop();
			$(window).on('scroll', function () {
				backToTop();
			});
			$('#scroll-to-top').on('click', function (e) {
				e.preventDefault();
				$('html,body').animate({
					scrollTop: 0
				}, 700);
			});
		}
		
		/* =========/=========/=========/=========/=========/=========/=========
			Contact Form
		=========/=========/=========/=========/=========/=========/========= *
		
		/** bottom form **
		
		var submitContact = $('#submit_contact'),
		message = $('#msg');
			submitContact.on('click', function(e){
				var $this = $(this);
				$.ajax({
					type: "POST",
					url: 'contact.php',
					dataType: 'json',
					cache: false,
					data: $('#contact-form').serialize(),
					success: function(data) {
						if(data.info !== 'error'){
							$this.parents('form').find('input[type=text],textarea,select').filter(':visible').val('');
							message.hide().removeClass('success').removeClass('error').addClass('success').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
						} else {
							message.hide().removeClass('success').removeClass('error').addClass('error').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
						}
					}
				});
			});
			
		/** top form **
			
			var submitContact = $('#submit_contact-cont'),
		message = $('#msg');
			submitContact.on('click', function(e){
				var $this = $(this);
				$.ajax({
					type: "POST",
					url: 'contact.php',
					dataType: 'json',
					cache: false,
					data: $('#contact-form-cont').serialize(),
					success: function(data) {
						if(data.info !== 'error'){
							$this.parents('form').find('input[type=text],textarea,select').filter(':visible').val('');
							message.hide().removeClass('success').removeClass('error').addClass('success').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
						} else {
							message.hide().removeClass('success').removeClass('error').addClass('error').html(data.msg).fadeIn('slow').delay(5000).fadeOut('slow');
						}
					}
				});
			});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Colorbox popup
		=========/=========/=========/=========/=========/=========/========= */
		
		$(".membername").colorbox({
			inline:true,
			width:"100%",
			maxWidth:766,
			top :20,
		});
		 
		/* =========/=========/=========/=========/=========/=========/=========
			Colorbox popup Reply form
		=========/=========/=========/=========/=========/=========/========= */
		
		$(".reply-form-box").colorbox({
			inline:true,
			width:"100%",
			maxWidth:500,
			top :20,
		});
		
		/* =========/=========/=========/=========/=========/=========/=========
			Add class Active
		=========/=========/=========/=========/=========/=========/========= */
		
		$('#accordion .card-header a').on( "click", function() {
			$('#accordion .card-header').removeClass('active');
			if(!$(this).closest('.card').find('.collapse').hasClass('show'))
				$(this).parents('.card-header').addClass('active');
		 });
		 
		$('#accordion-a .card-header a').on( "click", function() {
			$('#accordion-a .card-header').removeClass('active');
			if(!$(this).closest('.card').find('.collapse').hasClass('show'))
				$(this).parents('.card-header').addClass('active');
		 });
		 
		 
		/* =========/=========/=========/=========/=========/=========/=========
			Placeholder on focus 
		=========/=========/=========/=========/=========/=========/========= */
		  
		$("input,textarea").each( function(){
			$(this).data('holder',$(this).attr('placeholder'));
            $(this).on('focusin', function() {
                $(this).attr('placeholder','');
            });
            $(this).on('focusout', function() {
                $(this).attr('placeholder',$(this).data('holder'));
            });     
        });
		
		/* =========/=========/=========/=========/=========/=========/=========
			Maps
		=========/=========/=========/=========/=========/=========/========= */
		
		/*var locations = [
		  ['Flatlands Brooklyn', 40.6221572, -73.9409306, 4],
		  ['Apple SoHo', 40.7187663, -73.9986326, 5],
		  ['Trinity Church', 40.7092145, -74.0089267, 3],
		  ['Brooklyn Web design', 40.6470376, -73.9558279, 2],
		  ['Greenpoint', 40.729337, -73.9545708, 1]
		];
		var infowindow = new google.maps.InfoWindow();
		var marker, i;
		google.maps.event.addDomListener(window, 'load', init);
		function init() {
			var mapOptions = {
				zoom: 11,
				center: new google.maps.LatLng(40.6700, -73.9400), // New York
				styles: [{"featureType":"all","elementType":"geometry.fill","stylers":[{"weight":"2.00"}]},{"featureType":"all","elementType":"geometry.stroke","stylers":[{"color":"#9c9c9c"}]},{"featureType":"all","elementType":"labels.text","stylers":[{"visibility":"on"}]},{"featureType":"landscape","elementType":"all","stylers":[{"color":"#f2f2f2"}]},{"featureType":"landscape","elementType":"geometry.fill","stylers":[{"color":"#ffffff"}]},{"featureType":"landscape.man_made","elementType":"geometry.fill","stylers":[{"color":"#ffffff"}]},{"featureType":"poi","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"road","elementType":"all","stylers":[{"saturation":-100},{"lightness":45}]},{"featureType":"road","elementType":"geometry.fill","stylers":[{"color":"#eeeeee"}]},{"featureType":"road","elementType":"labels.text.fill","stylers":[{"color":"#7b7b7b"}]},{"featureType":"road","elementType":"labels.text.stroke","stylers":[{"color":"#ffffff"}]},{"featureType":"road.highway","elementType":"all","stylers":[{"visibility":"simplified"}]},{"featureType":"road.arterial","elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"all","stylers":[{"visibility":"off"}]},{"featureType":"water","elementType":"all","stylers":[{"color":"#46bcec"},{"visibility":"on"}]},{"featureType":"water","elementType":"geometry.fill","stylers":[{"color":"#c8d7d4"}]},{"featureType":"water","elementType":"labels.text.fill","stylers":[{"color":"#070707"}]},{"featureType":"water","elementType":"labels.text.stroke","stylers":[{"color":"#ffffff"}]}]
			};
			var mapElement = document.getElementById('map');
			var map = new google.maps.Map(mapElement, mapOptions);
			var marker = new google.maps.Marker({
				position: new google.maps.LatLng(40.6700, -73.9400),
				map: map,
				title: 'Brooklyn',
				animation: google.maps.Animation.BOUNCE,
				icon: 'images/google-pin.png'
			});
			for (i = 0; i < locations.length; i++) { 
			  marker = new google.maps.Marker({
				position: new google.maps.LatLng(locations[i][1], locations[i][2]),
				map: map,
				animation: google.maps.Animation.BOUNCE,
				icon: 'images/google-pin.png',
			  });
			  google.maps.event.addListener(marker, 'click', (function(marker, i) {
				return function() {
				  infowindow.setContent(locations[i][0]);
				  infowindow.open(map, marker);
				}
			  })(marker, i));
			}
		}*/
		
		
})(jQuery);
