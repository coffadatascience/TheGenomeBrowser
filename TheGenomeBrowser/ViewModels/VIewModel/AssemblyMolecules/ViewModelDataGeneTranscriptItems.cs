using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules
{

    /// <summary>
    /// class used to model for list of and item ViewModelDataGeneTranscriptItem (can have variable forms to minimise the data (e.g. all on a chromsome, all for one gene, etc)
    /// </summary>
    public class ViewModelDataGeneTranscriptItems
    {

        #region fields

        /// <summary>
        /// dictionary with all the items (key is the gene id + transcript id + numerical value of item in the list)
        /// </summary>
        private Dictionary<string, ViewModelDataGeneTranscriptItem> _dictionaryViewModelDataGeneTranscriptItems;

        /// <summary>
        /// list of all the items
        /// </summary>
        public List<ViewModelDataGeneTranscriptItem> _listViewModelDataGeneTranscriptItems;

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataGeneTranscriptItems()
        {
            //init the dictionary
            _dictionaryViewModelDataGeneTranscriptItems = new Dictionary<string, ViewModelDataGeneTranscriptItem>();
        }


        /// <summary>
        /// processes all the assembly sources into a dictionary with all the items (key is the gene id + transcript id + numerical value of item in the list)
        /// </summary>
        /// <param name="assemblySources"></param>
        public void ProcessAssemblySources(List<DataModelAssemblySource> assemblySources)
        {
            //loop the assembly sources
            int entryNumber = 1;

            //loop over all assembly sources
            foreach (var assemblySource in assemblySources)
            {

                //loop all molecules
                foreach (var DicItemMolecule in assemblySource.TheGenome.DictionaryOfMolecules)
                {

                    //loop over all genes
                    foreach (var DicItemGenId in DicItemMolecule.Value.GeneIds)
                    {

                        //loop over all transcripts
                        foreach (var transcript in DicItemGenId.Value.ListGeneTranscripts)
                        {

                            //loop the list of items
                            foreach (var GeneTranscriptElementExon in transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementExon)
                            {

                                //create the key
                                string key = DicItemMolecule.Value.moleculeChromosome + "_" + DicItemGenId.Value.GeneId + "_" + transcript.TranscriptId + "_" + entryNumber.ToString();

                                //check if the item is already in the dictionary
                                if (_dictionaryViewModelDataGeneTranscriptItems.ContainsKey(key))
                                {
                                    //if so, then we have a problem
                                    throw new Exception("Error: the key " + key + " is already in the dictionary");
                                }

                                //var for source
                                string source = assemblySource.SourceName;
                                //var molecule name
                                string moleculeName = DicItemMolecule.Value.moleculeChromosome;
                                //var gene id
                                string geneId = DicItemGenId.Value.GeneId;
                                //var gene name
                                string geneName = DicItemGenId.Value.GeneName;
                                //var transcript id
                                string transcriptId = transcript.TranscriptId;
                                //var start
                                int start = GeneTranscriptElementExon.Start;
                                //var end
                                int end = GeneTranscriptElementExon.End;
                                //var exon number
                                int exonNumber = GeneTranscriptElementExon.ExonNumber;
                                //var product
                                string product = "na";
                                //var protein id
                                string proteinId = "na";

                                //create the item
                                ViewModelDataGeneTranscriptItem item = new ViewModelDataGeneTranscriptItem(source, moleculeName, geneId, geneName, transcriptId, start, end, exonNumber, product, proteinId);

                                //add the item to the dictionary
                                _dictionaryViewModelDataGeneTranscriptItems.Add(key, item);


                                //increase the entry number
                                entryNumber++;
                            }


                        }
                    }
                }

                

            }

            //sort the dictionary
            _dictionaryViewModelDataGeneTranscriptItems = _dictionaryViewModelDataGeneTranscriptItems.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            //create the list
            _listViewModelDataGeneTranscriptItems = _dictionaryViewModelDataGeneTranscriptItems.Values.ToList();

        }




        #endregion


        #region methods


        

        #endregion


    }

    /// <summary>
    /// data we want to use to view the data for a gene transcript item (we want to see the main keys with also start, end, exon number, product, protein id, etc)
    /// </summary>
    public class ViewModelDataGeneTranscriptItem
    {

        #region fields

        //here we want the different items for each item of the transcript (e.g. start, end, exon number, product, protein id, etc) Those that are in the feature type (other than gene and transcript).
        //We also want the Molecule name, gene id and transcript id
        //We also want the source (e.g. NCBI, Ensembl, etc)

        /// <summary>
        /// var for source
        /// </summary>
        public string _source { get; set; }

        /// <summary>
        /// var for molecule name
        /// </summary>
        public string _moleculeName { get; set; }

        /// <summary>
        /// var for gene id
        /// </summary>
        public string _geneId { get; set; }

        /// <summary>
        /// var for gene name
        /// </summary>
        public string _geneName { get; set; }

        /// <summary>
        /// var for transcript id
        /// </summary>
        public string _transcriptId { get; set; }

        /// <summary>
        /// var for start
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// ar for end
        /// </summary>
        public int End { get; set; }

        /// <summary>
        /// var for exon number
        /// </summary>
        public int ExonNumber { get; set; }

        /// <summary>
        /// var for product
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// var for protein id
        /// </summary>
        public string ProteinId { get; set; }

        #endregion

        #region constructors

        /// <summary>
        /// constructor taking all the fields as input
        /// </summary>
        /// <param name="source"></param>
        /// <param name="moleculeName"></param>
        /// <param name="geneId"></param>
        /// <param name="geneName"></param>
        /// <param name="transcriptId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exonNumber"></param>
        /// <param name="product"></param>
        /// <param name="proteinId"></param>
        public ViewModelDataGeneTranscriptItem(string source, string moleculeName, string geneId, string geneName, string transcriptId, int start, int end, int exonNumber, string product, string proteinId)
        {
            _source = source;
            _moleculeName = moleculeName;
            _geneId = geneId;
            _geneName = geneName;
            _transcriptId = transcriptId;
            Start = start;
            End = end;
            ExonNumber = exonNumber;
            Product = product;
            ProteinId = proteinId;
        }


        #endregion

    }

}
