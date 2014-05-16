/* 
 * VCardReader - A program that reads a VCard (.vcf) formatted file and outputs the data into a readable comma seperated values (csv) file
 * Version 1.0
 * Created by Prashanth Rajaram
 * Copyright © 2014
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace VCardReader
{
	class Program
	{
		public static List<VCard> vCards = new List<VCard>();
		public static List<int> BeginLines = new List<int>();
		public static List<int> EndLines = new List<int>();
		public static string ContactFileName = "";
		static void Main(string[] args)
		{
			var argumentLength = args.Length;			
			if (!(argumentLength > 0))
			{
				Console.WriteLine("Please enter the full path to the VCard file:\n");
				ContactFileName = Console.ReadLine();				
			}
			else
			{
				if (argumentLength > 1)
				{
					foreach (string arg in args)
					{
						ContactFileName += " " + arg.ToString();
					}
				}                
			}
			
			var fileLines = ReadFile(ContactFileName);
			ParseFileLines(fileLines);
			CreateVCards(fileLines);
			//PrintVCards();
			SaveCardsAsCSV();
		}

		private static void PrintVCards()
		{
			foreach (VCard vcard in vCards)
			{
				foreach(PropertyDescriptor descriptor in TypeDescriptor.GetProperties(vcard))
				{
					object value = descriptor.GetValue(vcard);
					if (value != null)
					{
						Console.WriteLine("{0}", value);
					}
					
				}                
			}
		}

		private static void CreateVCards(List<string> fileLines)
		{
			for(int index=0; index<Math.Min(BeginLines.Count, EndLines.Count); index++)
			{
				VCard card = new VCard();
				for (int index2 = BeginLines[index]; index2 < EndLines[index]; index2++)
				{
					InterpretLineElement(fileLines[index2], ref card);
				}
				card.BEGIN = fileLines[BeginLines[index]];
				card.END = fileLines[EndLines[index]];
				vCards.Add(card);
			}
			
		}

		private static void InterpretLineElement(string line, ref VCard card)
		{
		   int end = -1, begin = -1;
		   end = line.IndexOf(Enums.VCardMetaData.KeyValueSeperator);
		   begin = end + 1;
		   string val = line.Substring(begin);

		   if (val != null)
		   {
			   val = val.Replace(Enums.File.CsvDelimiter, " ");
		   }

		   if(line.StartsWith(Enums.VCardElements.VERSION))
		   {
			   card.VERSION = card.VERSION ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.N))
		   {
			   card.N = card.N ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.FN))
		   {
			   card.FN = card.FN ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.EMAIL))
		   {
			   card.EMAIL = card.EMAIL ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.TEL))
		   {
			   if (card.TEL == null)
			   {
				   card.TEL = val;
			   }
			   else
			   {
				   card.TEL += Enums.VCardMetaData.ValueJoinDelimiter + val;
			   }
		   }
		   else if (line.StartsWith(Enums.VCardElements.ORG))
		   {
			   card.ORG = card.ORG ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.PHOTO))
		   {
			   card.PHOTO = card.PHOTO ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.ADR))
		   {
			   card.ADR = card.ADR ?? val;               
		   }
		   else if (line.StartsWith(Enums.VCardElements.LABEL))
		   {
			   card.LABEL = card.LABEL ?? val;
		   }
		   else if (line.StartsWith(Enums.VCardElements.REV))
		   {
			   card.REV = card.REV ?? val;
		   }
		}

		private static void SaveCardsAsCSV()
		{
			var dirInfo = Directory.GetParent(ContactFileName);
			var dirName = dirInfo.ToString();
			string filePath = string.Format(@"{0}\\{1}.{2}", dirName,Enums.File.OutputName,Enums.File.CsvFormat);
			string delimiter = Enums.File.CsvDelimiter;
			string header = null;
			var c = new VCard();
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(c))
			{
				header += descriptor.Name + delimiter;               
			}       

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(header);


			var contactLines = new List<string>();
			foreach (VCard card in vCards)
			{
				string contactLine = null;
				foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(card))
				{
					object value = descriptor.GetValue(card);
					if (value != null)
					{
						contactLine += value.ToString() + delimiter;
					}
					else
					{
						contactLine += "" + delimiter;
					}
				}
				contactLines.Add(contactLine);
			}

			foreach (string line in contactLines)
			{
				sb.AppendLine(line);           
			}

			File.WriteAllText(filePath, sb.ToString());
			Console.WriteLine(string.Format("All contents successfully written to {0}", filePath));
		}

		private static void ParseFileLines(List<string> fileLines)
		{
			var numberOfLines = fileLines.Count;
			for (int index = 0; index < numberOfLines; index++)
			{
				if (fileLines[index].Contains(Enums.VCardElements.BEGIN))
				{
					BeginLines.Add(index);					
				}
				else if (fileLines[index].Contains(Enums.VCardElements.END))
				{
					EndLines.Add(index);
				}
			}
		}

		private static List<string> ReadFile(string contactFileName)
		{
			if (File.Exists(contactFileName))
			{
				return File.ReadAllLines(@contactFileName).ToList<string>();
			}
			else
			{
				return new List<string>();
			}
		}
	}
}
