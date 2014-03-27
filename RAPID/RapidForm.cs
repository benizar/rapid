
//RAPID. Rough Agricultural Plots IDentifier.
//Copyright (C) 2009 Benito M. Zaragozí
//Authors: Benito M. Zaragozí (www.gisandchips.org)
//Send comments and suggestions to benito.zaragozi@ua.es

//This program is free software: you can redistribute it and/or modify
//it under the terms of the GNU General Public License as published by
//the Free Software Foundation, either version 3 of the License, or
//(at your option) any later version.
//
//This program is distributed in the hope that it will be useful,
//but WITHOUT ANY WARRANTY; without even the implied warranty of
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//GNU General Public License for more details.
//
//You should have received a copy of the GNU General Public License
//along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using System.Drawing.Imaging;

using AForge.Imaging;
using AForge.Imaging.Filters;



namespace RAPID
{
    public partial class RapidForm : Form
    {
        Bitmap image;
        Bitmap binaryImage;
        
        ResourceManager rm = new ResourceManager("RAPID.images", System.Reflection.Assembly.GetExecutingAssembly());



        public RapidForm()
        {
            InitializeComponent();

            Bitmap parcela1 = rm.GetObject("parcela1") as Bitmap;
            Bitmap parcela2 = rm.GetObject("parcela2") as Bitmap;
            Bitmap parcela3 = rm.GetObject("parcela3") as Bitmap;
            Bitmap parcela4 = rm.GetObject("parcela4") as Bitmap;
            Bitmap parcela5 = rm.GetObject("parcela5") as Bitmap;
            Bitmap parcela6 = rm.GetObject("parcela6") as Bitmap;
            Bitmap parcela7 = rm.GetObject("parcela7") as Bitmap;
            Bitmap parcela8 = rm.GetObject("parcela8") as Bitmap;
            Bitmap parcela9 = rm.GetObject("parcela9") as Bitmap;
            Bitmap parcela10 = rm.GetObject("parcela10") as Bitmap;
            Bitmap parcela11 = rm.GetObject("parcela11") as Bitmap;
            Bitmap parcela12 = rm.GetObject("parcela12") as Bitmap;
            Bitmap parcela13 = rm.GetObject("parcela13") as Bitmap;
            Bitmap parcela14 = rm.GetObject("parcela14") as Bitmap;
            Bitmap parcela15 = rm.GetObject("parcela15") as Bitmap;
            Bitmap parcela16 = rm.GetObject("parcela16") as Bitmap;
            Bitmap parcela17 = rm.GetObject("parcela17") as Bitmap;
            Bitmap parcela18 = rm.GetObject("parcela18") as Bitmap;
            Bitmap parcela19 = rm.GetObject("parcela19") as Bitmap;
            Bitmap parcela20 = rm.GetObject("parcela20") as Bitmap;
            Bitmap parcela21 = rm.GetObject("parcela21") as Bitmap;
            Bitmap parcela22 = rm.GetObject("parcela22") as Bitmap;

            btParcela1.Image = parcela1;
            btParcela2.Image = parcela2;
            btParcela3.Image = parcela3;
            btParcela4.Image = parcela4;
            btParcela5.Image = parcela5;
            btParcela6.Image = parcela6;
            btParcela7.Image = parcela7;
            btParcela8.Image = parcela8;
            btParcela9.Image = parcela9;
            btParcela10.Image = parcela10;
            btParcela11.Image = parcela11;
            btParcela12.Image = parcela12;
            btParcela13.Image = parcela13;
            btParcela14.Image = parcela14;
            btParcela15.Image = parcela15;
            btParcela16.Image = parcela16;
            btParcela17.Image = parcela17;
            btParcela18.Image = parcela18;
            btParcela19.Image = parcela19;
            btParcela20.Image = parcela20;
            btParcela21.Image = parcela21;
            btParcela22.Image = parcela22;
       
        }

        private void limpiarPorNuevaParcela()
        {
            //Cuando abrimos una nueva imagen borramos todo lo anterior
            btBinarize.Enabled = false;
            btHough.Enabled = false;
            btTrees.Enabled = false;
            lbl1ADirecc.Text = "";
            lbl2ADirecc.Text = "";
            lblPorcLin1D.Text = "";
            lblPorcLin2D.Text = "";
            lblDifAng.Text = "";
            lblHoughResult.Text = "";
            lblLin1ADirecc.Text = "";
            lblLin2ADirecc.Text = "";
            lblTrees.Text = "";
            picBoxBinary.Image = null;


        }

        private void nuevaParcelaSeleccionada()
        {
            lblThreshold.ForeColor = Color.Black;
            btBinarize.Enabled = true;
            lblThreshold.Image = rm.GetObject("Flecha_activa")as Bitmap;
            btHough.Image = rm.GetObject("Flecha") as Bitmap;
            btTrees.Image = rm.GetObject("Flecha") as Bitmap;
        }




        private void btOpen_Click(object sender, EventArgs e)
        {
            //Cuando abrimos una nueva imagen borramos todo lo anterior
            limpiarPorNuevaParcela();


            //Cargamos una nueva imagen
            openFileDialog1.Filter = "Tif image (*.tif)|*.tif";
            if ((openFileDialog1.ShowDialog() == DialogResult.Cancel)) return;

            // load image
            image = (Bitmap)Bitmap.FromFile(openFileDialog1.FileName);



            picBoxSource.Image = image;

            nuevaParcelaSeleccionada();

        }


        private void btBinarize_Click(object sender, EventArgs e)
        {

            //Cuando abrimos una nueva imagen borramos todo lo anterior
            btHough.Enabled = false;
            btTrees.Enabled = false;
            lbl1ADirecc.Text = "";
            lbl2ADirecc.Text = "";
            lblPorcLin1D.Text = "";
            lblPorcLin2D.Text = "";
            lblDifAng.Text = "";
            lblHoughResult.Text = "";
            lblLin1ADirecc.Text = "";
            lblLin2ADirecc.Text = "";
            lblTrees.Text = "";


            //binarize
            binaryImage = RAPID_Binarize.RBinarize(image, Convert.ToInt16(txtThreshold.Text));

            //show the binary image
            picBoxBinary.Image = binaryImage;

            btHough.Enabled = true;
            btHough.Image = rm.GetObject("Flecha_activa") as Bitmap;
            btTrees.Image = rm.GetObject("Flecha") as Bitmap;


        }




        private void btHough_Click(object sender, EventArgs e)
        {

            HoughStats hough = new HoughStats(binaryImage);

            lbl1ADirecc.Text = hough.PrimDirecc.ToString();
            lbl2ADirecc.Text = hough.SegDirecc.ToString();
            lblDifAng.Text = hough.DifAngular.ToString();
            lblLin1ADirecc.Text = hough.NumLineas1ADirecc.ToString();
            lblLin2ADirecc.Text = hough.NumLineas2ADirecc.ToString();
            lblPorcLin1D.Text = hough.PorcLineas1ADirecc.ToString();
            lblPorcLin2D.Text = hough.PorcLineas2ADirecc.ToString();



            if (hough.DifAngular > 120 || hough.DifAngular < 80 || hough.PorcLineas1ADirecc > 2*hough.PorcLineas2ADirecc)
            {
                lblHoughResult.Text = "la estructura no vale";
            }

            btTrees.Enabled = true;

            btTrees.Image = rm.GetObject("Flecha_activa") as Bitmap;

        }

        private void btTrees_Click(object sender, EventArgs e)
        {
            lblTrees.Text = Counter.Count(binaryImage).ToString();
        }



        private void btParcela1_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela1");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }


        private void btParcela2_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela2");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela3_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela3");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela4_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela4");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela5_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela5");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela6_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela6");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela7_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela7");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela8_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela8");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela9_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela9");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela10_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela10");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela11_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela11");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela12_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela12");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela13_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela13");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela14_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela14");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela15_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela15");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela16_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela16");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela17_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela17");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela18_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela18");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela19_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela19");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela20_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela20");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela21_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela21");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

        private void btParcela22_Click(object sender, EventArgs e)
        {
            limpiarPorNuevaParcela();


            picBoxSource.Image = (Bitmap)rm.GetObject("parcela22");


            // load image
            image = (Bitmap)picBoxSource.Image;


            nuevaParcelaSeleccionada();
        }

















    }
}
