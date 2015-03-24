using System;
using Moq;
using MyApplication;
using NUnit.Framework;

namespace Test.MyApplication
{
    [TestFixture]
    public class PersonServiceTests
    {
        /*******************************************************************************************************
         * Your mission (should you choose to accept it):
         *      > Unit test the PersonService.GetAgeGroup function.
         *      > Fix the PersonService.GetAgeGroup function so that it returns the expected results (see below)
         *  
         * Expected behavior of the GetAgeGroup function:
         * 
         * 1) If the customer is invalid, throw an ApplicationException
         * 
         * 2) Use the customer DOB to calculate the age and then determine the AgeGroup using the following:
         *      Child: < 13
         *      Teen:  13 - 18
         *      YoungAdult: 18 - 25
         *      Adult: 26 - 74
         *      Retired: > 75
         *          
         * NOTE: you are free to refactor anything and everything to make your life easier
         *          
         ******************************************************************************************************/

        private MockRepository _mocks;

        private PersonService _target; // Service under test

        [SetUp]
        public void __init()
        {
            _mocks = new MockRepository(MockBehavior.Strict);

            _target = new PersonService();
        }

        [Test]
        public void GetAgeGroup_InvalidPerson_ApplicationExceptionThrown()
        {
            // TODO: implement test
        }

        // TODO: Test that the GetAgeGroup returns the correct result
    }
}
