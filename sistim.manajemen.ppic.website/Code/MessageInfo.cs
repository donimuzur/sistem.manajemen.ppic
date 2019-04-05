using sistem.manajemen.ppic.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sistem.manajemen.ppic.website.Code
{
    public class MessageInfo
    {
        public List<string> MessageText { get; set; }
        public Enums.MessageInfoType MessageInfoType { get; set; }

        public MessageInfo()
        {
        }

        public MessageInfo(List<string> messagetext, Enums.MessageInfoType messageinfotype)
        {
            MessageText = messagetext;
            MessageInfoType = messageinfotype;
        }
    }
}