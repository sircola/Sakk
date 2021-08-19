using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Sakk
{
    public partial class Form1 : Form
    {
        public static int oszlopkatt;
        public static int sorkatt;
        public static String[,] sakktabla = new String[8, 8];
        public static int kattintas;
        public static int kattintasoszlop;
        public static int kattintasor;

        
        private void Kattintos()
        {

            if (kattintas == 0)
            {
                // először kattintott
                if (sakktabla[oszlopkatt - 1, sorkatt - 1].CompareTo("") != 0)
                {
                    ++kattintas;
                    kattintasoszlop = oszlopkatt;
                    kattintasor = sorkatt;

                    txtUzi.Text = $"kijelölve {sakktabla[oszlopkatt - 1, sorkatt - 1]} oszlop: {oszlopkatt} sor: {sorkatt} bábu: ";

                }
                else
                    txtUzi.Text = "bábura kellene.";

            }
            else
            {
                // txtUzi.Text = $"{sakktabla[kattintasoszlop - 1, kattintasor - 1]} kellena az oszlop: {oszlopkatt} sor: {sorkatt} bábu: ";

                bool lepett = false;

                if (sakktabla[kattintasoszlop - 1, kattintasor - 1].CompareTo("vgyalog") == 0)
                    lepett = GyalogEllenorzes(kattintasoszlop - 1, kattintasor - 1, oszlopkatt - 1, sorkatt - 1, 0);
                else
                if (sakktabla[kattintasoszlop - 1, kattintasor - 1].CompareTo("sgyalog") == 0)
                    lepett = GyalogEllenorzes(kattintasoszlop - 1, kattintasor - 1, oszlopkatt - 1, sorkatt - 1, 1);
                else
                if (sakktabla[kattintasoszlop - 1, kattintasor - 1].CompareTo("vbastya") == 0 || sakktabla[kattintasoszlop - 1, kattintasor - 1].CompareTo("sbastya") == 0)
                    lepett = BastyaEllenorzes(kattintasoszlop - 1, kattintasor - 1, oszlopkatt - 1, sorkatt - 1);

                if (lepett == true)
                {
                    sakktabla[oszlopkatt - 1, sorkatt - 1] = sakktabla[kattintasoszlop - 1, kattintasor - 1];
                    sakktabla[kattintasoszlop - 1, kattintasor - 1] = "";

                    kattintas = 0;
                    txtUzi.Text = "lépett.";
                }
                else {
                    txtUzi.Text = "oda nem tud lépni.";
                    kattintas = 0;
                }
            }
            
            
            
            TablaRajz();
        }


        public bool GyalogEllenorzes( int oszlopbol, int sorrol, int oszlopba, int sorba, int szin )
        {

            // lelép-e a tábláról
            if( oszlopba < 0 || oszlopba > 7 || sorba < 0 || sorba > 7 )
                return false;

            // TODO: ütni tudni kellene, most csak üres-e
            if (sakktabla[oszlopba, sorba].CompareTo("") != 0)
                return false;

            if ( szin == 0)
            {
                if (oszlopbol == oszlopba && sorba == sorrol + 1 && sorba < 9 )
                    return true;
            }
            else
            {
                if (oszlopbol == oszlopba && sorba == sorrol - 1 && sorba >= 0 )
                    return true;
            }

            return false;
        }

        public bool BastyaEllenorzes(int oszlopbol, int sorrol, int oszlopba, int sorba )
        {

            // lelép-e a tábláról
            if (oszlopba < 0 || oszlopba > 7 || sorba < 0 || sorba > 7)
                return false;

            if (oszlopbol != oszlopba && sorrol != sorba)
                return false;

            // van-e az utjában bábu?
            if( oszlopbol == oszlopba )
            {
                if (sorba > sorrol)
                {
                    for (int i = sorrol + 1; i <= sorba; i++)
                        if (sakktabla[oszlopba, i].CompareTo("") != 0)
                            return false;
                }
                else
                    for (int i = sorrol - 1; i >= sorba; i--)
                        if (sakktabla[oszlopba, i].CompareTo("") != 0)
                            return false;
            }
            else
            {
                if (oszlopbol > oszlopba)
                {
                    for (int i = oszlopbol + 1; i <= oszlopba; i++)
                        if (sakktabla[i, sorba].CompareTo("") != 0)
                            return false;
                }
                else
                    for (int i =oszlopbol - 1; i >= oszlopba; i--)
                        if (sakktabla[i, sorba].CompareTo("") != 0)
                            return false;
            }

            return true;
        }

        public Form1()
        {
            InitializeComponent();

            kattintas = 0;

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    sakktabla[i, j] = "";

            // oszlop, sor
            sakktabla[0, 0] = "vbastya";
            sakktabla[1, 0] = "vhuszar";
            sakktabla[2, 0] = "vfuto";
            sakktabla[3, 0] = "vvezer";
            sakktabla[4, 0] = "vkiraly";
            sakktabla[5, 0] = "vfuto";
            sakktabla[6, 0] = "vhuszar";
            sakktabla[7, 0] = "vbastya";

            sakktabla[0, 1] = "vgyalog";
            sakktabla[1, 1] = "vgyalog";
            sakktabla[2, 1] = "vgyalog";
            sakktabla[3, 1] = "vgyalog";
            sakktabla[4, 1] = "vgyalog";
            sakktabla[5, 1] = "vgyalog";
            sakktabla[6, 1] = "vgyalog";
            sakktabla[7, 1] = "vgyalog";

            sakktabla[0, 6] = "sgyalog";
            sakktabla[1, 6] = "sgyalog";
            sakktabla[2, 6] = "sgyalog";
            sakktabla[3, 6] = "sgyalog";
            sakktabla[4, 6] = "sgyalog";
            sakktabla[5, 6] = "sgyalog";
            sakktabla[6, 6] = "sgyalog";
            sakktabla[7, 6] = "sgyalog";

            sakktabla[0, 7] = "sbastya";
            sakktabla[1, 7] = "shuszar";
            sakktabla[2, 7] = "sfuto";
            sakktabla[3, 7] = "svezer";
            sakktabla[4, 7] = "skiraly";
            sakktabla[5, 7] = "sfuto";
            sakktabla[6, 7] = "shuszar";
            sakktabla[7, 7] = "sbastya";

            TablaRajz();
        }


        private void TablaRajz()
        {
            string picutvonal = "";
            string utvonal = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\Adatok\");

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    picutvonal = "";

                    if (sakktabla[i, j].CompareTo("vgyalog") == 0)
                        picutvonal = "vgyalog.gif";
                    else if (sakktabla[i, j].CompareTo("vbastya") == 0)
                        picutvonal = "vbastya.gif";
                    else if (sakktabla[i, j].CompareTo("vhuszar") == 0)
                        picutvonal = "vhuszar.gif";
                    else if (sakktabla[i, j].CompareTo("vfuto") == 0)
                        picutvonal = "vfuto.gif";
                    else if (sakktabla[i, j].CompareTo("vvezer") == 0)
                        picutvonal = "vvezer.gif";
                    else if (sakktabla[i, j].CompareTo("vkiraly") == 0)
                        picutvonal = "vkiraly.gif";
                    else if (sakktabla[i, j].CompareTo("sgyalog") == 0)
                        picutvonal = "sgyalog.gif";
                    else if (sakktabla[i, j].CompareTo("sbastya") == 0)
                        picutvonal = "sbastya.gif";
                    else if (sakktabla[i, j].CompareTo("shuszar") == 0)
                        picutvonal = "shuszar.gif";
                    else if (sakktabla[i, j].CompareTo("sfuto") == 0)
                        picutvonal = "sfuto.gif";
                    else if (sakktabla[i, j].CompareTo("svezer") == 0)
                        picutvonal = "svezer.gif";
                    else if (sakktabla[i, j].CompareTo("skiraly") == 0)
                        picutvonal = "skiraly.gif";
                    else if (sakktabla[i, j].CompareTo("") == 0)
                        picutvonal = "semmi.gif";



                    if ((i == 0) & (j == 0))
                        pictureBoxA1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 1))
                        pictureBoxA2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 2))
                        pictureBoxA3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 3))
                        pictureBoxA4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 4))
                        pictureBoxA5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 5))
                        pictureBoxA6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 6))
                        pictureBoxA7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 0) & (j == 7))
                        pictureBoxA8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 1) & (j == 0))
                        pictureBoxB1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 1))
                        pictureBoxB2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 2))
                        pictureBoxB3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 3))
                        pictureBoxB4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 4))
                        pictureBoxB5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 5))
                        pictureBoxB6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 6))
                        pictureBoxB7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 1) & (j == 7))
                        pictureBoxB8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 2) & (j == 0))
                        pictureBoxC1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 1))
                        pictureBoxC2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 2))
                        pictureBoxC3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 3))
                        pictureBoxC4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 4))
                        pictureBoxC5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 5))
                        pictureBoxC6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 6))
                        pictureBoxC7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 2) & (j == 7))
                        pictureBoxC8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 3) & (j == 0))
                        pictureBoxD1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 1))
                        pictureBoxD2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 2))
                        pictureBoxD3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 3))
                        pictureBoxD4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 4))
                        pictureBoxD5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 5))
                        pictureBoxD6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 6))
                        pictureBoxD7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 3) & (j == 7))
                        pictureBoxD8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 4) & (j == 0))
                        pictureBoxE1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 1))
                        pictureBoxE2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 2))
                        pictureBoxE3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 3))
                        pictureBoxE4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 4))
                        pictureBoxE5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 5))
                        pictureBoxE6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 6))
                        pictureBoxE7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 4) & (j == 7))
                        pictureBoxE8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 5) & (j == 0))
                        pictureBoxF1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 1))
                        pictureBoxF2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 2))
                        pictureBoxF3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 3))
                        pictureBoxF4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 4))
                        pictureBoxF5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 5))
                        pictureBoxF6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 6))
                        pictureBoxF7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 5) & (j == 7))
                        pictureBoxF8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 6) & (j == 0))
                        pictureBoxG1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 1))
                        pictureBoxG2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 2))
                        pictureBoxG3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 3))
                        pictureBoxG4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 4))
                        pictureBoxG5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 5))
                        pictureBoxG6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 6))
                        pictureBoxG7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 6) & (j == 7))
                        pictureBoxG8.Load(string.Concat(utvonal, picutvonal));

                    else if ((i == 7) & (j == 0))
                        pictureBoxH1.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 1))
                        pictureBoxH2.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 2))
                        pictureBoxH3.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 3))
                        pictureBoxH4.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 4))
                        pictureBoxH5.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 5))
                        pictureBoxH6.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 6))
                        pictureBoxH7.Load(string.Concat(utvonal, picutvonal));
                    else if ((i == 7) & (j == 7))
                        pictureBoxH8.Load(string.Concat(utvonal, picutvonal));
                }
            }
            
            Application.DoEvents();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kattintas = 0;
            txtUzi.Text = "okés új bábu.";
            TablaRajz();
        }

        private void pictureBoxA1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxB1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxC1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxD1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxE1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 1;
            Kattintos();
        }
        private void pictureBoxF1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxG1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxH1_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 1;
            Kattintos();
        }

        private void pictureBoxA2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxB2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxC2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxD2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxE2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 2;
            Kattintos();
        }
        private void pictureBoxF2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxG2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxH2_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 2;
            Kattintos();
        }

        private void pictureBoxA3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxB3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxC3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxD3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxE3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxF3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxG3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxH3_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 3;
            Kattintos();
        }

        private void pictureBoxA4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxB4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxC4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxD4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxE4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxF4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxG4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxH4_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 4;
            Kattintos();
        }

        private void pictureBoxA5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxB5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxC5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxD5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxE5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxF5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxG5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxH5_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 5;
            Kattintos();
        }

        private void pictureBoxA6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxB6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxC6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxD6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxE6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxF6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxG6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxH6_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 6;
            Kattintos();
        }

        private void pictureBoxA7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxB7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxC7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxD7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxE7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxF7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxG7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxH7_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 7;
            Kattintos();
        }

        private void pictureBoxA8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 1;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxB8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 2;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxC8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 3;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxD8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 4;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxE8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 5;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxF8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 6;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxG8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 7;
            sorkatt = 8;
            Kattintos();
        }

        private void pictureBoxH8_Click(object sender, EventArgs e)
        {
            oszlopkatt = 8;
            sorkatt = 8;
            Kattintos();
        }

    }
}
