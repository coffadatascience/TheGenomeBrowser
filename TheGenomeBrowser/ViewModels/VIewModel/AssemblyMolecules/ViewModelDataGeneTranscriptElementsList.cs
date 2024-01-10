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
    public class ViewModelDataGeneTranscriptElementsList
    {

        #region fields

        /// <summary>
        /// dictionary with all the items (key is the gene id + transcript id + numerical value of item in the list)
        /// </summary>
        private Dictionary<string, ViewModelDataGeneTranscriptElement> _dictionaryViewModelDataGeneTranscriptElements;

        /// <summary>
        /// list of all the items
        /// </summary>
        public List<ViewModelDataGeneTranscriptElement> _listElements;

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataGeneTranscriptElementsList()
        {
            //init the dictionary
            _dictionaryViewModelDataGeneTranscriptElements = new Dictionary<string, ViewModelDataGeneTranscriptElement>();
            //init the list
            _listElements = new List<ViewModelDataGeneTranscriptElement>();
        }


        /// <summary>
        /// processes all the assembly sources into a dictionary with all the items (key is the gene id + transcript id + numerical value of item in the list)
        /// The bool may be used to include/exclude the exon liss, the second bool may be used to include/exclude the CDS list (note that the CDS should also include start and stop items)
        /// </summary>
        /// <param name="assemblySources"></param>
        /// <param name="includeExonList"></param>
        /// <param name="includeCDSlist"></param>
        /// <exception cref="Exception"></exception>
        public void ProcessAssemblySources(List<DataModelAssemblySource> assemblySources, bool includeExonList, bool includeCDSlist)
        {

            //clear the dictionary
            _dictionaryViewModelDataGeneTranscriptElements.Clear();
            //clear the list
            _listElements.Clear();

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

                            //-------------------------------------------
                            // 1. check if we should add the exon list
                            //-------------------------------------------
                            if (includeExonList == true)
                            {
                                //key for exon list
                                int EntreeNumberExon = 0;

                                //loop the list of items
                                foreach (var GeneTranscriptElementExon in transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementExon)
                                {

                                    //var that denotes EntreeNumberExon with 3 digits
                                    string EntreeNumberExonString = EntreeNumberExon.ToString("D3");

                                    //create the key
                                    string key = DicItemMolecule.Value.moleculeChromosome + "_" + DicItemGenId.Value.GeneId + "_" + transcript.TranscriptId +  "_exon (n-" + EntreeNumberExonString + ")";

                                    //check if the item is already in the dictionary
                                    if (_dictionaryViewModelDataGeneTranscriptElements.ContainsKey(key))
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
                                    //var exon number (also used in key)
                                    int exonNumber = GeneTranscriptElementExon.ExonNumber;

                                    //var product
                                    string product = "na";
                                    //var protein id
                                    string proteinId = "na";

                                    //create the item
                                    ViewModelDataGeneTranscriptElement item = new ViewModelDataGeneTranscriptElement(key, source, moleculeName, geneId, geneName, transcriptId, start, end, exonNumber, product, proteinId);

                                    //add the item to the dictionary
                                    _dictionaryViewModelDataGeneTranscriptElements.Add(key, item);

                                    //count one up
                                    EntreeNumberExon++;
                                }

                            }
                            //-------------------------------------------


                            //-------------------------------------------
                            // 2. check if we should add the CDS list
                            //-------------------------------------------
                            if (includeCDSlist == true)
                            {

                                //-------------------------------------------
                                // 2a. create entree for the start codon
                                //-------------------------------------------

                                //note that we count start codon as EntreeNumberCDS = 0 and end codon as EntreeNumberCDS +1
                                //note that in the CDS the exon numbering may nog be unique (so for the key we use a counter)
                                int EntreeNumberCDS = 0;
                                //string that denotes EntreeNumberCDS with 3 digits
                                string EntreeNumberCDSString = EntreeNumberCDS.ToString("D3");
                                //create the key
                                string keyStartCodon = DicItemMolecule.Value.moleculeChromosome + "_" + DicItemGenId.Value.GeneId + "_" + transcript.TranscriptId + "_(CDS_entree nr. " + EntreeNumberCDSString + " - start)";
                                //count one up
                                EntreeNumberCDS++;

                                //check if the item is already in the dictionary
                                if (_dictionaryViewModelDataGeneTranscriptElements.ContainsKey(keyStartCodon))
                                {
                                    //if so, then we have a problem
                                    throw new Exception("Error: the key " + keyStartCodon + " is already in the dictionary");
                                }

                                //var for source
                                string sourceStartCodon = assemblySource.SourceName;
                                //var molecule name
                                string moleculeNameStartCodon = DicItemMolecule.Value.moleculeChromosome;
                                //var gene id
                                string geneIdStartCodon = DicItemGenId.Value.GeneId;
                                //var gene name
                                string geneNameStartCodon = DicItemGenId.Value.GeneName;
                                //var transcript id
                                string transcriptIdStartCodon = transcript.TranscriptId;
                                //var start
                                int startStartCodon = -1;
                                //var end
                                int endStartCodon = -1;
                                //var exon number
                                int exonNumberStartCodon = -1;
                                //var product
                                string productStartCodon = "na";
                                //var protein id
                                string proteinIdStartCodon = "na";

                                //set start codon if there is one
                                if (transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon != null)
                                {

                                    //add tag to the source line (start codon found)
                                    sourceStartCodon = sourceStartCodon + " (start codon found)";
                                    //set exon number
                                    exonNumberStartCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon.Exon;

                                    //set
                                    startStartCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon.Start;
                                    endStartCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon.End;
                                }
                                else
                                {
                                    //overwrite source line with a note that there is no start codon in the data
                                    sourceStartCodon = "no start codon in data";
                                }

                                //create the item
                                ViewModelDataGeneTranscriptElement itemStartCodon = new ViewModelDataGeneTranscriptElement(keyStartCodon, sourceStartCodon, moleculeNameStartCodon, geneIdStartCodon, geneNameStartCodon, transcriptIdStartCodon, startStartCodon, endStartCodon, exonNumberStartCodon, productStartCodon, proteinIdStartCodon);

                                //add the item to the dictionary
                                _dictionaryViewModelDataGeneTranscriptElements.Add(keyStartCodon, itemStartCodon);
                                //-------------------------------------------


                                //-------------------------------------------
                                // 2b. Process the list of CDS items
                                //-------------------------------------------


                                //loop the list of items
                                foreach (var GeneTranscriptElementCDS in transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementCDS)
                                {

                                    //var that denotes EntreeNumberCDS with 3 digits
                                    EntreeNumberCDSString = EntreeNumberCDS.ToString("D3");

                                    //create the key
                                    string key = DicItemMolecule.Value.moleculeChromosome + "_" + DicItemGenId.Value.GeneId + "_" + transcript.TranscriptId + "_(CDS_entree nr. " + EntreeNumberCDSString + ")";

                                    //check if the item is already in the dictionary
                                    if (_dictionaryViewModelDataGeneTranscriptElements.ContainsKey(key))
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
                                    int start = GeneTranscriptElementCDS.Start;
                                    //var end
                                    int end = GeneTranscriptElementCDS.End;
                                    //var exon number
                                    int exonNumber = GeneTranscriptElementCDS.ExonNumber;
                                    //var product
                                    string product = GeneTranscriptElementCDS.Product;
                                    //var protein id
                                    string proteinId = GeneTranscriptElementCDS.ProteinId;


                                    //create the item
                                    ViewModelDataGeneTranscriptElement item = new ViewModelDataGeneTranscriptElement(key, source, moleculeName, geneId, geneName, transcriptId, startStartCodon, endStartCodon, exonNumber, product, proteinId);

                                    //add the item to the dictionary
                                    _dictionaryViewModelDataGeneTranscriptElements.Add(key, item);

                                    EntreeNumberCDS++;
                                }
                                //-------------------------------------------

                                //-------------------------------------------
                                // 2c. create entree for the stop codon
                                //-------------------------------------------

                                //note that the last of the loop counts one for the end codon
                                //var that denotes EntreeNumberCDS with 3 digits
                                EntreeNumberCDSString = EntreeNumberCDS.ToString("D3");

                                //create the key
                                string keyStopCodon = DicItemMolecule.Value.moleculeChromosome + "_" + DicItemGenId.Value.GeneId + "_" + transcript.TranscriptId + "_(CDS_entree nr. " + EntreeNumberCDSString + " - end)";

                                //check if the item is already in the dictionary
                                if (_dictionaryViewModelDataGeneTranscriptElements.ContainsKey(keyStopCodon))
                                {
                                    //if so, then we have a problem
                                    throw new Exception("Error: the key " + keyStopCodon + " is already in the dictionary");
                                }

                                //var for source
                                string sourceStopCodon = assemblySource.SourceName;
                                //var molecule name
                                string moleculeNameStopCodon = DicItemMolecule.Value.moleculeChromosome;
                                //var gene id
                                string geneIdStopCodon = DicItemGenId.Value.GeneId;
                                //var gene name
                                string geneNameStopCodon = DicItemGenId.Value.GeneName;
                                //var transcript id
                                string transcriptIdStopCodon = transcript.TranscriptId;
                                //var start
                                int startStopCodon = -1;
                                //var end
                                int endStopCodon = -1;
                                //var exon number
                                int exonNumberStopCodon = -1;
                                //var product
                                string productStopCodon = "na";
                                //var protein id
                                string proteinIdStopCodon = "na";

                                //set start codon if there is one
                                if (transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon != null)
                                {

                                    //add tag to the source line (stop codon found)
                                    sourceStopCodon = sourceStopCodon + " (stop codon found)";
                                    //set exon number
                                    exonNumberStopCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon.Exon;

                                    //set
                                    startStopCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon.Start;
                                    endStopCodon = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon.End;
                                }
                                else
                                {
                                    //overwrite source line with a note that there is no start codon in the data
                                    //overwrite source line with a note that there is no start codon in the data
                                    sourceStopCodon = "no end codon in data";
                                }

                                //create the item
                                ViewModelDataGeneTranscriptElement itemStopCodon = new ViewModelDataGeneTranscriptElement(keyStopCodon, sourceStopCodon, moleculeNameStopCodon, geneIdStopCodon, geneNameStopCodon, transcriptIdStopCodon, startStopCodon, endStopCodon, exonNumberStopCodon, productStopCodon, proteinIdStopCodon);

                                //add the item to the dictionary
                                _dictionaryViewModelDataGeneTranscriptElements.Add(keyStopCodon, itemStopCodon);
                                //-------------------------------------------

                            }

                        }
                    }
                }

                

            }

            //sort the dictionary
            _dictionaryViewModelDataGeneTranscriptElements = _dictionaryViewModelDataGeneTranscriptElements.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            //create the list
            _listElements = _dictionaryViewModelDataGeneTranscriptElements.Values.ToList();

        }




        #endregion


        #region methods


        

        #endregion


    }

    /// <summary>
    /// data we want to use to view the data for a gene transcript item (we want to see the main keys with also start, end, exon number, product, protein id, etc)
    /// </summary>
    public class ViewModelDataGeneTranscriptElement
    {

        #region fields

        //here we want the different items for each item of the transcript (e.g. start, end, exon number, product, protein id, etc) Those that are in the feature type (other than gene and transcript).
        //We also want the Molecule name, gene id and transcript id
        //We also want the source (e.g. NCBI, Ensembl, etc)

        /// <summary>
        /// add also the key in the view model (this is the key that is used in the dictionary)
        /// </summary>
        public string KeyElement { get; set; }

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
        public ViewModelDataGeneTranscriptElement(string key, string source, string moleculeName, string geneId, string geneName, string transcriptId, int start, int end, int exonNumber, string product, string proteinId)
        {
            //set the key
            KeyElement = key;
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
