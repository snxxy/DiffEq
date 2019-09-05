using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Text;

namespace Generator
{
    public class PyScriptsPool
    {
        private ConcurrentBag<PyScriptsLoader> objects;
        private readonly int maxCount = 2;

        private static readonly object singleLock = new object();

        private static PyScriptsPool instance = null;
        public static PyScriptsPool Instance
        {
            get
            {
                lock (singleLock)
                {
                    if (instance == null)
                    {
                        instance = new PyScriptsPool();
                    }
                    return instance;
                }
            }
        }

        public PyScriptsPool()
        {
            objects = new ConcurrentBag<PyScriptsLoader>();
        }

        public PyScriptsLoader GetObject()
        {
            PyScriptsLoader item;
            if (objects.TryTake(out item))
            {
                return item;
            }
            if (objects.Count < maxCount)
            {
                return new PyScriptsLoader();
            }
            throw new InvalidOperationException("Max item count reached");
        }
        
        public void PutObject(PyScriptsLoader item)
        {
            objects.Add(item);
        }
    }
}
