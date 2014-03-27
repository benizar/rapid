
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
using System.Linq;
using System.Text;
using System.Drawing;

using System.Drawing.Imaging;

using AForge.Imaging;
using AForge.Imaging.Filters;

namespace RAPID
{
    public class HoughStats
    {


        public HoughStats(Bitmap RAPID_binarySource)
        {
            this.calcStats(RAPID_binarySource);
 
        }


        double _primDirecc = 0;
        double _segDirecc = 0;
        double _difAngular = 0;
        int _numLineas1ADireccion = 0;
        int _numLineas2ADireccion = 0;
        double _porcLineas1ADireccion = 0;
        double _porcLineas2ADireccion = 0;





        public double PrimDirecc
        {
            get { return _primDirecc; }
        }

        public double SegDirecc
        {
            get { return _segDirecc; }
        }


        public double DifAngular
        {
            get { return _difAngular; }
        }

        public int NumLineas1ADirecc
        {
            get { return _numLineas1ADireccion; }
        }

        public int NumLineas2ADirecc
        {
            get { return _numLineas2ADireccion; }
        }

        public double PorcLineas1ADirecc
        {
            get { return _porcLineas1ADireccion; }
        }

        public double PorcLineas2ADirecc
        {
            get { return _porcLineas2ADireccion; }
        }




        private void calcStats(Bitmap RAPID_binarySource)
        {


            HoughLineTransformation lineTransform = new HoughLineTransformation();


            
            // apply Hough line transofrm
            lineTransform.ProcessImage(RAPID_binarySource);


            HoughLine[] mostIntensiveLines = lineTransform.GetMostIntensiveLines(2);
            HoughLine[] relativeLines = lineTransform.GetLinesByRelativeIntensity(0);


            //Listamos todas las thetas(en este caso solo dos)
            List<double> mostIntensiveThetas = new List<double>();
            foreach (HoughLine line in mostIntensiveLines)
            {
                mostIntensiveThetas.Add((double)line.Theta);
            }



            _primDirecc = mostIntensiveThetas.Max();
            _segDirecc = mostIntensiveThetas.Min();


            //Mostramos las dos direcciones principales y su diferencia angular
            _difAngular = mostIntensiveThetas.Max() - mostIntensiveThetas.Min();


            //Listamos todas las thetas y calculamos algunas estadísticas de apoyo
            List<double> relativeThetas = new List<double>();
            foreach (HoughLine line in relativeLines)
            {
                relativeThetas.Add((double)line.Theta);

                if (line.Theta == mostIntensiveThetas.Max())
                {
                    _numLineas1ADireccion++;
                }

                if (line.Theta == mostIntensiveThetas.Min())
                {
                    _numLineas2ADireccion++;
                }
            }


            _porcLineas1ADireccion = System.Math.Round((double)_numLineas1ADireccion*100 / lineTransform.LinesCount,2);
            _porcLineas2ADireccion = System.Math.Round((double)_numLineas2ADireccion * 100 / lineTransform.LinesCount,2);




        }



    }
}
