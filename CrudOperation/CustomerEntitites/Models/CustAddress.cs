using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEntities.Models
{
    [PrimaryKey(nameof(AddressId))]
    public class CustAddress
    {
        public int AddressId { get; set; }
        public string AddressText { get; set; }
    }
}
