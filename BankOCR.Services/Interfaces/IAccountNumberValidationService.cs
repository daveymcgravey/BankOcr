namespace BankOCR.Services.Interfaces
{
    public interface IAccountNumberValidationService
    {
        public bool IsChecksumValid(string accountNumber);
    }
}
