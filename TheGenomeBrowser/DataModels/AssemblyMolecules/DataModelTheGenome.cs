using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// data model class that holds the genome assembly but constructed based on lists of molecules
    /// we build up the data model from the molecules, to the SeqId (gene id), to the individual  entities (exon, itron, etc)
    /// This data model can serve as a base for the other data models, such an investigation (product), where multiple probes may be linked to an individual entity (multiple probes on a exon location)
    /// This subdivision allows a user to have a product and direct link that data to a known and referenced assembly (this allows user to refind data to actual locations on the genome)
    /// Also when data is updated over time, the download may be automated and information may be easier synced (especially when using fact tables and star schemas), where no contaminated data sets exist (such is known for the CDM sheets)
    /// </summary>
    public class DataModelTheGenome
    {


        #region properties


        /// <summary>
        /// dictionary that holds the different molecules (key is the molecule name, value is the molecule)
        /// Note JCO --> we use dictionaries because we want to use the molecule name as a key to quickly find the molecule (this may results in problems for saving)
        /// </summary>
        public Dictionary<string, DataModelMolecule> DictionaryOfMolecules { get; set; }

        /// <summary>
        /// list of Line feeds of the GTF file that could not be identified in the annotation report file (for correct processing of the GTF file)
        /// TODO JCO --> implement this in the code in the future in the reader (so we ensure we have all lines processed)
        /// </summary>
        public List<string> ListOfLineFeedsOfGtfFileThatCouldNotBeIdentifiedInAnnotationReportFile { get; set; }

        /// <summary>
        /// list of GeneId of which we have no information to link to a molecule (keep GtfFeature)
        /// </summary>
        public List<GTFFeature> ListOfGeneIdsWithNoMolecule { get; set; }

        /// <summary>
        /// list of transcripts that have no molecule or gene id (keep GtfFeature) -- > but have the specific feature type transcript
        /// </summary>
        public List<GTFFeature> ListOfTranscriptsThatHaveNoMoleculeOrGeneId { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelTheGenome()
        {
            //init the dictionary of molecules
            DictionaryOfMolecules = new Dictionary<string, DataModelMolecule>();

            //init the list of line feeds of the GTF file that could not be identified in the annotation report file
            ListOfLineFeedsOfGtfFileThatCouldNotBeIdentifiedInAnnotationReportFile = new List<string>();

            //init the list of gene ids with no molecule
            ListOfGeneIdsWithNoMolecule = new List<GTFFeature>();

            //init the list of transcripts that have no molecule or gene id
            ListOfTranscriptsThatHaveNoMoleculeOrGeneId = new List<GTFFeature>();


        }

        #endregion

        #region methods



        /// <summary>
        /// procedure that returns the molecule with the given name
        /// </summary>
        /// <param name="moleculeName"></param>
        /// <returns></returns>
        public DataModelMolecule GetMolecule(string moleculeName)
        {
            //check if the molecule is in the dictionary
            if (DictionaryOfMolecules.ContainsKey(moleculeName))
            {
                //return the molecule
                return DictionaryOfMolecules[moleculeName];
            }
            else
            {
                //return null
                return null;
            }
        }

        /// <summary>
        /// procedure that take a Molecule name and a gene id and returns the gene id
        /// </summary>
        /// <param name="moleculeName"></param>
        /// <param name="geneId"></param>
        /// <returns></returns>
        public DataModelGeneId GetGeneId(string moleculeName, string geneId)
        {
            //get the molecule
            var molecule = GetMolecule(moleculeName);

            //check if the molecule is not null
            if (molecule != null)
            {
                //return the gene id
                return molecule.GetGeneId(geneId);
            }
            else
            {
                //return null
                return null;
            }
        }


        #endregion




    }


}
