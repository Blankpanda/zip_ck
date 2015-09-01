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

namespace zipck
{
	/* this class deals with moving and creating directories based on user options */

	class FileManipulation
	{

		private string fullPath = "";
		public string MainDirPath
		{
			get { return fullPath; }
			set { MainDirPath = fullPath; }
		}
	   
		

		/* Creates a directory in the location of the users choice, for now we will default to downloads */
		public void CreateMainDirectory()
		{
			DateTime time = DateTime.Now;

			string Path = @"C:\Users\CalebsComp\Downloads\";
			string formattedTime = time
										.ToString()
										.Replace(":", "")
										.Replace("/", "");

			string TimeStampedPath = "Extracted on " + formattedTime;
			fullPath = Path + TimeStampedPath + @"\";
			
			Directory.CreateDirectory(fullPath);
		
		}

		// fuck this 
		private void MoveFilesToMainDirectory(string DestinationPath)
		{
			List<string> Paths = GuiInteraction.GetPathsList(); // list of all of the file names
			List<string> SourceDirectories =  new List<string>(); // parallel to Paths but the directories they're contained in

			//creates parallel list to hold source directories
			for (int i = 0; i < Paths.Count; i++)
			{
				FileInfo inf = new FileInfo(Paths[i]);
				SourceDirectories.Add(inf.DirectoryName);
			}
			
			Paths = GetFileNames(Paths); // re purpose the Paths list to hold only the file names.

			for (int i = 0; i < Paths.Count; i++)
			{
				string SourceFile = Path.Combine(SourceDirectories[i], Paths[i]);
				string DestinationFile = Path.Combine(DestinationPath + Paths[i]);
				FileInfo inf = new FileInfo(Paths[i]); // use this to get the directory that we want to move a file
				File.Copy(SourceFile, DestinationFile);
				
			}
		
		}

		/* add _Extracted stub to the end of a string */
		public string RenameDirectory(string Name)
		{            
			string EndStub = "_Extracted";
			string NewDirectory = Name + EndStub;

			return NewDirectory;
		}

		/* gets the file names of a list of paths and returns a list */
		public List<string> GetFileNames(List<string> list)
		{
			List<string> FileNames = new List<string>();
			for (int i = 0; i < list.Count; i++)
			{
				FileInfo inf = new FileInfo(list[i]);
				FileNames.Add(inf.Name);
			}

			return FileNames;
		}

		/* gets a single path and returns the file name of that path. */
		public string GetFileName(string path)
		{
			FileInfo inf = new FileInfo(path);

			string FileName = inf.Name.ToString();

			return FileName;
		}


		/* gets the files directory so we can create new  sub directories
		 * also aggregates a \ onto the end.*/
		public string GetFilesInDirectory(string FilePath)
		{
			FileInfo inf = new FileInfo(FilePath);
			string FileDirectory = inf.DirectoryName + @"\"; 

			return FileDirectory;
			
		}

	








	}

}
