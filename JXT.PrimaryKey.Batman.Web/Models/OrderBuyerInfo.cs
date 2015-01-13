using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXT.PrimaryKey.Batman.Web.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class OrderBuyerInfo
    {
        
        [DataMember(Name = "buyerSignerFullname")]
        public string BuyerSignerFullName { get; set; }

        [DataMember(Name = "castro")]
        public string LastName { get; set; }

        [DataMember(Name = "loginId")]
        public string LoginId { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name="country")]
        public string Country { get; set; }
    }
}