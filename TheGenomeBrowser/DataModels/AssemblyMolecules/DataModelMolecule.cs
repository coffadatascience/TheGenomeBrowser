using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class that holds the data model for a molecule (this is typically a chromosome, but can also be a plasmid or other molecule)
    /// the data model for the molecule contains the list with all the seqids (genes) that are located on the molecule
    /// </summary>
    public class DataModelMolecule
    {

        #region properties

        /// <summary>
        /// the name of the molecule (typically a chromosome)
        /// </summary>
        private string moleculeChromosome;

        /// <summary>
        /// dictionary with all the Gene id (genes) that are located on the molecule (here the key is the GeneId field)
        /// </summary>
        public Dictionary<string, DataModelGeneId> GeneIds { get; set; }

        /// <summary>
        /// dictionary as GeneIds but with the key as the XrefId field (note we keep a second dictionary because gene id doesnt always have a match
        /// </summary>
        public Dictionary<string, DataModelGeneId> XrefIds { get; set; }

        /// <summary>
        /// list of transcripts that have no gene id <--> but do have a molecule id (note that we also have a list for the entire genome that keeps the transcript without a molecule id) (keep GtfFeature) -- > but have the specific feature type transcript
        /// </summary>
        public List<GTFFeature> ListOfTranscriptsThatHaveNoGeneId { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="moleculeChromosome"></param>
        public DataModelMolecule(string moleculeChromosome)
        {
            //set the molecule chromosome
            this.moleculeChromosome = moleculeChromosome;

            //init the dictionary with the seqids
            GeneIds = new Dictionary<string, DataModelGeneId>();

            //init the dictionary with the xref ids
            XrefIds = new Dictionary<string, DataModelGeneId>();

            //init the list of transcripts that have no gene id
            ListOfTranscriptsThatHaveNoGeneId = new List<GTFFeature>();
        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that returns the gene id for a given seqid
        /// </summary>
        /// <param name="seqid"></param>
        /// <returns></returns>
        public DataModelGeneId GetGeneId(string seqid)
        {
            //check if the seqid is in the dictionary
            if (GeneIds.ContainsKey(seqid))
            {
                //return the gene id
                return GeneIds[seqid];
            }
            else
            {
                //return null
                return null;
            }
        }

        /// <summary>
        /// procedure that returns the gene id for a given xref id
        /// </summary>
        /// <param name="xrefId"></param>
        /// <returns></returns>
        public DataModelGeneId GetGeneIdForXrefId(string xrefId)
        {
            //check if the xref id is in the dictionary
            if (XrefIds.ContainsKey(xrefId))
            {
                //return the gene id
                return XrefIds[xrefId];
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
