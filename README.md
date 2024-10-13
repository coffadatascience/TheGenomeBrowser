------------------------------------------------------------------
TheGenomeBrowser
------------------------------------------------------------------
Content
Table of Contents
TheGenomeBrowser	1
Content	1
Introduction	2
Genome Assembly: Reconstruction of Genetic Sequences	2
NCBI Datasets	3
The Genome Browser	3
Types of Data Models	4
Project status	5
Possible updates	5
Useability	5
Tested files % versions	5
Annotation file formats	6
Setup project / Manual	6
Feedback / collaboration	6
Technical information	6
Reading an Annotation File	6
Example Entries	7
Reading and Processing Assembly Report File	8
Data architecture	9
The Gene Lookup Model	9
Data Models for Genomic Information	10
ViewModel Approach	10
Genomic Data Management System	12
Ensembl and Genomic Data Organization	12
Utilizing GbKey and CDS for Data Interpretation	12
Genomic Data Organization and Management	12
Notes on GTF File Import and Data Model Optimization	13
Alternative Sources in Gene Model	13

-----------------------------------------------
Introduction
-----------------------------------------------

# Genomic Data Organizer

This project aims to streamline the process of organizing and utilizing genomic annotation data from the NCBI website. It creates a relational database from the downloaded files and exports an interface model that can be used across various genomic research projects.

## Key Features

- Organizes data into a relational database
- Exports a versatile interface model
- Minimizes data redundancy and improves accessibility
- Facilitates easier data exploration and analysis

## Benefits

- Standardizes genomic data management
- Reduces reliance on error-prone spreadsheets
- Improves efficiency in genomic research workflows
- Enables flexible and autonomous data handling
- Supports long-term result comparison and product development

-----------------------------------------------
Genome Assembly: Reconstruction of Genetic Sequences
-----------------------------------------------
Genome assembly is the process of reconstructing a complete genome sequence from shorter sequence reads. There are two main types:

1. Reference-based Assembly: Compares sample sequences to existing database sequences.
2. De novo Assembly: Constructs the genome without prior reference, using only sample reads.

Key Points:
- De novo assembly is more challenging than reference-based assembly.
- Modern algorithms use overlapping regions in reads to align and construct the genome.
- Genome annotation follows assembly, identifying and labeling genomic features.
- Assembly and annotation complexity varies:
  - Simple for small genomes (e.g., bacteria, fungi)
  - Complex and time-consuming for eukaryotic genomes, especially without reference genomes

-----------------------------------------------
NCBI Datasets 
-----------------------------------------------
Although NCBI provides a lot of online tools to search the genome they also provide complete genomic datasets that may be downloaded for usage.

https://www.ncbi.nlm.nih.gov/datasets/genome/

Having such a complete data set available at a local computer may provide some advantages to relying on online sources:

-	It removes the need of that computer to access the internet
-	We are not limited by internet traffic or busy moments at the NCBI limiting access
-	We may hierachically reorganise the data models making it suitable to access on a functional level suitable for our needs
-	We are not subject to changes that occur without having knowledge of it that may influence our research
-	We can refer to a single static assembly rather than collecting information from different versions of assemblies
-	Rather than the need to maintain reference information to each used piece of information (such as a gene name or used accession number) in relation to the assembly version we only have a reference to a single reference assembly. This would then allow use only to maintain the positionality to the human genome to access all information about that position. 
-	When an update of the used reference genome assembly is available all used information can be updated in a batch action allowing one to evaluate updates to all location and information that was used during research and data gathering. This would then also allow to curate an overview of all changes in the available assembly data that are relevant to our research.

-----------------------------------------------
The Genome Browser
-----------------------------------------------
This project is created for research purposes. We want to examine how to import entire genome assemblies from online sources and filter and structure useful information into a multidimensional data model that mimics the genomic structure of the human genome. 

We then aim to normalize in the context of data modeling and:

1. Remove redundant data
2. Organizing data into separate tables
3. Establishing relationships between these tables

This process helps to:

1. Eliminate data duplication
2. Improve data integrity
3. Reduce data anomalies
4. Simplify data maintenance

In the context of converting a flat file into a structured data model, as described in the search results, this process involves:

1. Identifying dimensions and facts
2. Creating separate tables for each dimension and fact
3. Removing redundant information from each table
4. Establishing relationships between tables using keys

To be able to also access online information we will also use index keys following the structure as the NBCI uses to connect different levels of hierarchical data. This will then allow groupings on these keys to be used to retrieve batches of information, rather than accessing each piece of information manually in browsers. These batches may be formed based on collections that are grouped on a functional level in the context of our investigation. 

-----------------------------------------------
Types of Data Models
-----------------------------------------------
In this project we will ue at least 3 different data models, these being:

-	The file import data model: this model is used to import the assembly data and may different between different versions that we may want to use. Typically this model includes all fields of the file. 
-	The internal data model: this model receives the normalized extract of the import data model removing all redundant information. This data model includes the identified dimensions and facts as is understood about the structure of the human genome.
-	The export (interface) data model: this model allows the data to be transported to a data model that can be used as an interface between different components. This then allows a single import / export procedure and data model to be maintained between different component which may be separately maintained. This separation thus allows different related large components to be synchronized in a separate life cycle update by only updated these parts. The main development of each separate component may therefore be under constant development without the need to constantly synchronize or wait on the development of another component. The advantage of this is that focus can be maintained to what is currently at hand, while influences that are of a higher hierarchical level may be evaluated on a more comprehensive functional level. 
-	Quality control models: view models that are used to organize that in such a way that the data is organized for easy visualization. This allows quality control steps to be executed while import the data in a comprehensive manner.

-----------------------------------------------
Project status
-----------------------------------------------
As this project is currently used on a developer level, the state of readiness may be defined as a proof of concept. What is included?
-	Import procedures for GTF file
-	Import procedures for assembly report data
-	Procedures to extract all molecules present in the genome assembly file
-	Procedures to extract and position all gene information to the recognized molecules
-	Procedures to extract all transcript information and link them to each gene
-	Created setup project and placed in the repository as a zip file. Tested installation on Windows 11 ARM64  (SetupTheGenomeBrowser.zip)

Possible updates
-	Make a BLAZOR front end
-	Include downloading of the annotation data files from the application
-	Make the entire workflow command line approachable

-----------------------------------------------
Useability
-----------------------------------------------
This project can be used to download/install via the setup project that can be found in the Github repository and attempt evaluate its usage its value in its current state; that would be by using it to create a normalized database model based on NCBI annotation data. This project may also be used open code to use import procedure or data models and to reuse code in research project that have a focus on the (human) genome.
Tested files % versions
Genome annotation is the process of finding and designating locations of individual genes and other features on raw DNA sequences, called assemblies. Annotation gives meaning to a given sequence and makes it much easier for researchers to view and analyze its contents.
In a de novo genome assembly and annotation project, the nucleotide sequence of a genome is first assembled, as completely as possible, and then annotated. The annotation process infers the structure and function of the assembled sequences.

---------------------------------------
Tested file / data:
---------------------------------------
This project was launched to investigate the possibilities of using organised datamodels downstream a pipelines development, therefore development was based  on the availability of a single input file version of NCBI (GCF_000001405.39_GRCh38.p13). We may however note that version (GCF_000001405.40_GRCh38.p14) also works. New releases of complete annotation sets seems to be limited, next to that due to the data model setup (being true to known features in phsyical reality) we may only need to add new import proecedures, rather than requiring much adjustment in internal models.

Availability of file (Google drive download adress):

•	The Genome Browser Setup file
o	https://drive.google.com/file/d/1qnHg7FElFNETApGQb3lnMqPQ4P1ASqyW/view?usp=sharing
•	Assembly Report file (71kb)
o	https://drive.google.com/file/d/1m3LuL6JX3syxWOAiQiWmLksY9NpJlKAD/view?usp=sharing
•	Gene annotation file (GCF_000001405.39_GRCh38.p13)
o	https://drive.google.com/file/d/1afxu4hhOypCvESS6IomQOtC45ZKUv1_U/view?usp=sharing

Other known formats:  as appears on the NCBI website https://www.ncbi.nlm.nih.gov/datasets/taxonomy/9606/ 

---------------------------------------
Annotation file formats
---------------------------------------
Formats of annotation files available in NCBI Datasets Data Packages
https://www.ncbi.nlm.nih.gov/datasets/docs/v2/reference-docs/file-formats/annotation-files/

---------------------------------------
Setup project / Manual
---------------------------------------

In the Github repository a setup file may be found (“SetupTheGenomeBrowser.zip”) that may be used to install the application on a Windows computer. The repository also include a manual that describes the installation procedure as well as how one may use the program to convert an annotation file.

-----------------------------------------------
Feedback / collaboration
-----------------------------------------------
This project is open-source and available for free use. We encourage feedback and welcome collaboration from anyone interested in contributing. Your insights can help improve the functionality and usability of the project. If you have suggestions, questions, or would like to collaborate, please feel free to reach out via the email address provided on my GitHub profile. Together, we can enhance this tool and make genomic data management more efficient for everyone.

Share

Rewrite

-----------------------------------------------
Technical information
-----------------------------------------------
---------------------------------------
Reading an Annotation File
---------------------------------------

File Formats

Annotation files commonly use formats such as GFF (General Feature Format) or GTF (Gene Transfer Format). These formats are tab-separated and share similar structures.

Fields in GFF/GTF Format

1. **seqname**: Chromosome or scaffold name
2. **source**: Program or data source name
3. **feature**: Feature type (e.g., Gene, Variation)
4. **start**: Start position (1-based numbering)
5. **end**: End position (1-based numbering)
6. **score**: Floating point value
7. **strand**: + (forward) or - (reverse)
8. **frame**: 0, 1, or 2 (codon start position)
9. **attribute**: Semicolon-separated tag-value pairs

Key Fields for Gene Entries

- Description
- Gene name
- DbXref
- GeneSynonym (list)
- Pseudo (boolean)
- GeneBiotype

Example Entries

**Gene:**
```
NC_000001.11 BestRefSeq gene 14362 29370 . - . gene_id "WASH7P"; transcript_id ""; db_xref "GeneID:653635"; db_xref "HGNC:HGNC:38034"; description "WASP family homolog 7, pseudogene"; gbkey "Gene"; gene "WASH7P"; gene_biotype "transcribed_pseudogene"; gene_synonym "FAM39F"; gene_synonym "WASH5P"; pseudo "true";
```

**Transcript:**
```
NC_000001.11 BestRefSeq transcript 14362 29370 . - . gene_id "WASH7P"; transcript_id "NR_024540.1"; db_xref "GeneID:653635"; gbkey "misc_RNA"; gene "WASH7P"; product "WASP family homolog 7, pseudogene"; pseudo "true"; transcript_biotype "transcript";
```

**Exon:**
```
NC_000001.11 BestRefSeq exon 29321 29370 . - . gene_id "WASH7P"; transcript_id "NR_024540.1"; db_xref "GeneID:653635"; gene "WASH7P"; product "WASP family homolog 7, pseudogene"; pseudo "true"; transcript_biotype "transcript"; exon_number "1";
```

Alternative Source

For additional information, refer to the Gene Ontology Annotation File Format (GAF) version 2.2 documentation.

---------------------------------------
Reading and Processing Assembly Report File
---------------------------------------
The assembly_report.txt file is a crucial resource for NCBI RefSeq genome assemblies. It can be obtained from:

1. The NCBI Assembly portal by searching for the desired genome and selecting "Assembly structure report" from the downloads menu.
2. The NCBI genomes FTP path for the specific assembly of interest.

For the human genome, the file is available at:
https://ftp.ncbi.nlm.nih.gov/genomes/all/GCF/000/001/405/GCF_000001405.39_GRCh38.p13/GCF_000001405.39_GRCh38.p13_assembly_report.txt

File Structure
The assembly_report.txt file is tab-delimited and contains the following columns:

1. Sequence-Name
2. Sequence-Role
3. Assigned-Molecule
4. Assigned-Molecule-Location/Type
5. GenBank-Accn
6. Relationship
7. RefSeq-Accn
8. Assembly-Unit
9. Sequence-Length
10. UCSC-style-name

Processing the Report File
When reading the report file:

1. Skip the header information at the beginning of the file.
2. Start parsing from the line containing column headers.
3. Use the data to create lookup dictionaries for chromosome information.

Organizing Data
To efficiently process and organize the genomic data:

1. First, create lookup dictionaries based on chromosome information.
2. Place all genes on their respective chromosomes.
3. Generate a genes list as a secondary lookup table.

This approach allows for more stable and efficient data retrieval compared to downloading individual gene positions via Entrez.

-----------------------------------------------
Data architecture
-----------------------------------------------
This section discusses some of the used data models in this piece of software. These models may be found in the architecture. Here we merely discuss some reasoning behind the hierarchical structure.

Note that this is technical information and not necessarily organized in a manner for reading, it is merely added here to elucidate considerations in data models and processing steps. Note that more extensive documentation is available, we merely here provide some background in the modelling processes.

---------------------------------------
The Gene Lookup Model
---------------------------------------

To minimize complexity and create a hierarchical order, we can develop a list with unique gene IDs from the assembly annotation file. This approach allows for the creation of view models that provide insight into the data present in the file.

Key Points:

1. Alternative Accession Numbers 
   - Include a field for alternative accession numbers.
   - Add a list of all known accession numbers for each gene ID.
   - Note: Currently, no alternative accession numbers were found, but this field serves as a backup check.

2. Gene and Transcript Entries
   - Distinguish between gene, transcript, and exon entries.
   - Gene and transcript entries may share start locations but lack exon numbers.
   - Exon entries contain specific exon numbers.

3. Data Organization
   - Create two dictionaries:
     1. All elements
     2. Unique elements (excluding "transcript" and "gene" as element names)
   - Multiple entries may exist for the same exon notation, stop codons, and CDS.

4. Location and Ordering
   - Actual locations are less critical for large chromosomal changes.
   - Order and proximity are more important for results display.
   - For mutation and SNP investigations, exact locations become crucial.

5. Strand Configuration
   - Include strand information for each element.
   - Exon positions may vary based on strand and configurations.

6. Transcript Variants
   - Account for multiple transcript variants (e.g., X1 to X4).
   - The same exon may have different start locations in different transcripts.
   - Consider a structure that incorporates transcript information.

### Example: LINC01128 ###

- 8 variants in total
- First 7 variants share the same exon notation
- Variant 8 has a different start position for exon 1
- Each configuration has a unique transcript ID and variant type name
- Transcript IDs may be used to download additional information

By implementing this model, we can create a more flexible and comprehensive gene lookup system that accounts for the complexities of genomic data organization.

Your analysis of the data models and organization for genomic data is comprehensive and insightful. Here's a revised and structured version of your thoughts:

---------------------------------------
Data Models for Genomic Information
---------------------------------------
ViewModel Approach

Using ViewModels can provide flexibility in displaying and filtering genomic data. While a full hierarchical structure may not be necessary for a gene list, it offers better depth insight, removes duplicate entries, and allows for easier overviews.

Proposed Hierarchical Structure

1. Sequence ID (gene)
2. Source (typically BestRefSeq or Gnomon)
3. mRNA or Protein
   a. FeatureType:
      - Transcript
      - Start_codon
      - End_codon
      - Exon
      - CDS
   b. Transcript ID & transcript variant name
4. Db_Xref (Ensembl), product entry, & protein ID

This structure allows for untangling the GTF file and creating a framework for genome browsing based on molecules.

Alternative Organization: List by Source and mRNA/Protein

- Split entries by source and separate mRNA and protein-related information.
- This approach can simplify the structure and reduce confusion.
- Allows for selective imports based on specific needs.
- Can be implemented through ViewModels and active filtering in the UI.

Functional Considerations in Genomics

- For copy number investigations, transcript_biotype "mRNA" may be most crucial.
- Protein configurations and different transcripts, while interesting, may not be essential for genomic analysis.
- Consider maintaining a split library for less essential information, allowing users to view it without basing further modeling on those projections.

Different Models for Different Purposes

1. Single Build Focus:
   - Use a single source and mRNA for simplicity.

2. Comprehensive Gene Information
   - Organize with gene as the highest level.
   - Include different sources within each gene.
   - Separate mRNA and transcript projections.

3. Design Mode
   - Gene list-based organization with all information present.
   - Allow for specific target alternatives when needed.

4. Simple All List
   - Create overviews based on current total gene loads.
   - Focus on simplifying and maintaining a single projection for synchronized information.

Flexibility in Implementation

- Consider implementing multiple import options based on specific needs.
- Use ViewModels and active filtering in the UI to manage selections and display.
- Balance between comprehensive data inclusion and practical usability for genomic analysis.

This approach provides a flexible framework that can adapt to various genomic data analysis needs while maintaining organization and accessibility.

Your detailed analysis provides valuable insights into genomic data organization and management. Here's a summary of the key points and some suggestions for implementation:

---------------------------------------
Genomic Data Management System
---------------------------------------
Ensembl and Genomic Data Organization
Ensembl is a comprehensive genome browser for vertebrate genomes, supporting research in comparative genomics, evolution, sequence variation, and transcriptional regulation. It provides gene annotations, computes multiple alignments, predicts regulatory functions, and collects disease-related data.

Utilizing GbKey and CDS for Data Interpretation
When analyzing genomic data, it's important to consider the GbKey field, particularly when it contains "CDS" (Coding Sequence). These entries typically denote protein transcripts and provide crucial information:

1. Protein Boundaries
   - The start position usually corresponds to the start codon.
   - The end position typically aligns with the stop codon.

2. Exon Notation
   - For proteins, "Exon 1" often refers to the start codon.
   - The last exon usually contains the stop codon.

3. Potential Misinterpretation
   - This notation, while logical, can be misleading.
   - For proteins, these positions represent the true start and end of the coding sequence.

4. mRNA vs. Protein Differences
   - mRNA transcripts generally span a larger area than the coding sequence.
   - mRNA transcripts typically begin upstream of the start codon.
   - The last exon in mRNA often extends beyond the stop codon.

Understanding these distinctions is crucial for accurate interpretation of genomic data, especially when comparing protein-coding sequences to full-length transcripts.

---------------------------------------
Genomic Data Organization and Management
---------------------------------------
Gene List Structure
- Unique GeneId for each gene, regardless of source
- Approximately 61,215 genes, including pseudogenes
- Potential for synonyms, but a complete gene list can be used for comprehensive searches
- ViewModels can be used to arrange data for easier viewing (e.g., by Chromosome/Molecule)

Transcript List
- 177,497 transcripts related to 61,215 genes
- Unique transcript IDs for each transcript
- Serves as a unique entry point to the assembly

Non-Gene/Transcript Items (Transcript Elements)
- 3,976,064 items (exons, CDS, and codons)
- Some CDS exon items appear twice
- Not all CDS items have start and end codons
- Exons without start/end codons are numbered "-1"

Transcript-Gene Relationships
- All transcripts reference a gene via ID
- "Unmatched" lists created for troubleshooting annotation file updates

Transcript Fields
- Standard fields: SeqName, Start, End, Score, Strand, Frame
- Attribute fields: GeneId, Db_Xref, Gb_Key, GeneName, Pseudo, Product, Transcript_Biotype

GeneId Notes
- Some transcripts may not have a corresponding "gene" line
- Option to create gene data models based on transcript information
- Differentiation between gene and transcript-derived information

Proposed Genome Data Model
1. Genome (list of molecules)
2. Molecule ≈ Chromosome (list of sequences)
3. Sequences ≈ Genes (list of entities)
4. Sequence entity ≈ exon, intron

This model aims to organize genomic data efficiently, allowing easy access to relevant features while minimizing data for better management. It also facilitates the creation of divisions for easier access and local storage of organized libraries.

---------------------------------------
Notes on GTF File Import and Data Model Optimization
---------------------------------------
Alternative Sources in Gene Model

The gene model used for data import allows multiple entries related to the same gene ID. When multiple lines are noted as "gene" in the data sources:

- A column with the header "source type 2" will appear in the data grid gene list
- If filled, a "gene name 2" column indicates a second source was found
- Multiple entries in the same GTF file are unexpected but don't halt the import process

Improving Performance

Current processing of the entire GDF file requires about 5 GB of RAM. While not an issue for Windows 10 systems, significant improvements are possible:

1. Data Reduction
   - Remove GTF fields not used in later stages
   - Consider removing rarely used fields like score and frame
   - CDS information (60-70% of data) may be superfluous for certain purposes

2. Data Type Optimization
   - Convert fields to enums where possible (e.g., source, feature type, strand, Gbkey, pseudo, gene biotype, transcript biotype)
   - Convert numeric fields to integers (start, end, frame, exon number)
   - Convert string values (like attribute lines) to byte arrays, converting back when needed:

     E.g. C#
     string s = "whatever";
     byte[] b = System.Text.Encoding.UTF8.GetBytes(s);
     string s = System.Text.Encoding.UTF8.GetString(b);

3. View Model Optimization
   - Release view models, retain only view projections
   - Build view window incrementally
   - Implement a wizard-like interface with dynamic button appearance
   - Perform view production on background threads

Your text provides a comprehensive overview of the data management and visualization process for genomic data. Here's a revised and structured version of the content focusing on the views for data evaluation:

---------------------------------------
Views for Genomic Data Evaluation
---------------------------------------
To assess the data used in creating curated files, we can implement various views that display the results of each import and processing step. These views allow verification of correct content extraction and processing. The following views can be accessed through a combo box selection:

1. Imported GTF Data
- Displays all lines from the annotation file
- Shows processed first columns and possible attribute fields
- Note: Not all attributes are available for all line feeds (e.g., no products for "gene" lines, protein IDs typically only for CDS lines)

Memory Consideration
- Full GTF file import with split content can consume ~6 GB of memory
- Consider alternative imports that compress data types or exclude non-essential information at this stage

2. Annotation Report
a) Assembly Comments
   - Header information from the assembly report file
b) Assembly Report Table
   - Contains assigned molecule information for each accession number
   - Used to assign molecules to entries in the annotation file

3. Unique Gene List
- Result of "Untangle GTF data" process
- List of all unique gene ID names

4. Unique Transcript List
- Result of "Create unique list of transcripts" process
- List of all unique transcripts

5. Exon, CDS, and Codon Information
- Result of "Process Exon/CDS" operation
- Note: CDS information may not be necessary if exon notation from the exome is used
- Consider including all information initially and filtering later as needed

Subviews:
a) Filtered list showing only the Exome
b) Filtered list of only CDS and codon information

Data Management Tip
Consider making a subselection of needed information and parsing some fields to less memory-intensive data types for improved manageability.

Implementation Notes:
- Use a combo box for easy switching between views
- Implement memory-efficient data structures and processing methods
- Consider background processing for large datasets to maintain UI responsiveness
- Provide options for filtering and customizing views based on user needs

By implementing these views, users can efficiently evaluate the genomic data at various stages of processing, ensuring accuracy and completeness of the curated files.

Your detailed description of the "TheGenome" browser model provides a comprehensive structure for organizing and accessing genomic data. Here's a summary and analysis of the proposed model:

---------------------------------------
TheGenome Browser Test Data Model for storage
---------------------------------------
1. Human Genome Data Model
- Contains high-level information about the genome assembly
- Includes source, file list, key, and various assembly details

2. Sequence Role
- Dictionary with a list of sequence roles (e.g., 24 chromosomes)

3. Molecules
- Keys: GenBankAccn, RefSeqAccn, UCSCStyleName
- Fields: Sequence name, role, assigned molecule, location type, length

4. Genes
- Keys: GeneId, DbRefXrefOne, DbRefXrefTwo
- Fields: SourceType, GeneBioType, Start, End, Strand, Description, Synonyms

5. Transcripts
- Keys: TranscriptId
- Fields: SourceName, DbRefXref, TranscriptBiotype, Start, End, Strand

6. Exon List
- Key: ExonNumber
- Fields: Start, End

7. CDS (Coding Sequences)
- Fields: Product, ProteinId, Start codon, Stop codon, CDS List

Analysis and Recommendations

1. Hierarchical Structure: The model follows a logical hierarchical structure from genome to molecules to genes to transcripts to exons/CDS. This allows for efficient data organization and retrieval.

2. Flexibility: The model accommodates various data sources and types, including RefSeq and Gnomon annotations.

3. Optimization: 
   - Use enums for fields with limited options (e.g., SourceType, Strand) to save memory.
   - Consider using integer IDs instead of strings for frequently used keys to improve performance.

4. Extensibility: The model allows for future additions, such as including unlocalized sequences or additional molecule types.

5. Data Compression: Implement data compression techniques for large text fields to reduce storage requirements.

6. Indexing: Create indexes on frequently queried fields (e.g., GeneId, TranscriptId) to improve search performance.

7. Version Management: Implement a system to handle transcript version numbers, allowing easy selection of the latest version.

8. Cross-referencing: Utilize star diagrams for efficient cross-referencing between different levels (e.g., molecules to genes, genes to transcripts).

9. Modularity: Consider implementing each major component (Genome, Molecule, Gene, Transcript, Exon, CDS) as separate classes or modules for better code organization and maintainability.

10. Data Validation: Implement validation checks to ensure data integrity, especially for fields with expected formats or value ranges.

![image](https://github.com/user-attachments/assets/24387cb1-ab87-4eff-9338-5ba5860a05aa)
