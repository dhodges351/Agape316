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
    public bool Monday { get; set; }
    public bool Tuesday { get; set; }
    public bool Wednesday { get; set; }
    public bool Thursday { get; set; }
    public bool Friday { get; set; }
    public bool Saturday { get; set; }
    public bool Sunday { get; set; }
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
            return $"{RecipientPhone} {RecipientEmail} {RecipientAddress} {RecipientCity} {RecipientState} {RecipientZipcode}";            
        }
    }

    public string RecipientMealSchedule
    {
        get
        {            
            return $"{(Monday ? "Monday" : "")} {(Tuesday ? "Tuesday" : "")} {(Wednesday ? "Wednesday" : "")} {(Thursday ? "Thursday" : "")} {(Friday ? "Friday" : "")} {(Saturday ? "Saturday" : "")} {(Sunday ? "Sunday" : "")} ";           
        }
    }

    public string RecipientFullName
    {
        get
        {
            return $"{RecipientFName} {RecipientLName}";
        }
    }

    public string EditLink
    {
        get
        {
            string link = string.Empty;
            if (string.IsNullOrEmpty(UserName))
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
