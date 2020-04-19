using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

namespace CSharp_Shell
{

    public static class Program 
    {
    	static Dictionary<int, string> groupId;
    	static Dictionary<char, string> digitNames;
    	static Dictionary<char, string> tenthDigits;
    	static Dictionary<char, string> teenDigits;
    	static int noOfGroups;
    	static bool negativeNumber;
    	static ConsoleColor DEFAULT_COLOUR = Console.ForegroundColor;
    	
        public static void Main() 
        {
            InitialiseDictionaries();
            bool endApp = true;
            long number = 0;
            
            while (endApp)
            {
            	Console.WriteLine("Put in a number:");
            	string numberInString = Console.ReadLine();
            	
            	if (numberInString[0] == '-')
            	{
            		negativeNumber = true;
            		numberInString = numberInString.Remove(0, 1);
            		Console.WriteLine(numberInString);
            	}
            	
                try
                {
                    number = Convert.ToInt64(numberInString);
                    Console.WriteLine("In words: " + GetWordsEquivalent(number.ToString()));
                }
                catch (FormatException e)
                {
                    Debug.Log("Error: " + e.Message);
                    printErrorMessage("Please put in only whole numbers.");
                }
                catch(OverflowException e)
                {
                	Debug.Log("Error: " + e.Message);
                    printErrorMessage(
                    	string.Format("Number is too large. \nPlease put in nunbers between {0} and {1}", long.MinValue, long.MaxValue)
                    	); 
                }
                finally
                {
                    Console.WriteLine("\nRestart? Press Y for Yes or any key to end.");
                    if (Console.ReadKey<string>().ToLower() != "y")
                        endApp = false;
                    Console.WriteLine("\n\n");
                }
            }
            
            //Console.ReadLine();
        }
        
        private static void printErrorMessage(string message)
        {
        	Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = DEFAULT_COLOUR;
        }
        
        public static string GetWordsEquivalent(string number)
        {
        	StringBuilder result = new StringBuilder();
            List<string> listNumberGroups = DivideNumberIntoGroups(number);
            noOfGroups = listNumberGroups.Count;
            
            if (negativeNumber)
            	result.Append("nagative ");
            	
            for (int i = listNumberGroups.Count - 1; i >= 0; i--)
            {
            	result.Append(ConvertGroupToWords(listNumberGroups[i]));
            	noOfGroups--;
            }
            
            return result.ToString().Remove(result.Length - 1, 1);
        }
        
        public static string ConvertGroupToWords(string numberGroup)
        {
            StringBuilder wordBuilder = new StringBuilder();
            
            for (int i = numberGroup.Length - 1; i >= 0; i--)
            {
            	if (i == 2 && numberGroup[i] != '0')
            	{
            		wordBuilder.Append(digitNames[numberGroup[i]] + " hundred");
            	}
            	
            	if(i == 1 && numberGroup.Length == 3 && (numberGroup[i] != '0' || numberGroup[i - 1] != '0'))
            	{
            		wordBuilder.Append(" and");
            	}
            	if(i == 1 && numberGroup[i] == '1' && numberGroup[i - 1] != '0')
            	{
            		wordBuilder.Append(" " + teenDigits[numberGroup[i - 1]]);
            		break;
            	}
            	if (i == 1  && numberGroup[i] != '0')
            	{
            		wordBuilder.Append(" " + tenthDigits[numberGroup[i]]);
            	}
            	else if(i == 0 && numberGroup[i] != '0')
            	{
            		if(numberGroup.Length > 1 && numberGroup[i + 1] != '0')
            		{
            			wordBuilder.Append("-");
            		}
            		else
            		{
            			wordBuilder.Append(" ");
            		}
            		wordBuilder.Append(digitNames[numberGroup[i]]);
            	}
            }
            
            if (wordBuilder.ToString() != "")
                wordBuilder.Append(" " + groupId[noOfGroups - 1] + ", ");
            
            return wordBuilder.ToString().Trim();
        }
        
        public static List<string> DivideNumberIntoGroups(string number)
        {
            List<string> dividedNumber = new List<string>();
            StringBuilder groupBuilder = new StringBuilder();
            
            for (int i = number.Length - 1; i >= 0; i--)
            {
                groupBuilder.Append(number[i]);
             
                if ((number.Length - i) % 3 == 0 || i == 0)
                {
                    dividedNumber.Add(groupBuilder.ToString());
                    groupBuilder = new StringBuilder();
                    continue;
                }
            }
            
            return dividedNumber;
        }
        
        public static void InitialiseDictionaries()
        {
        	groupId = new Dictionary<int, string>();
         	groupId.Add(0, "");
         	groupId.Add(1, "thousand");
           	groupId.Add(2, "million");
            groupId.Add(3, "billion");
            groupId.Add(4, "trillion");
            groupId.Add(5, "quadrillion");
            
            digitNames = new Dictionary<char, string>();
            digitNames.Add('0', "");
            digitNames.Add('1', "one");
            digitNames.Add('2', "two");
            digitNames.Add('3', "three");
            digitNames.Add('4', "four");
            digitNames.Add('5', "five");
            digitNames.Add('6', "six");
            digitNames.Add('7', "seven");
            digitNames.Add('8', "eight");
            digitNames.Add('9', "nine");
            
            tenthDigits = new Dictionary<char, string>();
            tenthDigits.Add('0', "");
            tenthDigits.Add('1', "ten");
            tenthDigits.Add('2', "twenty");
            tenthDigits.Add('3', "thirty");
            tenthDigits.Add('4', "forty");
            tenthDigits.Add('5', "fifty");
            tenthDigits.Add('6', "sixty");
            tenthDigits.Add('7', "seventy");
            tenthDigits.Add('8', "eighty");
            tenthDigits.Add('9', "ninety");
            
            teenDigits = new Dictionary<char, string>();
            teenDigits.Add('0', "");
            teenDigits.Add('1', "eleven");
            teenDigits.Add('2', "twelve");
            teenDigits.Add('3', "thirteen");
            teenDigits.Add('4', "forteen");
            teenDigits.Add('5', "fifteen");
            teenDigits.Add('6', "sixteen");
            teenDigits.Add('7', "seventeen");
            teenDigits.Add('8', "eighteen");
            teenDigits.Add('9', "nineteen");
        }
    }
}