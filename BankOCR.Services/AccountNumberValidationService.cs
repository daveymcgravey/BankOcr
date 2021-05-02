using BankOCR.Services.Interfaces;
using System.Linq;

namespace BankOCR.Services
{
    public class AccountNumberValidationService : IAccountNumberValidationService
    {
        public bool IsChecksumValid(string accountNumber)
        {
            var accountNumberAsDigits = accountNumber.ToCharArray().Select(x => char.GetNumericValue(x));

            double checksumTotal = 0;
            double multiplier = 1;
            foreach (var number in accountNumberAsDigits.Reverse())
            {
                checksumTotal += number * multiplier;
                multiplier++;
            }

            return checksumTotal % 11 == 0;
        }
    }
}
