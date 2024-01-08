using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;
using TheGenomeBrowser.DataModels.Genes;
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
        /// constant for used name of start line of gene information (states gene)
        /// </summary>
        public const string _startLineGene = "gene";

        /// <summary>
        /// constant for used name of start line of transcript information (states transcript)
        /// </summary>
        public const string _startLineTranscript = "transcript";

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

        /// <summary>
        /// var for assembly source data model
        /// </summary>
        public DataModels.AssemblyMolecules.DataModelAssemblySource DataModelAssemblySource { get; set; }

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


        //Note JCO -- > this region processes the GTF file feature list up into sources and molecules, rather than trying immediatly to genes
        //              this approach may remove a lot of double which makes the gene list smaller and more manageable (also with extra structure there is a more appropriate hierarchy, minimizing the number of elements in the list)
        #region  methods "processing the gene list and establishing TheGenome data model"

        /// <summary>
        /// procedure that takes the datamodelGtfFile and the annotation file and processes that into appropriate data models by source, molecule, gene, transcript, and then start_codon, end_codon, exon, cds
        /// Rather than filling out all the data, we will first create the main layers and split the data in to sources, molecules, genes, transcripts. When the list is processes, then we can add the other type in their appropriate places.
        /// --> Note that (even though the annotation list seem sorted, we cannot take this for sure with objects) we will need to go through the entire list to get all genes (by feature entree "gene"), this first has to be finished before getting the transcripts, else we may encounter a transcript without having made an entree for the gene (note that the list seems sorted, but we cannot rely on that), as such we will pass a few time through the list to make all required hierarchical levels.
        /// ---> we may however collect collect all unique transcript names and then process the list again to get the transcripts, but this may be more complicated than just passing through the list a few times
        /// </summary>
        /// <param name="progress"></param>
        public void ProcessNcbiDataToAssemblyBySource() //IProgress<int> progress)
        {

            //boolean that remembers if the user responds positive to continue import of the GTF file wihout an assembly report
            bool continueImportWithoutAssemblyReport = false;
            //int with total number of features
            int totalNumberOfFeatures = DataModelGtfFile.FeaturesList.Count;
            // int to count the number of features
            int numberOfFeatures = 0;
            //int to coutn update on every 100 features
            int numberOfFeaturesUpdate = 0;

            //create a new dictionary that has the source as key and the data model assembly source as value
            Dictionary<string, DataModels.AssemblyMolecules.DataModelAssemblySource> dictionarySourceDataModelAssemblySource = new Dictionary<string, DataModels.AssemblyMolecules.DataModelAssemblySource>();


            //loop all features in the GTF file
            foreach (GTFFeature feature in DataModelGtfFile.FeaturesList)
            {

                //----------------------------------------------------
                // 1. process the source
                //----------------------------------------------------

                //var for source
                string source = feature.Source;

                //local var for DataModelAssemblySource (either from the dictionary or a new one)
                DataModels.AssemblyMolecules.DataModelAssemblySource dataModelAssemblySource = null;

                //check if the source is in the dictionarySourceDataModelAssemblySource, if not add it by creating a new DataModelAssemblySource
                if (!dictionarySourceDataModelAssemblySource.ContainsKey(source))
                {
                    //create a new DataModelAssemblySource
                    dataModelAssemblySource = new DataModels.AssemblyMolecules.DataModelAssemblySource(source);
                    //add the data model assembly source to the dictionary
                    dictionarySourceDataModelAssemblySource.Add(source, dataModelAssemblySource);
                }
                else
                {
                    //get the data model assembly source from the dictionary
                    dataModelAssemblySource = dictionarySourceDataModelAssemblySource[source];
                }


                //----------------------------------------------------
                // 2. process the molecule
                //----------------------------------------------------

                //var for object that holds the molecule (this is typically a chromosome, but can also be a plasmid or other molecule)
                DataModels.AssemblyMolecules.DataModelMolecule dataModelMolecule = null;

                //use GetChromosomeNumber, to get the chromosome number
                string MoleculeChromosome = GetChromosomeNumber(DataModelGtfAssemblyReport, feature, ref continueImportWithoutAssemblyReport);

                //check if the molecule is in the dictionary, if not add it by creating a new DataModelMolecule
                if (!dataModelAssemblySource.DictionaryOfMolecules.ContainsKey(MoleculeChromosome))
                {
                    //create a new DataModelMolecule
                    dataModelMolecule = new DataModels.AssemblyMolecules.DataModelMolecule(MoleculeChromosome);
                    //add the data model molecule to the dictionary
                    dataModelAssemblySource.DictionaryOfMolecules.Add(MoleculeChromosome, dataModelMolecule);
                }
                else
                {
                    //get the data model molecule from the dictionary
                    dataModelMolecule = dataModelAssemblySource.DictionaryOfMolecules[MoleculeChromosome];

                }

                //----------------------------------------------------
                // 3. process the gene (seq id)
                //----------------------------------------------------

                //var for feature type
                string featureType = feature.FeatureType;

                //get the enum for the feature type (GetFeatureType)
                var featureTypeEnum = SettingsAssemblySource.GetFeatureType(featureType);

                //check if the feature type is gene (if it is a gene we need to create a new seq id and add it to the dictionary of seq ids)
                if (featureTypeEnum == SettingsAssemblySource.FeatureType.gene)
                {
                    //local object for DataModelGeneId
                    DataModels.AssemblyMolecules.DataModelGeneId DataModelGeneId = null;

                    //var for the gene id
                    string geneId = feature.GeneId;

                    //check if the gene id is not null or empty
                    if (!string.IsNullOrEmpty(geneId))
                    {

                        //check if the gene id is not yet in the dictionary
                        if (!dataModelMolecule.GeneIds.ContainsKey(geneId))
                        {

                            // Note JCO --> here we may recognize that it is not very useful to try to untanlge the GTF file direct (we may thus remove a lot of filed of the data model reads and instead retain the lines)
                            // Alternatively we may see how far we get with what we have

                            //local var with features needed to construct DataModelGeneId
                            string geneName = feature.Gene;
                            string geneDescription = feature.AttributesString;
                            string strand = feature.Strand;
                            int startLocation = feature.Start;
                            int endLocation = feature.End;
                            string db_Xref_One = feature.DbXref;
                            string db_Xref_Two = feature.DbXref;
                            string gb_Key = feature.GbKey;
                            string gene_Biotype = feature.TranscriptBiotype;
                            string gene_Synonym = feature.GeneId;

                            //create a new DataModelGeneId
                            DataModelGeneId = new DataModels.AssemblyMolecules.DataModelGeneId(geneId, geneName, geneDescription, strand, startLocation, endLocation, db_Xref_One, db_Xref_Two, gb_Key, gene_Biotype, gene_Synonym);
                            
                            //add the gene to the dictionary
                            dataModelMolecule.GeneIds.Add(geneId, DataModelGeneId);

                        }
                        else
                        {
                            //check if we are in debug mode
                            if (System.Diagnostics.Debugger.IsAttached)
                            {
                                //print message in debugger
                                System.Diagnostics.Debug.WriteLine("Gene id is already in the dictionary: " + geneId);
                            }
                        }

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


            }


            //increase the number of features
            numberOfFeatures++;
            //increase the number of features update
            numberOfFeaturesUpdate++;
        }

    

    /// <summary>
    /// procedure that takes a DataModelGtfAssemblyReport, GTFFeature feature, as well as a boolean (continueImportWithoutAssemblyReport) and returns a chromosome (string)
    /// </summary>
    /// <param name="dataModelAssemblyReport"></param>
    /// <param name="feature"></param>
    /// <param name="continueImportWithoutAssemblyReport"></param>
    /// <returns></returns>
    private string GetChromosomeNumber(DataModelAssemblyReport dataModelAssemblyReport, GTFFeature feature, ref bool continueImportWithoutAssemblyReport)
    {
        //set a var for the chromosome
        string chromosome = "?";

        //check if we have a DataModelGtfAssemblyReport
        if (dataModelAssemblyReport != null)
        {
            //get the chromosome number from the DataModelGtfAssemblyReport
            chromosome = dataModelAssemblyReport.GetChromosomeNumber(feature.Seqname);
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
                return null;
            }

        }

        //return the chromosome
        return chromosome;
    }


    #endregion


    #region methods "processing GTF file imported data into unique gene list"

    /// <summary>
    /// async procedure that processes the GTF file imported data set view
    /// </summary>
    /// <returns></returns>
    public async Task<bool> ProcessGtfFileImportedDataSetViewAsync(IProgress<int> progress)
        {

            //read GTF file async
            await Task.Run(() =>
            {
                //process the data model
                ProcessGtfFileImportedDataSetView(progress);

            });

            //return true
            return true;

        }

        /// <summary>
        /// procedure that take the DataModelGtfFile and processes this to a list of genes (based on GeneId, that will become the gene name and unique identifier)
        /// </summary>
        private void ProcessGtfFileImportedDataSetView(IProgress<int> progress)
        {


            //int with total number of features
            int totalNumberOfFeatures = DataModelGtfFile.FeaturesList.Count;
            // int to count the number of features
            int numberOfFeatures = 0;
            //int to coutn update on every 100 features
            int numberOfFeaturesUpdate = 0;

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

                        //----------------------------------------------------
                        // New Dictionary Item
                        //----------------------------------------------------

                        //get location start
                        int locationStart = feature.Start;
                        int locationEnd = feature.End;

                        //create a new data model lookup gene object
                        DataModels.Genes.DataModelLookupGene dataModelLookupGene = new DataModels.Genes.DataModelLookupGene();

                        //---------------------------------------------
                        // Note JCO --> we cannot set the start and end here, as we only have a single feature
                        // The start and end thus need to be established afterwards (or obtained from the innner list of elements)
                        //---------------------------------------------

                        //set the gene name, chromosome and start and end location
                        dataModelLookupGene.DataModelLookupGeneSetupNewItem(geneId, chromosome, feature.Seqname);

                        //add the gene to the dictionary
                        dictionaryGeneNameGeneObject.Add(geneId, dataModelLookupGene);

                        //new element item
                        DataModels.Genes.DataModelLookupGeneElement dataModelLookupGeneElement = new DataModels.Genes.DataModelLookupGeneElement();

                        //create a key for the element
                        string key = feature.GeneId + "-" + feature.Start + "-[0]";

                        //setup item
                        dataModelLookupGeneElement.DataModelLookupGeneElementSetupNewItem(feature.FeatureType, feature.ExonNumber, feature.Start, feature.End);

                        //add the gene element to the list of gene elements
                        dataModelLookupGene.Elements.Add(key + "-" + feature.Start, dataModelLookupGeneElement);

                        //Ignore the unique elements for feature type: gene and transcript, as denoted by constant _startLineGene and _startLineTranscript
                        if (feature.FeatureType != _startLineGene && feature.FeatureType != _startLineTranscript)
                        {
                            // var key for unique elements is feature type + exon number
                            string ValueUniqueElements = feature.FeatureType + "-" + feature.ExonNumber;

                            //add new items to the dictionary unique elements
                            dataModelLookupGene.UniqueElements.Add(feature.Start, ValueUniqueElements);
                        }
                        //----------------------------------------------------

                    }
                    else
                    {

                        //----------------------------------------------------
                        // process existing dictionary item (gene)
                        //----------------------------------------------------

                        //get the gene object from the dictionary
                        DataModels.Genes.DataModelLookupGene dataModelLookupGeneFromDictionary = dictionaryGeneNameGeneObject[geneId];


                        //----------------------------------------------------
                        // Note JCO --> it may be true that there are alternative accession numbers to the same gene
                        //----------------------------------------------------
                        // check if the current accession number is equal to that of the gene object
                        if (dataModelLookupGeneFromDictionary.GenBankAccn != feature.Seqname)
                        {

                            //if the alternative positions is empty, then add the current seqname, if its not empty add a comma and the seqname
                            if (string.IsNullOrEmpty(dataModelLookupGeneFromDictionary.AlternativeAccn))
                            {
                                //set the alternative positions
                                dataModelLookupGeneFromDictionary.AlternativeAccn = feature.Seqname;
                            }
                            else
                            {
                                //add the alternative positions
                                dataModelLookupGeneFromDictionary.AlternativeAccn += ", " + feature.Seqname;
                            }
          
                        }
                        //----------------------------------------------------


                        //----------------------------------------------------
                        // Process Element
                        //----------------------------------------------------

                        //create a key for the element
                        string key = feature.GeneId + "-" + feature.Start;

                        // add a tag that the denoted the number of this element as it is being in the list by + "-[n]";
                        // Note JCO --> this allows adding of doubles so we may investigate double data and what we do not need
                        key += "-[" + dataModelLookupGeneFromDictionary.Elements.Count + "]";

                        //check if the element is not yet in the dictionary
                        if (!dataModelLookupGeneFromDictionary.Elements.ContainsKey(key))
                        {

                            //new element item
                            DataModels.Genes.DataModelLookupGeneElement dataModelLookupGeneElement = new DataModels.Genes.DataModelLookupGeneElement();

                            //setup item
                            dataModelLookupGeneElement.DataModelLookupGeneElementSetupNewItem(feature.FeatureType, feature.ExonNumber, feature.Start, feature.End);

                            //add the gene element to the list of gene elements
                            dataModelLookupGeneFromDictionary.Elements.Add(key, dataModelLookupGeneElement);

                        }
                        else
                        {
                            //check if we are in debug mode
                            if (System.Diagnostics.Debugger.IsAttached)
                            {
                                //print message in debugger
                                System.Diagnostics.Debug.WriteLine("This should be a unique counter. Fault at line : " + feature.AttributesString);
                            }
                        }
                        //----------------------------------------------------

                        //----------------------------------------------------
                        //check if the unique elements dictionary contains the start location
                        //----------------------------------------------------
                        if (!dataModelLookupGeneFromDictionary.UniqueElements.ContainsKey(feature.Start))
                        {

                            //Ignore the unique elements for feature type: gene and transcript, as denoted by constant _startLineGene and _startLineTranscript
                            if (feature.FeatureType != _startLineGene && feature.FeatureType != _startLineTranscript)
                            {
                                // var key for unique elements is feature type + exon number
                                string ValueUniqueElements = feature.FeatureType + "-" + feature.ExonNumber;

                                //add new items to the dictionary unique elements
                                dataModelLookupGeneFromDictionary.UniqueElements.Add(feature.Start, ValueUniqueElements);
                            }

                        }

                    }


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

                //check if the progress is not null
                if (progress != null)
                {

                    //check if we need to update the progress
                    if (numberOfFeaturesUpdate > 100)
                    {
                        //reset the number of features update
                        numberOfFeaturesUpdate = 0;

                        //var for the progress percentage in double
                        double progressPercentageDouble = ((double)numberOfFeatures / (double)totalNumberOfFeatures) * 100;

                        //calculate the progress
                        int progressPercentage = (int)Math.Round(progressPercentageDouble);

                        //report the progress
                        progress.Report(progressPercentage);


                    }
                }
                
                //increase the number of features
                numberOfFeatures++;
                //increase the number of features update
                numberOfFeaturesUpdate++;
            }

            //sort all the unique elements by start location
            this.DataModelLookupGeneList.SortAllUniqueElementsByStartLocation();

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
