using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{


    /// <summary>
    /// class representing the field for the start_codon
    /// </summary>
    public class DataModelGeneTranscriptElementCodon 
    {

        #region properties

        /// <summary>
        /// var with name (start_codon or stop_codon)
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// var int start_codon (start)
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for start_codon (end)
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// exon notation (often simply the first and the last number of the exon --> we add for link to lists in between)
        /// </summary>
        public int Exon { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor taking the all the fields as input
        /// Note that there is an exon notation (often simply the first and the last number of the exon --> we add for link to lists in between)
        /// </summary>
        /// <param name="startCodonStart"></param>
        /// <param name="startCodonEnd"></param>
        /// <param name="endCodonStart"></param>
        /// <param name="endCodonEnd"></param>
        public DataModelGeneTranscriptElementCodon(string name, int start, int end, int exon)
        {
            //set the fields 
            Name = name;
            Start = start;
            End = end;
            Exon = exon;
        }

        #endregion

        #region methods

        #endregion

    }

    /// <summary>
    /// class that represent a coding sequence item (CDS) - CDS is different from the exon list in that it contains the sequence of the protein coded by the gene (the exon list contains the location with the non-protein-coding sequence which is not placed in the transcript. The transcript beginning and end should thus be equal to that of the CDS first exon start and last exon end.)
    ///   ----> note that though not certain yet, it could be expected that there is only a single CDS list, as it relates to an unique transcript. The exon list however may contain multiple entries for a single transcript, as there may be multiple splicing variants
    /// </summary>
    public class DataModelGeneTranscriptElementCDS
    {

        #region properties

        /// <summary>
        /// var for  start
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for exon number (note this relates to the exon this cds is part of) Note that some splicing variants may make use of alternative splice sites and thus have different CDSs
        ///  ---> we may thus have multiple exon with the same number in the CDS list, but there should only be one in the exon list (this is also the list that is interesting for probe design, as it constitutes of the data that is on the genome. Transcripts are more interesting for RNA -seq analysis or proteomics)
        /// </summary>
        public int ExonNumber { get; set; }
        
        /// <summary>
        /// var for strand
        /// </summary>
        public int Strand { get; set; }

        /// <summary>
        /// var for frame 
        /// </summary>
        public int Frame { get; set; }

        /// <summary>
        /// var for protein id
        /// </summary>
        public string ProteinId { get; set; }

        /// <summary>
        /// produce as found
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// note on CDS
        /// </summary>
        public string Note { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor taking all the fields as input
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exonNumber"></param>
        /// <param name="strand"></param>
        /// <param name="frame"></param>
        /// <param name="proteinId"></param>
        public DataModelGeneTranscriptElementCDS(int start, int end, int exonNumber, int strand, int frame, string proteinId, string product, string note)
        {
            //set the fields
            Start = start;
            End = end;
            ExonNumber = exonNumber;
            Strand = strand;
            Frame = frame;
            ProteinId = proteinId;
            Product = product;
            Note = note;
        }

                                                                                                               
        #endregion

        #region methods

        #endregion

    }

    /// <summary>
    /// class representing the field for the exon
    /// </summary>
    public class DataModelGeneTranscriptElementExon
    {

        #region properties

        /// <summary>
        /// var for start
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for exon number
        /// </summary>
        public int ExonNumber { get; set; }
        
        /// <summary>
        /// var for strand
        /// </summary>
        public int Strand { get; set; }

        /// <summary>
        /// produce as found (this is the product of the gene typically a transcript)
        /// </summary>
        public string Product { get; set; }

        #endregion

        #region constructors


        /// <summary>
        /// constructor taking all the fields as input
        /// We will need to find the exon number by looking at a split list in the line feed of the attributes
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exonNumber"></param>
        /// <param name="strand"></param>
        public DataModelGeneTranscriptElementExon(int start, int end, int strand, string exonNumber, string product)
        {
            //set the fields
            Start = start;
            End = end;
            //set the exon number
            ExonNumber = Convert.ToInt32(exonNumber);
            Strand = strand;
            Product = product;
        }


        #endregion

    }

}
