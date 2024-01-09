using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;
using TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.View.AssemblyMolecules
{

    /// <summary>
    /// data grid view that represent the view for ViewModelDataAssemblySources. Here we want show the list of all genes as found in all sources
    /// </summary>
    public class ViewDataGridDataModelAssemblySourceGenesUniqueGeneId : ViewDataGridBase
    {



        #region constructors

        /// <summary>
        /// constructor that takes the name of the grid view as input
        /// </summary>
        /// <param name="nameGridView"></param>
        public ViewDataGridDataModelAssemblySourceGenesUniqueGeneId(string nameGridView) : base(nameGridView)
        {


        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that takes the ViewModelDataAssemblySources and create the list of all genes as found in all sources
        /// </summary>
        /// <param name="viewModelDataAssemblySources"></param>
        public void CreateDataGrid(ViewModelDataAssemblySources viewModelDataAssemblySources)
        {
            //set the data source for the grid
            DataSource = viewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes;

            //adjust column width
            AdjustColumnWidth(_columnWidth);

        }



        #endregion


    }

}
