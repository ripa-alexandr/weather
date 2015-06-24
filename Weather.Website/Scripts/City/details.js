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
