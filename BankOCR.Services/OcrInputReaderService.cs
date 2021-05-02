using BankOCR.Services.Interfaces;
using System;

namespace BankOCR.Services
{
    public class OcrInputReaderService : IOcrInputReaderService
    {
        private readonly IAccountNumberParsingService accountNumberParsingService;
        private readonly IAccountNumberValidationService accountNumberValidationService;
        public OcrInputReaderService()
        {
            this.accountNumberParsingService = new AccountNumberParsingService();
            this.accountNumberValidationService = new AccountNumberValidationService();
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
    }
}
