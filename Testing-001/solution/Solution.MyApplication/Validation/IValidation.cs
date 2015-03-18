namespace Solution.MyApplication.Validation
{
    /// <summary>
    /// The object Validation interface
    /// </summary>
    /// <remarks>
    /// NB: Should be global at a solution level... Advocate using a convention based approach of registering it with your IOC container
    /// </remarks>
    public interface IValidation
    {
        /// <summary>
        /// Determines whether the specified target is valid.
        /// </summary>
        /// <param name="target">The target to Validate.</param>
        /// <returns><c>true</c> if the target is valid; otherwise <c>false</c></returns>
        bool IsValid(dynamic target);
    }
}