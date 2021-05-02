using BankOCR.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

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

        public string ParseOcrInput(string input)
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

            //Match each ocr input to a digit
            var ocrInputsAsDigits = new List<int>();
            foreach (var ocrInput in ocrInputs)
            {
                switch (ocrInput)
                {
                    case zeroAsString:
                        ocrInputsAsDigits.Add(0);
                        break;
                    case oneAsString:
                        ocrInputsAsDigits.Add(1);
                        break;
                    case twoAsString:
                        ocrInputsAsDigits.Add(2);
                        break;
                    case threeAsString:
                        ocrInputsAsDigits.Add(3);
                        break;
                    case fourAsString:
                        ocrInputsAsDigits.Add(4);
                        break;
                    case fiveAsString:
                        ocrInputsAsDigits.Add(5);
                        break;
                    case sixAsString:
                        ocrInputsAsDigits.Add(6);
                        break;
                    case sevenAsString:
                        ocrInputsAsDigits.Add(7);
                        break;
                    case eightAsString:
                        ocrInputsAsDigits.Add(8);
                        break;
                    case nineAsString:
                        ocrInputsAsDigits.Add(9);
                        break;
                    default:
                        break;
                }
            }

            //Return the identified digits concatenated as a string
            return string.Join(string.Empty, ocrInputsAsDigits.Select(x => x));
        }
    }
}
