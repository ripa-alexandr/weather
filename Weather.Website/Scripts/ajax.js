var requestAjax = function (item, link) {
    return $.ajax({
        url: link,
        type: 'POST',
        data: JSON.stringify(item),
        contentType: 'application/json; charset=utf-8',
        success: function (data) {
            $('#container').html(data);
        },
        error: function(data) {
            $('#container').html("Error");
        }
    });
};

$('#update-container').click(function (e) {
    e.preventDefault();
    var link = $(this).attr('href');
    var data = { CityId: 1, Date: new Date(2015, 03, 10, 15, 00, 00), Providers: [1] };
    requestAjax(data, link);
});