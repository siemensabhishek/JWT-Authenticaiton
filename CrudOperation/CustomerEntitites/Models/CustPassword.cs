using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerEntities.Models
{
    [PrimaryKey(nameof(CustomerId))]
    public class CustPassword
    {
        public int CustomerId { get; set; }
        public string? CPassword { get; set; }
    }
}
