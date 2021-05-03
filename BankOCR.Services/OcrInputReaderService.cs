using BankOCR.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BankOCR.Services
{
    public class OcrInputReaderService : IOcrInputReaderService
    {
        private readonly IAccountNumberParsingService accountNumberParsingService;
        private readonly IAccountNumberValidationService accountNumberValidationService;
        public OcrInputReaderService(
            IAccountNumberParsingService accountNumberParsingService,
            IAccountNumberValidationService accountNumberValidationService
        )
        {
            this.accountNumberParsingService = accountNumberParsingService;
            this.accountNumberValidationService = accountNumberValidationService;
        }

        public string VerifyOcrInput(string input)
        {
            var accountNumber = accountNumberParsingService.ParseOcrInput(input);
            if (accountNumber.Contains('?'))
            {
                return accountNumber + " ILL";
            }

            return accountNumberValidationService.IsChecksumValid(accountNumber) ? accountNumber : accountNumber + " ERR";
        }

        public string VerifyOcrInputAccountingForMistakes(string input)
        {
            var ocrInputAsAccountNumber = accountNumberParsingService.ParseOcrInput(input);
            var possibleAccountNumbers = accountNumberParsingService.GetPossibleAccountNumbers(input);

            var validAccountNumbers = new List<string>();
            foreach (var possibleAccountNumber in possibleAccountNumbers)
            {
                if (accountNumberValidationService.IsChecksumValid(possibleAccountNumber))
                {
                    validAccountNumbers.Add(possibleAccountNumber);
                }
            }

            if (validAccountNumbers.Count == 1) { return validAccountNumbers[0]; }
            if (validAccountNumbers.Count > 1) {
                return ocrInputAsAccountNumber + " AMB [" + string.Join(", ", validAccountNumbers.Select(x => "'" + x + "'")) + "]";
            }

            return ocrInputAsAccountNumber + " ILL";
        }
    }
}
