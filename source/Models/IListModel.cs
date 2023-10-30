namespace tyf.data.service.Models
{
    public interface IListModel<T>
    {
        public IEnumerable<T> Results { get; set; }
        public int TotalResults { get; set; }
        public int Page { get; set; }
    }
}



