// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MonoDiv.Example.Divs;

namespace MonoDiv.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            var view = new View<App>();
            view.RegisterDiv<HelloWorld>();
            view.Initialize();
        }
    }
}
