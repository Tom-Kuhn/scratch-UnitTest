using System.Collections.Generic;

namespace Solution.MyApplication.Mapping
{
    public interface IPersonMapping
    {
        /// <summary>
        /// Maps the specified values into a Person.
        /// </summary>
        /// <param name="values">The values for the person to be initialized with.</param>
        /// <returns>A fully constructed and instantiated Person</returns>
        Person Map(Dictionary<string, object> values);
    }
}