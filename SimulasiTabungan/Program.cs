using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimulasiTabungan
{
    class Program
    {
        
        static List<Pengguna> LsPengguna;
        static List<Tabungan> LsTabungan;

        static void Main(string[] args)
        {
            LsPengguna = new List<Pengguna>();
            LsTabungan = new List<Tabungan>();

            char jwb;
        //menu depan
        menuDepan:
            loading();
            header();




            TextBox(1, 7, 37, 8, "Menu Halaman Depan");
            Console.SetCursorPosition(3, 9);
            Console.Write("1. Login");
            Console.SetCursorPosition(3, 10);
            Console.Write("2. Daftar");
            Console.SetCursorPosition(3, 11);
            Console.Write("3. Keluar");
            Console.SetCursorPosition(3, 13);

            Console.Write("Pilihan Anda ? ");
            jwb = inputJwb(new char[3] { '1', '2', '3' });

            switch (jwb)
            {
                case '1':
                    if (LsPengguna.Count == 0)
                    {
                        Peringatan("Belum ada user yang terdaftar");
                        goto menuDepan;
                    }

                    int inx;
                    try { inx = Login(); }
                    catch
                    {
                        Peringatan("Gagal Login");
                        goto menuDepan;
                    }


                    MenuTransaksi(inx);
                    goto menuDepan;

                case '2':
                    LsPengguna.Add(Daftar());
                    LsTabungan.Add(new Tabungan(LsPengguna[LsPengguna.Count - 1].IdPengguna));
                    goto menuDepan;

            }

        }

        static void MenuTransaksi(int inx)
        {
            char jwb;

            //menu depan
        menu:
            header();
            TextBox(41, 7, 37, 8, "Profil");
            Console.SetCursorPosition(43, 9);
            Console.Write("Nama   : " + LsPengguna[inx].Nama);
            Console.SetCursorPosition(43, 11);
            Console.Write("Alamat : " + LsPengguna[inx].Alamat);
            Console.SetCursorPosition(43, 13);
            Console.Write("Saldo  : Rp. {0:N}", LsTabungan[inx].saldo);

            TextBox(41, 17, 37, 3 + LsTabungan[inx].PnjHistory(), "History");
            if (LsTabungan[inx].PnjHistory() != 0)
            {

                for (int i = LsTabungan[inx].PnjHistory() - 1, j = 0; i >= 0; i--, j++)
                {
                    Console.SetCursorPosition(43, 19 + j);
                    LsTabungan[inx].Lshistory(i);
                }

            }

            TextBox(1, 7, 37, 8, "Menu Tabungan");
            Console.SetCursorPosition(3, 9);
            Console.Write("1. Simapan Uang");
            Console.SetCursorPosition(3, 10);
            Console.Write("2. Ambil Uang");
            Console.SetCursorPosition(3, 11);
            Console.Write("3. Transfer Uang");
            Console.SetCursorPosition(3, 12);
            Console.Write("4. Logout");
            Console.SetCursorPosition(3, 13);


            Console.Write("Pilihan Anda ? ");
            jwb = inputJwb(new char[4] { '1', '2', '3', '4' });


            int Nominal;
            switch (jwb)
            {
                case '1':
                    TextBox(1, 17, 37, 4, "Simpan Uang");
                    Console.SetCursorPosition(3, 19);
                    Console.Write("Nominal : ");
                    try
                    {

                        Nominal = Convert.ToInt32(Console.ReadLine());
                        if (Nominal <= 0)
                        {
                            throw new ArgumentException("Nominal Harus lebih besar dari 0");
                        }
                    }
                    catch (Exception exp)
                    {
                        Peringatan("Periksa Inputan " + exp.Message);
                        goto case '1';
                    }
                    LsTabungan[inx].Simpan(Nominal, "Tabung");

                    goto menu;
                case '2':
                    TextBox(1, 17, 37, 4, "Ambil Uang");
                    Console.SetCursorPosition(3, 19);
                    Console.Write("Nominal : ");
                    try
                    {

                        Nominal = Convert.ToInt32(Console.ReadLine());
                        if (Nominal <= 0)
                        {
                            throw new ArgumentException("Nominal Harus lebih besar dari 0");
                        }
                    }
                    catch (Exception exp)
                    {
                        Peringatan("Periksa Inputan " + exp.Message);
                        goto case '2';
                    }

                    try
                    {
                        LsTabungan[inx].Ambil(Nominal, "Ambil");
                    }
                    catch (Exception exp)
                    {
                        Peringatan(exp.Message);
                    }
                    goto menu;

                case '3':
                    TextBox(1, 17, 37, 4, "Transfer Uang");
                    Console.SetCursorPosition(3, 19);
                    Console.Write("Nominal : ");
                    try
                    {

                        Nominal = Convert.ToInt32(Console.ReadLine());
                        if (Nominal <= 0)
                        {
                            throw new ArgumentException("Nominal Harus lebih besar dari 0");
                        }
                    }
                    catch (Exception exp)
                    {
                        Peringatan("Periksa Inputan " + exp.Message);
                        goto case '3';
                    }

                inputUntuk:
                    Console.SetCursorPosition(3, 20);
                    Console.Write("Untuk Id Pengguna : ");

                    string untuk;
                    int inxT;
                    try
                    {
                        untuk = Console.ReadLine();
                        inxT = cariID(untuk);
                    }
                    catch
                    {
                        Peringatan("Periksa nama user Penerima");
                        goto inputUntuk;
                    }


                    try
                    {
                        LsTabungan[inx].Ambil(Nominal, "untuk " + untuk);
                        LsTabungan[inxT].Simpan(Nominal, "dari " + LsTabungan[inx].IdPengguna);
                    }
                    catch (Exception exp)
                    {
                        Peringatan(exp.Message);
                    }
                    goto menu;

            }

        }


        static int Login()
        {
            int idx;
            int kali = 3;
        inputId:
            kali--;
            if (kali == 0)
            {
                throw new ArgumentException();
            }

            TextBox(41, 7, 37, 8, "Login");
            Console.SetCursorPosition(43, 9);
            Console.Write("User Id : ");
            string IdPengguna = Console.ReadLine();
            try
            {
                idx = cariID(IdPengguna);
            }
            catch
            {
                Peringatan("Periksa User Name");
                goto inputId;
            }

            string Pass;
            kali = 5;

            do
            {
                Console.SetCursorPosition(43, 10);
                Console.Write("Password : ");
                Pass = Console.ReadLine();
                if (!LsPengguna[idx].ValPass(Pass))
                {
                    Peringatan("Password Salah");
                    kali--;
                }
            }
            while (!LsPengguna[idx].ValPass(Pass) && kali != 0);

            if (kali == 0)
            {
                throw new ArgumentException();
            }

            return idx;
        }

        static Pengguna Daftar()
        {
            Pengguna pengguna = new Pengguna();
        inputId:
            TextBox(41, 7, 37, 8, "Login");
            Console.SetCursorPosition(43, 9);
            Console.Write("User Id   : ");

            try
            {
                pengguna.IdPengguna = Console.ReadLine();
            }
            catch (Exception exp)
            {
                Peringatan(exp.Message);
                goto inputId;
            }

            try
            {
                cariID(pengguna.IdPengguna);
                Peringatan("User id ini telah digunakan");
                goto inputId;
            }
            catch
            {
                //
            }

            Console.SetCursorPosition(43, 10);
            Console.Write("Password  : ");
            pengguna.pass = Console.ReadLine();

        inputNama:
            Console.SetCursorPosition(43, 11);
            Console.Write("Nama      : ");
            try
            {
                pengguna.Nama = Console.ReadLine();
            }
            catch (Exception exp)
            {
                Peringatan(exp.Message);
                goto inputNama;
            }

        inputAlamat:
            Console.SetCursorPosition(43, 12);
            Console.Write("Alamat    : ");
            try
            {
                pengguna.Alamat = Console.ReadLine();
            }
            catch (Exception exp)
            {
                Peringatan(exp.Message);
                goto inputAlamat;
            }
            return pengguna;
        }

        static int cariID(string IdPengguna)
        {

            if (LsPengguna.Count != 0)
            {
                int i = 0;
                while (i < LsPengguna.Count && LsPengguna[i].IdPengguna != IdPengguna)
                    i++;

                if (IdPengguna != LsPengguna[i].IdPengguna)
                    throw new Exception("IdUser ini tidak di temukan");

                return i;
            }
            else
            {
                throw new Exception("Belum ada member");

            }
        }

        //layout
        static Boolean CariJawab(char Cari, char[] Jawab)
        {
            Boolean ketemu = false;
            int i = 0;
            while ((i != Jawab.Length) && (!ketemu))
            {
                ketemu = (Cari == Jawab[i]);
                i++;
            }
            return ketemu;
        }

        static char inputJwb(char[] Jawab)
        {
            char jwb;
            do
            {
                jwb = Console.ReadKey(true).KeyChar;
            } while (!CariJawab(jwb, Jawab));
            Console.Write("{0}", jwb);
            return jwb;
        }

        static void TextBox(int kiri, int atas, int Lebar, int Panjang, String judul)
        {
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(kiri, atas);
            Console.Write("┌");
            for (int i = 1; i < Lebar; i++)
                Console.Write("─");
            Console.Write("╖");
            for (int i = 1; i < Panjang; i++)
            {
                Console.SetCursorPosition(kiri, atas + i);
                Console.Write("│");
                for (int j = 1; j < Lebar; j++)
                    Console.Write(" ");
                Console.Write("║");
            }
            Console.SetCursorPosition(kiri, atas + Panjang);
            Console.Write("╘");
            for (int i = 1; i < Lebar; i++)
                Console.Write("═");
            Console.Write("╝");
            Console.SetCursorPosition(kiri + 3, atas);
            Console.Write("{0}", judul);
        }

        static void loading()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.SetCursorPosition(28, 4);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("S E L A M A T  D A T A N G");
            Console.ForegroundColor = ConsoleColor.Black;

            Console.SetCursorPosition(26, 16);
            Console.Write("Restu Suhendar      │ 10110014");
            Console.SetCursorPosition(26, 17);
            Console.Write("Yopi Iswandi Dwi P. │ 10110011");
            Console.SetCursorPosition(26, 18);
            Console.Write("Rhendy Febrian      │ 10110016");
            Console.SetCursorPosition(26, 19);
            Console.Write("Hans Bryan Setra    │ 10110022");
            Console.SetCursorPosition(26, 20);
            Console.Write("Arie Prima Anggara  │ 10110038");
            Console.SetCursorPosition(23, 21);
            Console.Write("────────────────────────────────────");

            Console.SetCursorPosition(23, 23);
            Console.Write("────────────────────────────────────");
            Console.SetCursorPosition(23, 22);
            for (int i = 0; i < 36; i++)
            {
                System.Threading.Thread.Sleep(100);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(" ");
            }



        }
        static void header()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            TextBox(1, 1, 77, 2, "");
            Console.SetCursorPosition(27, 2);
            Console.Write("S I M U L A S I  T A B U N G A N");
        }

        static void Peringatan(string pesan)
        {

            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(1, 5);
            Console.Write(pesan);
            Console.ReadKey();
            Console.SetCursorPosition(1, 5);
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 0; i < 80; i++)
                Console.Write(" ");
            Console.BackgroundColor = ConsoleColor.DarkRed;
        }
    }
}
