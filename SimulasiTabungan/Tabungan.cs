using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan 
{
    sealed class Tabungan : Id, ITransaksi
    {
        //konstruktor 
        public Tabungan(string IdPengguna) : base(IdPengguna) { }

        public Tabungan() { }

        private struct Transaksi
        {
            public int Nominal;
            public string keterangan;
            public DateTime waktu;
        }

        private List<Transaksi> History = new List<Transaksi>();
        private Transaksi temp;

        //propertis
        private int _saldo;
        public int saldo
        {
            get
            {
                return _saldo;
            }
            set
            {
                throw new ArgumentException("Tidak Bisa di rubah");
            }
        }


        public void Simpan(int Nominal, string Keterangan)
        {
            temp.keterangan = Keterangan;
            temp.Nominal = Nominal;
            temp.waktu = DateTime.Now;
            _saldo += temp.Nominal;
            History.Add(temp);
        }

        public void Ambil(int Nominal, string Keterangan)
        {
            if (_saldo < Nominal)
            {
                throw new ArgumentException("Tabungan anda tidak mencukupi");
            }
            else
            {
                temp.keterangan = Keterangan;
                temp.Nominal = Nominal;
                _saldo = _saldo - temp.Nominal;
                temp.waktu = DateTime.Now;
                History.Add(temp);
            }
        }


        public void Lshistory(int inx)
        {
            Console.Write("{0:dd/mm/yy} │ {1,10:N} │ {2}", History[inx].waktu, History[inx].Nominal, History[inx].keterangan);
        }

        public int PnjHistory()
        {
            return History.Count;
        }
    }
}
