using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.ViewModels.View;
using TheGenomeBrowser.ViewModels.VIewModel;

namespace TheGenomeBrowser.ViewModels
{

    /// <summary>
    /// handler for imported GTF file data models, viewmodels and view
    /// </summary>
    public class HandlerImportedGtfFileData
    {

        #region properties

        /// <summary>
        /// var for column width
        /// </summary>
        public int _columnWidth { get; set; }

        /// <summary>
        /// data model for imported GTF file
        /// </summary>
        public DataModels.DataModelGtfFile DataModelGtfFile { get; set; }

        /// <summary>
        /// view model for imported GTF file
        /// </summary>
        public ViewModelGtfFile ViewModelGtfFile { get; set; }

        /// <summary>
        /// var for view data grid imported data GTF file
        /// </summary>
        public ViewDataGridImportedDataGtfFile ViewDataGridImportedDataGtfFile { get; set; }

        #endregion

        #region constructor

        //constructor
        public HandlerImportedGtfFileData()
        {
            //set column width
            _columnWidth = 50;

            //create data model
            DataModelGtfFile = new DataModels.DataModelGtfFile();

            //create view model
            ViewModelGtfFile = new ViewModelGtfFile();

            //create view data grid imported data GTF file
            ViewDataGridImportedDataGtfFile = new ViewDataGridImportedDataGtfFile("ViewDataGridImportedDataGtfFile");

        }

        #endregion

        #region methods



        #endregion






    }

}
