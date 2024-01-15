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
        public string moleculeChromosome;

        /// <summary>
        /// ref seqa accession number as found in the gtf file (we use this to link the gtf file to the assembly report, e.g. get the chromosome name)
        /// </summary>
        public string refSeqAccenGtf;

        /// <summary>
        /// dictionary with all the Gene id (genes) that are located on the molecule (here the key is the GeneId field)
        /// </summary>
        public Dictionary<string, DataModelGeneId> GeneIds { get; set; }

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
        public DataModelMolecule(string moleculeChromosome, string refSeqAccenGtf)
        {
            //set the molecule chromosome
            this.moleculeChromosome = moleculeChromosome;
            //set the ref seq accession number
            this.refSeqAccenGtf = refSeqAccenGtf;

            //init the dictionary with the seqids
            GeneIds = new Dictionary<string, DataModelGeneId>();

            //init the list of transcripts that have no gene id
            ListOfTranscriptsThatHaveNoGeneId = new List<GTFFeature>();
        }

        #endregion


        #region methods

        /// <summary>
        /// procedure that gets the number of genes on the molecule
        /// </summary>
        /// <returns></returns>
        public int GetNumberOfGenes()
        {
            //return the number of genes
            return GeneIds.Count;
        }

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
        /// procedure that first searches for the gene id and then returns the transcript
        /// </summary>
        /// <param name="seqid"></param>
        /// <param name="geneIdTranscriptId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public DataModelGeneTranscript GetGeneTranscript(string seqid, string geneIdTranscriptId)
        {
            //get the gene id
            var geneId = GetGeneId(seqid);

            //check if the gene id is not null
            if (geneId != null)
            {
                //return the gene id transcript id
                return geneId.GetGeneTranscript(geneIdTranscriptId);
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
