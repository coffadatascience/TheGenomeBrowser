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
        /// list of seqids (genes) that are located on the molecule
        /// </summary>
        public List<DataModelSeqId> ListOfSeqIds { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelMolecule()
        {
            //init the list of seqids
            ListOfSeqIds = new List<DataModelSeqId>();
        }

        #endregion


        #region methods


        #endregion



    }

}
