using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.View.AssemblyMolecules
{

    /// <summary>
    /// view model class that represent the data for the view model class ViewModelDataGeneTranscript. This view model is used to create a list of all unique transcripts (currently 20240108 with decent source of the annotation file about 60000 genes are listed over 5 different sources)
    /// </summary>
    public class ViewDataGridDataModelAssemblySourceGeneTranscriptElementList : ViewDataGridBase
    {

        #region fields

        #endregion

        #region constructors

        public ViewDataGridDataModelAssemblySourceGeneTranscriptElementList(string nameGridView) : base(nameGridView)
        {

        }

        #endregion

        #region methods

        /// <summary>
        /// method that takes ViewModelDataGeneTranscripts and create the data grid
        /// </summary>
        /// <param name="viewModelDataGeneTranscripts"></param>
        public void CreateDataGrid(ViewModelDataGeneTranscriptsList viewModelDataGeneTranscripts)
        {
            //set the data source for the grid
            DataSource = viewModelDataGeneTranscripts.ListViewModelDataGeneTranscriptsList;

            //adjust column width
            AdjustColumnWidth(_columnWidth);

        }

        #endregion

    }

}
