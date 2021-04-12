using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IPriceCalculatorService
    {
        Task<PriceModel> CalculateBookPrice(int pid, string lang);
    }
}