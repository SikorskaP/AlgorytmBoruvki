using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AlgorytmBoruvki
{
    public partial class AlgorytmBoruvki : Form
    {
        int LiczbaWierzchołków;
        int[,] Macierz, MacierzKońcowa;
        const short BRAK_DROGI = short.MaxValue;
        public AlgorytmBoruvki()
        {
            InitializeComponent();
        }

        private void TxtLiczbaWierzchołków_KeyPress(object sender, KeyPressEventArgs e)
        {
            // można wpisać jedynie liczby (int) do textboxa
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void BtnStworzenieMacierzy_Click(object sender, EventArgs e)
        {
            StworzenieMacierzy(DgvSzukanaMacierz);
            StworzenieMacierzy(DgvWynik);
        }
        public void StworzenieMacierzy(DataGridView DataGrid)
        {
            DataGrid.BackgroundColor = Color.DarkGray;


            // zdefiniowanie formatowania nagłówków w kontrolce DataGridView
            DataGridViewCellStyle StylHeader = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 10, FontStyle.Bold),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                Format = " ",
                BackColor = Color.DarkGray,
                ForeColor = Color.LightGray
            };

            // zdefiniowanie formatowania komórek w kontrolce DataGridView
            DataGridViewCellStyle StylKomórek = new DataGridViewCellStyle
            {
                Font = new Font("Arial", 10, FontStyle.Italic),
                Alignment = DataGridViewContentAlignment.MiddleCenter,
                BackColor = Color.LightGray,
                ForeColor = Color.DarkGray
            };

            // zgaszenie kontrolki errorProvider1
            ErrorProvider1.Dispose();
            // pobranie liczby wierzchołków grafu
            if (!int.TryParse(TxtLiczbaWierzchołków.Text, out LiczbaWierzchołków))
            {
                ErrorProvider1.SetError(TxtLiczbaWierzchołków, "ERROR: wpisz liczbę wierzchołków.");
                return;
            }
            if ((LiczbaWierzchołków == 0) || LiczbaWierzchołków.Equals(""))
            {
                ErrorProvider1.SetError(TxtLiczbaWierzchołków, "ERROR: Liczba wierzchołków musi być większa od zera.");
                return;
            }
            DataGrid.AutoResizeRows();
            DataGrid.ColumnCount = LiczbaWierzchołków;
            DataGrid.RowCount = LiczbaWierzchołków;

            DataGrid.DefaultCellStyle = StylKomórek;
            DataGrid.ColumnHeadersDefaultCellStyle = StylHeader;
            DataGrid.RowHeadersDefaultCellStyle = StylHeader;
            DataGrid.ColumnHeadersVisible = true;
            DataGrid.RowHeadersVisible = true;
            DataGrid.ReadOnly = false;
            DataGrid.RowTemplate.MinimumHeight = 25;


            ErrorProvider1.Dispose();
            // wpisanie numerów kolumn do komórek DataGrid
            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                DataGrid.Columns[i].HeaderText = "( " + i + " )";
            }

            // wpisanie numerów wierszy w Headerze wierszy
            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                DataGrid.Rows[i].HeaderCell.Value = "( " + i + " )";
            }

            // ustawienie autoresize dla dgv
            DataGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            DataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.ColumnHeader;
            DataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            DataGrid.AutoResizeColumnHeadersHeight();
            DataGrid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            DataGrid.ScrollBars = ScrollBars.Both;

        }
        private void BtnLosoweWagi_Click(object sender, EventArgs e)
        {
            Random Rnd = new Random();
            Macierz = new int[LiczbaWierzchołków, LiczbaWierzchołków];
            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                for (int j = i + 1; j < LiczbaWierzchołków; j++)
                {
                    if (Rnd.NextDouble() < 0.70)
                    {
                        Macierz[i, j] = Rnd.Next(20);
                        DgvSzukanaMacierz.Rows[i].Cells[j].Value = Macierz[i, j];
                        //dgvSzukanaMacierz.Rows[i].Cells[j].Value = Rnd.Next(10) + 1;
                        if (Macierz[i, j] == 0)
                        {
                            DgvSzukanaMacierz.Rows[j].Cells[i].Value = "";
                        }
                        else
                        {
                            DgvSzukanaMacierz.Rows[j].Cells[i].Value = DgvSzukanaMacierz.Rows[i].Cells[j].Value;
                        }

                        Debug.Write(i + ", " + j + ": " + Macierz[i, j] + "\n");
                    }
                    else
                    {
                        Macierz[i, j] = BRAK_DROGI;
                        DgvSzukanaMacierz.Rows[j].Cells[i].Value = "";
                    }
                }
            }

            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    Macierz[j, i] = Macierz[i, j];
                }
            }
        }
        private void DgvSzukanaMacierz_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            // wierzchołki nie mogą się odnosić do samych siebie
            if (e.ColumnIndex == e.RowIndex)
            {
                DgvSzukanaMacierz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 0;
            }

        }
        private void DgvSzukanaMacierz_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            // graf jest nieskierowany - macierz musi być lustrzanie identyczna
            DgvSzukanaMacierz.Rows[e.ColumnIndex].Cells[e.RowIndex].Value = DgvSzukanaMacierz.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
        }
        private int[,] Algorytm()
        {
            MacierzKońcowa = new int[LiczbaWierzchołków, LiczbaWierzchołków];
            int[,] MacierzPar = new int[LiczbaWierzchołków, 2];
            const int PRZERWA = int.MaxValue;
            int[] ZbieranieLiczbyElementów = new int[(LiczbaWierzchołków * 2) + 1];
            int x = 0;
            int NajmniejszaWaga;
            TxtListaSąsiedztwa.Text = "";

            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    MacierzKońcowa[i, j] = BRAK_DROGI;
                    if (Macierz[i, j] == 0)
                    {
                        Macierz[i, j] = BRAK_DROGI;
                    }
                }
            }

            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                NajmniejszaWaga = Macierz[i, 0];
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    if (WybórKrawędzi(Macierz[i, j], NajmniejszaWaga))
                    {
                        NajmniejszaWaga = Macierz[i, j];
                    }
                }
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    if (NajmniejszaWaga == Macierz[i, j])
                    {
                        MacierzKońcowa[i, j] = NajmniejszaWaga;
                        DodajTekst("Najmniejsza waga elementu " + i + " - z elementem " + j + " o wartości " + NajmniejszaWaga + ".");
                        NajmniejszaWaga = -1;
                    }
                }
            }
            int Grupa = 0;
            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    if (MacierzKońcowa[i, j] != BRAK_DROGI)
                    {
                        MacierzPar[i, 0] = i;
                        MacierzPar[i, 1] = j;
                        int Index;
                        if (x == 0)
                        {
                            Index = x;
                            ZbieranieLiczbyElementów[Index] = i;
                            ZbieranieLiczbyElementów[Index + 1] = j;
                            ZbieranieLiczbyElementów[Index + 2] = PRZERWA;
                            x += 3; Grupa++;
                        }
                        else if (ZbieranieLiczbyElementów.Contains(i) || ZbieranieLiczbyElementów.Contains(j))
                        {
                            if (ZbieranieLiczbyElementów.Contains(i) && ZbieranieLiczbyElementów.Contains(j))
                            {
                                Index = ZbieranieLiczbyElementów.FindIndex(i);
                                int Index2 = ZbieranieLiczbyElementów.FindIndex(j);
                                int MinGranicaWiększego = 0, MaxGranicaMniejszego = 0, MaxGranicaWiększego = 0;
                                if ((Index != Index2) && (Grupa > 0) && (!(Math.Abs(Index - Index2) == 1)))
                                {
                                    int MniejszyIndex = Math.Min(Index, Index2);
                                    int WiększyIndex = Math.Max(Index, Index2);
                                    for (int l = WiększyIndex; l >= 0; l--)
                                    {
                                        if (ZbieranieLiczbyElementów[l] == int.MaxValue)
                                        {
                                            MinGranicaWiększego = l;
                                            break;
                                        }
                                    }

                                    for (int l = WiększyIndex; l < ZbieranieLiczbyElementów.Length - 1; l++)
                                    {
                                        if (ZbieranieLiczbyElementów[l] == int.MaxValue)
                                        {
                                            MaxGranicaWiększego = l;
                                            break;
                                        }
                                    }

                                    for (int l = MniejszyIndex; l < ZbieranieLiczbyElementów.Length - 1; l++)
                                    {
                                        if (ZbieranieLiczbyElementów[l] == int.MaxValue)
                                        {
                                            MaxGranicaMniejszego = l;
                                            break;
                                        }
                                    }

                                    if (MaxGranicaMniejszego == MaxGranicaWiększego)
                                    {
                                        break;
                                    }

                                    int LiczbaElementówPóźniejszejGrupy = MaxGranicaWiększego - MinGranicaWiększego - 1;
                                    for (int l = ZbieranieLiczbyElementów.Length - 1 - LiczbaElementówPóźniejszejGrupy; l >= MaxGranicaMniejszego; l--)
                                        ZbieranieLiczbyElementów[l + LiczbaElementówPóźniejszejGrupy] = ZbieranieLiczbyElementów[l];
                                    for (int t = 0; t <= LiczbaElementówPóźniejszejGrupy; t++)
                                        ZbieranieLiczbyElementów[t + MaxGranicaMniejszego] = ZbieranieLiczbyElementów[MinGranicaWiększego + LiczbaElementówPóźniejszejGrupy + 1 + t];
                                    for (int t = MinGranicaWiększego + 1 + LiczbaElementówPóźniejszejGrupy; t <= ZbieranieLiczbyElementów.Length - 1; t++)
                                        if (t + LiczbaElementówPóźniejszejGrupy + 1 > ZbieranieLiczbyElementów.Length - 1)
                                            ZbieranieLiczbyElementów[t] = 0;
                                        else
                                            ZbieranieLiczbyElementów[t] = ZbieranieLiczbyElementów[t + LiczbaElementówPóźniejszejGrupy + 1];
                                    Grupa--; x--;
                                }
                            }
                            else if (ZbieranieLiczbyElementów.Contains(i))
                            {
                                int maxGranica = 0;
                                Index = ZbieranieLiczbyElementów.FindIndex(i);
                                for (int l = Index; l < ZbieranieLiczbyElementów.Length - 1; l++)
                                {
                                    if (ZbieranieLiczbyElementów[l] == int.MaxValue)
                                    {
                                        maxGranica = l;
                                        break;
                                    }
                                }

                                for (int l = ZbieranieLiczbyElementów.Length - 2; l > maxGranica; l--)
                                {
                                    ZbieranieLiczbyElementów[l + 1] = ZbieranieLiczbyElementów[l];
                                }

                                ZbieranieLiczbyElementów[maxGranica] = j;
                                ZbieranieLiczbyElementów[maxGranica + 1] = PRZERWA;
                                x += 1;
                            }
                            else
                            {
                                int maxGranica = 0;
                                Index = ZbieranieLiczbyElementów.FindIndex(j);
                                for (int l = Index; l < ZbieranieLiczbyElementów.Length - 1; l++)
                                {
                                    if (ZbieranieLiczbyElementów[l] == int.MaxValue)
                                    {
                                        maxGranica = l;
                                        break;
                                    }
                                }

                                for (int l = ZbieranieLiczbyElementów.Length - 2; l > maxGranica; l--)
                                {
                                    ZbieranieLiczbyElementów[l + 1] = ZbieranieLiczbyElementów[l];
                                }

                                ZbieranieLiczbyElementów[maxGranica] = i;
                                ZbieranieLiczbyElementów[maxGranica + 1] = PRZERWA;
                                x += 1;
                            }
                        }
                        else
                        {
                            Index = x;
                            ZbieranieLiczbyElementów[Index] = i;
                            ZbieranieLiczbyElementów[Index + 1] = j;
                            ZbieranieLiczbyElementów[Index + 2] = PRZERWA;
                            x += 3; Grupa++;
                        }
                    }
                }
            }

            int[][] MacierzGrup = new int[Grupa][];

            int DolnaGranica = 0, GórnaGranica = 0, NumerGrupy = 0;
            for (int j = 0; j < Grupa; j++)
            {
                for (int i = GórnaGranica + 1; i <= ZbieranieLiczbyElementów.Length - 1; i++)
                {
                    if (ZbieranieLiczbyElementów[i] == PRZERWA)
                    {
                        GórnaGranica = i;
                        break;
                    }
                }
                if (NumerGrupy > 0)
                {
                    MacierzGrup[NumerGrupy] = new int[GórnaGranica - DolnaGranica - 1];
                }
                else
                {
                    MacierzGrup[NumerGrupy] = new int[GórnaGranica - DolnaGranica];
                }

                DolnaGranica = GórnaGranica;
                NumerGrupy++;

            }
            if (MacierzGrup.Length == 1)
            {
                return MacierzKońcowa;
            }

            int NowyIndex = 0;
            NumerGrupy = 0;
            for (int i = 0; i < ZbieranieLiczbyElementów.Length - 1; i++)
            {
                if ((ZbieranieLiczbyElementów[i] == 0) && (i != 0))
                    break;
                if (NumerGrupy < MacierzGrup.Length)
                {
                    if (NowyIndex == MacierzGrup[NumerGrupy].Length)
                    {
                        NumerGrupy++;
                        NowyIndex = 0;
                    }
                    else if (ZbieranieLiczbyElementów[i] == PRZERWA)
                    {
                        NowyIndex++;
                    }
                    else
                    {
                        MacierzGrup[NumerGrupy][NowyIndex] = ZbieranieLiczbyElementów[i];
                        NowyIndex++;
                    }
                }
                else
                {
                    // ERROR
                    NumerGrupy = 0;
                }
            }

            for (int i = 0; i < MacierzGrup.Length; i++)
            {
                TxtListaSąsiedztwa.AppendText("Elementy połączyły się w grupę " + (i + 1) + ": \t");

                for (int j = 0; j < MacierzGrup[i].Length; j++)
                    if (j == MacierzGrup[i].Length - 1)
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[i][j] + ".");
                    else
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[i][j] + ",  ");
                TxtListaSąsiedztwa.AppendText(Environment.NewLine);

            }

            SzukanieWTablicy(MacierzGrup, MacierzKońcowa, Macierz);
            return MacierzKońcowa;




        }
        private void SzukanieWTablicy(int[][] MacierzGrup, int[,] MacierzKońcowa, int[,] Macierz)
        {
            int[,] MacierzPar = new int[MacierzGrup.Length, 2];

            for (int Grupa = 0; Grupa < MacierzGrup.Length; Grupa++)
            {
                int NajmniejszaWaga = int.MaxValue;
                int tymczasoweI = 0;
                int tymczasowyElementWMacierzyKońcowej = 0;

                bool[] ElementNależyDoTejSamejGrupy = new bool[LiczbaWierzchołków];
                for (int Element = 0; Element < MacierzGrup[Grupa].Length; Element++)
                    for (int i = 0; i < LiczbaWierzchołków; i++)
                    {
                       if (i == MacierzGrup[Grupa][Element])
                        {
                            ElementNależyDoTejSamejGrupy[i] = true;
                            break;
                        }
                    }
 
                for (int Element = 0; Element < MacierzGrup[Grupa].Length; Element++)
                    for (int i = 0; i < LiczbaWierzchołków; i++)
                        if (ElementNależyDoTejSamejGrupy[i] == false)
                        {
                            if (WybórKrawędzi(Macierz[MacierzGrup[Grupa][Element], i], NajmniejszaWaga))
                            {
                                NajmniejszaWaga = Macierz[MacierzGrup[Grupa][Element], i];
                                tymczasowyElementWMacierzyKońcowej = MacierzGrup[Grupa][Element];
                                tymczasoweI = i;
                            }
                        }
                MacierzKońcowa[tymczasowyElementWMacierzyKońcowej, tymczasoweI] = NajmniejszaWaga;
                DodajTekst("Najmniejsza waga elementu " + tymczasowyElementWMacierzyKońcowej + " - z elementem "
                    + tymczasoweI + " o wartości " + NajmniejszaWaga + ".");
                MacierzPar[Grupa, 0] = tymczasowyElementWMacierzyKońcowej;
                MacierzPar[Grupa, 1] = tymczasoweI;
            }
            ZapisywanieWTablicy(MacierzGrup, Macierz, MacierzPar, MacierzKońcowa);

        }
        private void ZapisywanieWTablicy(int[][] MacierzGrup, int[,] Macierz, int[,] MacierzPar, int[,] MacierzKońcowa)
        {
            const int PRZERWA = int.MaxValue;
            int[] ZbieranieLiczbyElementów = new int[(LiczbaWierzchołków * 2) + 1];
            int Grupa = 0, Index = 0;

            for (int i = 0; i < MacierzGrup.Length; i++)
            {
                for (int j = 0; j < MacierzGrup[i].Length; j++)
                {
                    ZbieranieLiczbyElementów[Index] = MacierzGrup[i][j];
                    Index++;
                    if (j == MacierzGrup[i].Length - 1)
                    {
                        ZbieranieLiczbyElementów[Index] = short.MaxValue;
                        Index++; Grupa++;
                    }
                }
            }

            for (int m = 0; m < MacierzPar.GetLength(0); m++)
            {
                int i = 0, j = 0;
                i = MacierzPar[m, 0];
                j = MacierzPar[m, 1];

                if (ZbieranieLiczbyElementów.Contains(i) || ZbieranieLiczbyElementów.Contains(j))
                {
                    if (ZbieranieLiczbyElementów.Contains(i) && ZbieranieLiczbyElementów.Contains(j))
                    {
                        Index = ZbieranieLiczbyElementów.FindIndex(i);
                        int Index2 = ZbieranieLiczbyElementów.FindIndex(j);
                        int MinGranicaWiększego = 0, MaxGranicaMniejszego = 0, MaxGranicaWiększego = 0, MinGranicaMniejszego = 0;
                        if ((Index != Index2) && (!(Math.Abs(Index - Index2) == 1)))
                        {
                            int MniejszyIndex = Math.Min(Index, Index2);
                            int WiększyIndex = Math.Max(Index, Index2);
                            for (int l = WiększyIndex; l >= 0; l--)
                                if (ZbieranieLiczbyElementów[l] == short.MaxValue)
                                {
                                    MinGranicaWiększego = l;
                                    break;
                                }
                            for (int l = WiększyIndex; l < ZbieranieLiczbyElementów.Length - 1; l++)
                                if (ZbieranieLiczbyElementów[l] == short.MaxValue)
                                {
                                    MaxGranicaWiększego = l;
                                    break;
                                }
                            for (int l = MniejszyIndex; l < ZbieranieLiczbyElementów.Length - 1; l++)

                                if (ZbieranieLiczbyElementów[l] == short.MaxValue)
                                {
                                    MaxGranicaMniejszego = l;
                                    break;
                                }


                            for (int l = MniejszyIndex; l >= 0; l--)
                            {
                                if (ZbieranieLiczbyElementów[l] == short.MaxValue)
                                {
                                    MinGranicaMniejszego = l;
                                    break;
                                }
                            }
                            if (MaxGranicaMniejszego == MaxGranicaWiększego)
                                break;
                            int LiczbaElementówPóźniejszejGrupy = MaxGranicaWiększego - MinGranicaWiększego - 1;
                            int LiczbaElementówWcześniejszejGrupy = 0;
                            if (MinGranicaMniejszego == 0)
                                LiczbaElementówWcześniejszejGrupy = MaxGranicaMniejszego - MinGranicaMniejszego;
                            else
                                LiczbaElementówWcześniejszejGrupy = MaxGranicaMniejszego - MinGranicaMniejszego - 1;


                            for (int l = ZbieranieLiczbyElementów.Length - 1 - LiczbaElementówPóźniejszejGrupy; l >= MaxGranicaMniejszego; l--)
                            {
                                ZbieranieLiczbyElementów[l + LiczbaElementówPóźniejszejGrupy] = ZbieranieLiczbyElementów[l];
                            }

                            for (int t = 0; t <= LiczbaElementówPóźniejszejGrupy; t++)
                            {
                                ZbieranieLiczbyElementów[t + MaxGranicaMniejszego] = ZbieranieLiczbyElementów[MinGranicaWiększego + LiczbaElementówPóźniejszejGrupy + 1 + t];
                            }

                            for (int t = MinGranicaWiększego + 1 + LiczbaElementówPóźniejszejGrupy; t <= ZbieranieLiczbyElementów.Length - 1; t++)
                            {
                                if (t + LiczbaElementówPóźniejszejGrupy + 1 > ZbieranieLiczbyElementów.Length - 1)
                                {
                                    ZbieranieLiczbyElementów[t] = 0;
                                }
                                else
                                {
                                    ZbieranieLiczbyElementów[t] = ZbieranieLiczbyElementów[t + LiczbaElementówPóźniejszejGrupy + 1];
                                }
                            }

                            Grupa--;
                        }
                    }
                }
            }


            int DługośćMacierzyDługości = MacierzGrup.Length;
            for (int j = Grupa - 1; j < DługośćMacierzyDługości + 1; j++)
                if (MacierzGrup.Length > Grupa + 1)
                    MacierzGrup = MacierzGrup.Where((Arr, i) => i != j)
                                              .Select(Arr => Arr.Where((Item, i) => i != j /*!= 2*/) // to skip col#3
                                                              .ToArray())
                                              .ToArray();

            DługośćMacierzyDługości = MacierzGrup.Length;
            for (int j = Grupa - 1; j < DługośćMacierzyDługości; j++)
                if (MacierzGrup.Length > Grupa)
                    MacierzGrup = MacierzGrup.Where((Arr, i) => i != j)
                                              .Select(Arr => Arr.Where((Item, i) => i != j /*!= 2*/) // to skip col#3
                                                              .ToArray())
                                              .ToArray();

            int DolnaGranica = 0, GórnaGranica = 0, NumerGrupy = 0;
            for (int j = 0; j < Grupa; j++)
            {
                for (int i = GórnaGranica + 1; i <= ZbieranieLiczbyElementów.Length - 1; i++)
                {
                    if (ZbieranieLiczbyElementów[i] == short.MaxValue)
                    {
                        GórnaGranica = i;
                        break;
                    }
                }
                if (NumerGrupy > 0)
                {
                    MacierzGrup[NumerGrupy] = new int[GórnaGranica - DolnaGranica - 1];
                }
                else
                {
                    MacierzGrup[NumerGrupy] = new int[GórnaGranica - DolnaGranica];
                }


                DolnaGranica = GórnaGranica;
                NumerGrupy++;

            }


            int NowyIndex = 0;
            NumerGrupy = 0;
            for (int i = 0; i < ZbieranieLiczbyElementów.Length; i++)
            {
                if ((ZbieranieLiczbyElementów[i] == 0) && (i != 0))
                    break;
                if (NumerGrupy < MacierzGrup.Length)
                {
                    if (NowyIndex == MacierzGrup[NumerGrupy].Length)
                    {
                        NumerGrupy++;
                        NowyIndex = 0;
                    }
                    else if (ZbieranieLiczbyElementów[i] == PRZERWA)
                    {
                        NowyIndex++;
                    }
                    else
                    {
                        MacierzGrup[NumerGrupy][NowyIndex] = ZbieranieLiczbyElementów[i];
                        NowyIndex++;
                    }
                }
                else
                {
                    // ERROR
                    NumerGrupy = 0;
                }
            }

            for (int i = 0; i < MacierzGrup.Length; i++)
            {
                TxtListaSąsiedztwa.AppendText("Elementy połączyły się w grupę " + (i + 1) + ": \t");

                for (int j = 0; j < MacierzGrup[i].Length; j++)
                    if (j == MacierzGrup[i].Length - 1)
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[i][j] + ".");
                    else
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[i][j] + ",  ");
                TxtListaSąsiedztwa.AppendText(Environment.NewLine);

            }
            if ((MacierzGrup.Length == 1) || (MacierzGrup[0].Length == LiczbaWierzchołków))
            {
                TxtListaSąsiedztwa.AppendText("Powstała Grupa o elementach: \t\t");
                for (int i = 0; i < MacierzGrup[0].Length; i++)
                    if (i == MacierzGrup[0].Length - 1)
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[0][i] + ". ");
                    else
                        TxtListaSąsiedztwa.AppendText(MacierzGrup[0][i] + ",  ");
                TxtListaSąsiedztwa.AppendText(Environment.NewLine);

                return;
            }
            else if (MacierzGrup.Length > 1)
                SzukanieWTablicy(MacierzGrup, MacierzKońcowa, Macierz);
        }
        private bool WybórKrawędzi(int Krawędź1, int Krawędź2)
        {
            if (Krawędź2 == int.MaxValue)
            {
                return true;
            }
            else if (Krawędź1 < Krawędź2)
            {
                return true;
            }
            else if (Krawędź1 == Krawędź2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private void BtnAlgorytmBoruvki_Click(object sender, EventArgs e)
        {
            Algorytm();
            for (int i = 0; i < LiczbaWierzchołków; i++)
            {
                for (int j = 0; j < LiczbaWierzchołków; j++)
                {
                    if (MacierzKońcowa[i, j] != BRAK_DROGI)
                    {
                        MacierzKońcowa[j, i] = MacierzKońcowa[i, j];
                    }
                    else if (MacierzKońcowa[i, j] == BRAK_DROGI)
                    {
                        DgvWynik.Rows[i].Cells[j].Value = "";
                    }
                    else
                    {
                        DgvWynik.Rows[i].Cells[j].Value = MacierzKońcowa[i, j];
                    }
                }
            }
        }
        private void DodajTekst(string text)
        {
            TxtListaSąsiedztwa.AppendText(text);
            TxtListaSąsiedztwa.AppendText(Environment.NewLine);
        }
    }
    public static class Extensions
    {
        public static int FindIndex<T>(this T[] Array, T Item)
        {
            try
            {
                return Array
                    .Select((Element, Index) => new KeyValuePair<T, int>(Element, Index))
                    .First(x => x.Key.Equals(Item)).Value;
            }
            catch (InvalidOperationException)
            {
                return -1;
            }
        }
    }
}
