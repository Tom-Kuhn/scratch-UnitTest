package com.tomkuhn.unnittest.solution;

import com.tomkuhn.unnittest.solution.validation.Validator;

import java.time.LocalDate;
import java.time.Period;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Supplier;

public class PersonService {
    private final Validator personValidator;
    private Map<Integer, AgeGroup> ageGroupMap = new HashMap<>();

    private Supplier<LocalDate> getLocalDateNow;

    public PersonService(Validator personValidator, Supplier<LocalDate> getNow) {

        this.personValidator = personValidator;
        this.getLocalDateNow = getNow;

        ageGroupMap.put(12, AgeGroup.CHILD);
        ageGroupMap.put(17, AgeGroup.TEEN);
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
        if (!personValidator.isValid(customer)) {
            throw new IllegalArgumentException("The customer is invalid");
        }

        int age = calculateAge(customer.getDob());

        int smallestViableAgeGroup = ageGroupMap.entrySet().stream()
                                        .mapToInt(Map.Entry::getKey)
                                        .filter(x -> x >= age)
                                        .min().orElse(999);

        return ageGroupMap.get(smallestViableAgeGroup);
    }

    public int calculateAge(LocalDate dob) {
        return Period.between(dob, getLocalDateNow.get()).getYears();
    }
}
