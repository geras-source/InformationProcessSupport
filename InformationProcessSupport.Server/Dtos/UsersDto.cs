namespace InformationProcessSupport.Server.Dtos
{
    public class UsersDto
    {
        /// <summary>
        /// Primary key in a database (don't used)
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// The real id that user
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// The nickname that user
        /// </summary>
        public string? Nickname { get; set; }
        /// <summary>
        /// His affiliation by roles
        /// </summary>
        public string? Roles { get; set; }

        public string? GroupName { get; set; }
    }
}
