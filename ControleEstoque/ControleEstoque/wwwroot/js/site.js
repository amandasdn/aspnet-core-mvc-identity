// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(".cnpj").keydown(function () {
    try {
        $(".cnpj").unmask();
    } catch (e) { }

    //var tamanho = $(".cnpj").val().length;

    //if (tamanho < 11) {
    //    $(".cnpj").mask("999.999.999-99");
    //} else {
        $(".cnpj").mask("99.999.999/9999-99");
    //}

    // ajustando foco
    var elem = this;
    setTimeout(function () {
        // mudo a posição do seletor
        elem.selectionStart = elem.selectionEnd = 10000;
    }, 0);
    // reaplico o valor para mudar o foco
    var currentValue = $(this).val();
    $(this).val('');
    $(this).val(currentValue);
});