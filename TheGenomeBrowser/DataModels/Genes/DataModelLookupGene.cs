using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.Genes
{
    /// <summary>
    /// class for the data model look up gene list but now for a single gene
    /// </summary>
    public class DataModelLookupGene()
    {

        #region fields

        /// <summary>
        /// var for id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// var for gene name
        /// </summary>
        public string GeneName { get; set; }

        /// <summary>
        /// var for chromosome
        /// </summary>
        public string Chromosome { get; set; }

        /// <summary>
        /// var for start position
        /// </summary>
        public int StartPosition { get; set; }

        /// <summary>
        /// var for end position
        /// </summary>
        public int EndPosition { get; set; }

        /// <summary>
        /// var with the list of elements that are part of the gene (note JCO--> we may also consider using a dictionary here, where position is the key and the element is the value) Whether a dictionary works, depends on storage and retrieval.
        /// </summary>
        public List<DataModelLookupGeneElement> GeneElements { get; set; }

        #endregion


        #region constructor

        /// <summary>
        /// passes for failed constructor, setups a new item
        /// 
        public void DataModelLookupGeneSetupNewItem(string geneName, string chromosome, int StartLocation, int EndLocation)
        {
            Id = Guid.NewGuid();
            GeneElements = new List<DataModelLookupGeneElement>();

            // set the other fields
            GeneName = geneName;
            Chromosome = chromosome;
            StartPosition = StartLocation;
            EndPosition = EndLocation;
        }


        #endregion

    }

}
