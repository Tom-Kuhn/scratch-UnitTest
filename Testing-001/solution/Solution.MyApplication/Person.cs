using System;
using System.Collections.Generic;
using System.Threading;

namespace Solution.MyApplication
{
    public class Person
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Person"/> class.
        /// </summary>
        /// <param name="values">The field values used for a person.</param>
        public Person()
        {
            
        }

        public string Name { get; set; }
        public string Occupation { get; set; }
        public DateTime DOB { get; set; }
    }
}