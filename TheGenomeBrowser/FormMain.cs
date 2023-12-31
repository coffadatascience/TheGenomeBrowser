using TheGenomeBrowser.Readers;

namespace TheGenomeBrowser
{
    /// <summary>
    /// general form
    /// </summary>
    public partial class FormMain : Form
    {
        #region fields

        #endregion

        #region properties

        //constant that holds the name of the GFF3 button
        private const string BUTTON_IMPORT_GTF = "Import GTF file";

        #endregion

        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public FormMain()
        {
            InitializeComponent();

            //add a new button to the form that states import GTF3 file
            Button buttonImportGFF3 = new Button();
            //set name of button
            buttonImportGFF3.Name = BUTTON_IMPORT_GTF;
            buttonImportGFF3.Text = "Import GTF file";
            buttonImportGFF3.Location = new Point(10, 10);
            buttonImportGFF3.Click += new EventHandler(buttonImportGTF3_Click);
            this.Controls.Add(buttonImportGFF3);

        }



        #endregion

        #region events

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
            openFileDialog.Filter = "GFF3 files (*.gff3)|*.gff3|All files (*.*)|*.*";
            openFileDialog.RestoreDirectory = true;

            //check if the user selected a file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                //use the NcbiGftReaderToVersion2 class to read the selected GFF3 file
                NcbiGftReaderToVersion2 reader = new NcbiGftReaderToVersion2();

                //read file
                reader.ReadGftFile(openFileDialog.FileName);

                //check data
            }


        }

        #endregion

    }
}
