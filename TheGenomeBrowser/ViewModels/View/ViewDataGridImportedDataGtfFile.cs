using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;

namespace TheGenomeBrowser.ViewModels.View
{
    /// <summary>
    /// class that holds the view for the imported data GTF file (provide an overview of the imported data so we may examine how to shape the data
    /// inherited from ViewDataGridBase
    /// </summary>
    public class ViewDataGridImportedDataGtfFile : ViewDataGridBase
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="nameGridView"></param>
        public ViewDataGridImportedDataGtfFile(string nameGridView) : base(nameGridView)
        {


        }

        /// <summary>
        /// constructor that takes a GTF data model as input, and set the list of GTF features as data source for the grid
        /// </summary>
        /// <param name="nameGridView"></param>
        /// <param name="dataModelGtfFile"></param>
        public ViewDataGridImportedDataGtfFile(string nameGridView, DataModelGtfFile dataModelGtfFile) : base(nameGridView)
        {

            //set the data source for the grid
            DataSource = dataModelGtfFile.FeaturesList;

            //adjust column width
            AdjustColumnWidth(_columnWidth);

        }



    }
}
