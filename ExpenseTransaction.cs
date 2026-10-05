using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgettracker
{
    public class ExpenseTransaction: Transaction
    {
        
public decimal Amount { get; set; }

        public override decimal GetAmount()
        {
            return Amount;
        }

    }
}
