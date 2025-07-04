using System;

namespace SharedKernel
{
    public class Hook<T>
    {
        public Object Id { get; set; }
        public string Text
        {
            get
            {
                T enumVal = (T)Enum.Parse(typeof(T), Id.ToString());

                return Enums.GetEnumDescription(enumVal);
            }
        }
    }
    public class Hook
    {
        public Guid? Id { get; set; }
        public string Text { get; set; }
    }

    public class StatusColorHook<T1,T2,T3>
    {
        public T1 priority { get; set; }
        public T2 color { get; set; }
        public T3 time { get; set; }
    }

    public class LocationHook
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Text { get; set; }
    }
    public class Hook<T1, T2>
    {
        public T1 Id { get; set; }
        public T2 Text { get; set; }
    }
    public class GroupHook : Hook
    {
        public string Group { get; set; }
    }
    public class GroupHook<T1, T2, T3> : Hook<T1, T2>
    {
        public T3 Group { get; set; }
    }
    public class ResourcesHook<T1, T2, T3>
    {
        public T1 Id { get; set; }
        public T2 LanguageId { get; set; }
        public T3 Value { get; set; }
    }
}