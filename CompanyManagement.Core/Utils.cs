using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement.Core
{
    public static class Utils
    {

        /// <summary>
        /// Gest custom attributes of an Enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttributeOfType<T>(Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfos = type.GetMember(value.ToString());

            if (memberInfos.Length == 0)
                return null;

            var attributes = memberInfos[0].GetCustomAttributes(typeof(T), false);

            return attributes.Length > 0 ? (T)attributes[0] : null;

        }
    }
}
