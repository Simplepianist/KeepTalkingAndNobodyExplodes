using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KeepTalkingAndNobodyExplodes
{
    /// <summary>
    ///     Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Main

        private int fehler;

        private bool? car = false;

        private bool? frk = false;

        private int batteries;


        private bool? iseven = false;
        private bool? vokal = false;
        private bool? parralel = false;

        private void bntSaveConfig_Click(object sender, RoutedEventArgs e)
        {
            car = ch_CAR.IsChecked;
            frk = ch_FRK.IsChecked;
            iseven = ch_iseven.IsChecked;
            vokal = ch_vokal.IsChecked;
            parralel = ch_parallel.IsChecked;
            if (!int.TryParse(count_Batterie.Text, out batteries))
                MessageBox.Show("Keine Zahl bei Batterie eingegeben", "Error Batterie");

            lb_saved.Content = "Saved";
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = 0;
        }

        private bool SwitchingTab()
        {
            if (lb_saved.Content != "Saved")
            {
                MessageBox.Show("Erst Konfig rechts angeben", "Konfiguration");
                return false;
            }

            return true;
        }

        private void btn_addFehler_wires_Click(object sender, RoutedEventArgs e)
        {
            fehler++;
            if (TabControl.SelectedIndex == 4) simonsays();

            lb_error_count_button.Content = fehler;
            lb_error_count_complicated.Content = fehler;
            lb_error_count_labyrinth.Content = fehler;
            lb_error_count_main.Content = fehler;
            lb_error_count_memory.Content = fehler;
            lb_error_count_passwd.Content = fehler;
            lb_error_count_sequence.Content = fehler;
            lb_error_count_simonsays.Content = fehler;
            lb_error_count_symbols.Content = fehler;
            lb_error_count_wires.Content = fehler;
            lb_error_count_word.Content = fehler;
        }

        private void btn_removeFehler_wires_Click(object sender, RoutedEventArgs e)
        {
            fehler--;
            if (fehler < 0) fehler = 0;

            if (TabControl.SelectedIndex == 4) simonsays();

            lb_error_count_button.Content = fehler;
            lb_error_count_complicated.Content = fehler;
            lb_error_count_labyrinth.Content = fehler;
            lb_error_count_main.Content = fehler;
            lb_error_count_memory.Content = fehler;

            lb_error_count_passwd.Content = fehler;
            lb_error_count_sequence.Content = fehler;
            lb_error_count_simonsays.Content = fehler;
            lb_error_count_symbols.Content = fehler;
            lb_error_count_wires.Content = fehler;
            lb_error_count_word.Content = fehler;
        }

        #endregion

        #region Wires

        private void btnWires_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 1;
            tb_wires.Text = "";
            lb_ans_wire.Visibility = Visibility.Hidden;
            lb_wire_cut.Visibility = Visibility.Hidden;
        }

        private void btn_send_wires_Click(object sender, RoutedEventArgs e)
        {
            var seqence = tb_wires.Text;
            var seqencelength = seqence.Length;
            var lenright = false;
            var ret = "error";
            IDictionary<string, int> farben = new Dictionary<string, int>();
            farben["r"] = 0;
            farben["b"] = 0;
            farben["g"] = 0;
            farben["w"] = 0;
            farben["s"] = 0;
            for (var i = 0; i < seqencelength; i++)
            {
                var count = farben[seqence.Substring(i, 1)] + 1;
                farben[seqence.Substring(i, 1)] = count;
            }

            switch (seqencelength)
            {
                case 3:
                    lenright = true;
                    if (!seqence.Contains("r"))
                        ret = "2. Draht";
                    else if (seqence.Substring(seqencelength - 1, 1) == "w")
                        ret = "letzten Draht";
                    else if (farben["b"] > 1)
                        ret = "letzten blauen Draht";
                    else
                        ret = "letzten Draht";

                    break;
                case 4:
                    lenright = true;
                    if (farben["r"] > 1 && !(bool) iseven)
                        ret = "letzten roten Draht";
                    else if (seqence.Substring(seqencelength - 1, 1) == "g" && farben["r"] == 0 || farben["b"] == 1)
                        ret = "ersten Draht";
                    else if (farben["g"] > 1)
                        ret = "letzten Draht";
                    else
                        ret = "zweiten Draht";

                    break;
                case 5:
                    lenright = true;
                    if (seqence.Substring(seqencelength - 1, 1) == "s" && !(bool) iseven)
                        ret = "vierten Draht";
                    else if (farben["r"] == 1 && farben["g"] > 1)
                        ret = "ersten Draht";
                    else if (farben["s"] == 0)
                        ret = "zweiten Draht";
                    else
                        ret = "ersten Draht";

                    break;
                case 6:
                    lenright = true;
                    if (farben["g"] == 0 && !(bool) iseven)
                        ret = "dritten Draht";
                    else if (farben["g"] == 1 && farben["w"] > 1)
                        ret = "vierten Draht";
                    else if (farben["r"] == 0)
                        ret = "letzen Draht";
                    else
                        ret = "vierten Draht";

                    break;
            }

            if (!lenright) MessageBox.Show("Keine richtige Anzahl der Drähte: " + seqencelength, "Anzahl der Drähte");

            lb_ans_wire.Visibility = Visibility.Visible;
            lb_wire_cut.Visibility = Visibility.Visible;
            lb_wire_cut.Content = ret;
        }

        #endregion

        #region Button

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 2;
            lb_type.Content = "";
            lb_ans_stripes.Content = "";
            ch_stripe_blue.Visibility = Visibility.Hidden;
            ch_stripe_white.Visibility = Visibility.Hidden;
            ch_stripe_other.Visibility = Visibility.Hidden;
            ch_stripe_yellow.Visibility = Visibility.Hidden;
            lb_stripes.Visibility = Visibility.Hidden;
            lb_ans_button.Visibility = Visibility.Hidden;
            btn_send_stripe.Visibility = Visibility.Hidden;
        }

        private void btn_senden_Click(object sender, RoutedEventArgs e)
        {
            var type = "";
            if ((bool) ch_blue.IsChecked && (bool) ch_abbrechen.IsChecked)
                type = "gedrückt halten";
            else if (batteries > 1 && (bool) ch_spreng.IsChecked)
                type = "kurz drücken";
            else if ((bool) ch_white.IsChecked && (bool) car)
                type = "gedrückt halten";
            else if (batteries > 2 && (bool) frk)
                type = "kurz drücken";
            else if ((bool) ch_yellow.IsChecked)
                type = "gedrückt halten";
            else if ((bool) ch_red.IsChecked && (bool) ch_hold.IsChecked)
                type = "kurz drücken";
            else
                type = "gedrückt halten";

            lb_ans_button.Visibility = Visibility.Visible;
            lb_type.Content = type;
            if (type == "gedrückt halten")
            {
                lb_stripes.Visibility = Visibility.Visible;
                ch_stripe_other.Visibility = Visibility.Visible;
                ch_stripe_white.Visibility = Visibility.Visible;
                ch_stripe_yellow.Visibility = Visibility.Visible;
                ch_stripe_blue.Visibility = Visibility.Visible;
                btn_send_stripe.Visibility = Visibility.Visible;
            }
        }

        private void btn_send_stripe_Click(object sender, RoutedEventArgs e)
        {
            var ans = "Loslassen, wenn Timer eine ";
            if ((bool) ch_stripe_white.IsChecked)
                ans += "1 ";
            else if ((bool) ch_stripe_blue.IsChecked)
                ans += "4 ";
            else if ((bool) ch_stripe_yellow.IsChecked)
                ans += "5 ";
            else if ((bool) ch_stripe_other.IsChecked)
                ans += "1 ";
            else
                MessageBox.Show("Bitte eine Farbe von Streifen auswählen", "Error with Stripes");

            lb_ans_stripes.Content = ans + "anzeigt";
        }

        #endregion

        #region Symbols

        private int counter;
        private readonly int[] pics = {50, 50, 50, 50};

        private readonly string[] symbuttons =
        {
            "sechs", "AT", "NBogen", "OStrich", "Omega", "Absatz", "ae", "alien", "weird3", "bT", "WKomma", "Bahn",
            "CLoop", "broken3", "dotedC", "Copyright", "HCurly", "NCurly", "EDoted", "backCDoted", "empStar",
            "fullStar", "question", "MirrorK", "Candelight", "Smiley", "Y"
        };

        private readonly string[] reihe1 = {"OStrich", "AT", "Y", "NCurly", "alien", "HCurly", "backCDoted"};

        private readonly string[] reihe2 =
            {"EDoted", "OStrich", "backCDoted", "CLoop", "empStar", "HCurly", "question"};

        private readonly string[] reihe3 = {"Copyright", "WKomma", "CLoop", "MirrorK", "broken3", "Y", "empStar"};
        private readonly string[] reihe4 = {"sechs", "Absatz", "bT", "alien", "MirrorK", "question", "Smiley"};
        private readonly string[] reihe5 = {"Candelight", "Smiley", "bT", "dotedC", "Absatz", "weird3", "fullStar"};
        private readonly string[] reihe6 = {"sechs", "EDoted", "Bahn", "ae", "Candelight", "NBogen", "Omega"};

        private readonly string[] pngs =
        {
            "6.PNG", "AT.PNG", "N_mit_bogen.PNG", "O_mit_Strich.PNG", "Omega.PNG", "absatz.PNG", "ae.PNG", "alien.PNG",
            "alien_3.PNG", "bT.PNG", "w_mit_komma.PNG", "bahnübergang.PNG", "c_looping.PNG", "broken_3.PNG",
            "c_mit_punkt.PNG", "copyright.PNG", "curly_H.PNG", "curly_N.PNG", "e_mit_punkten.PNG",
            "umgekehrte_C_mit_punkt.PNG", "empty_star.PNG", "filled_star.PNG", "espaniol_fragezeichen.PNG",
            "k_spiegel.PNG", "kerzenständer.PNG", "smiley.PNG", "umgekehrtes_Y.PNG"
        };

        private readonly IDictionary<string, string> path = new Dictionary<string, string>();

        private void btnSymbols_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                path.Clear();
                TabControl.SelectedIndex = 3;
                counter = 0;
                for (var i = 0; i < pics.Length; i++) pics[i] = 50;

                for (var i = 0; i < symbuttons.Length; i++)
                {
                    var pfad = "Bilder/Symbols/" + pngs[i];
                    path.Add(symbuttons[i], pfad);
                }
            }
        }

        private void getButton(object sender, RoutedEventArgs e)
        {
            if (counter >= 4)
            {
                MessageBox.Show("Schon 4 Symbole angegeben.\nPlease reset or Solve", "To many Symbols");
            }
            else
            {
                if (sender == sechs)
                    pics[counter] = 0;
                else if (sender == AT)
                    pics[counter] = 1;
                else if (sender == NBogen)
                    pics[counter] = 2;
                else if (sender == OStrich)
                    pics[counter] = 3;
                else if (sender == Omega)
                    pics[counter] = 4;
                else if (sender == Absatz)
                    pics[counter] = 5;
                else if (sender == ae)
                    pics[counter] = 6;
                else if (sender == alien)
                    pics[counter] = 7;
                else if (sender == weird3)
                    pics[counter] = 8;
                else if (sender == bT)
                    pics[counter] = 9;
                else if (sender == WKomma)
                    pics[counter] = 10;
                else if (sender == Bahn)
                    pics[counter] = 11;
                else if (sender == CLoop)
                    pics[counter] = 12;
                else if (sender == broken3)
                    pics[counter] = 13;
                else if (sender == dotedC)
                    pics[counter] = 14;
                else if (sender == Copyright)
                    pics[counter] = 15;
                else if (sender == HCurly)
                    pics[counter] = 16;
                else if (sender == NCurly)
                    pics[counter] = 17;
                else if (sender == EDoted)
                    pics[counter] = 18;
                else if (sender == backCDoted)
                    pics[counter] = 19;
                else if (sender == empStar)
                    pics[counter] = 20;
                else if (sender == fullStar)
                    pics[counter] = 21;
                else if (sender == question)
                    pics[counter] = 22;
                else if (sender == MirrorK)
                    pics[counter] = 23;
                else if (sender == Candelight)
                    pics[counter] = 24;
                else if (sender == Smiley)
                    pics[counter] = 25;
                else if (sender == Y) pics[counter] = 26;

                counter++;
            }
        }

        private void btn_Solve_Click(object sender, RoutedEventArgs e)
        {
            var found1 = 50;
            var found2 = 50;
            var found3 = 50;
            var found4 = 50;
            var reihe = 0;
            if (!pics.Contains(50))
            {
                if (reihe1.Contains(symbuttons[pics[0]]) && reihe1.Contains(symbuttons[pics[1]]) &&
                    reihe1.Contains(symbuttons[pics[2]]) && reihe1.Contains(symbuttons[pics[3]]))
                {
                    reihe = 1;
                    for (var i = 0; i < reihe1.Length; i++)
                    {
                        if (reihe1[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe1[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe1[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe1[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }
                else if (reihe2.Contains(symbuttons[pics[0]]) && reihe2.Contains(symbuttons[pics[1]]) &&
                         reihe2.Contains(symbuttons[pics[2]]) && reihe2.Contains(symbuttons[pics[3]]))
                {
                    reihe = 2;
                    for (var i = 0; i < reihe2.Length; i++)
                    {
                        if (reihe2[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe2[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe2[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe2[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }
                else if (reihe3.Contains(symbuttons[pics[0]]) && reihe3.Contains(symbuttons[pics[1]]) &&
                         reihe3.Contains(symbuttons[pics[2]]) && reihe3.Contains(symbuttons[pics[3]]))
                {
                    reihe = 3;
                    for (var i = 0; i < reihe3.Length; i++)
                    {
                        if (reihe3[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe3[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe3[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe3[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }
                else if (reihe4.Contains(symbuttons[pics[0]]) && reihe4.Contains(symbuttons[pics[1]]) &&
                         reihe4.Contains(symbuttons[pics[2]]) && reihe4.Contains(symbuttons[pics[3]]))
                {
                    reihe = 4;
                    for (var i = 0; i < reihe4.Length; i++)
                    {
                        if (reihe4[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe4[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe4[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe4[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }
                else if (reihe5.Contains(symbuttons[pics[0]]) && reihe5.Contains(symbuttons[pics[1]]) &&
                         reihe5.Contains(symbuttons[pics[2]]) && reihe5.Contains(symbuttons[pics[3]]))
                {
                    reihe = 5;
                    for (var i = 0; i < reihe5.Length; i++)
                    {
                        if (reihe5[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe5[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe5[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe5[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }
                else if (reihe6.Contains(symbuttons[pics[0]]) && reihe6.Contains(symbuttons[pics[1]]) &&
                         reihe6.Contains(symbuttons[pics[2]]) && reihe6.Contains(symbuttons[pics[3]]))
                {
                    reihe = 6;
                    for (var i = 0; i < reihe6.Length; i++)
                    {
                        if (reihe6[i] == symbuttons[pics[0]]) found1 = i;

                        if (reihe6[i] == symbuttons[pics[1]]) found2 = i;

                        if (reihe6[i] == symbuttons[pics[2]]) found3 = i;

                        if (reihe6[i] == symbuttons[pics[3]]) found4 = i;
                    }
                }

                int[] founds = {found1, found2, found3, found4};
                var ausgabe = "";
                foreach (var VARIABLE in founds) ausgabe += VARIABLE + " ";

                var length = founds.Length;

                var temp = founds[0];

                for (var i = 0; i < length; i++)
                for (var j = i + 1; j < length; j++)
                    if (founds[i] > founds[j])
                    {
                        temp = founds[i];
                        founds[i] = founds[j];
                        founds[j] = temp;
                    }

                

                var b1 = new BitmapImage();
                var b2 = new BitmapImage();
                var b3 = new BitmapImage();
                var b4 = new BitmapImage();
                Uri urip;
                b1.BeginInit();
                b2.BeginInit();
                b3.BeginInit();
                b4.BeginInit();

                switch (reihe)
                {
                    case 1:
                        urip = new Uri(path[reihe1[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe1[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe1[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe1[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                    case 2:
                        urip = new Uri(path[reihe2[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe2[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe2[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe2[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                    case 3:
                        urip = new Uri(path[reihe3[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe3[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe3[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe3[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                    case 4:
                        urip = new Uri(path[reihe4[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe4[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe4[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe4[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                    case 5:
                        urip = new Uri(path[reihe5[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe5[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe5[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe5[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                    case 6:
                        urip = new Uri(path[reihe6[founds[0]]], UriKind.Relative);
                        b1.UriSource = urip;
                        urip = new Uri(path[reihe6[founds[1]]], UriKind.Relative);
                        b2.UriSource = urip;
                        urip = new Uri(path[reihe6[founds[2]]], UriKind.Relative);
                        b3.UriSource = urip;
                        urip = new Uri(path[reihe6[founds[3]]], UriKind.Relative);
                        b4.UriSource = urip;
                        b1.EndInit();
                        b2.EndInit();
                        b3.EndInit();
                        b4.EndInit();
                        break;
                }


                sym1.Source = b1;
                sym2.Source = b2;
                sym3.Source = b3;
                sym4.Source = b4;
            }
        }

        #endregion

        #region SimonSays

        private void btnSimonSays_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 4;
                simonsays();
            }
        }

        private void simonsays()
        {
            if ((bool) vokal)
                switch (fehler)
                {
                    case 0:
                        lb_red.Content = "Blau";
                        lb_red.Foreground = Brushes.Blue;
                        lb_blue.Content = "Rot";
                        lb_blue.Foreground = Brushes.Red;
                        lb_green.Content = "Gelb";
                        lb_green.Foreground = Brushes.Yellow;
                        lb_yellow.Content = "Grün";
                        lb_yellow.Foreground = Brushes.Green;
                        break;
                    case 1:
                        lb_red.Content = "Gelb";
                        lb_red.Foreground = Brushes.Yellow;
                        lb_blue.Content = "Grün";
                        lb_blue.Foreground = Brushes.Green;
                        lb_green.Content = "Blau";
                        lb_green.Foreground = Brushes.Blue;
                        lb_yellow.Content = "Rot";
                        lb_yellow.Foreground = Brushes.Red;
                        break;
                    case 2:
                        lb_red.Content = "Grün";
                        lb_red.Foreground = Brushes.Green;
                        lb_blue.Content = "Rot";
                        lb_blue.Foreground = Brushes.Red;
                        lb_green.Content = "Gelb";
                        lb_green.Foreground = Brushes.Yellow;
                        lb_yellow.Content = "Blau";
                        lb_yellow.Foreground = Brushes.Blue;
                        break;
                }
            else
                switch (fehler)
                {
                    case 0:
                        lb_red.Content = "Blau";
                        lb_red.Foreground = Brushes.Blue;
                        lb_blue.Content = "Gelb";
                        lb_blue.Foreground = Brushes.Yellow;
                        lb_green.Content = "Grün";
                        lb_green.Foreground = Brushes.Green;
                        lb_yellow.Content = "Rot";
                        lb_yellow.Foreground = Brushes.Red;
                        break;
                    case 1:
                        lb_red.Content = "Rot";
                        lb_red.Foreground = Brushes.Red;
                        lb_blue.Content = "Blau";
                        lb_blue.Foreground = Brushes.Blue;
                        lb_green.Content = "Gelb";
                        lb_green.Foreground = Brushes.Yellow;
                        lb_yellow.Content = "Grün";
                        lb_yellow.Foreground = Brushes.Green;
                        break;
                    case 2:
                        lb_red.Content = "Gelb";
                        lb_red.Foreground = Brushes.Yellow;
                        lb_blue.Content = "Grün";
                        lb_blue.Foreground = Brushes.Green;
                        lb_green.Content = "Blau";
                        lb_green.Foreground = Brushes.Blue;
                        lb_yellow.Content = "Rot";
                        lb_yellow.Foreground = Brushes.Red;
                        break;
                }

            lb_errors.Content = fehler + ". Fehler";
        }

        #endregion

        #region Words

        private readonly IDictionary<string, string> kette = new Dictionary<string, string>();


        private void btnWords_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 5;

                Reset_Words();
            }
        }

        private void Reset_Words(object sender, RoutedEventArgs e)
        {
            Reset_Words();
            Reset_Words();
        }

        private void Reset_Words()
        {
            kette["Drück"] = "JA, Q, NEIN, O. K., OKAY, COUP, OK,\n LEER, DRÜCK,\t, NOCHMAL, WAS, NICHTS, KUH";
            kette["Nochmal"] = "OKAY, Q, JA, O. K.,\t, OK, NICHTS,\n WAS, KUH, DRÜCK, LEER, NEIN, COUP, NOCHMAL";
            kette[""] = "LEER, WAS, KUH, NOCHMAL, NEIN, DRÜCK, OK,\n JA, NICHTS, OKAY, COUP, Q,\t";
            kette["Leer"] = "KUH, OK, Q, O. K., LEER";
            kette["Nichts"] = "WAS, OK, Q, O. K., JA, LEER,\t, COUP,\n OKAY, NEIN, KUH, NOCHMAL, NICHTS";
            kette["Ja"] = "Q, OK, WAS, O. K., NOCHMAL, NEIN, COUP,\n DRÜCK, NICHTS, JA";
            kette["Nein"] = "WAS, NEIN";
            kette["Was"] = "DRÜCK, NICHTS, OKAY, NEIN, Q, JA, OK,\t,\n COUP, LEER, WAS, O. K., KUH, NOCHMAL";
            kette["Okay"] = "OK, OKAY";
            kette["OK"] = "JA, NICHTS, DRÜCK, COUP,\t, KUH, NEIN, OK";
            kette["O. K."] = "LEER, DRÜCK, Q, NEIN, NICHTS, COUP,\t,\n KUH, OKAY, O. K.";
            kette["Q"] = "O. K.,\t, NOCHMAL, JA, WAS, NICHTS, KUH, Q";
            kette["Kuh"] = "WAS,\t, LEER, Q, JA, OKAY, NOCHMAL,\nCOUP, NEIN, KUH";
            kette["Coup"] = "OK, O. K., JA, DRÜCK, COUP";
            kette["Sohn"] = "MOMENT, SO EIN, SO'N, OH GOTT, WAS?,\nZEH, ZEHN, WARTE, C, SOHN";
            kette["So ein"] =
                "SO'N, WAS?, DA STEHT, ZEH, C, ZEHEN,\n10, WARTE, SOHN, CN, OH GOTT, MOMENT, ZEHN, SO EIN";
            kette["So'n"] = "10, SO EIN, ZEH, SO'N";
            kette["Oh Gott"] = "SOHN, OH GOTT";
            kette["Zehn"] = "ZEHEN, CN, ZEHN";
            kette["CN"] = "ZEH, MOMENT, WAS?, C, OH GOTT, ZEHN,\n10, ZEHEN, CN";
            kette["Zeh"] = "ZEH";
            kette["10"] = "ZEHN, CN, SO EIN, OH GOTT, WAS?, 10";
            kette["C"] = "SOHN, WARTE, OH GOTT, SO'N, CN, ZEHEN, 10,\nDA STEHT, SO EIN, ZEH, ZEHN, WAS?, C";
            kette["Zehen"] = "MOMENT, ZEH, WAS?, C, SO'N, ZEHN, OH GOTT,\nWARTE, DA STEHT, SOHN, CN, SO EIN, 10, ZEHEN";
            kette["Was?"] = "C, ZEH, 10, SO'N, WARTE, MOMENT, WAS?";
            kette["Warte"] = "SO EIN, CN, ZEHEN, 10, SOHN, ZEHN, MOMENT,\nC, OH GOTT, WAS?, WARTE";
            kette["Moment"] = "SO EIN, ZEHEN, DA STEHT, OH GOTT, SOHN,\nWARTE, ZEH, ZEHN, MOMENT";
            kette["Da Steht"] = "OH GOTT, WAS?, CN, ZEHN, WARTE, ZEHEN,\n10, C, ZEH, SOHN, DA STEHT";
            Display_inhalt.Items.Add("Ja");
            Display_inhalt.Items.Add("Moment");
            Display_inhalt.Items.Add("Oben");
            Display_inhalt.Items.Add("Okay");
            Display_inhalt.Items.Add("Da Steht");
            Display_inhalt.Items.Add("Nichts");
            Display_inhalt.Items.Add("");
            Display_inhalt.Items.Add("Leer");
            Display_inhalt.Items.Add("Nein");
            Display_inhalt.Items.Add("Kuh");
            Display_inhalt.Items.Add("Q");
            Display_inhalt.Items.Add("Coup");
            Display_inhalt.Items.Add("Warte");
            Display_inhalt.Items.Add("Oh Gott");
            Display_inhalt.Items.Add("Fertig");
            Display_inhalt.Items.Add("Bumm");
            Display_inhalt.Items.Add("So ein");
            Display_inhalt.Items.Add("So'n");
            Display_inhalt.Items.Add("Sohn");
            Display_inhalt.Items.Add("Zehn");
            Display_inhalt.Items.Add("CN");
            Display_inhalt.Items.Add("Zehen");
            Display_inhalt.Items.Add("10");
            Display_inhalt.Items.Add("Zäh");
            Display_inhalt.Items.Add("Zeh");
            Display_inhalt.Items.Add("CE");
            Display_inhalt.Items.Add("C");
            Display_inhalt.Items.Add("Zu Spät");
            foreach (var keys in kette.Keys) Key_inhalt.Items.Add(keys);

            lb_key.Visibility = Visibility.Hidden;
            Key_inhalt.Visibility = Visibility.Hidden;
            lb_answers.Content = "";
            lb_answers.Visibility = Visibility.Hidden;
            lb_moeglich.Visibility = Visibility.Hidden;
            Display_inhalt.SelectedIndex = -1;
            Key_inhalt.SelectedIndex = -1;
            left_up_word.Source = null;
            left_middle_word.Source = null;
            left_bottom_word.Source = null;
            right_up_word.Source = null;
            right_middle_word.Source = null;
            right_bottom_word.Source = null;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var inhalt = (string) Display_inhalt.SelectedValue;
            var pic = new BitmapImage();
            var uripic = new Uri("Bilder/Words/eyes.png", UriKind.Relative);
            pic.BeginInit();
            pic.UriSource = uripic;
            pic.EndInit();

            if (inhalt == "CN")
            {
                left_up_word.Stretch = Stretch.Fill;
                left_up_word.Source = pic;
                left_middle_word.Source = null;
                left_bottom_word.Source = null;
                right_up_word.Source = null;
                right_middle_word.Source = null;
                right_bottom_word.Source = null;
            }
            else if (inhalt == "Ja" || inhalt == "Nichts" || inhalt == "Kuh" || inhalt == "Zeh")
            {
                left_middle_word.Stretch = Stretch.Fill;
                left_middle_word.Source = pic;
                left_up_word.Source = null;
                left_bottom_word.Source = null;
                right_up_word.Source = null;
                right_middle_word.Source = null;
                right_bottom_word.Source = null;
            }
            else if (inhalt == "" || inhalt == "Oh Gott" || inhalt == "Fertig" || inhalt == "10")
            {
                left_bottom_word.Stretch = Stretch.Fill;
                left_bottom_word.Source = pic;
                left_middle_word.Source = null;
                left_up_word.Source = null;
                right_up_word.Source = null;
                right_middle_word.Source = null;
                right_bottom_word.Source = null;
            }
            else if (inhalt == "Moment" || inhalt == "Okay" || inhalt == "C")
            {
                right_up_word.Stretch = Stretch.Fill;
                right_up_word.Source = pic;
                left_middle_word.Source = null;
                left_bottom_word.Source = null;
                left_up_word.Source = null;
                right_middle_word.Source = null;
                right_bottom_word.Source = null;
            }
            else if (inhalt == "Leer" || inhalt == "Coup" || inhalt == "Warte" || inhalt == "So ein" ||
                     inhalt == "Sohn" || inhalt == "Zehn" || inhalt == "Zäh")
            {
                right_middle_word.Stretch = Stretch.Fill;
                right_middle_word.Source = pic;
                left_middle_word.Source = null;
                left_bottom_word.Source = null;
                right_up_word.Source = null;
                left_up_word.Source = null;
                right_bottom_word.Source = null;
            }
            else if (inhalt == "Oben" || inhalt == "Da Steht" || inhalt == "Nein" || inhalt == "Q" ||
                     inhalt == "Bumm" || inhalt == "So'n" || inhalt == "Zehen" || inhalt == "CE" || inhalt == "Zu Spät")
            {
                right_bottom_word.Stretch = Stretch.Fill;
                right_bottom_word.Source = pic;
                left_middle_word.Source = null;
                left_bottom_word.Source = null;
                right_up_word.Source = null;
                right_middle_word.Source = null;
                left_up_word.Source = null;
            }

            lb_key.Visibility = Visibility.Visible;
            Key_inhalt.Visibility = Visibility.Visible;
        }

        private void Key_inhalt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Key_inhalt.SelectedIndex != -1)
            {
                lb_answers.Visibility = Visibility.Visible;
                lb_moeglich.Visibility = Visibility.Visible;
                lb_answers.Content = kette[(string) Key_inhalt.SelectedValue];
            }
        }

        #endregion

        #region Memory

        private int stufe;

        //                          { Zahl, Position }
        private int[] durch1 = {0, 0};
        private int[] durch2 = {0, 0};
        private int[] durch3 = {0, 0};
        private int[] durch4 = {0, 0};
        private int[] durch5 = {0, 0};
        private bool changed;
        private string search = "";

        private void btnMemory_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 6;
                stufe = 0;
                search = "";
                durch1 = new[] {0, 0};
                durch2 = new[] {0, 0};
                durch3 = new[] {0, 0};
                durch4 = new[] {0, 0};
                durch5 = new[] {0, 0};
                changed = false;
                lb_answer.Content = "";
                tb_anzeige.Text = "";
                tb_num.Visibility = Visibility.Hidden;
                tb_pos.Visibility = Visibility.Hidden;
                memory_num.Visibility = Visibility.Hidden;
                memory_pos.Visibility = Visibility.Hidden;
                lb_saved_mem.Visibility = Visibility.Hidden;
                lb_memory_durchgang.Content = stufe + 1 + ". Druchgang";
            }
        }


        private void btn_check_memory_Click(object sender, RoutedEventArgs e)
        {
            if (stufe < 5)
            {
                var num = 0;
                int pos;
                var ausgabe = "";
                lb_saved_mem.Visibility = Visibility.Hidden;
                btn_save_memory.Visibility = Visibility.Visible;
                if (!int.TryParse(tb_anzeige.Text, out num))
                {
                    MessageBox.Show("Keine Zahl zum Checken", "No Number in Check");
                }
                else
                {
                    if (stufe == 0)
                    {
                        switch (num)
                        {
                            case 1:
                                ausgabe = "2. Position";
                                durch1[1] = 2;
                                search = "Number";
                                break;
                            case 2:
                                ausgabe = "2. Position";
                                durch1[1] = 2;
                                search = "Number";
                                break;
                            case 3:
                                ausgabe = "3. Position";
                                durch1[1] = 3;
                                search = "Number";
                                break;
                            case 4:
                                ausgabe = "4. Position";
                                durch1[1] = 4;
                                search = "Number";
                                break;
                            default:
                                ausgabe = "Falsch";
                                stufe--;
                                break;
                        }
                    }
                    else
                    {
                        if (changed)
                            switch (stufe)
                            {
                                case 1:
                                    switch (num)
                                    {
                                        case 1:
                                            ausgabe = "Beschriftung 4";
                                            durch2[0] = 4;
                                            search = "Position";
                                            break;
                                        case 2:
                                            pos = durch1[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        case 3:
                                            ausgabe = "1. Position";
                                            durch2[1] = 2;
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch1[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        default:
                                            ausgabe = "Falsch";
                                            stufe--;
                                            break;
                                    }

                                    break;
                                case 2:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch2[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        case 2:
                                            pos = durch1[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        case 3:
                                            ausgabe = "3. Position";
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch1[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        default:
                                            ausgabe = "Falsch";
                                            stufe--;
                                            stufe--;
                                            break;
                                    }

                                    break;
                                case 3:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch1[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        case 2:
                                            ausgabe = "1. Position";
                                            search = "Number";
                                            break;
                                        case 3:
                                            pos = durch2[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch3[1];
                                            ausgabe = pos + ". Position";
                                            search = "Number";
                                            break;
                                        default:
                                            ausgabe = "Falsch";
                                            stufe--;
                                            stufe--;
                                            break;
                                    }

                                    break;
                                case 4:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch1[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        case 2:
                                            pos = durch2[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        case 3:
                                            pos = durch4[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        case 4:
                                            pos = durch3[0];
                                            ausgabe = "Beschriftung " + pos;
                                            search = "Position";
                                            break;
                                        default:
                                            ausgabe = "Falsch";
                                            stufe--;
                                            stufe--;
                                            break;
                                    }

                                    break;
                            }
                    }

                    lb_answer.Content = ausgabe;
                    if (changed || stufe == 0 || stufe == -1)
                    {
                        changed = false;
                        stufe++;
                    }

                    if (search == "Position")
                    {
                        memory_pos.Visibility = Visibility.Visible;
                        tb_pos.Visibility = Visibility.Visible;
                    }
                    else if (search == "Number")
                    {
                        memory_num.Visibility = Visibility.Visible;
                        tb_num.Visibility = Visibility.Visible;
                    }
                    
                }
            }
        }

        private void btn_save_memory_Click(object sender, RoutedEventArgs e)
        {
            if (stufe < 5)
            {
                changed = true;
                string content;
                if (search == "Position")
                {
                    content = tb_pos.Text;
                    switch (stufe)
                    {
                        case 0:
                            durch1[1] = int.Parse(content);
                            break;
                        case 1:
                            durch2[1] = int.Parse(content);
                            break;
                        case 2:
                            durch3[1] = int.Parse(content);
                            break;
                        case 3:
                            durch4[1] = int.Parse(content);
                            break;
                        case 4:
                            durch5[1] = int.Parse(content);
                            break;
                    }
                }
                else
                {
                    content = tb_num.Text;
                    switch (stufe - 1)
                    {
                        case 0:
                            durch1[0] = int.Parse(content);
                            break;
                        case 1:
                            durch2[0] = int.Parse(content);
                            break;
                        case 2:
                            durch3[0] = int.Parse(content);
                            break;
                        case 3:
                            durch4[0] = int.Parse(content);
                            break;
                        case 4:
                            durch5[0] = int.Parse(content);
                            break;
                    }
                }

                lb_saved_mem.Visibility = Visibility.Visible;
                memory_num.Visibility = Visibility.Hidden;
                memory_pos.Visibility = Visibility.Hidden;
                tb_num.Visibility = Visibility.Hidden;
                tb_pos.Visibility = Visibility.Hidden;
                btn_save_memory.Visibility = Visibility.Hidden;
                tb_num.Text = "";
                tb_pos.Text = "";
                tb_anzeige.Text = "";
                lb_memory_durchgang.Content = stufe + 1 + ". Druchgang";
            }
        }

        #endregion

        #region Complicated

        private void btnComplicated_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 7;
                compli_blue.IsChecked = false;
                compli_white.IsChecked = false;
                compli_led.IsChecked = false;
                compli_red.IsChecked = false;
                compli_stern.IsChecked = false;
                lb_comli_cut.Content = "";
            }
        }

        private void btn_compli_solve_Click(object sender, RoutedEventArgs e)
        {
            var ans = "";
            if ((bool) compli_stern.IsChecked)
            {
                if ((bool) compli_led.IsChecked)
                {
                    if ((bool) compli_blue.IsChecked)
                    {
                        if ((bool) compli_red.IsChecked)
                            ans = "N";
                        else
                            ans = "P";
                    }
                    else if ((bool) compli_red.IsChecked)
                    {
                        ans = "B";
                    }
                    else if ((bool) compli_white.IsChecked)
                    {
                        ans = "B";
                    }
                }
                else
                {
                    if ((bool) compli_blue.IsChecked)
                    {
                        if ((bool) compli_red.IsChecked)
                            ans = "P";
                        else
                            ans = "N";
                    }
                    else if ((bool) compli_red.IsChecked)
                    {
                        ans = "D";
                    }
                    else if ((bool) compli_white.IsChecked)
                    {
                        ans = "D";
                    }
                }
            }
            else
            {
                if ((bool) compli_led.IsChecked)
                {
                    if ((bool) compli_blue.IsChecked)
                    {
                        if ((bool) compli_red.IsChecked)
                            ans = "S";
                        else
                            ans = "P";
                    }
                    else if ((bool) compli_red.IsChecked)
                    {
                        ans = "B";
                    }
                    else if ((bool) compli_white.IsChecked)
                    {
                        ans = "N";
                    }
                }
                else
                {
                    if ((bool) compli_blue.IsChecked)
                    {
                        if ((bool) compli_red.IsChecked)
                            ans = "S";
                        else
                            ans = "S";
                    }
                    else if ((bool) compli_red.IsChecked)
                    {
                        ans = "S";
                    }
                    else if ((bool) compli_white.IsChecked)
                    {
                        ans = "D";
                    }
                }
            }

            var cut = "";
            if (ans == "D")
            {
                cut = "Cut it";
            }
            else if (ans == "N")
            {
                cut = "Don't cut it";
            }
            else if (ans == "S")
            {
                if ((bool) iseven)
                    cut = "Cut it";
                else
                    cut = "Don't cut it";
            }
            else if (ans == "P")
            {
                if ((bool) parralel)
                    cut = "Cut it";
                else
                    cut = "Don't cut it";
            }
            else if (ans == "B")
            {
                if (batteries >= 2)
                    cut = "Cut it";
                else
                    cut = "Don't cut it";
            }

            lb_comli_cut.Content = cut;
        }

        #endregion

        #region Sequence

        private int blue;
        private int red;
        private int black;

        private void btnSequence_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 8;
                blue = 0;
                red = 0;
                black = 0;
                CutOrNot.Content = "";
                CutOrNot_Label.Content = "";
            }
        }

        private void Complicated_Blue_Click(object sender, RoutedEventArgs e)
        {
            var content = "Cut wenn ";
            blue++;
            var cases = false;
            switch (blue)
            {
                case 1:
                    content += "B";
                    cases = true;
                    break;
                case 2:
                    content += "A oder C";
                    cases = true;
                    break;
                case 3:
                    content += "B";
                    cases = true;
                    break;
                case 4:
                    content += "A";
                    cases = true;
                    break;
                case 5:
                    content += "B";
                    cases = true;
                    break;
                case 6:
                    content += "B oder C";
                    cases = true;
                    break;
                case 7:
                    content += "C";
                    cases = true;
                    break;
                case 8:
                    content += "A oder C";
                    cases = true;
                    break;
                case 9:
                    content += "A";
                    cases = true;
                    break;
            }

            CutOrNot_Label.Content = "Blau";
            if (cases)
                CutOrNot.Content = content;
            else
                CutOrNot.Content = "No more Wires";
        }

        private void Complicated_Red_Click(object sender, RoutedEventArgs e)
        {
            var content = "Cut wenn ";
            red++;
            var cases = false;
            switch (red)
            {
                case 1:
                    content += "C";
                    cases = true;
                    break;
                case 2:
                    content += "B";
                    cases = true;
                    break;
                case 3:
                    content += "A";
                    cases = true;
                    break;
                case 4:
                    content += "A oder C";
                    cases = true;
                    break;
                case 5:
                    content += "B";
                    cases = true;
                    break;
                case 6:
                    content += "A oder C";
                    cases = true;
                    break;
                case 7:
                    content = "Just Cut it";
                    cases = true;
                    break;
                case 8:
                    content += "A oder B";
                    cases = true;
                    break;
                case 9:
                    content += "B";
                    cases = true;
                    break;
            }

            CutOrNot_Label.Content = "Rot";
            if (cases)
                CutOrNot.Content = content;
            else
                CutOrNot.Content = "No more Wires";
        }

        private void Complicated_Black_Click(object sender, RoutedEventArgs e)
        {
            var content = "Cut wenn ";
            black++;
            var cases = false;
            switch (black)
            {
                case 1:
                    content = "Just Cut it";
                    cases = true;
                    break;
                case 2:
                    content += "A oder C";
                    cases = true;
                    break;
                case 3:
                    content += "B";
                    cases = true;
                    break;
                case 4:
                    content += "A oder C";
                    cases = true;
                    break;
                case 5:
                    content += "B";
                    cases = true;
                    break;
                case 6:
                    content += "B oder C";
                    cases = true;
                    break;
                case 7:
                    content += "A oder B";
                    cases = true;
                    break;
                case 8:
                    content += "C";
                    cases = true;
                    break;
                case 9:
                    content += "C";
                    cases = true;
                    break;
            }

            CutOrNot_Label.Content = "Schwarz";
            if (cases)
                CutOrNot.Content = content;
            else
                CutOrNot.Content = "No more Wires";
        }

        #endregion

        #region Labyrinth

        private void btnLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 9;
                buildLabyrinth(0);
            }
        }

        private bool placeThings(int pos, string type)
        {
            bool geht = false;
            if (type == "P")
            {
                switch (pos)
                {
                    case 11:
                        pos11.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 12:
                        pos12.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 13:
                        pos13.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 14:
                        pos14.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 15:
                        pos15.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 16:
                        pos16.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 21:
                        pos21.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 22:
                        pos22.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 23:
                        pos23.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 24:
                        pos24.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 25:
                        pos25.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 26:
                        pos26.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 31:
                        pos31.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 32:
                        pos32.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 33:
                        pos33.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 34:
                        pos34.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 35:
                        pos35.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 36:
                        pos36.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 41:
                        pos41.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 42:
                        pos42.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 43:
                        pos43.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 44:
                        pos44.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 45:
                        pos45.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 46:
                        pos46.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 51:
                        pos51.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 52:
                        pos52.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 53:
                        pos53.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 54:
                        pos54.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 55:
                        pos55.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 56:
                        pos56.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 61:
                        pos61.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 62:
                        pos62.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 63:
                        pos63.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 64:
                        pos64.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 65:
                        pos65.Background = Brushes.Red;
                        geht = true;
                        break;
                    case 66:
                        pos66.Background = Brushes.Red;
                        geht = true;
                        break;
                }

            }
            else
            {
                switch (pos)
                {
                    case 11:
                        pos11.Background = Brushes.White;
                        geht = true;
                        break;
                    case 12:
                        pos12.Background = Brushes.White;
                        geht = true;
                        break;
                    case 13:
                        pos13.Background = Brushes.White;
                        geht = true;
                        break;
                    case 14:
                        pos14.Background = Brushes.White;
                        geht = true;
                        break;
                    case 15:
                        pos15.Background = Brushes.White;
                        geht = true;
                        break;
                    case 16:
                        pos16.Background = Brushes.White;
                        geht = true;
                        break;
                    case 21:
                        pos21.Background = Brushes.White;
                        geht = true;
                        break;
                    case 22:
                        pos22.Background = Brushes.White;
                        geht = true;
                        break;
                    case 23:
                        pos23.Background = Brushes.White;
                        geht = true;
                        break;
                    case 24:
                        pos24.Background = Brushes.White;
                        geht = true;
                        break;
                    case 25:
                        pos25.Background = Brushes.White;
                        geht = true;
                        break;
                    case 26:
                        pos26.Background = Brushes.White;
                        geht = true;
                        break;
                    case 31:
                        pos31.Background = Brushes.White;
                        geht = true;
                        break;
                    case 32:
                        pos32.Background = Brushes.White;
                        geht = true;
                        break;
                    case 33:
                        pos33.Background = Brushes.White;
                        geht = true;
                        break;
                    case 34:
                        pos34.Background = Brushes.White;
                        geht = true;
                        break;
                    case 35:
                        pos35.Background = Brushes.White;
                        geht = true;
                        break;
                    case 36:
                        pos36.Background = Brushes.White;
                        geht = true;
                        break;
                    case 41:
                        pos41.Background = Brushes.White;
                        geht = true;
                        break;
                    case 42:
                        pos42.Background = Brushes.White;
                        geht = true;
                        break;
                    case 43:
                        pos43.Background = Brushes.White;
                        geht = true;
                        break;
                    case 44:
                        pos44.Background = Brushes.White;
                        geht = true;
                        break;
                    case 45:
                        pos45.Background = Brushes.White;
                        geht = true;
                        break;
                    case 46:
                        pos46.Background = Brushes.White;
                        geht = true;
                        break;
                    case 51:
                        pos51.Background = Brushes.White;
                        geht = true;
                        break;
                    case 52:
                        pos52.Background = Brushes.White;
                        geht = true;
                        break;
                    case 53:
                        pos53.Background = Brushes.White;
                        geht = true;
                        break;
                    case 54:
                        pos54.Background = Brushes.White;
                        geht = true;
                        break;
                    case 55:
                        pos55.Background = Brushes.White;
                        geht = true;
                        break;
                    case 56:
                        pos56.Background = Brushes.White;
                        geht = true;
                        break;
                    case 61:
                        pos61.Background = Brushes.White;
                        geht = true;
                        break;
                    case 62:
                        pos62.Background = Brushes.White;
                        geht = true;
                        break;
                    case 63:
                        pos63.Background = Brushes.White;
                        geht = true;
                        break;
                    case 64:
                        pos64.Background = Brushes.White;
                        geht = true;
                        break;
                    case 65:
                        pos65.Background = Brushes.White;
                        geht = true;
                        break;
                    case 66:
                        pos66.Background = Brushes.White;
                        geht = true;
                        break;
                }

            }

            return geht;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool trykreis = int.TryParse(labi_kreis.Text, out int kreis);
            bool tryplayer = int.TryParse(labi_you.Text, out int player);
            bool tryziel = int.TryParse(labi_ziel.Text, out int goal);
            if (trykreis && tryplayer && tryziel)
            {
                
                if (!buildLabyrinth(kreis))
                {
                    buildLabyrinth(0);
                }
                else if (!placeThings(player, "P"))
                {
                    buildLabyrinth(0);
                }
                else if (!placeThings(goal, "Z"))
                {
                    buildLabyrinth(0);
                }

                
                
            }
            
        }

        private bool buildLabyrinth(int layout)
        {
            bool geht = true;
            Thickness l5111 = new Thickness(5, 1, 1, 1);
            Thickness l1513 = new Thickness(1, 5, 1, 3);
            Thickness l1531 = new Thickness(1, 5, 3, 1);
            Thickness l3511 = new Thickness(3, 5, 1, 1);
            Thickness l1553 = new Thickness(1, 5, 5, 3);
            Thickness l5131 = new Thickness(5, 1, 3, 1);
            Thickness l3311 = new Thickness(3, 3, 1, 1);
            Thickness l1133 = new Thickness(1, 1, 3, 3);
            Thickness l3113 = new Thickness(3, 1, 1, 3);
            Thickness l1313 = new Thickness(1, 3, 1, 3);
            Thickness l1351 = new Thickness(1, 3, 5, 1);
            Thickness l1331 = new Thickness(1, 3, 3, 1);
            Thickness l3313 = new Thickness(3, 3, 1, 3);
            Thickness l1113 = new Thickness(1, 1, 1, 3);
            Thickness l1333 = new Thickness(1, 3, 3, 3);
            Thickness l3151 = new Thickness(3, 1, 5, 1);
            Thickness l1335 = new Thickness(1, 3, 3, 5);
            Thickness l3115 = new Thickness(3, 1, 1, 5);
            Thickness l1135 = new Thickness(1, 1, 3, 5);
            Thickness l5513 = new Thickness(5, 5, 1, 3);
            Thickness l1533 = new Thickness(1, 5, 3, 3);
            Thickness l5311 = new Thickness(5, 3, 1, 1);
            Thickness l3331 = new Thickness(3, 3, 3, 1);
            Thickness l3131 = new Thickness(3, 1, 3, 1);
            Thickness l5135 = new Thickness(5, 1, 3, 5);
            Thickness l1315 = new Thickness(1, 3, 1, 5);
            Thickness l3531 = new Thickness(3, 5, 3, 1);
            Thickness l5133 = new Thickness(5, 1, 3, 3);
            Thickness l1131 = new Thickness(1, 1, 3, 1);
            Thickness l3513 = new Thickness(3, 5, 1, 3);
            Thickness l3315 = new Thickness(3, 3, 1, 5);
            Thickness l3153 = new Thickness(3, 1, 5, 3);
            Thickness l3133 = new Thickness(3, 1, 3, 3);
            Thickness l5531 = new Thickness(5, 5, 3, 1);
            Thickness l5113 = new Thickness(5, 1, 1, 3);
            Thickness l3351 = new Thickness(3, 3, 5, 1);
            Thickness l1153 = new Thickness(1, 1, 5, 3);
            Thickness l1355 = new Thickness(1, 3, 5, 5);
            pos11.Background = Brushes.LightGray;
            pos12.Background = Brushes.LightGray;
            pos13.Background = Brushes.LightGray;
            pos14.Background = Brushes.LightGray;
            pos15.Background = Brushes.LightGray;
            pos16.Background = Brushes.LightGray;
            pos21.Background = Brushes.LightGray;
            pos22.Background = Brushes.LightGray;
            pos23.Background = Brushes.LightGray;
            pos24.Background = Brushes.LightGray;
            pos25.Background = Brushes.LightGray;
            pos26.Background = Brushes.LightGray;
            pos31.Background = Brushes.LightGray;
            pos32.Background = Brushes.LightGray;
            pos33.Background = Brushes.LightGray;
            pos34.Background = Brushes.LightGray;
            pos35.Background = Brushes.LightGray;
            pos36.Background = Brushes.LightGray;
            pos41.Background = Brushes.LightGray;
            pos42.Background = Brushes.LightGray;
            pos43.Background = Brushes.LightGray;
            pos44.Background = Brushes.LightGray;
            pos45.Background = Brushes.LightGray;
            pos46.Background = Brushes.LightGray;
            pos51.Background = Brushes.LightGray;
            pos52.Background = Brushes.LightGray;
            pos53.Background = Brushes.LightGray;
            pos54.Background = Brushes.LightGray;
            pos55.Background = Brushes.LightGray;
            pos56.Background = Brushes.LightGray;
            pos61.Background = Brushes.LightGray;
            pos62.Background = Brushes.LightGray;
            pos63.Background = Brushes.LightGray;
            pos64.Background = Brushes.LightGray;
            pos65.Background = Brushes.LightGray;
            pos66.Background = Brushes.LightGray;
            switch (layout)
            {
                case int pos when pos == 21 || pos == 36:

                    pos21.Background = Brushes.LightGreen;
                    pos36.Background = Brushes.LightGreen;
                    pos12.BorderThickness = l1513;
                    pos13.BorderThickness = l1531;
                    pos14.BorderThickness = l3511;
                    pos15.BorderThickness = l1513;
                    pos16.BorderThickness = l1553;
                    pos21.BorderThickness = l5131;
                    pos22.BorderThickness = l3311;
                    pos23.BorderThickness = l1133;
                    pos24.BorderThickness = l3113;
                    pos25.BorderThickness = l1313;
                    pos26.BorderThickness = l1351;
                    pos31.BorderThickness = l5131;
                    pos32.BorderThickness = l3113;
                    pos33.BorderThickness = l1331;
                    pos34.BorderThickness = l3311;
                    pos35.BorderThickness = l1313;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3313;
                    pos43.BorderThickness = l1113;
                    pos44.BorderThickness = l1133;
                    pos45.BorderThickness = l3313;
                    pos52.BorderThickness = l1313;
                    pos53.BorderThickness = l1331;
                    pos54.BorderThickness = l3311;
                    pos55.BorderThickness = l1333;
                    pos56.BorderThickness = l3151;
                    pos62.BorderThickness = l1335;
                    pos63.BorderThickness = l3115;
                    pos64.BorderThickness = l1135;
                    pos65.BorderThickness = l3115;
                    break;
                case int pos when pos == 25 || pos == 42:
                    pos25.Background = Brushes.LightGreen;
                    pos42.Background = Brushes.LightGreen;
                    pos11.BorderThickness = l5513;
                    pos13.BorderThickness = l1533;
                    pos16.BorderThickness = l1553;
                    pos21.BorderThickness = l5311;
                    pos22.BorderThickness = l1133;
                    pos23.BorderThickness = l3311;
                    pos24.BorderThickness = l1133;
                    pos25.BorderThickness = l3113;
                    pos26.BorderThickness = l1351;
                    pos31.BorderThickness = l5131;
                    pos32.BorderThickness = l3311;
                    pos33.BorderThickness = l1133;
                    pos34.BorderThickness = l3311;
                    pos35.BorderThickness = l1313;
                    pos42.BorderThickness = l1133;
                    pos43.BorderThickness = l3311;
                    pos44.BorderThickness = l1133;
                    pos45.BorderThickness = l3331;
                    pos46.BorderThickness = l3151;
                    pos51.BorderThickness = l5111;
                    pos52.BorderThickness = l3331;
                    pos53.BorderThickness = l3131;
                    pos54.BorderThickness = l3311;
                    pos55.BorderThickness = l1133;
                    pos56.BorderThickness = l3151;
                    pos61.BorderThickness = l5135;
                    pos62.BorderThickness = l3115;
                    pos63.BorderThickness = l1135;
                    pos64.BorderThickness = l3115;
                    pos65.BorderThickness = l1315;
                    break;
                case int pos when pos == 44 || pos == 46:
                    pos44.Background = Brushes.LightGreen;
                    pos46.Background = Brushes.LightGreen;
                    pos12.BorderThickness = l1513;
                    pos13.BorderThickness = l1531;
                    pos14.BorderThickness = l3531;
                    pos15.BorderThickness = l3511;
                    pos21.BorderThickness = l5133;
                    pos22.BorderThickness = l3331;
                    pos23.BorderThickness = l3131;
                    pos24.BorderThickness = l3113;
                    pos25.BorderThickness = l1133;
                    pos26.BorderThickness = l3151;
                    pos31.BorderThickness = l5311;
                    pos32.BorderThickness = l1131;
                    pos33.BorderThickness = l3131;
                    pos34.BorderThickness = l3311;
                    pos35.BorderThickness = l1331;
                    pos36.BorderThickness = l3151;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3131;
                    pos43.BorderThickness = l3131;
                    pos44.BorderThickness = l3131;
                    pos45.BorderThickness = l3131;
                    pos46.BorderThickness = l3151;
                    pos51.BorderThickness = l5131;
                    pos52.BorderThickness = l3113;
                    pos53.BorderThickness = l1133;
                    pos54.BorderThickness = l3131;
                    pos55.BorderThickness = l3131;
                    pos56.BorderThickness = l3151;
                    pos62.BorderThickness = l1315;
                    pos63.BorderThickness = l1315;
                    pos64.BorderThickness = l1135;
                    pos65.BorderThickness = l3115;
                    break;
                case int pos when pos == 11 || pos == 41:
                    pos11.Background = Brushes.LightGreen;
                    pos41.Background = Brushes.LightGreen;
                    pos12.BorderThickness = l1531;
                    pos13.BorderThickness = l3513;
                    pos14.BorderThickness = l1513;
                    pos15.BorderThickness = l1513;
                    pos21.BorderThickness = l5131;
                    pos22.BorderThickness = l3131;
                    pos23.BorderThickness = l3311;
                    pos24.BorderThickness = l1313;
                    pos25.BorderThickness = l1313;
                    pos31.BorderThickness = l5131;
                    pos32.BorderThickness = l3113;
                    pos33.BorderThickness = l1133;
                    pos34.BorderThickness = l3311;
                    pos35.BorderThickness = l1333;
                    pos36.BorderThickness = l3151;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3313;
                    pos43.BorderThickness = l1313;
                    pos44.BorderThickness = l1113;
                    pos45.BorderThickness = l1313;
                    pos52.BorderThickness = l1313;
                    pos53.BorderThickness = l1313;
                    pos54.BorderThickness = l1313;
                    pos55.BorderThickness = l1331;
                    pos56.BorderThickness = l3151;
                    pos62.BorderThickness = l1315;
                    pos63.BorderThickness = l1335;
                    pos64.BorderThickness = l3315;
                    pos65.BorderThickness = l1135;
                    pos66.BorderThickness = new Thickness(3, 1, 5, 5);
                    break;
                case int pos when pos == 35 || pos == 64:
                    pos35.Background = Brushes.LightGreen;
                    pos64.Background = Brushes.LightGreen;
                    pos11.BorderThickness = l5513;
                    pos12.BorderThickness = l1513;
                    pos13.BorderThickness = l1513;
                    pos14.BorderThickness = l1513;
                    pos21.BorderThickness = l5311;
                    pos22.BorderThickness = l1313;
                    pos23.BorderThickness = l1313;
                    pos24.BorderThickness = new Thickness(1, 3, 1, 1);
                    pos25.BorderThickness = l1133;
                    pos26.BorderThickness = l3153;
                    pos32.BorderThickness = l1331;
                    pos33.BorderThickness = l3313;
                    pos34.BorderThickness = l1133;
                    pos35.BorderThickness = l3311;
                    pos36.BorderThickness = l1351;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3113;
                    pos43.BorderThickness = l1313;
                    pos44.BorderThickness = l1331;
                    pos45.BorderThickness = l3133;
                    pos46.BorderThickness = l3151;
                    pos51.BorderThickness = l5131;
                    pos52.BorderThickness = l3311;
                    pos53.BorderThickness = l1313;
                    pos54.BorderThickness = l1113;
                    pos55.BorderThickness = l1333;
                    pos56.BorderThickness = l3151;
                    pos61.BorderThickness = l5135;
                    pos62.BorderThickness = l3115;
                    pos63.BorderThickness = l1315;
                    pos64.BorderThickness = l1315;
                    pos65.BorderThickness = l1315;
                    break;
                case int pos when pos == 15 || pos == 53:
                    pos15.Background = Brushes.LightGreen;
                    pos53.Background = Brushes.LightGreen;
                    pos11.BorderThickness = l5531;
                    pos12.BorderThickness = l3511;
                    pos13.BorderThickness = l1531;
                    pos14.BorderThickness = l3513;
                    pos21.BorderThickness = l5131;
                    pos22.BorderThickness = l3131;
                    pos23.BorderThickness = l3131;
                    pos24.BorderThickness = l3311;
                    pos25.BorderThickness = l1133;
                    pos26.BorderThickness = l3151;
                    pos32.BorderThickness = l1133;
                    pos33.BorderThickness = l3133;
                    pos34.BorderThickness = l3131;
                    pos35.BorderThickness = l3311;
                    pos41.BorderThickness = l5113;
                    pos42.BorderThickness = l1331;
                    pos43.BorderThickness = l3311;
                    pos44.BorderThickness = l1131;
                    pos45.BorderThickness = l3131;
                    pos46.BorderThickness = l3351;
                    pos51.BorderThickness = l5311;
                    pos52.BorderThickness = l1133;
                    pos53.BorderThickness = l3133;
                    pos54.BorderThickness = l3131;
                    pos55.BorderThickness = l3113;
                    pos62.BorderThickness = l1315;
                    pos63.BorderThickness = l1315;
                    pos64.BorderThickness = l1135;
                    pos65.BorderThickness = l3315;
                    break;
                case int pos when pos == 12 || pos == 62:
                    pos12.Background = Brushes.LightGreen;
                    pos62.Background = Brushes.LightGreen;
                    pos12.BorderThickness = l1513;
                    pos13.BorderThickness = l1513;
                    pos14.BorderThickness = l1531;
                    pos15.BorderThickness = l3511;
                    pos21.BorderThickness = l5131;
                    pos22.BorderThickness = l3311;
                    pos23.BorderThickness = l1333;
                    pos24.BorderThickness = l3113;
                    pos25.BorderThickness = l1133;
                    pos26.BorderThickness = l3151;
                    pos31.BorderThickness = l5113;
                    pos32.BorderThickness = l1133;
                    pos33.BorderThickness = l3311;
                    pos34.BorderThickness = l1333;
                    pos35.BorderThickness = l3311;
                    pos36.BorderThickness = l1153;
                    pos41.BorderThickness = l5311;
                    pos42.BorderThickness = l1331;
                    pos43.BorderThickness = new Thickness(3, 1, 1, 1);
                    pos44.BorderThickness = l1313;
                    pos45.BorderThickness = l1133;
                    pos46.BorderThickness = l3351;
                    pos51.BorderThickness = l5131;
                    pos52.BorderThickness = l3133;
                    pos53.BorderThickness = l3113;
                    pos54.BorderThickness = l1313;
                    pos55.BorderThickness = l1331;
                    pos56.BorderThickness = l3151;
                    pos62.BorderThickness = l1315;
                    pos63.BorderThickness = l1315;
                    pos64.BorderThickness = l1315;
                    break;
                case int pos when pos == 14 || pos == 43:
                    pos14.Background = Brushes.LightGreen;
                    pos43.Background = Brushes.LightGreen;
                    pos11.BorderThickness = l5531;
                    pos12.BorderThickness = l3511;
                    pos13.BorderThickness = l1513;
                    pos14.BorderThickness = l1531;
                    pos15.BorderThickness = l3511;
                    pos22.BorderThickness = l1113;
                    pos23.BorderThickness = l1333;
                    pos24.BorderThickness = l3113;
                    pos25.BorderThickness = l1133;
                    pos26.BorderThickness = l3151;
                    pos31.BorderThickness = l5131;
                    pos32.BorderThickness = l3311;
                    pos33.BorderThickness = l1313;
                    pos34.BorderThickness = l1313;
                    pos35.BorderThickness = l1331;
                    pos36.BorderThickness = l3151;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3113;
                    pos43.BorderThickness = l1331;
                    pos44.BorderThickness = l3313;
                    pos45.BorderThickness = l1113;
                    pos46.BorderThickness = l1153;
                    pos51.BorderThickness = l5131;
                    pos52.BorderThickness = l3331;
                    pos53.BorderThickness = l3113;
                    pos54.BorderThickness = l1313;
                    pos55.BorderThickness = l1313;
                    pos56.BorderThickness = new Thickness(1, 3, 5, 3);
                    pos63.BorderThickness = l1315;
                    pos64.BorderThickness = l1315;
                    pos65.BorderThickness = l1315;
                    pos66.BorderThickness = l1355;
                    break;
                case int pos when pos == 23 || pos == 51:
                    pos23.Background = Brushes.LightGreen;
                    pos51.Background = Brushes.LightGreen;
                    pos11.BorderThickness = l5531;
                    pos12.BorderThickness = l3511;
                    pos13.BorderThickness = l1513;
                    pos14.BorderThickness = l1513;
                    pos21.BorderThickness = l5131;
                    pos22.BorderThickness = l3131;
                    pos23.BorderThickness = l3311;
                    pos24.BorderThickness = l1333;
                    pos25.BorderThickness = l3131;
                    pos26.BorderThickness = l3151;
                    pos32.BorderThickness = l1113;
                    pos33.BorderThickness = l1133;
                    pos34.BorderThickness = l3311;
                    pos35.BorderThickness = l1133;
                    pos36.BorderThickness = l3151;
                    pos41.BorderThickness = l5131;
                    pos42.BorderThickness = l3331;
                    pos43.BorderThickness = l3311;
                    pos44.BorderThickness = l1133;
                    pos45.BorderThickness = l3313;
                    pos51.BorderThickness = l5131;
                    pos52.BorderThickness = l3131;
                    pos53.BorderThickness = l3131;
                    pos54.BorderThickness = l3311;
                    pos55.BorderThickness = l1331;
                    pos56.BorderThickness = l3153;
                    pos62.BorderThickness = l1135;
                    pos63.BorderThickness = l3115;
                    pos64.BorderThickness = l1135;
                    pos65.BorderThickness = l3115;
                    pos66.BorderThickness = l1355;
                    break;
                default:
                    Thickness l1511 = new Thickness(1, 5, 1, 1);
                    Thickness l1111 = new Thickness(1, 1, 1, 1);
                    Thickness l1151 = new Thickness(1, 1, 5, 1);
                    Thickness l1115 = new Thickness(1, 1, 1, 5);
                    pos11.BorderThickness = new Thickness(5, 5, 1, 1);
                    pos12.BorderThickness = l1511;
                    pos13.BorderThickness = l1511;
                    pos14.BorderThickness = l1511;
                    pos15.BorderThickness = l1511;
                    pos16.BorderThickness = new Thickness(1, 5, 5, 1);
                    pos21.BorderThickness = l5111;
                    pos22.BorderThickness = l1111;
                    pos23.BorderThickness = l1111;
                    pos24.BorderThickness = l1111;
                    pos25.BorderThickness = l1111;
                    pos26.BorderThickness = l1151;
                    pos31.BorderThickness = l5111;
                    pos32.BorderThickness = l1111;
                    pos33.BorderThickness = l1111;
                    pos34.BorderThickness = l1111;
                    pos35.BorderThickness = l1111;
                    pos36.BorderThickness = l1151;
                    pos41.BorderThickness = l5111;
                    pos42.BorderThickness = l1111;
                    pos43.BorderThickness = l1111;
                    pos44.BorderThickness = l1111;
                    pos45.BorderThickness = l1111;
                    pos46.BorderThickness = l1151;
                    pos51.BorderThickness = l5111;
                    pos52.BorderThickness = l1111;
                    pos53.BorderThickness = l1111;
                    pos54.BorderThickness = l1111;
                    pos55.BorderThickness = l1111;
                    pos56.BorderThickness = l1151;
                    pos61.BorderThickness = new Thickness(5, 1, 1, 5);
                    pos62.BorderThickness = l1115;
                    pos63.BorderThickness = l1115;
                    pos64.BorderThickness = l1115;
                    pos65.BorderThickness = l1115;
                    pos66.BorderThickness = new Thickness(1, 1, 5, 5);
                    geht = false;
                    break;
            }

            return geht;
        }

        #endregion

        #region Passwd

        private List<string> words = new List<string>();
        private List<string> filter_first = new List<string>();
        private List<string> filter_second = new List<string>();
        private List<string> filter_third = new List<string>();
        private List<string> filter_fourth = new List<string>();
        private void btnPasswd_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 10;
                words.Add("angst");
                words.Add("atmen");
                words.Add("beten");
                words.Add("bombe");
                words.Add("danke");
                words.Add("draht");
                words.Add("druck");
                words.Add("drück");
                words.Add("farbe");
                words.Add("fehlt");
                words.Add("ferse");
                words.Add("kabel");
                words.Add("knall");
                words.Add("knapp");
                words.Add("knopf");
                words.Add("leere");
                words.Add("legal");
                words.Add("lehre");
                words.Add("mathe");
                words.Add("matte");
                words.Add("panik");
                words.Add("pieps");
                words.Add("rauch");
                words.Add("ruhig");
                words.Add("saite");
                words.Add("sehne");
                words.Add("seite");
                words.Add("sende");
                words.Add("strom");
                words.Add("super");
                words.Add("timer");
                words.Add("übrig");
                words.Add("verse");
                words.Add("warte");
                words.Add("zange");
                filter_first.Clear();
                filter_second.Clear();
                filter_third.Clear();
                filter_fourth.Clear();
                Zeile1.Text = "";
                Zeile2.Text = "";
                Zeile3.Text = "";
                Zeile4.Text = "";
                pass.Content = "";
                Zeile2.Visibility = Visibility.Hidden;
                Zeile3.Visibility = Visibility.Hidden;
                Zeile4.Visibility = Visibility.Hidden;
            }
        }
        private void Zeile1_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter_first.Clear();
            for (int i = 0; i < Zeile1.Text.Length; i++)
            {
                foreach (string word in words)
                {
                    if (word.Substring(0,1) == Zeile1.Text.Substring(i,1))
                    {
                        filter_first.Add(word);
                    }
                }
            }

            Zeile2.Visibility = Visibility.Visible;
        }

        private void Zeile2_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter_second.Clear();
            
            for (int i = 0; i < Zeile2.Text.Length; i++)
            {
                foreach (string word in filter_first)
                {
                    if (word.Substring(1, 1) == Zeile2.Text.Substring(i, 1))
                    {
                        filter_second.Add(word);
                    }
                }
            }

            Zeile3.Visibility = Visibility.Visible;
        }

        private void Zeile3_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter_third.Clear();
            
            for (int i = 0; i < Zeile3.Text.Length; i++)
            {
                foreach (string word in filter_second)
                {
                    if (word.Substring(2, 1) == Zeile3.Text.Substring(i, 1))
                    {
                        filter_third.Add(word);
                    }
                }
            }

            try
            {
                if (filter_third.Count == 1 || filter_third[0] == filter_third[1])
                {
                 pass.Content = filter_third[0];
                }
                else
                {
                    Zeile4.Visibility = Visibility.Visible;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Cool");
            }
           
           
        }

        private void Zeile4_TextChanged(object sender, TextChangedEventArgs e)
        {
            filter_fourth.Clear();
            for (int i = 0; i < Zeile4.Text.Length; i++)
            {
                foreach (string word in filter_third)
                {
                    if (word.Substring(3, 1) == Zeile4.Text.Substring(i, 1))
                    {
                        filter_fourth.Add(word);
                    }
                }
            }

            try
            {
                if (filter_fourth.Count == 1 || filter_fourth[0] == filter_fourth[1])
                {
                    pass.Content = filter_fourth[0];
                }
                else
                {
                    pass.Content = "Da war wohl ein Fehler";
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("cool");
            }
            
        }

        #endregion

        #region Quengel
        private void bt_submit_queng_Click(object sender, RoutedEventArgs e)
        {
            string input = "";
            if (sender == bt_submit_queng_main)
            {
                input = tb_quengel_main.Text;
                tb_quengel_main.Text = "";
            }
            else if (sender == bt_submit_queng_wire)
            {
                input = tb_quengel_wire.Text;
                tb_quengel_wire.Text = "";
            }
            else if (sender == bt_submit_queng_Button)
            {
                input = tb_quengel_Button.Text;
                tb_quengel_Button.Text = "";
            }
            else if (sender == bt_submit_queng_Symbol)
            {
                input = tb_quengel_Symbol.Text;
                tb_quengel_Symbol.Text = "";
            }
            else if (sender == bt_submit_queng_Simon)
            {
                input = tb_quengel_Simon.Text;
                tb_quengel_Simon.Text = "";
            }
            else if (sender == bt_submit_queng_words)
            {
                input = tb_quengel_words.Text;
                tb_quengel_words.Text = "";
            }
            else if (sender == bt_submit_queng_Memory)
            {
                input = tb_quengel_Memory.Text;
                tb_quengel_Memory.Text = "";
            }
            else if (sender == bt_submit_queng_Complicated)
            {
                input = tb_quengel_Complicated.Text;
                tb_quengel_Complicated.Text = "";
            }
            else if (sender == bt_submit_queng_sequence)
            {
                input = tb_quengel_sequence.Text;
                tb_quengel_sequence.Text = "";
            }
            else if (sender == bt_submit_queng_laby)
            {
                input = tb_quengel_laby.Text;
            }
        }


        #endregion

        
    }
}