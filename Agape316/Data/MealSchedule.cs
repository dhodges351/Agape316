using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class MealSchedule
{
    public MealSchedule()
    {}
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Coordinator { get; set; }
    public string? CoordEmail { get; set; }
    public string? CoordPhone { get; set; }
    public string? RecipientFName { get; set; }
    public string? RecipientLName { get; set; }
    public string? RecipientEmail { get; set; }
    public string? RecipientPhone { get; set; }
    public string? RecipientAddress { get; set; }
    public string? RecipientCity { get; set; }
    public string? RecipientState { get; set; }
    public string? RecipientZipcode { get; set; }
    public DateTime Created { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }    
    public string? FoodAllergies { get; set; }
    public string? Notes { get; set; }
    public string? UserName
    {
        get
        {
            string userName = string.Empty;
            if (Agape316.Helpers.AppContext.Current != null)
            {
                userName = Agape316.Helpers.AppContext.Current.Session.GetString("UserName");
            }
            return userName;
        }
    }
    public string CoordinatorProfileLink
    {
        get
        {
            string link = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                link = $"<a href='#' class='disabled-link'>Coordinator</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(CoordEmail))
            {
                link = $"<a href='/Identity/Account/Profile/' class='enabled-link'>Coordinator</a>";
            }
            return link;
        }
    }

    public string RecipientInfo
    {
        get
        {
            return $"{RecipientAddress} {RecipientCity} {RecipientState}, {RecipientZipcode}";            
        }
    }

    public string RecipientInfoLink
    {
        get
        {            
            string link = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                link = $"<a href='#' class='disabled-link'>Recipient</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(CoordEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='showRecipientModal(\"{RecipientFullName}\", \"{RecipientFullAddress}\", \"{FoodAllergies}\")'>Recipient</a>";
            }
            return link;
        }
    }    

    public string RecipientFullName
    {
        get
        {
            return $"{RecipientFName} {RecipientLName}";
        }
    }

    public string RecipientFullAddress
    {
        get
        {
            return $"{RecipientAddress} {RecipientCity} {RecipientState}, {RecipientZipcode}";            
        }
    }

    public string EditLink
    {
        get
        {
            string link = string.Empty;
            DateTime date1 = DateTime.Now;
            DateTime date2 = DateTime.Parse($"{EndDate.ToShortDateString()}");
            int result = DateTime.Compare(date1, date2);
            bool isExpired = true;

            if (result <= 0)
            {
                isExpired = false;
            }

            if (string.IsNullOrEmpty(UserName) || isExpired)
            {
                link = $"<a href='#' class='disabled-link')'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(CoordEmail))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditMealSchedule({Id})'>Edit</a>";
            }
            return link;
        }
    }
}
