using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;

namespace TheGenomeBrowser.ViewModels.VIewModel.AssemblyMolecules
{

    /// <summary>
    /// This class is specific for the view model that holds the data for the assembly sources So data of different sources may produce a single gene list with all transcripts
    /// ----> This may give better insight in how to create a single gene list with all transcripts, and lower hiearchy levels
    /// </summary>
    public class ViewModelDataAssemblySources
    {


        #region properties


        /// <summary>
        /// var dictionary with GeneId as key and list of DataModelGeneAndAssemblySources items as value. We use dictionaries for sorting and then place them in a list for the View (so we may sort on gene id - > this is therefore only a complete unique list of all geneId entree in all sources used)
        /// </summary>
        public Dictionary<string, ViewModelDataAssemblySourceGene> DictionaryViewModelDataAssemblySourceGenes { get; set; }

        /// <summary>
        /// list of ViewModelDataGeneAndAssemblySources items
        ///  -- > make total list of all items of the DataModelGeneId in all sources
        ///  -- > we want to sort such lists on gene id, so the same gene id is always in the same place and we may compare source information
        /// </summary>
        public List<ViewModelDataAssemblySourceGene> ListViewModelDataAssemblySourceGenes { get; set; }

        #endregion


        #region constructors

        /// <summary>
        /// constructor
        /// </summary>
        public ViewModelDataAssemblySources()
        {
            //init the list
            ListViewModelDataAssemblySourceGenes = new List<ViewModelDataAssemblySourceGene>();

            //init the dictionary
            DictionaryViewModelDataAssemblySourceGenes = new Dictionary<string, ViewModelDataAssemblySourceGene>();

        }

        #endregion


        #region methods


        /// <summary>
        /// procedure that processes the assembly sources into a single list with all genes (so we may compare the different sources). note that we can collect multiple items for one gene so we can print all transcripts.
        /// </summary>
        public void ProcessAssemblySourcesToTotalGeneListDictionary(List<DataModelAssemblySource> assemblySources)
        {

            //clear the dictionary
            DictionaryViewModelDataAssemblySourceGenes = new Dictionary<string, ViewModelDataAssemblySourceGene>();

            //loop the assembly sources
            foreach (var assemblySource in assemblySources)
            {

                //loop all molecules in the Genome
                foreach (var molecule in assemblySource.TheGenome.DictionaryOfMolecules.Values)
                {

                    //loop all gene ids in the molecule
                    foreach (var geneId in molecule.GeneIds.Values)
                    {

                        //check if the gene id is already in the dictionary
                        if (DictionaryViewModelDataAssemblySourceGenes.ContainsKey(geneId.GeneId) == false)
                        { 

                            //New unique gene id found, so add it to the dictionary

                            //create a new ViewModelDataAssemblySourceGene item
                            var viewModelDataAssemblySourceGene = new ViewModelDataAssemblySourceGene(geneId);

                            //add the item to the dictionary
                            DictionaryViewModelDataAssemblySourceGenes.Add(geneId.GeneId, viewModelDataAssemblySourceGene);

                        }
                        else
                        {
                            //gene id already in the dictionary, so add the current source to the list

                            //get the ViewModelDataAssemblySourceGene from the dictionary
                            var viewModelDataAssemblySourceGene = DictionaryViewModelDataAssemblySourceGenes[geneId.GeneId];

                            //new ViewModelDataAssemblySourceGene item
                            viewModelDataAssemblySourceGene.AddNewDataModelGeneIdToList(geneId);


                        }


                    } // end foreach gene id

                }

            } // end foreach assembly source

            //sort the dictionary on gene id
            DictionaryViewModelDataAssemblySourceGenes = DictionaryViewModelDataAssemblySourceGenes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            //place the dictionary in a list so we may use it as a source for the view
            ListViewModelDataAssemblySourceGenes = DictionaryViewModelDataAssemblySourceGenes.Values.ToList();

        }


        #endregion


    }



    

    
}
