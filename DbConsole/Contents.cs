using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbConsole
{
    public class Contents
    {

        public int id { get; set; }
        public string batchNumber { get; set; }
        public int uploaded { get; set; }
        public decimal Unit_Cost { get; set; }
        public string Posting_Status { get; set; }
        public string TRX_Location { get; set; }
        public DateTime dateCreated { get; set; }
        public DateTime dateModified { get; set; }
        public string ProdId { get; set; }
        public string ProdName { get; set; }
        public DateTime expiryDate { get; set; }

        //public string batchNo { get; set; }
        //public DateTime expiryDate { get; set; }
        //public string prodId { get; set; }
        //public string prodName { get; set; }
        //public string quantity { get; set; }
        //public decimal unitPrice { get; set; }
        //public string docNo { get; set; }

    }

    public class ContentsJson
    {
      public  List<Contents> contents { get; set; }
    }

    public class Root
    {
        public ContentsJson contentsJson { get; set; }
    }
}

