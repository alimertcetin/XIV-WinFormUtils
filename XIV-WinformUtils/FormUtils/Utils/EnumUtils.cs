using System;

namespace XIV.Utils
{
    /// <summary>
    /// Utilities for <see cref="Enum"/> class
    /// </summary>
    public static class EnumUtils
    {
        public static string GetTypeString<T>(object value) where T : Enum
        {
            return Enum.GetName(typeof(T), value);
        }

        public static T GetType<T>(string value) where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            foreach (var item in values)
            {
                if (item.ToString() == value)
                {
                    return (T)item;
                }
            }
            return default(T);
        }

        public static Array GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T));
        }
    }
}