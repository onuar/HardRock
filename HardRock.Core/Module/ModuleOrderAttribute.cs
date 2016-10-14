using System;

namespace HardRock.Core.Module
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ModuleOrderAttribute : Attribute
    {
        public ModuleOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; }
    }
}