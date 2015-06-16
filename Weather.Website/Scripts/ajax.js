var sendAjaxRequest = function (request, url) {
    $.ajax({
        url: url,
        type: 'GET',
        data: $.param(request, true),
        contentType: 'application/json; charset=utf-8',
        success: function (response) {
            $('.tabsContent').html(response);
        },
        error: function (response) {
            $('#container').html("Error");
        }
    });
};

var updateUrl = function (request, url) {
    var newUrl = url + "?" + $.param(request, true);
    history.pushState({ foo: "bar" }, newUrl, newUrl);
    $('input#url').val(newUrl);
};

$('.update-container').click(function (e) {
    e.preventDefault();

    var url = $(this).attr('href');
    var request = {
        CityId: $('#city-name').data('id'),
        Date: $(this).text(),
        Providers: $('#poviders option:selected').map(function () { return this.value; }).get()
    };
    sendAjaxRequest(request, url);
    updateUrl({ Providers: request.Providers }, url + "/" + request.Date);

});