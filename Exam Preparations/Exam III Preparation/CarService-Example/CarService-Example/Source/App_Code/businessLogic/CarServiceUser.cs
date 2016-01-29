using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace businesslogic
{
    /// <summary>
    /// Represents information from membership user and its profile.
    /// </summary>
    public class CarServiceUser
    {
        private MembershipUser user;
        ProfileCommon profileCommon;

        public CarServiceUser(MembershipUser user, ProfileCommon profileCommon)
        {
            this.user = user;
            this.profileCommon = profileCommon;
        }

        public string UserName
        {
            get { return this.user.UserName; }
        }

        public string Email
        {
            get { return this.user.Email; }
        }

        public bool IsActive
        {
            get { return this.user.IsApproved; }
        }

        public string FirstName
        {
            get
            {
                return this.profileCommon.FirstName;
            }
        }

        public string LastName
        {
            get
            {
                return this.profileCommon.LastName;
            }
        }
    }
}