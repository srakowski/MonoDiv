// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using MonoDiv.Example.Divs;
using System;

namespace MonoDiv.Example
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            using (var game = new ExampleGame())
                game.Run();
        }
    }
}
