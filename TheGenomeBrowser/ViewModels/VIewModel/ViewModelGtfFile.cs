using System.Windows.Forms;

namespace TheGenomeBrowser.ViewModels.VIewModel
{


    /// <summary>
    /// class that holds the view model for the imported GTF file (and contains most business logic)
    /// </summary>
    public class ViewModelGtfFile
    {

        #region methods

        /// <summary>
        /// public static function that throws the user a message box to ask if he wants to continue without an assembly report
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static bool AskUserToContinueWithoutAssemblyReport()
        {
            bool continueWithoutAssemblyReport = false;
            DialogResult result = MessageBox.Show("Do you want to continue without the use of an assembly report?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                // continue with the rest of the code
                continueWithoutAssemblyReport = true;
            }
            else
            {
                // handle the case when the user doesn't want to continue without the assembly report
                continueWithoutAssemblyReport = false;
            }

            return continueWithoutAssemblyReport;
        }

        #endregion



    }

}