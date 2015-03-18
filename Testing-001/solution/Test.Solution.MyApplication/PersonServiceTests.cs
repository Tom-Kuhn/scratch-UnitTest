using System;
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
            _target = new PersonService(ValidationMock.Object, () => DateTime.Today);
        }

        public MockRepository Mocks;

        public Mock<IValidation> ValidationMock;

        public PersonService _target;

        [Test]
        [TestCase(0, AgeGroup.Child)]
        [TestCase(1, AgeGroup.Child)]
        [TestCase(5, AgeGroup.Child)]
        [TestCase(12, AgeGroup.Child)]

        [TestCase(13, AgeGroup.Teen)]
        [TestCase(15, AgeGroup.Teen)]
        [TestCase(17, AgeGroup.Teen)]

        [TestCase(18, AgeGroup.YoungAdult)]
        [TestCase(21, AgeGroup.YoungAdult)]
        [TestCase(25, AgeGroup.YoungAdult)]

        [TestCase(26, AgeGroup.Adult)]
        [TestCase(28, AgeGroup.Adult)]
        [TestCase(74, AgeGroup.Adult)]

        [TestCase(75, AgeGroup.Retired)]
        [TestCase(100, AgeGroup.Retired)]
        [TestCase(200, AgeGroup.Retired)]
        public void Solution_GetAgeGroup_ValidScenarios_CorrectResultReturned(int age, AgeGroup expectedResult)
        {
            // Arrange
            var testPerson = new Person() {Dob = DateTime.Today.AddYears(-age)};

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
        public void Solution_CalculateAge_DayBeforeBirthday_CorrectAgeReturned()
        {
            int expectedAge = 15;
            DateTime currentTestSystemDate = new DateTime(2015, 10, 3);

            // Calculate the day before the 16th birthday (i.e. the birthday is one day after the sys date)
            DateTime testDateOfBirth = currentTestSystemDate.AddYears(-(expectedAge) - 1).AddDays(1);

            // Use a static date
            _target.GetTodaysDate = () => currentTestSystemDate;

            int result = _target.CalculateAge(testDateOfBirth);

            Assert.AreEqual(expectedAge, result);
        }

        [Test]
        public void Solution_CalculateAge_DayOfBirthday_CorrectAgeReturned()
        {
            int expectedAge = 15;
            DateTime currentTestSystemDate = new DateTime(2015, 10, 3);

            // Calculate the day of the 15th birthday
            DateTime testDateOfBirth = currentTestSystemDate.AddYears(-(expectedAge));

            // Use a static date
            _target.GetTodaysDate = () => currentTestSystemDate;

            int result = _target.CalculateAge(testDateOfBirth);

            Assert.AreEqual(expectedAge, result);
        }

        /*
         * Test doesn't work due to leap year considerations
         * TODO: Fix the test + code in the Workshop as a live exercise
         */
        ////[Test]
        ////public void Solution_CalculateAge_BirthdayOnLeapYearFeb29DateFeb28_CorrectAgeReturned()
        ////{
        ////    DateTime currentTestSystemDate = new DateTime(2015, 2, 28);

        ////    // Birthday on Feb 29th of leap year
        ////    DateTime testDateOfBirth = new DateTime(2000, 2, 29);

        ////    // Use a static date
        ////    _target.GetTodaysDate = () => currentTestSystemDate;

        ////    int result = _target.CalculateAge(testDateOfBirth);

        ////    Assert.AreEqual(14, result);
        ////}

        [Test]
        public void Solution_CalculateAge_BirthdayOnLeapYearAndDateFeb29_CorrectAgeReturned()
        {
            DateTime currentTestSystemDate = new DateTime(2020, 2, 29);

            // Birthday on Feb 29th of leap year
            DateTime testDateOfBirth = new DateTime(2000, 2, 29);

            // Use a static date
            _target.GetTodaysDate = () => currentTestSystemDate;

            int result = _target.CalculateAge(testDateOfBirth);

            Assert.AreEqual(20, result);
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
            _target.GetAgeGroup(new Person() {Dob = new DateTime(1980, 1, 1)});

            ValidationMock.VerifyAll();
        }
    }
}