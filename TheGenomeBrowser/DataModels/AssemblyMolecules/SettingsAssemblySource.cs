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

        #region enums

        /// <summary>
        /// enum for the different assembly sources (BesteRefSeq, Gnomon, CuratedRefSeq)
        /// </summary>
        public enum AssemblySource
        {
            BestRefSeq,
            Gnomon,
            CuratedRefSeq
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

        #endregion

    }


}
