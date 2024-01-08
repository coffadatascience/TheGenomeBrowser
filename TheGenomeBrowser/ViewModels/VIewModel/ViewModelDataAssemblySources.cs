using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.VIewModel
{

    /// <summary>
    /// This class is specific for the view model that holds the data for the assembly sources So data of different sources may produce a single gene list with all transcripts
    /// ----> This may give better insight in how to create a single gene list with all transcripts, and lower hiearchy levels
    /// </summary>
    public class ViewModelDataAssemblySources
    {


        #region properties


        /// <summary>
        /// var dictionary with GeneId as key and list of DataModelGeneAndAssemblySources items as value. We use dictionaries for sorting and then place them in a list for the View (so we may sort on gene id - > this is therefore only a complete unique list of all geneId entree in all sources used)
        /// </summary>
        public Dictionary<string, List<ViewModelDataAssemblySourceGene>> DictionaryViewModelDataAssemblySourceGenes { get; set; }

        /// <summary>
        /// list of ViewModelDataGeneAndAssemblySources items
        ///  -- > make total list of all items of the DataModelGeneId in all sources
        ///  -- > we want to sort such lists on gene id, so the same gene id is always in the same place and we may compare source information
        /// </summary>
        public List<ViewModelDataAssemblySourceGene> ListViewModelDataAssemblySourceGenes { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataAssemblySources()
        {
            //init the list
            ListViewModelDataAssemblySourceGenes = new List<ViewModelDataAssemblySourceGene>();

            //init the dictionary
            DictionaryViewModelDataAssemblySourceGenes = new Dictionary<string, List<ViewModelDataAssemblySourceGene>>();

        }

        #endregion


        #region methods


        /// <summary>
        /// procedure that processes the assembly sources into a single list with all genes (so we may compare the different sources). note that we can collect multiple items for one gene so we can print all transcripts.
        /// </summary>        public void ProcessAssemblySourcesToTotalGeneListDictionary(List<DataModelAssemblySource> assemblySources)
        {

            //clear the dictionary
            DictionaryViewModelDataAssemblySourceGenes = new Dictionary<string, List<ViewModelDataAssemblySourceGene>>();

            //loop the assembly sources
            foreach (var assemblySource in assemblySources)
            {

                //loop all molecules in the Genome
                foreach (var molecule in assemblySource.TheGenome.DictionaryOfMolecules.Values)
                {

                    //loop all gene ids in the molecule
                    foreach (var geneId in molecule.GeneIds.Values)
                    {

                        //check if the gene id is already in the dictionary
                        if (DictionaryViewModelDataAssemblySourceGenes.ContainsKey(geneId.GeneId))
                        {

                            //create a new ViewModelDataAssemblySourceGene item
                            var viewModelDataAssemblySourceGene = new ViewModelDataAssemblySourceGene(geneId);

                            //add the item to the dictionary
                            DictionaryViewModelDataAssemblySourceGenes[geneId.GeneId].Add(viewModelDataAssemblySourceGene);

                        }
                        else
                        {

                            //get the ViewModelDataAssemblySourceGene from the dictionary
                            var viewModelDataAssemblySourceGene = DictionaryViewModelDataAssemblySourceGenes[geneId.GeneId];

                            //add the gene id data model to the list
                            viewModelDataAssemblySourceGene.AddDataModelGeneId(geneId);


                        }


                    } // end foreach gene id

                }

            } // end foreach assembly source


        }


        #endregion


    }



    /// <summary>
    /// class that holds the data for a single gene, add all source data to this class that have the same gene id (so we may see the different items that are available for a single gene)
    /// </summary>
    public class ViewModelDataAssemblySourceGene
    {


        #region properties


        /// <summary>
        /// var for gene id (so is the same for sources in this viewmodel)
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// var with list of DataModelGeneId items
        /// </summary>
        // --> we want to sort such lists on gene id, so the same gene id is always in the same place and we may compare source information
        public List<ViewModelDataAssemblySourceGeneItem> ListOfDataModelGeneId { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataAssemblySourceGene()
        {
            //init the list
            ListOfDataModelGeneId = new List<ViewModelDataAssemblySourceGeneItem>();
        }

        /// <summary>
        /// constructor that takes a DataModelGeneId (geneId) and setup this class by adding the gene id and processing the current item as a ViewModelDataAssemblySourceGeneItem and add it to the list
        /// </summary>
        /// <param name="geneId"></param>
        public ViewModelDataAssemblySourceGene(DataModelGeneId geneId)
        {

            //set the gene id
            GeneId = geneId.GeneId;

            //create a new ViewModelDataAssemblySourceGeneItem, setting the value from the GeneIdDatamodel and add it to the list
            ListOfDataModelGeneId.Add(new ViewModelDataAssemblySourceGeneItem
            {
                GeneId = geneId.GeneId,
                SourceType = geneId.Source,
                GeneName = geneId.GeneName,
                DbRefXrefOne = geneId.Db_Xref_One,
                DbRefXrefTwo = geneId.Db_Xref_Two,
                GeneBiotype = geneId.Gene_Biotype
            });

        }

        #endregion


        #region methods


        /// <summary>
        /// Procedure that adds a DataModelGeneId item to the list
        /// </summary>
        /// <param name="geneId"></param>
        public void AddDataModelGeneId(DataModelGeneId geneId)
        {

            //create a new ViewModelDataAssemblySourceGeneItem, setting the value from the GeneIdDatamodel and add it to the list
            ListOfDataModelGeneId.Add(new ViewModelDataAssemblySourceGeneItem
            {
                GeneId = geneId.GeneId,
                SourceType = geneId.Source,
                GeneName = geneId.GeneName,
                DbRefXrefOne = geneId.Db_Xref_One,
                DbRefXrefTwo = geneId.Db_Xref_Two,
                GeneBiotype = geneId.Gene_Biotype
            });

        }



        #endregion


    }

    /// <summary>
    /// class that holds different data items for a single gene that was found in different sources
    /// we want to view this so we can select the best source to obtain the base information from (exon numbering, etc)
    /// --> we aim to use the BestRefSeq as basis, but transcripts may be in different sources. We want to see the different sources and the different transcripts
    /// --> we do this to have one final reference build from which we may create framing and plotting models.
    /// ---> Note that these item reflect the different sources, so we may see the different transcripts and exons
    /// </summary>
    public class ViewModelDataAssemblySourceGeneItem
    {


        #region properties

        /// <summary>
        /// The properties here are the one that are typically shared by the source. We are mostly interested in the fields that can be used as key So: GeneID, accession, DbXref, TranscriptId, Number of transcripts, number of exons, total number of entrees for transcript etc
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// source type (name of the source type entree the gene id was found into)
        /// </summary>
        public string SourceType { get; set; }

        /// <summary>
        /// var for gene name
        /// </summary>
        public string GeneName { get; set; }

        /// <summary>
        /// var for Db Xref id one
        /// </summary>
        public string DbRefXrefOne { get; set; }

        /// <summary>
        /// var for Db Xref id Two
        /// </summary>
        public string DbRefXrefTwo { get; set; }

        /// <summary>
        /// var for gene_biotype (this may show pseudo information -- > note that pseudo genes may appear under the same name as the original gene)
        /// </summary>
        public string GeneBiotype { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor that takes all fields as input
        /// </summary>
        /// <param name="geneId"></param>
        /// <param name="sourceType"></param>
        /// <param name="geneName"></param>
        /// <param name="dbRefXrefOne"></param>
        /// <param name="dbRefXrefTwo"></param>
        /// <param name="geneBiotype"></param>
        public ViewModelDataAssemblySourceGeneItem(string geneId, string sourceType, string geneName, string dbRefXrefOne, string dbRefXrefTwo, string geneBiotype)
        {
            //set the properties
            GeneId = geneId;
            SourceType = sourceType;
            GeneName = geneName;
            DbRefXrefOne = dbRefXrefOne;
            DbRefXrefTwo = dbRefXrefTwo;
            GeneBiotype = geneBiotype;
        }

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataAssemblySourceGeneItem()
        {


        }

        #endregion


        #region methods


        #endregion


    }

}
