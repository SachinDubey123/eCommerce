function Validate() {
    var isValid = true;
    if ($('#FullName').val().trim() == "") {
        $('#FullName').css('border-color', 'Red');
        $('#FullName').attr('title', 'FullName is required');
        isValid = false;
    }
    else {
        $('#FullName').css('border-color', 'lightgrey');
    }
    if ($('#Mobile').val().trim() == "" || (!$('#Mobile').val().match('[0-9]{10}')) || ($('#Mobile').val().trim().length != 10)) {
        $('#Mobile').css('border-color', 'Red');
        $('#Mobile').attr('title', 'Enter 10 digit Mobile number');
        isValid = false;
    }
    else {
        $('#Mobile').css('border-color', 'lightgrey');
    }

    if ($('#PinCode').val().trim() == "") {
        $('#PinCode').css('border-color', 'Red');
        $('#PinCode').attr('title', 'Postal Code/ZIP is required');
        isValid = false;
    }
    else {
        $('#PinCode').css('border-color', 'lightgrey');
    }
    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        $('#Address').attr('title', 'Address is required');
        isValid = false;
    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }


    if ($("#CountryId").val().length <= 0) {
        $('#CountryId').css('border-color', 'Red');
        $('#CountryId').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#CountryId').css('border-color', 'lightgrey');
    }


    if ($("#StateId").val()=="0" || $("#StateId").is(":disabled")) {
        $('#StateId').css('border-color', 'Red');
        $('#StateId').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#StateId').css('border-color', 'lightgrey');
    }

    if ($("#CityId").val() == "0" || $("#CityId").is(":disabled")) {
        $('#CityId').css('border-color', 'Red');
        $('#CityId').attr('title', 'Select one Role');
        isValid = false;
    }
    else {
        $('#CityId').css('border-color', 'lightgrey');
    }
    if ($("input[name='AddressType']:checked").length <= 0) {
        $("#AddressTypemsg").css('color', 'Red');
        $("#AddressTypemsg").attr('title', 'Select one Gender');
        isValid = false;
    }
    else {
        $("#AddressTypemsg").css('color', 'Black');

    }

    return isValid;

}


