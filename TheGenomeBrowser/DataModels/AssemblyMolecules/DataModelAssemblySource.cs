using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.DataModels.AssemblyMolecules
{

    /// <summary>
    /// class that holds a list of the different assemblies that are available
    /// </summary>
    public class DataModelAssemblySourceList
    {

        #region properties

        /// <summary>
        /// list of the different assembly sources
        /// </summary>
        public List<DataModelAssemblySource> ListOfAssemblySources { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public DataModelAssemblySourceList() { }

        #endregion

    }

    /// <summary>
    /// class for that hold the name of the different assembly sources
    /// 20240108 - we will prepare for BestRefSeq, Genomom en curated RefSeq as these fit our needs, but we mya also consider for other sources. Note that these tree sources come out of the annotation file but that only BestRefSeq and Gnomon are complete. Curated is a gene set that is manually curated and is not complete
    ///            These tree source may use the same underlying Data Models as their built up is the ~same.
    /// </summary>
    public class DataModelAssemblySource
    {


        #region properties

        /// <summary>
        /// enum that holds the source type
        /// </summary>
        public SettingsAssemblySource.AssemblySource SourceType { get; set; }


        /// <summary>
        /// dictionary that holds the different molecules (key is the molecule name, value is the molecule)
        /// Note JCO --> we use dictionaries because we want to use the molecule name as a key to quickly find the molecule (this may results in problems for saving)
        /// </summary>
        public Dictionary<string, DataModelMolecule> DictionaryOfMolecules { get; set; }


        #endregion


        #region constructors

        /// <summary>
        /// constructor, that takes the string source name as noted in the file and converts it to the enum
        /// Note JCO --> on scanning the entire file we find the following sources (to understand if they are complete we need to check the file, note also that some are results of analysis, and some only denote transcript or signaling rnas)
        ///     1. {[BestRefSeq. {TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}]}
        //      2. {IGnomon, {TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}])
        //      3. {[Curated Genomic, {TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}]}
        //      4. {[BestRefSeq% 2CGnomon,(TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}]]
        //      5. {(tRNAscan - SE, (TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}]}
        //      6. [[RefSeq, {TheGenomeBrowser.DataModels.AssemblyMolecules.DataModelAssemblySource}1
        //      ---> it would seem true that the first 4 are complete in chromsome count, tRNA is a separate thing and RefSeq seem to be related to MT (>?methionine could also be mitochondrial)
        /// </summary>
        /// <param name="source"></param>
        /// <exception cref="Exception"></exception>
        public DataModelAssemblySource(string source)
        {

            //set the source type
            if (source == SettingsAssemblySource.BESTREFSEQ)
            {
                SourceType = SettingsAssemblySource.AssemblySource.BestRefSeq;
            }
            else if (source == SettingsAssemblySource.GNOMON)
            {
                SourceType = SettingsAssemblySource.AssemblySource.Gnomon;
            }
            else if (source == SettingsAssemblySource.CURATEDREFSEQ)
            {
                SourceType = SettingsAssemblySource.AssemblySource.CuratedRefSeq;
            }
            else
            {
                SourceType = SettingsAssemblySource.AssemblySource.Other;
                //throw new Exception("The source type is not recognized");
            }

            //init the dictionary of molecules
            DictionaryOfMolecules = new Dictionary<string, DataModelMolecule>();

        }

        #endregion



    }


}
