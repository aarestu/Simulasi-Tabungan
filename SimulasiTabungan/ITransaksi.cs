using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan
{
    interface ITransaksi : ISimpan, IAmbil
    {
        int saldo { get; set; }
    }
}
