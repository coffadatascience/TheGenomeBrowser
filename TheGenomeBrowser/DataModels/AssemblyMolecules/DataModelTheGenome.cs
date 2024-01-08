using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// data model class that holds the genome assembly but constructed based on lists of molecules
    /// we build up the data model from the molecules, to the SeqId (gene id), to the individual  entities (exon, itron, etc)
    /// This data model can serve as a base for the other data models, such an investigation (product), where multiple probes may be linked to an individual entity (multiple probes on a exon location)
    /// This subdivision allows a user to have a product and direct link that data to a known and referenced assembly (this allows user to refind data to actual locations on the genome)
    /// Also when data is updated over time, the download may be automated and information may be easier synced (especially when using fact tables and star schemas), where no contaminated data sets exist (such is known for the CDM sheets)
    /// </summary>
    public class DataModelTheGenome
    {


        #region properties

        /// <summary>
        /// var list of molecules (chromosomes, plasmids, etc)
        /// </summary>
        public List<DataModelMolecule> ListOfMolecules { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelTheGenome()
        {
            //init the list of molecules
            ListOfMolecules = new List<DataModelMolecule>();
        }

        #endregion


        #region methods

        //


        #endregion


    }


}
