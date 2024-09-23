namespace NES_SNES_Movie_Maker.Interfaces
{
    public interface IMovie
    {
        int Frames { get; set; }
        int TotalBytes { get; set; }
        string Name { get; set; }
        int Number { get; set; }
    }
}
