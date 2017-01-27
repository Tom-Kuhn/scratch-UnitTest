package com.tomkuhn.unnittest.solution.validation;

import com.tomkuhn.unnittest.solution.Person;

public class PersonValidator implements Validator {
    @Override
    public boolean isValid(Object target)
    {
        Person person = (Person)target;
        try {
            // Simulate some complex processing!
            Thread.sleep(400);
        } catch (InterruptedException e) {
            // Do nothing!
        }

        // Check that the name needs to contain an even number of letters
        boolean result = (person.getName().length() & 1) == 0;

        // Check that the occupation length is a multiple of 3
        result &= (person.getOccupation().length() & 3) == 0;

        // Business rules around dob being tied to the name and occupation
        result &= person.getDob().getDayOfMonth() == (person.getName().length());
        result &= person.getDob().getMonthValue() == (person.getOccupation().length());

        return result;
    }
}
