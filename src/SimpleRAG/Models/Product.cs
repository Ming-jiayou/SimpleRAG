using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleRAG.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Address: {Address}";
        }
    }
}
