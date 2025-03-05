using System.Runtime.Serialization;

namespace SportsTrackerApp.Dtos
{
    [DataContract]
    public class UserLoginDto
    {
        /// <summary>
        /// Defines the user's name
        /// </summary>
        [DataMember(IsRequired = true)]
        public required string UserName { get; set; }

        /// <summary>
        /// Defines the user's password
        /// </summary>
        [DataMember(IsRequired = true)]
        public required string Password { get; set; }
    }
}
