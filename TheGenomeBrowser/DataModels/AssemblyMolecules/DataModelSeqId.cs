using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class that hold the information for a seqid (gene id)
    /// a seq id is a unique identifier for usually a gene that may have its own accessions and other information
    /// a seq id may have a list of entities (exons, introns, etc) that are located on the seq id
    /// </summary>
    public class DataModelSeqId
    {

        #region properties

        /// <summary>
        /// list of entities (exons, introns, etc) that are located on the seq id
        /// </summary>
        public List<DataModelSeqElement> ListOfSeqElements { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelSeqId()
        {
            //init the list of seq elements
            ListOfSeqElements = new List<DataModelSeqElement>();
        }

        #endregion


        #region methods


        #endregion


    }


}
