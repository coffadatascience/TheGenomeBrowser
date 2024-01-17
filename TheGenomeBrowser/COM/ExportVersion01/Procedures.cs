using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.DataModels.AssemblyMolecules;
using TheGenomeBrowser.DataModels.NCBIImportedData;
using static TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01;

namespace TheGenomeBrowser.COM.ExportVersion01
{

    /// <summary>
    /// class that contains the procedure to create COM objects from the solutions data models
    /// ---> note that these procedures are typically specific for the data model of the solution but they are placed under the COM as they perform the translation from the data model to the COM objects
    /// ---> these thus also may be managed under different versions, this allows different version of different solutions to actually have some overlap in the COM objects preventing the need to update all solutions at the same time
    /// (which may save time if some parts are not updated or some part are already ready in earlier stages and the component itself is already validated/verified and a key path yet needs to be updated) --> note that with some planning such instnaces may be easily avoided)
    /// </summary>
    public static class ComProcedures
    {

        #region methods - conversion

        /// <summary>
        /// async method to parse the data model assembly source to COM
        /// </summary>
        /// <param name="dataModelAssemblySource"></param>
        /// <param name="dataModelGtfAssemblyReport"></param>
        /// <param name="listOfUsedSourceFiles"></param>
        /// <returns></returns>
        public static async Task<SophiasGenomeExportVersion01> ParseDataModelAssemblySourceToCOMAsync(
            DataModels.AssemblyMolecules.DataModelAssemblySource dataModelAssemblySource,
            DataModelAssemblyReport dataModelGtfAssemblyReport,
            List<string> listOfUsedSourceFiles,
            IProgress<int> progress, bool produceDebugReport)
        {
            return await Task.Run(() =>
            {
                return ParseDataModelAssemblySourceToCOM(dataModelAssemblySource, dataModelGtfAssemblyReport, listOfUsedSourceFiles, progress, produceDebugReport);
            });
        }


        /// <summary>
        /// procedure that accepts a DataModelAssemblySource and parses it to a COM object (SophiasGenome DataModel --> which may be accepted in the COM of the framing solution)
        /// --> not that we pass teh annotation report file (so the model van complete other molecules outside the scope of the assembly source)
        /// --> however we would prefer the model to be also working wihtout the need to assembly report files (as such only the assembly as found in GTF is in data), the annotation report also contains uplaced scaffold, mitochondia etc.
        /// </summary>
        /// <param name="dataModelAssemblySource"></param>
        /// <returns></returns>
        public static TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01 ParseDataModelAssemblySourceToCOM(
            DataModels.AssemblyMolecules.DataModelAssemblySource dataModelAssemblySource,
            DataModelAssemblyReport dataModelGtfAssemblyReport,
            List<string> listOfUsedSourceFiles, IProgress<int> progress, bool produceDebugReport)
        {

            //create the COM object
            TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01 sophiasGenomeExportVersion01 = new SophiasGenomeExportVersion01();


            //set the properties of the COM object
            // 1. date of creation to date now as taken from system
            sophiasGenomeExportVersion01.DateOfCreation = DateTime.Now;
            // 2. set a description (this is an export of the data model of the genome browser)
            sophiasGenomeExportVersion01.SourceDescription = "This is an export of the data model of the genome browser based on NCBI data sources.";
            //3. set conversion type to NCBI
            sophiasGenomeExportVersion01.SourceGenomeConversion = SophiasGenomeExportVersion01.SourceGenomeConversionType.NCBI;
            //4 set the file names used
            sophiasGenomeExportVersion01.ListOfUsedSourceFiles = listOfUsedSourceFiles;

            //Now we process the properties for the TheHumanGenomeSophiaDataModelCOM
            sophiasGenomeExportVersion01.TheHumanGenomeSophiaDataModelCOM = ProcessDataModelGtfAssemblyReportToTheHumanGenomeSophiaDataModelCOM(dataModelGtfAssemblyReport);

            // Now if we have a AssemblyReportItemsList in the dataModelGtfAssemblyReport then we process the data to the COM object that formulates the list of molecules for the genoem
            // ---> note that in its absence we may create the list based on the SEQNAME that is unique to an id (in an annotation report, even in its absence, then those fields contain accession number as denoted in hte assembly)
            if (dataModelGtfAssemblyReport != null)
            {
                //process the dataModelGtfAssemblyReport to the COM object
                sophiasGenomeExportVersion01.TheHumanGenomeSophiaDataModelCOM.ListOfMoleculesOther = ProcessDataModelGtfAssemblyReportToTheListOfMoleculesOtherCOM(dataModelGtfAssemblyReport, SequenceRole.assembledMolecule, false);

                //create the list with all molecules of the assembly report (so we may use it to create the list of genes and transcripts etc)
                sophiasGenomeExportVersion01.TheHumanGenomeSophiaDataModelCOM.ListOfChromosomesUsedForAssembly = ProcessDataModelGtfAssemblyReportToTheListOfMoleculesOtherCOM(dataModelGtfAssemblyReport, SequenceRole.assembledMolecule, true);

            }
            else
            {

                //Note that we will use a different procedure that build the assembled list of molecules
                // --> this is purposely done so that the assembly report is optional (though relevant and useful, we may have sources in the future that do not have this information
                // though we must note that molecule information in the assembly is then missing in the COM object (though not essential to many works), so we may not be able to create a complete list of genes and transcripts etc)
                sophiasGenomeExportVersion01.TheHumanGenomeSophiaDataModelCOM.ListOfChromosomesUsedForAssembly = CreateMoleculesList(dataModelAssemblySource.TheGenome);
            }
            

            // Now we have a list of molecules we may create the list of genes and transcripts etc
            // use LoopDictionaryOfMolecules to process the data model the genome and provide the list of chromosomes used for assembly
            LoopDictionaryOfMolecules(dataModelAssemblySource.TheGenome, sophiasGenomeExportVersion01.TheHumanGenomeSophiaDataModelCOM.ListOfChromosomesUsedForAssembly, progress, produceDebugReport);



            //return the COM object
            return sophiasGenomeExportVersion01;


        }


        /// <summary>
        /// Procedure that processes all genes, transcript and other information for the COM object
        /// </summary>
        /// <param name="dataModelTheGenome"></param>
        /// <param name="listOfChromosomesUsedForAssembly"></param>
        public static void LoopDictionaryOfMolecules(DataModelTheGenome dataModelTheGenome, List<MoleculeSophiaDataModelCOM> listOfChromosomesUsedForAssembly, IProgress<int> progress, bool produceDebugReport)
        {

            //var int counter for progress
            int counter = 0;

            //get total counter for all molecules
            int totalNumberOfMolecules = dataModelTheGenome.DictionaryOfMolecules.Count;

            //loop the dictionary of molecules
            foreach (var molecule in dataModelTheGenome.DictionaryOfMolecules.Values)
            {

                //get the molecule COM object
                MoleculeSophiaDataModelCOM retrievedMolecule = listOfChromosomesUsedForAssembly.FirstOrDefault(m => m.RefSeqAccn == molecule.refSeqAccenGtf);

                //check if the molecule is found
                if (retrievedMolecule != null)
                {

                    //var for new list of genes
                    List<GeneSophiaDataModelCOM> listOfGenes = new List<GeneSophiaDataModelCOM>();

                    //set to this molecule
                    retrievedMolecule.ListOfGenes = listOfGenes;

                    // Process genes here from molecule to retrievedMolecule
                    //loop the genes in the current molecule
                    foreach (var geneId in molecule.GeneIds.Values)
                    {

                        //create the COM object
                        GeneSophiaDataModelCOM geneSophiaDataModelCOM = new GeneSophiaDataModelCOM();

                        //set the properties of the COM object
                        //1. set the gene id (KEY)
                        geneSophiaDataModelCOM.GeneId = geneId.GeneId;
                        //2. set DbXrefOne (KEY)
                        geneSophiaDataModelCOM.DbRefXrefOne = geneId.Db_Xref_One;
                        //3. set DbXrefTwo (KEY)
                        geneSophiaDataModelCOM.DbRefXrefTwo = geneId.Db_Xref_Two;
                        //4. set source type using ConvertStringToSourceType
                        geneSophiaDataModelCOM.SourceType = ConvertStringToSourceType(geneId.Source);
                        //5. set gene biotype using ConvertStringToGeneBiotype
                        geneSophiaDataModelCOM.GeneBiotype = ConvertStringToGeneBiotype(geneId.Gene_Biotype);
                        //6. set start
                        geneSophiaDataModelCOM.Start = geneId.Start;
                        //7. set end
                        geneSophiaDataModelCOM.End = geneId.End;
                        //8. set strand
                        geneSophiaDataModelCOM.Strand = ConvertIntStrandToEnumStrand(geneId.Strand);
                        //9. set description
                        geneSophiaDataModelCOM.Description = geneId.Description;
                        //10. set synonyms
                        geneSophiaDataModelCOM.Gene_Synonyms = geneId.Gene_Synonyms;


                        //var for new list of transcripts
                        List<TranscriptSophiaDataModelCOM> listOfTranscripts = new List<TranscriptSophiaDataModelCOM>();
                        //loop the transcripts in the current gene (CreateTranscriptsList for this GeneId)
                        //create the list of transcripts COM objects
                        List<TranscriptSophiaDataModelCOM> transcriptsList = CreateTranscriptsList(geneId.ListGeneTranscripts, produceDebugReport);
                        //set to this geneId
                        geneSophiaDataModelCOM.ListOfTranscripts = transcriptsList;

                        //add the gene COM object to the list of genes
                        listOfGenes.Add(geneSophiaDataModelCOM);

                    }


                }


                //check if the progress is not null
                if (progress != null)
                {

                    //var for the progress percentage in double
                    double progressPercentageDouble = ((double)counter / (double)totalNumberOfMolecules) * 100;

                    //calculate the progress
                    int progressPercentage = (int)Math.Round(progressPercentageDouble);

                    //report the progress
                    progress.Report(progressPercentage);

                }

                //increase the counter
                counter++;
            }

        }


        /// <summary>
        /// create a list of all transcripts COM objects from the data model
        /// </summary>
        /// <param name="geneIds"></param>
        /// <returns></returns>
        public static List<TranscriptSophiaDataModelCOM> CreateTranscriptsList(List<DataModelGeneTranscript> listGeneTranscripts, bool produceDebugReport)
        {

            //new list of transcripts COM objects
            List<TranscriptSophiaDataModelCOM> transcriptsList = new List<TranscriptSophiaDataModelCOM>();

            //loop the transcripts in the current gene id
            foreach (var transcript in listGeneTranscripts)
            {
                //create the COM object
                TranscriptSophiaDataModelCOM transcriptSophiaDataModelCOM = new TranscriptSophiaDataModelCOM();

                // Set the properties of the COM object
                transcriptSophiaDataModelCOM.TranscriptId = transcript.TranscriptId;
                transcriptSophiaDataModelCOM.DbRefXref = transcript.Db_Xref;
                transcriptSophiaDataModelCOM.TranscriptBiotype = ConvertStringToTranscriptBiotype(transcript.Transcript_Biotype);
                transcriptSophiaDataModelCOM.Start = transcript.Start;
                transcriptSophiaDataModelCOM.End = transcript.End;
                transcriptSophiaDataModelCOM.Strand = ConvertIntStrandToEnumStrand(transcript.Strand);
                //model evidence
                transcriptSophiaDataModelCOM.ModelEvidence = transcript.ModelEvidence;

                //Now product of the exons should be one and can be derived from any of the exons (we use the first, and if there are more add an error value but continue adding the first two with the warning unexpected)
                transcriptSophiaDataModelCOM.ProductOfExons = transcript.GeneTranscriptObject.ReturnProductNameForExons();
                //set product of cds
                transcriptSophiaDataModelCOM.ProductOfCDS = transcript.GeneTranscriptObject.ReturnProductNameForCDS();
                //Note on CDS (CDS and start stop lines sometimes have notes that tell something about precursor that are transcribed by a different variant name -- these linkage may be interesting and so we may add it as the CDS note. Though they are not concrete blocks of information)
                transcriptSophiaDataModelCOM.NoteOnCDS = transcript.GeneTranscriptObject.ReturnNoteOnCDS();

                //when in debug mode print the Gene ID, ProductOfExons and ProductOfCDS into the debug window if ProductOfExons is not empty or ProductOfCDS is not empty
                //if we want to produce some details during export we can set the bool to true (else it is slower)
                if (produceDebugReport == true)
                {
#if DEBUG
                    if (transcriptSophiaDataModelCOM.ProductOfExons != "" || transcriptSophiaDataModelCOM.ProductOfCDS != "")
                    {
                        System.Diagnostics.Debug.WriteLine("Transcript ID: " + transcriptSophiaDataModelCOM.TranscriptId + " ProductOfExons: " + transcriptSophiaDataModelCOM.ProductOfExons + " ProductOfCDS: " + transcriptSophiaDataModelCOM.ProductOfCDS);
                    }
#endif
                }

                //add the COM object to the list
                transcriptsList.Add(transcriptSophiaDataModelCOM);

                //create the list of exons COM objects
                transcriptSophiaDataModelCOM.ListOfExons = ConvertExons(transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementExon);
                
                //create the list of cds COM objects
                transcriptSophiaDataModelCOM.CDSSophiaDataModelCOM.ListOfCDS = ConvertCds(transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementCDS);

                //set protein id (using GetCheckProteinIds)
                transcriptSophiaDataModelCOM.CDSSophiaDataModelCOM.ProteinId = GetCheckProteinIds(transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementCDS);
                //set product (using GetCheckProduct)
                transcriptSophiaDataModelCOM.CDSSophiaDataModelCOM.Product = GetCheckProduct(transcript.GeneTranscriptObject.ListDataModelGeneTranscriptElementCDS);
                
                //check if the transcript has a start codon and end codon
                if (transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon != null)
                {
                    //set start codon
                    transcriptSophiaDataModelCOM.StartCodonStart = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon.Start;
                    transcriptSophiaDataModelCOM.StartCodonEnd = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStartCodon.End;
                }
                else
                {
                    //set start codon
                    transcriptSophiaDataModelCOM.StartCodonStart = -1;
                    transcriptSophiaDataModelCOM.StartCodonEnd = -1;
                }

                //check if the transcript has a stop codon
                if (transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon != null)
                {
                    //set stop codon
                    transcriptSophiaDataModelCOM.StopCodonStart = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon.Start;
                    transcriptSophiaDataModelCOM.StopCodonEnd = transcript.GeneTranscriptObject.DataModelGeneTranscriptElementStopCodon.End;
                }
                else
                {
                    //set stop codon
                    transcriptSophiaDataModelCOM.StopCodonStart = -1;
                    transcriptSophiaDataModelCOM.StopCodonEnd = -1;
                }
                
            }


            return transcriptsList;
        }



        /// <summary>
        /// convert a list of cds COM objects from the data model
        /// </summary>
        /// <param name="cds"></param>
        /// <returns></returns>
        public static List<CDSItemSophiaDataModelCOM> ConvertCds(List<DataModelGeneTranscriptElementCDS> cds)
        {
            //new cds
            var cdsSophiaDataModelComList = new List<CDSItemSophiaDataModelCOM>();

            //loop elements
            foreach (var cd in cds)
            {
                //new cds
                var cdsSophiaDataModelCom = new CDSItemSophiaDataModelCOM();

                //set properties
                cdsSophiaDataModelCom.Start = cd.Start;
                cdsSophiaDataModelCom.End = cd.End;
                //exon
                cdsSophiaDataModelCom.ExonNumber = cd.ExonNumber;

                cdsSophiaDataModelComList.Add(cdsSophiaDataModelCom);
            }

            return cdsSophiaDataModelComList;
        }


        /// <summary>
        /// procedure that converts a list of exons to a list of COM objects
        /// </summary>
        /// <param name="exons"></param>
        /// <returns></returns>
        private static List<ExonItemSophiaDataModelCOM> ConvertExons(List<DataModelGeneTranscriptElementExon> exons)
        {

            //new list
            var exonSophiaDataModelComList = new List<ExonItemSophiaDataModelCOM>();

            //loop the exons
            foreach (var exon in exons)
            {
                //create the COM object
                var exonSophiaDataModelCom = new ExonItemSophiaDataModelCOM();

                //set the properties of the COM object
                exonSophiaDataModelCom.Start = exon.Start;
                exonSophiaDataModelCom.End = exon.End;
                //set exon
                exonSophiaDataModelCom.ExonNumber = exon.ExonNumber;

                //add the COM object to the list
                exonSophiaDataModelComList.Add(exonSophiaDataModelCom);
            }

            //return the list
            return exonSophiaDataModelComList;

        }


        /// <summary>
        /// procedure that makes a molecule COM object from a molecule data model (for cases where we do not have the assembly report)
        /// </summary>
        /// <param name="dataModelTheGenome"></param>
        /// <returns></returns>
        public static List<MoleculeSophiaDataModelCOM> CreateMoleculesList(DataModelTheGenome dataModelTheGenome)
        {
            //create the list of molecules COM objects
            List<MoleculeSophiaDataModelCOM> listOfMolecules = new List<MoleculeSophiaDataModelCOM>();

            //loop the dataModelTheGenome list of molecules and create a list as return factor
            foreach (var DicItemMolecule in dataModelTheGenome.DictionaryOfMolecules)
            {

                //get value object
                var molecule = DicItemMolecule.Value;

                //create the COM object
                MoleculeSophiaDataModelCOM moleculeSophiaDataModelCOM = new MoleculeSophiaDataModelCOM();

                //NOTE -- > in absence of the annotation report we use the SEQNAME as the molecule name (this is unique to the molecule) which is the Ref Seq accession number in the assembly
                //     --> this is not the best solution, the chromosome number may then be derived from the RefSeq accession number, e.g. NC_000001.11 is chromosome 1, NC_000009.12 is chromosome 9 etc

                // REF SEQ is moleculeChromosome
                moleculeSophiaDataModelCOM.RefSeqAccn = molecule.refSeqAccenGtf ?? "na";
                //get and seq chromosome number using GetChromosomeNumber (note that it is expected that molecule name has the same value as RefSeqAccnGtf
                string chromosomeNumber = GetChromosomeNumber(molecule.moleculeChromosome);
                //5. -- > set extract chromosome number (if it fails refseq is returned)
                moleculeSophiaDataModelCOM.AssignedMolecule = chromosomeNumber;

                //set the properties of the COM object to ConstantForNa
                //1. set the genbank accn
                moleculeSophiaDataModelCOM.GenbankAccn = ConstantForNa;
                //2. set uscs style name
                moleculeSophiaDataModelCOM.UCSCStyleName = ConstantForNa;
                //3. set sequence name
                moleculeSophiaDataModelCOM.SequenceName = ConstantForNa;
                //4. set the sequence role (by conversion of the string by using ConvertStringToSequenceRole)
                moleculeSophiaDataModelCOM.SequenceRole = SequenceRole.assembledMolecule; // assembly file was used to make this list
                //6. set the assigned molecule location type
                moleculeSophiaDataModelCOM.MoleculeLocationType = MoleculeLocationType.Unknown; 
                //7. set sequence length
                moleculeSophiaDataModelCOM.SequenceLength = -1;

                listOfMolecules.Add(moleculeSophiaDataModelCOM);
            }

            return listOfMolecules;
        }

        /// <summary>
        /// procedures that processes the molecules list of the assembly report to create a list of molecules COM objects and returns the list of molecules COM objects 
        /// -- > with the Exception of those with the sequence role: passed (we use this typically for assembledMolecule. In this model all information about genes and transcripts etc is placed, so the list may have all other molecules that may come to other use in later stages)
        /// -- > Note that we can pass a sequence role, and a boolean that states "include", thus if we state: sequence role assigned molecules and include = true, then we get all assigned molecules, if we state assigned molecules and include = false, then we get all molecules except assigned molecules
        /// </summary>
        /// <param name="dataModelGtfAssemblyReport"></param>
        /// <returns></returns>
        private static List<MoleculeSophiaDataModelCOM> ProcessDataModelGtfAssemblyReportToTheListOfMoleculesOtherCOM(
            DataModelAssemblyReport dataModelGtfAssemblyReport, SequenceRole sequenceRole, bool include)
        {

            //create the list of molecules COM objects
            List<MoleculeSophiaDataModelCOM> listOfMoleculesCOM = new List<MoleculeSophiaDataModelCOM>();

            //loop the dataModelGtfAssemblyReport list of molecules and create a list as return factor that includes all elements that are not assembledMolecule
            foreach (var molecule in dataModelGtfAssemblyReport.AssemblyReportItemsList)
            {
                //get sequence rol in var
                var SequenceRoleCurrentMolecule = TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01.ConvertStringToSequenceRole(molecule.SequenceRole);

                //check if the sequence role is provided value (eq assembled-molecules) and if the include is true all with that value are added (else all without that value are added)
                //this allows to use this procedure to create a list of all molecules except assembled molecules (and once again for all assembled molecules)
                if ((SequenceRoleCurrentMolecule == sequenceRole) & (include == true) |
                    (SequenceRoleCurrentMolecule != sequenceRole) & (include == false))
                {
                    //create the COM object
                    MoleculeSophiaDataModelCOM moleculeSophiaDataModelCOM = new MoleculeSophiaDataModelCOM();

                    //set the properties of the COM object
                    //1. set the genbank accn
                    moleculeSophiaDataModelCOM.GenbankAccn = molecule.GenBankAccn;
                    //2. set ref seq accn
                    moleculeSophiaDataModelCOM.RefSeqAccn = molecule.RefSeqAccn;
                    //3. set uscs style name
                    moleculeSophiaDataModelCOM.UCSCStyleName = molecule.UCSCStyleName;
                    //4. set sequence name
                    moleculeSophiaDataModelCOM.SequenceName = molecule.SequenceName;
                    //5. set the sequence role (by conversion of the string by using ConvertStringToSequenceRole)
                    moleculeSophiaDataModelCOM.SequenceRole = TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01.ConvertStringToSequenceRole(molecule.SequenceRole);
                    //6. set the assigned molecule
                    moleculeSophiaDataModelCOM.AssignedMolecule = molecule.AssignedMolecule;
                    //7. set the assigned molecule location type
                    moleculeSophiaDataModelCOM.MoleculeLocationType = TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01.ConvertStringToMoleculeLocationType(molecule.AssignedMoleculeLocationType);
                    //8. set sequence length
                    moleculeSophiaDataModelCOM.SequenceLength = TheGenomeBrowser.COM.ExportVersion01.SophiasGenomeExportVersion01.ParseStringToInt(molecule.SequenceLength);

                    //add the COM object to the list
                    listOfMoleculesCOM.Add(moleculeSophiaDataModelCOM);
                }

            }

            //return the list of molecules COM objects
            return listOfMoleculesCOM;

        }


        /// <summary>
        /// procedure that processes the dataModelGtfAssemblyReport to a TheHumanGenomeSophiaDataModelCOM COM object, in case the dataModelGtfAssemblyReport is NULL, then we add logical values denoting na
        /// </summary>
        /// <param name="dataModelGtfAssemblyReport"></param>
        /// <returns></returns>
        private static TheHumanGenomeSophiaDataModelCOM ProcessDataModelGtfAssemblyReportToTheHumanGenomeSophiaDataModelCOM(DataModelAssemblyReport dataModelGtfAssemblyReport)
        {

            //create the COM object
            TheHumanGenomeSophiaDataModelCOM theHumanGenomeSophiaDataModelCOM = new TheHumanGenomeSophiaDataModelCOM();

            //set the properties of the COM object

            //Now we process the properties for the TheHumanGenomeSophiaDataModelCOM
            // new Data model for Human genome
            var HumanGenome = new TheHumanGenomeSophiaDataModelCOM();

            //check if we have a dataModelGtfAssemblyReport (if it is without content then set the fields of the HumanGenome to Na)
            if (dataModelGtfAssemblyReport == null)
            {
                //1. set the assembly name
                HumanGenome.AssemblyName = "na";
                //2. set the assembly description
                HumanGenome.AssemblyDescription = "na";
                //3. set the taxid
                HumanGenome.TaxId = "na";
                //4. set the bioproject
                HumanGenome.BioProject = "na";
                //5. set the submitter
                HumanGenome.Submitter = "na";
                //6. set the date of creation
                HumanGenome.Date = "na";
                //7. set synonyms
                HumanGenome.Synonyms = "na";
                //8. set the assembly type
                HumanGenome.AssemblyType = "na";
                //9. set the release type
                HumanGenome.ReleaseType = "na";
                //10. set the assembly level
                HumanGenome.AssemblyLevel = "na";
                //11. set the genome representation
                HumanGenome.GenomeRepresentation = "na";
                //12. set the refseq category
                HumanGenome.RefSeqCategory = "na";

                //return the COM object
                return theHumanGenomeSophiaDataModelCOM;
            }

            //1. set the assembly name
            HumanGenome.AssemblyName = dataModelGtfAssemblyReport.AssemblyName;
            //2. set the assembly description
            HumanGenome.AssemblyDescription = dataModelGtfAssemblyReport.Description;
            //3. set the taxid
            HumanGenome.TaxId = dataModelGtfAssemblyReport.TaxId;
            //4. set the bioproject
            HumanGenome.BioProject = dataModelGtfAssemblyReport.BioProject;
            //5. set the submitter
            HumanGenome.Submitter = dataModelGtfAssemblyReport.Submitter;
            //6. set the date of creation
            HumanGenome.Date = dataModelGtfAssemblyReport.Date;
            //7. set synonyms
            HumanGenome.Synonyms = dataModelGtfAssemblyReport.Synonyms;
            //8. set the assembly type
            HumanGenome.AssemblyType = dataModelGtfAssemblyReport.AssemblyType;
            //9. set the release type
            HumanGenome.ReleaseType = dataModelGtfAssemblyReport.ReleaseType;
            //10. set the assembly level
            HumanGenome.AssemblyLevel = dataModelGtfAssemblyReport.AssemblyLevel;
            //11. set the genome representation
            HumanGenome.GenomeRepresentation = dataModelGtfAssemblyReport.GenomeRepresentation;
            //12. set the refseq category
            HumanGenome.RefSeqCategory = dataModelGtfAssemblyReport.RefSeqCategory;

            //return the COM object
            return theHumanGenomeSophiaDataModelCOM;
        }

        #endregion


        #region Functions

        /// <summary>
        /// Extract chromosome from refseq
        /// </summary>
        /// <param name="refSeqAccn"></param>
        /// <returns></returns>
        private static string GetChromosomeNumber(string refSeqAccn)
        {

            //try to extract chromosome from refseq, if it fails return refSeqAccn
            try
            {
                //extract chromosome from refseq
                string[] parts = refSeqAccn.Split('.');
                string chromosomeNumber = parts[0].Substring(3);

                //return chromosome number
                return chromosomeNumber;
            }
            catch (Exception)
            {
                //return refSeqAccn
                return refSeqAccn;
            }

            return refSeqAccn;

        }

        /// <summary>
        /// check and get CDS protein id
        /// </summary>
        /// <param name="cdsList"></param>
        /// <returns></returns>
        public static string GetCheckProteinIds(List<DataModelGeneTranscriptElementCDS> cdsList)
        {
            //check if the list is empty
            if (cdsList.Count == 0)
            {
                return SophiasGenomeExportVersion01.ConstantForNa;
            }

            //get the protein id
            string proteinId = cdsList[0].ProteinId;
            bool sameProteinId = true;

            //check if all protein ids are the same
            foreach (var cds in cdsList)
            {
                if (cds.ProteinId != proteinId)
                {
                    sameProteinId = false;
                    break;
                }
            }

            //if not all protein ids are the same, return na
            if (!sameProteinId)
            {
                System.Diagnostics.Debug.WriteLine("Protein IDs are not the same.");
            }

            //return the protein id
            return proteinId;
        }

        /// <summary>
        /// procedure that checks if all products are the same and returns the product
        /// </summary>
        /// <param name="cdsList"></param>
        /// <returns></returns>
        public static string GetCheckProduct(List<DataModelGeneTranscriptElementCDS> cdsList)
        {
            //check if the list is empty
            if (cdsList.Count == 0)
            {
                return SophiasGenomeExportVersion01.ConstantForNa;
            }

            //get the product
            string product = cdsList[0].Product;
            bool sameProduct = true;

            //check if all products are the same
            foreach (var cds in cdsList)
            {
                if (cds.Product != product)
                {
                    sameProduct = false;
                    break;
                }
            }

            //if not all products are the same, return na
            if (!sameProduct)
            {
                System.Diagnostics.Debug.WriteLine("Products are not the same.");
            }

            //return the product
            return product;
        }


        /// <summary>
        /// async method to serialize Sophia the genome
        /// </summary>
        /// <param name="sophiasGenome"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static async Task<string> SerializeSophiasGenomeAsync(SophiasGenomeExportVersion01 sophiasGenome, string filePath)
        {
            //create the serializer
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(SophiasGenomeExportVersion01));

            //create the stream writer
            using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(filePath))
            {
                //serialize the object
                serializer.Serialize(streamWriter, sophiasGenome);
            }

            //return file path of the stored XML file
            return filePath;
        }


        /// <summary>
        /// procedure that takes a filePath and zips the file using zip
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void ZipFile(string filePath)
        {

            //create the zip file path
            string zipFilePath = Path.ChangeExtension(filePath, ".zip");
            //create the zip file
            using (FileStream fileStream = new FileStream(zipFilePath, FileMode.Create))
            {
                //create the zip archive
                using (ZipArchive archive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(filePath, Path.GetFileName(filePath));
                }

            }
        }



        #endregion





    }


}
