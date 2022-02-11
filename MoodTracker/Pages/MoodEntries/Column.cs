using Microsoft.Data.SqlClient;

namespace MoodTracker.Pages.MoodEntries
{
    public class Column
    {
        private const SortOrder DEFAULT_SORT_ORDER = SortOrder.Descending;
        public Column(string columnName, bool sortable)
        {
            ColumnName = columnName;
            Sortable = sortable;
            CurrentSortOrder = SortOrder.Unspecified;
            Active = false;
        }
        
        public string ColumnName { get; }
        public bool Sortable { get; } 
        public SortOrder CurrentSortOrder { get; set; }
        public bool Active { get; set; }

        public string GetFormattedTitle()
        {
            if (!Sortable) return ColumnName;
            
            string orderIcon = CurrentSortOrder switch
            {
                SortOrder.Ascending => "▲",
                SortOrder.Descending => "▼",
                _ => "[UNSORTED]"
            };

            string formattedTitle = ColumnName;

            if (Active) formattedTitle += " " + orderIcon;

            return formattedTitle;
        }

        public SortOrder GetNextSortOrder()
        {
            if (CurrentSortOrder == SortOrder.Unspecified) return DEFAULT_SORT_ORDER;
            return CurrentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        }
    }
}