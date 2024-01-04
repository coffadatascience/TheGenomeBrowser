using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.NCBIImportedData;

namespace TheGenomeBrowser.Readers
{

    /// <summary>
    /// class to read NCBI GFT assembly report files. These files contain information about the assembly of a genome
    /// This is a static class that contains methods to read the file and return a data model based on the information in the file. The data model is the NCBIImportedData.DataModelGftAssemblyReport.cs. The file is a tab delimited TXT file.
    /// The items for the DataModelGftAssemblyReport are Noted in the file as:
    /// # Assembly name:  GRCh38.p13
    //  # Description:    Genome Reference Consortium Human Build 38 patch release 13 (GRCh38.p13)
    //  # Organism name:  Homo sapiens (human)
    //  # Taxid:          9606
    //  # BioProject:     PRJNA31257
    //  # Submitter:      Genome Reference Consortium
    //  # Date:           2019-02-28
    //  # Synonyms:       hg38	
    //  # Assembly type:  haploid-with-alt-loci
    //  # Release type:   patch
    //  # Assembly level: Chromosome
    //  # Genome representation: full
    //  # RefSeq category: Reference Genome
    //  # GenBank assembly accession: GCA_000001405.28
    //  # RefSeq assembly accession: GCF_000001405.39
    //  # RefSeq assembly and GenBank assemblies identical: no
    //  The list of DataModelGftAssemblyReport are located in the second part of the file where the line start can be recognized by a line containing the column headers noted in the file as:
    //  # Sequence-Name	Sequence-Role	Assigned-Molecule	Assigned-Molecule-Location/Type	GenBank-Accn	Relationship	RefSeq-Accn	Assembly-Unit	Sequence-Length	UCSC-style-name
    //  Example of a data line: 1	assembled-molecule	1	Chromosome	CM000663.2	=	NC_000001.11	Primary Assembly	248956422	chr1
    /// </summary>
    public static class NcbiGftAssemblyReportReader
    {

        /// <summary>
        /// Function that reads the file and returns a data model (DataModelGftAssemblyReport) by importing the data in the file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static TheGenomeBrowser.DataModels.NCBIImportedData.DataModelAssemblyReport ImportDataFromFile(string filePath)
        {
            // Read the file content
            string[] lines = File.ReadAllLines(filePath);

            // Create a new instance of DataModelGftAssemblyReport
            var dataModelDataModelAssemblyReport = new TheGenomeBrowser.DataModels.NCBIImportedData.DataModelAssemblyReport();

            // Parse the file content and populate the data model
            foreach (string line in lines)
            {

                // Import the fields from the comments section of the file (represenced by # as noted in the fields region of DataModelAssemblyReport). We use the header notation as found in the file downloaded at 2024-01-14)
                // Import the fields from the comments section of the file
                // Check if the line contains the desired field
                if (line.StartsWith("# Assembly name:"))
                {
                    // Extract the field value
                    string fieldValue = line.Substring("# Assembly name:".Length).Trim();

                    // Assign the field value to the appropriate property in the data model
                    dataModelDataModelAssemblyReport.AssemblyName = fieldValue;
                }
                else if (line.StartsWith("# Description:"))
                {
                    string fieldValue = line.Substring("# Description:".Length).Trim();
                    dataModelDataModelAssemblyReport.Description = fieldValue;
                }
                else if (line.StartsWith("# Organism name:"))
                {
                    string fieldValue = line.Substring("# Organism name:".Length).Trim();
                    dataModelDataModelAssemblyReport.OrganismName = fieldValue;
                }
                else if (line.StartsWith("# Taxid:"))
                {
                    string fieldValue = line.Substring("# Taxid:".Length).Trim();
                    dataModelDataModelAssemblyReport.Taxid = fieldValue;
                }
                else if (line.StartsWith("# BioProject:"))
                {
                    string fieldValue = line.Substring("# BioProject:".Length).Trim();
                    dataModelDataModelAssemblyReport.BioProject = fieldValue;
                }
                else if (line.StartsWith("# Submitter:"))
                {
                    string fieldValue = line.Substring("# Submitter:".Length).Trim();
                    dataModelDataModelAssemblyReport.Submitter = fieldValue;
                }
                else if (line.StartsWith("# Date:"))
                {
                    string fieldValue = line.Substring("# Date:".Length).Trim();
                    dataModelDataModelAssemblyReport.Date = fieldValue;
                }
                else if (line.StartsWith("# Synonyms:"))
                {
                    string fieldValue = line.Substring("# Synonyms:".Length).Trim();
                    dataModelDataModelAssemblyReport.Synonyms = fieldValue;
                }
                else if (line.StartsWith("# Assembly type:"))
                {
                    string fieldValue = line.Substring("# Assembly type:".Length).Trim();
                    dataModelDataModelAssemblyReport.AssemblyType = fieldValue;
                }
                else if (line.StartsWith("# Release type:"))
                {
                    string fieldValue = line.Substring("# Release type:".Length).Trim();
                    dataModelDataModelAssemblyReport.ReleaseType = fieldValue;
                }
                else if (line.StartsWith("# Assembly level:"))
                {
                    string fieldValue = line.Substring("# Assembly level:".Length).Trim();
                    dataModelDataModelAssemblyReport.AssemblyLevel = fieldValue;
                }
                else if (line.StartsWith("# Genome representation:"))
                {
                    string fieldValue = line.Substring("# Genome representation:".Length).Trim();
                    dataModelDataModelAssemblyReport.GenomeRepresentation = fieldValue;
                }
                else if (line.StartsWith("# RefSeq category:"))
                {
                    string fieldValue = line.Substring("# RefSeq category:".Length).Trim();
                    dataModelDataModelAssemblyReport.RefSeqCategory = fieldValue;
                }
                else if (line.StartsWith("# GenBank assembly accession:"))
                {
                    string fieldValue = line.Substring("# GenBank assembly accession:".Length).Trim();
                    dataModelDataModelAssemblyReport.GenBankAssemblyAccession = fieldValue;
                }
                else if (line.StartsWith("# RefSeq assembly accession:"))
                {
                    string fieldValue = line.Substring("# RefSeq assembly accession:".Length).Trim();
                    dataModelDataModelAssemblyReport.RefSeqAssemblyAccession = fieldValue;
                }
                else if (line.StartsWith("# RefSeq assembly and GenBank assemblies identical:"))
                {
                    string fieldValue = line.Substring("# RefSeq assembly and GenBank assemblies identical:".Length).Trim();
                    dataModelDataModelAssemblyReport.RefSeqAndGenBankAssembliesIdentical = fieldValue;
                }

                // Skip comment lines
                // This skips the file untill the line that starts with # Sequence-Name	Sequence-Role	Assigned-Molecule	Assigned-Molecule-Location/Type	GenBank-Accn	Relationship	RefSeq-Accn	Assembly-Unit	Sequence-Length	UCSC-style-name
                if (line.StartsWith("#"))
                    continue;

                // Split the line by tab delimiter
                string[] fields = line.Split('\t');

                // Create a new instance of DataModelGftAssemblyReportItem
                var item = new DataModelAssemblyReportItem();

                // Populate the item properties
                item.SequenceName = fields[0];
                item.SequenceRole = fields[1];
                item.AssignedMolecule = fields[2];
                item.AssignedMoleculeLocationType = fields[3];
                item.GenBankAccn = fields[4];
                item.Relationship = fields[5];
                item.RefSeqAccn = fields[6];
                item.AssemblyUnit = fields[7];
                item.SequenceLength = fields[8];
                item.UCSCStyleName = fields[9];

                // Add the item to the data model
                dataModelDataModelAssemblyReport.AssemblyReportItemsList.Add(item);


            }

            // Return the data model
            return dataModelDataModelAssemblyReport;
        }

        //procedure that reads the file and returns a data model (DataModelGftAssemblyReport) by importing the data in the file.
        //The file is a tab delimited TXT file.
        // write the function that imports the data from the file and returns a data model


    }

}
