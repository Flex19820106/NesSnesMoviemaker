using NES_SNES_Movie_Maker.NES;
using NES_SNES_Movie_Maker.SNES;
using System;
using System.IO;

namespace NES_SNES_Movie_Maker
{
    class Program
    {
        public void Main(string[] filesNames)
        {
            bool isNes;

            isNes = Path.GetExtension(filesNames[0]) == ".fm2" ? true : false;

            //if (Path.GetExtension(filesNames[0]) == ".fm2")
            //    isNes = true;
            //else
            //    isNes = false;
            try
            {
                if (isNes)
                {
                    NesMovieMaker nmm = new NesMovieMaker(filesNames);
                    nmm.InitializeMovieFiles();
                    nmm.SumTotalFrames();
                    nmm.AddNumberOfMovie();
                    nmm.Sort();
                    nmm.ConcatFiles();                }
                else
                {
                    MovieMaker mm = new MovieMaker(filesNames);
                    mm.InitializeMovieFiles();
                    mm.SumTotalFrames();
                    mm.AddNumberOfMovie();
                    mm.Sort();
                    mm.AddTotalFramesInNewFile();
                    mm.ConcatFiles();
                }
                CompleteWindow cw = new CompleteWindow();
                cw.ShowDialog();
            }

            catch (Exception ex)
            {
                ErrorWindow ew = new ErrorWindow();
                ew.Exception_catch(ex);
                ew.ShowDialog();
            }
        }
    }
}
