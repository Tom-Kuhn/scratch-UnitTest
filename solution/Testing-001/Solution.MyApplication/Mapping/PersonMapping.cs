using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.MyApplication.Mapping
{
    public interface IPersonMapping
    {
        Person Map(Dictionary<string, object> values);
    }

    public class PersonMapping : IPersonMapping
    {
        public Person Map(Dictionary<string, dynamic> values)
        {
            var result = new Person();

            result.Name = values["Name"];
            result.Occupation = values["Occupation"];
            result.DOB = values["DOB"];

            return result;
        }
    }
}
