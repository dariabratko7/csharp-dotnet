using System;
using System.Text.RegularExpressions;

namespace DataValidationLib
{
    public static class Validator
    { 
        public static bool ValidateFullName(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) return false;
            return Regex.IsMatch(fullName, @"^[a-zA-Zа-яА-ЯіІїЇєЄґҐ'\s-]+$");
        }
         
        public static bool ValidateAge(string age)
        {
            if (string.IsNullOrWhiteSpace(age)) return false;
            return Regex.IsMatch(age, @"^\d+$");
        }
         
        public static bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone)) return false; 
            return Regex.IsMatch(phone, @"^\+?\d{11,12}$");
        }
         
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false; 
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}