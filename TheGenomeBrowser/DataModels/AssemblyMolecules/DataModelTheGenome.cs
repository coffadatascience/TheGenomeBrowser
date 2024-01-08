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
        /// dictionary that holds the different molecules (key is the molecule name, value is the molecule)
        /// Note JCO --> we use dictionaries because we want to use the molecule name as a key to quickly find the molecule (this may results in problems for saving)
        /// </summary>
        public Dictionary<string, DataModelMolecule> DictionaryOfMolecules { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelTheGenome()
        {
            //init the dictionary of molecules
            DictionaryOfMolecules = new Dictionary<string, DataModelMolecule>();
        }

        #endregion




        #region methods

        /// <summary>
        /// procedure that returns the molecule with the given name
        /// </summary>
        /// <param name="moleculeName"></param>
        /// <returns></returns>
        public DataModelMolecule GetMolecule(string moleculeName)
        {
            //check if the molecule is in the dictionary
            if (DictionaryOfMolecules.ContainsKey(moleculeName))
            {
                //return the molecule
                return DictionaryOfMolecules[moleculeName];
            }
            else
            {
                //return null
                return null;
            }
        }


        #endregion




    }


}
