using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharedKernel
{
    public enum Errors
    {
        [Description("Item not found")]
        ItemNotFound = 0,
        [Description("Operation not allowed")]
        OperationNotAllowed = 1,
        [Description("Name field already exists")]
        NameFieldAlreadyExist = 2,
        [Description("Code field already exists")]
        CodeFieldAlreadyExist = 3,
        [Description("Operations not saved")]
        OperationsNotSaved = 4,
        [Description("Title already exist")]
        TitleAlreadyExist = 5,
        [Description("Key already exist")]
        KeyAlreadyExist = 6,
        [Description("Number already exist")]
        NumberAlreadyExist = 7,
        [Description("Old Password Incorrect")]
        OldPasswordIncorrect = 8,
        [Description("Password is required field")]
        PasswordIsRequiredField = 9,
        [Description("Username exists")]
        UsernameExists = 10,
        [Description("Email exists")]
        EmailExists = 11,
        [Description("Mobile exists")]
        MobileExists = 12,
        [Description("User is not authorized")]
        UserNotAuthorized = 16
    }

    public enum RoleEnum
    {
        Admin = 1,
        Management = 2,
        Auditor = 3
    }

    public static class Enums
    {
        public static string GetEnumDescription(System.Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
        public static string GetEnumDescription<T>(T value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            if (fi != null)
            {
                DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    return attributes[0].Description;
                }
                else
                {
                    return value.ToString();
                }
            }
            else
            {
                return value.ToString();
            }

        }
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }
}