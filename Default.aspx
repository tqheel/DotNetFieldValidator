<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.16/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <title>Validation Demo</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.14/jquery-ui.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="scripts/jquery.blockUI.js"></script>
    <style type="text/css">
        .error { color:Red; }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //event handler for validation button click
            $('#validate-button').click(function (e) {
                //Collect form data into vars
                var firstName = $('#first-name').val();
                var lastName = $('#last-name').val();
                var email = $('#email').val();
                var phone = $('#phone').val();
                //Use jQuery ajax to call web method to validate data and return error messages if any field fail to be validated
                $.blockUI({ message: '<h4> Validating form data ... </h4>' });
                $.ajax({
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    url: 'WebService.asmx/SaveDataFromWebForm',
                    data: JSON.stringify({ FirstName: firstName, LastName: lastName, Email: email, PhoneNumber: phone }),
                    dataType: 'json'
                })
                .success(function (data) {
                    $.unblockUI();

                    //Give message that validation succeeded
                    $('#confirmation-msg').text('Your submitted form data validated successfully! Yaaaaaaay!');
                    //reset error message from any previously failed submits
                    $('error-msg').html('');

                })
                .error(function (xhr) {
                    var r = JSON.parse(xhr.responseText);
                    //reset any confirmation message from previous submits
                    $('#confirmation-msg').text('');
                    $('#error-container').html('Please fix the following errors before saving again:<ol id="error-msg"></ol>');
                    $('#error-msg').html(r.Message);
                    $.unblockUI();
                });
            });


        });
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div id="error-container">
        <!--errors will be rendered here as a HTML ordered list from jquery event handler -->
        <span id="error-msg"></span>
    </div>
    <div id="demo-form">
        First Name: 
        <input id="first-name" type="text" />
        <br />
        Last Name: 
        <input id="last-name" type="text" />
        <br />
        E-mail: 
        <input id="email" type="text" />
        <br />
        Phone Number: 
        <input id="phone" type="text" />
        <div id="button-area">
            <input id="validate-button" type="button" value="Validate" />
            <span id="confirmation-msg"></span>
        </div>
    </div>
    </form>
</body>
</html>
