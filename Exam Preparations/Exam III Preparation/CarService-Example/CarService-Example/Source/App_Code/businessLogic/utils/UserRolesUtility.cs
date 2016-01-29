using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using constants;

namespace businesslogic.utils
{
    /// <summary>
    /// Summary description for UserRolesUtility
    /// </summary>
    public class UserRolesUtility
    {
        public UserRolesUtility()
        {

        }

        public static bool IsOperatorRoleExists()
        {
            string[] existingRoles = Roles.GetAllRoles();
            if (existingRoles != null && existingRoles.Length > 0)
            {
                foreach (string currentRole in existingRoles)
                {
                    if (currentRole.Equals(CarServiceConstants.OPERATOR_ROLE_NAME))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}