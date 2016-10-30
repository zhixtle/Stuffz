$(document).ready(function () {
    $('#txtBirthday').datetimepicker({
        maxDate: $.now(),
        minDate: moment(new Date('01-01-1800')),
        format: 'L',
        showClose: true,
        showClear: true,
        useCurrent: true,
        toolbarPlacement: 'top'
    });
});