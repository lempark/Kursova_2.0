using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Models
{
    public class Workbench
    {
        public string Name { get; set; }
        public List<double> PassportSupply { get; set; }
        public List<double> PassportRotationalSpeed { get; set; }
    }
}
