using System;
using System.Text;
using System.Text.RegularExpressions;

namespace OS_Grid_Calculator
{
    public static class OSGridCalculator
    {
        public static string ComputeTargetGrid(string yourGrid, int targetDistance, int targetBearing)
        {
            // Which varient on the maths will we need. Derive Theta.
            string quadrant = null;
            int angleTheta = 0;
            if (targetBearing <= 90)
            {
                quadrant = "NE";
                angleTheta = targetBearing;
            }
            else if (targetBearing <= 180)
            {
                quadrant = "SE";
                angleTheta = 180 - targetBearing;
            }
            else if (targetBearing <= 270)
            {
                quadrant = "SW";
                angleTheta = targetBearing - 180;
            }
            else
            {
                quadrant = "NW";
                angleTheta = 360 - targetBearing;
            }

            //Todo: Explode grid in to component parts
            int yourEastings = 0;
            int yourNorthings = 0;

            yourGrid = yourGrid.Replace(" ", "");
            string yourSquare = yourGrid.Substring(0, 2);
            string yourCoordsString = yourGrid.Substring(2);

            int gridResolution = yourCoordsString.Length;

            string yourEastingsString = yourCoordsString.Substring(0,gridResolution / 2);
            string yourNorthingsString = yourCoordsString.Substring(gridResolution / 2);

            int.TryParse(yourEastingsString, out yourEastings);
            int.TryParse(yourNorthingsString, out yourNorthings);
            
            //Math trig functions require a double of radians
            double angleThetaRads = Math.PI * (double)angleTheta / 180.0;

            //Define adjustment for resolution
            int adjustment = 1;
            if (gridResolution == 8)
            {
                adjustment = 10;
            }
            else if (gridResolution == 6)
            {
                adjustment = 100;
            }
            else if (gridResolution == 4)
            {
                adjustment = 1000;
            }
            else if (gridResolution == 2)
            {
                adjustment = 10000;
            }

            //Calculate the target grid
            int eastingsDelta = (int)(Math.Sin(angleThetaRads) * targetDistance) / adjustment;
            int northingsDelta = (int)(Math.Cos(angleThetaRads) * targetDistance) / adjustment;

            int targetEastings = 0;
            int targetNorthings = 0;

            if (quadrant == "NE")
            {
                targetEastings = yourEastings + eastingsDelta;
                targetNorthings = yourNorthings + northingsDelta;
            }
            else if (quadrant == "NW")
            {
                targetEastings = yourEastings - eastingsDelta;
                targetNorthings = yourNorthings + northingsDelta;
            }
            else if (quadrant == "SE")
            {
                targetEastings = yourEastings + eastingsDelta;
                targetNorthings = yourNorthings - northingsDelta;
            }
            else if (quadrant == "SW")
            {
                targetEastings = yourEastings - eastingsDelta;
                targetNorthings = yourNorthings - northingsDelta;
            }

            string targetSquare = yourSquare;

            //Build grid string and return
            string targetGrid = targetSquare + " " + targetEastings + " " + targetNorthings;
            return targetGrid;
        }

        public static Regex osGridValidation = new Regex(@"^([STNHOstnho][A-Za-z]\s?)(\d{5}\s?\d{5}|\d{4}\s?\d{4}|\d{3}\s?\d{3}|\d{2}\s?\d{2}|\d{1}\s?\d{1})$");

    }
}

