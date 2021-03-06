﻿using System;

namespace InscribedCircles.Abstraction.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ViewModelAttribute : Attribute
    {
        public ViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }

        public Type ViewModelType { get; private set; }
    }
}
