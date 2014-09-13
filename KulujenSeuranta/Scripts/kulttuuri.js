/// <reference path="jquery-1.10.2.js" />
/// <reference path="globalize/globalize.js" />

$(document).ready(function () {

    Globalize.culture("fi-FI");
    //alert(Globalize.culture().name);

    $('.datepicker').datepicker({
        format: "dd.mm.yyyy",
        viewMode: "weeks",
        minViewMode: "weeks",
        language: "fi",
        calendarWeeks: true,
        autoclose: true,
        todayHighlight: true
    });
});