function UpdateDb() {
    $.ajax({
        url: 'Controllers/UpdateDbController',
        type: 'Get',
        contentType: 'application/json;',
        data: JSON.stringify({ id: checkId }),
        success: function (valid) {
            if (valid) {
                //show that id is valid 
            } else {
                //show that id is not valid 
            }
        }
    });
}