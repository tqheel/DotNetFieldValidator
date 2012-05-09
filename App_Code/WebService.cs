using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService {

    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]
    public void SaveDataFromWebForm(string FirstName, string LastName, string Email, string PhoneNumber)
    {
        //Validate Each Parameter/Field from web form and concatenate error message for each field that fails
        string errorMsg = string.Empty;
        bool errorsCaught = false;

        //First make sure string fields are short enough to store in our database; for this example we are assuming the string fields are being stored in varchar(50) fields in the db.
        if(!Utils.IsStringShortEnough(FirstName, 50))
        {
            errorMsg+=Utils.ComposeErrMsg("First Name must be 50 characters or less.");
            errorsCaught=true;
        }
        //Make sure last name is populated, and if so check to make sure < 50 charss
        if (!Utils.IsFieldPopulated(LastName))
        {
            errorMsg += Utils.ComposeErrMsg("Last Name cannot be blank.");
            errorsCaught = true;
        }
        else if(!Utils.IsStringShortEnough(LastName, 50))
        {
            errorMsg+=Utils.ComposeErrMsg("Last Name must be 50 characters or less.");
            errorsCaught=true;
        }

        if(!Utils.IsStringShortEnough(Email, 50))
        {
            errorMsg+=Utils.ComposeErrMsg("E-mail must be 50 characters or less.");
            errorsCaught=true;
        }

        //Now validate that the actual email address is a valid address
        if (!Utils.IsValidEmail(Email))
        {
            errorMsg += Utils.ComposeErrMsg("The e-mail address provided is not valid.");
            errorsCaught = true;
        }

        if (!Utils.IsStringShortEnough(PhoneNumber, 50))
        {
            errorMsg += Utils.ComposeErrMsg("Phone number must be 50 characters or less.");
            errorsCaught = true;
        }

        //Now check the phone number as valid US number
        if (!Utils.IsValidUSOrCanadaPhone(PhoneNumber))
        {
            errorMsg += Utils.ComposeErrMsg("The phone number provided is not valid.");
            errorsCaught = true;
        }

        //If any validation failed, throw exception, with concatenated error message, which is formatted as a set of HTML <li> elements
        if (errorsCaught) throw new Exception(errorMsg);

        //Note: there are a few other validation methods in the Utils class not used in above code. Check 'em out...
            //For example, there is code for ensuring a server datestamp gets converted to Eastern time zone

        //If we get to this point in the method without an exception being raised, then validation succeeded and we persist form data to DB
        {
            //Do save to db stuff here...
        }
    }
    
}
