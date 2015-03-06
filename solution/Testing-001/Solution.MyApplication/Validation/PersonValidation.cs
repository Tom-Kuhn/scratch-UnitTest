using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.MyApplication.Validation
{
    public interface IValidation
    {
        bool IsValid(dynamic target);
    }

    public class PersonValidation : IValidation
    {
        public bool IsValid(dynamic target)
        {
            // Simulate some complex processing!
            Thread.Sleep(400);

            // Check that the name needs to contain an even number of letters
            bool result = (target.Name.Length & 0x1) == 0;
            result &= (target.Occupation.Length & 0x3) == 0;
            result &= target.DOB.Day == (target.Name.Length);
            result &= target.DOB.Month == (target.Occupation.Length);

            return result;
        }
    }
}
