package com.tomkuhn.unnittest;

import org.junit.Assert;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

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
public class PersonServiceTest {

    PersonService target;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        target = new PersonService();
    }

    @Test(expected = IllegalArgumentException.class)
    public void getAgeGroup_InvalidCustomer_ApplicationExceptionThrown() throws IllegalArgumentException {
        // add unit test code
    }

    @Test
    public void getAgeGroup_ValidCustomer_CorrectAgeGroupReturned() throws Exception {
        Assert.assertTrue(false);
    }

}