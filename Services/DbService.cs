using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using marvin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace marvin.Services
{
    public class DbService
    {
        private IConfigurationRoot config;
        private Settings settings;

        public DbService(IConfigurationRoot _config)
        {
            config = _config;
            settings = config.GetRequiredSection("Settings").Get<Settings>();
        }

        #region Household
        public string GetChoresbyDay(string day)
        {
            HouseholdContext context = new HouseholdContext(settings.HouseholdConnection);

            List<DailyChore> dailies = context.DailyChores.ToList();
            List<WeeklyChore> weeklies = context.WeeklyChores.ToList();

            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("Daily Chores");

            day = day.ToLower();

            if (day == "today")
            {
                day = DateTime.Today.DayOfWeek.ToString().ToLower();
            }

            switch (day)
            {
                case "monday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Monday);
                    }
                    break;
                case "tuesday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Tuesday);
                    }
                    break;
                case "wednesday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Wednesday);
                    }
                    break;
                case "thursday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Thursday);
                    }
                    break;
                case "friday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Friday);
                    }
                    break;
                case "saturday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Saturday);
                    }
                    break;
                case "sunday":
                    foreach (DailyChore daily in dailies)
                    {
                        strBuilder.AppendLine(daily.ChoreName + "....." + daily.Sunday);
                    }
                    break;
                default:
                    throw new ArgumentException();
            }

            string result = strBuilder.ToString();
            return result;
        }

        public string GetAllChores()
        {
            HouseholdContext context = new HouseholdContext(settings.HouseholdConnection);

            List<DailyChore> dailies = context.DailyChores.ToList();
            List<WeeklyChore> weeklies = context.WeeklyChores.ToList();

            StringBuilder strBuilder = new StringBuilder();

            strBuilder.AppendLine("Daily Chores");
            strBuilder.AppendLine("Chore ..... Monday ..... Tuesday ..... Wednesday ..... Thursday ..... Friday ..... Saturday ..... Sunday");

            foreach (DailyChore d in dailies)
            {
                strBuilder.AppendLine(d.ChoreName + "....." + d.Monday + "....." + d.Tuesday + "....." + d.Wednesday + "....." + d.Thursday + "....." + d.Friday + "....." + d.Saturday + "....." + d.Sunday);
            }

            return strBuilder.ToString();
        }
        #endregion
    }
}