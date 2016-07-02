


$(function () {

    jQuery(document.body).on('click', ':button', function (event) {
        $('#myForm').trigger('reset');

    });
});