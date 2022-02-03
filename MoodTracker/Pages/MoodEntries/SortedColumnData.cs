using Microsoft.Data.SqlClient;

namespace MoodTracker.Pages.MoodEntries
{
    public class SortedColumnData
    {
        public SortedColumnData(string columnName)
        {
            ColumnName = columnName;
            CurrentSortOrder = SortOrder.Descending;
            Active = false;
        }
        
        public string ColumnName { get; }
        public SortOrder CurrentSortOrder { get; set; }
        public bool Active { get; set; }

        public string GetFormattedTitle() 
        {
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
            return CurrentSortOrder == SortOrder.Ascending ? SortOrder.Descending : SortOrder.Ascending;
        }
    }
}