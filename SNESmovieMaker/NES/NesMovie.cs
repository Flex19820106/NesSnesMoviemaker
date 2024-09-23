using NES_SNES_Movie_Maker.Interfaces;

namespace NES_SNES_Movie_Maker.NES
{
    public class NesMovie : IMovie
    {
        public int Frames { get; set; }
        public int TotalBytes { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Position { get; set; }
    }
}
