// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace MonoDiv
{
    internal class Text : Div
    {
        public Text(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
