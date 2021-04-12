using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Barayand.Common.Constants;
using Barayand.Common.Services;
using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Miscellaneous;
using Barayand.OutModels.Models;
using Barayand.OutModels.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Barayand.DAL.Repositories
{
    public class WalletHistoryRepository : GenericRepository<WalletHistoryModel>,IPublicMethodRepsoitory<WalletHistoryModel>,IWalletHistoryRepository
    {
        private readonly BarayandContext _context;

        public WalletHistoryRepository(BarayandContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<ResponseStructure> DecreaseWallet(int user, decimal amount)
        {
           try
            {
                UserModel userModel = this._context.Users.FirstOrDefault(x=>x.U_Id == user);
                if(userModel.U_Wallet < amount)
                {
                    return ResponseModel.Error("Account balance is not enough");
                }
                userModel.U_Wallet = userModel.U_Wallet - amount;
                this._context.Users.Update(userModel);
                await this._context.SaveChangesAsync();
                await this.LogTransaction(new WalletHistoryModel() {
                    WH_AdderId = 0,
                    WH_CustomerId = user,
                    WH_Amount = amount,
                    WH_TransactionType = 2,
                    WH_PayType = 0,
                    WH_PayBankRecip = ""
                });
                return ResponseModel.Success("Transaction completed successfully.");
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError("Server Internal Error",ex);
            }
        }

        public async Task<List<WalletHistoryModel>> GetAllUserTransactions(int user)
        {
            try
            {
                return this._context.WalletHistory.Where(x=>x.WH_CustomerId == user).ToList();
            }
            catch(Exception ex)
            {
                return new List<WalletHistoryModel>();
            }
        }

        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseStructure> LogTransaction(WalletHistoryModel historyModel)
        {
            try
            {
                await this.Insert(historyModel);
                await this.CommitAllChanges();
                return ResponseModel.Success("Transaction logged successfully.");
            }
            catch(Exception ex)
            {
                return ResponseModel.ServerInternalError("Error in logging wallet transaction.",ex);
            }
        }

        public async Task<ResponseStructure> RechargeWallet(int user, decimal amount, string recieptInfo = "")
        {
            try
            {
                UserModel userModel = this._context.Users.FirstOrDefault(x => x.U_Id == user);
                if (amount == 0)
                {
                    return ResponseModel.Error("Cannot increase zero amount");
                }
                userModel.U_Wallet = userModel.U_Wallet + amount;
                this._context.Users.Update(userModel);
                await this._context.SaveChangesAsync();
                await this.LogTransaction(new WalletHistoryModel()
                {
                    WH_AdderId = 0,
                    WH_CustomerId = user,
                    WH_Amount = amount,
                    WH_TransactionType = 1,
                    WH_PayType = 0,
                    WH_PayBankRecip = recieptInfo
                });
                return ResponseModel.Success("Transaction completed successfully.");
            }
            catch (Exception ex)
            {
                return ResponseModel.ServerInternalError("Server Internal Error", ex);
            }
        }
    }
}
