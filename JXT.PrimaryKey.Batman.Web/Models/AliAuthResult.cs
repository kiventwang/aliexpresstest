using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;


namespace JXT.PrimaryKey.Batman.Web.Models
{
    [DataContract]
    public class AliAuthResult
    {
        [DataMember(Name = "refresh_token_timeout")]
        public string Refresh_Token_TimeOut { get; set; }

        [DataMember(Name="aliId")]
        public string AliId{get;set;}

        [DataMember(Name = "resource_owner")]
        public string Resource_Owner { get; set; }

        [DataMember(Name="expires_in")]
        public string Expires_In { get; set; }

        [DataMember(Name="refresh_token")]
        public string Refresh_Token { get; set; }

        [DataMember(Name = "access_token")]
        public string Access_Token { get; set; }
    }
}