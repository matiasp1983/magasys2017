if (window.jQuery) {
    $(document).ready(function () {
        LoadDatePicker();
        LoadFootable();
    });
}

function LoadDatePicker() {
    $('#datePickerRange .input-daterange').datepicker({
        todayBtn: "linked",
        clearBtn: true,
        autoclose: true,
        language: "es",
        format: "dd/mm/yyyy"        
    });
}

function LoadFootable() {
    $('.footable').footable();
}

function CargarIdCuitModalProveedorBaja(control) {
    if (window.jQuery) {
        var loValores = control.lastElementChild.defaultValue;
        var loArreglo = loValores.split(",", 2);
        var loId = loArreglo[0];
        var loCuit = loArreglo[1];
        $(hdIdProveedorBaja).val(loId);
        $(lblCuitProveedorBaja).text(loCuit);
    }
}