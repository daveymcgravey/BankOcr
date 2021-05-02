﻿using BankOCR.Services.Interfaces;
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

        private IReadOnlyDictionary<string, int> ocrInputToDigitMap = new Dictionary<string, int>
        {
            {zeroAsString, 0},
            {oneAsString, 1},
            {twoAsString, 2},
            {threeAsString, 3},
            {fourAsString, 4},
            {fiveAsString, 5},
            {sixAsString, 6},
            {sevenAsString, 7},
            {eightAsString, 8},
            {nineAsString, 9},
        };

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
                int digit;
                if(ocrInputToDigitMap.TryGetValue(ocrInput, out digit)) { ocrInputsAsDigits.Add(digit); }
            }

            //Return the identified digits concatenated as a string
            return string.Join(string.Empty, ocrInputsAsDigits.Select(x => x));
        }
    }
}
