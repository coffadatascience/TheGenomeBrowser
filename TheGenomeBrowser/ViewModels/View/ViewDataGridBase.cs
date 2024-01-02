using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.ViewModels.VIewModel.GenericModels;

namespace TheGenomeBrowser.ViewModels.View
{

    /// <summary>
    /// by creating a base class for grids, initialization and setup of grids can be done in a single place
    /// also we can have here main functionality that is shared between all grids
    /// the lower hierarchies may then we specific for type of views and linkage to specific view models
    /// </summary>
    public class ViewDataGridBase : DataGridView
    {

        #region "properties"

        /// <summary>
        /// local var that hold the column width
        /// </summary>
        public int _columnWidth = 50;

        #endregion

        #region "constructor"

        /// <summary>
        /// constructor
        /// </summary>
        public ViewDataGridBase(string nameGridView)
        {

            Name = nameGridView;
            Dock = DockStyle.Fill;
            BackgroundColor = Color.White;
            ForeColor = Color.Black;
            RowHeadersVisible = true;
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = true;
            AllowUserToResizeRows = true;
            AllowUserToResizeColumns = true;
            AllowUserToOrderColumns = true;
            AllowUserToResizeRows = true;
            AllowUserToOrderColumns = true;

        }


        #endregion


        #region "functions"

        /// <summary>
        /// adjust column width to provided width
        /// </summary>
        /// <param name="newColumnWidth"></param>
        public void AdjustColumnWidth(int newColumnWidth)
        {

            //loop all columns of dataGridView and set the column width of all columns to NewWidth
            foreach (DataGridViewColumn column in Columns)
            {
                //set the column width
                column.Width = newColumnWidth;
            }

            //set the local var to the new width --> so we may reapply the same width later
            _columnWidth = newColumnWidth;

            //refresh the datagridview
            Refresh();
        }


        /// <summary>
        /// proceudure that loads a generic view model for a data grid for displaying for instance settings of the assembly report data model
        /// </summary>
        /// <param name="viewModelForDataGridExperimentOverview"></param>
        /// <param name="rowHeaderWidth"></param>
        public void LoadViewModelGenericModelSettingInformationList(ViewModelGenericModelSettingInformationList viewModelGenericModelSettingInformationList, int rowHeaderWidth)
        {

            //clear the datagrid
            this.DataBindings.Clear();
            this.Columns.Clear();
            //release binding grid
            this.DataSource = null;


            //loop the columns 
            foreach (var Column in viewModelGenericModelSettingInformationList.ListDataColumnData)
            {

                string ColumnName = Column.IdIndex.ToString();
                //columns header text (sample Name)
                string ColumnHeader = Column.Label;

                //create a new column
                this.Columns.Add(ColumnName, ColumnHeader);
                //set column width
                this.Columns[ColumnName].Width = _columnWidth;

            }


            //loop the row of the ViewModelDataGrid and add a new row in the datagrid per item
            foreach (var Row in viewModelGenericModelSettingInformationList.ListDataRowData)
            {
                //add a new row to the datagrid
                this.Rows.Add();

                //set the row header to the row index (note these are the same in te row data and column item data so they should match
                this.Rows[Row.IDIndex].HeaderCell.Value = Row.RowHeaderText;
                //set row header width
                this.RowHeadersWidth = rowHeaderWidth;
                //add probe data in the first two columns

                //place the guid also in the tag of the row
                this.Rows[Row.IDIndex].Tag = Row.Identifier;

                //show row
                this.Rows[Row.IDIndex].Visible = true;

            }

            //loop the columns to add the data
            foreach (var Column in viewModelGenericModelSettingInformationList.ListDataColumnData)
            {

                //Now loop the inner item list of the Columns data to fill the row items
                foreach (var ColumnItem in Column.ListDataCellItem)
                {
                    //set the value of the cell
                    //note to correct for the first two columns (the data model itself is only fragment data)
                    this.Rows[ColumnItem.RowIndex].Cells[Column.IdIndex].Value = ColumnItem.Value;

                    //set a cell color background for the cell
                    this.Rows[ColumnItem.RowIndex].Cells[Column.IdIndex].Style.BackColor = ColumnItem.ColorBackgroundCell;

                    //set tooltip
                    this.Rows[ColumnItem.RowIndex].Cells[Column.IdIndex].ToolTipText = ColumnItem.InfoText;

                }

            }
        }

        #endregion

    }
}
