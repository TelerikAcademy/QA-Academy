using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using constants;
using System.Globalization;
using System.Web.SessionState;
using persistence;
using System.Web.UI.WebControls;
using System.Data.Objects;
using System.Text.RegularExpressions;

namespace businesslogic.utils
{
    /// <summary>
    /// Summary description for CarServiceUtility
    /// </summary>
    public class CarServiceUtility
    {
        private static readonly Regex GUID_REGEX = new Regex("^[A-Fa-f0-9]{32}$|" +
                              "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                              "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$", RegexOptions.Compiled);


        public CarServiceUtility()
        {

        }

        public static bool IsValidDate(string dateValueTxt, out DateTime dateValue)
        {
            dateValue = DateTime.Now;
            bool validDate = DateTime.TryParseExact(dateValueTxt, CarServiceConstants.DATE_FORMAT,
                                new CultureInfo(CarServiceConstants.ENGLISH_CULTURE_INFO),
                                DateTimeStyles.None, out dateValue);           
            return validDate;
        }


        public static void AddEntityId(List<int> enityIds, string entityIdTxt)
        {
            int partId;
            if (Int32.TryParse(entityIdTxt, out partId))
            {
                enityIds.Add(partId);
            }
        }

        public static bool GuidTryParse(string guidTxt, out Guid result)
        {
            if (String.IsNullOrEmpty(guidTxt) == false && GUID_REGEX.IsMatch(guidTxt))
            {
                result = new Guid(guidTxt);
                return true;
            }
            result = default(Guid);
            return false;
        }

        public static void ClearSessionAttributes(HttpSessionState session)
        {
            session[CarServiceConstants.AUTOMOBILE_ID_REQUEST_PARAM_NAME] = null;
            session[CarServiceConstants.REPAIR_CARD_ID_PARAM_NAME] = null;
            session[CarServiceConstants.REPAIR_CARDS_FILTER_SESSION_ATTR_NAME] = null;
        }

        #region Automobile specific

        public static Automobile GetAutomobile(string automobileIdTxt, ICarServicePersister persister)
        {
            Automobile automobile = null;
            int automobileId;
            if (Int32.TryParse(automobileIdTxt, out automobileId))
            {
                automobile = persister.GetAutomobilById(automobileId);
            }
            return automobile;
        }

        #endregion
    }
}