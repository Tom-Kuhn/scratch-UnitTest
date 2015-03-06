﻿using System;
using Moq;
using NUnit.Framework;
using Solution.MyApplication;
using Solution.MyApplication.Validation;

namespace Test.Solution.MyApplication
{
    [TestFixture]
    public class PersonServiceTests
    {
        [SetUp]
        public void __init()
        {
            Mocks = new MockRepository(MockBehavior.Loose);

            // Create all mocks required for the tests
            ValidationMock = Mocks.Create<IValidation>();

            // Automatically set up mocks for 80% scenarios
            ValidationMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(true);

            // Create the test target
            _target = new PersonService(ValidationMock.Object);
        }

        public MockRepository Mocks;

        public Mock<IValidation> ValidationMock;

        public PersonService _target;

        public void Solution_GetAgeGroup_TestRunner(int age, AgeGroup expectedResult)
        {
            // Arrange
            var testPerson = new Person() {DOB = DateTime.Today.AddYears(-age)};

            // Act
            AgeGroup result = _target.GetAgeGroup(testPerson);

            // Assert
            Assert.AreEqual(expectedResult, result,
                string.Format("Check that age: {0} correctly falls into the category {1}", age,
                    expectedResult.ToString()));
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(35)]
        [TestCase(99)]
        public void Solution_CalculateAge_ValidDates_AgeReturned(int age)
        {
            int result = _target.CalculateAge(DateTime.Today.AddYears(-age));

            Assert.AreEqual(age, result);
        }

        [Test]
        [ExpectedException(typeof (ApplicationException))]
        public void Solution_GetAgeGroup_InvalidPerson_ApplicationExceptionThrown()
        {
            // Create mock that states that the customer is invalid
            ValidationMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(false);

            _target.GetAgeGroup(new Person());
        }

        [Test]
        public void Solution_GetAgeGroup_PersonIsValidated_ValidateIsCalled()
        {
            _target.GetAgeGroup(new Person() {DOB = new DateTime(1980, 1, 1)});

            ValidationMock.VerifyAll();
        }

        [Test]
        public void Solution_GetAgeGroup_ValidScenarios_TestManager()
        {
            Solution_GetAgeGroup_TestRunner(0, AgeGroup.Child);
            Solution_GetAgeGroup_TestRunner(1, AgeGroup.Child);
            Solution_GetAgeGroup_TestRunner(5, AgeGroup.Child);
            Solution_GetAgeGroup_TestRunner(12, AgeGroup.Child);

            Solution_GetAgeGroup_TestRunner(13, AgeGroup.Teen);
            Solution_GetAgeGroup_TestRunner(15, AgeGroup.Teen);
            Solution_GetAgeGroup_TestRunner(17, AgeGroup.Teen);

            Solution_GetAgeGroup_TestRunner(18, AgeGroup.YoungAdult);
            Solution_GetAgeGroup_TestRunner(21, AgeGroup.YoungAdult);
            Solution_GetAgeGroup_TestRunner(25, AgeGroup.YoungAdult);

            Solution_GetAgeGroup_TestRunner(26, AgeGroup.Adult);
            Solution_GetAgeGroup_TestRunner(28, AgeGroup.Adult);
            Solution_GetAgeGroup_TestRunner(74, AgeGroup.Adult);

            Solution_GetAgeGroup_TestRunner(75, AgeGroup.Retired);
            Solution_GetAgeGroup_TestRunner(100, AgeGroup.Retired);
            Solution_GetAgeGroup_TestRunner(200, AgeGroup.Retired);
        }
    }
}