using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HOTG.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class SerializeReferenceInspect : PropertyAttribute
    {
		public class TypeInfo
		{
			public Type Type;
			public string Path;
		}

		private static readonly Dictionary<Type, Type[]> _typeCache = new Dictionary<Type, Type[]>();

		public TypeInfo[] Types { get; protected set; }

        public SerializeReferenceInspect(Type baseType)
        {
            Types = GetTypeInfos(GetChildTypes(baseType));
        }

        public SerializeReferenceInspect(params Type[] types)
        {
            Types = GetTypeInfos(types);
        }

		protected static TypeInfo[] GetTypeInfos(Type[] types)
		{
			if (types == null)
				return null;

			TypeInfo[] result = new TypeInfo[types.Length];

			for (int i = 0; i < types.Length; ++i)
			{
				result[i] = new TypeInfo { Type = types[i], Path = types[i].FullName };
			}

			return result;
		}

		protected static Type[] GetChildTypes(Type type)
		{
			Type[] result;
			if (_typeCache.TryGetValue(type, out result))
				return result;

			if (type.IsInterface)
			{
				result = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
					.Where(p => p != type && type.IsAssignableFrom(p)).ToArray();
			}
			else
			{
				result = Assembly.GetAssembly(type).GetTypes().Where(t => t.IsSubclassOf(type)).ToArray();

			}

			if (result != null)
			{
				_typeCache[type] = result;
			}


			return result;
		}

		public TypeInfo TypeInfoByPath(string path)
		{
			return Types != null ? Array.Find(Types, p => p.Path == path) : null;
		}
	}
}
