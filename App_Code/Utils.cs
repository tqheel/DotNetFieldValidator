using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Configuration;

/// <summary>
/// Collection of public utility methods for various common functions
/// </summary>
public class Utils
{
    

    public static bool IsValidEmail(string strIn)
    {
        // Return true if strIn is in valid e-mail format.
        return Regex.IsMatch(strIn,
               @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" +
               @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$");
    }

    public static bool IsFieldPopulated(string fieldValue)
    {
        int charCount = fieldValue.Length;
        if (charCount < 1) return false;
        else return true;
    }

    public static string FormatDecimalToFixed2(decimal? valueToFormat)
    {
        string specifier = "{0:0.00}";
        if (valueToFormat == null) return "0.00";
        else return string.Format(specifier,valueToFormat);
    }

    public static string FormatNumberToCommaNoDecimals(int? valueToFormat)
    {
        string specifier = "{0:0,000}";
        if (valueToFormat == null) return "0";
        else return string.Format(specifier, valueToFormat);
    }

    public static string FormatDecimalToComma2Decimals(decimal? valueToFormat)
    {
        string specifier = "{0:0,000.00}";
        if (valueToFormat == null) return "0";
        else return string.Format(specifier, valueToFormat);
    }

    public static DateTime ConvertTimeToETZ(DateTime timeToConvert)
    {
        const string etz = "Eastern Standard Time";
        TimeZoneInfo tzTo = TimeZoneInfo.FindSystemTimeZoneById(etz);
        DateTime convertedTime = TimeZoneInfo.ConvertTimeFromUtc(timeToConvert.ToUniversalTime(), tzTo);
        return convertedTime;
    }

    public static string ComposeErrMsg(string input)
    {
        string output = string.Empty;
        string prefix = "<li>";
        string suffix = "</li>";
        return output = prefix + input + suffix;
    }

    

    public static bool IsStringShortEnough(string input, int maxLength)
    {
        if (input == null) return true;
        if ( input.Length <= maxLength) return true;
        else return false;
    }
    
    public static bool IsValidUSZip(string num)
    {
        // Allows 5 digit, 5+4 digit and 9 digit zip codes
        string pattern = @"^(\d{5}-\d{4}|\d{5}|\d{9})$";
        Regex match = new Regex(pattern);
        return match.IsMatch(num);
    }


    public static DateTime GetLocalBapTimeStamp()
    {
        return ConvertTimeToETZ(DateTime.Now);
    }


    public static bool IsValidUSOrCanadaPhone(string phone)
    {
        // Match a US or Canadian phone number with an optional 0 or 1, optional separator of  -, . or space, optional parentheses
        string pattern = @"^[01]?[- .]?\(?[2-9]\d{2}\)?[- .]?\d{3}[- .]?\d{4}$";
        Regex match = new Regex(pattern);
        return match.IsMatch(phone);
    }

    public static bool IsValidIntlPhone (string phone)
    {
        string pattern = @"^(\+[1-9][0-9]*(\([0-9]*\)|-[0-9]*-))?[0]?[1-9][0-9\- ]*$";
        Regex match = new Regex(pattern);
        return match.IsMatch(phone);
    }

    
}



