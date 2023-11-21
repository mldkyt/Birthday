using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayApp
{
    [Serializable]
    public class DataType
    {
        public string Name { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }

        public override string ToString()
        {
            return $"({Month}/{Day}){Name}";
        }
    }
}
