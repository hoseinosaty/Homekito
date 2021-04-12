// JavaScript Document

var maxli = 0;
var hamid = "";
var createimg = "";
$(document).ready(function(){
	//count talk bubbles
	$(".ratestar ul li").click(function(){
        $(".shownatije").html("");	
	var selectrate = $(this).attr('data');
	var data_rateprod = {
		'ratestar' : selectrate,
		'prodid': $(this).parent().parent().attr('rel'),
		'submitrateprod':'set'
	};
	hrb_notify([
		'success',
		"",
		'fa-star',
		"امتیاز با موفقیت ثبت شد",
		'bottomLeft',
		'flipInY',
		'flipOutX',
		'5'
	]);
	// $.post("theme/fm-music/include.php",data_rateprod,function(data){
		
	// 	$(".shownatije").html(data);
	// 	setTimeout(function(){
	// 	   $(".shownatije").fadeOut(1000);
	// 	},1000);
		
	// });
	var stut = $(this).parent().parent().parent().attr('data');
		if(stut!=='disabled'){
			if(selectrate==1){
				$(this).parent().parent().parent().children(".shownatije").fadeOut(100);
				$(this).parent().parent().parent().children(".shownatije").delay(200).fadeIn(500);
				$(this).parent().parent().parent().children(".showactiverate").addClass("rateavtive1");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive2");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive3");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive4");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive5");
			}else if(selectrate==2){
				$(this).parent().parent().parent().children(".shownatije").fadeOut(100);
				$(this).parent().parent().parent().children(".shownatije").delay(200).fadeIn(500);
				$(this).parent().parent().parent().children(".showactiverate").addClass("rateavtive2");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive1");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive3");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive4");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive5");
			}else if(selectrate==3){
				$(this).parent().parent().parent().children(".shownatije").fadeOut(100);
				$(this).parent().parent().parent().children(".shownatije").delay(200).fadeIn(500);
				$(this).parent().parent().parent().children(".showactiverate").addClass("rateavtive3");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive2");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive1");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive4");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive5");
			}else if(selectrate==4){
				$(this).parent().parent().parent().children(".shownatije").fadeOut(100);
				$(this).parent().parent().parent().children(".shownatije").delay(200).fadeIn(500);
				$(this).parent().parent().parent().children(".showactiverate").addClass("rateavtive4");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive2");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive3");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive1");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive5");
			}else if(selectrate==5){
				$(this).parent().parent().parent().children(".shownatije").fadeOut(100);
				$(this).parent().parent().parent().children(".shownatije").delay(200).fadeIn(500);
				$(this).parent().parent().parent().children(".showactiverate").addClass("rateavtive5");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive2");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive3");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive4");
				$(this).parent().parent().parent().children(".showactiverate").removeClass("rateavtive1");
			}else{}
		}else{alert("امتیاز شما قبلا ثبت شده");}
	});
	$(".ratestar ul li").hover(function(){
		
		var selectrate = $(this).attr('data');
		
		if(selectrate==1){
			$(this).parent().parent().parent().children(".showresultrate").addClass("ratehover1");
		}else if(selectrate==2){
			$(this).parent().parent().parent().children(".showresultrate").addClass("ratehover2");
		}else if(selectrate==3){
			$(this).parent().parent().parent().children(".showresultrate").addClass("ratehover3");
		}else if(selectrate==4){
			$(this).parent().parent().parent().children(".showresultrate").addClass("ratehover4");
		}else if(selectrate==5){
			$(this).parent().parent().parent().children(".showresultrate").addClass("ratehover5");
		}else{}
	},function(){
	$(this).parent().parent().parent().children(".showresultrate").removeClass("ratehover1");	
	$(this).parent().parent().parent().children(".showresultrate").removeClass("ratehover2");
	$(this).parent().parent().parent().children(".showresultrate").removeClass("ratehover3");
	$(this).parent().parent().parent().children(".showresultrate").removeClass("ratehover4");
	$(this).parent().parent().parent().children(".showresultrate").removeClass("ratehover5");
	});
	
	
});