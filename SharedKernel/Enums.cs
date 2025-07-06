using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharedKernel
{
    public static class SystemExtendedMethods
    {
        public static IEnumerable<TSource> DistinctBy<TSource>(this IEnumerable<TSource> source, params Func<TSource, object>[] keySelectors)
        {
            var keys = keySelectors.ToDictionary(x => x, x => new HashSet<object>());

            foreach (var element in source)
            {
                var flag = true;

                foreach (var (keySelector, hashSet) in keys)
                {
                    flag = flag && hashSet.Add(keySelector(element));
                }

                if (flag)
                {
                    yield return element;
                }
            }
        }
    }


    // don't change the values of this enum , (Adnan sukkariyeh)
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
    public enum LanguageTypes
    {
        English = 1,
        Arabic = 2
    }

    public enum FolderType
    {
        Receiving = 1
    }
    public enum ClassesTypes
    {
        [Description("Entity.Entity.Imports.Items.ItemCSV, Entity")]
        Items = 1,
        [Description("Entity.Entity.Imports.warehouses.WarehouseCSV, Entity")]
        Warehouses = 2,
        [Description("Entity.Entity.Imports.Suppliers.SupplierCSV, Entity")]
        Suppliers = 3,
    }
    public enum CustomeAttributeTypes
    {
        [Description("SharedKernel.DynamicMapping.KeyAttribute")]
        Key = 0,
        [Description("SharedKernel.DynamicMapping.UniqueAttribute")]
        Unique = 1,
        [Description("SharedKernel.DynamicMapping.RequiredAttribute")]
        Required = 2,
        [Description("SharedKernel.DynamicMapping.DisplayAttribute")]
        Display = 3
    }
    public enum DynamicMappingConfigurationsRepeatBehaviorTypes
    {
        AddNew = 1,
        AddNewAndUpdate = 2,
        Update = 3
    }
    public enum ImportDataStatuses
    {
        [Description("New")]
        New = 1,
        [Description("Inserted")]
        Inserted = 2,
        [Description("Updated")]
        Updated = 3,
        [Description("Ignored")]
        Ignored = 4,
        [Description("Having error")]
        HavingError = 5
    }

    public enum ReportAndDashboardTarget
    {
        [Description("Back Office")]
        BackOffice = 1,
        [Description("Front Office")]
        FrontOffice = 2,
        [Description("Both")]
        Both = 3
    }

    public enum ReportAndDashboardType
    {
        [Description("Report")]
        Report = 1,
        [Description("Dashboard")]
        Dashboard = 2
    }
    public enum LabelEnum
    {
        [Description("Receiving")]
        Receiving = 1,
        [Description("Sorting")]
        Sorting = 2
    }

    public enum AttachmentReferenceTypes
    {
    }
    public enum EmailKeyType : int
    {
    }
    public enum EmailKey
    {
    }

    public enum SyncStatus
    {
        [Description("Inserted")]
        Inserted = 1,
        [Description("Updated")]
        Updated = 2,
        [Description("Deleted")]
        Deleted = 3,
        [Description("Synced")]
        Synced = 4,
        [Description("UnSynced")]
        UnSynced = 5,
    }
    public enum ConfigurationType
    {
        [Description("Status")]
        Status = 1,
        [Description("Email")]
        Email = 2
    }
    public enum DashboardType
    {
        [Description("Nurse Hub")]
        NurseHub = 1,
        [Description("Pharmacy Hub")]
        PharmacyHub = 2
    }
    public enum StakeholderType
    {
        [Description("Supplier")]
        Supplier = 1,
        [Description("Customer")]
        Customer = 2,
        [Description("Third party")]
        ThirdParty = 3
    }
    public enum TransactionStatusEnum
    {
        [Description("New")]
        New = 1,
        [Description("In progress")]
        Inprogress = 2,
        [Description("Completed")]
        Completed = 3,
        [Description("Canceled")]
        Canceled = 4,
        [Description("Weighing")]
        Weighing = 5,
        [Description("Rejected")]
        Rejected = 6,
    }
    public enum SequenceType
    {
        [Description("Receiving")]
        Receiving = 1,
        [Description("Order number")]
        OrderNumber = 2,
        [Description("Batch No")]
        BatchNo = 3,
        [Description("Returns")]
        Returns = 4,
        [Description("Aggregations")]
        Aggregations = 5,
        [Description("Wastes")]
        Wastes = 6,
        [Description("Disaggregations")]
        Disaggregations = 7
    }
    public enum TransactionType
    {
        [Description("Receiving")]
        Receiving = 1,
        [Description("Allocation")]
        Allocation = 2,
        [Description("Transformations")]
        Transformations = 3,
        [Description("Returns")]
        Returns = 4,
        [Description("Aggregations")]
        Aggregations = 5,
        [Description("Wastes")]
        Wastes = 6,
        [Description("Disaggregations")]
        Disaggregations = 7,
        [Description("Order")]
        Order = 8
    }
    public enum TransactionDetailsType
    {
        [Description("None")]
        None = 1,
        [Description("Input")]
        Input = 2,
        [Description("Output")]
        Output = 3,
        [Description("Pick")]
        Pick = 4,
        [Description("Put")]
        Put = 5
    }
    public enum AggregationStatus
    {
    }
    public enum SerialTemplateType
    {
        [Description("Hex")]
        Hex = 1,
        [Description("Number")]
        Number = 2
    }
    public enum OrderStatus
    {
        [Description("New")]
        New = 1,
        [Description("Partially prepared")]
        partiallyPrepared = 2,
        [Description("Prepared")]
        Prepared = 3,
        [Description("Delivered")]
        Delivered = 4,
        [Description("Canceled")]
        Canceled = 5
    }
    public enum OwnedBySupplierOptions
    {
        [Description("Yes")]
        Yes = 1,
        [Description("No")]
        No = 2,
        [Description("None")]
        None = 2,
    }

    public enum Types
    {
        [Description("damage")]
        Damage = 1,
        [Description("Return")]
        Return = 2,
        [Description("warehouse")]
        Warehouse = 3,
        [Description("Receiving area")]
        ReceivingArea = 4,
        [Description("Production area")]
        SortingZone = 5
    }

    public enum TokenType
    {
        [Description("ForgetPassword")]
        ForgetPassword = 1
    }

    public enum SerialStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Unreachable")]
        Unreachable = 2,
        [Description("Returned")]
        Returned = 3,
        [Description("Sold")]
        Sold = 4,
        [Description("Damaged")]
        Damaged = 5,
        [Description("Deactive")]
        Deactive = 6,
        [Description("Transformation")]
        Transformation = 7,
    }

    public enum SSCCStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("Deactive")]
        Deactive = 2,
        [Description("Disaggregated")]
        Disaggregated = 3,
        [Description("PartiallyDisaggregated")]
        PartiallyDisaggregated = 4,
        [Description("Aggregated")]
        Aggregated = 5,
        [Description("Returned")]
        Returned = 6,
        [Description("Sold")]
        Sold = 7,
        [Description("Damaged")]
        Damaged = 8,
        [Description("PartiallyActive")]
        PartiallyActive = 9,
    }

    public enum ActionType
    {
        Order = 1,
    }

    public enum SerialErrorHandler
    {
        [Description("Serial does not exsist")]
        SerialDoesNotExsist = 1,
        [Description("Serial already Sold")]
        SerialAlreadySold = 2
    }

    public enum NodeType
    {
        [Description("Input")]
        Input = 1,
        [Description("Output")]
        Output = 2,
        [Description("Receiving")]
        Receiving = 3,
        [Description("Allocation")]
        Allocation = 4,
        [Description("Returns")]
        Returns = 5,
        [Description("Aggregations")]
        Aggregations = 6,
        [Description("Wastes")]
        Wastes = 7,
        [Description("Disaggregations")]
        Disaggregations = 8,
        [Description("Order")]
        Order = 9,
        [Description("Main")]
        Main = 10,
    }

    public enum ConfigurationSource
    {
        Reports = 1
    }

    public enum PageSource
    {
        Receiving = 1,
        Order = 2,
        Waste = 3,
        Transformation =4,
        Return = 5,
        Allocation = 6
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