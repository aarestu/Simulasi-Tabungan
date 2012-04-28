using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan
{
    class Pengguna : Id 
    {
        public Pengguna(string IdPengguna) : base(IdPengguna) { }
        public Pengguna() { }

        private string _pass;
        public string pass
        {
            get
            {
                throw new ArgumentException("value tidak bisa di ambil");
            }
            set
            {
                _pass = value;
            }
        }

        private string _Nama;
        public string Nama
        {
            get
            {
                return _Nama;
            }
            set
            {
                if (value != "")
                {
                    _Nama = value;
                }
                else
                {
                    throw new ArgumentException("Nama Tidak Boleh Kosong");
                }
            }
        }

        private string _Alamat;
        public string Alamat
        {
            get
            {
                return _Alamat;
            }
            set
            {
                if (value != "")
                {
                    _Alamat = value;
                }
                else
                {
                    throw new ArgumentException("Alamat Tidak Boleh Kosong");
                }
            }
        }

        public Boolean ValPass(string pass)
        {
            if (pass == _pass)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
