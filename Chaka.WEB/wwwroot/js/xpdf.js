(function () {  
    var area = $('.area');
    var cache_width = area.width();
    var a4 = [595, 842]; // for a4 size paper width and height

    $('#create_pdf').on('click', function () {
        $('body').scrollTop(0);
        if(Number(window.innerWidth) > 768) {
            $( ".toggle-btn a" ).trigger( "click" );
        }

        createPDF();

        if(Number(window.innerWidth) > 768) {
            $( ".toggle-btn a" ).trigger( "click" );
        }

        area.css({
            "margin": "auto", 
            "width": "100%",
        });
    });

    function createPDF() {
        getCanvas().then(function (canvas) {
            var
            img = canvas.toDataURL("image/png"),
            doc = new jsPDF({
                orientation: 'landscape',
                unit: 'px',
                format: 'a4'
            });

            doc.addImage(img, 'JPEG', 20, 20);
            doc.save('cmiw-html-to-pdf.pdf');
            area.width(cache_width);
        });
    }

    // create canvas object
    function getCanvas() {
        area.width((a4[1])).css('max-width', 'none');
        return html2canvas(area, {
            imageTimeout: 2000,
            removeContainer: true
        });
    }
}());