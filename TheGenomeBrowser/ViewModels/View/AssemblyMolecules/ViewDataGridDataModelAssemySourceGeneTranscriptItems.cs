using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.View.AssemblyMolecules
{

    /// <summary>
    /// data grid class use to show the view model of ViewModelDataGeneTranscriptItems
    /// </summary>
    public class ViewDataGridDataModelAssemySourceGeneTranscriptItems : ViewDataGridBase
    {

        #region constructors

        /// <summary>
        /// constructor that takes the name of the grid view as input
        /// </summary>
        /// <param name="nameGridView"></param>
        public ViewDataGridDataModelAssemySourceGeneTranscriptItems(string nameGridView) : base(nameGridView)
        {


        }

        #endregion

        #region methods

        /// <summary>
        /// procedure that takes the ViewModelDataGeneTranscriptItems and create the data grid
        /// </summary>
        /// <param name="viewModelDataGeneTranscriptItems"></param>
        public void CreateDataGrid(ViewModelDataGeneTranscriptElementsList viewModelDataGeneTranscriptElementsList)
        {
            //set the data source for the grid
            DataSource = viewModelDataGeneTranscriptElementsList._listElements;

            //adjust column width
            AdjustColumnWidth(_columnWidth);

        }

        #endregion

    }
}
