$(document).ready(function () {
    LoadData();
});
var t;

function LoadData() {
    t = $('#ProductTable').DataTable({
        "ajax": {
            "url": "/Product/ProductLoadData",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
             { "data": "Name", "autowidth": true },
        {
            "data": "Image",
            "render": function (data, type, row) {
                var imgstring = "";
                imgstring += '<a href="/Product/Images/' + data[0].ProductId + '" ><image src="/Content/Images/Products/' + data[0].Filename + '" style="height:100px;width:100px;" alt="Image" /></a>';
                return imgstring;
            }
        },
                    { "data": "Description", "autowidth": true },
                    { "data": "Price", "autowidth": true },
                    { "data": "SKU", "autowidth": true },
                    { "data": "UPC", "autowidth": true },
                    { "data": "TotalQty", "autowidth": true },
                    { "data": "BrandName", "autowidth": true },

                    { "data": "IsOutOfStock", "autowidth": true },

                    { "data": "IsFeaturedProduct", "autowidth": true },
                    { "data": "IsLatestProduct", "autowidth": true },
                    { "data": "IsPopular", "autowidth": true },
                    { "data": "Remarks", "autowidth": true },
                    { "data": "CategoryName", "autowidth": true },
                    { "data": "FashionFor", "autowidth": true },


                    {
                        "data": "ProductId", "width": "50px", "render": function (data) {
                            return '<button onclick="return getbyID(' + data + ')"> <span class="glyphicon glyphicon-edit">Edit </span></button> ';
                        }
                    },
                    {
                        "data": "ProductId", "width": "50px", "render": function (data) {
                            return '<button onclick="Delete(' + data + ')"><span class="glyphicon glyphicon-trash">Delete </span></button>';
                        }
                    }

        ]
    });
}


function getbyID(ID) {
    $.ajax({
        url: "/Product/GetbyID/" + ID,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {

            $('#Title').html("Details");

            $('#ProductId').val(result.ProductId);
            $('#Name').val(result.Name);
            //$('#Image').val(result.Image);
            $('#Description').val(result.Description);
          
            $('#Price').val(result.Price);
            $('#SKU').val(result.SKU);
            $('#UPC').val(result.UPC);
            $('#TotalQty').val(result.TotalQty);
            $('#BrandName').val(result.BrandName);
            $('#Remarks').val(result.Remarks);

            $("#CategoryId option").prop('selected', false).filter(function () {
                return $(this).text() == result.CategoryName;
            }).prop('selected', true);
            
            $("#FashionFor option").prop('selected', false).filter(function () {
                return $(this).text() == result.FashionFor;
            }).prop('selected', true);

            $("#IsOutOfStock option").prop('selected', false).filter(function () {
                return $(this).val() == result.IsOutOfStock;
            }).prop('selected', true);

            $("#IsFeaturedProduct option").prop('selected', false).filter(function () {
                return $(this).val() == result.IsFeaturedProduct;
            }).prop('selected', true);

            $("#IsLatestProduct option").prop('selected', false).filter(function () {
                return $(this).val() == result.IsLatestProduct;
            }).prop('selected', true);

            $("#IsPopular option").prop('selected', false).filter(function () {
                return $(this).val() == result.IsPopular;
            }).prop('selected', true);

       

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
            url: "/Product/ProductDelete/" + ID,
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
    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'Red');
        $('#Price').attr('title', 'Price is required');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }

    if ($('#SKU').val().trim() == "") {
        $('#SKU').css('border-color', 'Red');
        $('#SKU').attr('title', 'SKU is required');
        isValid = false;
    }
    else {
        $('#SKU').css('border-color', 'lightgrey');
    }

    if ($('#UPC').val().trim() == "") {
        $('#UPC').css('border-color', 'Red');
        $('#UPC').attr('title', 'UPC is required');
        isValid = false;
    }
    else {
        $('#UPC').css('border-color', 'lightgrey');
    }
    if ($('#TotalQty').val().trim() == "") {
        $('#TotalQty').css('border-color', 'Red');
        $('#TotalQty').attr('title', 'Username is required');
        isValid = false;
    }
    else {
        $('#TotalQty').css('border-color', 'lightgrey');
    }
    if ($('#BrandName').val().trim() == "") {
        $('#BrandName').css('border-color', 'Red');
        $('#BrandName').attr('title', 'Username is required');
        isValid = false;
    }
    else {
        $('#BrandName').css('border-color', 'lightgrey');
    }

    
    if ($('#Image').val().trim() == "") {
        $("#Image").css('color', 'Red');
        $('#Image').attr('title', 'Select one Photo');
        isValid = false;
    }
    else {
        $("#Image").css('color', 'Black');
    }
    if ($("#IsOutOfStock").val().length <= 0) {
        $('#IsOutOfStock').css('border-color', 'Red');
        $('#IsOutOfStock').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#IsOutOfStock').css('border-color', 'lightgrey');
    }
    if ($("#IsFeaturedProduct").val().length <= 0) {
        $('#IsFeaturedProduct').css('border-color', 'Red');
        $('#IsFeaturedProduct').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#IsFeaturedProduct').css('border-color', 'lightgrey');
    }
    if ($("#IsLatestProduct").val().length <= 0) {
        $('#IsLatestProduct').css('border-color', 'Red');
        $('#IsLatestProduct').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#IsLatestProduct').css('border-color', 'lightgrey');
    }
    if ($("#IsPopular").val().length <= 0) {
        $('#IsPopular').css('border-color', 'Red');
        $('#IsPopular').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#IsPopular').css('border-color', 'lightgrey');
    }

    if ($('#Remarks').val().trim() == "") {
        $('#Remarks').css('border-color', 'Red');
        $('#Remarks').attr('title', 'Username is required');
        isValid = false;
    }
    else {
        $('#Remarks').css('border-color', 'lightgrey');
    }
    if ($("#CategoryId").val().length <= 0) {
        $('#CategoryId').css('border-color', 'Red');
        $('#CategoryId').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#CategoryId').css('border-color', 'lightgrey');
    }

    if ($("#FashionFor").val().length <= 0) {
        $('#FashionFor').css('border-color', 'Red');
        $('#FashionFor').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#FashionFor').css('border-color', 'lightgrey');
    }

    return isValid;

}
 

function clearModal() {
    $('#edit').hide();
    $('#save').show();
    $('#Title').html("Registration");

    $('#ProductId').val("");
    $('#Name').val("");
    
    $('#Description').val("");
    $('#Price').val("");
    $('#SKU').val("");
    $('#UPC').val("");

    $('#TotalQty').val("");

    $('#BrandName').val("");
    $('#Remarks').val("");
    $("#IsOutOfStock option").prop('selected', false);
    
    
    $("#IsFeaturedProduct option").prop('selected', false);


    $("#IsLatestProduct option").prop('selected', false);


    $("#IsPopular option").prop('selected', false);
    $("#CategoryId option").prop('selected', false);
    $("#FashionFor option").prop('selected', false);

    $('#ProductId').css('border-color', 'lightgrey');
    $('#Name').css('border-color', 'lightgrey');
    $('#Description').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $('#SKU').css('border-color', 'lightgrey');

    $('#UPC').css('border-color', 'lightgrey');

    $('#TotalQty').css('border-color', 'lightgrey');
    $('#Image').css('border-color', 'lightgrey');
    $("#Image").css('color', 'Black');
    $('#BrandName').css('border-color', 'lightgrey');
    $("#Photo").css('color', 'Black');

    $('#IsOutOfStock').css('border-color', 'lightgrey');

    $('#IsFeaturedProduct').css('border-color', 'lightgrey');
    $('#IsLatestProduct').css('border-color', 'lightgrey');

    $('#IsPopular').css('border-color', 'lightgrey');

    $('#Remarks').css('border-color', 'lightgrey');
    $('#FashionFor').css('border-color', 'lightgrey');
}
