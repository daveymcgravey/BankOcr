using BankOCR.Services.Interfaces;
using System.Linq;

namespace BankOCR.Services
{
    public class AccountNumberValidationService : IAccountNumberValidationService
    {
        public bool IsChecksumValid(string accountNumber)
        {
            //Parse account number to digits
            var accountNumberAsDigits = accountNumber.ToCharArray().Select(x => char.GetNumericValue(x));

            //Loop through the account numbers (in reverse to make the maths easier) as per the following:

            /*
                account number:  3  4  5  8  8  2  8  6  5
                position names:  d9 d8 d7 d6 d5 d4 d3 d2 d1

                checksum calculation:
                (d1+2*d2+3*d3+...+9*d9) mod 11 = 0
             */

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
