using System.Collections.Generic;

namespace App.Testes.Integrados.Models
{
    public class ResponseTestes
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public dynamic Result { get; set; }
    }
}