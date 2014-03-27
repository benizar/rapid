
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
    public static class Counter
    {



        public static int Count(Bitmap BinarizedImage)
        {

            FiltersSequence filtro = new FiltersSequence(new Invert());
            Bitmap ImagenInvertida = filtro.Apply(BinarizedImage);

            //create an instance of blob counter algorithm
            BlobCounterBase bc = new BlobCounter();

            bc.FilterBlobs = true;
            bc.MinWidth = 0;
            bc.MinHeight = 0;

            // set ordering options
            bc.ObjectsOrder = ObjectsOrder.Size;


            // process binary image
            bc.ProcessImage(ImagenInvertida);
            Blob[] blobs = bc.GetObjectsInformation();

            return bc.ObjectsCount;

        }


    }
}
