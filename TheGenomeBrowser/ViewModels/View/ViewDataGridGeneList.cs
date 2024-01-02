using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.View
{

    /// <summary>
    /// class for the view data grid processed gene list
    /// </summary>
    public class ViewDataGridGeneList : ViewDataGridBase
    {

        #region fields


        #endregion


        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="nameGridView"></param>
        public ViewDataGridGeneList(string nameGridView) : base(nameGridView)
        {
        }

        #endregion

        #region methods


        /// <summary>
        /// procedure that takes the DataModelLookupGeneList and creates a view for it showing a list of all genes
        /// </summary>
        /// <param name="dataModelLookupGeneList"></param>
        public void CreateView(DataModels.Genes.DataModelLookupGeneList dataModelLookupGeneList)
        {


            //set the data model to the data source of the grid view
            this.DataSource = dataModelLookupGeneList.Genes.Values.ToList();

        }


        #endregion

    }
}
