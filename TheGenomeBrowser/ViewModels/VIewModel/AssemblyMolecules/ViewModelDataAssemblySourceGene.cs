using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules
{

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
        /// var that return the number of items in the list
        /// </summary>
        public int NumberOfItemsInList
        {
            get
            {
                return ListOfDataModelGeneId.Count;
            }
        }

        /// <summary>
        /// var that returns the source type of the first item in the list
        /// </summary>
        public string SourceType
        {
            get
            {
                return ListOfDataModelGeneId[0].SourceType;
            }
        }

        /// <summary>
        /// var that returns the gene name of the first item in the list
        /// </summary>
        public string GeneName
        {
            get
            {
                return ListOfDataModelGeneId[0].GeneName;
            }
        }

        /// <summary>
        /// var that returns the description of the first item in the list
        /// </summary>
        public string Description
        {
            get
            {
                return ListOfDataModelGeneId[0].Description;
            }
        }

        /// <summary>
        /// var that returns the DbRefXrefOne of the first item in the list
        /// </summary>
        public string DbRefXrefOne
        {
            get
            {
                return ListOfDataModelGeneId[0].DbRefXrefOne;
            }
        }

        /// <summary>
        /// var that returns the DbRefXrefTwo of the first item in the list
        /// </summary>
        public string DbRefXrefTwo
        {
            get
            {
                return ListOfDataModelGeneId[0].DbRefXrefTwo;
            }
        }

        /// <summary>
        /// var that returns the GeneBiotype of the first item in the list
        /// </summary>
        public string GeneBiotype
        {
            get
            {
                return ListOfDataModelGeneId[0].GeneBiotype;
            }
        }

        /// <summary>
        /// var that returns if the gene is a pseudo for the first item in the list
        /// </summary>
        public bool IsPseudo
        {
            get
            {
                return ListOfDataModelGeneId[0].GeneBiotype == "pseudogene";
            }
        }

        /// <summary>
        /// var that returns the synonyms of the first item in the list
        /// </summary>
        public string Synonyms
        {
            get
            {
                return ListOfDataModelGeneId[0].Synonyms;
            }
        }   

        /// <summary>
        /// var that source type of the second item in the list (if it exists, else return na)
        /// </summary>
        public string SourceTypeTwo
        {
            get
            {
                if (NumberOfItemsInList > 1)
                {
                    return ListOfDataModelGeneId[1].SourceType;
                }
                else
                {
                    return "na";
                }
            }
        }

        /// <summary>
        /// var that returns the gene name of the second item in the list (if it exists, else return na)
        /// </summary>
        public string GeneNameTwo
        {
            get
            {
                if (NumberOfItemsInList > 1)
                {
                    return ListOfDataModelGeneId[1].GeneName;
                }
                else
                {
                    return "na";
                }
            }
        }


        /// <summary>
        /// var with list of DataModelGeneId items
        /// </summary>
        // --> we want to sort such lists on gene id, so the same gene id is always in the same place and we may compare source information
        // --> Typically we would expect this list to be empty (as the commplete list is expected to be in the dictionary and unique), however we keep this as a list so we may view possible duplicates easily (second item in the list should be na)
        public List<ViewModelDataAssemblySourceGeneItem> ListOfDataModelGeneId { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor that takes a DataModelGeneId (geneId) and setup this class by adding the gene id and processing the current item as a ViewModelDataAssemblySourceGeneItem and add it to the list
        /// </summary>
        /// <param name="geneId"></param>
        public ViewModelDataAssemblySourceGene(DataModelGeneId geneId)
        {

            //init the list
            ListOfDataModelGeneId = new List<ViewModelDataAssemblySourceGeneItem>();

            //set the gene id
            GeneId = geneId.GeneId;

            //add new data model gene id to the list
            AddNewDataModelGeneIdToList(geneId);
 
        }

        #endregion


        #region methods


        /// <summary>
        /// Procedure that adds a DataModelGeneId item to the list
        /// </summary>
        /// <param name="geneId"></param>
        public void AddNewDataModelGeneIdToList(DataModelGeneId geneId)
        {

            //create a new ViewModelDataAssemblySourceGeneItem, setting the value from the GeneIdDatamodel and add it to the list
            ListOfDataModelGeneId.Add(new ViewModelDataAssemblySourceGeneItem
            {
                GeneId = geneId.GeneId,
                SourceType = geneId.Source,
                GeneName = geneId.GeneName,
                Start = geneId.Start,
                End = geneId.End,
                DbRefXrefOne = geneId.Db_Xref_One,
                DbRefXrefTwo = geneId.Db_Xref_Two,
                GeneBiotype = geneId.Gene_Biotype,
                Description = geneId.Description,
                NumberOfTranscripts = geneId.NumberOfTranscripts,
                NumberOfExons = geneId.NumberOfExons,
                //Synonyms = geneId.Synonyms list to string
                Synonyms = geneId.GeneSynonymsAsString
            }); ;

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
        /// start of the gene
        /// </summary>
        public int Start { get; set; }
        
        /// <summary>
        /// end of the gene
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for description
        /// </summary>
        public string Description { get; set; }

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

        /// <summary>
        /// var for synonyms as a string
        /// </summary>
        public string Synonyms { get; set; }

        /// <summary>
        /// number of transcripts
        /// </summary>
        public int NumberOfTranscripts { get; set; }

        /// <summary>
        /// number of exons
        /// </summary>
        public int NumberOfExons { get; set; }


        #endregion


        #region constructors


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
