$(document).ready(function () {
    LoadData();
});
var t;

function LoadData() {
    t = $('#NewsTable').DataTable({
        "ajax": {
            "url": "/News/NewsLoadData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
             { "data": "Title", "autowidth": true },
              { "data": "Description", "autowidth": true },
                    
        {
            "data": "Photo",
            "render": function (data, type, row) {
                return '<image src="/Content/Images/News/' + data + '" style="height:100px;width:100px;" alt="Image" />';
            }
        },
                   

        ]
    });
}


function getbyID(ID) {
    $.ajax({
        url: "/News/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#Title').html("Details");

            $('#NewsId').val(result.NewsId);
            $('#Title').val(result.Title);
            //$('#Image').val(result.Image);
            $('#Description').val(result.Description);
          
           

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
            url: "/News/NewsDelete/" + ID,
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
    if ($('#Title').val().trim() == "") {
        $('#Title').css('border-color', 'Red');
        $('#Title').attr('title', 'Firstname is required');
        isValid = false;
    }
    else {
        $('#Title').css('border-color', 'lightgrey');
    }
    if ($('#Image').val().trim() == "") {
        $('#Image').css('border-color', 'Red');
        $('#Image').attr('title', 'Lastname is required');
        isValid = false;
    }
    else {
        $('#Image').css('border-color', 'lightgrey');
    }
    if ($('#Description').val().trim() == "") {
        $('#Description').css('border-color', 'Red');
        $('#Description').attr('title', 'Username is required');
        isValid = false;
    }
    else {
        $('#Description').css('border-color', 'lightgrey');
    }
    
    
    return isValid;

}
 

function clearModal() {
    $('#edit').hide();
    $('#save').show();
    $('#Title').html("Registration");

    $('#NewsId').val("");
    $('#Title').val("");
    
    $('#Description').val("");
   
    $('#NewsId').css('border-color', 'lightgrey');
    $('#Title').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#Image').css('border-color', 'lightgrey');
    $("#Image").css('color', 'Black');
}
