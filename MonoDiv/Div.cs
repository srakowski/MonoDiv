// MIT License - Copyright (C) Shawn Rakowski
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MonoDiv
{
    [DivTemplate("<slot></slot>")]
    public class Div
    {
        const string DEFAULT_SLOT_NAME = "default";

        private readonly Dictionary<string, Slot> _slots = new Dictionary<string, Slot>();

        private readonly List<Div> _children = new List<Div>();

        public Div Parent { get; private set; }

        public IEnumerable<Div> Children => _children;

        private void AppendChild(Div child)
        {
            _children.Add(child);
            child.Parent = this;
        }

        public Div Scope { get; private set; }

        internal static Func<Div, Div> Compile(
            Type divType,
            Dictionary<string, Type> divTypeRegistry,
            Dictionary<string, Func<Div, Div>> divActivatorRegistry)
        {
            var divTemplates = divType
                .GetCustomAttributes(typeof(DivTemplateAttribute), inherit: true)
                .OfType<DivTemplateAttribute>()
                .ToArray();

            if (divTemplates.Length != 1)
            {
                throw new Exception($"custom divs must have exactly one template, {divType.Name} does not");
            }

            var template = divTemplates.Single().Value;

            var el = XElement.Parse(template);

            var name = el.Name.LocalName.ToLower();
            var nodes = el.Nodes();

            if (name == "slot" && divType.FullName != typeof(Div).FullName)
            {
                throw new Exception($"template root disallows <slot> elements");
            }

            ValidateElement(name, divTypeRegistry);
            ValidateChildren(nodes, divTypeRegistry);

            var result = new Func<Div, Div>((parent) =>
            {
                var instance = Activator.CreateInstance(divType) as Div;
                parent.AppendChild(instance);

                if (divType.FullName == typeof(Div).FullName)
                {
                    instance._slots[DEFAULT_SLOT_NAME] = new Slot();
                    return instance;
                }

                var root = divActivatorRegistry[name](instance);
                root.Scope = instance;

                if (!root._slots.ContainsKey(DEFAULT_SLOT_NAME))
                {
                    return instance;
                }

                ProcessChildNodes(divActivatorRegistry, nodes, instance, root._slots[DEFAULT_SLOT_NAME]);

                return instance;
            });

            return result;
        }

        private static void ProcessChildNodes(Dictionary<string, Func<Div, Div>> divActivatorRegistry, IEnumerable<XNode> nodes, Div instance, Slot slot)
        {
            foreach (var childNode in nodes)
            {
                switch (childNode)
                {
                    case XElement cel:
                        var childName = cel.Name.LocalName.ToLower();
                        if (childName == "slot")
                        {
                            var childSlot = new Slot();
                            childSlot.Scope = instance;
                            instance._slots[DEFAULT_SLOT_NAME] = childSlot;
                            slot.AppendChild(childSlot);
                            return;
                        }

                        var childDiv = divActivatorRegistry[childName](slot);
                        childDiv.Scope = instance;

                        if (!childDiv._slots.ContainsKey(DEFAULT_SLOT_NAME))
                        {
                            return;
                        }

                        ProcessChildNodes(divActivatorRegistry, cel.Nodes(), instance, childDiv._slots[DEFAULT_SLOT_NAME]);
                        break;

                    case XText xtxt:
                        var childText = new Text(xtxt.Value);
                        childText.Scope = instance;
                        slot.AppendChild(childText);
                        break;

                    default:
                        throw new Exception($"unrecognized XNode type: {childNode.GetType().Name}");
                }
            }
        }

        private static void ValidateElement(string name, Dictionary<string, Type> divTypeRegistry)
        {
            if (name != "div" && name != "slot" && !divTypeRegistry.ContainsKey(name))
            {
                throw new Exception($"'{name}' is not a recognized div, did you forget to register it?");
            }
        }

        private static void ValidateChildren(IEnumerable<XNode> nodes, Dictionary<string, Type> divTypeRegistry)
        {
            foreach (var el in nodes.OfType<XElement>())
            {
                var name = el.Name.LocalName.ToLower();
                ValidateElement(name, divTypeRegistry);
                ValidateChildren(el.Nodes(), divTypeRegistry);
            }
        }
    }
}
