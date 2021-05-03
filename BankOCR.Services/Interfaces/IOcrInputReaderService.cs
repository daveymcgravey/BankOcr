namespace BankOCR.Services.Interfaces
{
    public interface IOcrInputReaderService
    {
        public string VerifyOcrInput(string input);
        public string VerifyOcrInputAccountingForMistakes(string input);
    }
}
