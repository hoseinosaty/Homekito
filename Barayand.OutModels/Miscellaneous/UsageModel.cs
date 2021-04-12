using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class UsageModel
    {
        public int PrintedBooks { get; set; } = 0;
        public int RequestedBook { get; set; } = 0;
        public int Langs { get; set; } = 0;
        public int Sales { get; set; } = 0;
        public int BooksCount { get; set; } = 0;
        public int UsersCount { get; set; } = 0;

    }
}
