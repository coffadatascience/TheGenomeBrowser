using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;
using TheGenomeBrowser.ViewModels.Settings;
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
        /// Combo box for the view settings
        /// </summary>
        public ComboBox comboBoxConditionalFormatExperimentView { get; set; }

        #endregion

        #region settings

        /// <summary>
        /// property that hold the enum for the current view as by the settings viewmodelparameter
        /// </summary>
        public TheGenomeBrowser.ViewModels.Settings.ViewModelParameters.EnumViewDataGridImportedDataGtfFile CurrentViewSettingsEnum { get; set; }


        #region DataModels 

        /// <summary>
        /// data model for imported GTF file
        /// </summary>
        public DataModelGtfFile DataModelGtfFile { get; set; }

        /// <summary>
        /// data model for the GTF assembly report information (this data can be used to retrieve the chromosome number for the gene and contains information about the assembly/analysis\submitters)
        /// </summary>
        public DataModelAssemblyReport DataModelGtfAssemblyReport { get; set; }

        /// <summary>
        /// var for the data model lookup gene list
        /// </summary>
        public DataModels.Genes.DataModelLookupGeneList DataModelLookupGeneList { get; set; }

        #endregion

        #region ViewModels

        /// <summary>
        /// view model for imported GTF file
        /// </summary>
        public ViewModelGtfFile ViewModelGtfFile { get; set; }

        /// <summary>
        /// view model for the assembly report comments
        /// </summary>
        public ViewModelAssemblyReportComments ViewModelAssemblyReportComments { get; set; }

        #endregion


        #region Views

        /// <summary>
        /// var for view data grid imported data GTF file
        /// </summary>
        public ViewDataGridImportedDataGtfFile ViewDataGridImportedDataGtfFile { get; set; }

        /// <summary>
        /// var for view data grid assembly report comments
        /// </summary>
        public ViewDataGridAssemblyReportComments ViewDataGridAssemblyReportComments { get; set; }

        /// <summary>
        /// var for view data grid assembly report list
        /// </summary>
        public ViewDataGridAssemblyReportList ViewDataGridAssemblyReportList { get; set; }

        /// <summary>
        /// view for the processed gene list
        /// </summary>
        public ViewDataGridGeneList ViewDataGridGeneList { get; set; }

        #endregion


        #endregion

        #region constructor

        //constructor
        public HandlerImportedGtfFileData()
        {
            //set column width
            _columnWidth = 70;

            //set the current view settings enum
            CurrentViewSettingsEnum = TheGenomeBrowser.ViewModels.Settings.ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelGtfFile;

            //create combo box for the view settings
            this.comboBoxConditionalFormatExperimentView = new ComboBox();
            //set size
            this.comboBoxConditionalFormatExperimentView.Size = new Size(150, 50);
            //place below the conditional format combo box
            this.comboBoxConditionalFormatExperimentView.Location = new Point(10, 70); //--> place the checkbox higher, so the lowest range may all be combo filters
            //set button text color to black
            this.comboBoxConditionalFormatExperimentView.ForeColor = System.Drawing.SystemColors.ControlText;
            //set the name of the combo box by the constant as found in the viewmodelparameters
            this.comboBoxConditionalFormatExperimentView.Name = TheGenomeBrowser.ViewModels.Settings.ViewModelParameters._comboBoxNameViewDataGridImportedDataGtfFile;
            this.comboBoxConditionalFormatExperimentView.Text = "Data views";
            this.comboBoxConditionalFormatExperimentView.Dock = DockStyle.None;
            this.comboBoxConditionalFormatExperimentView.DropDownStyle = ComboBoxStyle.DropDownList;
            //create a tooltip for comboBoxProbeFragmentDisplayType that explains what it does
            ToolTip toolTip4 = new ToolTip();
            //set tooltip2 text to ("This control can be used to change set of displayed samples based on the sample quality criteria")
            toolTip4.SetToolTip(this.comboBoxConditionalFormatExperimentView, "This control can be used to switch between the different views of the imported and procesed data");
            // 4. combo box with quality control criteria - add the enum values to the combobox
            LoadEnumEnumViewDataGridImportedDataGtfFileDisplayCombo(this.comboBoxConditionalFormatExperimentView);

            //create data model
            DataModelGtfFile = new DataModels.NCBIImportedData.DataModelGtfFile();

            //create view model
            ViewModelGtfFile = new ViewModelGtfFile();
            //create view model for the assembly report comments
            ViewModelAssemblyReportComments = new ViewModelAssemblyReportComments();

            //create view data grid imported data GTF file
            ViewDataGridImportedDataGtfFile = new ViewDataGridImportedDataGtfFile("ViewDataGridImportedDataGtfFile");
            //new view data grid assembly report comments
            ViewDataGridAssemblyReportComments = new ViewDataGridAssemblyReportComments("ViewDataGridAssemblyReportComments");
            //new view data grid assembly report list
            ViewDataGridAssemblyReportList = new ViewDataGridAssemblyReportList("ViewDataGridAssemblyReportList");

        }

        #endregion

        #region Methods

        /// <summary>
        /// rocedure that sets the viewmodels and views for the assembly report information
        /// </summary>
        /// <param name="dataModelAssemblyReport"></param>
        public void ProcessDataModelAssemblyReport(DataModelAssemblyReport dataModelAssemblyReport)
        {
            //set the data model in the handler
            this.DataModelGtfAssemblyReport = dataModelAssemblyReport;

            //process the data model in the ViewModelAssemblyReportComments
            ViewModelAssemblyReportComments.SetDataModelAssemblyReportComments(dataModelAssemblyReport);
            //set the data source for the comments grid
            this.ViewDataGridAssemblyReportComments.LoadViewModelGenericModelSettingInformationList(ViewModelAssemblyReportComments, 200);

            //set the data source for the grid
            this.ViewDataGridAssemblyReportList.DataSource = dataModelAssemblyReport.AssemblyReportItemsList;

            //set the text of the combo box to the current view
            this.comboBoxConditionalFormatExperimentView.Text = "Assembly report";
        }


        #endregion

        #region methods "processing GTF file imported data"

        /// <summary>
        /// procedure that take the DataModelGtfFile and processes this to a list of genes (based on GeneId, that will become the gene name and unique identifier)
        /// </summary>
        public void ProcessGtfFileImportedDataSetView()
        {

            //create a new DatamodelLookupGeneList object local
            this.DataModelLookupGeneList = new DataModels.Genes.DataModelLookupGeneList();

            //create a new cictionary that holds the gene name and the gene object
            Dictionary<string, DataModels.Genes.DataModelLookupGene> dictionaryGeneNameGeneObject = new Dictionary<string, DataModels.Genes.DataModelLookupGene>();

            //set the dictionary of genes in the data model lookup gene list
            this.DataModelLookupGeneList.Genes = dictionaryGeneNameGeneObject;

            //boolean that remembers if the user responds positive to continue import of the GTF file wihout an assembly report
            bool continueImportWithoutAssemblyReport = false;

            //loop all features in the GTF file
            foreach (GTFFeature feature in DataModelGtfFile.FeaturesList)
            {

                //get the gene id
                string geneId = feature.GeneId;

                //Check if we have a DataModelGtfAssemblyReport, if this is the case, we may use this to retrieve the chromosome number by looking up the accession number in the DataModelGtfAssemblyReport

                //set a var for the chromosome
                string chromosome = "?";

                //check if we have a DataModelGtfAssemblyReport
                if (DataModelGtfAssemblyReport != null)
                {
                    //get the chromosome number from the DataModelGtfAssemblyReport
                    chromosome = DataModelGtfAssemblyReport.GetChromosomeNumber(feature.Seqname);
                }
                else
                {

                    //ask the user if he/she wants to continue without an assembly report
                    if (!continueImportWithoutAssemblyReport)
                    {
                        //ask the user if he/she wants to continue without an assembly report
                        continueImportWithoutAssemblyReport = ViewModelGtfFile.AskUserToContinueWithoutAssemblyReport();
                    }

                    //check if the user wants to continue without an assembly report
                    if (continueImportWithoutAssemblyReport)
                    {
                        //set the chromosome to the seqname
                        chromosome = feature.Seqname;
                    }
                    else
                    {
                        //stop the import
                        return;
                    }

                }


                //check if the gene id is not null or empty
                if (!string.IsNullOrEmpty(geneId))
                {

                    //check if the gene id is not yet in the dictionary
                    if (!dictionaryGeneNameGeneObject.ContainsKey(geneId))
                    {

                        //get location start
                        int locationStart = feature.Start;
                        int locationEnd = feature.End;
                        //var for seqname
                        string seqname = feature.Seqname;

                        //create a new data model lookup gene object
                        DataModels.Genes.DataModelLookupGene dataModelLookupGene = new DataModels.Genes.DataModelLookupGene();

                        //set the gene name, chromosome and start and end location
                        dataModelLookupGene.DataModelLookupGeneSetupNewItem(geneId, chromosome, locationStart, locationEnd, seqname);

                        //create a new list of gene elements
                        List<DataModels.Genes.DataModelLookupGeneElement> listGeneElements = new List<DataModels.Genes.DataModelLookupGeneElement>();

                        //set the list of gene elements
                        dataModelLookupGene.GeneElements = listGeneElements;

                        //add the gene to the dictionary
                        dictionaryGeneNameGeneObject.Add(geneId, dataModelLookupGene);

                    }

                    //get the gene object from the dictionary
                    DataModels.Genes.DataModelLookupGene dataModelLookupGeneFromDictionary = dictionaryGeneNameGeneObject[geneId];

                    //create a new gene element
                    DataModels.Genes.DataModelLookupGeneElement dataModelLookupGeneElement = new DataModels.Genes.DataModelLookupGeneElement();

                    //add the gene element to the list of gene elements
                    dataModelLookupGeneFromDictionary.GeneElements.Add(dataModelLookupGeneElement);

                }
                //create and else if, that prints a message in the debugger that the gene id is null or empty, but that includes the AttributesString
                else if (!string.IsNullOrEmpty(feature.AttributesString))
                {
                    //check if we are in debug mode
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        //print message in debugger
                        System.Diagnostics.Debug.WriteLine("Gene id is null or empty, but AttributesString is not null or empty: " + feature.AttributesString);
                    }

                }
         
            }

            //create a new view data grid gene list
            ViewDataGridGeneList = new ViewDataGridGeneList("ViewDataGridGeneList");

            //create the view for the gene list
            ViewDataGridGeneList.CreateView(this.DataModelLookupGeneList);

        }



        #endregion


        #region methods "view settings"

        /// <summary>
        /// procedure that loads the enum types in the combo box for the EnumViewDataGridImportedDataGtfFile
        /// </summary>
        /// <param name="cbo"></param>
        public static void LoadEnumEnumViewDataGridImportedDataGtfFileDisplayCombo(ComboBox cbo)
        {
            cbo.DataSource = Enum
                .GetValues(typeof(ViewModelParameters.EnumViewDataGridImportedDataGtfFile))
                .Cast<Enum>()
                .Select(value => new
                {
                    Description = (Attribute.GetCustomAttribute(value.GetType().GetField(value.ToString()), typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? value.ToString(),
                    value
                })
                .OrderBy(item => item.value)
                .ToList();
            cbo.DisplayMember = "Description";
            cbo.ValueMember = "value";
        }


        #endregion



    }

}
