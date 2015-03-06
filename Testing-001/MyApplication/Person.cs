using System;
using System.Collections.Generic;
using System.Threading;

namespace MyApplication.dependencies
{
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="values">The field values used for a person.</param>
        public Person(Dictionary<string, dynamic> values)
        {
            Name = values["Name"];
            Occupation = values["Occupation"];
            DOB = values["DOB"];
        }

        public string Name { get; set; }
        public string Occupation { get; set; }
        public DateTime DOB { get; set; }

        /// <summary>
        /// Determines whether the p is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            // Simulate some complex processing!
            Thread.Sleep(400);

            // Check that the name needs to contain an even number of letters
            bool result = (Name.Length & 0x1) == 0;
            result &= (Occupation.Length & 0x3) == 0;
            result &= DOB.Day == (Name.Length);
            result &= DOB.Month == (Occupation.Length);

            return result;
        }
    }
}