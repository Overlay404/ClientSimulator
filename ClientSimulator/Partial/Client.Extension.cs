using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSimulator.Model
{
    partial class Client
    {
        public string Fullname => $"{Name} {Surname} {Patronymic}";
    }
}
