using System;
using System.Linq;
using MyApplication.dependencies;
using System.Collections.Generic;

namespace MyApplication
{
    public class PersonService
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        public PersonService()
        {
            AgeGroupMap = new Dictionary<int,AgeGroup>();

            AgeGroupMap[12] = AgeGroup.Child;
            AgeGroupMap[18] = AgeGroup.Teen;
            AgeGroupMap[25] = AgeGroup.YoungAdult;
            AgeGroupMap[74] = AgeGroup.Adult;
            AgeGroupMap[999] = AgeGroup.Retired;
        }

        public Dictionary<int, AgeGroup> AgeGroupMap { get; set; }

        /// <summary>
        /// Gets the age group for a particular customer.
        /// </summary>
        /// <param name="customer">The customer to calculate the AgeGroup of.</param>
        /// <returns>The correct age group for the customer</returns>
        /// <exception cref="System.ApplicationException">If the customer is invalid</exception>
        public AgeGroup GetAgeGroup(Person customer)
        {
            if (!customer.IsValid())
            {
                throw new ApplicationException("customer is invalid");
            }

            // Calculate age
            DateTime zeroTime = new DateTime(1, 1, 1);
            int age = (zeroTime + (DateTime.Today - customer.DOB)).Year;
            
            // Return the correct age group
            var viableBuckets = AgeGroupMap.Where(x => x.Key >= age);
            return AgeGroupMap[viableBuckets.Min(x => x.Key)];
        }
    }
}