using FluentAssertions;
using NUnit.Framework;

namespace BankOcrKata
{
    [TestFixture]
    public class UserStory2
    {
        [TestCase("711111111", true)]
        [TestCase("123456789", true)]
        [TestCase("490867715", true)]
        [TestCase("888888888", false)]
        [TestCase("490067715", false)]
        [TestCase("012345678", false)]
        public void Tests(string accountNumber, bool isValid)
        {
            //Arrange
            IAccountNumberValidationService accountNumberValidationService = new AccountNumberValidationService();

            //Act
            bool actualResult = accountNumberValidationService.IsChecksumValid(accountNumber);

            //Assert
            actualResult.Should().Be(isValid, because: "The validation service should return if the account number is valid or not");
        }
    }
}