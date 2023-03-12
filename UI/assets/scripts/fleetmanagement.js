var serverUrl = 'https://localhost:7117/';
var vehicleData = [];
var pendingDeleteId;

$(document).ready(function () {
    loadVehicles();
    $("#add").click(newVehicle);
    getVehicleTypes();
    $(".form-control").blur(function () { validateAddForm(this) });

    $("#vehicleEdit").dialog({
        autoOpen: false,
        buttons: {
            "OK": function () { saveVehicle(); },
            "Cancel": function () { $("#vehicleEdit").dialog('close'); }
        },
        modal: true,
        resizable: false,
        title: 'Edit Vehicle',
        width: 320,
        height: 250
    })
    $("#vehicleDelete").dialog({
        autoOpen: false,
        buttons: {
            "Yes": deleteVehicleAction,
            "No": function () { $("#vehicleDelete").dialog('close'); }
        },
        modal: true,
        resizable: false,
        title: 'Edit Vehicle',
        width: 320,
        height: 220
    })
    $("#vehicleAdd").dialog({
        autoOpen: false,
        buttons: [
            {
                id: "btnOk",
                text: "OK",
                click: addVehicle
            },
            {
                id: "btnCancel",
                text: "Cancel",
                click: function () { $("#vehicleAdd").dialog('close'); }
            }
        ],
        modal: true,
        resizable: false,
        title: 'Add Vehicle',
        width: 490,
        height: 600
    })
});

function loadVehicles() {
    $.ajax({
        type: "GET",
        url: serverUrl + "Vehicle/getall",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var table$ = $("#vehicleData");
            vehicleData = [];
            $(data).each(function (index, element) {
                row = createVehicleArray(element);
                vehicleData.push(row);
            })
            $("#vehicles").DataTable({
                data: vehicleData,
                search: {smart: false}
            });
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function createVehicleArray(vehicle) {
    var row = new Array();
    var link = "<a href='javascript:editChassis(\"" + vehicle.chassisId + "\");'>";
    link += vehicle.chassisId;
    link += "</a>";
    row.push(link);
    row.push(vehicle.chassisSeries);
    row.push(vehicle.chassisNumber);
    row.push(vehicle.vehicleType);
    row.push(vehicle.passengers);
    row.push(vehicle.color);

    var del = "<a title='Delete this vehicle' class='delete-row' href='javascript:deleteVehiclePrompt(\"" + vehicle.id + "\");'>X</a>";
    row.push(del);
    return row;
}

function refreshVehicles() {
    $.ajax({
        type: "GET",
        url: serverUrl + "Vehicle/getall",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            var table$ = $("#vehicleData");
            vehicleData = [];
            $(data).each(function (index, element) {
                row = createVehicleArray(element);
                vehicleData.push(row);
            })
            $('#vehicles').DataTable().clear().rows.add(vehicleData).draw();
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function editChassis(chassisId) {
    var url = serverUrl + "Vehicle/get?chassisId=" + chassisId;
    $.ajax({
        type: "GET",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#vehicleId").val(data.id);
            $("#chassisId").html(chassisId);
            $("#colorNew").val(data.color);
            $("#vehicleEdit").dialog("open");
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function saveVehicle() {
    var url = serverUrl + "Vehicle/update";
    $.ajax({
        type: "PUT",
        url: url,
        contentType: "application/json; charset=utf-8",
        data: "{" +
            '"id": "' + $("#vehicleId").val() + '",' +
            '"color": "' + $("#colorNew").val() + '"}',
        dataType: "json",
        success: function (data) {
            refreshVehicles();
            $("#vehicleEdit").dialog('close');
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function deleteVehiclePrompt(id) {
    pendingDeleteId = id;
    $("#vehicleDelete").dialog("open");
}

function deleteVehicleAction() {
    var url = serverUrl + "Vehicle/delete?vehicleId=" + pendingDeleteId;
    $.ajax({
        type: "DELETE",
        url: url,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            refreshVehicles();
            $("#vehicleDelete").dialog('close');
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function newVehicle() {
    // Reset form for new data
    $("#id").val("");
    $("#series").val("");
    $("#number").val("");
    $("#vehicleType").val(0);
    $("#color").val("");
    $("#btnOk").button("disable");
    $(".invalid-feedback").hide()
    $(".form-control").removeClass("is-invalid");
    $("#vehicleAdd").dialog("open");
}

function getVehicleTypes() {
    $.ajax({
        type: "GET",
        url: serverUrl + "Vehicle/types/get",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {            
            $(data).each(function (index, element) {
                var option$ = $("<option/>");
                option$
                    .attr("value", element.id)
                    .html(element.description);
                $("#vehicleType").append(option$);
            })            
        },
        error: function (error) {
            alert("Error communicating with API");
        }
    });
}

function addVehicle() {
    if (validateAddForm()) {
        var url = serverUrl + "Vehicle/create";
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            data: "{" +
                '"chassisId": "' + $("#id").val() + '",' +
                '"chassisSeries": "' + $("#series").val() + '",' +
                '"chassisNumber": "' + $("#number").val() + '",' +
                '"vehicleTypeId": "' + $("#vehicleType").val() + '",' +
                '"color": "' + $("#color").val() + '"}',
            dataType: "json",
            success: function (data) {
                refreshVehicles();
                $("#vehicleAdd").dialog('close');
            },
            error: function (error) {
                alert("Error communicating with API");
            }
        });
    }
}

function validateAddForm(control) {
    if (control) {
        if ($(control).id == "vehicleType") {
            if ($(control).val() == 0) {
                $(control).addClass("is-invalid").closest("td").find(".invalid-feedback").show();
            }
            else {
                $(control).removeClass("is-invalid").closest("td").find(".invalid-feedback").hide();
            }
        }
        else {
            if ($(control).val().length == 0) {
                $(control).addClass("is-invalid").closest("td").find(".invalid-feedback").show();
            }
            else {
                $(control).removeClass("is-invalid").closest("td").find(".invalid-feedback").hide();
            }
        }
    }

    var isInvalid = false

    $(".form-control").each(function (i, e) {
        if ($(e).val().length == 0) {
            isInvalid = true;
        }
    })
    if ($("#vehicleType").val() == 0) {
        isInvalid = true;
    }

    if (!isInvalid) {
        $("#btnOk").button("enable");
    }
    else {
        $("#btnOk").button("disable");
    }

    return !isInvalid;
}