﻿using System;
using Pathoschild.Stardew.LookupAnything.Framework.Constants;
using StardewValley;

namespace Pathoschild.Stardew.LookupAnything.Framework
{
    /// <summary>Represents an in-game date.</summary>
    public class GameDate
    {
        /*********
        ** Accessors
        *********/
        /// <summary>The season name.</summary>
        public string Season { get; }

        /// <summary>The day in the current season.</summary>
        public int Day { get; }

        /// <summary>The current year.</summary>
        public int Year { get; }

        /// <summary>The number of days in a season.</summary>
        public int DaysInSeason { get; }


        /*********
        ** Public methods
        *********/
        /// <summary>Construct an instance.</summary>
        /// <param name="season">The season name.</param>
        /// <param name="day">The day in the current season.</param>
        /// <param name="year">The current year.</param>
        /// <param name="daysInSeason">The number of days in a season.</param>
        public GameDate(string season, int day, int year, int daysInSeason)
        {
            this.Season = season;
            this.Day = day;
            this.Year = year;
            this.DaysInSeason = daysInSeason;
        }

        /// <summary>Add a day offset to the current date.</summary>
        /// <param name="offset">The offset to add in days.</param>
        /// <returns>Returns the resulting season and day.</returns>
        public GameDate GetDayOffset(int offset)
        {
            // simple case
            string season = this.Season;
            int day = this.Day + offset;
            int year = this.Year;

            // handle season transition
            if (day > this.DaysInSeason)
            {
                string[] seasons = { Constant.SeasonNames.Spring, Constant.SeasonNames.Summer, Constant.SeasonNames.Fall, Constant.SeasonNames.Winter };
                int curSeasonIndex = Array.IndexOf(seasons, Game1.currentSeason);
                if (curSeasonIndex == -1)
                    throw new InvalidOperationException($"The current season '{Game1.currentSeason}' wasn't recognised.");
                int seasonTransitions = curSeasonIndex + day / this.DaysInSeason;

                season = seasons[seasonTransitions % seasons.Length];
                year += seasonTransitions / seasons.Length;
                day %= this.DaysInSeason;
            }

            return new GameDate(season, day, year, this.DaysInSeason);
        }

        /// <summary>Get a string representation of the date.</summary>
        public override string ToString()
        {
            return $"{this.Season} {this.Day}";
        }
    }
}
