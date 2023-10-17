using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lottery.DoMain.Models
{
    public class Chanel
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
    public class ChanelGroup
    {
        public string Key { get; set; }
        public List<Chanel> Value { get; set; }
    }
}
