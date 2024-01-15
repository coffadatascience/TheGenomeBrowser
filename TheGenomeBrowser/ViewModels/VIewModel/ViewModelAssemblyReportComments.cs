using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TheGenomeBrowser.ViewModels.VIewModel.GenericModels;

namespace TheGenomeBrowser.ViewModels.VIewModel
{

    /// <summary>
    /// class with a view model that acts as a container for the assembly report comments and placed them in a format that can be displayed in a grid
    /// this model inherits from the generic model settings information
    /// </summary>
    public class ViewModelAssemblyReportComments : ViewModelGenericModelSettingInformationList
    {

        #region methods

        //procedur that take a DataModelAssemblyReport, and placed the comments in a format that can be displayed in a grid. The row header of each cell is the description of the comment
        //the column header is the name of the comment
        public void SetDataModelAssemblyReportComments(DataModels.NCBIImportedData.DataModelAssemblyReport dataModelAssemblyReport)
        {
            //var int for column index for comment info
            int columnIndexCommentInfo = 0;
            // var int for row index
            int rowIndex = 0;

            //create a new list of row data
            this.ListDataRowData =  new List<RowData>();

            //create a new list of column data
            this.ListDataColumnData = new List<ColumnData>();

            //new column data
            ColumnData columnData = new ColumnData(columnIndexCommentInfo, "Assembly comments");

            //add a new column data to the list of column data
            this.ListDataColumnData.Add(columnData);

            //create a new row data
            RowData rowData = new RowData(rowIndex);
            //set the row header text to the name of the variable of AssemblyName
            rowData.RowHeaderText = "AssemblyName";
            //set the label to the name of the variable of AssemblyName
            rowData.Label = "AssemblyName";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData);
            //create a data cell value
            var dataCellValue = new DataCellItem(rowIndex, dataModelAssemblyReport.AssemblyName);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue);
            //count the row index
            rowIndex++;


            //create a new row data
            RowData rowData2 = new RowData(rowIndex);
            //set the row header text to the name of the variable of Description
            rowData2.RowHeaderText = "Description";
            //set the label to the name of the variable of Description
            rowData2.Label = "Description";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData2);
            //create a data cell value
            var dataCellValue2 = new DataCellItem(rowIndex, dataModelAssemblyReport.Description);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue2);
            //count the row index
            rowIndex++;


            //create a new row data
            RowData rowData3 = new RowData(rowIndex);
            //set the row header text to the name of the variable of OrganismName
            rowData3.RowHeaderText = "OrganismName";
            //set the label to the name of the variable of OrganismName
            rowData3.Label = "OrganismName";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData3);
            //create a data cell value
            var dataCellValue3 = new DataCellItem(rowIndex, dataModelAssemblyReport.OrganismName);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue3);
            //count the row index
            rowIndex++;


            //create a new row data
            RowData rowData4 = new RowData(rowIndex);
            //set the row header text to the name of the variable of Taxid
            rowData4.RowHeaderText = "Taxid";
            //set the label to the name of the variable of Taxid
            rowData4.Label = "Taxid";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData4);
            //create a data cell value
            var dataCellValue4 = new DataCellItem(rowIndex, dataModelAssemblyReport.TaxId);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue4);
            //count the row index
            rowIndex++;


            RowData rowData5 = new RowData(rowIndex);
            //set the row header text to the name of the variable of BioProject
            rowData5.RowHeaderText = "BioProject";
            //set the label to the name of the variable of BioProject
            rowData5.Label = "BioProject";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData5);
            //create a data cell value
            var dataCellValue5 = new DataCellItem(rowIndex, dataModelAssemblyReport.BioProject);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue5);
            rowIndex++;


            RowData rowData6 = new RowData(rowIndex);
            //set the row header text to the name of the variable of Submitter
            rowData6.RowHeaderText = "Submitter";
            //set the label to the name of the variable of Submitter
            rowData6.Label = "Submitter";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData6);
            //create a data cell value
            var dataCellValue6 = new DataCellItem(rowIndex, dataModelAssemblyReport.Submitter);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue6);
            rowIndex++;


            RowData rowData7 = new RowData(rowIndex);
            //set the row header text to the name of the variable of Date
            rowData7.RowHeaderText = "Date";
            //set the label to the name of the variable of Date
            rowData7.Label = "Date";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData7);
            //create a data cell value
            var dataCellValue7 = new DataCellItem(rowIndex, dataModelAssemblyReport.Date);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue7);
            rowIndex++;

            //create a new row data
            RowData rowData8 = new RowData(rowIndex);
            //set the row header text to the name of the variable of Synonyms
            rowData8.RowHeaderText = "Synonyms";
            //set the label to the name of the variable of Synonyms
            rowData8.Label = "Synonyms";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData8);
            //create a data cell value
            var dataCellValue8 = new DataCellItem(rowIndex, dataModelAssemblyReport.Synonyms);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue8);
            rowIndex++;

            //create a new row data
            RowData rowData9 = new RowData(rowIndex);
            //set the row header text to the name of the variable of AssemblyType
            rowData9.RowHeaderText = "AssemblyType";
            //set the label to the name of the variable of AssemblyType
            rowData9.Label = "AssemblyType";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData9);
            //create a data cell value
            var dataCellValue9 = new DataCellItem(rowIndex, dataModelAssemblyReport.AssemblyType);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue9);
            rowIndex++;

            //create a new row data
            RowData rowData10 = new RowData(rowIndex);
            //set the row header text to the name of the variable of ReleaseType
            rowData10.RowHeaderText = "ReleaseType";
            //set the label to the name of the variable of ReleaseType
            rowData10.Label = "ReleaseType";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData10);
            //create a data cell value
            var dataCellValue10 = new DataCellItem(rowIndex, dataModelAssemblyReport.ReleaseType);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue10);
            rowIndex++;

            //create a new row data
            RowData rowData11 = new RowData(rowIndex);
            //set the row header text to the name of the variable of AssemblyLevel
            rowData11.RowHeaderText = "AssemblyLevel";
            //set the label to the name of the variable of AssemblyLevel
            rowData11.Label = "AssemblyLevel";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData11);
            //create a data cell value
            var dataCellValue11 = new DataCellItem(rowIndex, dataModelAssemblyReport.AssemblyLevel);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue11);
            rowIndex++;

            //create a new row data
            RowData rowData12 = new RowData(rowIndex);
            //set the row header text to the name of the variable of GenomeRepresentation
            rowData12.RowHeaderText = "GenomeRepresentation";
            //set the label to the name of the variable of GenomeRepresentation
            rowData12.Label = "GenomeRepresentation";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData12);
            //create a data cell value
            var dataCellValue12 = new DataCellItem(rowIndex, dataModelAssemblyReport.GenomeRepresentation);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue12);
            rowIndex++;

            //create a new row data
            RowData rowData13 = new RowData(rowIndex);
            //set the row header text to the name of the variable of RefSeqCategory
            rowData13.RowHeaderText = "RefSeqCategory";
            //set the label to the name of the variable of RefSeqCategory
            rowData13.Label = "RefSeqCategory";
            //add the row data to the list of row data
            this.ListDataRowData.Add(rowData13);
            //create a data cell value
            var dataCellValue13 = new DataCellItem(rowIndex, dataModelAssemblyReport.RefSeqCategory);
            //add the data cell value to the row data
            columnData.ListDataCellItem.Add(dataCellValue13);
            rowIndex++;


        }


        #endregion

    }


}
