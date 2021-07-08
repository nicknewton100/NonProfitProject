// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

<script type="text/javascript">
    var buttonClicked = null;

function highlight(element) {
    if (buttonClicked != null) {
        buttonClicked.style.background = "grey";
        buttonClicked.style.color = "black";
    }
    buttonClicked = element;
    buttonClicked.style.background = "red";
    buttonClicked.style.color = "white";
}
</script>