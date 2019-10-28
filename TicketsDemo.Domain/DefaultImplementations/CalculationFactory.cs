using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsDemo.Domain.DefaultImplementations;
using TicketsDemo.Domain.DefaultImplementations.PriceCalculationStrategy;
using TicketsDemo.Data.Repositories;
using TicketsDemo.Domain.Interfaces;
using TicketsDemo.Data.Entities;

namespace TicketsDemo.Domain.DefaultImplementations
{
    public class CalculationFactory : ICalculationFactory
    {
        private IRunRepository _runRepository;
        private ITrainRepository _trainRepository;

        public CalculationFactory(IRunRepository runRepository, ITrainRepository trainRepository)
        {
            _runRepository = runRepository;
            _trainRepository = trainRepository;
        }

        public IPriceCalculationStrategy create(bool iNeedAdditionalServices)
        {
            if (iNeedAdditionalServices)
            {
                return new PriceCalculationAggregator(
                    new DefaultPriceCalculationStrategy(_runRepository, _trainRepository), 
                    new AdditionalServicesCalculationStrategy()
                    );
            }
            else
            {
                return new DefaultPriceCalculationStrategy(_runRepository, _trainRepository);
            }
        }
    }

}
