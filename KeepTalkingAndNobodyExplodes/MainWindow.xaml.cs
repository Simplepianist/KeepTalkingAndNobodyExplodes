﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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
                    if (farben["r"] > 1 && !(bool)iseven)
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
                    if (seqence.Substring(seqencelength-1,1) == "s" && !(bool)iseven)
                    {
                        ret = "vierten Draht";
                    }else if (farben["r"] == 1 && farben["g"] > 1)
                    {
                        ret = "ersten Draht";
                    }else if (farben["s"] == 0)
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
                    if (farben["g"] == 0 && !(bool)iseven)
                    {
                        ret = "dritten Draht";
                    }else if (farben["g"] == 1 && farben["w"] > 1)
                    {
                        ret = "vierten Draht";
                    }else if (farben["r"] == 0)
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
            if ((bool)ch_blue.IsChecked && (bool)ch_abbrechen.IsChecked)
            {
                type = "gedrückt halten";
            }else if (batteries > 1 && (bool)ch_spreng.IsChecked)
            {
                type = "kurz drücken";
            }
            else if((bool)ch_white.IsChecked && (bool)car)
            {
                type = "gedrückt halten";
            }else if (batteries > 2 && (bool)frk)
            {
                type = "kurz drücken";
            }else if ((bool)ch_yellow.IsChecked)
            {
                type = "gedrückt halten";
            }else if ((bool)ch_red.IsChecked && (bool)ch_hold.IsChecked)
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
            if ((bool)ch_stripe_white.IsChecked)
            {
                ans += "1 ";
            }else if ((bool)ch_stripe_blue.IsChecked)
            {
                ans += "4 ";
            }else if ((bool)ch_stripe_yellow.IsChecked)
            {
                ans += "5 ";
            }else if ((bool)ch_stripe_other.IsChecked)
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

        private void btnSymbols_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 3;
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
            if ((bool)vokal)
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
        }


        #endregion

        #region Words

        private void btnWords_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 5;
        }

        #endregion

        #region Memory

        private void btnMemory_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 6;
        }

        #endregion

        #region Morse

        private void btnMorse_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 7;
        }

        #endregion

        #region Complicated

        private void btnComplicated_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 8;
        }

        #endregion

        #region Sequence

        private void btnSequence_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 9;
        }

        #endregion

        #region Labyrinth

        private void btnLabyrinth_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 10;
        }

        #endregion

        #region Passwd

        private void btnPasswd_Click(object sender, RoutedEventArgs e)
        {
            if (SwitchingTab()) TabControl.SelectedIndex = 11;
        }

        #endregion

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
        }

        private void btn_removeFehler_wires_Click(object sender, RoutedEventArgs e)
        {
            fehler--;
            if (TabControl.SelectedIndex == 4)
            {
                simonsays();
            }
        }
    }
}