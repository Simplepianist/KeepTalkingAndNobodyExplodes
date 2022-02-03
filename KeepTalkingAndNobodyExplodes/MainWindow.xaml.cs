using System;
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

        private int fehler = 0;

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
            if (TabControl.SelectedIndex == 4)
            {
                simonsays();
            }

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
            if (fehler < 0)
            {
                fehler = 0;
            }

            if (TabControl.SelectedIndex == 4)
            {
                simonsays();
            }

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
            lb_ans_wire.Visibility = Visibility.Hidden;
            lb_wire_cut.Visibility = Visibility.Hidden;
        }

        private void btn_send_wires_Click(object sender, RoutedEventArgs e)
        {
            string seqence = tb_wires.Text;
            int seqencelength = seqence.Length;
            bool lenright = false;
            string ret = "error";
            IDictionary<string, int> farben = new Dictionary<string, int>();
            farben["r"] = 0;
            farben["b"] = 0;
            farben["g"] = 0;
            farben["w"] = 0;
            farben["s"] = 0;
            for (int i = 0; i < seqencelength; i++)
            {
                int count = farben[seqence.Substring(i, 1)] + 1;
                farben[seqence.Substring(i, 1)] = count;
            }

            switch (seqencelength)
            {
                case 3:
                    lenright = true;
                    if (!seqence.Contains("r"))
                    {
                        ret = "2. Draht";
                    }
                    else if (seqence.Substring(seqencelength - 1, 1) == "w")
                    {
                        ret = "letzten Draht";
                    }
                    else if (farben["b"] > 1)
                    {
                        ret = "letzten blauen Draht";
                    }
                    else
                    {
                        ret = "letzten Draht";
                    }

                    break;
                case 4:
                    lenright = true;
                    if (farben["r"] > 1 && !(bool) iseven)
                    {
                        ret = "letzten roten Draht";
                    }
                    else if (seqence.Substring(seqencelength - 1, 1) == "g" && farben["r"] == 0 || farben["b"] == 1)
                    {
                        ret = "ersten Draht";
                    }
                    else if (farben["g"] > 1)
                    {
                        ret = "letzten Draht";
                    }
                    else
                    {
                        ret = "zweiten Draht";
                    }

                    break;
                case 5:
                    lenright = true;
                    if (seqence.Substring(seqencelength - 1, 1) == "s" && !(bool) iseven)
                    {
                        ret = "vierten Draht";
                    }
                    else if (farben["r"] == 1 && farben["g"] > 1)
                    {
                        ret = "ersten Draht";
                    }
                    else if (farben["s"] == 0)
                    {
                        ret = "zweiten Draht";
                    }
                    else
                    {
                        ret = "ersten Draht";
                    }

                    break;
                case 6:
                    lenright = true;
                    if (farben["g"] == 0 && !(bool) iseven)
                    {
                        ret = "dritten Draht";
                    }
                    else if (farben["g"] == 1 && farben["w"] > 1)
                    {
                        ret = "vierten Draht";
                    }
                    else if (farben["r"] == 0)
                    {
                        ret = "letzen Draht";
                    }
                    else
                    {
                        ret = "vierten Draht";
                    }

                    break;
            }

            if (!lenright)
            {
                MessageBox.Show("Keine richtige Anzahl der Drähte: " + seqencelength, "Anzahl der Drähte");
            }

            lb_ans_wire.Visibility = Visibility.Visible;
            lb_wire_cut.Visibility = Visibility.Visible;
            lb_wire_cut.Content = ret;
        }

        #endregion

        #region Button

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 2;
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
            string type = "";
            if ((bool) ch_blue.IsChecked && (bool) ch_abbrechen.IsChecked)
            {
                type = "gedrückt halten";
            }
            else if (batteries > 1 && (bool) ch_spreng.IsChecked)
            {
                type = "kurz drücken";
            }
            else if ((bool) ch_white.IsChecked && (bool) car)
            {
                type = "gedrückt halten";
            }
            else if (batteries > 2 && (bool) frk)
            {
                type = "kurz drücken";
            }
            else if ((bool) ch_yellow.IsChecked)
            {
                type = "gedrückt halten";
            }
            else if ((bool) ch_red.IsChecked && (bool) ch_hold.IsChecked)
            {
                type = "kurz drücken";
            }
            else
            {
                type = "gedrückt halten";
            }

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
            string ans = "Loslassen, wenn Timer eine ";
            if ((bool) ch_stripe_white.IsChecked)
            {
                ans += "1 ";
            }
            else if ((bool) ch_stripe_blue.IsChecked)
            {
                ans += "4 ";
            }
            else if ((bool) ch_stripe_yellow.IsChecked)
            {
                ans += "5 ";
            }
            else if ((bool) ch_stripe_other.IsChecked)
            {
                ans += "1 ";
            }
            else
            {
                MessageBox.Show("Bitte eine Farbe von Streifen auswählen", "Error with Stripes");
            }

            lb_ans_stripes.Content = ans + "anzeigt";
        }

        #endregion

        #region Symbols

        int counter = 0;
        int[] pics = new int[] {50, 50, 50, 50};

        String[] symbuttons = new[]
        {
            "sechs", "AT", "NBogen", "OStrich", "Omega", "Absatz", "ae", "alien", "weird3", "bT", "WKomma", "Bahn",
            "CLoop", "broken3", "dotedC", "Copyright", "HCurly", "NCurly", "EDoted", "backCDoted", "empStar",
            "fullStar", "question", "MirrorK", "Candelight", "Smiley", "Y"
        };

        private String[] reihe1 = new[] {"OStrich", "AT", "Y", "NCurly", "alien", "HCurly", "backCDoted"};
        private String[] reihe2 = new[] {"EDoted", "OStrich", "backCDoted", "CLoop", "empStar", "HCurly", "question"};
        private String[] reihe3 = new[] {"Copyright", "WKomma", "CLoop", "MirrorK", "broken3", "Y", "empStar"};
        private String[] reihe4 = new[] {"sechs", "Absatz", "bT", "alien", "MirrorK", "question", "Smiley"};
        private String[] reihe5 = new[] {"Candelight", "Smiley", "bT", "dotedC", "Absatz", "weird3", "fullStar"};
        private String[] reihe6 = new[] {"sechs", "EDoted", "Bahn", "ae", "Candelight", "NBogen", "Omega"};

        private String[] pngs = new[]
        {
            "6.PNG", "AT.PNG", "N_mit_bogen.PNG", "O_mit_Strich.PNG", "Omega.PNG", "absatz.PNG", "ae.PNG", "alien.PNG",
            "alien_3.PNG", "bT.PNG", "w_mit_komma.PNG", "bahnübergang.PNG", "c_looping.PNG", "broken_3.PNG",
            "c_mit_punkt.PNG", "copyright.PNG", "curly_H.PNG", "curly_N.PNG", "e_mit_punkten.PNG",
            "umgekehrte_C_mit_punkt.PNG", "empty_star.PNG", "filled_star.PNG", "espaniol_fragezeichen.PNG",
            "k_spiegel.PNG", "kerzenständer.PNG", "smiley.PNG", "umgekehrtes_Y.PNG"
        };

        private IDictionary<string, string> path = new Dictionary<string, string>();

        private void btnSymbols_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                path.Clear();
                TabControl.SelectedIndex = 3;
                counter = 0;
                for (int i = 0; i < pics.Length; i++)
                {
                    pics[i] = 50;
                }

                for (int i = 0; i < symbuttons.Length; i++)
                {
                    string pfad = "Bilder/Symbols/" + pngs[i];
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
                {
                    pics[counter] = 0;
                }
                else if (sender == AT)
                {
                    pics[counter] = 1;
                }
                else if (sender == NBogen)
                {
                    pics[counter] = 2;
                }
                else if (sender == OStrich)
                {
                    pics[counter] = 3;
                }
                else if (sender == Omega)
                {
                    pics[counter] = 4;
                }
                else if (sender == Absatz)
                {
                    pics[counter] = 5;
                }
                else if (sender == ae)
                {
                    pics[counter] = 6;
                }
                else if (sender == alien)
                {
                    pics[counter] = 7;
                }
                else if (sender == weird3)
                {
                    pics[counter] = 8;
                }
                else if (sender == bT)
                {
                    pics[counter] = 9;
                }
                else if (sender == WKomma)
                {
                    pics[counter] = 10;
                }
                else if (sender == Bahn)
                {
                    pics[counter] = 11;
                }
                else if (sender == CLoop)
                {
                    pics[counter] = 12;
                }
                else if (sender == broken3)
                {
                    pics[counter] = 13;
                }
                else if (sender == dotedC)
                {
                    pics[counter] = 14;
                }
                else if (sender == Copyright)
                {
                    pics[counter] = 15;
                }
                else if (sender == HCurly)
                {
                    pics[counter] = 16;
                }
                else if (sender == NCurly)
                {
                    pics[counter] = 17;
                }
                else if (sender == EDoted)
                {
                    pics[counter] = 18;
                }
                else if (sender == backCDoted)
                {
                    pics[counter] = 19;
                }
                else if (sender == empStar)
                {
                    pics[counter] = 20;
                }
                else if (sender == fullStar)
                {
                    pics[counter] = 21;
                }
                else if (sender == question)
                {
                    pics[counter] = 22;
                }
                else if (sender == MirrorK)
                {
                    pics[counter] = 23;
                }
                else if (sender == Candelight)
                {
                    pics[counter] = 24;
                }
                else if (sender == Smiley)
                {
                    pics[counter] = 25;
                }
                else if (sender == Y)
                {
                    pics[counter] = 26;
                }

                counter++;
            }
        }

        private void btn_Solve_Click(object sender, RoutedEventArgs e)
        {
            int found1 = 50;
            int found2 = 50;
            int found3 = 50;
            int found4 = 50;
            int reihe = 0;
            if (!pics.Contains(50))
            {
                if (reihe1.Contains(symbuttons[pics[0]]) && reihe1.Contains(symbuttons[pics[1]]) &&
                    reihe1.Contains(symbuttons[pics[2]]) && reihe1.Contains(symbuttons[pics[3]]))
                {
                    reihe = 1;
                    for (int i = 0; i < reihe1.Length; i++)
                    {
                        if (reihe1[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe1[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe1[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe1[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }
                else if (reihe2.Contains(symbuttons[pics[0]]) && reihe2.Contains(symbuttons[pics[1]]) &&
                         reihe2.Contains(symbuttons[pics[2]]) && reihe2.Contains(symbuttons[pics[3]]))
                {
                    reihe = 2;
                    for (int i = 0; i < reihe2.Length; i++)
                    {
                        if (reihe2[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe2[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe2[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe2[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }
                else if (reihe3.Contains(symbuttons[pics[0]]) && reihe3.Contains(symbuttons[pics[1]]) &&
                         reihe3.Contains(symbuttons[pics[2]]) && reihe3.Contains(symbuttons[pics[3]]))
                {
                    reihe = 3;
                    for (int i = 0; i < reihe3.Length; i++)
                    {
                        if (reihe3[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe3[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe3[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe3[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }
                else if (reihe4.Contains(symbuttons[pics[0]]) && reihe4.Contains(symbuttons[pics[1]]) &&
                         reihe4.Contains(symbuttons[pics[2]]) && reihe4.Contains(symbuttons[pics[3]]))
                {
                    reihe = 4;
                    for (int i = 0; i < reihe4.Length; i++)
                    {
                        if (reihe4[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe4[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe4[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe4[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }
                else if (reihe5.Contains(symbuttons[pics[0]]) && reihe5.Contains(symbuttons[pics[1]]) &&
                         reihe5.Contains(symbuttons[pics[2]]) && reihe5.Contains(symbuttons[pics[3]]))
                {
                    reihe = 5;
                    for (int i = 0; i < reihe5.Length; i++)
                    {
                        if (reihe5[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe5[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe5[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe5[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }
                else if (reihe6.Contains(symbuttons[pics[0]]) && reihe6.Contains(symbuttons[pics[1]]) &&
                         reihe6.Contains(symbuttons[pics[2]]) && reihe6.Contains(symbuttons[pics[3]]))
                {
                    reihe = 6;
                    for (int i = 0; i < reihe6.Length; i++)
                    {
                        if (reihe6[i] == symbuttons[pics[0]])
                        {
                            found1 = i;
                        }

                        if (reihe6[i] == symbuttons[pics[1]])
                        {
                            found2 = i;
                        }

                        if (reihe6[i] == symbuttons[pics[2]])
                        {
                            found3 = i;
                        }

                        if (reihe6[i] == symbuttons[pics[3]])
                        {
                            found4 = i;
                        }
                    }
                }

                int[] founds = new[] {found1, found2, found3, found4};
                string ausgabe = "";
                foreach (var VARIABLE in founds)
                {
                    ausgabe += VARIABLE.ToString() + " ";
                }

                MessageBox.Show(ausgabe);
                int length = founds.Length;

                int temp = founds[0];

                for (int i = 0; i < length; i++)
                {
                    for (int j = i + 1; j < length; j++)
                    {
                        if (founds[i] > founds[j])
                        {
                            temp = founds[i];
                            founds[i] = founds[j];
                            founds[j] = temp;
                        }
                    }
                }

                ausgabe = "";
                foreach (var VARIABLE in founds)
                {
                    ausgabe += VARIABLE.ToString() + " ";
                }

                MessageBox.Show(ausgabe);

                BitmapImage b1 = new BitmapImage();
                BitmapImage b2 = new BitmapImage();
                BitmapImage b3 = new BitmapImage();
                BitmapImage b4 = new BitmapImage();
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
            {
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
            }
            else
            {
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
            }

            lb_errors.Content = fehler.ToString() + ". Fehler";
        }

        #endregion

        #region Words

        IDictionary<string, string> kette = new Dictionary<string, string>();


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
            foreach (string keys in kette.Keys)
            {
                Key_inhalt.Items.Add(keys);
            }

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

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string inhalt = (string) Display_inhalt.SelectedValue;
            BitmapImage pic = new BitmapImage();
            Uri uripic = new Uri("Bilder/Words/eyes.png", UriKind.Relative);
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

        private int stufe = 0;

        //                          { Zahl, Position }
        private int[] durch1 = new[] {0, 0};
        private int[] durch2 = new[] {0, 0};
        private int[] durch3 = new[] {0, 0};
        private int[] durch4 = new[] {0, 0};
        private int[] durch5 = new[] {0, 0};
        private bool changed = false;
        string search = "";

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
                lb_memory_durchgang.Content = (stufe + 1).ToString() + ". Druchgang";
            }
        }

        private void btn_check_memory_Click(object sender, RoutedEventArgs e)
        {
            if (stufe < 5)
            {
                int num = 0;
                int pos;
                string ausgabe = "";
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
                        }
                    }
                    else
                    {
                        if (changed)
                        {
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
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                        case 3:
                                            ausgabe = "1. Position";
                                            durch2[1] = 2;
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch1[1];
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                    }

                                    break;
                                case 2:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch2[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                        case 2:
                                            pos = durch1[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                        case 3:
                                            ausgabe = "3. Position";
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch1[1];
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                    }

                                    break;
                                case 3:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch1[1];
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                        case 2:
                                            ausgabe = "1. Position";
                                            search = "Number";
                                            break;
                                        case 3:
                                            pos = durch2[1];
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                        case 4:
                                            pos = durch3[1];
                                            ausgabe = pos.ToString() + ". Position";
                                            search = "Number";
                                            break;
                                    }

                                    break;
                                case 4:
                                    switch (num)
                                    {
                                        case 1:
                                            pos = durch1[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                        case 2:
                                            pos = durch2[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                        case 3:
                                            pos = durch4[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                        case 4:
                                            pos = durch3[0];
                                            ausgabe = "Beschriftung " + pos.ToString();
                                            search = "Position";
                                            break;
                                    }

                                    break;
                            }
                        }
                    }

                    lb_answer.Content = ausgabe;
                    if (changed || stufe == 0)
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
                    else
                    {
                        MessageBox.Show("Fehler im Code schau nochmal nach", "Error");
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
            }
        }

        #endregion


        #region Complicated

        private void btnComplicated_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 8;
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
            string ans = "";
            if ((bool)compli_stern.IsChecked)
            {
                if ((bool)compli_led.IsChecked)
                {
                    if ((bool)compli_blue.IsChecked)
                    {
                        if ((bool)compli_red.IsChecked)
                        {
                            ans = "N";
                        }
                        else
                        {
                            ans = "P";
                        }
                    }
                    else if ((bool)compli_red.IsChecked)
                    {
                        ans = "B";
                    }
                    else if ((bool)compli_white.IsChecked)
                    {
                        ans = "B";
                    }
                }
                else
                {
                    if ((bool)compli_blue.IsChecked)
                    {
                        if ((bool)compli_red.IsChecked)
                        {
                            ans = "P";
                        }
                        else
                        {
                            ans = "N";
                        }
                    }
                    else if ((bool)compli_red.IsChecked)
                    {
                        ans = "D";
                    }
                    else if ((bool)compli_white.IsChecked)
                    {
                        ans = "D";
                    }
                }
            }
            else
            {
                if ((bool)compli_led.IsChecked)
                {
                    if ((bool)compli_blue.IsChecked)
                    {
                        if ((bool)compli_red.IsChecked)
                        {
                            ans = "S";
                        }
                        else
                        {
                            ans = "P";
                        }
                    }
                    else if ((bool)compli_red.IsChecked)
                    {
                        ans = "B";
                    }
                    else if((bool)compli_white.IsChecked)
                    {
                        ans = "N";
                    }
                }
                else
                {
                    if ((bool)compli_blue.IsChecked)
                    {
                        if ((bool)compli_red.IsChecked)
                        {
                            ans = "S";
                        }
                        else
                        {
                            ans = "S";
                        }
                    }
                    else if ((bool)compli_red.IsChecked)
                    {
                        ans = "S";
                    }
                    else if ((bool)compli_white.IsChecked)
                    {
                        ans = "D";
                    }
                }
            }

            string cut = "";
            if (ans == "D")
            {
                cut = "Cut it";
            }else if (ans == "N")
            {
                cut = "Don't cut it";
            }else if (ans == "S")
            {
                if ((bool)iseven)
                {
                    cut = "Cut it";
                }
                else
                {
                    cut = "Don't cut it";
                }
            }else if (ans == "P")
            {
                if ((bool)parralel)
                {
                    cut = "Cut it";
                }
                else
                {
                    cut = "Don't cut it";
                }
            }
            else if (ans == "B")
            {
                if (batteries >= 2)
                {
                    cut = "Cut it";
                }
                else
                {
                    cut = "Don't cut it";
                }
            }
            lb_comli_cut.Content = cut;
        }

        #endregion

        #region Sequence

        private int blue = 0;
        private int red = 0;
        private int black = 0;

        private void btnSequence_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 9;
                blue = 0;
                red = 0;
                black = 0;
                CutOrNot.Content = "";
                CutOrNot_Label.Content = "";
            }
        }

        private void Complicated_Blue_Click(object sender, RoutedEventArgs e)
        {
            string content = "Cut wenn ";
            blue++;
            bool cases = false;
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
            {
                CutOrNot.Content = content;
            }
            else
            {
                CutOrNot.Content = "No more Wires";
            }
        }

        private void Complicated_Red_Click(object sender, RoutedEventArgs e)
        {
            string content = "Cut wenn ";
            red++;
            bool cases = false;
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
            {
                CutOrNot.Content = content;
            }
            else
            {
                CutOrNot.Content = "No more Wires";
            }
        }

        private void Complicated_Black_Click(object sender, RoutedEventArgs e)
        {
            string content = "Cut wenn ";
            black++;
            bool cases = false;
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
            {
                CutOrNot.Content = content;
            }
            else
            {
                CutOrNot.Content = "No more Wires";
            }
        }

        #endregion 

        // Noch nicht done
        #region Labyrinth

        private void btnLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab())
            {
                TabControl.SelectedIndex = 9;
                buildLabyrinth(0);
                buildLabyrinth(21);

            }
        }


        void buildLabyrinth(int layout)
        {
            switch (layout)
            {
                case int pos when (pos == 21 || pos == 36):
                    pos21.Background = Brushes.LightGreen;
                    pos36.Background = Brushes.LightGreen;
                    pos12.BorderThickness = new Thickness(0, 5, 0, 3);
                    pos13.BorderThickness = new Thickness(1, 5, 3, 1);
                    pos14.BorderThickness = new Thickness(3, 5, 1, 1);
                    pos15.BorderThickness = new Thickness(1, 5, 1, 3);
                    pos16.BorderThickness = new Thickness(1, 5, 5, 3);
                    pos21.BorderThickness = new Thickness(5, 1, 3, 1);
                    pos22.BorderThickness = new Thickness(3, 3, 1, 1);
                    pos23.BorderThickness = new Thickness(1, 1, 3, 3);
                    pos24.BorderThickness = new Thickness(3, 1, 1, 3);
                    pos25.BorderThickness = new Thickness(1, 3, 1, 3);
                    pos26.BorderThickness = new Thickness(1, 3, 5, 1);

                    break;
                case int pos when (pos == 25 || pos == 42):
                    
                    break;
                case int pos when (pos == 44 || pos == 46):
                    
                    break;
                case int pos when (pos == 11 || pos == 41):
                    
                    break;
                case int pos when (pos == 35 || pos == 64):
                    
                    break;
                case int pos when (pos == 15 || pos == 53):
                    
                    break;
                case int pos when (pos == 12 || pos == 62):
                    break;
                case int pos when (pos == 14 || pos == 43):
                    break;
                case int pos when (pos == 23 || pos == 51):
                default:
                    pos11.BorderThickness = new Thickness(5, 5, 1, 1);
                    pos12.BorderThickness = new Thickness(1, 5, 1, 1);
                    pos13.BorderThickness = new Thickness(1, 5, 1, 1);
                    pos14.BorderThickness = new Thickness(1, 5, 1, 1);
                    pos15.BorderThickness = new Thickness(1, 5, 1, 1);
                    pos16.BorderThickness = new Thickness(1, 5, 5, 1);
                    pos21.BorderThickness = new Thickness(5, 1, 1, 1);
                    pos22.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos23.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos24.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos25.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos26.BorderThickness = new Thickness(1, 1, 5, 1);
                    pos31.BorderThickness = new Thickness(5, 1, 1, 1);
                    pos32.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos33.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos34.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos35.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos36.BorderThickness = new Thickness(1, 1, 5, 1);
                    pos41.BorderThickness = new Thickness(5, 1, 1, 1);
                    pos42.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos43.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos44.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos45.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos46.BorderThickness = new Thickness(1, 1, 5, 1);
                    pos51.BorderThickness = new Thickness(5, 1, 1, 1);
                    pos52.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos53.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos54.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos55.BorderThickness = new Thickness(1, 1, 1, 1);
                    pos56.BorderThickness = new Thickness(1, 1, 5, 1);
                    pos61.BorderThickness = new Thickness(5, 1, 1, 5);
                    pos62.BorderThickness = new Thickness(1, 1, 1, 5);
                    pos63.BorderThickness = new Thickness(1, 1, 1, 5);
                    pos64.BorderThickness = new Thickness(1, 1, 1, 5);
                    pos65.BorderThickness = new Thickness(1, 1, 1, 5);
                    pos66.BorderThickness = new Thickness(1, 1, 5, 5);
                    break;
            }
        }
        #endregion

        // Noch nicht done
        #region Passwd

        private void btnPasswd_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 10;
        }



        #endregion

    }
}