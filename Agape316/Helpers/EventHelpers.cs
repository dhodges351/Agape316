using Agape316.Enums;

namespace Agape316.Helpers
{
    public static class EventHelpers
    {
        public static string GetCategoryName(int categoryId)
        {
            var categoryName = string.Empty;
            var categories = Enum.GetNames(typeof(CategoryEnum)).ToList();
            for (int i = 0; i < categories.Count; i++)
            {
                if ((i + 1) == categoryId)
                {
                    categoryName = categories[i].ToString();
                    break;
                }
            }
            return categoryName;
        }


        public static string GetStandardTimeFromMilitaryTime(string timeToCheck)
        {
            if (string.IsNullOrEmpty(timeToCheck))
            { return string.Empty; }

            string output = string.Empty;
            int militaryHour = Convert.ToInt32(timeToCheck.Split(':')[0]);
            string militaryMinutes = timeToCheck.Split(":")[1];
            militaryMinutes = militaryMinutes.Split(" ")[0];
            int standardHour = 0;
            string strStandardHour = "0";

            if (militaryHour > 12)
            {
                standardHour = militaryHour - 12;
                if (standardHour < 10)
                {
                    strStandardHour = "0" + standardHour.ToString();
                }
                else
                {
                    strStandardHour = standardHour.ToString();
                }
            }
            else
            {
                strStandardHour = militaryHour.ToString();
            }

            if (militaryHour >= 1 && militaryHour <= 12)
            {
                output = strStandardHour + ":" + militaryMinutes + " AM";
            }
            else if (militaryHour >= 12 && militaryHour <= 24)
            {
                output = strStandardHour + ":" + militaryMinutes + " PM";
            }

            return output;
        }
    }
}
