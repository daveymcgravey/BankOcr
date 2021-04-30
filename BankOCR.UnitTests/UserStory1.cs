﻿using BankOCR;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Linq;

namespace BankOcrKata
{
    [TestFixture]
    public class UserStory1
    {
        [TestCase(@"
 _  _  _  _  _  _  _  _  _
| || || || || || || || || |
|_||_||_||_||_||_||_||_||_|", "000000000")]
        [TestCase(@"
                           
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "111111111")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
 _| _| _| _| _| _| _| _| _|
|_ |_ |_ |_ |_ |_ |_ |_ |_ ", "222222222")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _
 _| _| _| _| _| _| _| _| _|
 _| _| _| _| _| _| _| _| _|", "333333333")]
        [TestCase(@"
                           
|_||_||_||_||_||_||_||_||_|
  |  |  |  |  |  |  |  |  |", "444444444")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
 _| _| _| _| _| _| _| _| _|", "555555555")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_ |_ |_ |_ |_ |_ |_ |_ |_ 
|_||_||_||_||_||_||_||_||_|", "666666666")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
  |  |  |  |  |  |  |  |  |
  |  |  |  |  |  |  |  |  |", "777777777")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
|_||_||_||_||_||_||_||_||_|", "888888888")]
        [TestCase(@"
 _  _  _  _  _  _  _  _  _ 
|_||_||_||_||_||_||_||_||_|
 _| _| _| _| _| _| _| _| _|", "999999999")]
        [TestCase(@"
    _  _     _  _  _  _  _ 
  | _| _||_||_ |_   ||_||_|
  ||_  _|  | _||_|  ||_| _|", "123456789")]
        public void Tests(string input, string expectedResult)
        {
            //Arrange
            IAccountNumberParsingService accountNumberParsingService = new AccountNumberParsingService();

            //Act
            string actualResult = accountNumberParsingService.ParseOcrInput(input);

            //Assert
            actualResult.Should().BeEquivalentTo(expectedResult, because: "The parsing service should return the correctly parsed OCR input");
        }
    }
}