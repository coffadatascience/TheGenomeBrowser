using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class that hold the information for a seqid (gene id)
    /// a seq id is a unique identifier for usually a gene that may have its own accessions and other information
    /// a seq id may have a list of entities (exons, introns, etc) that are located on the seq id
    /// </summary>
    public class DataModelGeneId
    {

        #region properties

        /// <summary>
        /// var for gene id (field of GeneId)
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// denotation from the file under "gene", typically expected to be equal to gene ID
        /// </summary>
        public string GeneName { get; set; }

        /// <summary>
        /// var for gene description (field of GeneDescription)
        /// </summary>
        public string GeneDescription { get; set; }

        /// <summary>
        /// var for strand
        /// </summary>
        public string Strand { get; set; }

        /// <summary>
        /// var for end location 
        /// </summary>
        public int EndLocation { get; set; }


        /// <summary>
        /// var for start location (note as noted for the line "gene" in the annotation file. Note that we may have list of exon and CDS that have alternative start locations. Though we assume the the first exon has the start location)
        /// </summary>
        public int StartLocation { get; set; }


        /// <summary>
        /// var for Db_Xref_One. Hold the Db ref on the first position (note that on gene level we may have multiple Db refs)
        /// </summary>
        public string Db_Xref_One { get; set; }

        /// <summary>
        /// var for Db_Xref_Two. Hold the Db ref on the second position
        /// </summary>
        public string Db_Xref_Two { get; set; }

        /// <summary>
        /// var for gb_key (value for filed Gb_Key)
        /// </summary>
        public string Gb_Key { get; set; }

        /// <summary>
        /// var for Gene_Biotype (typically denoted as gene_biotype in the annotation file, what the investigator is looking for, mRNA or protein coding)
        /// </summary>
        public string Gene_Biotype { get; set; }

        /// <summary>
        /// var for Gene_Synonym (alternative gene names)
        /// </summary>
        public string Gene_Synonym { get; set; }


        /// <summary>
        /// list of entities (exons, introns, etc) that are located on the seq id
        /// </summary>
        public List<DataModelGeneElement> ListOfGeneElements { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor taking all variable values
        /// </summary>
        /// <param name="geneId"></param>
        /// <param name="geneName"></param>
        /// <param name="geneDescription"></param>
        /// <param name="strand"></param>
        /// <param name="startLocation"></param>
        /// <param name="db_Xref_One"></param>
        /// <param name="db_Xref_Two"></param>
        /// <param name="gb_Key"></param>
        /// <param name="gene_Biotype"></param>
        /// <param name="gene_Synonym"></param>
        public DataModelGeneId(string geneId, string geneName, string geneDescription, string strand, int startLocation, int endLocation, string db_Xref_One, string db_Xref_Two, string gb_Key, string gene_Biotype, string gene_Synonym)
        {
            //set the gene id
            GeneId = geneId;

            //set the gene name
            GeneName = geneName;

            //set the gene description
            GeneDescription = geneDescription;

            //set the strand
            Strand = strand;

            //set the start location
            StartLocation = startLocation;

            //set the end location
            EndLocation = endLocation;

            //set the list of gene elements
            ListOfGeneElements = new List<DataModelGeneElement>();

            //set the db xref one
            Db_Xref_One = db_Xref_One;

            //set the db xref two
            Db_Xref_Two = db_Xref_Two;

            //set the gb key
            Gb_Key = gb_Key;

            //set the gene biotype
            Gene_Biotype = gene_Biotype;

            //set the gene synonym
            Gene_Synonym = gene_Synonym;
        }


        #endregion


        #region methods


        #endregion


    }


}
