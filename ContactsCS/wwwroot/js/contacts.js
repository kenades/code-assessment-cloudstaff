function fnGetContacts() {
    var tbody = document.getElementById("contactsTbl").querySelector("tbody");
    fetch("https://localhost:7282/api/contactsapi")
        .then(result => {
            return result.json();
        })
        .then(data =>
            jQuery.each(data, function (key, val) {
                tbody.innerHTML += "<tr><td>" + val.Id + "</td>" + "<td>" + val.firstName + "</td>"
                    + "<td>" + val.lastName + " </td>" + "<td>" + val.companyName + "</td>" + "<td>" + val.mobile + "</td>"
                    + "<td>" + val.email + "</td>"
                    + "<td><button class='btn btn-primary btn-sm' data-id='" + val.Id + "' onclick='fnShowEditModal(event," + val.Id + ")'>Edit</button></td>" + "</tr>";
            })
        )
        .catch(error => console.log(error));
}
function fnAddContacts(e) {
    e.preventDefault();

    var formData = {
        firstName: $("#FirstName").val(),
        lastName: $("#LastName").val(),
        companyName: $("#CompanyName").val(),
        mobile: $("#Mobile").val(),
        email: $("#Email").val(),
    };

    $.ajax({
        type: 'POST',
        contentType: 'application/json',
        url: 'https://localhost:7282/api/contactsapi/add',
        data: JSON.stringify(formData),
        success: function (data) {
            $("#addContactModal").modal('hide');
            location.reload();
            
        },
        error: function (data) {
            alert('An error occurred during saving data. Please try again.');
        },
    });

    return false;
}

function fnShowEditModal(e, id) {
    e.preventDefault();
    $("#editContactModal").modal('show');

    $.ajax({
        type: 'GET',
        contentType: 'application/json',
        url: 'https://localhost:7282/api/contactsapi/getcontact/',
        data: { id: id },
        success: function (data) {
            $("#editContactId").val(data.Id);
            $("#editFirstName").val(data.firstName);
            $("#editLastName").val(data.lastName);
            $("#editCompanyName").val(data.companyName);
            $("#editMobile").val(data.mobile);
            $("#editEmail").val(data.email);
        },
        error: function (data) {
            alert('An error occurred during retrieving data. Please try again.');
        },
    });
}

function fnEditContact(e) {
    e.preventDefault();

    var id = $("#editContactId").val();
    var formData = {
        firstName: $("#editFirstName").val(),
        lastName: $("#editLastName").val(),
        companyName: $("#editCompanyName").val(),
        mobile: $("#editMobile").val(),
        email: $("#editEmail").val(),
    };

    $.ajax({
        type: 'PUT',
        contentType: 'application/json',
        url: 'https://localhost:7282/api/contactsapi/'+id,
        data: JSON.stringify(formData),
        success: function (data) {
            $("#editContactModal").modal('hide');
            location.reload();
        },
        error: function (data) {
            alert('An error occurred during saving. Please try again.');
        },
    });

    return false;
}

function fnSearchContact(e) {
    e.preventDefault();
    var tbody = document.getElementById("contactsTbl").querySelector("tbody");

    // Remove all child nodes (rows) from the tbody
    while (tbody.firstChild) {
        tbody.removeChild(tbody.firstChild);
    }

    $.ajax({
        type: 'GET',
        contentType: 'application/json',
        url: 'https://localhost:7282/api/contactsapi/searchcontact/',
        data: { search: $("#search").val() },
        success: function (data) {
            if (data.length == 0) {
                tbody.innerHTML += "<tr><td colspan=7> No record found.</td></tr>";
            }
            jQuery.each(data, function (key, val) {
                tbody.innerHTML += "<tr><td>" + val.Id + "</td>" + "<td>" + val.firstName + "</td>"
                    + "<td>" + val.lastName + "</td>" + "<td>" + val.companyName + "</td>" + "<td>" + val.mobile + "</td>"
                    + "<td>" + val.email + "</td>"
                    + "<td><button class='btn btn-primary btn-sm' data-id='" + val.Id + "' onclick='fnShowEditModal(event," + val.Id + ")'>Edit</button></td>" + "</tr>";
            });
        },
        error: function (data) {
            alert('An error occurred during search. Please try again.');
        },
    });
}