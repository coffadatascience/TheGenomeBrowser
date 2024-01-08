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
        public DataModelAssemblySourceList() 
        { 
            //init the list
            ListOfAssemblySources = new List<DataModelAssemblySource>();
        }

        #endregion

        #region methods
         
        /// <summary>
        /// procedure that loops all the sources to get a gene id (take a molecule name and a gene id and return the gene id)
        /// Note that this procedure exists to always get a gene id, but also to see how the different sources relate to each other
        /// </summary>
        /// <param name="moleculeName"></param>
        /// <param name="geneId"></param>
        /// <returns></returns>
        public DataModelGeneId ReturnGeneId(string moleculeName, string geneId)
        {
            //loop the list of sources
            foreach (var DataModelAssemblySource in this.ListOfAssemblySources)
            {
                //get the molecule
                var molecule = DataModelAssemblySource.TheGenome.GetMolecule(moleculeName);

                //check if the molecule is not null
                if (molecule != null)
                {
                    //get the gene id
                    var geneIdToReturn = molecule.GetGeneId(geneId);

                    //check if the gene id is not null
                    if (geneIdToReturn != null)
                    {
                        //return the gene id
                        return geneIdToReturn;
                    }
                }
            }

            //return null
            return null;
        }

        /// <summary>
        /// procedure that loops the list of source and return the proper list based on the provided enum type
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public DataModelAssemblySource ReturnDataModelAssemblySource(SettingsAssemblySource.AssemblySource sourceType)
        {
            //loop list
            foreach (var DataModelAssemblySource in this.ListOfAssemblySources)
            {
                //match enum
                if (DataModelAssemblySource.SourceType == sourceType) return DataModelAssemblySource;
            }

            //return null
            return null;
        }

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
        /// Data Model of the Genome for this source based on annotation file (note that we may get other data models from this source that may be in this class
        /// </summary>
        public DataModelTheGenome TheGenome { get; set; }


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

            //get set the source type
            SourceType = SettingsAssemblySource.ReturnSourceEnumByString(source);

            //list of the genomes
            TheGenome = new DataModelTheGenome();
        }

        #endregion


        #region methods


 


        #endregion

    }


}
