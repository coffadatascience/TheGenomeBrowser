using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheGenomeBrowser.Readers
{
    public class NcbiGftReaderToVersion2
    {

        /// <summary>
        /// parse a GFF3 file
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadGftFile(string filePath)
        {
            //create a new GFF3Reader
            GTFReader reader = new GTFReader();
            reader.ReadGFF3(filePath);

            //loop through the features
            foreach (var feature in reader.Features)
            {
                //print the information of the feature
                Console.WriteLine("Seqname: {0}", feature.Seqname);
                Console.WriteLine("Source: {0}", feature.Source);
                Console.WriteLine("FeatureType: {0}", feature.FeatureType);
                Console.WriteLine("Start: {0}", feature.Start);
                Console.WriteLine("End: {0}", feature.End);
                Console.WriteLine("Score: {0}", feature.Score);
                Console.WriteLine("Strand: {0}", feature.Strand);
                Console.WriteLine("Frame: {0}", feature.Frame);

                // Loop through the attributes
                foreach (KeyValuePair<string, string> attribute in feature.Attributes)
                {
                    Console.WriteLine("Attribute: {0} = {1}", attribute.Key, attribute.Value);
                }

                // Print a blank line
                Console.WriteLine();
            }
        }



    }

    /// <summary>
    /// class to read GFF3 files
    /// </summary>
    public class GTFReader
    {

        /// <summary>
        /// list of features read from the GFF3 file
        /// </summary>
        public List<GTFFeature> Features { get; private set; }

        /// <summary>
        /// constructor
        /// </summary>
        public GTFReader()
        {
            Features = new List<GTFFeature>();
        }

        /// <summary>
        /// parse a GFF3 file
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadGFF3(string filePath)
        {
            Features.Clear();

            foreach (string line in File.ReadLines(filePath))
            {
                if (line.StartsWith("#"))
                {
                    continue; // Skip comments
                }

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

                GTFFeature feature = new GTFFeature
                {
                    Seqname = seqname,
                    Source = source,
                    FeatureType = featureType,
                    Start = start,
                    End = end,
                    Score = score,
                    Strand = strand,
                    Frame = frame
                };

                foreach (string pair in attributePairs)
                {
                    string[] keyValue = pair.Split('=');
                    if (keyValue.Length == 2)
                    {
                        feature.Attributes[keyValue[0].Trim()] = keyValue[1].Trim();
                    }
                }

                Features.Add(feature);
            }
        }
    }

    /// <summary>
    /// class that hold the information of a GFF3 file (contains a list of GFF3Features)
    /// </summary>
    public class GTFFile
    {
        //list of GFF3Features
        public List<GTFFeature> Features { get; set; }

        //constructor
        public GTFFile()
        {
            Features = new List<GTFFeature>();
        }

    }

    /// <summary>
    /// class to read GTF files
    /// </summary>
    public class GTFFeature
    {

        public string Seqname { get; set; }
        public string Source { get; set; }
        public string FeatureType { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
        public string Score { get; set; }
        public string Strand { get; set; }
        public string Frame { get; set; }
        public Dictionary<string, string> Attributes { get; set; }

        public GTFFeature()
        {
            Attributes = new Dictionary<string, string>();
        }
    }
}
