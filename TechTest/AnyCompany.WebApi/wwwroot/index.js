
var customerApiUrl = 'api/Customer';
var orderApiUrl = 'api/Order';

function addCustomer() {
    event.preventDefault();
    var formData = new FormData(document.getElementById("frmAddCustomer"));
    fetch(customerApiUrl, {
            method: 'POST',
            body: formData
        })
        .then(async (response) => {
            if (!response.ok) {
                await showErrors(response);
                return;
            }
            showSuccessfulMessage("Customer added");
            document.getElementById("frmAddCustomer").reset();
        })
        .catch((error) => {
            showErrors("Unexpected error occured." + error);
        });
}

async function showErrors(errorResponse) {

    if (typeof errorResponse === "string") {
        document.getElementById("errorList").innerHTML = errorResponse;
    }
    else {
        var errorResponseJson = await errorResponse.json();
        var errors = "<li>";
        errors += errorResponseJson.message;

        errors += "<ul>";
        for (errorMessage of errorResponseJson.errorList) {
            errors += "<li>" + errorMessage + "</li>";
        }
        errors += "</ul>";

        errors += "</li>";
        document.getElementById("errorList").innerHTML = errors;
    }

    document.getElementById("errorWidget").hidden = false;
}

function clearErrors() {

    document.getElementById("errorList").innerHTML = "";
    document.getElementById("errorWidget").hidden = true;
}

function showSuccessfulMessage() {
    document.getElementById("successWidget").hidden = false;
}

function hideSuccessfulMessage() {
    document.getElementById("successWidget").hidden = true;
}