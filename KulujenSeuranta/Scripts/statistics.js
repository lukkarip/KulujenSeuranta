/// <reference path="jquery-1.10.2.js" />
/// <reference path="globalize/globalize.js" />

$(document).ready(function () {

    Globalize.culture("fi-FI");

    $('#datepicker').datepicker({
        format: "mm-yyyy",
        viewMode: "months",
        minViewMode: "months",
        language: "fi",
        calendarWeeks: true,
        autoclose: false,
        todayHighlight: true
    })
    //.on('hide', function (e) {
    //    $('#datepicker').datepicker('show');
    //});
    //
    //$('#datepicker').datepicker('show');

});