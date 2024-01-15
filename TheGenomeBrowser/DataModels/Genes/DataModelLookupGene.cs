using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.Genes
{
    /// <summary>
    /// NOTE JCO -- > these classes are under construction, they are not yet used in the application (could be for future use of quick look up of genes)
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
        /// var that returns the location start of the gene by looking at the inner elements list and returning the lowest start location
        /// </summary>
        public int LocationStart
        {
            get
            {
                // return the lowest start location
                return Elements.Min(x => x.Value.StartLocation);
            }
        }

        /// <summary>
        /// var that returns the location end of the gene by looking at the inner elements list and returning the highest end location
        /// </summary>
        public int LocationEnd
        {
            get
            {
                // return the highest end location
                return Elements.Max(x => x.Value.EndLocation);
            }
        }

        /// <summary>
        /// var that returns the number of unique elements in the gene
        /// </summary>
        public int NumberOfUniqueElements
        {
            get
            {
                // return the number of unique elements
                return UniqueElements.Count;
            }
        }

        /// <summary>
        /// var that returns a concentation of the text in the first and last unique element
        /// </summary>
        public string FirstAndLastUniqueElements
        {
            get
            {
                //check if there are any unique elements
                if (UniqueElements.Count == 0)
                {
                    //return empty string
                    return "";
                }

                // return the first and last unique elements
                return UniqueElements.First().Value + " - " + UniqueElements.Last().Value;
            }
        }

        /// <summary>
        /// var that returns the number of elements in the gene
        /// </summary>
        public int NumberOfElements
        {
            get
            {
                // return the number of elements
                return Elements.Count;
            }
        }

        /// <summary>
        /// var for GenBankAccn (assembly used to retrieve the chromosome number for the gene)
        /// </summary>
        public string GenBankAccn { get; set; }

        /// <summary>
        /// var for alternative accesion numbers
        /// </summary>
        public string AlternativeAccn { get; set; }

        /// <summary>
        /// dictionary that collects the unique elements (these removes duplicates of the same exon, intron, etc)
        /// the unique element here will be the position, as such we may sort and get the start and end of the gene (as well as a value that indicates the type of element)
        /// Location start + Type and exon number (if available) will be the unique key
        /// </summary>
        public Dictionary<int, string> UniqueElements { get; set; }

        /// <summary>
        /// dictionary with chr plus - location start of an element and the object gene element
        /// </summary>
        public Dictionary<string, DataModelLookupGeneElement> Elements { get; set; }

        #endregion


        #region constructor

        /// <summary>
        /// passes for failed constructor, setups a new item
        /// 
        public void DataModelLookupGeneSetupNewItem(string geneName, string chromosome, string genBankAccn)
        {
            Id = Guid.NewGuid();
            Elements = new Dictionary<string, DataModelLookupGeneElement>();
            UniqueElements = new Dictionary<int, string>();


            // set the other fields
            GeneName = geneName;
            Chromosome = chromosome;
            GenBankAccn = genBankAccn;
        }


        #endregion

    }

}
