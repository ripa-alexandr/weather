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

var updateHistory = function (request, url) {
    var newUrl = updateUrl(request, url);
    history.pushState({ foo: "bar" }, newUrl, newUrl);
};

var updateUrl = function (request, url) {
    return url + "?" + $.param(request, true);
};

