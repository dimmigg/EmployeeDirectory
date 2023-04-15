﻿using System;

namespace System
{
    public static class RandomExtensions
    {
        public static T NextItem(this Random rnd, params T[] items ) => items[rnd.Next(items.Length)];
    }
}
