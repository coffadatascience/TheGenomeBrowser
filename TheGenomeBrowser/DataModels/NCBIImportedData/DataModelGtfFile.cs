using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.NCBIImportedData
{
    /// <summary>
    /// class that hold the information of a GFF3 file (contains a list of GFF3Features)
    /// </summary>
    public class DataModelGtfFile
    {

        #region fields

        //list of GFF3Features
        public List<GTFFeature> FeaturesList { get; set; }


        #endregion


        #region properties

        //constructor
        public DataModelGtfFile()
        {
            FeaturesList = new List<GTFFeature>();
        }

        #endregion


        #region methods




        #endregion

    }

    /// <summary>
    /// class to read GTF files
    /// </summary>
    public class GTFFeature
    {

        #region fields

        /// <summary>
        /// var for seqname --> matches with the Field Relationship in the assembly report
        /// </summary>
        public string Seqname { get; set; }

        /// <summary>
        /// var for source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// var for feature type
        /// </summary>
        public string FeatureType { get; set; }

        /// <summary>
        /// var for start and end
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for score
        /// </summary>
        public string Score { get; set; }

        /// <summary>
        /// var for strand
        /// </summary>
        public string Strand { get; set; }

        /// <summary>
        /// var for frame
        /// </summary>
        public string Frame { get; set; }

        /// <summary>
        /// var for gene id
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// var for exon number
        /// </summary>
        public string ExonNumber { get; set; }


        /// <summary>
        /// var for transcript id
        /// </summary>
        public string TranscriptId { get; set; }

        /// <summary>
        /// var for db ref
        /// </summary>
        public string DbXref { get; set; }

        /// <summary>
        /// var for gb key
        /// </summary>
        public string GbKey { get; internal set; }

        /// <summary>
        /// var for gene name
        /// </summary>
        public string Gene { get; set; }

        /// <summary>
        /// var for product
        /// </summary>
        public string Product { get; internal set; }

        /// <summary>
        /// var for gene biotype
        /// </summary>
        public string Pseudo { get; internal set; }

        /// <summary>
        /// var for transcript biotype
        /// </summary>
        public string TranscriptBiotype { get; internal set; }


        /// <summary>
        /// attributes in a string format
        /// </summary>
        public string AttributesString { get; set; }


        // -- example of a GTF file line --
        // --> we want to parse the attributes into a separate vars (gene_id, transcript_id, exon_number, exon_id, gene_name)
        //chr1 wgEncodeGencodeBasicV26 exon	11189341	11189955	.	+	.	gene_id "ANGPTL7"; transcript_id "ENST00000376819.3"; exon_number "1"; exon_id "ENST00000376819.3.1"; gene_name "ANGPTL7";


        #endregion


        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        public GTFFeature()
        {
        }

        #endregion

        #region methods

        /// <summary>
        /// procedure that extract the chromosome from the seqname field (a seqname may be NC_000001.11, where the chromosome is 1, or NC_000006.12, where the chromosome is 6)
        /// </summary>
        /// <returns></returns>
        public string ExtractChromosomeFromSeqname()
        {
            //split the seqname on the dot
            string[] splitSeqname = Seqname.Split('.');

            //get the first element
            string chromosome = splitSeqname[0];

            //remove all non numeric characters
            chromosome = new string(chromosome.Where(c => char.IsDigit(c)).ToArray());

            //remove all leading zeros
            chromosome = chromosome.TrimStart('0');

            //return the chromosome
            return chromosome;
        }

        #endregion

    }



}
