using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.View
{

    /// <summary>
    /// class that is used to display the list of assembly report information as found in the NcbiImportedData DataModelAssemblyReportItems
    /// </summary>
    public class ViewDataGridAssemblyReportList : ViewDataGridBase
    {

        #region constructor

        public ViewDataGridAssemblyReportList(string nameGridView) : base(nameGridView)
        {

        }


        /// <summary>
        /// constructor that takes a DataAssemblyReport data model as input, and set the list of comments as data source for the grid
        /// </summary>
        /// <param name="nameGridView"></param>
        /// <param name="dataModelAssemblyReport"></param>
        public ViewDataGridAssemblyReportList(string nameGridView, DataModels.NCBIImportedData.DataModelAssemblyReport dataModelAssemblyReport) : base(nameGridView)
        {

            //set the data source for the grid
            DataSource = dataModelAssemblyReport.AssemblyReportItemsList;

            //adjust column width
            AdjustColumnWidth(_columnWidth);

        }

        #endregion


    }

}
