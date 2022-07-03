
namespace CompanyManagement.Core.Attributes
{
    /// <summary>
    /// Sets the order of columns when the data is parsed.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ParserColumnOrderAttribute : Attribute
    {
        public ParserColumnOrderAttribute(int order)
        {
            Order= order;   
        }
        public int Order { get; set; }
    }
}
