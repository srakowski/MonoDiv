﻿// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;

namespace MonoDiv
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DivTemplateAttribute : Attribute
    {
        public DivTemplateAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
