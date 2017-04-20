﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment1.Models
{
    public class ModelCollection
    {
        private Dictionary<Type, object> models = new Dictionary<Type, object>();

        public void AddModel<T>(T t)
        {
            models.Add(t.GetType(), t);
        }

        public T GetModel<T>()
        {
            return (T)models[typeof(T)];
        }
    }
}
