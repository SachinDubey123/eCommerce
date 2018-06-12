$(document).ready(function () {
    LoadData();
});
var t;
function LoadData() {
    t = $('#AdminTable').DataTable({
        "ajax": {
            "url": "/User/AdminLoadData",
            "type": "POST",
            "datatype": "json"
        },
        "columns": [
             
        {
            "data": "Photo",
            "render": function (data, type, row) {
                return '<image src="/Content/Images/Photos/' + data + '" style="height:100px;width:100px;" alt="Image" />';
            }
        },
                    { "data": "Username", "autowidth": true },
                    { "data": "Firstname", "autowidth": true },
                    { "data": "Lastname", "autowidth": true },
                    { "data": "Gender", "autowidth": true },
                    { "data": "Mobile", "autowidth": true },
                    { "data": "Email", "autowidth": true },
                    { "data": "Role", "autowidth": true },
                    {
                        "data": "AdminId", "width": "50px", "render": function (data) {
                            return '<button onclick="return getbyID(' + data + ')"> <span class="glyphicon glyphicon-edit">Edit </span></button> ';
                        }
                    },
                    {
                        "data": "AdminId", "width": "50px", "render": function (data) {
                            return '<button onclick="Delete(' + data + ')"><span class="glyphicon glyphicon-trash">Delete </span></button>';
                        }
                    }

        ]
    });
}

function getbyID(ID) {
    $.ajax({
        url: "/User/GetbyID/" + ID,
        type: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#trPassword').hide();
            $('#AdminId').val(result.AdminId);
            $('#Username').val(result.Username);
            $('#Firstname').val(result.Firstname);
            $('#Lastname').val(result.Lastname);
            $("input[name='Gender'][value='" + result.Gender + "']").attr("checked", true);
            $('#Mobile').val(result.Mobile);
            $('#Email').val(result.Email);
            $("#Role option").prop('selected', false).filter(function () {
                return $(this).val() == result.Role;
            }).prop('selected', true);
            $('#Image').html("<td rowspan='3'><img src='/Content/Images/Photos/" + result.Photo + "' style='border-radius:30%;height:100px;width:100px;' /></td>");
            $('#RegisterModal').modal({
                backdrop: 'static',
                keyboard: false
            });
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
            url: "/User/UserDelete/" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                t.ajax.reload();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}
function Validate() {
    var isValid = true;
    if ($('#Firstname').val().trim() == "") {
        $('#Firstname').css('border-color', 'Red');
        $('#Firstname').attr('title', 'Firstname is required');
        isValid = false;
    }
    else {
        $('#Firstname').css('border-color', 'lightgrey');
    }
    if ($('#Lastname').val().trim() == "") {
        $('#Lastname').css('border-color', 'Red');
        $('#Lastname').attr('title', 'Lastname is required');
        isValid = false;
    }
    else {
        $('#Lastname').css('border-color', 'lightgrey');
    }
    if ($('#Username').val().trim() == "") {
        $('#Username').css('border-color', 'Red');
        $('#Username').attr('title', 'Username is required');
        isValid = false;
    }
    else {
        $('#Username').css('border-color', 'lightgrey');
    }
    if ($('#Mobile').val().trim() == "" || (!$('#Mobile').val().match('[0-9]{10}')) || ($('#Mobile').val().trim().length!=10))  {
        $('#Mobile').css('border-color', 'Red');
        $('#Mobile').attr('title', 'Enter 10 digit Mobile number');
        isValid = false;
    }
    else {
        $('#Mobile').css('border-color', 'lightgrey');
    }
    var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([com\co\.\in])+$/;
    if ($('#Email').val().trim() == "" || (!($('#Email').val().match(emailExp)))) {
        $('#Email').css('border-color', 'Red');
        $('#Email').attr('title', 'Enter Email in proper formate');
        isValid = false;
    }
    else {
        $('#Email').css('border-color', 'lightgrey');
    }
    $('#save').click(function () {
    if ($('#Password').val().trim() == "" || $('#Password').val().length < 6) {
        $('#Password').css('border-color', 'Red');
        $('#Password').attr('title', 'Enter minimum 6 letters Password');
        isValid = false;
    }
    else {
        $('#Password').css('border-color', 'lightgrey');
    }
    });
    if ($("input[name='Gender']:checked").length <= 0) {
        $("#radiomsg").css('color', 'Red');
        $("input[name='Gender']").attr('title', 'Select one Gender');
        isValid = false;
    }
    else {
        $("#radiomsg").css('color', 'Black');

    }
    if ($('#Photo').val().trim() == "") {
        $("#Photo").css('color', 'Red');
        $('#Photo').attr('title', 'Select one Photo');
        isValid = false;
    }
    else {
        $("#Photo").css('color', 'Black');
    }
    if ($("#Role").val().length <= 0) {
        $('#Role').css('border-color', 'Red');
        $('#Role').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#Role').css('border-color', 'lightgrey');
    }
    return isValid;

}
function clearModal() {
    $('#edit').hide();
    $('#save').show();
    $('#trPassword').show();

    $('#AdminId').val("");
    $('#Photo').html(" ");
    $('#Firstname').val("");
    $('#Lastname').val("");
    $('#Username').val("");
    $('#Email').val("");
    $('#Password').val("");
    $("input[name='Gender'][value='Male']").attr("checked", false);
    $("input[name='Gender'][value='Female']").attr("checked", false);
    $('#Mobile').val("");
    $("#Role option").prop('selected', false);

    $('#Firstname').css('border-color', 'lightgrey');
    $('#Lastname').css('border-color', 'lightgrey');
    $('#Username').css('border-color', 'lightgrey');
    $('#Mobile').css('border-color', 'lightgrey');
    $('#Password').css('border-color', 'lightgrey');
    $("#radiomsg").css('color', 'Black'); $('#Email').css('border-color', 'lightgrey');
    $("#Photo").css('color', 'Black');
    $('#Role').css('border-color', 'lightgrey');
}
