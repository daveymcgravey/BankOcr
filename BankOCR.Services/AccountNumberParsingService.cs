using BankOCR.Services.Interfaces;
using System.Linq;

namespace BankOCR.Services
{
    public class AccountNumberParsingService : IAccountNumberParsingService
    {
        public string ParseOcrInput(string input)
        {
            var newLinesplits = input.Split("\r\n").ToList();
            newLinesplits.RemoveAll(x => string.IsNullOrEmpty(x));
            var digitAsOcr = string.Concat(newLinesplits.Select(x => x.Substring(0, 3)));
            var zeroAsString = 
                " _ " +
                "| |" +
                "|_|";

            if (digitAsOcr.Equals(zeroAsString))
            {
                return "000000000";
            }

            return "";
        }
    }
}
