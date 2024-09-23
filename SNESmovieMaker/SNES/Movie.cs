using NES_SNES_Movie_Maker.Interfaces;
using System;
using System.Collections.Generic;

namespace NES_SNES_Movie_Maker.SNES
{
    public class Movie : IComparer<Movie>, IMovie
    {
        public int Frames { get; set; }
        public int TotalBytes { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        public int Compare(Movie x, Movie y)
        {
            throw new NotImplementedException();
        }
    }
}
