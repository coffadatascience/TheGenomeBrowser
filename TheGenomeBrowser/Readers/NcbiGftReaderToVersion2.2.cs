using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TheGenomeBrowser.Readers
{
    public class NcbiGftReaderToVersion2
    {


    }

    /// <summary>
    /// class to read GFF3 files
    /// </summary>
    public class GTFReader
    {


        #region properties


    

        #endregion


        #region constructor

 
        #endregion


        #region methods


        /// <summary>
        /// read GTF file async (note that the original void procedure is kept for reference below this procedure)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public async Task<DataModelGtfFile> ReadGFF3Async(string filePath, IProgress<int> progress)
        {

            //var for the data model
            DataModelGtfFile DataModelGtfFile = new DataModelGtfFile();

            //read GTF file async
            await Task.Run(() =>
            {
                //read GTF file
                DataModelGtfFile = ReadGFF3(filePath, progress);

            });

            //return the data model
            return DataModelGtfFile;

         
        }


        /// <summary>
        /// parse a GFF3 file
        /// #gtf-version 2.2
        //  #!genome-build GRCh38.p13
        //  #!genome-build-accession NCBI_Assembly:GCF_000001405.39
        //  #!annotation-date 11/19/2021
        /// </summary>
        /// <param name="filePath"></param>
        public DataModelGtfFile ReadGFF3(string filePath, IProgress<int> progress)
        {
            //create a new GFF3Reader
            var DataModelGtfFile = new TheGenomeBrowser.DataModels.NCBIImportedData.DataModelGtfFile();

            //local var for line counter
            int LineCounter = 0;
            //counter that counts for progress (every 1000 lines)
            int ProgressCounter = 0;

            //get total number of lines in the file
            int TotalNumberOfLines = File.ReadLines(filePath).Count();

            //read the file line by line
            foreach (string line in File.ReadLines(filePath))
            {
                //skip empty lines
                if (line.StartsWith("#"))
                {
                    continue; // Skip comments
                }

                //split the line into fields
                string[] fields = line.Split('\t');
                string seqname = fields[0];
                string source = fields[1];
                string featureType = fields[2];
                int start = int.Parse(fields[3]);
                int end = int.Parse(fields[4]);
                string score = fields[5];
                string strand = fields[6];
                string frame = fields[7];
                string[] attributePairs = fields[8].Split(';');

                //create a new GFF3Feature
                GTFFeature feature = new DataModels.NCBIImportedData.GTFFeature
                {
                    Seqname = seqname,
                    Source = source,
                    FeatureType = featureType,
                    Start = start,
                    End = end,
                    Score = score,
                    Strand = strand,
                    Frame = frame,
                };

                //var string builder for the attributes
                StringBuilder sb = new StringBuilder();
                //locla var counter for the attributes
                int counter = 0;


                //add the attributes to the feature
                foreach (string pair in attributePairs)
                {

                    // a pair is noted for instance as: gene_id "WASH7P", in order to get the current value we will check for the constant string we need e.g. gene_id. If the pair contains the constant, then we remove the constant string and remove the double quotes

                    //var for the pair
                    string CurrentPair = pair.Trim();

                    // Position 01: process the gene id
                    if (CurrentPair.Contains( TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneIdHeaderName))
                    {
                        //var for GeneId
                        string GeneId = "";

                        //remove the constant string
                        GeneId = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneIdHeaderName, "");

                        //remove the double quotes
                        GeneId = GeneId.Replace("\"", "");

                        //trim the string
                        GeneId = GeneId.Trim();

                        //add the attribute to the feature
                        feature.GeneId = GeneId;
                    }

                    // Position 02: process the transcript id
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptIdHeaderName))
                    {
                        //var for transcript id
                        string TranscriptId = "";

                        //remove the constant string
                        TranscriptId = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptIdHeaderName, "");

                        //remove the double quotes
                        TranscriptId = TranscriptId.Replace("\"", "");

                        //trim the string
                        TranscriptId = TranscriptId.Trim();

                        //add the attribute to the feature
                        feature.TranscriptId = TranscriptId;
                    }
                    // Position 03: process the db_xref
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName))
                    {
                        //var for db_xref
                        string DbXref = "";

                        //remove the constant string
                        DbXref = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.DbXrefHeaderName, "");

                        //remove the double quotes
                        DbXref = DbXref.Replace("\"", "");

                        //trim the string
                        DbXref = DbXref.Trim();

                        //add the attribute to the feature
                        feature.DbXref = DbXref;
                    }
                    // position 04: process the gbkeyHeaderName
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.gbkeyHeaderName))
                    {
                        //var for gbkeyHeaderName
                        string gbkey = "";

                        //remove the constant string
                        gbkey = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.gbkeyHeaderName, "");

                        //remove the double quotes
                        gbkey = gbkey.Replace("\"", "");

                        //trim the string
                        gbkey = gbkey.Trim();

                        //add the attribute to the feature
                        feature.GbKey = gbkey;
                    }

                    // --> note that this tag is almost impossible to properly process (positions are not constant, header title is not unique, possibly we only need the gene Id
                    // position 05: process the gene name
                    // !!!!! note that the tag Gene is often used for the gene name, but not always. Sometimes the tag gene is used for the gene id. So we need to check if the tag gene is used for the gene name or for the gene id
                    // we may thus also need to use the position of the tag to determine if the tag is used for the gene name or for the gene id
                    else if ((CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName)) & (CurrentPair.Contains(feature.GeneId)))
                    {
                        //var for gene name
                        string GeneName = "";

                        //remove the constant string
                        GeneName = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.GeneNameHeaderName, "");

                        //remove the double quotes
                        GeneName = GeneName.Replace("\"", "");

                        //trim the string
                        GeneName = GeneName.Trim();

                        //add the attribute to the feature
                        feature.Gene = GeneName;
                    }

                    // position 06a: process the product
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProductHeaderName))
                    {
                        //var for product
                        string Product = "";

                        //remove the constant string
                        Product = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProductHeaderName, "");

                        //remove the double quotes
                        Product = Product.Replace("\"", "");

                        //trim the string
                        Product = Product.Trim();

                        //add the attribute to the feature
                        feature.Product = Product;
                    }
                    // position 6b: process protein id (note this is only present for CDS)
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProteinIdHeaderName))
                    {
                        //var for protein id
                        string ProteinId = "";

                        //remove the constant string
                        ProteinId = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ProteinIdHeaderName, "");

                        //remove the double quotes
                        ProteinId = ProteinId.Replace("\"", "");

                        //trim the string
                        ProteinId = ProteinId.Trim();

                        //add the attribute to the feature
                        feature.ProteinId = ProteinId;
                    }

                    // position 07: process the pseudo
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.PseudoHeaderName))
                    {
                        //var for pseudo
                        string Pseudo = "";

                        //remove the constant string
                        Pseudo = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.PseudoHeaderName, "");

                        //remove the double quotes
                        Pseudo = Pseudo.Replace("\"", "");

                        //trim the string
                        Pseudo = Pseudo.Trim();

                        //add the attribute to the feature
                        feature.Pseudo = Pseudo;
                    }
                    // position 08: process the transcript_biotype
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptBiotypeHeaderName))
                    {
                        //var for transcript_biotype
                        string TranscriptBiotype = "";

                        //remove the constant string
                        TranscriptBiotype = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.TranscriptBiotypeHeaderName, "");

                        //remove the double quotes
                        TranscriptBiotype = TranscriptBiotype.Replace("\"", "");

                        //trim the string
                        TranscriptBiotype = TranscriptBiotype.Trim();

                        //add the attribute to the feature
                        feature.TranscriptBiotype = TranscriptBiotype;
                    }

                    // Position 09: process the exon number
                    else if (CurrentPair.Contains(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ExonNumberHeaderName))
                    {
                        //local var
                        string ExonNumber = "";

                        //remove the constant string
                        ExonNumber = CurrentPair.Replace(TheGenomeBrowser.DataModels.AssemblyMolecules.SettingsAssemblySource.ExonNumberHeaderName, "");

                        //remove the double quotes
                        ExonNumber = ExonNumber.Replace("\"", "");

                        //trim the string
                        ExonNumber = ExonNumber.Trim();

                        //add the attribute to the feature
                        feature.ExonNumber = ExonNumber;
                    }


                    //add the attribute to the string builder
                    sb.Append(pair + ";");

                    //increase the counter
                    counter++;
                }

                //add the string builder to the feature
                feature.AttributesString = sb.ToString();

                //add the feature to the list of features
                DataModelGtfFile.FeaturesList.Add(feature);

                //check if the progress counter is 1000
                if (ProgressCounter == 1000)
                {
                    //update the progress
                    progress.Report((LineCounter * 100) / TotalNumberOfLines);

                    //reset the progress counter
                    ProgressCounter = 0;
                }

                //increase the line counter
                LineCounter++;
                //increase the progress counter
                ProgressCounter++;
            }

            //clear memory of reader


            //return the list of features
            return DataModelGtfFile;

        }


        #endregion

    }


}
