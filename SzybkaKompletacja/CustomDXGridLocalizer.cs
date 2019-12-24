using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KpInfohelp
{
    public class CustomDXGridLocalizer : GridControlLocalizer
    {
        protected override void PopulateStringTable()
        {
            base.PopulateStringTable();
            AddString(GridControlStringId.MenuColumnSortAscending, "Sortuj Rosnąco");
            AddString(GridControlStringId.MenuColumnSortBySummaryAscending, "Sortuj Rosnąco");
            AddString(GridControlStringId.MenuColumnSortDescending, "Sortuj Malejąco");
            AddString(GridControlStringId.MenuColumnClearSorting, "Wyczyść Sortowanie");
            AddString(GridControlStringId.MenuColumnGroup, "Grupuj po tej kolumnie");
            AddString(GridControlStringId.MenuColumnShowGroupPanel, "Pokaż Panel Grupowania");
            AddString(GridControlStringId.MenuColumnShowColumnChooser, "Wybierz Kolumny");
            AddString(GridControlStringId.MenuColumnBestFit, "Dopasuj Szerokość");
            AddString(GridControlStringId.MenuColumnBestFitColumns, "Dopasuj Szerokość (wszystkie)");
            AddString(GridControlStringId.MenuColumnFilterEditor, "Edytuj Filtrowanie...");
            AddString(GridControlStringId.MenuColumnShowSearchPanel, "Pokaż Panel Wyszukiwania");
            AddString(GridControlStringId.GridGroupPanelText, "Przeciągnij tu nagłówek kolumny aby pogrupować");
            AddString(GridControlStringId.MenuGroupPanelFullCollapse, "Zwiń");
            AddString(GridControlStringId.MenuGroupPanelFullExpand, "Rozwiń");
            AddString(GridControlStringId.MenuGroupPanelClearGrouping, "Wyczyść Grupowanie");
            AddString(GridControlStringId.MenuColumnHideGroupPanel, "Ukryj Panel Grupowania");
            AddString(GridControlStringId.ExcelColumnFilterPopupSearchNullText, "Wyszukaj");
            AddString(GridControlStringId.DefaultGroupColumnSummaryFormatString_Count, "Zlicz={0}");
            AddString(GridControlStringId.DefaultGroupColumnSummaryFormatStringInSameColumn_Count, "Zlicz={0}");
            AddString(GridControlStringId.DefaultGroupSummaryFormatString_Count, "Zlicz={0}");
            AddString(GridControlStringId.DefaultTotalSummaryFormatString_Count, "Zlicz={0}");
            AddString(GridControlStringId.DefaultTotalSummaryFormatStringInSameColumn_Count, "Zlicz={0}");
            AddString(GridControlStringId.MenuColumnSortBySummaryCount, "Zlicz");
            AddString(GridControlStringId.MenuFooterCount, "Zlicz");
            AddString(GridControlStringId.MenuFooterCustomize, "Dostosuj...");
            AddString(GridControlStringId.SummaryEditorFormItemsTabCaption, "Element");
            AddString(GridControlStringId.SummaryEditorFormOrderTabCaption, "Kolejność");
            AddString(GridControlStringId.MenuColumnSortBySummaryAverage, "Średnia");
            AddString(GridControlStringId.MenuFooterAverage, "Średnia");
            AddString(GridControlStringId.TotalSummaryEditorFormCaption, "Podsumowanie dla '{0}'");
            AddString(GridControlStringId.TotalSummaryPanelEditorFormCaption, "Podsumowanie dla '{0}'");
        }
    }
}
