package com.tomkuhn.unnittest;

import java.time.LocalDate;
import java.time.Period;
import java.util.HashMap;
import java.util.Map;
import java.util.OptionalInt;

public class PersonService {
    private Map<Integer, AgeGroup> ageGroupMap = new HashMap<>();

    public PersonService() {

        ageGroupMap.put(12, AgeGroup.CHILD);
        ageGroupMap.put(18, AgeGroup.TEEN);
        ageGroupMap.put(25, AgeGroup.YOUNG_ADULT);
        ageGroupMap.put(74, AgeGroup.ADULT);
        ageGroupMap.put(999, AgeGroup.RETIRED);
    }

    public Map<Integer, AgeGroup> getAgeGroupMap() {
        return ageGroupMap;
    }

    public void setAgeGroupMap(Map<Integer, AgeGroup> ageGroupMap) {
        this.ageGroupMap = ageGroupMap;
    }

    public AgeGroup getAgeGroup(Person customer) {
        if (!customer.isValid()) {
            throw new IllegalArgumentException("The customer is invalid");
        }

        // Calculate the customer's age
        LocalDate zeroDate = LocalDate.of(1, 1, 1);
        int age = zeroDate.plus(Period.between(LocalDate.now(), customer.getDob())).getYear();

        OptionalInt targetAgeGroup = ageGroupMap.keySet().stream().filter(x -> x >= age).mapToInt(x -> x).min();

        return ageGroupMap.get(targetAgeGroup.getAsInt());
    }
}
