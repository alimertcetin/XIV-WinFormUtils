using System;

namespace XIV.Utils
{
    /// <summary>
    /// Utilities for <see cref="Enum"/> class
    /// </summary>
    public static class EnumUtils
    {
        //TODO : Add summary to methods

        public static string GetName<T>(object value) where T : Enum
        {
            return Enum.GetName(typeof(T), value);
        }

        public static int GetIndex<T>(object value) where T : Enum
        {
            var type = typeof(T);
            Array values = Enum.GetValues(type);
            int counter = 0;
            foreach (var item in values)
            {
                if (item == value)
                {
                    return counter;
                }
                counter++;
            }
            return -1;
        }

        public static T GetValueByName<T>(string valueName) where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            foreach (var item in values)
            {
                if (item.ToString() == valueName)
                {
                    return (T)item;
                }
            }
            return default(T);
        }

        public static T GetValueByIndex<T>(int index) where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            int counter = 0;
            foreach (var item in values)
            {
                if (counter == index)
                {
                    return (T)item;
                }
                counter++;
            }
            return default(T);
        }

        public static Array GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T));
        }

        public static string GetNameByIndex<T>(int index) where T : Enum
        {
            var value = GetValueByIndex<T>(index);
            var name = GetName<T>(value);
            return name;
        }

        public static int GetIndexByName<T>(string valueName) where T : Enum
        {
            var val = GetValueByName<T>(valueName);
            var index = GetIndex<T>(val);
            return index;
        }
    }
}