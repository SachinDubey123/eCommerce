$(document).ready(function () {
    LoadData();
});

function LoadData() {
    $.ajax({
        url: "/Manage/CountryLoadData",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (json) {
            var tr;
            //Append each row to html table
            for (var i = 0; i < json.length; i++) {
                tr = $('<tr/>');
                tr.append("<td>" + json[i].Name + "</td>");
                tr.append("<td>" + json[i].Remarks + "</td>");
                tr.append("<td><button onclick='return getbyID(" + json[i].CountryId + ")'>Edit</button>  <button onclick='Delete(" + json[i].CountryId + ")'>Delete</button></td>");
                $('table#CountryTable').append(tr);
            }
            var tbl = $('#CountryTable').DataTable();
        }
    });
}
function getbyID(ID) {
    $.ajax({
        url: "/Manage/GetCoutrybyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#CountryId').val(result.CountryId);
            $('#Name').val(result.Name);
            $('#Remarks').val(result.Remarks);
            $('#RegisterModal').modal();
            $('#edit').show();
            $('#save').hide();
        },
        error: function (errormessage) {
            //debugger
            console.log(errormessage.responseText);
        }
    });
    return false;
}
function Delete(ID) {
    var ans = confirm("Are you sure you want to delete this Record?");
    if (ans) {
        $.ajax({
            url: "/Manage/CountryDelete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                tbl.clear();
                LoadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function Validate() {
    var isValid = true;
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        $('#Name').attr('title', 'Firstname is required');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Remarks').val().trim() == "") {
        $('#Remarks').css('border-color', 'Red');
        $('#Remarks').attr('title', 'Lastname is required');
        isValid = false;
    }
    else {
        $('#Remarks').css('border-color', 'lightgrey');
    }
    return isValid;

}