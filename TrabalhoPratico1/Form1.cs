using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TrabalhoPratico1
{
    public partial class tela : Form
    {
        Bitmap areaDesenho;
        Bitmap boxCohen;
        Bitmap boxLian;
        Bitmap assist;
        Color corPreenche;
        int x1, x2, y1, y2;
        bool inicio;
        List<int> retas;

        private Rectangle clipRectangle;

        public tela()
        {
            InitializeComponent();

            areaDesenho = new Bitmap(imagem.Size.Width, imagem.Size.Height);
            boxCohen = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            boxLian = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            assist = new Bitmap(imagem.Size.Width, imagem.Size.Height);
            corPreenche = Color.Black;

            x1 = x2 = y1 = y2 = 0;
            txtX1.Text = txtX2.Text = txtY1.Text = txtY2.Text = "0";
            retas = new List<int>();
            inicio = true;

            // Configura a área de recorte para o algoritmo (estes são apenas valores de exemplo)
            clipRectangle = new Rectangle();
        }

        #region FuncoesPadrao
        private void btCor_Click(object sender, EventArgs e)
        {
            DialogResult result = cdlg.ShowDialog();
            if (result == DialogResult.OK)
            {
                corPreenche = cdlg.Color;
            }
        }

        private void btApagar_Click(object sender, EventArgs e)
        {
            areaDesenho = new Bitmap(imagem.Size.Width, imagem.Size.Height);
            imagem.Image = areaDesenho;
            retas = new List<int>();
        }

        private void imagem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = e.X;
                int y = e.Y;
                txtX1.Text = Convert.ToString(x);
                txtY1.Text = Convert.ToString(y);
                areaDesenho.SetPixel(x, y, corPreenche);
                imagem.Image = areaDesenho;
            }
        }

        private void imagem_MouseClick(object sender, MouseEventArgs e)
        {
            if (inicio)
            {
                x1 = e.X;
                y1 = e.Y;
                txtX1.Text = x1.ToString();
                txtY1.Text = y1.ToString();
                areaDesenho.SetPixel(x1, y1, Color.Red);
                imagem.Image = areaDesenho;
                inicio = false;
            }
            else
            {
                x2 = e.X;
                y2 = e.Y;
                txtX2.Text = x2.ToString();
                txtY2.Text = y2.ToString();
                areaDesenho.SetPixel(x2, y2, Color.Red);
                imagem.Image = areaDesenho;

                inicio = true;
            }
        }

        #endregion

        #region DDA
        private void bt_DDA(object sender, EventArgs e)
        {
            retas.Add(x1);
            retas.Add(y1);
            retas.Add(x2);
            retas.Add(y2);
            DDALineDraw(areaDesenho, imagem, x1, y1, x2, y2);
        }

        public void DDALineDraw(Bitmap bitmap, PictureBox picBox, int x1, int y1, int x2, int y2)
        {
            try
            {
                int deltaX, deltaY, passos, moduloDeltaX, moduloDeltaY;
                double x, y, xincrease, yincrease;

                x = x1;
                y = y1;

                bitmap.SetPixel((int)x, (int)y, corPreenche);
                picBox.Image = bitmap;

                deltaX = x2 - x1;
                deltaY = y2 - y1;

                moduloDeltaX = Math.Abs(deltaX);
                moduloDeltaY = Math.Abs(deltaY);

                passos = moduloDeltaX > moduloDeltaY ? moduloDeltaX : moduloDeltaY;

                xincrease = calcularValorIncremento((double)deltaX, passos);
                yincrease = calcularValorIncremento((double)deltaY, passos);

                for (int i = 1; i <= passos; i++)
                {
                    x += xincrease;
                    y += yincrease;
                    bitmap.SetPixel((int)x, (int)y, corPreenche);
                    picBox.Image = bitmap;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao desenhar linha DDA: " + ex.Message);
            }
        }
        #endregion

        #region transformacoes2d
        private void translacao(object sender, EventArgs e)
        {
            this.btApagar_Click(sender, e);

            int x1Translacao = x1 = x1 + 20;
            int y1Translacao = y1 = y1 + 20;
            int x2Translacao = x2 = x2 + 30;
            int y2Translacao = y2 = y2 + 30;

            DDALineDraw(areaDesenho, imagem, x1Translacao, y1Translacao, x2Translacao, y2Translacao);
        }
        private void escala(object sender, EventArgs e)
        {
            this.btApagar_Click(sender, e);

            var fatorEscala = 1.3;

            int xEscala1 = (int)(x1 * fatorEscala);
            int yEscala1 = (int)(y1 * fatorEscala);
            int xEscala2 = (int)(x2 * fatorEscala);
            int yEscala2 = (int)(y2 * fatorEscala);

            DDALineDraw(areaDesenho, imagem, xEscala1, yEscala1, xEscala2, yEscala2);
        }

        private void escalaDown(object sender, EventArgs e)
        {
            this.btApagar_Click(sender, e);

            var fatorEscala = 1.3;

            int xEscala1 = (int)(x1 / fatorEscala);
            int yEscala1 = (int)(y1 / fatorEscala);
            int xEscala2 = (int)(x2 / fatorEscala);
            int yEscala2 = (int)(y2 / fatorEscala);

            DDALineDraw(areaDesenho, imagem, xEscala1, yEscala1, xEscala2, yEscala2);
        }

        private void rotacao(object sender, EventArgs e)
        {
            this.btApagar_Click(sender, e);

            double ParaRadianos = 15 * (Math.PI / 180.0);

            int PontoMedioX = (x1 + x2) / 2;
            int PontoMedioY = (y1 + y2) / 2;

            int xRotacionado1 = (int)((x1 - PontoMedioX) * Math.Cos(ParaRadianos) - (y1 - PontoMedioY) * Math.Sin(ParaRadianos) + PontoMedioX);
            int yRotacionado1 = (int)((x1 - PontoMedioX) * Math.Sin(ParaRadianos) + (y1 - PontoMedioY) * Math.Cos(ParaRadianos) + PontoMedioY);
            int xRotacionado2 = (int)((x2 - PontoMedioX) * Math.Cos(ParaRadianos) - (y2 - PontoMedioY) * Math.Sin(ParaRadianos) + PontoMedioX);
            int yRotacionado2 = (int)((x2 - PontoMedioX) * Math.Sin(ParaRadianos) + (y2 - PontoMedioY) * Math.Cos(ParaRadianos) + PontoMedioY);

            DDALineDraw(areaDesenho, imagem, xRotacionado1, yRotacionado1, xRotacionado2, yRotacionado2);
        }

        private void reflexao(object sender, EventArgs e)
        {
            this.btApagar_Click(sender, e);
            int xRefletido1 = -x1;
            int xRefletido2 = -x2;

            DDALineDraw(areaDesenho, imagem, xRefletido1, y1, xRefletido2, y2);
        }
        #endregion

        #region Bresenham
        private void bt_BresenhamLineDraw(object Sender, EventArgs e)
        {
            retas.Add(x1);
            retas.Add(y1);
            retas.Add(x2);
            retas.Add(y2);
            BresenhamLineDraw(x1, y1, x2, y2);
        }

        private void BresenhamLineDraw(int x1, int y1, int x2, int y2)
        {
            int deltaX = x2 - x1;
            int deltaY = y2 - y1;
            int x = x1, y = y1, xincremento, yincremento, c1, c2, p;

            if (deltaX > 0)
                xincremento = 1;
            else
            {
                xincremento = -1;
                deltaX = -deltaX;
            }

            if (deltaY > 0)
                yincremento = 1;
            else
            {
                yincremento = -1;
                deltaY = -deltaY;
            }

            areaDesenho.SetPixel(x, y, corPreenche);
            imagem.Image = areaDesenho;

            if (deltaX > deltaY)
            {
                p = 2 * deltaY - deltaX;
                c1 = 2 * deltaY;
                c2 = 2 * (deltaY - deltaX);

                for (int i = 0; i < deltaX; i++)
                {
                    x += xincremento;
                    if (p < 0)
                        p += c1;
                    else
                    {
                        p += c2;
                        y += yincremento;
                    }
                    areaDesenho.SetPixel(x, y, corPreenche);
                    imagem.Image = areaDesenho;
                }
            }
            else
            {
                p = 2 * deltaX - deltaY;
                c1 = 2 * deltaX;
                c2 = 2 * (deltaX - deltaY);
                for (int i = 0; i < deltaY; i++)
                {
                    y += yincremento;
                    if (p < 0)
                        p += c1;
                    else
                    {
                        p += c2;
                        x += xincremento;
                    }
                    areaDesenho.SetPixel(x, y, corPreenche);
                    imagem.Image = areaDesenho;
                }
            }
        }
        #endregion

        #region CirculoBresenham
        private void btCirculoBres(object Sender, EventArgs e)
        {
            int raio = (int)Math.Sqrt(((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)));
            FazerCirculo(x1, y1, raio);
        }

        private void FazerCirculo(int xc, int yc, int r)
        {
            int x = 0, y = r;
            int d = 3 - 2 * r;

            DrawCircle(xc, yc, x, y);

            while (y >= x)
            {
                x++;

                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    d = d + 4 * x + 6;
                }

                DrawCircle(xc, yc, x, y);
            }

            imagem.Image = areaDesenho;
        }

        private void DrawCircle(int xc, int yc, int x, int y)
        {
            areaDesenho.SetPixel(xc + x, yc + y, corPreenche);
            areaDesenho.SetPixel(xc - x, yc + y, corPreenche);
            areaDesenho.SetPixel(xc + x, yc - y, corPreenche);
            areaDesenho.SetPixel(xc - x, yc - y, corPreenche);
            areaDesenho.SetPixel(xc + y, yc + x, corPreenche);
            areaDesenho.SetPixel(xc - y, yc + x, corPreenche);
            areaDesenho.SetPixel(xc + y, yc - x, corPreenche);
            areaDesenho.SetPixel(xc - y, yc - x, corPreenche);
        }
        #endregion

        #region CohenSutherland
        private void btCohenSutherlandLineDraw(object sender, EventArgs e)
        {
            assist = new Bitmap(imagem.Size.Width, imagem.Size.Height);
            pictureBox1.Image = assist;

            int rectX = Math.Min(x1, x2);
            int rectY = Math.Min(y1, y2);
            int rectWidth = Math.Abs(x2 - x1);
            int rectHeight = Math.Abs(y2 - y1);

            rectWidth = Math.Max(1, rectWidth);
            rectHeight = Math.Max(1, rectHeight);

            clipRectangle = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            CohenSutherlandLineClipAndDraw();

            pictureBox1.Image = RedimensionarBitmap(assist, boxCohen.Width, boxCohen.Height);
        }

        private const int INSIDE = 0;
        private const int LEFT = 1;
        private const int RIGHT = 2;
        private const int BOTTOM = 4;
        private const int TOP = 8;

        private int ComputeOutCode(int x, int y)
        {
            int code = INSIDE;

            if (x < clipRectangle.Left)
                code += LEFT;
            else if (x > clipRectangle.Right)
                code += RIGHT;
            if (y < clipRectangle.Top)
                code += TOP;
            else if (y > clipRectangle.Bottom)
                code += BOTTOM;

            return code;
        }

        public void CohenSutherlandLineClipAndDraw()
        {
            for (int a = 0; a < retas.Count; a += 4)
            {
                List<int> pontos = new List<int>();
                for (int j = a; j < a + 4 && j < retas.Count; j++)
                {
                    pontos.Add(retas[j]);
                }
                int x1 = pontos[0], y1 = pontos[1], x2 = pontos[2], y2 = pontos[3];
                bool accept = false;
                bool done = false;

                while (!done)
                {
                    int c1 = ComputeOutCode(x1, y1);
                    int c2 = ComputeOutCode(x2, y2);
                    if (c1 == 0 && c2 == 0)
                    {
                        accept = true;
                        done = true;
                    }
                    else if ((c1 & c2) != 0)
                    {
                        done = true;
                    }
                    else
                    {
                        int x, y;
                        int cfora = c1 != 0 ? c1 : c2;

                        if ((cfora & TOP) != 0)
                        {
                            x = x1 + (x2 - x1) * (clipRectangle.Top - y1) / (y2 - y1);
                            y = clipRectangle.Top;
                        }
                        else if ((cfora & BOTTOM) != 0)
                        {
                            x = x1 + (x2 - x1) * (clipRectangle.Bottom - y1) / (y2 - y1);
                            y = clipRectangle.Bottom;
                        }
                        else if ((cfora & RIGHT) != 0)
                        {
                            y = y1 + (y2 - y1) * (clipRectangle.Right - x1) / (x2 - x1);
                            x = clipRectangle.Right;
                        }
                        else
                        {
                            y = y1 + (y2 - y1) * (clipRectangle.Left - x1) / (x2 - x1);
                            x = clipRectangle.Left;
                        }

                        if (cfora == c1)
                        {
                            x1 = x;
                            y1 = y;
                        }
                        else
                        {
                            x2 = x;
                            y2 = y;
                        }
                    }
                }
                if (accept)
                {
                    DDALineDraw(assist, pictureBox1, x1, y1, x2, y2);
                }
            }
        }
        #endregion

        #region LiangBarsky

        private void bt_LiangBarskyLineDraw(object sender, EventArgs e)
        {
            assist = new Bitmap(imagem.Size.Width, imagem.Size.Height);
            pictureBox2.Image = assist;

            int rectX = Math.Min(x1, x2);
            int rectY = Math.Min(y1, y2);
            int rectWidth = Math.Abs(x2 - x1);
            int rectHeight = Math.Abs(y2 - y1);

            rectWidth = Math.Max(1, rectWidth);
            rectHeight = Math.Max(1, rectHeight);

            clipRectangle = new Rectangle(rectX, rectY, rectWidth, rectHeight);

            LiangBarskyLineClipAndDraw();

            pictureBox2.Image = RedimensionarBitmap(assist, boxLian.Width, boxLian.Height);
        }

        private bool cliptest(float p, float q, ref float u1, ref float u2)
        {
            float r;
            if (p < 0.0)
            {
                r = q / p;
                if (r > u2)
                    return false;
                else if (r > u1)
                    u1 = r;
            }
            else if (p > 0.0)
            {
                r = q / p;
                if (r < u1)
                    return false;
                else if (r < u2)
                    u2 = r;
            }
            else if (q < 0.0)
                return false;

            return true;
        }

        public void LiangBarskyLineClipAndDraw()
        {
            for (int a = 0; a < retas.Count; a += 4)
            {
                List<int> pontos = new List<int>();
                for (int j = a; j < a + 4 && j < retas.Count; j++)
                {
                    pontos.Add(retas[j]);
                }
                float x1 = pontos[0], y1 = pontos[1], x2 = pontos[2], y2 = pontos[3];

                float dx = x2 - x1, dy = y2 - y1;
                float u1 = 0, u2 = 1;

                if (cliptest(-dx, x1 - clipRectangle.Left, ref u1, ref u2))
                {
                    if (cliptest(dx, clipRectangle.Right - x1, ref u1, ref u2))
                    {
                        if (cliptest(-dy, y1 - clipRectangle.Top, ref u1, ref u2))
                        {
                            if (cliptest(dy, clipRectangle.Bottom - y1, ref u1, ref u2))
                            {
                                if (u2 < 1.0)
                                {
                                    x2 = x1 + u2 * dx;
                                    y2 = y1 + dy * u2;
                                }
                                if (u1 > 0)
                                {
                                    x1 += dx * u1;
                                    y1 += dy * u1;
                                }
                                DDALineDraw(assist, pictureBox2, (int)Math.Round(x1), (int)Math.Round(y1), (int)Math.Round(x2), (int)Math.Round(y2));
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region AuxMethods
        private double calcularValorIncremento(double delta, int passos)
        {
            return delta / passos;
        }

        static Bitmap RedimensionarBitmap(Bitmap original, int largura, int altura)
        {
            Bitmap redimensionado = new Bitmap(largura, altura);
            using (Graphics g = Graphics.FromImage(redimensionado))
            {
                g.DrawImage(original, 0, 0, largura, altura);
            }
            return redimensionado;
        }
        #endregion
    }
}
