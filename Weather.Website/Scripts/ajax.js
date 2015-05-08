var requestAjax = function (request, link) {
    return $.ajax({
        url: link,
        type: 'GET',
        data: $.param(request, true),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('.tabsContent').html(data);
        },
        error: function(data) {
            $('#container').html("Error");
        }
    });
};

$('.update-container').click(function (e) {
    e.preventDefault();

    var link = $(this).attr('href');
    var data = {
        CityId: $("#city-name").data("id"),
        Date: $(this).text(),
        Providers: [0, 1, 2]
    };
    requestAjax(data, link);
});
