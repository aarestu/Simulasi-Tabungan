using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan
{
    abstract class Id
    {
        private string _IdPengguna;
        public string IdPengguna
        {
            get
            {
                return _IdPengguna;
            }
            set
            {
                if (value != "")
                {
                    _IdPengguna = value;
                }
                else
                {
                    throw new ArgumentException("Id Tidak Boleh Kosong");
                }
            }
        }

        public Id(string IdPengguna)
        {
            this.IdPengguna = IdPengguna;
        }

        public Id()
        {
        }

        
    }
}
