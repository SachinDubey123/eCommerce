$(document).ready(function () {
    $('#spCart').on('click', function () {
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: '/Clothes/GetCartProducts',

            success: function (data) {
                $('#CartModal').html(data);
                $('#CartModal').modal({
                    backdrop: false
                });
            },
            error: function (data) {
                alert(data.html());
            }
        });
    });

    $(document).delegate('#cartDel', 'click', function () {
        var DeleteItem = parseInt($(this).closest('tr').prop('id'));
        $.ajax({
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            url: '/Clothes/DeleteCart',
            data: "{ 'id':' " + DeleteItem + "' }",
            success: function (data) {
                $('#CartModal').html(data);
                $('#spCart').html($(data).find('#cartList tbody tr').length);
                $('#CartModal').modal({
                    backdrop: false
                });
            },
            error: function (data) {
                alert(data);
            }
        });
    });

    $(document).delegate('input[id="Qty"]', 'change', function () {
        if (this.id == "Qty") {
            var CartItemId = parseInt($(this).attr('name'));
            var Quantity = parseInt($(this).val());
            //alert(ProductId + "  " + Quantity);
            //var data = new FormData();
            //data.append("CartItemId", CartItemId);
            //data.append("Quantity", Quantity);
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: '/Clothes/EditQuantity',
                data: "{ 'CartItemId':' " + CartItemId + "', 'Quantity':' " + Quantity + "'}",
                success: function (data) {
                    $('#CartModal').html(data);
                    $('#spCart').html($(data).find('#cartList tbody tr').length);
                    $('#CartModal').modal({
                        backdrop: false
                    });
                },
                error: function (data) {
                    alert(data);
                }
            });
        }
    });

});