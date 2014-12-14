using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Domain.Models
{
    /// <summary>
    /// Application user
    /// </summary>
    [DebuggerDisplay("{Id}, {Email}")]
    public class AppUser : BaseEntity
    {
        /// <summary>
        /// First name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Full user name.
        /// </summary>
        public string FullName { get { return string.Format("{0} {1}", FirstName, LastName).Trim(); } }

        /// <summary>
        /// Has the user been activated.
        /// </summary>
        public bool Activated { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Password hash.
        /// </summary>
        [IgnoreDataMember]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Date/time of the users last visit.
        /// </summary>
        public DateTime? LastVisited { get; set; }

        /// <summary>
        /// Role associated to this user.
        /// </summary>
        public AppRole Role { get; set; }

        /// <summary>
        /// Token used for activating the user via email.
        /// </summary>
        [IgnoreDataMember]
        public string EmailActivationToken { get; private set; }

        /// <summary>
        /// Token used for authentication with the Web Api.
        /// </summary>
        [IgnoreDataMember]
        public string ApiToken { get; private set; }

        /// <exclude />
        private static string UniqueToken { get { return Guid.NewGuid().ToString(); } }

        /// <summary>
        /// Construction.
        /// </summary>
        public AppUser()
        {
            Role = AppRole.User;
            EmailActivationToken = UniqueToken;
            ApiToken = UniqueToken;
        }

        /// <exclude />
        internal AppUser(string fistname, string lastname, string email, string password, AppRole role, bool activated)
            : this()
        {
            FirstName = fistname;
            LastName = lastname;
            Email = email;
            SetPassword(password);
            Activated = activated;
            Role = role;
        }

        /// <summary>
        /// Check an entered password against the user password.
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool CheckPassword(string password)
        {
            return (PasswordHash == GetPasswordHash(password));
        }

        /// <summary>
        /// Set the user password.
        /// </summary>
        /// <param name="password"></param>
        public void SetPassword(string password)
        {
            PasswordHash = GetPasswordHash(password);
        }

        /// <exclude />
        private static string GetPasswordHash(string password)
        {
            using (var sha1 = new System.Security.Cryptography.SHA1Managed())
            {
                return Convert.ToBase64String(sha1.ComputeHash(Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}
