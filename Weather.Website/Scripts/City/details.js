var details = (function() {
    var initHandlers = function() {
        $('.update-container').click(function (e) {
            e.preventDefault();

            var url = $(this).attr('href');
            var request = {
                CityId: $('#city-name').data('id'),
                Date: $(this).text(),
                Providers: $('#poviders option:selected').map(function () { return this.value; }).get()
            };
            sendAjaxRequest(request, url);
            updateUrls({ Providers: request.Providers }, url + "/" + request.Date);
        });

        var updateUrls = function (request, url) {
            // Update url in browser
            updateHistory(request, url);

            // Update url in page
            $('input#url').val(updateUrl(request, url));
        };

        $('.nav-tabs a').click(function () {
            $('.nav-tabs li').removeClass('active');
            $(this).parents('li').addClass('active');
        });

        $(window).load(function () {
            var tab = window.location.pathname.match(/\d{4}-\d{2}-\d{2}/i)
                ? window.location.pathname.match(/\d{4}-\d{2}-\d{2}/i).toString()
                : $('.nav-tabs li').first().text().trim();
            
            $('#' + tab).parents('li').addClass('active');
        });
    };

    return {
        init: initHandlers
    }
})();

