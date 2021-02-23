using System;
using System.Collections.Generic;
using System.Text;

namespace FL.Cache.Standard.Contracts
{
    public interface ICustomMemoryCache<T>
    {
        T Get(object key);

        void Add(object key, T items);

        T GetOrCreate(object key, Func<T> createItem);
    }
}
