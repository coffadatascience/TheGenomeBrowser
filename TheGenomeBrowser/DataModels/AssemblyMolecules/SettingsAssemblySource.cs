using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class with a list of settings that are used to load the assembly molecules
    /// </summary>
    public static class SettingsAssemblySource
    {


        #region constants

        //constant for the name of the BestRefSeq assembly source
        public const string BESTREFSEQ = "BestRefSeq";

        //constant for the name of the Gnomon assembly source
        public const string GNOMON = "Gnomon";

        //constant for the name of the CuratedRefSeq assembly source
        public const string CURATEDREFSEQ = "Curated Genomic";

        //constant for the name of the BestRefSeq% 2CGnomon
        public const string BESTREFSEQGNOMON = "BestRefSeq%2CGnomon";

        //constant for the name of the tRNAscan
        public const string TRNASCAN = "tRNAscan";

        //constant for the name of the RefSeq
        public const string REFSEQ = "RefSeq";

        //constant for feature type transcript
        public const string TRANSCRIPT = "transcript";

        //constant for feature type start_codon
        public const string START_CODON = "start_codon";

        //constant for feature type stop_codon
        public const string STOP_CODON = "stop_codon";

        //constant for feature type exon
        public const string EXON = "exon";

        //constant for feature type CDS
        public const string CDS = "CDS";

        //constant for feature type gene
        public const string GENE = "gene";


        #endregion

        #region constant headers annotation file


        // Note: note all elements in the file have a header name on the line, some are position based (e.g. the first 8 elements of the file are always the same)
        // this is true for 1. Seqname 2. source 3. feature 4. start 5. end 6. score 7. score 8. strand 9. frame 10. attribute Example = NC_000001.11	BestRefSeq	gene	11874	14409	.	+	.	

        //-------------------------------------
        // list of constants used for gene entree
        //--------------------------------------

        // Example line of a gene entree
        // NC_000001.11	BestRefSeq	gene	14362	29370	.	-	.	gene_id "WASH7P"; transcript_id ""; db_xref "GeneID:653635"; db_xref "HGNC:HGNC:38034"; description "WASP family homolog 7, pseudogene"; gbkey "Gene"; gene "WASH7P"; gene_biotype "transcribed_pseudogene"; gene_synonym "FAM39F"; gene_synonym "WASH5P"; pseudo "true"; 

        /// <summary>
        /// constant for the header name of the gene id
        /// </summary>
        public const string GeneIdHeaderName = "gene_id";

        /// <summary>
        /// var for db_xref (note that there may be multiple entrees with the same db_xref header name)
        /// </summary>
        public const string DbXrefHeaderName = "db_xref";

        /// <summary>
        /// constant for description
        /// </summary>
        public const string DescriptionHeaderName = "description";

        /// <summary>
        /// constant name for gbkey
        /// </summary>
        public const string gbkeyHeaderName = "gbkey";

        /// <summary>
        /// constant for the header name of the gene name
        /// </summary>
        public const string GeneNameHeaderName = "gene";

        /// <summary>
        /// constant for gene_synonym 
        /// </summary>
        public const string GeneSynonymHeaderName = "gene_synonym";

        /// <summary>
        /// var for pseudo (true or false)
        /// </summary>
        public const string PseudoHeaderName = "pseudo";

        /// <summary>
        /// var for gene_biotype
        /// </summary>
        public const string GeneBiotypeHeaderName = "gene_biotype";
        //--------------------------------------


        //--------------------------------------
        // list used for Transcript entree (note that the entree below are unique for transcript but that they may include field described above)
        //--------------------------------------

        // Example line of a transcript entree
        // NC_000001.11	BestRefSeq	transcript	14362	29370	.	-	.	gene_id "WASH7P"; transcript_id "NR_024540.1"; db_xref "GeneID:653635"; gbkey "misc_RNA"; gene "WASH7P"; product "WASP family homolog 7, pseudogene"; pseudo "true"; transcript_biotype "transcript"; 

        /// <summary>
        /// constant for the header name of the transcript id
        /// </summary>
        public const string TranscriptIdHeaderName = "transcript_id";

        /// <summary>
        /// constant for product
        /// </summary>
        public const string ProductHeaderName = "product";

        /// <summary>
        /// constant for protein_id
        /// </summary>
        public const string ProteinIdHeaderName = "protein_id";

        /// <summary>
        /// var for transcript_biotype
        /// </summary>
        public const string TranscriptBiotypeHeaderName = "transcript_biotype";


        //--------------------------------------
        // list used for Gene element (exon) entree (note that the entree below are unique for exon but that they may include field described above)
        //--------------------------------------

        // Example line of a gene element (exon) entree
        // NC_000001.11	BestRefSeq	exon	29321	29370	.	-	.	gene_id "WASH7P"; transcript_id "NR_024540.1"; db_xref "GeneID:653635"; gene "WASH7P"; product "WASP family homolog 7, pseudogene"; pseudo "true"; transcript_biotype "transcript"; exon_number "1"; 

        /// <summary>
        /// constant for the header name of the exon number
        /// </summary>
        public const string ExonNumberHeaderName = "exon_number";






        #endregion



        #region enums

        /// <summary>
        /// enum for the different assembly sources (BesteRefSeq, Gnomon, CuratedRefSeq)
        /// Other will contain all other sources that are currently unmatched against the other sources
        /// </summary>
        public enum AssemblySource
        {
            BestRefSeq,
            Gnomon,
            CuratedRefSeq,
            BestRefSeqGnomon,
            tRNAscan,
            RefSeq,
            Other
        }

        /// <summary>
        /// enum for feature types there is transcript, start_codon, stop_codon, exon, CDS and gene (these denotation are how they are found in the file, and how we may recognize them)
        /// </summary>
        public enum FeatureType
        {
            transcript,
            start_codon,
            stop_codon,
            exon,
            CDS,
            gene
        }

        #endregion

        #region methods

        /// <summary>
        /// procedure that returns the feature type based on the string (by matching it against the constants)
        /// </summary>
        /// <param name="featureType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static FeatureType GetFeatureType(string featureType)
        {

            //set the feature type
            if (featureType == TRANSCRIPT)
            {
                return FeatureType.transcript;
            }
            else if (featureType == START_CODON)
            {
                return FeatureType.start_codon;
            }
            else if (featureType == STOP_CODON)
            {
                return FeatureType.stop_codon;
            }
            else if (featureType == EXON)
            {
                return FeatureType.exon;
            }
            else if (featureType == CDS)
            {
                return FeatureType.CDS;
            }
            else if (featureType == GENE)
            {
                return FeatureType.gene;
            }
            else
            {
                throw new Exception("The feature type " + featureType + " is not recognized");
            }

        }

        /// <summary>
        /// procedure that return the enum for the source type based on the string
        /// </summary>
        /// <param name="sourceName"></param>
        public static SettingsAssemblySource.AssemblySource ReturnSourceEnumByString(string source)
        {

            //set the source type
            if (source == SettingsAssemblySource.BESTREFSEQ)
            {
                return SettingsAssemblySource.AssemblySource.BestRefSeq;
            }
            else if (source == SettingsAssemblySource.GNOMON)
            {
                return SettingsAssemblySource.AssemblySource.Gnomon;
            }
            else if (source == SettingsAssemblySource.CURATEDREFSEQ)
            {
                return SettingsAssemblySource.AssemblySource.CuratedRefSeq;
            }
            else if (source == SettingsAssemblySource.BESTREFSEQGNOMON)
            {
                return SettingsAssemblySource.AssemblySource.BestRefSeqGnomon;
            }
            else if (source == SettingsAssemblySource.TRNASCAN)
            {
                return SettingsAssemblySource.AssemblySource.tRNAscan;
            }
            else if (source == SettingsAssemblySource.REFSEQ)
            {
                return SettingsAssemblySource.AssemblySource.RefSeq;
            }
            else
            {
                return SettingsAssemblySource.AssemblySource.Other;
                //throw new Exception("The source type is not recognized");
            }

        }

        /// <summary>
        /// procedure that return the string for the source type based on the enum
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        internal static string ReturnSourceStringByEnum(AssemblySource sourceType)
        { 
            //translate the source type enum to a string in a readable format
            switch (sourceType)
            {
                case AssemblySource.BestRefSeq:
                    return BESTREFSEQ;
                case AssemblySource.Gnomon:
                    return GNOMON;
                case AssemblySource.CuratedRefSeq:
                    return CURATEDREFSEQ;
                case AssemblySource.BestRefSeqGnomon:
                    return BESTREFSEQGNOMON;
                case AssemblySource.tRNAscan:
                    return TRNASCAN;
                case AssemblySource.RefSeq:
                    return REFSEQ;
                case AssemblySource.Other:
                    return "Other";
                default:
                    throw new NotImplementedException();
            }

        }

        #endregion

    }


}
