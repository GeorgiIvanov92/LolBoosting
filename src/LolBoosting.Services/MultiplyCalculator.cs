using LoLBoosting.Contracts.Dtos;
using System;
using LoLBoosting.Contracts.Models;

namespace LolBoosting.Services
{
    public class MultiplyCalculator
    {
        public double GetMultiplier(EDivision division,int points, EOrderType orderType)
        {
            double multiplier = 1.25;

            multiplier -= ((int) division * 0.10);
            multiplier += points * 0.001;

            switch (orderType)
            {
                case EOrderType.GamesBoost:
                {
                    multiplier *= 0.80;
                    break;
                }

                case EOrderType.DuoQWinsBoost:
                {
                    multiplier *= 1.25;
                    break;
                }
            }
            return multiplier;
        }
    }
}
