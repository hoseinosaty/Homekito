using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barayand.Services.Services
{
    public class FileAccessSerivce : IFileAccessService
    {
        private readonly IPublicMethodRepsoitory<InvoiceModel> _invoicerepo;
        private readonly IPublicMethodRepsoitory<OrderModel> _Orderrepo;
        public FileAccessSerivce(IPublicMethodRepsoitory<InvoiceModel> invoicerepo, IPublicMethodRepsoitory<OrderModel> orderrepo)
        {
            _invoicerepo = invoicerepo;
            _Orderrepo = orderrepo;
        }
        public async Task<bool> UserAccessProductFile(int pid, int userid)
        {
            try
            {
                List<OrderModel> orders = ((List<OrderModel>)(await _Orderrepo.GetAll()).Data);
                List<InvoiceModel> invoices = ((List<InvoiceModel>)(await _invoicerepo.GetAll()).Data);

                List<InvoiceModel> userInvoices = invoices.Where(x=>x.I_UserId == userid && x.I_Status > 0).ToList();
                if(userInvoices.Count < 1)
                {
                    return false;
                }
                foreach(var i in userInvoices)
                {
                    if(orders.Count(x => x.O_ReciptId == i.I_Id && x.O_ProductId == pid) > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
