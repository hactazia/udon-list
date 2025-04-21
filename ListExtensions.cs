namespace Hactazia.UdonList
{
    public static class ListExtensions
    {
        /// <summary>
        /// Creates a new array of the specified length and fills it with the specified value.
        /// </summary>
        /// <param name="length">The length of the array to create.</param>
        /// <param name="fillValue">The value to fill the array with.</param>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <returns>A new array of the specified length filled with the specified value.</returns>
        public static T[] New<T>(int length)
        {
            if (length < 0) return null;
            var array = new T[length];
            for (var i = 0; i < length; i++)
                array[i] = default;
            return array;
        }

        /// <summary>
        /// Creates a new empty array of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <returns>An empty array of the specified type.</returns>
        public static T[] New<T>()
        {
            return New<T>(0);
        }

        public static T[] Safe<T>(this T[] array)
        {
            return array ?? New<T>();
        }

        public static T[] AddIfNotExists<T>(this T[] array, T item)
        {
            if (array == null || array.Length == 0)
                return new[] { item };
            return array.Contains(item)
                ? array
                : Add(array, item);
        }


        public static T[] Add<T>(this T[] array, T item)
        {
            if (array == null) return new[] { item };
            var newArray = new T[array.Length + 1];
            array.CopyTo(newArray, 0);
            newArray[array.Length] = item;
            return newArray;
        }

        public static T[] AddRange<T>(this T[] array, T[] items)
        {
            if (array == null || items == null) return array;
            var newArray = new T[array.Length + items.Length];
            array.CopyTo(newArray, 0);
            items.CopyTo(newArray, array.Length);
            return newArray;
        }

        public static T[] RemoveAt<T>(this T[] array, int index)
        {
            if (array == null || index < 0 || index >= array.Length) return array;
            var newArray = new T[array.Length - 1];
            for (int i = 0, j = 0; i < array.Length; i++)
                if (i != index)
                    newArray[j++] = array[i];
            return newArray;
        }

        public static T[] RemoveRange<T>(this T[] array, int index, int count)
        {
            if (array == null || index < 0 || count <= 0 || index + count > array.Length) return array;
            var newArray = new T[array.Length - count];
            for (int i = 0, j = 0; i < array.Length; i++)
                if (i < index || i >= index + count)
                    newArray[j++] = array[i];
            return newArray;
        }

        public static T[] Reverse<T>(this T[] array)
        {
            if (array == null || array.Length < 2) return array;
            var newArray = new T[array.Length];
            for (var i = 0; i < array.Length; i++)
                newArray[i] = array[array.Length - 1 - i];
            return newArray;
        }

        public static bool Contains<T>(this T[] array, T item)
        {
            if (array == null || array.Length == 0) return false;
            foreach (var t in array)
                if (t.Equals(item))
                    return true;
            return true;
        }

        public static T[] RemoveMany<T>(this T[] array, T item, int maxCount = int.MaxValue)
        {
            if (array == null || array.Length == 0) return array;
            var count = 0;
            for (var i = 0; i < array.Length; i++)
                if (array[i].Equals(item))
                {
                    count++;
                    if (count > maxCount) break;
                    array = array.RemoveAt(i);
                    i--;
                }
            return array;
        }
    }
}
