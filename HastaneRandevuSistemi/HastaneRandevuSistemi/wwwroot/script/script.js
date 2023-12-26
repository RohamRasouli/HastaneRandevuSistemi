// drop-down menu
let flag=0;
$(document).ready(function(){
$('#hamburger-btn').click(function(){
  if(flag==0){
    $('.drop-down-id').toggle();
    flag=1;
  }
  else{
    $('.drop-down-id').hide();
    flag=0;
  }
});
});

// home page slider
//$('.carousel').carousel({
//interval: 1000
//})
//$('.carousel').carousel('cycle');

// pop-up click
$(document).ready(function(){
    $('#pop-up').click(function(){
        $('.pop-up').hide();
        $('nav').removeClass('opacity-50 pe-none');         
        $('.container-fluid').removeClass('opacity-50 pe-none');        
        $('footer').removeClass('opacity-50 pe-none');
    });
 });
// doctor-detay click
    $(document).ready(function(){
    $('.doctor-detay').click(function(){
        $('.pop-up').toggle();
        $('nav').addClass('opacity-50 pe-none');        
        $('.container-fluid').addClass('opacity-50 pe-none');        
        $('footer').addClass('opacity-50 pe-none');
    });
    });

// side-bar item click
    $(document).ready(function(){
    $('.side-bar').click(function(){      
      $("li").find("a.side-bar").removeClass('active');
      $("li").find("a.side-bar").addClass('link-dark'); 
        $(this).addClass('active');
        // $("div").find("table").hide();
        $("div.col-lg-10 ").hide();
          //alert('#'+$(this).attr('name'));
          var element='#menu-'+$(this).attr('name')
          //alert(element);          
        $(element).toggle();
    });
    });
    