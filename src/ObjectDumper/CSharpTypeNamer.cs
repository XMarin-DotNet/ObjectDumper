﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JetBrains.Annotations;

namespace ObjectDumper
{
    internal class CSharpTypeNamer : ITypeNamer
    {
        [NotNull]
        private static readonly Dictionary<Type, string> _KnownTypes = new Dictionary<Type, string>
                                                                       {
                                                                           [typeof(short)] = "short",
                                                                           [typeof(ushort)] = "ushort",
                                                                           [typeof(sbyte)] = "sbyte",
                                                                           [typeof(byte)] = "byte",
                                                                           [typeof(int)] = "int",
                                                                           [typeof(uint)] = "uint",
                                                                           [typeof(long)] = "long",
                                                                           [typeof(ulong)] = "ulong",
                                                                           [typeof(double)] = "double",
                                                                           [typeof(decimal)] = "decimal",
                                                                           [typeof(float)] = "float",
                                                                           [typeof(bool)] = "bool",
                                                                           [typeof(string)] = "string",
                                                                       };

        public string GetNameOfType(Type type)
        {
            if (_KnownTypes.TryGetValue(type, out string knownName))
                return knownName;

            if (type.IsGenericType)
            {
                string name = type.FullName;
                name = name.Substring(0, name.IndexOf("`"));

                name = name + "<" + string.Join(",", type.GetGenericArguments().Select(t => GetNameOfType(t))) + ">";
                return name;
            }

            return type.FullName;
        }
    }
}