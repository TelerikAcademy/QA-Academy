using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using constants;
using persistence;
using System.Web.UI.WebControls;
using businesslogic;
using System.Web.UI;

/// <summary>
/// Summary description for SortingUtility
/// </summary>
public class SortingUtility
{
	public SortingUtility()
	{

	}

    #region Sorting utility

    public static IQueryable<RepairCard> SortRepairCards(IQueryable<RepairCard> repairCards,
        string sortExpression, SortDirection sortDirection)
    {
        bool ascending = sortDirection.Equals(SortDirection.Ascending);
        IQueryable<RepairCard> sortedCards;
        if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_ID_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.CardId);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.CardId);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_CHASSIS_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.Automobile.ChassisNumber);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.Automobile.ChassisNumber);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_Vin_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.Automobile.Vin);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.Automobile.Vin);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_START_DATE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.StartRepair);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.StartRepair);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_FINISH_DATE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.FinishRepair);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.FinishRepair);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.REPAIR_CARD_PRICE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedCards = repairCards.OrderBy(card => card.CardPrice);
            }
            else
            {
                sortedCards = repairCards.OrderByDescending(card => card.CardPrice);
            }
        }
        else
        {
            sortedCards = repairCards;
        }
        return sortedCards;
    }

    public static IEnumerable<CarServiceUser> SortUsers(List<CarServiceUser> users,
        string sortExpression, SortDirection sortDirection)
    {
        bool ascending = sortDirection.Equals(SortDirection.Ascending);
        IEnumerable<CarServiceUser> sortedUsers = null;
        if (sortExpression.Equals(CarServiceConstants.USER_NAME_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedUsers = users.OrderBy(user => user.UserName);
            }
            else
            {
                sortedUsers = users.OrderByDescending(user => user.UserName);
            }

        }
        else if (sortExpression.Equals(CarServiceConstants.USER_EMAIL_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedUsers = users.OrderBy(user => user.Email);
            }
            else
            {
                sortedUsers = users.OrderByDescending(user => user.Email);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.USER_FIRST_NAME_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedUsers = users.OrderBy(user => user.FirstName);
            }
            else
            {
                sortedUsers = users.OrderByDescending(user => user.FirstName);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.USER_LAST_NAME_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedUsers = users.OrderBy(user => user.LastName);
            }
            else
            {
                sortedUsers = users.OrderByDescending(user => user.LastName);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.USER_ACTIVE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedUsers = users.OrderBy(user => user.IsActive);
            }
            else
            {
                sortedUsers = users.OrderByDescending(user => user.IsActive);
            }
        }
        return sortedUsers;
    }

    public static IQueryable<SparePart> SortSpareParts(IQueryable<SparePart> spareParts,
                string sortExpression, SortDirection sortDirection)
    {
        bool ascending = sortDirection.Equals(SortDirection.Ascending);
        IQueryable<SparePart> sortedSpareParts = spareParts;
        if (sortExpression.Equals(CarServiceConstants.SPARE_PART_ID_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedSpareParts = spareParts.OrderBy(part => part.PartId);
            }
            else
            {
                sortedSpareParts = spareParts.OrderByDescending(part => part.PartId);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.SPARE_PART_NAME_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedSpareParts = spareParts.OrderBy(part => part.Name);
            }
            else
            {
                sortedSpareParts = spareParts.OrderByDescending(part => part.Name);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.SPARE_PART_PRICE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedSpareParts = spareParts.OrderBy(part => part.Price);
            }
            else
            {
                sortedSpareParts = spareParts.OrderByDescending(part => part.Price);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.SPARE_PART_ACTIVE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedSpareParts = spareParts.OrderBy(part => part.IsActive);
            }
            else
            {
                sortedSpareParts = spareParts.OrderByDescending(part => part.IsActive);
            }
        }
        return sortedSpareParts;
    }

    public static IQueryable<Automobile> SortAutomobiles(IQueryable<Automobile> automobiles,
                string sortExpression, SortDirection sortDirection)
    {
        bool ascending = sortDirection.Equals(SortDirection.Ascending);
        IQueryable<Automobile> sortedAutomobiles = automobiles;
        if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_ID_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.AutomobileId);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.AutomobileId);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_VIN_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.Vin);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.Vin);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_CHASSIS_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.ChassisNumber);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.ChassisNumber);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_MAKE_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.Make);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.Make);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_MODEL_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.Model);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.Model);
            }
        }
        else if (sortExpression.Equals(CarServiceConstants.AUTOMOBILE_OWNER_SORT_EXPRESSION))
        {
            if (ascending)
            {
                sortedAutomobiles = automobiles.OrderBy(auto => auto.Owner);
            }
            else
            {
                sortedAutomobiles = automobiles.OrderByDescending(auto => auto.Owner);
            }
        }
        return sortedAutomobiles;
    }


    public static SortDirection GetSortDirection(StateBag viewState)
    {
        SortDirection newSortDirection = SortDirection.Descending;
        object currentSortDirectionObject = viewState[CarServiceConstants.SORT_DIRECTION_VIEW_STATE_ATTR];
        if (currentSortDirectionObject != null)
        {
            SortDirection currentSortDirection = (SortDirection)currentSortDirectionObject;
            newSortDirection = (currentSortDirection.Equals(SortDirection.Ascending)) ? SortDirection.Descending : SortDirection.Ascending;
        }
        return newSortDirection;
    }

    public static void SetSortDirectionImage(GridView gridView, GridViewRow headerRow,
    string sortExpression, SortDirection sortDirection)
    {
        int sortColumnIndex = GetSortColumnIndex(gridView, sortExpression);
        if (sortColumnIndex != -1)
        {
            AddSortImage(headerRow, sortColumnIndex, sortDirection);
        }
    }

    private static int GetSortColumnIndex(GridView gridView, string sortExpression)
    {
        if (string.IsNullOrEmpty(sortExpression) == false)
        {
            DataControlFieldCollection columns = gridView.Columns;
            foreach (DataControlField field in columns)
            {
                if (field.SortExpression.Equals(sortExpression))
                {
                    return gridView.Columns.IndexOf(field);
                }
            }
        }
        return -1;
    }

    private static void AddSortImage(GridViewRow headerRow, int columnIndex, SortDirection sortDirection)
    {
        Image sortImage = new Image();
        if (sortDirection == SortDirection.Ascending)
        {
            sortImage.ImageUrl = "~/Resources/Images/up_arrow.gif";
            sortImage.AlternateText = "Ascending Order";
        }
        else
        {
            sortImage.ImageUrl = "~/Resources/Images/down_arrow.gif";
            sortImage.AlternateText = "Descending Order";
        }
        headerRow.Cells[columnIndex].Controls.Add(sortImage);
    }

    #endregion
}