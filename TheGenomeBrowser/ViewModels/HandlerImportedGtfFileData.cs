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


        #region DataModels 

        /// <summary>
        /// data model for imported GTF file
        /// </summary>
        public DataModels.DataModelGtfFile DataModelGtfFile { get; set; }

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

        #endregion

        #region Views

        /// <summary>
        /// var for view data grid imported data GTF file
        /// </summary>
        public ViewDataGridImportedDataGtfFile ViewDataGridImportedDataGtfFile { get; set; }

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
            _columnWidth = 50;

            //create data model
            DataModelGtfFile = new DataModels.DataModelGtfFile();

            //create view model
            ViewModelGtfFile = new ViewModelGtfFile();

            //create view data grid imported data GTF file
            ViewDataGridImportedDataGtfFile = new ViewDataGridImportedDataGtfFile("ViewDataGridImportedDataGtfFile");

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


            //loop all features in the GTF file
            foreach (DataModels.GTFFeature feature in DataModelGtfFile.FeaturesList)
            {

                //get the gene id
                string geneId = feature.GeneId;

                //get the chromosome
                string chromosome = feature.ExtractChromosomeFromSeqname();

                //check if the gene id is not null or empty
                if (!string.IsNullOrEmpty(geneId))
                {

                    //check if the gene id is not yet in the dictionary
                    if (!dictionaryGeneNameGeneObject.ContainsKey(geneId))
                    {

                        //get location start
                        int locationStart = feature.Start;
                        int locationEnd = feature.End;

                        //create a new data model lookup gene object
                        DataModels.Genes.DataModelLookupGene dataModelLookupGene = new DataModels.Genes.DataModelLookupGene();

                        //set the gene name, chromosome and start and end location
                        dataModelLookupGene.DataModelLookupGeneSetupNewItem(geneId, chromosome, locationStart, locationEnd);

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






    }

}
