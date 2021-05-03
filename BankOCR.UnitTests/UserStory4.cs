using BankOCR.Services;
using BankOCR.Services.Interfaces;
using FluentAssertions;
using NUnit.Framework;

namespace BankOcrKata
{
    public class UserStory4
    {
        [TestCase(@"
                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "711111111")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "777777177")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
 _|| || || || || || || || |
|_ |_||_||_||_||_||_||_||_|", "200800000")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|", "333393333")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|", "888888888 AMB ['888886888', '888888988', '888888880']")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|", "555555555 AMB ['559555555', '555655555']")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|", "666666666 AMB ['686666666', '666566666']")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|", "999999999 AMB ['899999999', '993999999', '999959999']")]
        [TestCase(@"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  ||_ 
  | _||_||_||_|  |  |  | _|", "490067715 AMB ['490867715', '490067115', '490067719']")]
        [TestCase(@"
    _  _     _  _  _  _  _ 
 _| _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|", "123456789")]
        [TestCase(@"
 _     _  _  _  _  _  _    
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |", "000000051")]
        [TestCase(@"
    _  _  _  _  _  _     _ 
|_||_|| ||_||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|", "490867715")]
        public void Tests(string input, string expectedResult)
        {
            //Arrange
            IAccountNumberParsingService accountNumberParsingService = new AccountNumberParsingService();
            IAccountNumberValidationService accountNumberValidationService = new AccountNumberValidationService();

            IOcrInputReaderService ocrInputReaderService = new OcrInputReaderService(accountNumberParsingService, accountNumberValidationService);

            //Act
            string actualResult = ocrInputReaderService.VerifyOcrInputAccountingForMistakes(input);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "The file reader service should return the account number along with information on validity of legibility and checksum");
        }
    }
}