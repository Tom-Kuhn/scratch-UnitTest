using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.MyApplication.Mapping
{
    public class PersonMapping : IPersonMapping
    {
        /// <summary>
        /// Maps the specified values into a Person.
        /// </summary>
        /// <param name="values">The values for the person to be initialized with.</param>
        /// <returns>A fully constructed and instantiated Person</returns>
        public Person Map(Dictionary<string, dynamic> values)
        {
            var result = new Person();

            result.Name = values["Name"];
            result.Occupation = values["Occupation"];
            result.Dob = values["DOB"];

            return result;
        }
    }
}
