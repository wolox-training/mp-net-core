// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function () {
    $('#submitBtn').click(function () {
        var userName = $('#formUserName').val();
        var text = $('#formText').val();
        var movieId = $('#movieId').val();
        $.ajax({
            type: "POST",
            url: "../api/v1/Comment/AddComment",
            data: { "id": movieId, "text": text },
            success: function (result) {
                debugger;
                alert("Todo piola");
            },
            error: function (jqXHR, state, error) {
                debugger;
                alert("Todo maaaal");
            },
            complete: function (jqXHR, state) {
                $('#commentsTable').append("<dl> <td> # | <strong>" + userName + "</strong> says:</td> <td> <i>" + text + "</i></td></dl>");
            }
        })
    })
});
