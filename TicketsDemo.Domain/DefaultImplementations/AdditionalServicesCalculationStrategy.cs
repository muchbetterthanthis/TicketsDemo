using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Data.Entities;
using TicketsDemo.Domain.Interfaces;

namespace TicketsDemo.Domain.DefaultImplementations
{
//    public class ServicesContext {
//        public bool Tee { get; set; }
//    }

    public class AdditionalServicesCalculationStrategy : IPriceCalculationStrategy
    {
        public AdditionalServicesCalculationStrategy()
        {
        }

        public List<PriceComponent> CalculatePrice(PlaceInRun placeInRun /*,ServicesContext requestedServices*/)
        {
            var components = new List<PriceComponent>();

            //if (requestedServices.Tee)
            //{
                // тут какая то логика. много.
                // обращения к сервисам

                var beveragesPriceComponent = new PriceComponent()
                {
                    Name = "Beverages",
                    Value = 3
                };
                components.Add(beveragesPriceComponent);
            //}

            var domesticsPriceComponent = new PriceComponent()
            {
                Name = "Domestics",
                Value = 2
            };
            components.Add(domesticsPriceComponent);

            return components;
        }
    }
}
