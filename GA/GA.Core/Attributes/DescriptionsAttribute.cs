using System;

namespace GA.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DescriptionsAttribute : Attribute
    {
        public DescriptionsAttribute(params string[] descriptions)
        {
            Descriptions = descriptions;

        }
        public string[] Descriptions { get; }
    }
}
