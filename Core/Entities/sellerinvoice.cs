using Core.Entities;

namespace core.Entities
{
    public class sellerinvoice : BaseEntity
    {
       public  int productid{get;set;}
       public string sellername{get;set;}
       public int quantity{get;set;}
        public int totalprice{get;set;}
        public string imageurl{get;set;}
        public string productname{get;set;}
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    }
}