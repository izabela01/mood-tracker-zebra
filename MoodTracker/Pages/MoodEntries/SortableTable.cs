using System.Collections.Generic;
using System.Collections.Specialized;
using Microsoft.Data.SqlClient;
using MoodTracker.Models;

namespace MoodTracker.Pages.MoodEntries
{
    public class SortableTable<T>
    {
        public PaginatedList<T> PaginatedList { get; set; }
        private string CurrentSortedColumn { get; }
        public List<Column> Columns { get; }

        public SortableTable(List<Column> columnNameEditable, string currentSortedColumn, SortOrder currentSortOrder)
        {
            Columns = columnNameEditable;
            CurrentSortedColumn = currentSortedColumn;

            var currentColumn = GetCurrentlySortedColumn();
            currentColumn.CurrentSortOrder = currentSortOrder;
            currentColumn.Active = true;
        }

        public Column GetCurrentlySortedColumn()
        {
            return GetColumn(CurrentSortedColumn);
        }
        
        public Column GetColumn(string columnName)
        {
            return Columns.Find(column => column.ColumnName == columnName);
        }
    }
}