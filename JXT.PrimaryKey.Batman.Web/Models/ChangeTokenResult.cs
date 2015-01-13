using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JXT.PrimaryKey.Batman.Web.Models
{
    using System.Runtime.Serialization;

    [DataContract]
    public class ChangeTokenResult
    {

        [DataMember(Name="aliId")]
        public string AliId{get;set;}

        [DataMember(Name="resource_owner")]
        public string Resource_Owner{get;set;}

        [DataMember(Name="expires_in")]
        public string Expires_In{get;set;}

        [DataMember(Name="access_token")]
        public string Access_Token{get;set;}
    }
}