using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace constants
{
    /// <summary>
    /// Summary description for CarServiceConstants
    /// </summary>
    public class CarServiceConstants
    {
        public const string OPERATOR_ROLE_NAME = "operator";
        public const string USER_ID_REQUEST_PARAM_NAME = "user";
        public const string EMAIL_REQUEST_PARAM_NAME = "email";
        public const string SPARE_PART_ID_REQUEST_PARAM_NAME = "sparePartId";
        public const string AUTOMOBILE_ID_REQUEST_PARAM_NAME = "automobileId";
        public const string REPAIR_CARD_ID_PARAM_NAME = "repairCardId";
        public const string REPAIR_CARDS_FILTER_SESSION_ATTR_NAME = "repairCardsFilter";

        public const string ACTIVE_CSS_CLASS_NAME = "active";
        public const string INACTIVE_CSS_CLASS_NAME = "inactive";
        public const string POSITIVE_CSS_CLASS_NAME = "positiveMsg";
        public const string NEGATIVE_CSS_CLASS_NAME = "negativeMsg";

        public const string DATE_FORMAT = "yyyy-MM-dd";

        public const string ENGLISH_CULTURE_INFO = "en-US";

        public const int ALL_REPAIR_CARDS_FILTER_TYPE = 0;
        public const int UNFINISHED_REPAIR_CARDS_FILTER_TYPE = 1;
        public const int FINISHED_REPAIR_CARDS_FILTER_TYPE = 2;

        public const int INACTIVE_STATUS = 0;
        public const int ACTIVE_STATUS = 1;

        public const string SORT_DIRECTION_VIEW_STATE_ATTR = "SortDirection";
        public const string SORT_EXPRESSION_VIEW_STATE_ATTR = "SortExpression";

        public const string USER_ID_SORT_EXPRESSION = "UserId";       

        public const string REPAIR_CARD_ID_SORT_EXPRESSION = "CardId";
        public const string REPAIR_CARD_CHASSIS_SORT_EXPRESSION = "ChassisNumber";
        public const string REPAIR_CARD_Vin_SORT_EXPRESSION = "Vin";
        public const string REPAIR_CARD_START_DATE_SORT_EXPRESSION = "StartRepair";
        public const string REPAIR_CARD_FINISH_DATE_SORT_EXPRESSION = "FinishRepair";
        public const string REPAIR_CARD_PRICE_SORT_EXPRESSION = "CardPrice";
        public const string USER_NAME_SORT_EXPRESSION = "UserName";
        public const string USER_EMAIL_SORT_EXPRESSION = "Email";
        public const string USER_FIRST_NAME_SORT_EXPRESSION = "FirstName";
        public const string USER_LAST_NAME_SORT_EXPRESSION = "LastName";
        public const string USER_ACTIVE_SORT_EXPRESSION = "IsActive";
        public const string SPARE_PART_ID_SORT_EXPRESSION = "PartId";
        public const string SPARE_PART_NAME_SORT_EXPRESSION = "Name";
        public const string SPARE_PART_PRICE_SORT_EXPRESSION = "Price";
        public const string SPARE_PART_ACTIVE_SORT_EXPRESSION = "IsActive";
        public const string AUTOMOBILE_ID_SORT_EXPRESSION = "AutomobileId";
        public const string AUTOMOBILE_VIN_SORT_EXPRESSION = "Vin";
        public const string AUTOMOBILE_CHASSIS_SORT_EXPRESSION = "ChassisNumber";
        public const string AUTOMOBILE_MAKE_SORT_EXPRESSION = "Make";
        public const string AUTOMOBILE_MODEL_SORT_EXPRESSION = "Model";
        public const string AUTOMOBILE_OWNER_SORT_EXPRESSION = "Owner";

        public CarServiceConstants()
        {

        }
    }
}