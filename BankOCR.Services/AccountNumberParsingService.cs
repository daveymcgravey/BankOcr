using BankOCR.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BankOCR.Services
{
    public class AccountNumberParsingService : IAccountNumberParsingService
    {
        private const string zeroAsString =
            " _ " +
            "| |" +
            "|_|";
        private const string oneAsString =
            "   " +
            "  |" +
            "  |";
        private const string twoAsString =
            " _ " +
            " _|" +
            "|_ ";
        private const string threeAsString =
            " _ " +
            " _|" +
            " _|";

        private const string fourAsString =
            "   " +
            "|_|" +
            "  |";

        private const string fiveAsString =
            " _ " +
            "|_ " +
            " _|";

        private const string sixAsString =
            " _ " +
            "|_ " +
            "|_|";

        private const string sevenAsString =
            " _ " +
            "  |" +
            "  |";

        private const string eightAsString =
            " _ " +
            "|_|" +
            "|_|";

        private const string nineAsString =
            " _ " +
            "|_|" +
            " _|";

        private IReadOnlyDictionary<string, string> ocrInputToDigitMap = new Dictionary<string, string>
        {
            {zeroAsString, "0"},
            {oneAsString,"1"},
            {twoAsString, "2"},
            {threeAsString, "3"},
            {fourAsString, "4"},
            {fiveAsString, "5"},
            {sixAsString, "6"},
            {sevenAsString, "7"},
            {eightAsString,"8"},
            {nineAsString, "9"},
        };

        public string ParseOcrInput(string input)
        {
            var ocrInputs = ConvertOcrInputToDigitsAsStrings(input);

            //Match each ocr input to a digit
            var ocrInputsAsDigits = new List<string>();
            foreach (var ocrInput in ocrInputs)
            {
                string digitAsString;
                if (ocrInputToDigitMap.TryGetValue(ocrInput, out digitAsString)) { ocrInputsAsDigits.Add(digitAsString); } else { ocrInputsAsDigits.Add("?"); }
            }

            //Return the identified digits concatenated as a string
            return string.Join(string.Empty, ocrInputsAsDigits);
        }

        public IEnumerable<string> GetPossibleAccountNumbers(string input)
        {
            var ocrInputs = ConvertOcrInputToDigitsAsStrings(input);
            var parsedAccountNumber = ParseOcrInput(input);
            var possibleAccountNumbers = new List<string>();

            var counter = -1;
            foreach (var ocrInput in ocrInputs)
            {
                counter++;
                //Try and get the string representation of the digit
                for (int i = 0; i < ocrInput.Length; i++)
                {
                    //Replace a character and see if can get a different value from the dictionary
                    StringBuilder sb = new StringBuilder(ocrInput);
                    sb[i] = ' ';
                    var ocrInputWithNewCharacter = sb.ToString();

                    string stringAsDigit;
                    //Try and see if there is a value in the dictionary for the new string with the new character
                    if (ocrInputToDigitMap.TryGetValue(ocrInputWithNewCharacter, out stringAsDigit) && ocrInputToDigitMap.FirstOrDefault(x => x.Value == stringAsDigit).Key != ocrInput)
                    {
                        //If it exists we need to figure ut what the whole string would be and add it in
                        //First, get the where we are in the account number
                        var possibleAccountNumber = parsedAccountNumber.Substring(0, counter) + stringAsDigit + parsedAccountNumber.Substring(counter + 1, parsedAccountNumber.Length - counter - 1);
                        possibleAccountNumbers.Add(possibleAccountNumber);
                    }

                    sb[i] = '_';
                    ocrInputWithNewCharacter = sb.ToString();

                    if (ocrInputToDigitMap.TryGetValue(ocrInputWithNewCharacter, out stringAsDigit) && ocrInputToDigitMap.FirstOrDefault(x => x.Value == stringAsDigit).Key != ocrInput)
                    {
                        var possibleAccountNumber = parsedAccountNumber.Substring(0, counter) + stringAsDigit + parsedAccountNumber.Substring(counter + 1, parsedAccountNumber.Length - counter - 1);
                        possibleAccountNumbers.Add(possibleAccountNumber);
                    }

                    sb[i] = '|';
                    ocrInputWithNewCharacter = sb.ToString();

                    if (ocrInputToDigitMap.TryGetValue(ocrInputWithNewCharacter, out stringAsDigit) && ocrInputToDigitMap.FirstOrDefault(x => x.Value == stringAsDigit).Key != ocrInput)
                    {
                        var possibleAccountNumber = parsedAccountNumber.Substring(0, counter) + stringAsDigit + parsedAccountNumber.Substring(counter + 1, parsedAccountNumber.Length - counter - 1);
                        possibleAccountNumbers.Add(possibleAccountNumber);
                    }
                }
            }

            possibleAccountNumbers.RemoveAll(x => !Regex.IsMatch(x, @"[0-9]{9}"));
            return possibleAccountNumbers;
        }

        private IEnumerable<string> ConvertOcrInputToDigitsAsStrings(string input)
        {
            //Split the input in to each individual line of the OCR input and remove the first line as this is always blank so not needed for parsing
            var newLinesplits = input.Split("\r\n").ToList();
            newLinesplits.RemoveAll(x => string.IsNullOrEmpty(x));

            //Take 3 chars from each line to create each individual digit as a separate string
            var ocrInputs = new List<string>();
            for (int i = 0; i < 27; i = i + 3)
            {
                ocrInputs.Add(string.Concat(newLinesplits.Select(x => x.Substring(i, 3))));
            }

            return ocrInputs;
        }
    }
}
