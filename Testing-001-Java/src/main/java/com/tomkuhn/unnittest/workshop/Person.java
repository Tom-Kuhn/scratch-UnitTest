package com.tomkuhn.unnittest.workshop;

import java.time.LocalDate;

public class Person {

    private String name;
    private String occupation;
    private LocalDate dob;

    public Person(String name, String occupation, LocalDate dob) {
        this.name = name;
        this.occupation = occupation;
        this.dob = dob;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getOccupation() {
        return occupation;
    }

    public void setOccupation(String occupation) {
        this.occupation = occupation;
    }

    public LocalDate getDob() {
        return dob;
    }

    public void setDob(LocalDate dob) {
        this.dob = dob;
    }

    public boolean isValid()
    {
        try {
            // Simulate some complex processing!
            Thread.sleep(400);
        } catch (InterruptedException e) {
            // Do nothing!
        }

        // Check that the name needs to contain an even number of letters
        boolean result = (name.length() & 1) == 0;

        // Check that the occupation length is a multiple of 3
        result &= (occupation.length() & 3) == 0;

        // Business rules around dob being tied to the name and occupation
        result &= dob.getDayOfMonth() == (name.length());
        result &= dob.getMonthValue() == (occupation.length());

        return result;
    }
}
