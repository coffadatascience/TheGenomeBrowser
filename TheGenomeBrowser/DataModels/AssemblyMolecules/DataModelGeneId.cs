using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        /// var for seqname (typically the accession number of the chromosome or scaffold)
        /// </summary>
        public string SeqName { get; set; }

        /// <summary>
        /// var for 2.	source - name of the program that generated this feature, or the data source (database or project name)
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// feature feature type name, e.g. Gene, Variation, Similarity
        /// </summary>
        public string Feature { get; set; }

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


        // ------------------------------------------------------------------------
        //The fields below are extracted from the attribute list (note that the attribute list is different for each feature type, so we need to process it different for each feature type)
        // ------------------------------------------------------------------------

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
        public string Description { get; set; }

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
        /// var for Pseudo (true or false)
        /// </summary>
        public string Pseudo { get; set; }

        /// <summary>
        /// var for Gene_Synonym (alternative gene names)
        /// </summary>
        public string Gene_Synonym_One { get; set; }

        /// <summary>
        /// var for Gene_Synonym (alternative gene names) -- > note that these may be lists but we only take the first two alternative (so that already 3)
        /// </summary>
        public string Gene_Synonym_Two { get; set; }

        /// <summary>
        /// list of transcript elements
        /// </summary>
        public List<DataModelGeneTranscript> ListGeneTranscripts { get; set; }


        #endregion


        #region constructors


        /// <summary>
        /// constructor that take string GeneId, and a string that is the line feed from the annotation file. A line feed with feature gene may contain different elements that e.g. transcript. So it will be more organised to make the split here, where the relevant field can be expected. (Example: NC_000001.11	BestRefSeq	gene	14362	29370	.	-	.	gene_id "WASH7P"; transcript_id ""; db_xref "GeneID:653635"; db_xref "HGNC:HGNC:38034"; description "WASP family homolog 7, pseudogene"; gbkey "Gene"; gene "WASH7P"; gene_biotype "transcribed_pseudogene"; gene_synonym "FAM39F"; gene_synonym "WASH5P"; pseudo "true"; 
        /// here we process the line feed and set the relevant fields (example line NC_000001.11	BestRefSeq	gene	14362	29370	.	-	.	gene_id "WASH7P"; transcript_id ""; db_xref "GeneID:653635"; db_xref "HGNC:HGNC:38034"; description "WASP family homolog 7, pseudogene"; gbkey "Gene"; gene "WASH7P"; gene_biotype "transcribed_pseudogene"; gene_synonym "FAM39F"; gene_synonym "WASH5P"; pseudo "true"; 
        //  we can use the constant header names as note in the SettingsAssemblySource.cs file to find the relevant fields
        //  note that we may have multiple gene_synonym and db_xref, so we take the first two
        // ------------------------------------------------------------------------
        // Note --> the first 8 lines are split positionally in the GTF file (column position) and contain for all types the same information (correct notated) so we may copy them here
        //          The Attribute list is stored in the GTF Data Model as a string and is different depending on the feature type. So the attribute line feed is passed here to GeneId so that the information may be processed correct.    
        /// </summary>
        /// <param name="geneId"></param>
        /// <param name="lineFeed"></param>
        public DataModelGeneId(string seqname, string source, string feature, int start, int end, string score, string strand, string frame, string geneId, string lineFeedAttributes)
        {

            //set fields
            SeqName = seqname;
            Source = source;
            Feature = feature;
            Start = start;
            End = end;
            Score = score;
            Strand = strand;
            Frame = frame;

            //set the gene id
            GeneId = geneId;

            //split the line feed --> note that how we store the line, or take it from the field (either we read and store to our own format or keep the original line and format)
            string[] splitLineFeed = lineFeedAttributes.Split(';');

            //loop through the split line feed (recognize items by the header name and compare to the constant header names)
            //loop split line feed --> we first process positionally based elements (first 8 elements) (after the first 8 elements we can use the constant header names)
            for (int i = 0; i < splitLineFeed.Length; i++)
            {

                //var for the pair (pair is how we will save the entree (not the same as how we recognize the entree)
                string CurrentPair = splitLineFeed[i].Trim();

                //check if the current item is the gene name
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName))
                {
                    //set the gene name
                    GeneName = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName + " ", "");
                    //remove the double quotes
                    GeneName = GeneName.Replace("\"", "");
                    //remove the semi colon
                    GeneName = GeneName.Replace(";", "");
                    //trim the gene name
                    GeneName = GeneName.Trim();
                }

                //check if the current item is the gene description
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DescriptionHeaderName))
                {
                    //set the gene description
                    Description = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DescriptionHeaderName + " ", "");
                    //remove the double quotes
                    Description = Description.Replace("\"", "");
                    //remove the semi colon
                    Description = Description.Replace(";", "");
                    //trim the gene description
                    Description = Description.Trim();
                }
         
                //check if the current item is the db xref one
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName))
                {

                    //check if we have already set the db xref one
                    if (Db_Xref_One == null)
                    {
                        //set the db xref one
                        Db_Xref_One = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName + " ", "");
                        //remove the double quotes
                        Db_Xref_One = Db_Xref_One.Replace("\"", "");
                        //remove the semi colon
                        Db_Xref_One = Db_Xref_One.Replace(";", "");
                        //trim the db xref one
                        Db_Xref_One = Db_Xref_One.Trim();
                    }
                    else
                    {
                        //set the db xref two
                        Db_Xref_Two = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName + " ", "");
                        //remove the double quotes
                        Db_Xref_Two = Db_Xref_Two.Replace("\"", "");
                        //remove the semi colon
                        Db_Xref_Two = Db_Xref_Two.Replace(";", "");
                        //trim the db xref two
                        Db_Xref_Two = Db_Xref_Two.Trim();
                    }

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
                    //trim the gb key
                    Gb_Key = Gb_Key.Trim();
                }

                //process the gene synonym
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneSynonymHeaderName))
                {

                    //check if we have already set the gene synonym one
                    if (Gene_Synonym_One == null)
                    {
                        //set the gene synonym one
                        Gene_Synonym_One = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneSynonymHeaderName + " ", "");
                        //remove the double quotes
                        Gene_Synonym_One = Gene_Synonym_One.Replace("\"", "");
                        //remove the semi colon
                        Gene_Synonym_One = Gene_Synonym_One.Replace(";", "");
                        //trim the gene synonym one
                        Gene_Synonym_One = Gene_Synonym_One.Trim();
                    }
                    else
                    {
                        //set the gene synonym two
                        Gene_Synonym_Two = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneSynonymHeaderName + " ", "");
                        //remove the double quotes
                        Gene_Synonym_Two = Gene_Synonym_Two.Replace("\"", "");
                        //remove the semi colon
                        Gene_Synonym_Two = Gene_Synonym_Two.Replace(";", "");
                        //trim the gene synonym two
                        Gene_Synonym_Two = Gene_Synonym_Two.Trim();
                    }

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
                    //trim the pseudo
                    Pseudo = Pseudo.Trim();
                }
                
                //check if the current item is the gene biotype
                if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneBiotypeHeaderName))
                {
                    //set the gene biotype
                    Gene_Biotype = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneBiotypeHeaderName + " ", "");
                    //remove the double quotes
                    Gene_Biotype = Gene_Biotype.Replace("\"", "");
                    //remove the semi colon
                    Gene_Biotype = Gene_Biotype.Replace(";", "");
                    //trim the gene biotype
                    Gene_Biotype = Gene_Biotype.Trim();
                }

            }

            //set the list of transcript elements
            ListGeneTranscripts = new List<DataModelGeneTranscript>();
    
        }


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
        public DataModelGeneId(string geneId, string geneName, string geneDescription, string strand, int startLocation, int endLocation, string db_Xref_One, string db_Xref_Two, string gb_Key, string gene_Biotype, string gene_Synonym_One, string gene_Synonym_Two)
        {
            //set the gene id
            GeneId = geneId;

            //set the gene name
            GeneName = geneName;

            //set the gene description
            Description = geneDescription;

            //set the strand
            Strand = strand;

            //set the start location
            Start = startLocation;

            //set the end location
            End = endLocation;

            //set the db xref one
            Db_Xref_One = db_Xref_One;

            //set the db xref two
            Db_Xref_Two = db_Xref_Two;

            //set the gb key
            Gb_Key = gb_Key;

            //set the gene biotype
            Gene_Biotype = gene_Biotype;

            //set the gene synonym
            Gene_Synonym_One = gene_Synonym_One;

            //set the gene synonym
            Gene_Synonym_Two = gene_Synonym_Two;

            //set the list of gene elements
            ListGeneTranscripts = new List<DataModelGeneTranscript>();
        }

        /// <summary>
        /// searches the list of gene transcripts for a given gene transcript id and returns the gene transcript
        /// </summary>
        /// <param name="geneIdTranscriptId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataModelGeneTranscript GetGeneTranscript(string geneIdTranscriptId)
        {
            //loop the list of gene transcripts
            foreach (var geneTranscript in ListGeneTranscripts)
            {
                //check if the gene transcript id is the same as the input
                if (geneTranscript.TranscriptId == geneIdTranscriptId)
                {
                    //return the gene transcript
                    return geneTranscript;
                }
            }

            //return null
            return null;
        }


        #endregion


        #region methods


        #endregion


    }


}
