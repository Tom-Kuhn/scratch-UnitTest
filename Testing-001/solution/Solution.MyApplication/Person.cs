using System;
using System.Collections.Generic;
using System.Threading;

namespace Solution.MyApplication
{
    /// <summary>
    /// Domain model describing a Person
    /// </summary>
    public class Person
    {
        public string Name { get; set; }
        public string Occupation { get; set; }
        public DateTime Dob { get; set; }
    }
}