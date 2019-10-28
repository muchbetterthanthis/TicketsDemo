using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class PriceCalculationAggregator : IPriceCalculationStrategy
    {
        IPriceCalculationStrategy _strat1;
        IPriceCalculationStrategy _strat2;

        public PriceCalculationAggregator(IPriceCalculationStrategy strat1, IPriceCalculationStrategy strat2)
        {
            _strat1 = strat1;
            _strat2 = strat2;
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun)
        {
            var components = new List<PriceComponent>();

            components.AddRange(_strat1.CalculatePrice(placeInRun));
            components.AddRange(_strat2.CalculatePrice(placeInRun));

            return components;
        }

    }
}
