using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTest.Services.Models
{ 
    public class Address
    {
        public string addressLine1 { get; set; }
        public string addressLine2 { get; set; }
        public string city { get; set; }
        public string postcode { get; set; }
        public string country { get; set; }
    }

    public class DebtInfo
    {
        public string originalCreditor { get; set; }
        public string debtReference { get; set; }
        public double originalBalance { get; set; }
        public double currentBalance { get; set; }
        public double interestRate { get; set; }
        public DateTime dateIncurred { get; set; }
        public DateTime lastPaymentDate { get; set; }
        public string status { get; set; }
        public string currency { get; set; }
    }

    public class DebtorInfo
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Address address { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class AccountDetails
    {
        public string id { get; set; }
        public string accountId { get; set; }
        public DebtorInfo debtorInfo { get; set; }
        public DebtInfo debtInfo { get; set; }
    }


}
