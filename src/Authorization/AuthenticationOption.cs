namespace Sample.AspNetCoreWebApi.Authorization
{

    /// <summary>
    /// Represent secure information.
    /// </summary>
    public class AuthenticationOption
    {
        /// <summary>
        /// Represent security key.
        /// </summary>
        public string Key {get;set;}

        /// <summary>
        /// Represent subject.
        /// </summary>
        public string Subject {get;set;}

        /// <summary>
        /// Represent issuer.
        /// </summary>
        public string Issuer {get;set;}

        /// <summary>
        /// Represent autience.
        /// </summary>
        public string Audience {get;set;}

        /// <summary>
        /// Represent name of admininistrator claim.
        /// </summary>
        public string AdminClaimName {get;set;}

        /// <summary>
        /// Represent expiration time.
        /// </summary>
        public int ExpirationTime {get;set;}

        /// <summary>
        /// Represent salt in hashing proccess.
        /// </summary>
        public string Salt {get;set;}
    }
}