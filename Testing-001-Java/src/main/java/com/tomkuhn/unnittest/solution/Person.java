package com.tomkuhn.unnittest.solution;

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
}
