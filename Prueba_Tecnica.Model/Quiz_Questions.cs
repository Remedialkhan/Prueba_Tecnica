using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Tecnica.Model
{
    public class Quiz_Questions
    {
        public int id { get; set; }
        public Guid idquiz { get; set; }
        public double? numberquestion { get; set; }
        public String? letteranswer { get; set; }
    }
}
