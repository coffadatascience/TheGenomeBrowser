using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.VIewModel
{

    /// <summary>
    /// viewmodel that created a view model using the dataModelLookupGeneList, which include from relevant information using deeper layers
    /// also this view model can be used for filtering and sorting
    /// </summary>
    public class ViewModelLookupGeneList
    {


        #region fields

        /// <summary>
        /// data model that holds the information of the lookup gene list
        /// </summary>
        public DataModels.Genes.DataModelLookupGeneList DataModelLookupGeneList { get; set; }

        #endregion




    }
}
