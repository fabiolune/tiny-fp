﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace TinyFp.Extensions
{
    public static partial class FunctionalExtension
    {
        [Pure]
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> @this, Func<T, bool> predicate) 
            => @this.Where(predicate);

        [Pure]
        public static S Fold<S, T>(this IEnumerable<T> @this, S state, Func<S, T, S> folder)
        {
            foreach (var item in @this)
            {
                state = folder(state, item);
            }
            return state;
        }

        [Pure]
        public static T Reduce<T>(this IEnumerable<T> @this, Func<T, T, T> reducer) 
            => @this.Fold(default, reducer);

        [Pure]
        public static IEnumerable<R> Map<T, R>(this IEnumerable<T> @this, Func<T, R> map)
            => @this.Select(map);
    }
}
