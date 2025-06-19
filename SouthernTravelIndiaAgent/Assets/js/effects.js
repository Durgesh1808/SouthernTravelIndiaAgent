$(window).load(function(){
	//alert('abhay')
		$('.purechat').attr('style', 'right: 68px !important');//.css({'right':500});
		
})

$(document).ready(function() {
/*Tab start*/
	// setting the tabs in the sidebar hide and show, setting the current tab
	//$('div.srchResult div').hide();
	//$('div.t4').show();
	//$('div#bookTab li a.t4').addClass('active');
	// SIDEBAR TABS
	$('div#bookTab a.tab').click(function(){
		var thisClass = this.className.slice(0,2);
		$('div#bookContent div.box').hide();
		$('div.' + thisClass).fadeIn(900);
		$('div#bookTab li a').removeClass('active');
		$(this).addClass('active');
	});
	
	
	// SIDEBAR TABS
	$('div.subTab li a.ssTab').click(function(){
		var thisClass = this.className.slice(0,2);
		$('div.subTab div.subcontent').hide();
		$('div.' + thisClass).fadeIn(900);
		$('div.subTab li a').removeClass('subact');
		$(this).addClass('subact');
	});
	
	
	$('div#nbookTab a.tab').click(function(){
		var thisClass = this.className.slice(0,2);
		$('div#nbookContent div.box').hide();
		$('div.' + thisClass).fadeIn(900);
		$('div#nbookTab li a').removeClass('active');
		$(this).addClass('active');
	});
	
	
	// SIDEBAR TABS
	$('div.nsubTab li a.nssTab').click(function(){
		var thisClass = this.className.slice(0,2);
		$('div.nsubTab div.nsubcontent').hide();
		$('div.' + thisClass).fadeIn(900);
		$('div.nsubTab li a').removeClass('nsubact');
		$(this).addClass('nsubact');
	});
	
	
	
	
	
		// preferd TABS

        $(function() {
            var $items = $('#vtab1>ul>li>a');
            $items.mousedown(function() {
                $items.removeClass('selected');
                $(this).addClass('selected');

                var index = $items.index($(this));
                $('#vtab1>div').hide().eq(index).show();
            }).eq(0).mousedown();
        });	
	
	
	
	
/*Tab end*/	

/*Navigation start*/	
	$('#nav li').hover(function(){
		
		
		$(this).addClass('navHover');
		
		
		}, function(){
			$(this).removeClass('navHover');
		});
/*Navigation end*/



$(function() {
	$('.slidebttn').hover(
		function () {
			var $this 		= $(this);
			var $slidelem 	= $this.prev();
			$slidelem.stop().animate({'width':'80px'},100);
			$slidelem.find('span').stop(true,true).fadeIn();
			$this.addClass('button_c');
		},
		function () {
			var $this 		= $(this);
			var $slidelem 	= $this.prev();
			$slidelem.stop().animate({'width':'14px'},200);
			$slidelem.find('span').stop(true,true).fadeIn();
			$this.removeClass('button_c');
			
		}
	);
	
});
$(document).ready(function() {
	$('#button_aLeft').mouseenter(function() {
		$(this).stop();
	});
	$('#button_aLeft').mouseleave(function() {
		$(this).stop().animate({'width':'14px'},200);
	});
});

$(function() {
                if( jQuery('#sidebar').length>0){
            var offset = $("#sidebar").offset();
            var topPadding = 150;
            $(window).scroll(function() {
                if ($(window).scrollTop() > offset.top) {
                    $("#sidebar").stop().animate({
                        marginTop: $(window).scrollTop() - offset.top + topPadding
                    });
                } else {
                    $("#sidebar").stop().animate({
                        marginTop: 0
                    });
                };
            });
            }
            
        });

});