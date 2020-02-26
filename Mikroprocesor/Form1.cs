using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mikroprocesor
{
    public partial class Form1 : Form
    {
        string statusWykonania = " >> wykonano <<";
        bool czekaj_na_messageBox = false;
        bool przyklady_auto = false;

        Stos stos = new Stos();
        public Form1()
        {
            InitializeComponent();
            ax = new rejestr();
            bx = new rejestr();
            cx = new rejestr();
            dx = new rejestr();
            stala = new rejestr();

            ahButtons = new Button[8]; alButtons = new Button[8];
            bhButtons = new Button[8]; blButtons = new Button[8];
            chButtons = new Button[8]; clButtons = new Button[8];
            dhButtons = new Button[8]; dlButtons = new Button[8];

            ahButtons[0] = ah0; ahButtons[1] = ah1; ahButtons[2] = ah2; ahButtons[3] = ah3;
            ahButtons[4] = ah4; ahButtons[5] = ah5; ahButtons[6] = ah6; ahButtons[7] = ah7;

            alButtons[0] = al0; alButtons[1] = al1; alButtons[2] = al2; alButtons[3] = al3;
            alButtons[4] = al4; alButtons[5] = al5; alButtons[6] = al6; alButtons[7] = al7;

            bhButtons[0] = bh0; bhButtons[1] = bh1; bhButtons[2] = bh2; bhButtons[3] = bh3;
            bhButtons[4] = bh4; bhButtons[5] = bh5; bhButtons[6] = bh6; bhButtons[7] = bh7;

            blButtons[0] = bl0; blButtons[1] = bl1; blButtons[2] = bl2; blButtons[3] = bl3;
            blButtons[4] = bl4; blButtons[5] = bl5; blButtons[6] = bl6; blButtons[7] = bl7;

            chButtons[0] = ch0; chButtons[1] = ch1; chButtons[2] = ch2; chButtons[3] = ch3;
            chButtons[4] = ch4; chButtons[5] = ch5; chButtons[6] = ch6; chButtons[7] = ch7;

            clButtons[0] = cl0; clButtons[1] = cl1; clButtons[2] = cl2; clButtons[3] = cl3;
            clButtons[4] = cl4; clButtons[5] = cl5; clButtons[6] = cl6; clButtons[7] = cl7;

            dhButtons[0] = dh0; dhButtons[1] = dh1; dhButtons[2] = dh2; dhButtons[3] = dh3;
            dhButtons[4] = dh4; dhButtons[5] = dh5; dhButtons[6] = dh6; dhButtons[7] = dh7;

            dlButtons[0] = dl0; dlButtons[1] = dl1; dlButtons[2] = dl2; dlButtons[3] = dl3;
            dlButtons[4] = dl4; dlButtons[5] = dl5; dlButtons[6] = dl6; dlButtons[7] = dl7;

            ax.tabH = ahButtons; ax.tabL = alButtons;
            bx.tabH = bhButtons; bx.tabL = blButtons;
            cx.tabH = chButtons; cx.tabL = clButtons;
            dx.tabH = dhButtons; dx.tabL = dlButtons;

            ax.hLabel = AH_label; ax.lLabel = AL_label;
            bx.hLabel = BH_label; bx.lLabel = BL_label;
            cx.hLabel = CH_label; cx.lLabel = CL_label;
            dx.hLabel = DH_label; dx.lLabel = DL_label;

            ax.hLabelHex = AH_labelHex; ax.lLabelHex = AL_labelHex;
            bx.hLabelHex = BH_labelHex; bx.lLabelHex = BL_labelHex;
            cx.hLabelHex = CH_labelHex; cx.lLabelHex = CL_labelHex;
            dx.hLabelHex = DH_labelHex; dx.lLabelHex = DL_labelHex;

        }

        //zamiana 0->1 i odwrotnie
        private void clickBit(object sender, EventArgs e)
        {
            Button bitButton = (Button)sender;
            if (bitButton.Text == "0")
                bitButton.Text = "1";
            else
                bitButton.Text = "0";
        }

        //obliczanie wartosci dziesietnej rejestru (ah, al, bh, bl..)
        private void ahClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (ahButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            ax.h = wartosc;
            ustawBityILabelki(ax, false);
        }
        private void alClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (alButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            ax.l = wartosc;
            ax.lLabel.Text = ax.l.ToString();
            ustawBityILabelki(ax, true);
        }
        private void bhClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (bhButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            bx.h = wartosc;
            ustawBityILabelki(bx, false);
        }
        private void blClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (blButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            bx.l = wartosc;
            ustawBityILabelki(bx, true);
        }
        private void chClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (chButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            cx.h = wartosc;
            ustawBityILabelki(cx, false);
        }
        private void clClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (clButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            cx.l = wartosc;
            ustawBityILabelki(cx, true);
        }
        private void dhClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (dhButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            dx.h = wartosc;
            ustawBityILabelki(dx, false);
        }
        private void dlClick(object sender, EventArgs e)
        {
            clickBit(sender, e);

            int wartosc = 0;

            for (int i = 0; i < 8; i++)
            {
                if (dlButtons[i].Text == "1")
                    wartosc = wartosc + (int)Math.Pow(2, i);
            }
            dx.l = wartosc;
            ustawBityILabelki(dx, true);
        }

        //reset wszystkiego
        private void resetButtonClick(object sender, EventArgs e)
        {
            //czyszczenie listy rozkazow
            lista_textbox.Clear();
            liczbaRozkazowDoWykonania = 0;
            biezacyRozkaz = 0;

            //czyszczenie buttonow
            for (int i = 0; i < 8; i++)
            {
                ahButtons[i].Text = "0"; alButtons[i].Text = "0";
                bhButtons[i].Text = "0"; blButtons[i].Text = "0";
                chButtons[i].Text = "0"; clButtons[i].Text = "0";
                dhButtons[i].Text = "0"; dlButtons[i].Text = "0";
            }

            AH_label.Text = "0"; AL_label.Text = "0";
            BH_label.Text = "0"; BL_label.Text = "0";
            CH_label.Text = "0"; CL_label.Text = "0";
            DH_label.Text = "0"; DL_label.Text = "0";

            AH_labelHex.Text = "0h"; AL_labelHex.Text = "0h";
            BH_labelHex.Text = "0h"; BL_labelHex.Text = "0h";
            CH_labelHex.Text = "0h"; CL_labelHex.Text = "0h";
            DH_labelHex.Text = "0h"; DL_labelHex.Text = "0h";

            ax.h = 0; ax.l = 0;
            bx.h = 0; bx.l = 0;
            cx.h = 0; cx.l = 0;
            dx.h = 0; dx.l = 0;
            stala.l = 0;

            numericUpDown_stala.Value = 0;

            stos.Clear();
            stos_textbox.Clear();
        }

        private void exitButtonClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //dodanie rozkazu do listy
        private void dodaj_button_Click(object sender, EventArgs e)
        {
            string rejestr1 = "";
            string rozkaz = "";
            string rejestr2 = "";

            if (r1_AH.Checked) rejestr1 = "AH";
            if (r1_AL.Checked) rejestr1 = "AL";
            if (r1_BH.Checked) rejestr1 = "BH";
            if (r1_BL.Checked) rejestr1 = "BL";
            if (r1_CH.Checked) rejestr1 = "CH";
            if (r1_CL.Checked) rejestr1 = "CL";
            if (r1_DH.Checked) rejestr1 = "DH";
            if (r1_DL.Checked) rejestr1 = "DL";

            if (ADD.Checked) rozkaz = "ADD";
            if (MOV.Checked) rozkaz = "MOV";
            if (SUB.Checked) rozkaz = "SUB";
            if (INT_05.Checked) rozkaz = "INT 05";
            if (INT_1A.Checked) rozkaz = "INT 1A";
            if (INT_10.Checked) rozkaz = "INT 10";
            if (INT_11.Checked) rozkaz = "INT 11";
            if (INT_13.Checked) rozkaz = "INT 13";
            if (INT_14.Checked) rozkaz = "INT 14";
            if (INT_15.Checked) rozkaz = "INT 15";
            if (INT_16.Checked) rozkaz = "INT 16";
            if (INT_17.Checked) rozkaz = "INT 17";
            if (INT_19.Checked) rozkaz = "INT 19";
            
            if (PUSH.Checked) rozkaz = "PUSH";
            if (POP.Checked) rozkaz = "POP";

            if (r2_AH.Checked) rejestr2 = "AH";
            if (r2_AL.Checked) rejestr2 = "AL";
            if (r2_BH.Checked) rejestr2 = "BH";
            if (r2_BL.Checked) rejestr2 = "BL";
            if (r2_CH.Checked) rejestr2 = "CH";
            if (r2_CL.Checked) rejestr2 = "CL";
            if (r2_DH.Checked) rejestr2 = "DH";
            if (r2_DL.Checked) rejestr2 = "DL";

            if (checkBox_stala.Checked)
            {
                try
                {
                    stala.l = Convert.ToInt32(numericUpDown_stala.Value);
                }
                //jesli nie uda sie konwersja
                catch
                {
                    czekaj_na_messageBox = true;
                    MessageBox.Show("Zła wartość w polu 'Stała'. Dozwolone wartości: 0-255");
                    czekaj_na_messageBox = false;
                    stala.l = -1;
                    numericUpDown_stala.Value = 0;
                }
                //jesli wpiszemy za duza lub za mala liczbe
                if (stala.l >= 0 && stala.l <= 255)
                {
                    string wartoscHex = Convert.ToString(stala.l, 16);
                    if (wartoscHex.Length == 1)
                        wartoscHex = "0" + wartoscHex;
                    lista_textbox.AppendText(rozkaz + " " + rejestr1 + ", " + wartoscHex + "h" + Environment.NewLine);
                }

                else if (stala.l != -1)
                {
                    czekaj_na_messageBox = true;
                    MessageBox.Show("Zła wartość w polu 'Stała'. Dozwolone wartości: 0-255");
                    czekaj_na_messageBox = false;
                    stala.l = 0;
                    numericUpDown_stala.Value = 0;
                }
            }
            else
            {
                if (rozkaz == "MOV" || rozkaz == "ADD" || rozkaz == "SUB")
                    lista_textbox.AppendText(rozkaz + " " + rejestr1 + ", " + rejestr2 + Environment.NewLine);
                else
                if (rozkaz == "POP" || rozkaz == "PUSH")
                    lista_textbox.AppendText(rozkaz + " " + rejestr1 + Environment.NewLine);
                else //dla rozkazow przerwan INT
                lista_textbox.AppendText(rozkaz + Environment.NewLine);
            }
            liczbaRozkazowDoWykonania++;
        }

        private void wyczysc_button_Click(object sender, EventArgs e)
        {
            lista_textbox.Clear();
            liczbaRozkazowDoWykonania = 0;
            biezacyRozkaz = 0;
            stos.Clear();
            stos_textbox.Clear();
        }

        //wykonanie programu w calosci, start timera
        private void calosc_button_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (czekaj_na_messageBox == false)
            {
                if (biezacyRozkaz < liczbaRozkazowDoWykonania)
                    krokowa_button_Click(sender, e);
                else
                    timer1.Stop();
            }
        }

        //ustawienie nowych bitow w buttonach, po wykonaniu rozkazu
        private void ustawBityILabelki(rejestr rej, bool low1)
        {
            string wartoscBinarna = "";
            int dlugosc;

            if (low1 == true)
            {
                wartoscBinarna = Convert.ToString(rej.l, 2);
                dlugosc = 8 - wartoscBinarna.Length;
                //dopelniamy zerami jesli string jest za krotki (mniejszy od 8 znakow)
                for (int i = 0; i < dlugosc; i++)
                {
                    wartoscBinarna = "0" + wartoscBinarna;
                }
                //przepisujemy do buttona
                for (int i = 0; i < 8; i++)
                {
                    rej.tabL[7 - i].Text = wartoscBinarna.Substring(i, 1);
                }
                rej.lLabel.Text = rej.l.ToString();
                rej.lLabelHex.Text = Convert.ToString(rej.l, 16) + "h";
            }
            else
            {
                wartoscBinarna = Convert.ToString(rej.h, 2);
                dlugosc = 8 - wartoscBinarna.Length;
                //dopelniamy zerami jesli string jest za krotki (mniejszy od 8 znakow)
                for (int i = 0; i < dlugosc; i++)
                {
                    wartoscBinarna = "0" + wartoscBinarna;
                }
                //przepisujemy do buttona
                for (int i = 0; i < 8; i++)
                {
                    rej.tabH[7 - i].Text = wartoscBinarna.Substring(i, 1);
                }
                rej.hLabel.Text = rej.h.ToString();
                rej.hLabelHex.Text = Convert.ToString(rej.h, 16) + "h";
            }
            textBox_SP.Text = stos.Get_SP();
        }
        private void wykonajRozkaz(string rozkaz, ref rejestr rej1, bool low1, ref rejestr rej2lubStala, bool low2)
        {
            string zawartosc_stosu = ""; // zmienna uzyta w petli odczytu stosu
            switch (rozkaz)
            {
                case "MOV":
                    if (low1 == true)
                    {
                        if (low2 == true)
                            rej1.l = rej2lubStala.l;
                        else
                            rej1.l = rej2lubStala.h;
                    }
                    else
                    {
                        if (low2 == true)
                            rej1.h = rej2lubStala.l;
                        else
                            rej1.h = rej2lubStala.h;
                    }
                    ustawBityILabelki(rej1, low1);
                    statusWykonania = " >> wykonano <<";
                    break;
                case "ADD":
                    if (low1 == true)
                    {
                        if (low2 == true)
                            rej1.l += rej2lubStala.l;
                        else
                            rej1.l += rej2lubStala.h;
                        if (rej1.l > 255) rej1.l = rej1.l - 255;
                    }
                    else
                    {
                        if (low2 == true)
                            rej1.h += rej2lubStala.l;
                        else
                            rej1.h += rej2lubStala.h;
                        if (rej1.h > 255) rej1.h = rej1.h - 255;
                    }
                    ustawBityILabelki(rej1, low1);
                    statusWykonania = " >> wykonano <<";
                    break;
                case "SUB":
                    if (low1 == true)
                    {
                        if (low2 == true)
                            rej1.l -= rej2lubStala.l;
                        else
                            rej1.l -= rej2lubStala.h;
                        if (rej1.l < 0) rej1.l = -rej1.l;
                    }
                    else
                    {
                        if (low2 == true)
                            rej1.h -= rej2lubStala.l;
                        else
                            rej1.h -= rej2lubStala.h;
                        if (rej1.h < 0) rej1.h = -rej1.h;
                    }
                    ustawBityILabelki(rej1, low1);
                    statusWykonania = " >> wykonano <<";
                    break;
                case "INT05":
                    czekaj_na_messageBox = true;
                    MessageBox.Show("Wciśnięto klawisz: Print-screen", "INT 05");
                    czekaj_na_messageBox = false;
                    break;
                case "INT16":
                    if (przyklady_auto == true)
                    {
                        ax.h = 0x10; // funkcja 10h
                        ustawBityILabelki(ax, false);
                    }
                    if (ax.h == 16)
                    {
                        int kodZnaku;
                        Random r = new Random();
                        kodZnaku = r.Next(97, 122);
                        ax.l = kodZnaku;
                        ax.h = 28;  //scancode = 1C (key pressed)
                        ustawBityILabelki(ax, true);
                        ustawBityILabelki(ax, false);
                        char c = Convert.ToChar(ax.l);
                        czekaj_na_messageBox = true;
                        MessageBox.Show("Wciśnięto klawisz: " + c + " ASCII: " + 
                            Convert.ToString(ax.l, 16) + " Scan code: 1C", "INT 16");
                        czekaj_na_messageBox = false;
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja: " 
                            + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT17":
                    if (przyklady_auto == true)
                    {
                        ax.h = 0; // funkcja 00h
                        ax.l = 0x61;   // ASCII code of character to be printed: 'a'
                        dx.h = 0;
                        dx.l = 1;  // DX (DH + DL) = printer number(i.e. 0 = LPT1, 1 = LPT2, 2 = LPT3)
                        ustawBityILabelki(ax, false);
                        ustawBityILabelki(ax, true);
                        ustawBityILabelki(dx, false);
                        ustawBityILabelki(dx, true);
                    }
                    if (ax.h == 0 && dx.h == 0 && (dx.l == 0 || dx.l == 1 || dx.l == 2))
                    {
                        char c = Convert.ToChar(ax.l);
                        czekaj_na_messageBox = true;
                        MessageBox.Show("Na drukarce LPT" + (dx.l + 1).ToString() + 
                            " wydrukowano znak: " + c, "INT 17");
                        ax.h = 64;
                        ustawBityILabelki(ax, false);
                        rej1.hLabel.Text = "64";
                        czekaj_na_messageBox = false;
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja lub błędne parametry: " + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT19":
                    czekaj_na_messageBox = true;
                    MessageBox.Show("system reboot", "INT 19");
                    czekaj_na_messageBox = false;
                    break;
                case "INT1A":
                    if (przyklady_auto == true)
                    {
                        ax.h = 3;    // funkcja 03h
                        cx.h = 0x16; // hour
                        cx.l = 0x49; // minutes
                        dx.h = 0x30; // seconds
                        dx.l = 1;    // daylight time
                        ustawBityILabelki(ax, false);
                        ustawBityILabelki(cx, false);
                        ustawBityILabelki(cx, true);
                        ustawBityILabelki(dx, false);
                        ustawBityILabelki(dx, true);
                    }
                    if (ax.h == 3 &&
                        (Enumerable.Range(0x00, 9).Contains(cx.h) ||  // cx.h , godzina, 00-23
                        Enumerable.Range(0x10, 9).Contains(cx.h) ||
                        Enumerable.Range(0x20, 9).Contains(cx.h)) &&
                        (Enumerable.Range(0x00, 9).Contains(cx.l) ||  // cx.l, minuty, 00-59
                        Enumerable.Range(0x10, 9).Contains(cx.l) ||
                        Enumerable.Range(0x20, 9).Contains(cx.l) ||
                        Enumerable.Range(0x30, 9).Contains(cx.l) ||
                        Enumerable.Range(0x40, 9).Contains(cx.l) ||
                        Enumerable.Range(0x50, 9).Contains(cx.l)) &&
                        (Enumerable.Range(0x00, 9).Contains(dx.h) ||  // dx.h, sekundy, 00-59
                        Enumerable.Range(0x10, 9).Contains(dx.h) ||
                        Enumerable.Range(0x20, 9).Contains(dx.h) ||
                        Enumerable.Range(0x30, 9).Contains(dx.h) ||
                        Enumerable.Range(0x40, 9).Contains(dx.h) ||
                        Enumerable.Range(0x50, 9).Contains(dx.h)) &&
                        (dx.l == 0 || dx.l == 1)) // dx.l : 00h standard time, 01h daylight time
                    {
                        string godzina = Convert.ToString(cx.h, 16);
                        string minuta = Convert.ToString(cx.l, 16);
                        string sekunda = Convert.ToString(dx.h, 16);
                        string letniLubZimowy = "";
                        if (dx.l == 0)
                            letniLubZimowy = "czas zimowy";
                        else
                            letniLubZimowy = "czas letni";
                        czekaj_na_messageBox = true;
                        MessageBox.Show("Ustawiono czas w zegarze czasu rzeczywistego: " 
                            + godzina + ":" + minuta + ":" + sekunda + " " + letniLubZimowy, "INT 1A");
                        czekaj_na_messageBox = false;
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja lub błędne parametry: " 
                            + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT10":
                    if (przyklady_auto == true)
                    {
                        ax.h = 0;
                        ustawBityILabelki(ax, false);
                        ax.l = 8;
                        ustawBityILabelki(ax, true);
                    }
                    if (ax.h == 0)
                    {
                        czekaj_na_messageBox = true;
                        switch (ax.l)
                        {
                            case 0:
                                MessageBox.Show("Ustawiono tryb video: 40x25 Black & White", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 2:
                                MessageBox.Show("Ustawiono tryb video: 40x25 Color", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 4:
                                MessageBox.Show("Ustawiono tryb video: 80x25 Black & White", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 8:
                                MessageBox.Show("Ustawiono tryb video: 80x25 Color", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 16:
                                MessageBox.Show("Ustawiono tryb video: 320x200 Color", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 32:
                                MessageBox.Show("Ustawiono tryb video: 320x200 Black & White", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 64:
                                MessageBox.Show("Ustawiono tryb video: 640x200 Black & White", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            case 128:
                                MessageBox.Show("Ustawiono tryb video: Monochrome only", "INT 10");
                                statusWykonania = " >> wykonano <<";
                                break;
                            default:
                                statusWykonania = " >> nieobsługiwana wartość AL: " + Convert.ToString(ax.l, 16) + "H <<";
                                break;
                        }
                        czekaj_na_messageBox = false;
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja: " + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT11":
                    ax.l = 3;     //00000110
                    ax.h = 2;     //00000010
                    ustawBityILabelki(ax, true);
                    ustawBityILabelki(ax, false);
                    czekaj_na_messageBox = true;
                    MessageBox.Show("System Information zwrócone w rejestrze AH, AL", "INT 11");
                    czekaj_na_messageBox = false;
                    statusWykonania = " >> wykonano <<";
                    break;
                case "INT13":
                    if (przyklady_auto == true)
                    {
                        ax.h = 1;
                        ustawBityILabelki(ax, false);
                        dx.l = 128;
                        ustawBityILabelki(dx, true);
                    }
                    if (ax.h == 1)
                    {
                        ax.h = 7;
                        ustawBityILabelki(ax, false);
                        czekaj_na_messageBox = true;
                        MessageBox.Show("Drive initialization error", "INT 13");
                        czekaj_na_messageBox = false;
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja: " + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT14":
                    if (przyklady_auto==true)
                    {
                        ax.h = 1;
                        int kodZnaku;
                        Random r = new Random();
                        kodZnaku = r.Next(97, 122);
                        ax.l = kodZnaku;
                        dx.l = 0;   //serial port nr 0 (pierwszy)
                        ustawBityILabelki(ax, true);
                        ustawBityILabelki(ax, false);
                        ustawBityILabelki(dx, true);
                    }
                    if (ax.h == 1)
                    {
                        ax.h = 1; //data ready
                        ustawBityILabelki(ax, false);
                        czekaj_na_messageBox = true;
                        char c = Convert.ToChar(ax.l);
                        MessageBox.Show("Send character: " + c, "INT 14");
                        czekaj_na_messageBox = false; 
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja: " + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "INT15":
                    if (przyklady_auto==true)
                    {
                        ax.h = 0x86;
                        cx.h = 0;
                        cx.l = 0;
                        dx.h = 1;
                        dx.l = 0x2C;         //h + l = 1 0010 1100 czyli 300ms
                        ustawBityILabelki(ax, false);
                        ustawBityILabelki(cx, false);
                        ustawBityILabelki(cx, true);
                        ustawBityILabelki(dx, false);
                        ustawBityILabelki(dx, true);
                    }
                    if (ax.h == 0x86)
                    {
                        string tempHex = "";
                        long tempDec = 0;
                        tempHex = "0x" + Convert.ToString(cx.h, 16) + Convert.ToString(cx.l, 16) + Convert.ToString(dx.h, 16) + Convert.ToString(dx.l, 16);
                        tempDec = Convert.ToInt64(tempHex, 16);
                        czekaj_na_messageBox = true;
                        MessageBox.Show("Wait: " + tempDec + " microseconds", "INT 15");
                        czekaj_na_messageBox = false;
                        statusWykonania = " >> wykonano <<";
                    }
                    else
                        statusWykonania = " >> nieobsługiwana funkcja: " + Convert.ToString(ax.h, 16) + "H <<";
                    break;
                case "PUSH":
                    if (low1 == true)
                    {
                        stos.Push(rej1.l); // wrzucamy rej1.l na stos
                    }
                    else
                    {
                        stos.Push(rej1.h); // wrzucamy rej1.h na stos
                    }

                    // wyswietlamy stos w text boxie - uzywając własnej implementacji stosu
                    zawartosc_stosu = stos.Wyswietl_stos();
                    stos_textbox.Text = zawartosc_stosu;
                    ustawBityILabelki(rej1, low1);
                    statusWykonania = " >> wykonano <<";
                    break;
                case "POP":
                    if (low1 == true)
                    {
                        rej1.l = (int) stos.Pop(); // pobieramy wartosc ze stosu do rej1.l
                    }
                    else
                    {
                        rej1.h = (int) stos.Pop(); // pobieramy wartosc ze stosu do rej1.h
                    }
                    ustawBityILabelki(rej1, low1);

                    // wyswietlamy stos w text boxie - uzywając własnej implementacji stosu
                    zawartosc_stosu = stos.Wyswietl_stos();
                    stos_textbox.Text = zawartosc_stosu;

                    statusWykonania = " >> wykonano <<";
                    break;
            }
        }
        private void krokowa_button_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"rozkazy.txt", lista_textbox.Text);  //zapis rozkazow do pliku
            string[] rozkazy = File.ReadAllLines(@"rozkazy.txt");   //czytamy w formie pojedynczych linii

            string rejestr1 = "";
            string rozkaz = "";
            string rejestr2lubStala = "";
            ref rejestr rej1 = ref ax;
            ref rejestr rej2lubStala = ref bx;
            bool low1 = false, low2 = false;   //rejestr np. al, bl

            if (biezacyRozkaz < liczbaRozkazowDoWykonania)
            {
                rozkaz = rozkazy[biezacyRozkaz].Substring(0, 3);

                if (rozkaz == "MOV" || rozkaz == "ADD" || rozkaz == "SUB" || rozkaz == "POP" || rozkaz == "PUS")
                {
                    if (rozkaz == "PUS") // powyzej odczytywalismy 3 znaki, wiec to wystarczy, zeby rozpoznac PUSH. 
                    {
                        rozkaz = "PUSH"; // dalej bedziemy uzywac pełnej nazwy PUSH
                        rejestr1 = rozkazy[biezacyRozkaz].Substring(5, 2); // odczytujemy kolejne 2 znaki, czyli nazwe rejestru
                    }
                    else
                        rejestr1 = rozkazy[biezacyRozkaz].Substring(4, 2); // odczytujemy kolejne 2 znaki, czyli nazwe rejestru
                    
                    if (rozkaz == "MOV" || rozkaz == "ADD" || rozkaz == "SUB")
                        rejestr2lubStala = rozkazy[biezacyRozkaz].Substring(8, 2); // odczytujemy kolejne dwa znaki czyli nazwe drugiego rejestru lub stałej

                    switch (rejestr1)
                    {
                        case "AH":
                            rej1 = ref ax;
                            low1 = false;
                            break;
                        case "AL":
                            rej1 = ref ax;
                            low1 = true;
                            break;
                        case "BH":
                            rej1 = ref bx;
                            low1 = false;
                            break;
                        case "BL":
                            rej1 = ref bx;
                            low1 = true;
                            break;
                        case "CH":
                            rej1 = ref cx;
                            low1 = false;
                            break;
                        case "CL":
                            rej1 = ref cx;
                            low1 = true;
                            break;
                        case "DH":
                            rej1 = ref dx;
                            low1 = false;
                            break;
                        case "DL":
                            rej1 = ref dx;
                            low1 = true;
                            break;
                    }
                    if (rozkaz == "MOV" || rozkaz == "ADD" || rozkaz == "SUB")
                        switch (rejestr2lubStala)
                        {
                            case "AH":
                                rej2lubStala = ref ax;
                                low2 = false;
                                break;
                            case "AL":
                                rej2lubStala = ref ax;
                                low2 = true;
                                break;
                            case "BH":
                                rej2lubStala = ref bx;
                                low2 = false;
                                break;
                            case "BL":
                                rej2lubStala = ref bx;
                                low2 = true;
                                break;
                            case "CH":
                                rej2lubStala = ref cx;
                                low2 = false;
                                break;
                            case "CL":
                                rej2lubStala = ref cx;
                                low2 = true;
                                break;
                            case "DH":
                                rej2lubStala = ref dx;
                                low2 = false;
                                break;
                            case "DL":
                                rej2lubStala = ref dx;
                                low2 = true;
                                break;
                            default:
                                stala.l = Convert.ToInt32(rejestr2lubStala, 16);
                                rej2lubStala = ref stala;
                                low2 = true;
                                break;
                        }
                }
                else //rozkaz: INT, więc odczytujemy jeszcze XY i sklejamy do postaci INTXY (uzywanej w switchu w funkcji wykonajRozkaz)
                {
                    rozkaz = rozkaz + rozkazy[biezacyRozkaz].Substring(4, 2);
                }
                wykonajRozkaz(rozkaz, ref rej1, low1, ref rej2lubStala, low2);
                rozkazy[biezacyRozkaz] = rozkazy[biezacyRozkaz] + statusWykonania;
                File.WriteAllLines(@"rozkazy.txt", rozkazy);    //zapisujemy wszystko z tablicy do pliku
                lista_textbox.Text = File.ReadAllText(@"rozkazy.txt");  //czytamy wszystko z pliku do textboxa
                biezacyRozkaz++;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_stala.Checked)
                groupBox2.Enabled = false;
            else
                groupBox2.Enabled = true;
        }
        private void INT_05_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_05.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else 
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_16_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_16.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_17_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_17.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_19_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_19.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_1A_CheckedChanged(object sender, EventArgs e)
        {

            if (INT_1A.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_10_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_10.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_11_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_11.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_13_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_13.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_14_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_14.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void INT_15_CheckedChanged(object sender, EventArgs e)
        {
            if (INT_15.Checked)
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void PUSH_CheckedChanged(object sender, EventArgs e)
        {
            if (PUSH.Checked)
            {
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void POP_CheckedChanged(object sender, EventArgs e)
        {
            if (POP.Checked)
            {
                groupBox2.Enabled = false;
                checkBox_stala.Enabled = false;
                checkBox_stala.Checked = false;
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox2.Enabled = true;
                checkBox_stala.Enabled = true;
                groupBox2.Enabled = true;
            }
        }
        private void numericUpDown_stala_ValueChanged(object sender, EventArgs e)
        {
            stala.l = Convert.ToInt32(numericUpDown_stala.Value);
            stala_label.Text = Convert.ToString(stala.l, 16) + "h";
        }
        private void przyklady_automat_CheckedChanged(object sender, EventArgs e)
        {
            if (przyklady_automat.Checked)
                przyklady_auto = true;
            else
                przyklady_auto = false;
        }
        private void button_INT1A_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 3;    // funkcja 03h
            cx.h = 0x16; // hour
            cx.l = 0x49; // minutes
            dx.h = 0x30; // seconds
            dx.l = 1;    // daylight time
            ustawBityILabelki(ax, false);
            ustawBityILabelki(cx, false);
            ustawBityILabelki(cx, true);
            ustawBityILabelki(dx, false);
            ustawBityILabelki(dx, true);
        }

        private void button_INT16_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 0x10; // funkcja 10h
            ustawBityILabelki(ax, false);
        }

        private void button_INT17_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 0; // funkcja 00h
            ax.l = 0x61;   // ASCII code of character to be printed: 'a'
            dx.h = 0;
            dx.l = 1;  // DX (DH + DL) = printer number(i.e. 0 = LPT1, 1 = LPT2, 2 = LPT3)
            ustawBityILabelki(ax, false);
            ustawBityILabelki(ax, true);
            ustawBityILabelki(dx, false);
            ustawBityILabelki(dx, true);
        }
        
        private void button_INT10_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 0;
            ustawBityILabelki(ax, false);
        }

        private void button_INT13_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 1;
            ustawBityILabelki(ax, false);
            dx.l = 128;
            ustawBityILabelki(dx, true);
        }

        private void button_INT14_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 1;
            int kodZnaku;
            Random r = new Random();
            kodZnaku = r.Next(97, 122);
            ax.l = kodZnaku;
            dx.l = 0;   //serial port nr 0 (pierwszy)
            ustawBityILabelki(ax, true);
            ustawBityILabelki(ax, false);
            ustawBityILabelki(dx, true);
        }

        private void button_INT15_przyklad_Click(object sender, EventArgs e)
        {
            ax.h = 0x86;
            cx.h = 0;
            cx.l = 0;
            dx.h = 1;
            dx.l = 0x2C;         //h + l = 1 0010 1100 czyli 300ms
            ustawBityILabelki(ax, false);
            ustawBityILabelki(cx, false);
            ustawBityILabelki(cx, true);
            ustawBityILabelki(dx, false);
            ustawBityILabelki(dx, true);
        }
    }
}
