// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(function()
    {
        $('#submitBtn').click(function()
        {
            var movieId = $('#movieId').val();
            $.ajax({
                type: "GET",
                url: "../api/v1/Comment/Comments",
                dataType: "json",
                data: {id: movieId},
                success: function (result)
                {
                    $('#commentsTable').empty();
                    result.foreach(function(element){
                        console.log(element);
                    });
                    /* {
                        var comment = 
                        ` <dl class="dl-horizontal">
                        <td>
                         | <strong> </strong> says:
                        </td>
                        <td>
                        <i>"@Html.DisplayFor(modelItem => item.Text)"</i>
                        </td>
                        </dl> ` ;
                    } */
                    alert("El comentario ha sido cargado");
                },
                error: function (error)
                {
                    debugger;
                    alert("Hubo un error");
                }
            })
        })
});
