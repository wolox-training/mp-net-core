// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('#submitBtn').click(function () {
        var userName = $('#formUserName').val();
        var text = $('#formText').val();
        var movieId = $('#movieId').val();
        var commentsQty = parseInt($('#commentsQty').data().qty);
        $.ajax({
            type: "POST",
            url: "../api/v1/Comment/AddComment",
            data: { "id": movieId, "text": text },
            success: function (result) {
                alert("Thanks for your comment!");
            },
            error: function (jqXHR, state, error) {
                alert("The comment cannot be added");
            },
            complete: function (jqXHR, state) {
                $('#commentsTable').append( `<dl> <td>${commentsQty} | <strong>${userName}</strong> says:</td> <td><i>${text}</i></td></dl>`);
                $('#commentsQty').html(commentsQty+1);
            }
        })
    })
});
