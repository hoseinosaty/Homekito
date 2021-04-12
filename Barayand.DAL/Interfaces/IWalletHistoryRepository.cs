using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Interfaces
{
    public interface IWalletHistoryRepository : IPublicMethodRepsoitory<WalletHistoryModel>
    {
        Task<List<WalletHistoryModel>> GetAllUserTransactions(int user);
        Task<ResponseStructure> RechargeWallet(int user,decimal amount,string recieptInfo = "");
        Task<ResponseStructure> DecreaseWallet(int user,decimal amount);
        Task<ResponseStructure> LogTransaction(WalletHistoryModel historyModel);
    }
}
