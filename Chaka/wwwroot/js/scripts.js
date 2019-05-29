$(document).ready(function() {

    // side nav container height
    var sideNav = $('.side-nav-container');
    var sideHeight = String(Number(window.innerHeight) - 60) + "px";
    sideNav.css('height', sideHeight);

    // body section height
    var body = $('.body-section');
    var mainHeight = String(Number(window.innerHeight) - 185) + "px";
    body.css('min-height', mainHeight);

    // collapse menu
    $(".collapsible-trigger").click(function(){
        $(this).next().slideToggle();
    });

    $(".toggle-btn a").click(function() {
        if(Number(window.innerWidth) > 768) {
            sideNav.removeClass("show-nav");
            sideNav.toggleClass("hide-nav");
            $(".main-container").toggleClass("remove-margin");
        } else {
            sideNav.removeClass("hide-nav");
            sideNav.toggleClass("show-nav");
        }
    });

    // tooltip n popover
    $('[data-toggle="tooltip"]').tooltip();   
    $('[data-toggle="popover"]').popover();

    $(window).resize(function() { 
        // side nav container height
        var sideNav = $('.side-nav-container');
        var sideHeight = String(Number(window.innerHeight) - 60) + "px";
        sideNav.css('height', sideHeight);

        // body section height
        var body = $('.body-section');
        var mainHeight = String(Number(window.innerHeight) - 185) + "px";
        body.css('min-height', mainHeight);

        $(".toggle-btn a").click(function() {
            if(Number(window.innerWidth) > 768) {
                sideNav.toggleClass("hide-nav");
                $(".main-container").toggleClass("remove-margin");
            } else {
                sideNav.toggleClass("show-nav");
            }
        });
    });
});