using UnityEngine;

namespace HOTG.CustomAttributes
{
    public class CustomAttributeAttribute : PropertyAttribute
    {
        public readonly string Name;

        public CustomAttributeAttribute(string name)
        {
            Name = name;
        }
    }
}