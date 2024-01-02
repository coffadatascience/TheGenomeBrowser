using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.VIewModel.GenericModels
{

    /// <summary>
    /// generic class that holds the information for the settings information
    /// </summary>
    public class ViewModelGenericModelSettingInformationList
    {


        #region "declarations"


        /// <summary>
        /// Row data intelligence for the data grid
        /// </summary>
        public System.Collections.Generic.List<RowData> ListDataRowData { get; set; }

        /// <summary>
        /// Column data intelligence for the data grid
        /// </summary>
        public System.Collections.Generic.List<ColumnData> ListDataColumnData { get; set; }

        #endregion

    }

    /// <summary>
    /// generic class for the row data
    /// </summary>
    public class RowData
    {
        /// <summary>
        /// identifier for the row
        /// </summary>
        public int IDIndex { get; set; }

        /// <summary>
        /// for the GUID is also added so that the id may be confirmed with certainty
        /// Note that an identifier is typically used for a probe but we may also use row for summarized hierarchies (such as QC)
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// the text that is used in the row header
        /// </summary>
        public string RowHeaderText { get; set; }

        /// <summary>
        /// the text that is used in the row header
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// constructor, passes the id index
        /// </summary>
        /// <param name="idIndex"></param>
        public RowData(int idIndex)
        {
            this.IDIndex = idIndex;
        }
    }

    /// <summary>
    /// class containing the logic to build the column data for the data grid
    /// </summary>
    public class ColumnData
    {

        /// <summary>
        /// identifier for the row
        /// </summary>
        public int IdIndex { get; set; }

        /// <summary>
        // Typically the sample name
        /// </summary>
        public string Label { get; set; }


        /// <summary>
        /// column item list that are used for display of values
        /// </summary>
        public System.Collections.Generic.List<DataCellItem> ListDataCellItem { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="label"></param>
        public ColumnData(int IdIndex, string label)
        {

            //suare index
            this.IdIndex = IdIndex;

            //set the label
            this.Label = label;

            //initialize the list of data cell items
            this.ListDataCellItem = new System.Collections.Generic.List<DataCellItem>();

        }
    }

    /// <summary>
    /// alternative for a data cell item (here the data cell values are setup to be collected in the row hiearchy)
    /// </summary>
    public class DataCellItem
    {

        #region "declarations"


        /// <summary>
        /// variable for the row index
        /// </summary>
        public int RowIndex { get; set; }

        /// <summary>
        /// value of the cell
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// variable that can set ColorBackground for grid cells
        /// </summary>
        public Color ColorBackgroundCell { get; set; }

        /// <summary>
        /// variable for info text (e.g. loaded in the tooltip)
        /// </summary>
        public string InfoText { get; set; }

        #endregion

        /// <summary>
        /// constructor for a data cells item
        /// note that this version has a cell color background coding that uses default warning values and matches the color to the value
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="value"></param>
        public DataCellItem(int rowIndex, string value)
        {
            this.RowIndex = rowIndex;
            this.Value = value;

        }

        /// <summary>
        /// alternative constructor for a data cells item, that sets a color background
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="value"></param>
        /// <param name="colorBackground"></param>
        public DataCellItem(int rowIndex, string value, System.Drawing.Color colorBackground)
        {
            this.RowIndex = rowIndex;
            this.Value = value;
            this.ColorBackgroundCell = colorBackground;
        }

    }


}
