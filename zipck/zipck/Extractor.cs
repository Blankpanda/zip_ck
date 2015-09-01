using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnrar.Common;
using NUnrar.Reader;
using NUnrar.Archive;
using System.IO.Compression;
using System.IO;
using System.Windows;
using System.Threading;

namespace zipck
{
    /* this class handles all of the extraction operation going on
      this includes the extraction 
    * of the following file types:
    * .rar   
    * .zip
    * .7z
       */
 
    class Extractor 
    {
        /* seperate thread to handle extraction */
      
        
        /* Lists to hold the seperated files */
       private static List<string> RarFiles = new List<string>(); // .rar
       private static List<string> SevFiles = new List<string>(); // .7z
       private static List<string> ZipFiles = new List<string>(); // .zip       

       /* all of the lists combined for "easier" iteration */
       private static List<string>[] TypesList = { RarFiles , SevFiles , ZipFiles };
       enum Files : int { Rar, SevenZip, Zip }; // 0 , 1 , 2
    
        /* list holds the paths to all of the extracted directories so we can move it to another directory */
       private static List<string> ExtractedDirectories = new List<string>();

        // this is used to move extracted folders to a directory that holds all of the extracted directories
       private string MainDirectoryPath = "";

 

       /* Extract all of the files to be passed to the FileManipulation Class */

        public string ExtractAllFiles()
        {
          
            SeperateByFileType();
            FileManipulation FM = new FileManipulation();
            /* goes through all of the lists and executes a certain extraction function based on that specific list */

            for (int i = 0; i < TypesList.Length; i++)
            {
                
                for (int j = 0; j < TypesList[i].Count; j++)
                {
                    
                    switch (i)
                    {
                        case (int)Files.Rar:
                            ExtractRarFiles(TypesList[i][j]);
                            break;

                        case (int)Files.SevenZip:
                            ExtractZipFiles(TypesList[i][j]);
                            break;

                        case (int)Files.Zip:
                            ExtractZipFiles(TypesList[i][j]);
                            break;
                        
                    }
                }
                
            }

            /* Move everything into another directory (Probably want to redo this)  */
           
            FM.CreateMainDirectory();
            string MainDirectoryPath = FM.MainDirPath;

            foreach (string ExtractedDir in ExtractedDirectories)
            {
                DirectoryInfo DirectoryInfo = new DirectoryInfo(ExtractedDir);
                try
                {
                    Directory.Move(ExtractedDir, Path.Combine(MainDirectoryPath, DirectoryInfo.Name));
                }
                catch (Exception)
                {
                    
                    
                }
                
            }

            OutputPath = MainDirectoryPath;
            return MainDirectoryPath;
            
        }
       

      


        /* iterates through the GuiInteration.Paths list and seperates the different file types. */
       private void SeperateByFileType()
       {
           GuiInteraction UserInformation = new GuiInteraction();
           List<string> Paths = UserInformation.Paths;

          
           /* seperates the files into different lists */
           for (int i = 0; i < Paths.Count; i++)
           {
               FileInfo inf = new FileInfo(Paths[i]);
               string CurrentFileExtension = inf.Extension;

               switch (CurrentFileExtension)
               {

                   case ".rar":

                       RarFiles.Add(Paths[i]);

                       break;

                   case ".7z":

                       SevFiles.Add(Paths[i]);

                       break;

                   case ".zip":


                       ZipFiles.Add(Paths[i]);

                       break;
                   
               }

           }   

           

       } 


        /* extracts .rar files */
       private void ExtractRarFiles(string FilePath)
       {
           FileManipulation FileMover = new FileManipulation(); // used to obtain and set various values based on the working directory	
       
           string PathStub = ""; 
           PathStub = FileMover.RenameDirectory(PathStub); //  PathStub = "_Extracted"

           string FullPath = FilePath + PathStub; // the file path with the new name ( target directory )

           RarArchive ExtractorRar = RarArchive.Open(FilePath); // open an extractor to begin extracting files

           
           //extracts and moves files
           foreach (RarArchiveEntry entry in ExtractorRar.Entries)
           {
             string NewDirectory = Path.Combine(FullPath, entry.FilePath); // creates the path for a New target directory
             entry.WriteToDirectory(NewDirectory);  //move the fiels oto the newely created directory
           }

           ExtractedDirectories.Add(FullPath); // creates a list of extracted directories so we can move them all to a single folder.		   
       }


       /* extracts .zip files */
       private void ExtractZipFiles(string FilePath)
       {
           FileManipulation FileMover = new FileManipulation();

           string PathStub =  "";
           PathStub = FileMover.RenameDirectory(PathStub);
           

           FileInfo FileInPath = new FileInfo(FilePath); // used to get the name and directory of the target file

           string FinalPath = FileInPath.DirectoryName + @"\" + FileInPath.Name + PathStub ; // the directory of the target file
                                                                                             // the name + the stub of the target file 
                                                                                            // used to create a new directory with the file

           ZipFile.ExtractToDirectory(FilePath, FinalPath);

           ExtractedDirectories.Add(FinalPath);

       }


       /* extracts .7z files */
       private void ExtractSevZFiles(string FilePath)
       {
           FileManipulation FileMan = new FileManipulation();

           string PathStub = "";
           PathStub = FileMan.RenameDirectory(PathStub);           
           string FinalPath = FilePath + PathStub;
      
           // SevenZipFile ZipExtract = new SevenZipFile()


           
       }

       public string OutputPath { get; set; }
    }
}
