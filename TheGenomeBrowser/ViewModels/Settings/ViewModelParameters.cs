using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.ViewModels.Settings
{
    /// <summary>
    /// class that holds the parameters for the view model
    /// </summary>
    public static class ViewModelParameters
    {

        #region constants

        /// <summary>
        /// constant for combo box name of the different view of the ImportDataGtfFile and processed data (1. DataModelGtfFile, 2. DataAssemblyReportComments, 3. DataAssemblyReport, 4. DataModelLookupGeneList)
        /// </summary>
        public const string _comboBoxNameViewDataGridImportedDataGtfFile = "comboBoxViewDataGridImportedDataGtfFile";

        #endregion


        #region properties

        /// <summary>
        /// Enum for the different view of the ImportDataGtfFile and processed data (1. DataModelGtfFile, 2. DataAssemblyReportComments, 3. DataAssemblyReport, 4. DataModelLookupGeneList)
        /// </summary>
        public enum EnumViewDataGridImportedDataGtfFile
        {
            [Description("Imported Gtf data")]
            DataModelGtfFile,
            [Description("Assembly comments")]
            DataAssemblyReportComments,
            [Description("Assembly report")]
            DataAssemblyReport,
            [Description("Gene list")]
            DataModelGeneList,
            [Description("Transcript list")]
            DataModelTranscriptList,
            [Description("Exome")]
            DataModelExonList,
            [Description("CDS")]
            DataModelCdsList,
        }


        #endregion


    }
}
