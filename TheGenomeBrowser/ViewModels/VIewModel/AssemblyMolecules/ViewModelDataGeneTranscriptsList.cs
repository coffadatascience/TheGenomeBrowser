
using TheGenomeBrowser.DataModels.AssemblyMolecules;

/// <summary>
/// view model class that represent the data for the view model class ViewModelDataGeneTranscript. This view model is used to create a list of all unique transcripts (currently 20240108 with decent source of the annotation file about 60000 genes are listed over 5 different sources)
/// we use this view model to create a list of all unique transcripts 
/// </summary>
public class ViewModelDataGeneTranscriptsList
{

    #region fields

    /// <summary>
    /// dictionary that represent the list of all unique transcripts (we will use the GeneIdTranscriptId as key)
    /// </summary>
    public Dictionary<string, ViewModelDataGeneTranscript> DictionaryViewModelDataGeneTranscripts { get; set; }

    /// <summary>
    /// list of all unique transcripts (we use this list for the data grid view)
    /// </summary>
    public List<ViewModelDataGeneTranscript> ListViewModelDataGeneTranscriptsList { get; set; }

    #endregion


    #region constructors

    /// <summary>
    /// constructor
    /// </summary>
    public ViewModelDataGeneTranscriptsList()
    {
        //create the dictionary
        DictionaryViewModelDataGeneTranscripts = new Dictionary<string, ViewModelDataGeneTranscript>();

        //create the list
        ListViewModelDataGeneTranscriptsList = new List<ViewModelDataGeneTranscript>();

    }

    #endregion

    #region methods

    /// <summary>
    /// procedure that takes List<DataModelAssemblySource> assemblySources as input and create the ListViewModelDataGeneTranscriptItems of all unique transcripts by using the GeneIdTranscriptId as key and all ViewModelDataGeneTranscriptItem as value
    /// <summary>
    public void ProcessAssemblySourcesToTotalGeneTranscriptListDictionary(List<DataModelAssemblySource> assemblySources, IProgress<int> progress)
    {

        //init the progress
        progress.Report(0);
        //init the counter
        int counter = 0;
        //init the number of features update
        int numberOfFeaturesUpdate = 0;

        //get total number of transcripts
        int totalNumberOfTranscripts = assemblySources.Sum(x => x.TheGenome.DictionaryOfMolecules.Sum(y => y.Value.GeneIds.Sum(z => z.Value.ListGeneTranscripts.Count)));

        //loop over all assembly sources
        foreach (DataModelAssemblySource assemblySource in assemblySources)
        {

            // loop all molecules
            foreach (var DicItemMolecule in assemblySource.TheGenome.DictionaryOfMolecules)
            {
                //loop over all genes
                foreach (var DicItemGenId in DicItemMolecule.Value.GeneIds)
                {
                    //loop over all transcripts
                    foreach (var transcript in DicItemGenId.Value.ListGeneTranscripts)
                    {
                        //create the key
                        string key = DicItemGenId.Value.GeneId + " - " + transcript.TranscriptId;

                        //check if the key is already in the dictionary
                        if (DictionaryViewModelDataGeneTranscripts.ContainsKey(key) == true)
                        {
                            //increase the number of transcripts
                            DictionaryViewModelDataGeneTranscripts[key].NumberOfTranscripts++;

                            //throw a message to the debug window
                            //System.Diagnostics.Debug.WriteLine("!!!UNEXPECTED!!! ViewModelDataGeneTranscripts.ProcessAssemblySourcesToTotalGeneTranscriptListDictionary: key already in dictionary: " + key);

                        }
                        else
                        {
                            //create the new ViewModelDataGeneTranscriptItem
                            ViewModelDataGeneTranscript viewModelDataGeneTranscriptItem = new ViewModelDataGeneTranscript();

                            //set the GeneIdTranscriptId
                            viewModelDataGeneTranscriptItem.GeneIdTranscriptId = key;

                            //set the GeneId
                            viewModelDataGeneTranscriptItem.GeneId = transcript.GeneId;

                            //set the TranscriptId
                            viewModelDataGeneTranscriptItem.TranscriptId = transcript.TranscriptId;

                            //source name is set from the assemblySource
                            viewModelDataGeneTranscriptItem.SourceName = assemblySource.SourceName;

                            //set the GeneName
                            viewModelDataGeneTranscriptItem.GeneName = transcript.GeneName;

                            //set the SourceType
                            viewModelDataGeneTranscriptItem.SourceType = "Transcript";

                            //set the MoleculeName
                            viewModelDataGeneTranscriptItem.MoleculeName = DicItemMolecule.Value.moleculeChromosome;

                            //set the DbRefXref
                            viewModelDataGeneTranscriptItem.DbRefXref = transcript.Db_Xref;

                            //set the TranscriptBiotype
                            viewModelDataGeneTranscriptItem.TranscriptBiotype = transcript.Transcript_Biotype;

                            //set the NumberOfTranscripts
                            viewModelDataGeneTranscriptItem.NumberOfTranscripts = 1;

                            //set the NumberOfExons -- > inner list
                            viewModelDataGeneTranscriptItem.NumberOfExons = transcript.GeneTranscriptObject.NumberOfExons; 

                            //add the new ViewModelDataGeneTranscriptItem to the dictionary
                            DictionaryViewModelDataGeneTranscripts.Add(key, viewModelDataGeneTranscriptItem);


                        }


                        //check if the progress is not null
                        if (progress != null)
                        {

                            //check if we need to update the progress
                            if (numberOfFeaturesUpdate > 100)
                            {
                                //reset the number of features update
                                numberOfFeaturesUpdate = 0;

                                //var for the progress percentage in double
                                double progressPercentageDouble = ((double)counter / (double)totalNumberOfTranscripts) * 100;

                                //calculate the progress
                                int progressPercentage = (int)Math.Round(progressPercentageDouble);

                                //report the progress
                                progress.Report(progressPercentage);


                            }
                        }

                        //increase the counter
                        counter++;
                    }

                }

            }

         
        }

        //sort the dictionary
        DictionaryViewModelDataGeneTranscripts = DictionaryViewModelDataGeneTranscripts.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        //create the list
        ListViewModelDataGeneTranscriptsList = DictionaryViewModelDataGeneTranscripts.Values.ToList();

    }

    #endregion


}

/// <summary>
/// class that represent the data for the view model class ViewModelDataGeneTranscriptItem. This view model is used to create a list of all unique transcripts (currently 20240108 with decent source of the annotation file about 60000 genes are listed over 5 different sources)
/// </summary>
public class ViewModelDataGeneTranscript
{


    #region fields

    /// <summary>
    /// Unique GeneId combined with TranscriptId field
    /// </summary>
    public string GeneIdTranscriptId { get; set; }

    /// <summary>
    /// field that represent the gene id
    /// </summary>
    public string GeneId { get; set; }

    /// <summary>
    /// var fpr transcript id
    /// </summary>
    public string TranscriptId { get; set; }

    /// <summary>
    /// Source name --> relates to the data source name
    /// </summary>
    public string SourceName { get; set; }

    /// <summary>
    /// field that represent the transcript id
    /// </summary>
    public string SourceType { get; set; }

    /// <summary>
    /// var for molecule name
    /// </summary>
    public string MoleculeName { get; set; }


    /// <summary>
    /// field that represent the gene name
    /// </summary>
    public string GeneName { get; set; }

    /// <summary>
    /// variable that represent the DbRefXref
    /// </summary>
    public string DbRefXref { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string TranscriptBiotype { get; set; }

    /// <summary>
    /// field that represent the number of transcripts
    /// </summary>
    public int NumberOfTranscripts { get; set; }

    /// <summary>
    /// variable that represent the number of exons
    /// </summary>
    public int NumberOfExons { get; set; }

    /// <summary>
    /// variable that represent the number of entrees total for a exon
    /// </summary>
    public int TotalNumberOfEntriesForExon { get; set; }

    #endregion

    #region constructors



    #endregion

}
