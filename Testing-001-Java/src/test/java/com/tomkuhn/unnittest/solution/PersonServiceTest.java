package com.tomkuhn.unnittest.solution;

import com.tomkuhn.unnittest.solution.validation.Validator;
import org.junit.Before;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.time.LocalDate;
import java.util.function.Supplier;

import static junit.framework.Assert.assertEquals;
import static org.mockito.Mockito.when;

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

    Person testCustomer = new Person("Bob", "Software Engineer", LocalDate.now().minusYears(30));
    private PersonService target;
    @Mock
    private Validator personValidator;
    private Supplier<LocalDate> testGetNow;

    @Before
    public void setup() {
        MockitoAnnotations.initMocks(this);
        testGetNow = LocalDate::now;

        target = new PersonService(personValidator, testGetNow);

        // Setup mock conditions for the majority of tests
        when(personValidator.isValid(testCustomer)).thenReturn(true);
    }

    @Test(expected = IllegalArgumentException.class)
    public void getAgeGroup_InvalidCustomer_ApplicationExceptionThrown() throws IllegalArgumentException {
        Person testCustomer = new Person("Bob", "Software Engineer", LocalDate.now().minusYears(30));

        target.getAgeGroup(testCustomer);
    }

    @Test
    public void getAgeGroup_TestManager() throws Exception {

        getAgeGroup_TestRunner(0, AgeGroup.CHILD);
        getAgeGroup_TestRunner(1, AgeGroup.CHILD);
        getAgeGroup_TestRunner(5, AgeGroup.CHILD);
        getAgeGroup_TestRunner(12, AgeGroup.CHILD);

        getAgeGroup_TestRunner(13, AgeGroup.TEEN);
        getAgeGroup_TestRunner(15, AgeGroup.TEEN);
        getAgeGroup_TestRunner(17, AgeGroup.TEEN);

        getAgeGroup_TestRunner(18, AgeGroup.YOUNG_ADULT);
        getAgeGroup_TestRunner(21, AgeGroup.YOUNG_ADULT);
        getAgeGroup_TestRunner(25, AgeGroup.YOUNG_ADULT);

        getAgeGroup_TestRunner(26, AgeGroup.ADULT);
        getAgeGroup_TestRunner(28, AgeGroup.ADULT);
        getAgeGroup_TestRunner(74, AgeGroup.ADULT);

        getAgeGroup_TestRunner(75, AgeGroup.RETIRED);
        getAgeGroup_TestRunner(100, AgeGroup.RETIRED);
        getAgeGroup_TestRunner(1000, AgeGroup.RETIRED);
    }

    private void getAgeGroup_TestRunner(int customerAgeInYears, AgeGroup expectedResult) throws Exception {
        testCustomer.setDob(LocalDate.now().minusYears(customerAgeInYears));

        AgeGroup result = target.getAgeGroup(testCustomer);

        assertEquals(String.format("Expected a customer of age: %d to return %s", customerAgeInYears, expectedResult.name()), expectedResult, result);
    }

    @Test
    public void calculateAge_TestManager() {
        calculateAge_TestRunner(LocalDate.now().minusYears(0), 0);
        calculateAge_TestRunner(LocalDate.now().minusYears(1), 1);
        calculateAge_TestRunner(LocalDate.now().minusYears(35), 35);
        calculateAge_TestRunner(LocalDate.now().minusYears(99), 99);

        // Deal with the day before a birthday
        calculateAgeWithTimeTravel_TestRunner(LocalDate.of(2000, 5, 20), LocalDate.of(2010, 5, 19), 9);

        // Deal with the day of a birthday
        calculateAgeWithTimeTravel_TestRunner(LocalDate.of(2000, 5, 20), LocalDate.of(2010, 5, 20), 10);

        // Deal with the day before a birthday which is on Feb 29
        calculateAgeWithTimeTravel_TestRunner(LocalDate.of(2000, 2, 29), LocalDate.of(2012, 2, 28), 11);

        // Deal with the day of a birthday which is on Feb 29
        calculateAgeWithTimeTravel_TestRunner(LocalDate.of(2000, 2, 29), LocalDate.of(2012, 2, 29), 12);
    }

    private void calculateAge_TestRunner(LocalDate dob, int expectedAge) {
        int result = target.calculateAge(dob);

        assertEquals(expectedAge, result);
    }

    private void calculateAgeWithTimeTravel_TestRunner(LocalDate dob, LocalDate timeTravelDate, int expectedAge) {
        target = new PersonService(personValidator, () -> timeTravelDate);
        calculateAge_TestRunner(dob, expectedAge);
    }
}