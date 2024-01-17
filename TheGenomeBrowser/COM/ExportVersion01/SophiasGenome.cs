using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TheGenomeBrowser.COM.ExportVersion01
{

    /// <summary>
    /// Class that hold the curated genome data for Sophia, this is restructured and organised based on the Sophias Genome Browser
    /// We use these structures mainly to create relative viewpoints and order on a reference scale that may accommodate all products of the genome
    /// </summary>
    public class SophiasGenomeExportVersion01
    {

        #region constants

        //fields that are used in the files as constant tags

        //list with constant tag names as used for the fields found in the assembly report for sequences roles
        //constant for assembled-molecule
        public const string assembledMolecule = "assembled-molecule";
        //constant for unlocalized-scaffold
        public const string unlocalizedScaffold = "unlocalized-scaffold";
        //constant for unplaced-scaffold
        public const string unplacedScaffold = "unplaced-scaffold";
        //constant for fix-patch
        public const string fixPatch = "fix-patch";
        //constant for novel-patch
        public const string novelPatch = "novel-patch";
        //constant for alt-scaffold
        public const string altScaffold = "alt-scaffold";
        //constant for Mitochondrion
        public const string Mitochondrion = "Mitochondrion";
        //constant for other
        public const string other = "other";

        //constant names as used for Assigned-Molecule-Location/Type
        //constant for chromosome
        public const string chromosome = "Chromosome";
        //constant for mitochondrion
        public const string mitochondrion = "Mitochondrion";
        //constant for na
        public const string ConstantForNa = "na";

        //list of constants of source type value as found in the GTF file
        // --> note that a constant list of these value also exist in some source searching models (e.g. the NCBI search model)
        // --> because the COM needs to be independent of the other solutions, we keep these constants here as well)
        // --> This allows a copy of a COM object to be used in other solutions without the need to include the other solutions (autonomy in development only synchronisation of the COM object is needed when making a Suite or pipeline)
        //constant for the name of the Havana assembly source
        public const string HAVANA = "Havana";
        //constant for the name of the NCBI assembly source
        public const string NCBI = "NCBI";
        //constant for the name of the Ensembl assembly source
        public const string ENSEMBL = "Ensembl";
        //constant for the name of the Vega assembly source
        public const string VEGA = "Vega";
        //constant for the name of the Wormbase assembly source
        public const string WORMBASE = "Wormbase";

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

        //constants we use for GeneBiotype -	GeneBioType (filter): (enum 1. Protein_coding 2. Transcribed_pseudogene 3. Pseudogene 4. lncRNA (long non coding RNAs) 5. Other  should be none as noted in current annotation file)
        //constant for the name of the Protein_coding
        public const string Protein_coding = "protein_coding";
        //constant for the name of the Transcribed_pseudogene
        public const string Transcribed_pseudogene = "transcribed_pseudogene";
        //constant for the name of the Pseudogene
        public const string Pseudogene = "pseudogene";
        //constant for the name of the lncRNA
        public const string lncRNA = "lncRNA";
        //constant for the name of the Other
        public const string Other = "other";

        //constants we use for TranscriptBiotype - TranscriptBiotype (filter): (enum 1. transcript 2. mRNA 3. miRNA 4. Primary_transcript 5. lncRNA 6. Other)
        //constant for the name of the transcript
        public const string TranscriptBiotype_Transcript = "transcript";
        //constant for the name of the mRNA
        public const string TranscriptBiotype_mRNA = "mRNA";
        //constant for the name of the miRNA
        public const string TranscriptBiotype_miRNA = "miRNA";
        //constant for the name of the Primary_transcript
        public const string TranscriptBiotype_Primary_transcript = "Primary_transcript";
        //constant for the name of the lncRNA
        public const string TranscriptBiotype_lncRNA = "lncRNA";


        #endregion


        #region enums

        // Here we keep the enums that we may use for parsing the data of this model
        // As such this entire model may exist in the COM as well, and it is thus only the COM object that needs to sync (this allows all other solutions to remain autonomous)


        /// <summary>
        /// enum for Genome Conversion Type (enum: 1. NCBI 2. UCSC 3. Ensembl 4. Vega 5. Wormbase 6. RefSeq 7. Other)
        /// </summary>
        public enum SourceGenomeConversionType
        {
            NCBI = 1,
            UCSC = 2,
            Ensembl = 3,
            Vega = 4,
            Wormbase = 5,
            RefSeq = 6,
            Other = 7
        }

        /// <summary>
        /// var enum for SequenceRole (may be 1. assembled-molecule 2. unlocalized-scaffold 3. unplaced-scaffold 4. fix-patch 5. novel-patch 6. alt-scaffold 7. Mitochondrion 8. unplaced-scaffold 9. other (we add this enum with numbers and description to each enum with a user friendly notation)
        //  These are different names for assembly sequences as found in the annotation report. They indicate used sequences, but also sequences that are not used in the assembly. It is typically the assembly that is used to evaluate the genome, but there are more sequences such as mitochondria but also unplaced parts.
        /// </summary>
        public enum SequenceRole
        {
            assembledMolecule = 1,
            unlocalizedScaffold = 2,
            unplacedScaffold = 3,
            fixPatch = 4,
            novelPatch = 5,
            altScaffold = 6,
            Mitochondrion = 7,
            other = 8
        }

        /// <summary>
        /// Enum for source name (enums 1. BestRefSeq 2. Gnomon 3. Other (examples for future are: Havana 4. NCBI 5. RefSeq 6. RNAcentral 7. Vega 8. ensembl 9. genbank 10. mirbase 11. rfam 12. trnascan 13. uniprot 14. wormbase) but we leave them out for now)
        //  These are different sources that are used to annotate the genome. We may use these to compare the different sources and see if they are consistent. We may also use these to see if we can find the same gene in different sources.
        /// </summary>
        public enum SourceName
        {
            BestRefSeq = 1,
            Gnomon = 2,
            Other = 3
        }

        /// <summary>
        /// enum for Source Type
        /// </summary>&2CGnomon 6. Other (all other entrees found  should be none on the current annotation file)
        public enum SourceType
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
        /// enum for  GeneBioType (filter): (enum 1. Protein_coding 2. Transcribed_pseudogene 3. Pseudogene 4. lncRNA (long non coding RNAs) 5. Other  should be none as noted in current annotation file)
        /// </summary>
        public enum GeneBiotype
        {
            Protein_coding = 1,
            Transcribed_pseudogene = 2, //"transcribed_pseudogene
            Pseudogene = 3,
            lncRNA = 4,
            Other = 5
        }

        /// <summary>
        /// enum for TranscriptBiotype (filter): (enum 1. transcript 2. mRNA 3. miRNA 4. Primary_transcript 5. lncRNA 6. Other) 
        /// </summary>
        public enum TranscriptBiotype
        {
            transcript = 1,
            mRNA = 2,
            miRNA = 3,
            Primary_transcript = 4,
            lncRNA = 5,
            Other = 6
        }

        /// <summary>
        /// enum for strand (enums are 1. plus 2. minus 3. other)
        /// </summary>
        public enum Strand
        {
            plus = 1,
            minus = 2,
            unknown = 3
        }

        /// <summary>
        /// enum for Molecule location type (Enum:1. Chromosome 2. Mitochondrion 3. NA relation ship is equal 4. NA relation ship is not equal) 
        /// </summary>
        public enum MoleculeLocationType
        {
            Chromosome = 1,
            Mitochondrion = 2,
            NA_RelationShipIsEqual = 3,
            NA_RelationShipIsNotEqual = 4,
            Unknown = 5
        }
        
        //enum for Gene biotype (enum 1. Protein_coding 2. Transcribed_pseudogene 3. Pseudogene 4. lncRNA (long non coding RNAs) 5. Other  should be none as noted in current annotation file)
        //enum for Transcript biotype (enum 1. transcript 2. mRNA 3. miRNA 4. Primary_transcript 5. lncRNA 6. Other)
        public enum GeneTranscriptBiotype
        {
            Protein_coding = 1,
            Transcribed_pseudogene = 2,
            Pseudogene = 3,
            lncRNA = 4,
            Other = 5,
            transcript = 6,
            mRNA = 7,
            miRNA = 8,
            Primary_transcript = 9,
        }


        #endregion


        #region fields

        //For an export version we may want to keep the following information: a. Source: string output to that summarizes the used information source b. List of files(names of the files used to compose the model) c. Key: source and conversion type(specific for TheGenomeBrowser)


        /// <summary>
        /// date of creation curated mode
        /// </summary>
        public DateTime DateOfCreation { get; set; }

        /// <summary>
        /// var for source name (open field to describe the created export version)
        /// </summary>
        public string SourceDescription { get; set; }

        /// <summary>
        /// var for GenomeConversionType
        /// </summary>
        public SophiasGenomeExportVersion01.SourceGenomeConversionType SourceGenomeConversion { get; set; }

        /// <summary>
        /// var for list with files used to create the export version
        /// </summary>
        public List<string> ListOfUsedSourceFiles { get; set; }

        /// <summary>
        /// var for human genome data model
        /// </summary>
        public TheHumanGenomeSophiaDataModelCOM TheHumanGenomeSophiaDataModelCOM { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// var constructor
        /// </summary>
        public SophiasGenomeExportVersion01()
        {
            //create the human genome data model
            TheHumanGenomeSophiaDataModelCOM = new TheHumanGenomeSophiaDataModelCOM();
        }

        #endregion


        #region "methods"

        /// <summary>
        /// procedure that converts a string to a SequenceRole
        /// </summary>
        /// <param name="sequenceRoleString"></param>
        /// <returns></returns>
        public static SophiasGenomeExportVersion01.SequenceRole ConvertStringToSequenceRole(string sequenceRoleString)
        {

            //init the sequence role
            SophiasGenomeExportVersion01.SequenceRole sequenceRole = new SophiasGenomeExportVersion01.SequenceRole();

            //switch on the string (use the constant tags)
            switch (sequenceRoleString)
            {
                case SophiasGenomeExportVersion01.assembledMolecule:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.assembledMolecule;
                    break;
                case SophiasGenomeExportVersion01.unlocalizedScaffold:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.unlocalizedScaffold;
                    break;
                case SophiasGenomeExportVersion01.unplacedScaffold:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.unplacedScaffold;
                    break;
                case SophiasGenomeExportVersion01.fixPatch:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.fixPatch;
                    break;
                case SophiasGenomeExportVersion01.novelPatch:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.novelPatch;
                    break;
                case SophiasGenomeExportVersion01.altScaffold:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.altScaffold;
                    break;
                case SophiasGenomeExportVersion01.Mitochondrion:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.Mitochondrion;
                    break;
                case SophiasGenomeExportVersion01.other:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.other;
                    break;
                default:
                    sequenceRole = SophiasGenomeExportVersion01.SequenceRole.other;
                    break;
            }

            //return the sequence role
            return sequenceRole;

        }

        /// <summary>
        /// procedure that converts a string to a MoleculeLocationType using the constants as denoted in this class
        /// </summary>
        /// <param name="moleculeLocationTypeString"></param>
        /// <returns></returns>
        public static SophiasGenomeExportVersion01.MoleculeLocationType ConvertStringToMoleculeLocationType(string moleculeLocationTypeString)
        {
            //match the string to the constants and return an enum
            switch (moleculeLocationTypeString)
            {
                case SophiasGenomeExportVersion01.chromosome:
                    return SophiasGenomeExportVersion01.MoleculeLocationType.Chromosome;
                case SophiasGenomeExportVersion01.mitochondrion:
                    return SophiasGenomeExportVersion01.MoleculeLocationType.Mitochondrion;
                case SophiasGenomeExportVersion01.ConstantForNa:
                    return SophiasGenomeExportVersion01.MoleculeLocationType.NA_RelationShipIsEqual;
                default:
                    return SophiasGenomeExportVersion01.MoleculeLocationType.NA_RelationShipIsNotEqual;
            }
        }

        /// <summary>
        /// procedures that attempts to convert a string to int, if the value is not correct then it will return -1 (since this is usually used to state that a samples is failed)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ParseStringToInt(string value)
        {
            int myInt = -1;

            if (int.TryParse(value, out myInt))
            {
                return myInt;
            }
            return myInt;
        }

        /// <summary>
        /// Procedure that converts a string "source" to a source type enum by matching to the constant in this class (BESTREFSEQ, GNOMON, CURATEDREFSEQ, BESTREFSEQGNOMON, TRNASCAN, REFSEQ, OTHER)
        /// </summary>
        /// <param name="source">The string source to be converted</param>
        /// <returns>The corresponding source type enum</returns>
        public static SophiasGenomeExportVersion01.SourceType ConvertStringToSourceType(string source)
        {
            //match the string to the constants and return an enum
            switch (source)
            {
                case SophiasGenomeExportVersion01.BESTREFSEQ:
                    return SophiasGenomeExportVersion01.SourceType.BestRefSeq;
                case SophiasGenomeExportVersion01.GNOMON:
                    return SophiasGenomeExportVersion01.SourceType.Gnomon;
                case SophiasGenomeExportVersion01.CURATEDREFSEQ:
                    return SophiasGenomeExportVersion01.SourceType.CuratedRefSeq;
                case SophiasGenomeExportVersion01.BESTREFSEQGNOMON:
                    return SophiasGenomeExportVersion01.SourceType.BestRefSeqGnomon;
                case SophiasGenomeExportVersion01.TRNASCAN:
                    return SophiasGenomeExportVersion01.SourceType.tRNAscan;
                case SophiasGenomeExportVersion01.REFSEQ:
                    return SophiasGenomeExportVersion01.SourceType.RefSeq;
                default:
                    return SophiasGenomeExportVersion01.SourceType.Other;
            }
        }

        /// <summary>
        /// procedure that converts a string to a GeneBiotype using the constants as denoted in this class
        /// </summary>
        /// <param name="geneBiotypeString"></param>
        /// <returns></returns>
        public static SophiasGenomeExportVersion01.GeneBiotype ConvertStringToGeneBiotype(string geneBiotypeString)
        {
            //match the string to the constants and return an enum
            switch (geneBiotypeString)
            {
                case SophiasGenomeExportVersion01.Protein_coding:
                    return SophiasGenomeExportVersion01.GeneBiotype.Protein_coding;
                case SophiasGenomeExportVersion01.Transcribed_pseudogene:
                    return SophiasGenomeExportVersion01.GeneBiotype.Transcribed_pseudogene;
                case SophiasGenomeExportVersion01.Pseudogene:
                    return SophiasGenomeExportVersion01.GeneBiotype.Pseudogene;
                case SophiasGenomeExportVersion01.lncRNA:
                    return SophiasGenomeExportVersion01.GeneBiotype.lncRNA;
                case SophiasGenomeExportVersion01.Other:
                    return SophiasGenomeExportVersion01.GeneBiotype.Other;
                default:
                    return SophiasGenomeExportVersion01.GeneBiotype.Other;
            }
        }

        /// <summary>
        /// convert a string to a TranscriptBiotype using the constants as denoted in this class
        /// During import we set Plus to +1, Minus to -1 and Unknown to 0
        /// </summary>
        /// <param name="strandString"></param>
        /// <returns></returns>
        public static SophiasGenomeExportVersion01.Strand ConvertIntStrandToEnumStrand(int strandString)
        {
            //match the string to the constants and return an enum
            switch (strandString)
            {
                case 1:
                    return SophiasGenomeExportVersion01.Strand.plus;
                case -1:
                    return SophiasGenomeExportVersion01.Strand.minus;
                default:
                    return SophiasGenomeExportVersion01.Strand.unknown;

            }
        }

        /// <summary>
        /// procedure that converts a string to a GeneTranscriptBiotype using the constants as denoted in this class
        /// </summary>
        /// <param name="transcriptBiotypeString"></param>
        /// <returns></returns>
        public static SophiasGenomeExportVersion01.TranscriptBiotype ConvertStringToTranscriptBiotype(string transcriptBiotypeString)
        {
            //match the string to the constants and return an enum
            switch (transcriptBiotypeString)
            {
                case SophiasGenomeExportVersion01.TranscriptBiotype_Transcript:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.transcript;
                case SophiasGenomeExportVersion01.TranscriptBiotype_mRNA:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.mRNA;
                case SophiasGenomeExportVersion01.TranscriptBiotype_miRNA:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.miRNA;
                case SophiasGenomeExportVersion01.TranscriptBiotype_Primary_transcript:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.Primary_transcript;
                case SophiasGenomeExportVersion01.TranscriptBiotype_lncRNA:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.lncRNA;
                default:
                    return SophiasGenomeExportVersion01.TranscriptBiotype.Other;
            }
        }

        // convert a string to a TranscriptBiotype using the constants as denoted in this class


        


        #endregion

    }


    /// <summary>
    /// class for the human genome data model as curated by Sophia. For a genome assembled on a assemblye report we attempt to keep the following information:
    /// 0. -	Assembly name: (string) 
    //  1. -	Assembly description(string)
    //  2. -	Taxid(Int - 9606)
    //  3. -	Bioproject(string)
    //  4. -	Submitter(string)
    //  5. -	Date(string)
    //  6. -	Synonyms(string)
    //  7. -	Assembly type(string)
    //  8. -	Release type(string)
    //  9. -	Assembly Level(string)
    //  10. -	Genome Representation(string)
    /// </summary>
    public class TheHumanGenomeSophiaDataModelCOM
    {

        #region fields

        // here are the variable that are interesting for this class
        // we may add more variables if we want to keep more information
        // Note that we keep these fields so tha we may extent our essembly with other assemblies, profiles (e.g. an aberrant pattern or a different Taxid


        /// <summary>
        /// var for assembly name
        /// </summary>
        public string AssemblyName { get; set; }

        /// <summary>
        /// var for assembly description
        /// </summary>
        public string AssemblyDescription { get; set; }

        /// <summary>
        /// var for taxid
        /// </summary>
        public string TaxId { get; set; }

        /// <summary>
        /// var for bioproject
        /// </summary>
        public string BioProject { get; set; }

        /// <summary>
        /// var for submitter
        /// </summary>
        public string Submitter { get; set; }

        /// <summary>
        /// var for date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// var for synonyms
        /// </summary>
        public string Synonyms { get; set; }
        
        /// <summary>
        /// var for assembly type
        /// </summary>
        public string AssemblyType { get; set; }

        /// <summary>
        /// var for release type
        /// </summary>
        public string ReleaseType { get; set; }

        /// <summary>
        /// var for assembly level
        /// </summary>
        public string AssemblyLevel { get; set; }

        /// <summary>
        /// var for genome representation
        /// </summary>
        public string GenomeRepresentation { get; set; }

        /// <summary>
        /// var for RefSeqCategory
        /// </summary>
        public string RefSeqCategory { get; set; }

        /// <summary>
        /// list of Molecules (chromosomes used in the assembly of the genome model (note that the other list contains more molecules such as mitochondria, unplaced sequences, etc)
        /// ---> this list is supplemented with genes, transcript and genes (currently 20230115: the other is list currently not supplemented with genes, transcripts and genes)
        /// </summary>
        public List<MoleculeSophiaDataModelCOM> ListOfChromosomesUsedForAssembly { get; set; }

        /// <summary>
        /// Extended list of molecules (e.g. mitochondria, unplaced sequences, etc)
        /// </summary>
        public List<MoleculeSophiaDataModelCOM> ListOfMoleculesOther { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// var constructor
        /// </summary>
        public TheHumanGenomeSophiaDataModelCOM()
        {
            //init the list of chromosomes used for assembly
            ListOfChromosomesUsedForAssembly = new List<MoleculeSophiaDataModelCOM>();

            //init the list of molecules other
            ListOfMoleculesOther = new List<MoleculeSophiaDataModelCOM>();

        }

        #endregion


    }





    /// <summary>
    /// class for the molecule data model as curated by Sophia
    /// </summary>
    public class MoleculeSophiaDataModelCOM
    {

        #region fields


        /// <summary>
        /// KEY - name of the molecule as commonly used (chromosome number)
        ///     --> this is a weak key in case we have the annotation report there may be more entrees (molecules for a single chromosome)
        ///     --> RefSeqAccn is a unique key that is in both the GTF file and the annotation report and unique for each identified molecule
        /// NOTE JCO -- > this is also the key for the dictionary of molecules in the genome (as they are curated, we therefore use this the key for this value
        /// </summary>
        public string AssignedMolecule { get; set; }

        /// <summary>
        /// var for sequence name (naming of the sequence fragment used in the annotations analysis)
        /// </summary>
        public string SequenceName { get; set; }

        /// <summary>
        /// Key item - GenBankAccn (access to genbank entree for sequence as denoted in annotation report)
        /// </summary>
        public string GenbankAccn { get; set; }

        /// <summary>
        /// Key item - RefSeqAccn
        /// </summary>
        public string RefSeqAccn { get; set; }

        /// <summary>
        /// possible Key item - UCSCStyleName 
        /// </summary>
        public string UCSCStyleName { get; set; }

        /// <summary>
        /// sequence role -- > sequence role may determine how its used (e.g. to create the assembly)
        /// we may expect here that all sequences that have the enum "assigned molecule" are used in the assembly, but there are more types that are not used in the assembly, but included here anyways to have a complete genome.
        /// </summary>
        public SophiasGenomeExportVersion01.SequenceRole SequenceRole { get; set; }

        /// <summary>
        /// var for MoleculeLocationType
        /// </summary>
        public SophiasGenomeExportVersion01.MoleculeLocationType MoleculeLocationType { get; set; }

        /// <summary>
        /// var Sequence length 
        /// </summary>
        public int SequenceLength { get; set; }

        /// <summary>
        /// list of genes
        /// </summary>
        public List<GeneSophiaDataModelCOM> ListOfGenes { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public MoleculeSophiaDataModelCOM()
        {
            //init the list of genes
            ListOfGenes = new List<GeneSophiaDataModelCOM>();
        }

        #endregion



    }


    /// <summary>
    /// class that hold the data for a gene
    /// </summary>
    public class GeneSophiaDataModelCOM
    {

        #region fields


        /// <summary>
        /// gene ID -- Key item
        /// </summary>
        public string GeneId { get; set; }

        /// <summary>
        /// KEY - DbRefXrefOne
        /// </summary>
        public string DbRefXrefOne { get; set; }

        /// <summary>
        /// KEY - DbRefXrefTwo 
        /// </summary>
        public string DbRefXrefTwo { get; set; }

        /// <summary>
        /// start position of gene (typically the start of the first exon)
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// end position of gene (typically the end of the last exon)
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// enum for strand
        /// </summary>
        public SophiasGenomeExportVersion01.Strand Strand { get; set; }

        /// <summary>
        /// sourcetype
        /// </summary>
        public SophiasGenomeExportVersion01.SourceType SourceType { get; set; }

        /// <summary>
        /// gene biotype
        /// </summary>
        public SophiasGenomeExportVersion01.GeneBiotype GeneBiotype { get; set; }

        /// <summary>
        /// list of synonyms known
        /// </summary>
        public List<string> Gene_Synonyms { get; set; }

        /// <summary>
        /// list of transcripts for this gene
        /// </summary>
        public List<TranscriptSophiaDataModelCOM> ListOfTranscripts { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public GeneSophiaDataModelCOM() 
        { 
            //init the list of transcripts
            ListOfTranscripts = new List<TranscriptSophiaDataModelCOM>();

        }

        #endregion

        #region methods


        /// <summary>
        /// count the number of unique exons for this gene (these are all the exon entrees in all transcripts that are unique)
        /// </summary>
        /// <returns></returns>
        public int CountUniqueExons()
        {
            //init the hashset
            HashSet<int> uniqueExons = new HashSet<int>();

            //loop through the transcripts and add the exon numbers to the hashset
            foreach (TranscriptSophiaDataModelCOM transcript in ListOfTranscripts)
            {
                //loop through the exons
                foreach (ExonItemSophiaDataModelCOM exon in transcript.ListOfExons)
                {

                    //add the exon number to the hashset
                    uniqueExons.Add(exon.ExonNumber);

                }

            }

            //return the count of the hashset
            return uniqueExons.Count;

        }

        /// <summary>
        /// procedure that counts the number of unique exons using linq (we may check what is faster, and if we can use linq in the COM)
        /// </summary>
        /// <returns></returns>
        public int CountUniqueExonsLinq()
        {
            //use the linq to get the count of the unique exons
            var NumberOfExons = this.ListOfTranscripts.SelectMany(x => x.ListOfExons).Distinct().Count();

            //return the number of exons
            return NumberOfExons;

        }

        /// <summary>
        /// procedure that counts the number of unique transcripts for this gene
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int CountUniqueTranscripts()
        {
            //init the hashset
            HashSet<string> uniqueTranscripts = new HashSet<string>();

            //loop through the transcripts and add the exon numbers to the hashset
            foreach (TranscriptSophiaDataModelCOM transcript in ListOfTranscripts)
            {
                //add the transcript id to the hashset
                uniqueTranscripts.Add(transcript.TranscriptId);
            }

            //return the count of the hashset
            return uniqueTranscripts.Count;

        }

        /// <summary>
        /// procedure that make one string of all synonyms divided by a comma and a space
        /// </summary>
        /// <returns></returns>
        public string MakeSynonymsString()
        {
            //init the string
            string synonymsString = "";

            //loop through the synonyms and add them to the string
            foreach (string synonym in Gene_Synonyms)
            {
                //add the synonym to the string
                synonymsString += synonym + ", ";
            }

            //return the string
            return synonymsString;
        }

        /// <summary>
        /// procedure that return the name of the product of the first transcript
        /// </summary>
        /// <returns></returns>
        public string GetProductNameFirstTranscript()
        {
            //check if there are transcripts
            if (ListOfTranscripts.Count > 0)
            {
                //return the product name of the first transcript
                return ListOfTranscripts[0].CDSSophiaDataModelCOM.Product;
            }

            //return empty string
            return "";
        }

        #endregion


    }


    /// <summary>
    /// public class for a transcript. A transcript is a molecule that is transcribed from a gene. A gene may have multiple transcripts
    /// A transcript has a start and end codon, it has a list with exons and a list with CDS (coding sequences - the exons that are translated into a cds starting at the start codon and finishing at the end codon.)
    /// Typically a list of elements under the type "transcript" is denote in the data as the true transcript, the mRNA denotes to a transcript that was modified (e.g. spliced) and is ready for translation into protein
    /// In the transcript we will hold the strand information (plus /  minus), we could assume all transcript of a gene are on the same strand, but this is not always the case ? (e.g. for pseudo genes)
    /// -There is NO explicit or implicit association made between a CDS entry and its corresponding mRNA. The CDS feature entry contains no mRNA ID and if you think you can match them up based on the order of the mRNA and CDS entries, sorry, there is no guarantee that they will match. The only way to be confident of the CDS<->mRNA association is to perform an Entrez query of the CDS GI and parse the mRNA GI from that.
    /// </summary>
    public class TranscriptSophiaDataModelCOM
    {


        #region fields

        /// <summary>
        /// KEY - transcript id
        /// </summary>
        public string TranscriptId { get; set; }

        /// <summary>
        /// KEY - -	DbRefXref
        /// </summary>
        public string DbRefXref { get; set; }

        /// <summary>
        /// start position of transcript (typically the start of the first exon)
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end position of transcript (typically the end of the last exon)
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for strand
        /// </summary>
        public SophiasGenomeExportVersion01.Strand Strand { get; set; }
     
        /// <summary>
        /// var for transcript biotype
        /// </summary>
        public SophiasGenomeExportVersion01.TranscriptBiotype TranscriptBiotype { get; set; }

        //Note -- > a transcript has a list of exon that produce a product (usually a transcript BioType is mRNA. Its the CDS that is translated into a protein and thus here we have two products, one related to what is transcribed by the exons and possibly a second which is the product of that transcript (usually after modification) but a transcript may also be a functional molecule on its own.
        /// <summary>
        /// var for product of exons (note that we obtain this from the first exon item in the list of exons --> we assume that all exon items in the list are the same)
        /// </summary>
        public string ProductOfExons { get; set; }

        /// <summary>
        /// list of exons
        /// </summary>
        public List<ExonItemSophiaDataModelCOM> ListOfExons { get; set; }

        /// <summary>
        /// var for start codon (note that is the strand is plus, then the end of the start codon is plus 3, if its minus it is minus 3)
        /// this number typically matches with a position in the first exon where the CDS start --> the part that gets translated into amino acids
        /// </summary>
        public int StartCodonStart { get; set; }

        /// <summary>
        /// var for end of the start codon (typically + 3)
        /// </summary>
        public int StartCodonEnd{ get; set; }

        /// <summary>
        /// var for end codon (note that is the strand is plus, then the end of the end codon is plus 3, if its minus it is minus 3)
        /// This number typically matches with a position in the last exon where the CDS ends --> the part that gets translated into amino acids
        /// </summary>
        public int StopCodonStart { get; set; }

        /// <summary>
        /// end of stop codon (typically + 3)
        /// </summary>
        public int StopCodonEnd { get; set; }

        /// <summary>
        /// string var to product of CDS (note that we obtain this from the first CDS item in the list of CDS --> we assume that all CDS items in the list are the same)
        /// </summary>
        public string ProductOfCDS { get; set; }
        
        /// <summary>
        /// var for note on cDS (extracted from a CDS line, tell something about the product)
        /// </summary>
        public string NoteOnCDS { get; set; }


        /// <summary>
        /// var for model evidence (typically present on the transcript)
        /// </summary>
        public string ModelEvidence { get; set; }

        /// <summary>
        /// CDS object
        /// </summary>
        public CDSSophiaDataModelCOM CDSSophiaDataModelCOM { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public TranscriptSophiaDataModelCOM()
        {
            //init the list of exons
            ListOfExons = new List<ExonItemSophiaDataModelCOM>();

            //init the CDS object
            CDSSophiaDataModelCOM = new CDSSophiaDataModelCOM();
        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that takes information to create a new CDS item in parameters (then makes a object and adds it to the list by AddCDSItemToList)
        /// </summary>
        /// <param name="exonNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddCDSItemToList(int exonNumber, int start, int end)
        {
            //create a new CDS item
            var cdsItem = new CDSItemSophiaDataModelCOM(exonNumber, start, end);

            //add the item to the list
            CDSSophiaDataModelCOM.AddCDSItemToList(cdsItem);
        }


        /// <summary>
        /// procedure that takes al checks if the exon number is unique and then adds it to the list
        /// </summary>
        /// <param name="exonNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void AddExonItemToList(int exonNumber, int start, int end)
        {
            //create a new exon item
            var exonItem = new ExonItemSophiaDataModelCOM(exonNumber, start, end);

            //check if the exon number is unique
            if (ListOfExons.Any(x => x.ExonNumber == exonItem.ExonNumber))
            {
                //throw an error
                throw new Exception("The exon number is not unique");
            }

            //add the item to the list
            ListOfExons.Add(exonItem);
        }


        #endregion




    }


    /// <summary>
    /// class to hold the data for a single exon
    /// </summary>
    public class ExonItemSophiaDataModelCOM
    {

        #region fields

        /// <summary>
        /// KEY - exon number
        /// </summary>
        public int ExonNumber { get; set; }

        /// <summary>
        /// start
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end
        /// </summary>
        public int End { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// var parameterless constructor
        /// </summary>
        public ExonItemSophiaDataModelCOM()
        {
        }

        /// <summary>
        /// contructor with all fields
        /// </summary>
        /// <param name="exonNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public ExonItemSophiaDataModelCOM(int exonNumber, int start, int end)
        {
            //set the fields
            ExonNumber = exonNumber;
            Start = start;
            End = end;
        }

        #endregion


    }


    /// <summary>
    ///  Coding sequence information for which we use a separate class as this CDS contain the protein ID and product information
    /// --> Note that the data does suggest that each transcript has one CDS, as such we will place the CDS as an object within transcript (and error check if exon items in the list are unique)
    /// </summary>
    public class CDSSophiaDataModelCOM
    {

        #region fields

        /// <summary>
        /// var for product
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        ///  KEY var for protein id
        /// </summary>
        public string ProteinId { get; set; }

        /// <summary>
        /// list of CDS
        /// </summary>
        public List<CDSItemSophiaDataModelCOM> ListOfCDS { get; set; }

        #endregion


        #region constructors


        /// <summary>
        /// constructor with all fields
        /// </summary>
        /// <param name="product"></param>
        /// <param name="proteinId"></param>
        /// <param name="listOfCDS"></param>
        public CDSSophiaDataModelCOM(string product, string proteinId)
        {
            //init the list of CDS
            ListOfCDS = new List<CDSItemSophiaDataModelCOM>();
        }

        public CDSSophiaDataModelCOM()
        {
        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that adds a CDS item to the list by checking if the exon number is unique
        /// </summary>
        /// <param name="cdsItem"></param>
        /// <exception cref="Exception"></exception>
        public void AddCDSItemToList(CDSItemSophiaDataModelCOM cdsItem)
        {
            //check if the exon number is unique
            if (ListOfCDS.Any(x => x.ExonNumber == cdsItem.ExonNumber))
            {
                //throw an error
                throw new Exception("The exon number is not unique");
            }
            else
            {
                //add the item to the list
                ListOfCDS.Add(cdsItem);
            }
        }


        #endregion

    }


    /// <summary>
    /// class to hold the data for a single CDS (coding sequence)
    /// </summary>
    public class CDSItemSophiaDataModelCOM
    {

        #region fields


        /// <summary>
        /// KEY - exon number -- > exon here means which exon the cds seems to be located within (typically these a suggested linkages and are technically non existent)
        /// </summary>
        public int ExonNumber { get; set; }

        /// <summary>
        /// start
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// var for end
        /// </summary>
        public int End { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// parameterless constructor
        /// </summary>
        public CDSItemSophiaDataModelCOM()
        {
        }

        /// <summary>
        /// constructor with all fields
        /// </summary>
        /// <param name="exonNumber"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public CDSItemSophiaDataModelCOM(int exonNumber, int start, int end)
        {
            //set the fields
            ExonNumber = exonNumber;
            Start = start;
            End = end;
        }

        #endregion


        #region methods

        //procedure 


        #endregion


    }


}
