using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.Genes
{

    /// <summary>
    /// class that takes and extract of different genomic data bases (GTF, GFF3, sequence, etc) and provides it in such a way that a user may easily access the data
    /// - this class may therefore be used to supplement data results models with additional data (such as location, sequence, etc)
    /// - it may also be used to build an investigation tool that allows a user to explore the data (that may also function as a frame for a product) that is more stable that a product sheet or content of a mix
    ///   --> rather it focuses on the intended purposes of a product which is quite stable over time as the number of diseases and genomic aberrations are limited and the related genomic alteration are generally already known)
    ///   --> this may remove the difficulty of managing results over longer periods of time and the every changing product content (even if only by a few probes)
    /// </summary>
    public class DataModelLookupGeneList
    {

        #region fields

        /// <summary>
        /// var with a dictionary of genes based on a unique string name and the data model for the gene
        /// </summary>
        public Dictionary<string, DataModelLookupGene> Genes { get; set; }

        #endregion


        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelLookupGeneList()
        {
            Genes = new Dictionary<string, DataModelLookupGene>();
        }

        #endregion


    }


   
}
