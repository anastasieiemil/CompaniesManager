namespace CompanyManagement.Models.Commons.Search
{
    /// <summary>
    /// Class used for describing a model used by dataTable for rendering.
    /// </summary>
    /// <typeparam name="ModelType"></typeparam>
    public class DataTableModel<ModelType>
        where ModelType : class
    {
        public int Draw { get; set; }

        public int RecordsTotal { get; set; }

        public int RecordsFiltered { get; set; }

        public List<ModelType> Data { get; set; }
    }
}
