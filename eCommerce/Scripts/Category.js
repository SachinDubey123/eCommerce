$(document).ready(function () {
    LoadData();
});
var t;

function LoadData() {
    t = $('#CategoryTable').DataTable({
        "ajax": {
            "url": "/Category/CategoryLoadData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
                      { "data": "CategoryId", "autowidth": true },

                    { "data": "Name", "autowidth": true },
                   
                    {
                        "data": "CategoryId", "width": "50px", "render": function (data) {
                            return '<button onclick="return getbyID(' + data + ')"> <span class="glyphicon glyphicon-edit">Edit </span></button> ';
                        }
                    },
                    {
                        "data": "CategoryId", "width": "50px", "render": function (data) {
                            return '<button onclick="Delete(' + data + ')"><span class="glyphicon glyphicon-trash">Delete </span></button>';
                        }
                    }

        ]
    });
}


function getbyID(ID) {
    $.ajax({
        url: "/Category/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#register').html("Details");

            $('#CategoryId').val(result.CategoryId);
            $('#Name').val(result.Name);
            //$('#Image').val(result.Image);
           


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
            url: "/Category/CategoryDelete/" + ID,
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
    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        $('#Name').attr('title', 'Firstname is required');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    
    return isValid;

}


function clearModal() {
    $('#edit').hide();
    $('#save').show();

    $('#register').html("Registration");

    $('#CategoryId').val("");
    $('#Name').val("");

    }
