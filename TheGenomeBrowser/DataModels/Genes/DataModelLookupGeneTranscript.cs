using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.Genes
{

    /// <summary>
    /// class that represent a single transcript of a gene (a gene can have multiple transcripts)
    /// Each transcript may have a list with multiple exons and introns, representing the the GeneElement data model
    /// 
    /// </summary>
    public class DataModelLookupGeneTranscript
    {


        /// <summary>
        /// list of gene elements (exons, introns, etc) that are located on the transcript
        /// </summary>
        public List<DataModelLookupGeneElement> ListOfGeneElements { get; set; }

    }
}
