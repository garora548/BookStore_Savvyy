using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore_Domain.Models
{
    public class Response<T>
    {
        public T Data { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }        
    }
}
