using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.Genes
{


    /// <summary>
    /// class for the elements that are part of a gene (names as element)
    /// this is typically a list of all exon information but may also contain different elements such as introns, UTRs, etc
    /// </summary>
    public class DataModelLookupGeneElement()
    {

        #region fields

        /// <summary>
        /// var for id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// var for element name
        /// </summary>
        public string ElementName { get; set; }

        /// <summary>
        /// var for element symbol
        /// </summary>
        public string ElementSymbol { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// constructor
        /// </summary>


        #endregion


    }

}
