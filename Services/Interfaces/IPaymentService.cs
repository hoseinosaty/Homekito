
using Barayand.OutModels.Miscellaneous;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Interfaces
{
    public interface IPaymentService
    {
        void SetupService();
        SessionCreateOptions PrepareSession(BasketModel basket, decimal wallet,Barayand.DAL.Interfaces.IWalletHistoryRepository _walletrepository,int user);
        SessionCreateOptions CreateRechargeSession(int userId,decimal Amount);
    }
}
