using CompanyManagement.Core.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core.Parsers.Commons
{
    public class PropertyItem
    {
        private static readonly Type orderAttributeType = typeof(ParserColumnOrderAttribute);

        public PropertyItem()
        {

        }
        public PropertyItem(PropertyInfo property)
        {
            Property = property;
            var orderAttribute = property.GetCustomAttributes(orderAttributeType).FirstOrDefault();

            if (orderAttribute is ParserColumnOrderAttribute attr)
            {
                Order = attr.Order;
            }
            else
            {
                Order = int.MaxValue;
            }
        }
        public PropertyInfo Property { get; set; }
        public int Order { get; set; }
    }
}
