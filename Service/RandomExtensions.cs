using System;

namespace System
{
    public static class RandomExtensions
    {
        public static T NextItem2<T>(this Random rnd, params T[] items ) => items[rnd.Next(items.Length)];
    }
}
