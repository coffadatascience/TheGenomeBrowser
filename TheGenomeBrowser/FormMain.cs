using System.ComponentModel;
using System.Windows.Forms;
using TheGenomeBrowser.COM.ExportVersion01;
using TheGenomeBrowser.DataModels.NCBIImportedData;
using TheGenomeBrowser.Readers;
using TheGenomeBrowser.ViewModels;
using TheGenomeBrowser.ViewModels.Settings;
using TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules;

namespace TheGenomeBrowser
{
    /// <summary>
    /// general form
    /// </summary>
    public partial class FormMain : Form
    {

        #region fields

        /// <summary>
        /// var for the handler for imported GTF file data
        /// </summary>
        private HandlerImportedGtfFileData _handlerImportedGtfFileData;

        /// <summary>
        /// var to print the debug in the debug window (we want to turn this off so we can use the console for other things and the processing is much faster)
        /// but we also want an easy trigger to get the output in the debug window
        /// </summary>
        private bool _printDebug = false;

        /// <summary>
        /// var string for file path of save XML
        /// </summary>
        private string _filePathSaveXml = "";

        #endregion

        #region properties

        /// <summary>
        /// constant for the name of the split container 1
        /// </summary>
        private const string SPLIT_CONTAINER_1 = "splitContainer1";

        /// <summary>
        /// constant that holds the name of the GFF3 button
        /// </summary>
        private const string BUTTON_IMPORT_GTF = "Import GTF file";

        /// <summary>
        /// constant for button name that processes imported GTF data into a.o. gene lists (but may be all usefull data models)
        /// </summary>
        private const string BUTTON_PROCESS_GTF = "Process GTF file";

        /// <summary>
        /// private const string for button Untangle GTF data
        /// </summary>
        private const string BUTTON_UNTANGLE_GTF = "Untangle GTF data";

        /// <summary>
        /// constant for name of button used to process all transcript elements
        /// </summary>
        private const string BUTTON_PROCESS_TRANSCRIPT_ELEMENTS = "Process Exon/CDS";

        /// <summary>
        /// constant for name of data grid view of GTF datamodel
        /// </summary>
        private const string DATA_GRID_VIEW_GTF_DATA_MODEL = "ViewDataGridImportedDataGtfFile";

        /// <summary>
        /// progress bar (bar that shows the progress)
        /// </summary>
        private ProgressBar progressBar = new ProgressBar();

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //set name of form (title) The Genome Browser
            this.Text = "The Genome Browser";

            //set size of form
            this.Size = new Size(1200, 800);

            //new handler for imported GTF file data
            _handlerImportedGtfFileData = new HandlerImportedGtfFileData();

            //create a split container that holds the menu (smal panel in the top of the form) and the main panel (the panel that holds the genome browser)
            SplitContainer splitContainerMain = new SplitContainer();
            this.Controls.Add(splitContainerMain);
            
            //set name of split container
            splitContainerMain.Name = SPLIT_CONTAINER_1;
            splitContainerMain.Dock = DockStyle.Fill;
            splitContainerMain.Orientation = Orientation.Horizontal;
            splitContainerMain.SplitterDistance = 120;
            splitContainerMain.SplitterWidth = 8;
            splitContainerMain.BackColor = Color.LightGray;
            splitContainerMain.Panel1.BackColor = Color.LightGray;
            splitContainerMain.Panel2.BackColor = Color.LightGray;
            splitContainerMain.Panel1MinSize = 100;
            splitContainerMain.Panel2MinSize = 100;
            splitContainerMain.IsSplitterFixed = false;
            splitContainerMain.BorderStyle = BorderStyle.Fixed3D;

            //add a new button to the form that states import GTF3 file
            Button buttonImportGFF3 = new Button();
            //set name of button
            buttonImportGFF3.Name = BUTTON_IMPORT_GTF;
            buttonImportGFF3.Text = "Import GTF file";
            //set size
            buttonImportGFF3.Location = new Point(10, 10);
            buttonImportGFF3.Size = new Size(100, 50);
            buttonImportGFF3.Click += new EventHandler(buttonImportGTF3_Click);
            splitContainerMain.Panel1.Controls.Add(buttonImportGFF3);

            //add a new button to the form that triggers and event to read the Assembly report file (this file can be used to lookup the chromosome to each accession number --> or that it is on a different molecule)
            Button buttonReadAssemblyReportFile = new Button();
            //set name of button
            buttonReadAssemblyReportFile.Name = "buttonReadAssemblyReportFile";
            buttonReadAssemblyReportFile.Text = "Read Assembly Report file";
            //set size
            buttonReadAssemblyReportFile.Location = new Point(120, 10);
            buttonReadAssemblyReportFile.Size = new Size(150, 50);
            buttonReadAssemblyReportFile.Click += new EventHandler(async (sender, e) => await buttonReadAssemblyReportFile_ClickAsync(sender, e));
            splitContainerMain.Panel1.Controls.Add(buttonReadAssemblyReportFile);
            //add a new button to the form that triggers and event to read the Assembly report file (this file can be used to lookup the chromosome to each accession number --> or that it is on a different molecule)
            Button buttonReadGff3File = new Button();

            //setup progress bar (to the right side of the combo box)
            progressBar.Location = new Point(225, 73);
            progressBar.Size = new Size(545, 15);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Step = 1;
            progressBar.Value = 0;
            progressBar.Visible = false;
            progressBar.ForeColor = Color.Red;
            progressBar.Enabled = false;
            //add the progress bar to the form
            splitContainerMain.Panel1.Controls.Add(progressBar);

            //add a label that is placed to the left side of the combo box (that states "views")
            Label labelViews = new Label();
            //set name of label
            labelViews.Name = "labelViews";
            //set text of label
            labelViews.Text = "Views";
            //set location of label
            labelViews.Location = new Point(13, 75);
            //set size of label
            labelViews.Size = new Size(50, 50);
            //add label to split container 1
            splitContainerMain.Panel1.Controls.Add(labelViews);

            //add the combo box from the handler in the form just below the first button (import GTF file) and set the event handler for the combo box (this.comboBoxConditionalFormatExperimentView )
            splitContainerMain.Panel1.Controls.Add(_handlerImportedGtfFileData.comboBoxConditionalFormatExperimentView);

            //add button that executes the untangle procedure for GTF data into the datamodel assembly source (this is a different way than going to gene lists and more attuned to the form of the genome and its underlying structures / regulations)
            Button buttonUntangleGtfData = new Button();
            //set name of button BUTTON_PROCESS_GTF
            buttonUntangleGtfData.Name = BUTTON_UNTANGLE_GTF;
            //set text of button
            buttonUntangleGtfData.Text = "Untangle GTF data";
            //set location of button
            buttonUntangleGtfData.Location = new Point(300, 10);
            //set size of button
            buttonUntangleGtfData.Size = new Size(150, 50);
            //add event handler
            buttonUntangleGtfData.Click += new EventHandler(buttonUntangleGtfData_Click);
            //add button to split container 1
            splitContainerMain.Panel1.Controls.Add(buttonUntangleGtfData);

            //button that Creates a unique list of all transcripts
            Button buttonCreateUniqueListTranscripts = new Button();
            //set name of button BUTTON_PROCESS_GTF
            buttonCreateUniqueListTranscripts.Name = "buttonCreateUniqueListTranscripts";
            //set text of button
            buttonCreateUniqueListTranscripts.Text = "Create unique list of transcripts";
            //set location of button
            buttonCreateUniqueListTranscripts.Location = new Point(460, 10);
            //set size of button
            buttonCreateUniqueListTranscripts.Size = new Size(150, 50);
            //add event handler
            buttonCreateUniqueListTranscripts.Click += new EventHandler(buttonCreateUniqueListTranscripts_Click);
            //add button to split container 1
            splitContainerMain.Panel1.Controls.Add(buttonCreateUniqueListTranscripts);

            //button that places all entree in the GftFeature list that are stop_codon, end_codon, exon, and CDS is the right object (button name BUTTON_PROCESS_TRANSCRIPT_ELEMENTS)
            Button buttonProcessTranscriptElements = new Button();
            //set name of button BUTTON_PROCESS_GTF
            buttonProcessTranscriptElements.Name = BUTTON_PROCESS_TRANSCRIPT_ELEMENTS;
            //set text of button
            buttonProcessTranscriptElements.Text = "Process Exon/CDS";
            //set location of button
            buttonProcessTranscriptElements.Location = new Point(620, 10);
            //set size of button
            buttonProcessTranscriptElements.Size = new Size(150, 50);
            //add event handler
            buttonProcessTranscriptElements.Click += new EventHandler(buttonProcessTranscriptElements_Click);
            //add button to split container 1
            splitContainerMain.Panel1.Controls.Add(buttonProcessTranscriptElements);

            //button that save the imported data model to ParseDataModelAssemblySourceToCOM
            Button buttonSaveDataModelAssemblySourceToCOM = new Button();
            //set name of button BUTTON_PROCESS_GTF
            buttonSaveDataModelAssemblySourceToCOM.Name = "buttonSaveDataModelAssemblySourceToCOM";
            //set text of button
            buttonSaveDataModelAssemblySourceToCOM.Text = "Save data model to COM";
            //set location of button
            buttonSaveDataModelAssemblySourceToCOM.Location = new Point(780, 10);
            //set size of button
            buttonSaveDataModelAssemblySourceToCOM.Size = new Size(150, 50);
            //add event handler
            buttonSaveDataModelAssemblySourceToCOM.Click += ButtonSaveDataModelAssemblySourceToCOM_Click;
            //add button to split container 1
            splitContainerMain.Panel1.Controls.Add(buttonSaveDataModelAssemblySourceToCOM);

            //add the text box to the form just below the combo box
            _handlerImportedGtfFileData.comboBoxConditionalFormatExperimentView.SelectedIndexChanged += new EventHandler(ComboBoxViewDataGridImportedDataGtfFile_SelectedIndexChanged);


            //add on close event
            this.FormClosing += new FormClosingEventHandler(FormMain_FormClosing);
        }

        #endregion


        #region events


        /// <summary>
        /// on close event that asks the user to delete the unzipped XML file that was created (note that a zipped version is present). Add a title and icon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void FormMain_FormClosing(object? sender, FormClosingEventArgs e)
        {

            //check if the file exists
            if (File.Exists(_filePathSaveXml))
            {
                //ask the user if he wants to delete the file
                DialogResult dialogResult = MessageBox.Show("Do you want to delete the unzipped XML file that was created?", "Delete unzipped XML file", MessageBoxButtons.YesNo);
                //check if the user wants to delete the file
                if (dialogResult == DialogResult.Yes)
                {
                    //delete the file
                    File.Delete(_filePathSaveXml);
                }
            }

        }

        /// <summary>
        /// events that triggers the parse the imported data model to a COM object (using ParseDataModelAssemblySourceToCOM)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonSaveDataModelAssemblySourceToCOM_Click(object? sender, EventArgs e)
        {

            //var to produce debug report (bool)
            bool printDebug = false; //--> for troubleshooting or watching the progress in the debug window with interesting information

            //check if we have a data model in the GTF file handler, if not return appropiate message
            if (_handlerImportedGtfFileData.DataModelGtfFile == null)
            {
                MessageBox.Show("No GTF file imported yet, please import a GTF file first");
                return;
            }

            //create a save file dialog that will determine the location where the COM data model will be serialized
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //COM file will be an XML file
            saveFileDialog.Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.Title =
                "Save COM data model file";
            //check if the user selected a file
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                //make the progress bar visible
                progressBar.Visible = true;

                //setup a progress bar
                var progress = new Progress<int>();

                //set the progress bar to the text box
                progress.ProgressChanged += (s, message) =>
                {
                    //update the progress bar
                    progressBar.Value = message;
                };

                //get the file path
                string filePath = saveFileDialog.FileName;

                //use ComProcedures to parse the data model to a COM object
                var SophiasGenome = await ComProcedures.ParseDataModelAssemblySourceToCOMAsync(_handlerImportedGtfFileData.DataModelAssemblySourceList.ListOfAssemblySources, _handlerImportedGtfFileData.DataModelGtfAssemblyReport, _handlerImportedGtfFileData.ListOfUsedSourceFiles, progress, printDebug);

                //serialize SophiasGenome
                this._filePathSaveXml = await ComProcedures.SerializeSophiasGenomeAsync(SophiasGenome, filePath);

                //gzip the file
                ComProcedures.ZipFile(filePath);

                //make the progress bar invisible
                progressBar.Visible = false;

                //return a message to the user that the genome has been serialized and zipped and what the location of the file is. Ask the user if they want to open the folder where the file is located.
                DialogResult dialogResult = MessageBox.Show("SophiasGenome has been serialized and zipped to " + this._filePathSaveXml + ". Do you want to open the folder where the file is located?", "SophiasGenome has been serialized and zipped", MessageBoxButtons.YesNo);

                //check if the user wants to open the folder
                if (dialogResult == DialogResult.Yes)
                {
                    //open the folder
                    System.Diagnostics.Process.Start("explorer.exe", "/select, " + this._filePathSaveXml);
                }

            }


        }


        /// <summary>
        /// triggers event that places all entree in the GftFeature list that are stop_codon, end_codon, exon, and CDS is the right object (button name BUTTON_PROCESS_TRANSCRIPT_ELEMENTS)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void buttonProcessTranscriptElements_Click(object? sender, EventArgs e)
        {
            //check if we have a data model in the GTF file handler, if not return appropiate message
            if (_handlerImportedGtfFileData.DataModelGtfFile == null)
            {
                MessageBox.Show("No GTF file imported yet, please import a GTF file first");
                return;
            }

            //check if we have sources in the handler, if not return appropiate message
            if (_handlerImportedGtfFileData.ViewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes.Count() == 0)
            {
                MessageBox.Show("No sources found, please untangle the GTF data first");
                return;
            }

            //check if we have a list in ProcessAssemblySourcesToTotalGeneTranscriptListDictionary, if not response to user with appropiate message
            if (_handlerImportedGtfFileData.ViewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes.Count() == 0)
            {
                MessageBox.Show("No sources found, please process the transcripts first");
                return;
            }

            //make the progress bar visible
            progressBar.Visible = true;

            //setup a progress bar
            var progress = new Progress<int>();

            //set the progress bar to the text box
            progress.ProgressChanged += (s, message) =>
            {
                //update the progress bar
                progressBar.Value = message;
            };


            //trigger ProcessAssemblySourcesToTotalGeneTranscriptListDictionary in the handler
            var ProcessedItems = await _handlerImportedGtfFileData.ProcessNcbiDataToAssemblyBySourceTranscriptElementsExonCDSAsync(this._printDebug, progress);


            //make the progress bar invisible
            progressBar.Visible = false;

            //get the split container 1 and add the grid view to it (panel 2)
            var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);
            //clear the split container 1
            splitContainer1.Panel2.Controls.Clear();
            //add the grid view to the split container 1
            splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscriptsElementsExome);

            //return a message that the procedure is finished and give the total of unique transcripts
            MessageBox.Show("Procedure finished, found " + ProcessedItems + " items");


        }

        /// <summary>
        /// event handler that creates a unique list of all transcripts
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void buttonCreateUniqueListTranscripts_Click(object? sender, EventArgs e)
        {
            //check if we have a data model in the GTF file handler, if not return appropiate message
            if (_handlerImportedGtfFileData.DataModelGtfFile == null)
            {
                MessageBox.Show("No GTF file imported yet, please import a GTF file first");
                return;
            }

            //check if we have sources in the handler, if not return appropiate message
            if (_handlerImportedGtfFileData.ViewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes.Count() == 0)
            {
                MessageBox.Show("No sources found, please untangle the GTF data first");
                return;
            }

            //make the progress bar visible
            progressBar.Visible = true;

            //setup a progress bar
            var progress = new Progress<int>();

            //set the progress bar to the text box
            progress.ProgressChanged += (s, message) =>
            {
                //update the progress bar
                progressBar.Value = message;
            };

            //trigger CreateUniqueListTranscripts in the handler
            await _handlerImportedGtfFileData.ProcessDataModelAssemblySourceListToViewModelDataGeneTranscriptsAsync(progress);

            //set the data source for the grid
            this._handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscripts.CreateDataGrid(this._handlerImportedGtfFileData.ViewModelDataGeneTranscriptsList);

            //make the progress bar invisible
            progressBar.Visible = false;

            //get the split container 1 and add the grid view to it (panel 2)
            var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);
            //clear the split container 1
            splitContainer1.Panel2.Controls.Clear();
            //add the grid view to the split container 1
            splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscripts);

            //return a message that the procedure is finished and give the total of unique transcripts
            MessageBox.Show("Procedure finished, found " + _handlerImportedGtfFileData.ViewModelDataGeneTranscriptsList.ListViewModelDataGeneTranscriptsList.Count() + " unique transcripts");

        }

        /// <summary>
        /// event that triggers the untangle procedure for GTF data into the datamodel assembly source (this is a different way than going to gene lists and more attuned to the form of the genome and its underlying structures / regulations)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void buttonUntangleGtfData_Click(object? sender, EventArgs e)
        {
            //check if we have a data model in the GTF file handler, if not return appropiate message
            if (_handlerImportedGtfFileData.DataModelGtfFile == null)
            {
                MessageBox.Show("No GTF file imported yet, please import a GTF file first");
                return;
            }

            //make the progress bar visible
            progressBar.Visible = true;

            //setup a progress bar
            var progress = new Progress<int>();

            //set the progress bar to the text box
            progress.ProgressChanged += (s, message) =>
            {
                //update the progress bar
                progressBar.Value = message;
            };


            //trigger ProcessNcbiDataToAssemblyBySource in the handler
            await _handlerImportedGtfFileData.ProcessNcbiDataToAssemblyBySourceAsync(_printDebug, progress);

            //set the data source for the grid
            this._handlerImportedGtfFileData.ViewDataGridDataModelAssemblySourceGenesUniqueGeneId.DataSource = this._handlerImportedGtfFileData.ViewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes;

            //make the progress bar invisible
            progressBar.Visible = false;

            //get the split container 1 and add the grid view to it (panel 2)
            var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);
            //clear the split container 1
            splitContainer1.Panel2.Controls.Clear();
            //add the grid view to the split container 1
            splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemblySourceGenesUniqueGeneId);

            //throw a message box that the untangle procedure is finished and report the number of genes found with a unique gene id
            MessageBox.Show("Untangle procedure finished, found " + _handlerImportedGtfFileData.ViewModelDataAssemblySources.ListViewModelDataAssemblySourceGenes.Count() + " genes with a unique gene id");

        }

        /// <summary>
        /// event hanlder that switched between the different views of the imported GTF data (1. DataModelGtfFile, 2. DataAssemblyReportComments, 3. DataAssemblyReport, 4. DataModelLookupGeneList)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void ComboBoxViewDataGridImportedDataGtfFile_SelectedIndexChanged(object? sender, EventArgs e)
        {

            //get the selected item
            var selectedItem = _handlerImportedGtfFileData.comboBoxConditionalFormatExperimentView.SelectedItem;

            //check if the selected item is not null
            if (selectedItem != null)
            {
                //parse selected item to combobox=
                var ComboxBox = ((ComboBox)sender);
                //parse item to enum
                var SelectedEnum = (ViewModelParameters.EnumViewDataGridImportedDataGtfFile)Enum.Parse(typeof(ViewModelParameters.EnumViewDataGridImportedDataGtfFile), ComboxBox.SelectedValue.ToString());

                //get the split container 1 and add the grid view to it (panel 2)
                var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);

                //clear the split container 1
                splitContainer1.Panel2.Controls.Clear();

                //switch between the different views
                switch (SelectedEnum)
                {
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelGtfFile:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataAssemblyReportComments:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridAssemblyReportComments);
                        //adjust column width (note JCO: we have to do this after the load)
                        _handlerImportedGtfFileData.ViewDataGridAssemblyReportComments.AdjustColumnWidth(150);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataAssemblyReport:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridAssemblyReportList);
                        //adjust column width (note JCO: we have to do this after the load)
                        _handlerImportedGtfFileData.ViewDataGridAssemblyReportList.AdjustColumnWidth(120);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelGeneList:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemblySourceGenesUniqueGeneId);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelTranscriptList:

                        //NOTE JCO -- > if we want to have the overview for transcript then the transcript processing has to be redone after untangling the exons and CDS
                        //              Or only the total need to be examined
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscripts);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelExonList:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscriptsElementsExome);
                        break;
                    case ViewModelParameters.EnumViewDataGridImportedDataGtfFile.DataModelCdsList:
                        //add the grid view to the split container 1
                        splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridDataModelAssemySourceGeneTranscripsElementsCds);
                        break;
                    default:
                        throw new NotImplementedException();
                }
      
            }


        }

        /// <summary>
        /// triggers event that reads the assembly report file into a DataModelAssemblyReport
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async Task buttonReadAssemblyReportFile_ClickAsync(object? sender, EventArgs e)
        {

            //new open file dialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            //check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //get the file path
                string filePath = openFileDialog.FileName;

                //new reader for the assembly report
                var AssemblyReportReader = new NcbiGftAssemblyReportReader();

                //read the file using ImportDataFromFileAsync. wait for the result
                var assemblyReport = await AssemblyReportReader.ImportDataFromFileAsync(filePath);

                //process the assembly report
                _handlerImportedGtfFileData.ProcessDataModelAssemblyReport(assemblyReport);

                //extract file name from path
                string fileName = Path.GetFileName(filePath);
                //add the name of the file in the list source files of the handler
                _handlerImportedGtfFileData.ListOfUsedSourceFiles.Add(fileName);

                //get the split container 1 and add the grid view to it (panel 2)
                var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);
                splitContainer1.Panel2.Controls.Clear();
                splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridAssemblyReportList);



            }

        }

        /// <summary>
        /// event handler for the button that imports a GFF3 file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private async void buttonImportGTF3_Click(object? sender, EventArgs e)
        {

            //open a file dialog to select a GFF3 file
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "GTF files (*.gtf)|*.gtf|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            //check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {


                //get the file path
                string filePath = openFileDialog.FileName;

                //make the progress bar visible
                progressBar.Visible = true;

                // Note JCO --> implementation example of a progress event with a async await procedure wihtin the caller
                //setup a progress bar
                var progress = new Progress<int>();
                //set the progress bar to the text box
                progress.ProgressChanged += (s, message) =>
                {
                    //update the progress bar
                    progressBar.Value = message;
                };

                //new GTF reader (pasing progress as a parameter)
                var GTFReader = new GTFReader();

                // Note JCO --> the method below placed the reader on a separate thread keeping the UI responsive (alternaively we can use the async await procedure placed in the reader itself
                //read the GTF file with the async gtf reader
                DataModelGtfFile GtfFile = await GTFReader.ReadGFF3Async(filePath, progress);

                //clear the GTFReader
                GTFReader = null;

                // check if the GTF file is not null, then load the GTF data model in the handler dataview
                if (GtfFile != null)
                {

                    //make the progress bar invisible
                    progressBar.Visible = false;

                    //extract file name from path
                    string fileName = Path.GetFileName(filePath);
                    //add the name of the file in the list source files of the handler
                    _handlerImportedGtfFileData.ListOfUsedSourceFiles.Add(fileName);

                    //set the GTF file in the handler
                    _handlerImportedGtfFileData.DataModelGtfFile = GtfFile;

                    //set the data source for the grid
                    _handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile.DataSource = GtfFile.FeaturesList;

                    //adjust column width
                    _handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile.AdjustColumnWidth(this._handlerImportedGtfFileData._columnWidth);

                    //get the split container 1 and add the grid view to it (panel 2)
                    var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);

                    //clear the split container 1
                    splitContainer1.Panel2.Controls.Clear();

                    //add the grid view to the split container 1
                    splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile);

                }

                //report to the user a message that the GTF file is imported, and state the number of items
                MessageBox.Show("GTF file imported, found " + GtfFile.FeaturesList.Count() + " items");
            }


        }

        #endregion


        #region methods

        /// <summary>
        /// return a split container of this form by searching its name (note that structure are based here on logic by their placement in the constructor; not hard force)
        /// </summary>
        /// <param name="nameSplitContainer"></param>
        /// <returns></returns>
        private SplitContainer ReturnSplitContainerByName(string nameSplitContainer)
        {

            //get splitcontainer 1
            var splitContainer1 = this.Controls.Find(SPLIT_CONTAINER_1, true).FirstOrDefault() as SplitContainer;

            //return the request split contaier
            if (nameSplitContainer == splitContainer1.Name) return splitContainer1;

            //return null if we didnt find the requested splitcontainerq
            return null;

        }

        #endregion



    }
}
