using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Windows.Controls;
using System.IO;

namespace zipck
{
	/* functions that are used to interact with the GUI */
	class GuiInteraction
	{
		/*--- Private Memebers ---*/

		/* For message box interaction */
		private string pEmptyInputBoxMessage =             //  the user forgot to input data
			"Please enter in one or more files to be extracted.";
		private string pBadFileTypeMessage =                //   the user tried to enter the wrong file
			"One or more files have an invalid file type. \n Valid File Types: \n .zip \n .tar \n .jar \n .7z \n";
		private string pInvalidPathMessage =                //    the user didn't enter in a valid path
			"One or more of the following files do not have a valid path.";
		private string pSingleFileExtractedMessage =        //     the user is enter a file that has already been extracted (maybe I dont want to do this?)
			"This file is already extracted in the current directory. \n would you like to create another one?";
		private string pNothingToRemoveMessage =           // the user tried to remove nothing
			"Please select a record to remove.";
	   

		private static List<string> pPaths = new List<string>(); // used to store all of the entered file paths
		private static List<string> pPaths_Temp = new List<string>(); // used to temp. hold the entered file paths when the list needs to be cleared


		/*--- Public Memebers ---*/
		public string EmptyInputBoxMessage
		{
			get { return pEmptyInputBoxMessage; }
			set { EmptyInputBoxMessage = pEmptyInputBoxMessage; }
		}
		public string BadFileTypeMessage
		{
			get { return pBadFileTypeMessage; }
			set { BadFileTypeMessage = pBadFileTypeMessage; }
		}
		public string InvalidPathMessage
		{
			get { return pInvalidPathMessage; }
			set { InvalidPathMessage = pInvalidPathMessage; }
		}
		public string SingleFileExtractedMessage
		{
			get { return pSingleFileExtractedMessage; }
			set { SingleFileExtractedMessage = pSingleFileExtractedMessage; }
		}	
		public string NothingToRemoveMessage
		{
			get { return pNothingToRemoveMessage;}
			set { NothingToRemoveMessage = pNothingToRemoveMessage;}
		}
	
		
		public List<string> Paths
		{
			get { return pPaths; }
			set { Paths = pPaths; }
		}
		

		/*--- Public methods ---*/

		/* open DLG and the user file selection options */
		public void OpenFIleDialogMenu()
		{
			OpenFileDialog fileOpener = new OpenFileDialog(); // user searches for the file he wants to input
			bool? OFD_result = false;                        // used when File Dialog is opened to execute something if the user clicks OK



			//options           
			fileOpener.Multiselect = true;
			fileOpener.Title = "Mass File Extractor";          
			fileOpener.Filter = "Compress (*.rar, *.7z, *.zip, *.tar) |*.rar; *.7z; *.zip; *.tar | All (*.*) | *.*";
			
			//open the File Dialog and have the user select compressed files
			OFD_result = fileOpener.ShowDialog();
			
			if (OFD_result == true)
			{
				// populate an array ( UGGHH ) that corresponds with the paths Listbox
				// and add it to Paths list
				 
				string[] files = fileOpener.FileNames;


				for (int i = 0; i <= files.Length - 1; i++)
				{
					pPaths.Add(files[i]);
				}				
			  
			}
			else
			{
				pPaths_Temp = PopulateTempList(pPaths,pPaths_Temp); // store the contents of the list temporarliy so we can save the contents on the list when the user clicks cancel
				ClearList(pPaths);                
				fileOpener = new OpenFileDialog();
				UpdatePathsList();
				

			}
		 
		}

		/* used to enforce certain Isenabled and Isdisabled properties for controls on the form */
		public void EnforceButtonOptions(List<Button> buttons)
		{
			for (int i = 0; i < buttons.Count; i++)
			{
				if (pPaths.Count >= 1)
				{
					buttons[i].IsEnabled = true;
				}
				else
				{
					buttons[i].IsEnabled = false;
				}
			}

		}

		/* when the extract button is clicked we want to make sure that only the right file types are being entered. */
		public bool ValidFileCheck()
		{
			string[] FileTypes = { ".zip" , ".7z" , ".rar"}; // used to compare FileInfo values 
			int counter = 0; // this counter is incremented when the file has the correct format.
		   
			// checks file paths for anything that isn't .rar, .7z or .zip. 

			for (int i = 0; i <= FileTypes.Length - 1; i++)
			{

				for (int j = 0; j <= pPaths.Count - 1; j++)
				{
					FileInfo FI = new FileInfo(pPaths[j]);

					if (FI.Extension == FileTypes[i])
					{
						counter++;
					}
				}
			}

			
			// if the counter is equal to the Count of the list then all of the files have the correct type and return true.
			// if the counter is not equal to the Count of the list then one or more hav the wrong file type.

			if (counter == pPaths.Count)
			{
				
				return true;    
			}else
			{
				return false;
			}
			
		}


		/*--- List manipulation ---*/



		/* populate a temporary list */
		public static List<string> PopulateTempList(List<string> MasterList , List<string> Temp )
		{			
			// (re)populate the temporary list
			for (int i = 0; i <= MasterList.Count - 1; i++)
			{                               
				Temp.Add(MasterList[i]);
				if (MasterList.Count == i)
				{
					break;
				}            
			}

			return Temp;
		}

		// puts the contents of the temporary folder into the first list.
		public void UpdatePathsList()
		{
			if (pPaths_Temp.Count >= 1)
			{
				pPaths = pPaths_Temp;    
			}
			
		}
		// clears the path list so if the user clicks cancel the contents selected dont be placed into the list
		public void ClearList(List<string> list)
		{
			list.Clear();
		}
		// Overload to remove the contents of a ListBox as well
		public void ClearList(List<string> list , ListBox listbox)
		{
			list.Clear();
			listbox.Items.Clear();
		}


		public static List<string> GetPathsList()
		{
			return pPaths;
		}

	


		/*--- Listbox Manipulation ---*/




		/* run this everytime values are being changed from the list box.
		   for example when we remove items from the list 
			we simply update after every removal or addition*/ 
		
		public void UpdateListBox(ListBox lb)
		{
			lb.Items.Clear();
			for (int i = 0; i < pPaths.Count; i++)
			{
				lb.Items.Add(pPaths[i]);
			}

		}   



		/*Deletes an item from the list box where p = Listbox.SelectedItem 
		 * and lb is the ListBox
		 * also removes this value from the list */

		public void DeleteItem(object p, ListBox lb)
		{
			
			// removes the selected item
			if (lb.SelectedIndex != -1)
			{
				lb.Items.Remove(p);
			}

			//update the pPaths List.
			string RemovedItem = p.ToString();

			if (!(string.IsNullOrEmpty(RemovedItem)))
			{
				for (int i = 0; i < pPaths.Count; i++)
				{
					if (pPaths[i] == RemovedItem)
					{
							pPaths.RemoveAt(i); //removes the value based on what was stored in the list box.
					}

				}
			}
		}






		
	}

	}

   

