using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// public class that holds the list of transcripts for a given gene
    /// </summary>
    public class DataModelGeneTranscript
    {

        #region properties

        // A transcript may have multiple fields that are important for us. We will store these in a list of GeneTranscriptElements
        // example line: NC_000001.11	BestRefSeq	transcript	11874	14409	.	+	.	gene_id "DDX11L1"; transcript_id "NR_046018.2"; db_xref "GeneID:100287102"; gbkey "misc_RNA"; gene "DDX11L1"; product "DEAD/H-box helicase 11 like 1 (pseudogene)"; pseudo "true"; transcript_biotype "transcript"; 
        // we may notice that many of the fields are the same as to the gene it originates from. We will store these in the GeneId class. However, start, end, transcipt id, biotype etc are different and we will store these in the GeneTranscriptElement class
        // Due to their difference we will make a special class for these elements

        /// <summary>
        /// var for seqname (typically the accession number of the chromosome or scaffold)
        /// </summary>
        public string SeqName { get; set; }

        /// <summary>
        /// var for start location (note as noted for the line "gene" in the annotation file. Note that we may have list of exon and CDS that have alternative start locations. Though we assume the the first exon has the start location)
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end location 
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var to score
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
        /// var for gene id (field of GeneId) (note that although this should be the same as the higher level gene id, we will store it here as well -- > since its placed in the file and we still investigate the links)
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// var for Xref, for transcript this is the transcript id, there is only one id per transcript
        /// </summary>
        public string Db_Xref { get; set; }

        /// <summary>
        /// var for gb_key (value for filed Gb_Key)
        /// </summary>
        public string Gb_Key { get; set; }

        /// <summary>
        /// denotation from the file under "gene", typically expected to be equal to gene ID
        /// </summary>
        public string GeneName { get; set; }

        /// <summary>
        /// var for Pseudo (true or false)
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// var for product  ---> note that gene does not have this feature
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// var for transcript biotype (note that gene does not have this feature)
        /// </summary>
        public string Transcript_Biotype { get; set; }


        /// <summary>
        /// list of GeneTranscriptsElements
        /// </summary>
        public List<DataModelGeneTranscriptElement> ListOfGeneTranscriptElements { get; set; }

        #endregion


        #region constructors

        public DataModelGeneTranscript() 
        {
            //init the list
            ListOfGeneTranscriptElements = new List<DataModelGeneTranscriptElement>();
        
        }

        #endregion


        #region methods




        #endregion


    }


    /// <summary>
    /// Struct class that contains the transcript id and the chromosome. We use this class to speed up the process of finding the gene related to a transcript
    /// so we may store the chromosome with the transcript ids, allowing fast tracking via the chromosome
    /// </summary>
    public struct TranscriptInfo
    {

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="transcriptId"></param>
        /// <param name="moleculeChromosome"></param>
        public TranscriptInfo(string transcriptId, string moleculeChromosome) : this()
        {
            this.TranscriptId = transcriptId;
            this.moleculeChromosome = moleculeChromosome;
        }

        /// <summary>
        /// The transcript id.
        /// </summary>
        public string TranscriptId { get; set; }

        /// <summary>
        /// The chromosome.
        /// </summary>
        public string moleculeChromosome { get; set; }


    }




}
