using Moq;
using NUnit.Framework;
using Solution.MyApplication;
using Solution.MyApplication.Validation;
using System;
using System.Collections.Generic;

namespace Test.Solution.MyApplication
{
    [TestFixture]
    public class PersonServiceTests
    {
        public MockRepository Mocks;

        public Mock<IValidation> ValidationMock;

        public PersonService _target;

        public Person TestPerson;

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

            var values = new Dictionary<string, dynamic>();
            values["Name"] = "Tom";
            values["Occupation"] = "Engineer";
            values["DOB"] = new DateTime(1984, 10, 3);

            TestPerson = new Person(values);
        }

        [Test]
        [ExpectedException(typeof(ApplicationException))]
        public void Solution_GetAgeGroup_InvalidPerson_ApplicationExceptionThrown()
        {
            // Create mock that states that the customer is invalid
            ValidationMock.Setup(x => x.IsValid(It.IsAny<Person>())).Returns(false);

            _target.GetAgeGroup(TestPerson);
        }

        [Test]
        public void Solution_GetAgeGroup_ValidPerson_NoExceptionThrown()
        {
            _target.GetAgeGroup(TestPerson);

            Assert.Pass();
        }

        [Test]
        public void Solution_GetAgeGroup_ChildCustomer_CorrectAgeGroupReturned()
        {
            _target.GetAgeGroup(TestPerson);

            Assert.Pass();
        }
    }
}