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
        [Description("Code field doesn’t exists")]
        CodeNotExists = 13,
        [Description("Dispense doesn’t exist")]
        DispenseNotExist = 14,
        [Description("Archive dispense doesn’t exist")]
        ArchiveDispenseNotExist = 15,
        [Description("User is not authorized")]
        UserNotAuthorized = 16,
        [Description("Warning")]
        Warning = 17,
        [Description("{0}")]
        InternalServreError = 500,
        [Description("Incorrect source warehouse")]
        IncorrectSourceWarehouse = 18,
        [Description("Serial does not exsist")]
        SerialDoesNotExsist = 19,
        [Description("Serial already Sold")]
        SerialAlreadySold = 20,
        [Description("Return already completed")]
        ReturnalreadyCompleted = 21,
        [Description("Serial Already in use")]
        SerialAlreadyInUse = 22,
        [Description("Net weight not valid")]
        NetWeightNotValid = 23,
        [Description("SSCC serial already exsist")]
        SSCCSerialAlreadyExsist = 24
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