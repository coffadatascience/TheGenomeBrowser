using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel;

namespace TheGenomeBrowser.DataModels.NCBIImportedData
{

    /// <summary>
    /// class that hold the assembly report information of a GTF file (contains a list of GTFFeatures)
    /// Note: although the read data model may contain all fields of the GTF file, the search table should be limited to the information needed, and normalized where possible. So for a gene, we only need have a single entrée of the name (and not the name with every exon). Next to that we must aim to use star diagrams and fact tables to minimize pollution.
    /// We may furthermore choose to only download gene sequences, when they are needed.These may then be comprised and stored, so that a investigation (or product over time) may download the sequence in the background, but doesn’t need to download and sort the content each time it is used.We do not want to download everything as this may be too much, and we may not need it. Next to that ref seq version may be updates over time, so we must take into account that designs are based on a version, and that we thus make copies of what we use (in and organized manner).
    /// Some "NT_ , NW_" are alternative assemblies of NC_ and the info contained is "the same" being placed lines below that NC_, but some others do not and contains genes of interest which I could be loosing when using bedtools i.e https://www.ncbi.nlm.nih.gov/gene/3806. Some do not have a known location but that gene is known to be in the chromosome 19 and then we cannot deduce it from its accession number. Note also that these accession number may contain ribosomal DNA and other elements that may not have a chromosomal position. Options to solution:
    /// An assembly_report.txt file accompanies NCBI RefSeq genome assemblies that can be downloaded either from the NCBI Assembly portal by searching for the genome of interest and picking the Assembly structure report from the big blue downloads button menu or by going to the NCBI genomes FTP path for the assembly of interest.
    /// </summary>
    public class DataModelAssemblyReport
    {


        #region fields

        /// <summary>
        /// var for assembly name
        /// </summary>
        // set description name with a user friendly name format
        [Description("Assembly Name")]
        public string AssemblyName { get; internal set; }

        /// <summary>
        /// var for description
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// var for organism name
        /// </summary>
        public string OrganismName { get; internal set; }

        /// <summary>
        /// var for taxid
        /// 
        /// </summary>
        public string TaxId { get; internal set; }

        /// <summary>
        /// var for bio project
        /// </summary>
        public string BioProject { get; internal set; }

        /// <summary>
        /// var for submitter
        /// </summary>
        public string Submitter { get; internal set; }

        /// <summary>
        /// var for date
        /// </summary>
        public string Date { get; internal set; }

        /// <summary>
        /// var for synonyms
        /// </summary>
        public string Synonyms { get; internal set; }

        /// <summary>
        /// var for assembly type
        /// </summary>
        public string AssemblyType { get; internal set; }

        /// <summary>
        /// var for release type
        /// </summary>
        public string ReleaseType { get; internal set; }

        /// <summary>
        /// var for assembly level
        /// </summary>
        public string AssemblyLevel { get; internal set; }

        /// <summary>
        /// var for genome representation
        /// </summary>
        public string GenomeRepresentation { get; internal set; }

        /// <summary>
        /// var for ref seq category
        /// </summary>
        public string RefSeqCategory { get; internal set; }

        /// <summary>
        /// var for gen bank assembly accession
        /// </summary>
        public string GenBankAssemblyAccession { get; internal set; }

        /// <summary>
        /// var for ref seq assembly accession
        /// </summary>
        public string RefSeqAssemblyAccession { get; internal set; }

        /// <summary>
        /// var for ref seq and gen bank assemblies identical
        /// </summary>
        public string RefSeqAndGenBankAssembliesIdentical { get; internal set; }

        /// <summary>
        /// list of assembly report items
        /// </summary>
        public List<DataModelAssemblyReportItem> AssemblyReportItemsList { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelAssemblyReport()
        {
            //set the list
            AssemblyReportItemsList = new List<DataModelAssemblyReportItem>();
        }

        #endregion


        #region methods

        /// <summary>
        /// function that accepts Relationship as a parameter and returns the Assigned-Molecule (chromosome) and the RefSeq-Accn (chromosome number) (the relation should match with Seqname in the GTF file)
        /// </summary>
        /// <param name="relationship"></param>
        /// <returns></returns>
        public string GetChromosomeNumber(string seqAccessionNumber)
        {
            //create a new instance of the data model
            var dataModelAssemblyReportItem = new DataModelAssemblyReportItem();

            //loop through the list of items
            foreach (var item in AssemblyReportItemsList)
            {
                //check if the relationship matches
                if (item.RefSeqAccn == seqAccessionNumber)
                {
                    //return the assigned molecule and the ref seq accn
                    return item.AssignedMolecule;
                }
            }

            //return null if no match is found
            return null;
        }

        #endregion

    }

    //class with a DataModelAssemblyReportItem that is used to store the columns in the report file
    //This file has the following columns:
    //-	Sequence-Name[1]: 1
    //-	Sequence-Role[2]: assembled-molecule
    //-	Assigned-Molecule[3]: 1
    //-	Assigned-Molecule-Location/Type[4]: Chromosome
    //-	GenBank-Accn[5]: CM000663.2
    //-	Relationship[6]: =
    //-	RefSeq-Accn[7]: NC_000001.11
    //-	Assembly-Unit[8]: Primary Assembly
    //-	Sequence-Length[9]: 248956422
    //-	UCSC-style-name[10]: chr1
    // The items are separated by a tab, and all fields may be imported as string (since this is only used for import and not for search, the information is not normalized and only to view the content of the imported file)
    public class DataModelAssemblyReportItem
    {

        #region fields

        /// <summary>
        ///  var for sequence name
        ///  </summary>
        public string SequenceName { get; set; }
        ///                      
        /// <summary>
        ///  var for sequence role
        /// </summary>
        public string SequenceRole { get; set; }
        ///                      
        /// <summary>
        /// var for assigned molecule
        /// Note JCO: the assigned molecule is the chromosome
        /// </summary>
        public string AssignedMolecule { get; set; }
        ///                      
        /// <summary>
        /// var for assigned molecule location type
        /// </summary>
        public string AssignedMoleculeLocationType { get; set; }
        ///                      
        /// <summary>
        /// var for gen bank accession. This field has the accession number that can be matched with the Seqname in the GTF file
        /// </summary>
        public string GenBankAccn { get; set; }
        ///                      
        /// <summary>
        /// var for relationship
        /// </summary>
        public string Relationship { get; set; }
        ///                      
        /// <summary>
        /// var for ref seq accession
        ///</summary>
        public string RefSeqAccn { get; set; }
        ///                      
        /// <summary>
        /// var for assembly unit
        /// </summary>
        public string AssemblyUnit { get; set; }
        ///                      
        /// <summary>
        /// var for sequence length
        /// </summary>
        public string SequenceLength { get; set; }
        ///                      
        /// <summary>
        /// var for UCSC style name
        ///  </summary>
        public string UCSCStyleName { get; set; }
        ///                      
        #endregion

        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelAssemblyReportItem()
        {

        }


        #endregion

    }


}
