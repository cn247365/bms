using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class filterRule
    {
        public string field { get; set; } = string.Empty;
        public string op { get; set; } = string.Empty;
        public string value { get; set; } = string.Empty;
    }
}