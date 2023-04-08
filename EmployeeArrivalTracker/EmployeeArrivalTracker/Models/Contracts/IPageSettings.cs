namespace EmployeeArrivalTracker.Models.Contracts
{
    public interface IPageSettings<TSorting, TCollection>
    {
        string SearchTerm { get; set; }
        int CurrentPage { get; set; }
        int TotalElements { get; set; }
        TSorting Sorting { get; init; }
        IEnumerable<TCollection> ModelCollection { get; set; }
    }
}
