﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogDLL
{
    interface IForThread
    {
        void Set_Stop(bool stop);
        bool Is_Started();
        void LOOP();
    }
}
