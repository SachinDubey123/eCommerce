$(function () {
    $("select").each(function () {
        if ($(this).find("option").length <= 1) {
            $(this).attr("disabled", "disabled");
        }
    });
    $("select").change(function () {
        var value = 0;
        if ($(this).val() != "") {
            value = $(this).val();
        }
        var id = $(this).attr("id");
        $.ajax({
            type: "POST",
            url: "/Order/AjaxMethod",
            data: '{type: "' + id + '",value: ' + value + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {
                var dropDownId;
                var list;
                switch (id) {
                    case "CountryId":
                        list = response.States;
                        DisableDropDown("#StateId");
                        DisableDropDown("#CityId");
                        PopulateDropDown("#StateId", list);
                        break;
                    case "StateId":
                        dropDownId = "#CityId";
                        list = response.Cities;
                        DisableDropDown("#CityId");
                        PopulateDropDown("#CityId", list);
                        break;
                }
            },
            failure: function (response) {
                alert(response.responseText);
            },
            error: function (response) {
                alert(response.responseText);
            }
        });
    });
});

function getbyID() {
    $.ajax({
        url: "/Order/GetbyID",
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#AddressId').val(result.AddressId);
            $('#FullName').val(result.FullName);
            $('#Mobile').val(result.Mobile);
            $('#PinCode').val(result.PinCode);
            $('#Address').val(result.Address);

            $("input[name='AddressType'][value='" + result.AddressType + "']").attr("checked", true);


            $("#CountryId option").prop('selected', false).filter(function () {
                return $(this).val() == result.CountryId;
                $("#StateId").removeAttr("disabled");
            }).prop('selected', true);

            var sid = result.StateId;
            var cid = result.CountryId;
            fillstate(cid);
            fillcity(sid);
            function fillstate(cid) {
                $.ajax({
                    url: '/Order/fillstate/' + cid,
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json;charset=UTF-8",
                    success: function (States) {
                        $("#StateId").removeAttr("disabled");
                        $("#StateId").html("");
                        $.each(States, function (i, state) {
                            $("#StateId").append($('<option></option>').val(this['Value']).html(this['Text']));

                        });
                    }
                });
            }
            function fillcity(sid) {
                $.ajax({
                    url: '/Order/fillcity/' + sid,
                    type: "POST",
                    dataType: "json",
                    contentType: "application/json;charset=UTF-8",
                    success: function (Cities) {
                        $("#CityId").removeAttr("disabled");
                        $("#CityId").html("");
                        $.each(Cities, function (i, city) {
                            $("#CityId").append($('<option></option>').val(this['Value']).html(this['Text']));

                        });
                    }
                });
            }

        },
        error: function (errormessage) {
            //debugger
            console.log(errormessage.responseText);
        }
    });
    return false;
}

function DisableDropDown(dropDownId) {
    $(dropDownId).attr("disabled", "disabled");
    $(dropDownId).empty().append('<option selected="selected" value="0">Please select</option>');
}
function PopulateDropDown(dropDownId, list) {
    if (list != null && list.length > 0) {
        $(dropDownId).removeAttr("disabled");
        $.each(list, function () { $(dropDownId).append($("<option></option>").val(this['Value']).html(this['Text'])); });
    }
}
$(function () {
    if ($("#CountryId").val() != "" && $("#StateId").val() != "" && $("#CityId").val() != "") {
        var message = "Country: " + $("#CountryId option:selected").text();
        message += "\nState: " + $("#StateId option:selected").text();
        message += "\nCity: " + $("#CityId option:selected").text();

    }
});
