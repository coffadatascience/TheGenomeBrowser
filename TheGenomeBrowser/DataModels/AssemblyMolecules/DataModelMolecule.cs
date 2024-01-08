using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// dictionary with all the seqids (genes) that are located on the molecule
        /// </summary>
        public Dictionary<string, DataModelGeneId> GeneIds { get; set; }


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
        }

        #endregion


        #region methods


        #endregion



    }

}
