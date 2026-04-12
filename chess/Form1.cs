using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
namespace chess
{
    public partial class Form1 : Form
    {
        private TableLayoutPanel? tablaVizibila;
        private PictureBox[,]? pictureBoxes;
        private Manager gameManager;
        

        private Color culoareAlb = Color.Beige;
        private Color culoareNegru = Color.SaddleBrown;
        private Color culoareSelectat = Color.LightGreen;
        private Color culoareFog = Color.FromArgb(33,40, 40, 40);

        public Form1()
        {
            InitializeComponent();
            this.Text = "Sah";
            this.Size = new Size(1920, 1080);

            gameManager = new Manager();

            InitializareTabla();
            DeseneazaTabla();
        }
        private void InitializareTabla()
        {
            tablaVizibila = new TableLayoutPanel
            {
                RowCount = 8,
                ColumnCount = 8,
                Dock = DockStyle.Fill,
                Margin = new Padding(0)
            };

            pictureBoxes = new PictureBox[8, 8];

            for (int i = 0; i < 8; i++)
            {
                tablaVizibila.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5f));
                tablaVizibila.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5f));
            }
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox? pb = new PictureBox
                    {
                        Dock = DockStyle.Fill,
                        Margin = new Padding(0),
                        SizeMode = PictureBoxSizeMode.CenterImage,

                        Tag = new Point(i, j)
                    };

                    pb.Click += Patrat_Click;

                    pictureBoxes[i, j] = pb;
                    tablaVizibila.Controls.Add(pb, i, j);
                }
            }
            this.Controls.Add(tablaVizibila);
        }
        private void Patrat_Click(object? sender, EventArgs e)
        {
            if (gameManager.jocTerminat)
            {
                return;
            }
            PictureBox? clickedpb = sender as PictureBox;
            if (clickedpb == null) return;
            if (clickedpb.Tag is not Point coordonate) return;

            gameManager.ProcesareClick(coordonate.X, coordonate.Y);
            DeseneazaTabla();

            if (gameManager.jocTerminat)
            {
                MessageBox.Show($"Sah-mat! {gameManager.Castigator} a capturat regele!");
                Close();
            }
        }
        private void DeseneazaTabla()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    PictureBox? pb = pictureBoxes[i, j];

                    pb.Image= null; 
                    Piesa? piesaCurenta = gameManager.tabla.grid[i, j];
                    bool esteVizibl = gameManager.vizibilitate[i, j];

                    if (!esteVizibl)
                    {
                        pb.BackColor = culoareFog;
                    }
                    else if (gameManager.piesaSelectata != null &&
                        gameManager.piesaSelectata.pozitie.x == i &&
                        gameManager.piesaSelectata.pozitie.y == j)
                    {
                        pb.BackColor = culoareSelectat;
                    }
                    else
                    {
                        bool estePatratAlb = (i + j) % 2 == 0;
                        pb.BackColor = estePatratAlb ? culoareAlb : culoareNegru;

                        if (piesaCurenta != null && esteVizibl)
                        {
                            pb.Image = GetImaginePentruPiesa(piesaCurenta);
                        }
            
                    }
                }
            }
        }
        private Image? GetImaginePentruPiesa(Piesa piesa)
        {
            string Culoare = piesa.isWhite ? "Alb" : "Negru";

            string numePiesa = "";
            if (piesa is Pion) numePiesa = "Pion";
            else if (piesa is Tura) numePiesa = "Tura";
            else if (piesa is Rege) numePiesa = "Rege";
            else if (piesa is Regina) numePiesa = "Regina";
            else if (piesa is Nebun) numePiesa = "Nebun";
            else if (piesa is Cal) numePiesa = "Cal";

            string NumeFisier = $"{numePiesa}_{Culoare}.png";

            string caleFisier = Path.Combine(Application.StartupPath, "PieseSah", NumeFisier);

            if (File.Exists(caleFisier))
            {
                return Image.FromFile(caleFisier);
            }
            return null;
        }
    }
}
