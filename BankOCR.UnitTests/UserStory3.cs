using FluentAssertions;
using NUnit.Framework;

namespace BankOcrKata
{
    public class UserStory3
    {
        [TestCase(@"
 _  _  _  _  _  _  _  _     
| || || || || || || ||_   |
|_||_||_||_||_||_||_| _|  |", "000000051")]
        [TestCase(@"
    _  _  _  _  _  _     _ 
|_||_|| || ||_   |  |  | _ 
  | _||_||_||_|  |  |  | _|", "49006771? ILL")]
        [TestCase(@"
    _  _     _  _  _  _  _ 
  | _| _||_| _ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _ ", "1234?678? ILL")]
        public void Tests(string input, string expectedResult)
        {
            //Arrange
            IOcrInputReaderService ocrInputReaderService = new OcrInputReaderService();

            //Act
            string actualResult = ocrInputReaderService.VerifyOcrInput(input);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "The file reader service should return the account number along with information on validity of legibility and checksum");
        }
    }
}