using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Orleans_BettingSite.States;
using Random = System.Random;

namespace Orleans_BettingSite_UnitTesting.Helper
{
    public static class BetGrainHelper
    {
        public static AmountState AmountStateData()
        {
            return new Faker<AmountState>()
                .RuleFor(x => x.Amount, (decimal) new Random().NextDouble()).Generate();
        }
    }
}
