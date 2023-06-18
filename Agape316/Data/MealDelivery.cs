using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Agape316.Data;

public partial class MealDelivery
{
    public MealDelivery()
    {}
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string? Phone { get; set; }
    public DateTime Created { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string DeliveryTime { get; set; }
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
    public string ContactProfileLink
    {
        get
        {
            string link = string.Empty;
            if (string.IsNullOrEmpty(UserName))
            {
                link = $"<a href='#' class='disabled-link'>Contact</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(Email))
            {
                link = $"<a href='/Identity/Account/Profile/' class='enabled-link'>Contact</a>";
            }
            return link;
        }
    }

    public string ContactFullName
    {
        get
        {
            return $"{FirstName} {LastName}";            
        }
    }

    public string EditLink
    {
        get
        {
            string link = string.Empty;
            MealSchedule mealSchedule = null;
            DateTime date1 = DateTime.Now;
            DateTime? date2 = null;
            bool isExpired = true;

            string connectionString = Agape316.Helpers.ConfigHelper.AppSetting("DefaultConnection");
            var contextOptions = new DbContextOptionsBuilder<Agape316.Data.ApplicationDbContext>()
            .UseSqlServer(connectionString)
            .Options;

            using (var context = new Agape316.Data.ApplicationDbContext(contextOptions))
            {
                mealSchedule = context.MealSchedule.Where(x => x.Id == ScheduleId).FirstOrDefault();
                if (mealSchedule != null)
                {
                    date2 = DateTime.Parse($"{mealSchedule.EndDate.ToShortDateString()}");
                    int result = DateTime.Compare(date1, date2.Value);
                    if (result <= 0)
                    {
                        isExpired = false;
                    }
                }
            };

            if (string.IsNullOrEmpty(UserName) || isExpired)
            {
                link = $"<a href='#' class='disabled-link')'>Edit</a>";
            }
            else if (!string.IsNullOrEmpty(UserName) && UserName.Equals(Email))
            {
                link = $"<a href='#' class='enabled-link' onclick='EditDelivery({ScheduleId}, {Id})'>Edit</a>";
            }
            return link;
        }
    }
}
