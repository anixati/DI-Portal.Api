using System;

namespace DI.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NoPatchAttribute : Attribute
    {
    }
}