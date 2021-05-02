using BankOCR.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BankOCR.Services
{
    public class AccountNumberParsingService : IAccountNumberParsingService
    {
        public string ParseOcrInput(string input)
        {
            var newLinesplits = input.Split("\r\n").ToList();
            newLinesplits.RemoveAll(x => string.IsNullOrEmpty(x));
            
            var ocrInputs = new List<string>();
            for (int i = 0; i < 27; i = i + 3)
            {
                ocrInputs.Add(string.Concat(newLinesplits.Select(x => x.Substring(i, 3))));
            }

            var ocrInputsAsDigits = new List<int>();
            foreach (var ocrInput in ocrInputs)
            {
                switch (ocrInput)
                {
                    case    " _ " +
                            "| |" +
                            "|_|":
                        ocrInputsAsDigits.Add(0);
                        break;
                    default:
                        break;
                }
            }

            return string.Join(string.Empty, ocrInputsAsDigits.Select(x => x));
        }
    }
}
