﻿using Barayand.DAL.Context;
using Barayand.DAL.Interfaces;
using Barayand.Models.Entity;
using Barayand.OutModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.DAL.Repositories
{
    public class IndexBoxInfoRepository:GenericRepository<IndexBoxInfoModel>,IPublicMethodRepsoitory<IndexBoxInfoModel>
    {
        private readonly BarayandContext _context;
        public IndexBoxInfoRepository(BarayandContext context):base(context)
        {
            _context = context;
        }

        public Task<ResponseStructure> LogicalAvailable(object id, bool newState)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseStructure> LogicalDelete(object id)
        {
            throw new NotImplementedException();
        }
    }
}
