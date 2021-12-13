using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleAppStore
{
    class Expressions
    {
        //Online reference
        public bool IsNameValid(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        //Online reference
        public bool IsPasswordValid(string pwd)
        {
            return Regex.IsMatch(pwd, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$");

        }

        //Online reference
        public bool IsEmailValid(string email)
        {
            return Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        }

        public bool IsAddressValid(string address)
        {
            return Regex.IsMatch(address, @"^\d+(\s?)+(\w?)+(\s?)+(\w?)+$");
        }

        public bool IsStateValid(string state)
        {
            return Regex.IsMatch(state, @"[A-z]{1}?$");
        }

        public bool IsZipValid(string zip)
        {
            return Regex.IsMatch(zip, @"^\d{5}(?:[-\s]\d{4})?$");
        }

        public bool IsPhoneValid(string phone)
        {
            return Regex.IsMatch(phone, @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]\d{3}[\s.-]\d{4}$");
        }

        public bool IsCategoryValid(string cat)
        {
            return Regex.IsMatch(cat, @"(\blaptop\b|\bdesktop\b|\btablet\b|\bmobile\b)");
        }
    }


}
