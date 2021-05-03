using BankOCR.Services.Interfaces;

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
            throw new System.NotImplementedException();
        }
    }
}
