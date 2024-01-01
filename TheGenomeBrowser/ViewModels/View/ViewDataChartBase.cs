using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion

    }
}
