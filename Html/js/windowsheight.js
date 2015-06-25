// JavaScript Document

 
  $(window).load(function() { 
           widowsheight();
		 
        });
		/* var h =parseInt(screen.availHeight);*/
	 function widowsheight(){
		 var h =parseInt($(window).height());
		 var w=parseInt(screen.availWidth);
		 $(".fixed").width(w);
		 $(".fixed").height(h);
		/*改变视频高度 自适应*/
		 t=setTimeout('widowsheight()',500) 
		 
		 }
		 
	  
      
        

        
		
         

