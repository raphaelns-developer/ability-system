using System;
using UnityEngine;

namespace HOTG.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class GenericListAttribute : SerializeReferenceInspect
    {
		public GenericListAttribute(Type baseType) : base(baseType)
		{
			//
		}

		public GenericListAttribute(params Type[] types) : base(types)
		{
			//
		}

		public void SetTypeByName(string typeName)
		{
			if(string.IsNullOrEmpty(typeName))
			{
				Debug.LogError("[SRAttribute] Incorrect type name.");
			}
			var type = GetTypeByName(typeName);
			if(type == null)
			{
				Debug.LogError("[SRAttribute] Incorrect type.");
			}

			Types = GetTypeInfos(GetChildTypes(type));
		}

		public static Type GetTypeByName(string typeName)
		{
			if(string.IsNullOrEmpty(typeName))
				return null;

			var typeSplit = typeName.Split(char.Parse(" "));
			var typeAssembly = typeSplit[0];
			var typeClass = typeSplit[1];

			return Type.GetType(typeClass + ", " + typeAssembly);
		}
    }
}