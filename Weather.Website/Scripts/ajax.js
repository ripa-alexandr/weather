var sendAjaxRequest = function (request, link) {
    return $.ajax({
        url: link,
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

$('.update-container').click(function (e) {
    e.preventDefault();

    var link = $(this).attr('href');
    var request = {
        CityId: $("#city-name").data("id"),
        Date: $(this).text(),
        Providers: [0, 1, 2]
    };
    sendAjaxRequest(request, link);
});
