using System.Windows.Forms;
using TheGenomeBrowser.Readers;
using TheGenomeBrowser.ViewModels;

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
        /// constant for name of data grid view of GTF datamodel
        /// </summary>
        private const string DATA_GRID_VIEW_GTF_DATA_MODEL = "ViewDataGridImportedDataGtfFile";

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
            this.Size = new Size(1200, 1000);

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

            //test button that retrieves the data from the database using the gene name and exon number
            Button buttonTest = new Button();
            buttonTest.Text = "Test";
            //set location of button
            buttonTest.Location = new Point(120, 10);
            //set size of button
            buttonTest.Size = new Size(100, 50);
            //add event handler
            buttonTest.Click += new EventHandler(buttonTest_Click);
            //add button to split container 1
            splitContainerMain.Panel1.Controls.Add(buttonTest);
        }



        #endregion

        #region events

        /// <summary>
        /// event handler for the button that tests the database connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonTest_Click(object? sender, EventArgs e)
        {



        }


        /// <summary>
        /// event handler for the button that imports a GFF3 file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void buttonImportGTF3_Click(object? sender, EventArgs e)
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

                //read GTF file
                var GtfFile = GTFReader.ReadGFF3(filePath);

                // check if the GTF file is not null, then load the GTF data model in the handler dataview
                if (GtfFile != null)
                {

                    //set the data source for the grid
                    _handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile.DataSource = GtfFile.Features;

                    //adjust column width
                    _handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile.AdjustColumnWidth(this._handlerImportedGtfFileData._columnWidth);

                    //get the split container 1 and add the grid view to it (panel 2)
                    var splitContainer1 = ReturnSplitContainerByName(SPLIT_CONTAINER_1);

                    //clear the split container 1
                    splitContainer1.Panel2.Controls.Clear();

                    //add the grid view to the split container 1
                    splitContainer1.Panel2.Controls.Add(_handlerImportedGtfFileData.ViewDataGridImportedDataGtfFile);


                }

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
            if (nameSplitContainer == nameSplitContainer) return splitContainer1;

            //return null if we didnt find the requested splitcontainerq
            return null;

        }

        #endregion
    }
}
