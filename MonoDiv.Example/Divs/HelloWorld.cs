// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

namespace MonoDiv.Example.Divs
{
    [DivTemplate(@"
        <div>
            <div>Things:</div>
            <div>Hello World!</div>
            <slot></slot>
        </div>"
    )]
    class HelloWorld : Div
    {
    }
}
