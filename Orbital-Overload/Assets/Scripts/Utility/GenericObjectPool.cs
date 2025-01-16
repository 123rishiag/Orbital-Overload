using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServiceLocator.Utility
{
    /// <summary>
    /// This is a Generic Object Pool Class with basic functionality, which can be inherited to implement object pools for any type of objects.
    /// </summary>
    /// <typeparam object Type to be pooled = "T"></typeparam>
    public class GenericObjectPool<T> where T : class
    {
        public List<PooledItem<T>> pooledItems = new List<PooledItem<T>>();

        public virtual T GetItem(Vector2 _spawnPosition)
        {
            if (pooledItems.Count > 0)
            {
                PooledItem<T> item = pooledItems.Find(item => !item.isUsed);
                if (item != null)
                {
                    item.isUsed = true;
                    return item.Item;
                }
            }
            return CreateNewPooledItem(_spawnPosition);
        }

        private T CreateNewPooledItem(Vector2 _spawnPosition)
        {
            PooledItem<T> newItem = new PooledItem<T>();
            newItem.Item = CreateItem(_spawnPosition);
            newItem.isUsed = true;
            pooledItems.Add(newItem);
            return newItem.Item;
        }

        protected virtual T CreateItem(Vector2 _spawnPosition)
        {
            throw new NotImplementedException("CreateItem() method not implemented in derived class");
        }

        public virtual void ReturnItem(T _item)
        {
            PooledItem<T> pooledItem = pooledItems.Find(i => i.Item.Equals(_item));
            pooledItem.isUsed = false;
        }

        public class PooledItem<T>
        {
            public T Item;
            public bool isUsed;
        }
    }
}