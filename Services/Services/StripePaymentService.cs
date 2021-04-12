using Microsoft.Extensions.Configuration;
using Barayand.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Stripe.Checkout;
using Barayand.Models.Entity;
using Newtonsoft.Json;
using Barayand.OutModels.Miscellaneous;
using Barayand.Common;
using System.Text.RegularExpressions;

namespace Barayand.Services.Services
{
    public class StripePaymentService : IPaymentService
    {
        IConfiguration Configuration;

        private readonly Stripe.Token PaymentToken;
        public StripePaymentService(IConfiguration configuration)
        {
            Configuration = configuration;

            SetupService();
        }
        public void SetupService()
        {
            StripeConfiguration.ApiKey = Configuration["Stripe:ApiKey"];

        }
        public SessionCreateOptions PrepareSession(BasketModel basket, decimal wallet, Barayand.DAL.Interfaces.IWalletHistoryRepository _walletrepository, int user)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                      "card",
                    },
                    LineItems = AddProducts(basket,wallet,_walletrepository, user),
                    Mode = "payment",
                    SuccessUrl = "https://localhost:44320/cart/payment/success?session_id={CHECKOUT_SESSION_ID}",
                    //SuccessUrl = "https://valhallaplanet.art/cart/payment/success?session_id={CHECKOUT_SESSION_ID}",
                    //CancelUrl = "https://valhallaplanet.art/cart/payment/cancel?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://localhost:44320/cart/payment/cancel?session_id={CHECKOUT_SESSION_ID}",

                };

                return options;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private List<SessionLineItemOptions> AddProducts(BasketModel basket,decimal wallet, Barayand.DAL.Interfaces.IWalletHistoryRepository _walletrepository,int user)
        {
            List<SessionLineItemOptions> Items = new List<SessionLineItemOptions>();

            var shippingCost = (basket.CartItems.Count > 0) ? basket.ShippingCost / basket.TotalQuantity() : basket.ShippingCost;
            decimal WALLET = wallet;
            decimal USEDWALLET = 0;
            foreach (var item in basket.CartItems)
            {
                SessionLineItemOptions ItemOptions = new SessionLineItemOptions();
                SessionLineItemPriceDataOptions priceData = new SessionLineItemPriceDataOptions();
                SessionLineItemPriceDataProductDataOptions prdData = new SessionLineItemPriceDataProductDataOptions();

                prdData.Name = item.Product.P_Code + "|" + item.Product.P_Title;
                prdData.Images = new List<string>() { Barayand.Common.Services.UtilesService.MediaUrls("ProductMainImage") + item.Product.P_Image };
                //prdData.Description = (!string.IsNullOrEmpty(item.Product.P_Description)) ? Regex.Replace(item.Product.P_Description, "<.*?>", String.Empty) : "Valhallaplanet Product";

                //priceData.ProductData = prdData;
                //priceData.Currency = "eur";
                //priceData.UnitAmountDecimal = item.Product.FinalPrice(basket.SumDiscount(), shippingCost) * 100;
                //decimal TotalUnit = item.Product.FinalPrice(basket.SumDiscount(), shippingCost);
                if (WALLET > 0)
                {
                    //if (TotalUnit <= WALLET)
                    //{
                    //    priceData.UnitAmountDecimal = 0;
                    //    USEDWALLET +=  TotalUnit;
                    //    WALLET = WALLET - TotalUnit;
                    //}
                    //else
                    //{
                    //    USEDWALLET += WALLET;
                    //    priceData.UnitAmountDecimal = ((priceData.UnitAmountDecimal / 100) - WALLET) * 100;
                    //}
                }

                ItemOptions.Quantity = item.Quantity;
                ItemOptions.PriceData = priceData;

                Items.Add(ItemOptions);
            }
            if(USEDWALLET > 0)
            {
                _walletrepository.DecreaseWallet(user,USEDWALLET);
            }
            return Items;
        }

        public SessionCreateOptions CreateRechargeSession(int userId, decimal Amount)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    PaymentMethodTypes = new List<string>
                    {
                      "card",
                    },
                    LineItems = CreateRecharge(userId,Amount),
                    Mode = "payment",
                    SuccessUrl = "https://localhost:44320/Recharge/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                    //SuccessUrl = "https://valhallaplanet.art/Recharge/Payment/Success?session_id={CHECKOUT_SESSION_ID}",
                    //CancelUrl = "https://valhallaplanet.art/Recharge/Payment/Cancel?session_id={CHECKOUT_SESSION_ID}",
                    CancelUrl = "https://localhost:44320/Recharge/Payment/Cancel?session_id={CHECKOUT_SESSION_ID}",

                };

                return options;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        private List<SessionLineItemOptions> CreateRecharge(int userId, decimal Amount)
        {
            List<SessionLineItemOptions> Items = new List<SessionLineItemOptions>();
            SessionLineItemOptions ItemOptions = new SessionLineItemOptions();
            SessionLineItemPriceDataOptions priceData = new SessionLineItemPriceDataOptions();
            SessionLineItemPriceDataProductDataOptions prdData = new SessionLineItemPriceDataProductDataOptions();

            prdData.Name = "Customer Recharge Wallet | "+userId;
            prdData.Images = new List<string>() { "https://valhallaplanet.art/img/Logo.png" };
            prdData.Description = "Valhallaplanet User Wallet Recharge";

            priceData.ProductData = prdData;
            priceData.Currency = "eur";
            priceData.UnitAmountDecimal = Amount * 100;


            ItemOptions.Quantity = 1;
            ItemOptions.PriceData = priceData;

            Items.Add(ItemOptions);

            return Items;
        }
    }
}
