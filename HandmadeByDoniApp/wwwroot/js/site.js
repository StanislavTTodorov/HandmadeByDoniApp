// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    $('#culture-select').addClass('button-menu').select2({
        minimumResultsForSearch: Infinity, // Скрива полето за търсене
        templateResult: function (data) {
            if (!data.element) return data.text;
            var flagUrl = $(data.element).data('flag');
            if (flagUrl) {
                return $('<span><img src="' + flagUrl + '" style="width: 40px; margin-right: 5px;"> </span>');
            }

        },
        templateSelection: function (data) {
            if (!data.element) return data.text;
            var flagUrl = $(data.element).data('flag');
            if (flagUrl) {
                return $('<span><img src="' + flagUrl + '" style="width: 40px; margin-right: 5px;"> </span>');
            }
            return data.text;
        }
    });
});
