using Barayand.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Barayand.OutModels.Miscellaneous
{
    public class AttributeAnswerList
    {
        public AttributeModel Attribute { get; set; }
        public List<AttrAnswerModel> Answers { get; set; }
    }
}
