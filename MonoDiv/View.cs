// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;

namespace MonoDiv
{
    public class View<TRoot> where TRoot : Div, new()
    {
        private readonly Dictionary<string, Type> _divTypeRegistry = new Dictionary<string, Type>();
        private readonly Dictionary<string, Func<Div, Div>> _divActivatorRegistry = new Dictionary<string, Func<Div, Div>>();
        private readonly string _rootName;

        public View(string rootName = null)
        {
            _divTypeRegistry["div"] = typeof(Div);
            this._rootName = (rootName ?? typeof(TRoot).Name).ToKebabCase();
            RegisterDiv<TRoot>(_rootName);
        }

        public View<TRoot> RegisterDiv<T>(string name = null) where T : Div, new()
        {
            var key = (name ?? typeof(T).Name).ToKebabCase();

            if (key == "slot")
                throw new ArgumentException($"'slot' is reserved and may not be used for custom divs");

            if (_divTypeRegistry.ContainsKey(key))
                throw new ArgumentException($"a div named '{key}' is already registered");

            _divTypeRegistry[key] = typeof(T);
            return this;
        }

        public View<TRoot> Initialize()
        {
            try
            {
                foreach (var key in _divTypeRegistry.Keys)
                {
                    var divType = _divTypeRegistry[key];
                    _divActivatorRegistry[key] = Div.Compile(divType, _divTypeRegistry, _divActivatorRegistry);
                }

                var root = _divActivatorRegistry[_rootName](null);
            }
            catch (Exception ex)
            {
                throw new Exception($"failed to compile div, reason {ex.Message}");
            }

            return this;
        }
    }
}
