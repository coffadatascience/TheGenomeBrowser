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

        /// <summary>
        /// var for start location
        /// </summary>
        public int StartLocation { get; set; }

        /// <summary>
        /// var for end location
        /// </summary>
        public int EndLocation { get; set; }

        #endregion

        #region constructor

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="elementName"></param>
        /// <param name="elementSymbol"></param>
        public void DataModelLookupGeneElementSetupNewItem(string elementName, string elementSymbol, int startLocation, int endLocation)
        {
            //set the id
            Id = Guid.NewGuid();

            //set the element name
            ElementName = elementName;

            //set the element symbol
            ElementSymbol = elementSymbol;

            //set the start location
            StartLocation = startLocation;

            //set the end location
            EndLocation = endLocation;
        }

        #endregion


    }

}
