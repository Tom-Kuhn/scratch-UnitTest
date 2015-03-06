using Solution.MyApplication.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Solution.MyApplication
{
    public class PersonService
    {
        public IValidation Validator { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        public PersonService(IValidation validator)
        {
            Validator = validator;

            // Ideally this would be a DB call
            AgeGroupMap = new Dictionary<int,AgeGroup>();

            AgeGroupMap[12] = AgeGroup.Child;
            AgeGroupMap[17] = AgeGroup.Teen;
            AgeGroupMap[25] = AgeGroup.YoungAdult;
            AgeGroupMap[74] = AgeGroup.Adult;
            AgeGroupMap[999] = AgeGroup.Retired;
        }

        /// <summary>
        /// Gets or sets the age group map.
        /// </summary>
        /// <remarks>
        /// The map = 
        ///     key: max age in bracket
        ///     value: corresponding Age Group for bucket
        /// </remarks>
        /// <value>
        /// The age group map.
        /// </value>
        public Dictionary<int, AgeGroup> AgeGroupMap { get; set; }

        /// <summary>
        /// Gets the age group for a particular customer.
        /// </summary>
        /// <param name="customer">The customer to calculate the AgeGroup of.</param>
        /// <returns>The correct age group for the customer</returns>
        /// <exception cref="System.ApplicationException">If the customer is invalid</exception>
        public AgeGroup GetAgeGroup(Person customer)
        {
            if (!Validator.IsValid(customer))
            {
                throw new ApplicationException("customer is invalid");
            }

            // Calculate age
            int age = CalculateAge(customer.DOB);

            // Return the correct age group
            var viableBuckets = AgeGroupMap.Where(x => x.Key >= age).ToList();
            return AgeGroupMap[viableBuckets.Min(x => x.Key)];
        }

        /// <summary>
        /// Calculates age.
        /// </summary>
        /// <param name="dateOfBirth">The date of birth.</param>
        /// <returns>The age (in whole years)</returns>
        public int CalculateAge(DateTime dateOfBirth)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            int age = (zeroTime + (DateTime.Today - dateOfBirth)).Year - 1;

            return age;
        }
    }
}