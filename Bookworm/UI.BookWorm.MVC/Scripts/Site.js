$(document).ready(function () {


    $(".search-field").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".data-row").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });

    $(".star-search").change(function () {
        var value = $(this).val();
        if (value == 0) {
            $(".data-row").show();
        }
        else {
            $(".data-row").hide()
            $(".rating-" + value).show();        }
    })
    
 
});