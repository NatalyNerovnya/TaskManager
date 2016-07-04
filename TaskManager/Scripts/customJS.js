
$(function () {
    $(document.body).on('change', '#myMiss', function (e) {
        var id = $(this).attr('class');
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Mission/MarkAsDone',
            data: postData,
            cache: false,
        });
        return false;
    });
});

$(function () {
    $(document.body).on('change', '#myTask', function (e) {
        var id = $(this).attr('class');
        var postData = { id: id };
        $.ajax({
            type: "POST",
            url: '/Home/MarkAsChecked',
            data: postData,
            cache: false,
        });
        return false;
    });
});

//$(function () {

//    jQuery(document.body).on('click', ':button', function (event) {
//        $('#myForm').trigger('reset');

//    });
//});