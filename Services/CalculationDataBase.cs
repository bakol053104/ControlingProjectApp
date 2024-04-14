using ControlingProjectApp.Data.Entities;

namespace ControlingProjectApp.Services;

public abstract class CalculationDataBase
    {
        protected static decimal SetHourlyRate(JobPosition jobposition)
        {
            var hourly = 0.0m; ;
            switch (jobposition)
            {
                case JobPosition.Employee:
                    hourly = 100.0m;
                    break;
                case JobPosition.Engineer:
                    hourly = 200.0m;
                    break;
                case JobPosition.Manager:
                    hourly = 300.0m;
                    break;
                case JobPosition.Supervisor:
                    hourly = 400.0m;
                    break;
            }
            return hourly;
        }
    }

