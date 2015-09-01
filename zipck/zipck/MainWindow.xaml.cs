using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Threading;
using NUnrar.Common;
using NUnrar.Reader;
using NUnrar.Archive;
using System.IO.Compression;
using System.IO;

namespace zipck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {

    
        
        /* list of button controls to iterate through when nesscary */
        private static List<Button> guiButtons = new List<Button>();

        public MainWindow()
        {
            InitializeComponent();


        }
        private void mainFrm_Loaded(object sender, RoutedEventArgs e)
        {
            /* build guiButtons List */
            guiButtons.Add(extractButton);
            guiButtons.Add(RemoveButton);
            guiButtons.Add(ClearButton);

            /* set button options */
            GuiInteraction guiInitializer = new GuiInteraction();
            guiInitializer.EnforceButtonOptions(guiButtons);

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }


        private void AddFIleButton_Click(object sender, RoutedEventArgs e)
        {
            GuiInteraction FileBrowser = new GuiInteraction();
            FileBrowser.OpenFIleDialogMenu();
            FileBrowser.UpdateListBox(filePathsListBox);
            FileBrowser.EnforceButtonOptions(guiButtons);  // if nothing is occupying the list box then we should disable remove and extract            
        }
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            GuiInteraction Remover = new GuiInteraction();
            try
            {
                Remover.DeleteItem(filePathsListBox.SelectedItem, filePathsListBox); // Removes the selected item and updates the list
            }
            catch (Exception)
            {
                MessageBox.Show(Remover.NothingToRemoveMessage, 
                                "Mass File Extractor",
                                MessageBoxButton.OK);
            }

            // if nothing is occupying the list box then we should disable remove and extract
            Remover.UpdateListBox(filePathsListBox);
            Remover.EnforceButtonOptions(guiButtons);
        }


        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            GuiInteraction RemoveAll = new GuiInteraction();
            RemoveAll.ClearList(RemoveAll.Paths, filePathsListBox);
            RemoveAll.UpdateListBox(filePathsListBox);

            // if nothing is occupying the list box then we should disable remove , extract and clear
            RemoveAll.EnforceButtonOptions(guiButtons);


        }

        /* extracts the files and opens the InProgress form to show the user the extractors progress. */
        private void ExtractButton_Click(object sender, RoutedEventArgs e)
        {
           
            GuiInteraction Validation = new GuiInteraction(); // using tihs to check if the right information was put in.
            FileManipulation fm = new FileManipulation(); //using this to create the output directory.
            Extractor Extraction = new Extractor(); // extractor


            MessageBoxResult ProceedResult = new MessageBoxResult(); 
            ProceedResult = MessageBox.Show("Would you like to begin extraction? (this window will close and reopen when the task is done",
                            "Mass File Extractor",
                            MessageBoxButton.YesNo);

            if (ProceedResult == MessageBoxResult.Yes)
            {

                if (Validation.ValidFileCheck())
                {
                    mainFrm.Hide(); //Minimize the main window                
                    Extraction.ExtractAllFiles(); // extract the files listed on the Paths list. 
                    string OutputPath = Extraction.OutputPath;
                    mainFrm.Show(); // after everything is done reopen the first form.
                    MessageBox.Show("Files Extracted to " + OutputPath, "Mass File Extractor", MessageBoxButton.OK);
                }
                else
                {
                    MessageBox.Show(Validation.InvalidPathMessage, "Mass File Extractor", MessageBoxButton.OK);
            
                }

            }

        }

        private void OptionsButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

    }
}


          


//            ▄█▀█▀█▄
//         ▄█▀　 █　 ▀█▄
//       ▄█▀　　　　　　▀█▄
//       █　　　　　　　　 █
//       █　　　　　　　　 █
//       ▀█▄▄　　█　　　▄█▀
//        █　　▄▀▄　　█
//        █　▀　　　▀　█
//        █　　　　　　█
//        █　　　　　　█
//        █　　　　 　 █        MEMES :^)
//        █　　　　　　█
//        █　　　　　　█
//   ▄█▀▀█▄█　　　　　　  █▄█▀█▄
// ▄█▀▀　　　　▀　　　　　　　　　▀▀█
//█▀　　　　　　　　　　　　　　　　　▀█
//█　　　　　　　　　　　　　　　　　　 █
//█　　　　　　　　　　　▄█▄　　　　　　█
//▀█　　　　　　　　　█▀　▀█　　　　　 █▀
// ▀█▄　　　　　　█▀　　　▀█　　　▄█▀
//   ▀█▄▄▄█▀　　　　　　▀█▄▄▄█▀