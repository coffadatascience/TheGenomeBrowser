using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class that holds a data model for a single entity (exon, intron, etc)
    /// and entitiy is typically a part of a seqid (gene) and has a location on the seqid 
    /// We have nothing to sort anymore in this class so we add a full list of all the data
    /// Depending on the type of entity we have different properties
    /// </summary>
    public class DataModelGeneTranscriptObject
    {

        #region properties

        /// <summary>
        /// var with DataModelGeneTranscriptElementStartEndCodon information for this transcript
        /// </summary>
        public DataModelGeneTranscriptElementCodon DataModelGeneTranscriptElementStartCodon { get; set; }

        /// <summary>
        /// var with DataModelGeneTranscriptElementStartEndCodon information for this transcript
        /// </summary>
        public DataModelGeneTranscriptElementCodon DataModelGeneTranscriptElementStopCodon { get; set; }

        /// <summary>
        /// list of all coding sequences (CDS) for this transcript
        /// </summary>
        public List<DataModelGeneTranscriptElementCDS> ListDataModelGeneTranscriptElementCDS { get; set; }

        /// <summary>
        /// list of all exons for this transcript
        /// </summary>
        public List<DataModelGeneTranscriptElementExon> ListDataModelGeneTranscriptElementExon { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor setting the lists
        /// </summary>
        public DataModelGeneTranscriptObject()
        {
            //init the lists
            ListDataModelGeneTranscriptElementCDS = new List<DataModelGeneTranscriptElementCDS>();
            ListDataModelGeneTranscriptElementExon = new List<DataModelGeneTranscriptElementExon>();
            //init the codon
            DataModelGeneTranscriptElementStartCodon = null;
            DataModelGeneTranscriptElementStopCodon = null;
        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that take all necessary information to create a new DataModelGeneTranscriptElementExon and add it to the list
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exonNumber"></param>
        /// <param name="strand"></param>
        public void AddDataModelGeneTranscriptElementExon(int start, int end, string exonNumber, string strand)
        {
            //create a new DataModelGeneTranscriptElementExon
            var dataModelGeneTranscriptElementExon = new DataModelGeneTranscriptElementExon(start, end, exonNumber, strand);

            //add the DataModelGeneTranscriptElementExon to the list
            ListDataModelGeneTranscriptElementExon.Add(dataModelGeneTranscriptElementExon);
        }


        /// <summary>
        /// procedure that take all necessary information to create a new DataModelGeneTranscriptElementCDS and add it to the list
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exonNumber"></param>
        /// <param name="strand"></param>
        /// <param name="frame"></param>
        /// <param name="proteinId"></param>
        public void AddDataModelGeneTranscriptElementCDS(int start, int end, int exonNumber, string strand, string frame, string proteinId, string product)
        {
            //create a new DataModelGeneTranscriptElementCDS
            var dataModelGeneTranscriptElementCDS = new DataModelGeneTranscriptElementCDS(start, end, exonNumber, strand, frame, proteinId, product);

            //add the DataModelGeneTranscriptElementCDS to the list
            ListDataModelGeneTranscriptElementCDS.Add(dataModelGeneTranscriptElementCDS);
        }




        #endregion




    }

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
        public string Strand { get; set; }

        /// <summary>
        /// var for frame 
        /// </summary>
        public string Frame { get; set; }

        /// <summary>
        /// var for protein id
        /// </summary>
        public string ProteinId { get; set; }

        /// <summary>
        /// produce as found
        /// </summary>
        public string Product { get; set; }

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
        public DataModelGeneTranscriptElementCDS(int start, int end, int exonNumber, string strand, string frame, string proteinId, string product)
        {
            //set the fields
            Start = start;
            End = end;
            ExonNumber = exonNumber;
            Strand = strand;
            Frame = frame;
            ProteinId = proteinId;
            Product = product;
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
        public string Strand { get; set; }
    

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
        public DataModelGeneTranscriptElementExon(int start, int end, string strand, string exonNumber)
        {
            //set the fields
            Start = start;
            End = end;
            //set the exon number
            ExonNumber = Convert.ToInt32(exonNumber);
            Strand = strand;
        }


        #endregion

    }

}
