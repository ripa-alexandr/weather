var init = function (objects) {
    objects.forEach(function(object) {
        object.forEach(function (objectName) {
            var item = window[objectName];
            if (typeof item === 'object' && typeof item.init === "function") {
                item.init();
            }
        });
    });
};

$(document).ready(function() {
    var initCity = [
       'details'
    ];

    init([
        initCity
    ]);
});

