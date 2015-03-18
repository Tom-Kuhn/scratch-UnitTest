using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Solution.MyApplication.Validation
{
    /// <summary>
    /// This class only validates People
    /// </summary>
    /// <remarks>
    /// If there were multiple ways of validating the Person, you could overload the IsValid 
    /// function with functions that take in a validation context. Normally you would look to 
    /// use a different model for each layer of the application, therefore allowing you to 
    /// specify one isValid method per object type that needs validating.
    /// </remarks>
    public class PersonValidation : IValidation
    {
        /// <summary>
        /// Determines whether the specified target is valid.
        /// </summary>
        /// <param name="target">The target to Validate.</param>
        /// <returns><c>true</c> if the target is valid; otherwise <c>false</c></returns>
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
