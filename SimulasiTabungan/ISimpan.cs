using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan
{
    interface ISimpan
    {
        void Simpan(int Nominal, string Keterangan);
    }
}
