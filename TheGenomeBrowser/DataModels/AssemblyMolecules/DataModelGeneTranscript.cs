﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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
        /// var for transcript Id as used as a key (gene <--> transcript) This should be the same as Db_Xref which is extracted from the line feed
        /// </summary>
        public string TranscriptId { get; set; }

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

        // tag = 2024117TestMemoryUpdateGTFFile  ==> we will test to remove some of the attributes to see if the memory usage is reduced (we consider that we may remove these as they are later imported to the specific type and then taken from the line feed)
        /// <summary>
        /// var to score
        /// </summary>
        //public string Score { get; set; }

        /// <summary>
        /// var for strand
        /// </summary>
        public int Strand { get; set; }

        /// <summary>
        /// var for frame
        /// </summary>
        public int Frame { get; set; }

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
        /// var for model evidence (note that gene does not have this feature in this model but that such data was also note found 20240116)
        /// </summary>
        public string ModelEvidence { get; set; }

        /// <summary>
        /// read only property returning the number of exons
        /// </summary>
        public int NumberOfExons
        {
            get
            {
                //return the number of exons
                return GeneTranscriptObject.ListDataModelGeneTranscriptElementExon.Count;
            }
        }

        /// <summary>
        /// DataModelGeneTranscriptObject (contains start_codon, end_codon, list of exons, list of CDS)
        /// </summary>
        public DataModelGeneTranscriptObject GeneTranscriptObject{ get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelGeneTranscript() 
        {
            //init the GeneTranscriptObject
            GeneTranscriptObject = new DataModelGeneTranscriptObject();
        }

        /// <summary>
        /// constructor with parameters, that takes the first column based feeds as input and take the attribute line and splits this into the attribute fields using the header for recognition (note that this is similar to the constructor DataModelGeneId but there are few different fields)
        /// Note that the Id is passed as a parameter, as it is the Key
        /// </summary>
        /// <param name="seqName"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="score"></param>
        /// <param name="strand"></param>
        /// <param name="frame"></param>
        /// <param name="attributeLine"></param>
        public DataModelGeneTranscript(string transcripIdUsedKey, string seqName, int start, int end, int strand, int frame, string lineFeedAttributes) : this()
        {

            //new GeneTranscriptObject
            GeneTranscriptObject = new DataModelGeneTranscriptObject();

            //set the properties
            this.SeqName = seqName;
            this.Start = start;
            this.End = end;

            // tag = 2024117TestMemoryUpdateGTFFile  ==> we will test to remove some of the attributes to see if the memory usage is reduced (we consider that we may remove these as they are later imported to the specific type and then taken from the line feed)
            //this.Score = score;
            this.Strand = strand;
            this.Frame = frame;
            //set Key transcripId
            this.TranscriptId = transcripIdUsedKey;

            //We want to extract the following fields from the attribute line: gene_id, transcript_id, db_xref, gbkey, gene, pseudo, product, transcript_biotype
            //  - GeneId
            //  - Db_Xref(singular)
            //  - Gb_Key
            //  - GeneName(gene)
            //  - Pseudo(Boolean)
            //  - Product(unique for transcripts)
            //  -Transcript_Biotype(unique for transcripts)

            //split the line feed --> note that how we store the line, or take it from the field (either we read and store to our own format or keep the original line and format)
            string[] splitLineFeed = lineFeedAttributes.Split(';');

            //loop through the split line feed (recognize items by the header name and compare to the constant header names)
            //loop split line feed --> we first process positionally based elements (first 8 elements) (after the first 8 elements we can use the constant header names)
            for (int i = 0; i < splitLineFeed.Length; i++)
            {

                //var for the pair (pair is how we will save the entree (not the same as how we recognize the entree)
                string CurrentPair = splitLineFeed[i].Trim();

                //check if the current item is the gene id
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneIdHeaderName))
                {
                    //set the gene id
                    GeneId = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneIdHeaderName + " ", "");
                    //remove the double quotes
                    GeneId = GeneId.Replace("\"", "");
                    //remove the semi colon
                    GeneId = GeneId.Replace(";", "");
                    //trim
                    GeneId = GeneId.Trim();
                }

                //check if the current item is the db xref
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName))
                {
                    //set the db xref
                    Db_Xref = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName + " ", "");
                    //remove the double quotes
                    Db_Xref = Db_Xref.Replace("\"", "");
                    //remove the semi colon
                    Db_Xref = Db_Xref.Replace(";", "");
                    //trim
                }

                //check if the current item is the gb key
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.gbkeyHeaderName))
                {
                    //set the gb key
                    Gb_Key = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.gbkeyHeaderName + " ", "");
                    //remove the double quotes
                    Gb_Key = Gb_Key.Replace("\"", "");
                    //remove the semi colon
                    Gb_Key = Gb_Key.Replace(";", "");
                    //trim
                    Gb_Key = Gb_Key.Trim();
                }

                //check if the current item is the gene name
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName))
                {
                    //set the gene name
                    GeneName = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName + " ", "");
                    //remove the double quotes
                    GeneName = GeneName.Replace("\"", "");
                    //remove the semi colon
                    GeneName = GeneName.Replace(";", "");
                    //trim
                    GeneName = GeneName.Trim();
                }

                //check if the current item is the pseudo
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.PseudoHeaderName))
                {
                    //set the pseudo
                    Pseudo = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.PseudoHeaderName + " ", "");
                    //remove the double quotes
                    Pseudo = Pseudo.Replace("\"", "");
                    //remove the semi colon
                    Pseudo = Pseudo.Replace(";", "");
                    //trim
                    Pseudo = Pseudo.Trim();
                }

                //check if the current item is the product
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProductHeaderName))
                {
                    //set the product
                    Product = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProductHeaderName + " ", "");
                    //remove the double quotes
                    Product = Product.Replace("\"", "");
                    //remove the semi colon
                    Product = Product.Replace(";", "");
                    //trim
                    Product = Product.Trim();
                }

                //check if the current item is the transcript biotype
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptBiotypeHeaderName))
                {
                    //set the transcript biotype
                    Transcript_Biotype = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptBiotypeHeaderName + " ", "");
                    //remove the double quotes
                    Transcript_Biotype = Transcript_Biotype.Replace("\"", "");
                    //remove the semi colon
                    Transcript_Biotype = Transcript_Biotype.Replace(";", "");
                    //trim
                    Transcript_Biotype = Transcript_Biotype.Trim();
                }

                //check if the current item is the model evidence
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ModelEvidenceHeaderName))
                {
                    //set the model evidence
                    ModelEvidence = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ModelEvidenceHeaderName + " ", "");
                    //remove the double quotes
                    ModelEvidence = ModelEvidence.Replace("\"", "");
                    //remove the semi colon
                    ModelEvidence = ModelEvidence.Replace(";", "");
                    //trim
                    ModelEvidence = ModelEvidence.Trim();
                }

            }

        }


        #endregion


        #region methods




        #endregion


    }

    /// <summary>
    /// class that holds a data model for a single entity (exon, intron, etc)
    /// and entitiy is typically a part of a seqid (gene) and has a location on the seqid 
    /// We have nothing to sort anymore in this class so we add a full list of all the data
    /// Depending on the type of entity we have different properties
    /// NOTE --> 1. transcript heeft een lijst met exons, start en stop codon en CDS
    /// </summary>
    public class DataModelGeneTranscriptObject
    {


        #region properties

        /// <summary>
        /// read only property for the number of exons, return -1 if there are no exons
        /// </summary>
        public int NumberOfExons
        {
            get
            {
                //check if there are exons
                if (ListDataModelGeneTranscriptElementExon != null)
                {
                    //return the number of exons
                    return ListDataModelGeneTranscriptElementExon.Count;
                }
                else
                {
                    //return -1
                    return -1;
                }
            }
        }


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
        public void AddDataModelGeneTranscriptElementExon(int start, int end, string exonNumber, int strand, string product)
        {
            //create a new DataModelGeneTranscriptElementExon
            var dataModelGeneTranscriptElementExon = new DataModelGeneTranscriptElementExon(start, end, strand, exonNumber, product);

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
        public void AddDataModelGeneTranscriptElementCDS(int start, int end, int exonNumber, int strand, int frame, string proteinId, string product, string note)
        {
            //create a new DataModelGeneTranscriptElementCDS
            var dataModelGeneTranscriptElementCDS = new DataModelGeneTranscriptElementCDS(start, end, exonNumber, strand, frame, proteinId, product, note);

            //add the DataModelGeneTranscriptElementCDS to the list
            ListDataModelGeneTranscriptElementCDS.Add(dataModelGeneTranscriptElementCDS);
        }

        /// <summary>
        /// procedure that returns checks if all product are the same for all exons and then returns the product a string that states "Multiple products + the concatenated string of all products" if there are multiple products
        /// </summary>
        /// <returns></returns>
        public string ReturnProductNameForExons()
        {
            //var for the product
            string product = "";

            //loop the exons
            foreach (var exon in ListDataModelGeneTranscriptElementExon)
            {
                //check if the product is empty
                if (product == "")
                {
                    //set the product
                    product = exon.Product;
                }
                else
                {
                    //check if the product is not the same
                    if (product != exon.Product)
                    {
                        //return the product
                        return "Unexpected event found: multiple products in one exon list: " + product + " + " + exon.Product;
                    }
                }
            }

            //return the product
            return product;
        }


        /// <summary>
        /// similar procedure like ReturnProductNameForExons but for CDS
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ReturnProductNameForCDS()
        {

            //var for the product
            string product = "";

            //loop the CDS
            foreach (var cds in ListDataModelGeneTranscriptElementCDS)
            {
                //check if the product is empty
                if (product == "")
                {
                    //set the product
                    product = cds.Product;
                }
                else
                {
                    //check if the product is not the same
                    if (product != cds.Product)
                    {
                        //return the product
                        return "Unexpected event found: multiple products in one CDS list: " + product + " + " + cds.Product;
                    }
                }
            }

            //return the product
            return product;

        }

        /// <summary>
        /// procedure like ReturnProductNameForCDS but for notes in the CDS
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public string ReturnNoteOnCDS()
        {

            //var for the note
            string note = "";

            //loop the CDS
            foreach (var cds in ListDataModelGeneTranscriptElementCDS)
            {
                //check if the note is empty
                if (note == "")
                {
                    //set the note
                    note = cds.Note;
                }
                else
                {
                    //check if the note is not the same
                    if (note != cds.Note)
                    {
                        //return the note
                        return "Unexpected event found: multiple notes in one CDS list: " + note + " + " + cds.Note;
                    }
                }
            }

            //return the note
            return note;

        }
        #endregion


    }

    /// <summary>
    /// Struct class that contains the transcript id and the chromosome. We use this class to speed up the process of finding the gene related to a transcript
    /// so we may store the chromosome with the transcript ids, allowing fast tracking via the chromosome
    /// </summary>
    public struct TranscriptInfo
    {

        #region properties

        /// <summary>
        /// The transcript id.
        /// </summary>
        public string TranscriptId { get; set; }

        /// <summary>
        /// The chromosome.
        /// </summary>
        public string moleculeChromosome { get; set; }

        #endregion

        #region methods

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

        #endregion

    }



}
