namespace InfluencerWannaBe.Models
{
    using System.Collections.Generic;
    using EmployeeArrivalTracker.Models.Contracts;

    public abstract class PageSettingsAbstract<TSorting, TCollection> : IPageSettings<TSorting, TCollection>
    {
        public string SearchTerm { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalElements { get; set; }
        public TSorting Sorting { get; init; }
        public IEnumerable<TCollection> ModelCollection { get; set; }
    }
}
