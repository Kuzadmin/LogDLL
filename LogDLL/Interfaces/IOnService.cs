using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDLL.Interfaces
{
    interface IOnService
    {
        void Initialization();
        void Start();
        void Stop();
    }
}
