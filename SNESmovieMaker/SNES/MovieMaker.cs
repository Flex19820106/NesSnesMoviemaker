using System;
using System.IO;

namespace NES_SNES_Movie_Maker.SNES
{
    public class MovieMaker
    {
        private string[] files;
        public Movie[] movies;
        private FilesEditor fe;
        private int maxFramesComplete;

        public MovieMaker(string[] fileNames)
        {
            files = fileNames;
            movies = new Movie[fileNames.Length];
        }

        public void InitializeMovieFiles() => _InitializeMovieFiles();
        public void SumTotalFrames() => _SumTotalFrames();
        public void AddNumberOfMovie() => _AddNumberOfMovie();
        public void Sort() => _Sort();
        public void AddTotalFramesInNewFile() => _AddTotalFramesInNewFile();
        public void ConcatFiles() => _ConcatFiles();

        private void _InitializeMovieFiles()
        {
            int i = 0;
            foreach (string fileName in files)
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    movies[i] = new Movie();
                    movies[i].Name = fileName;
                    movies[i].TotalBytes = (int)binaryReader.BaseStream.Length;
                    binaryReader.BaseStream.Seek(16, SeekOrigin.Begin);
                    movies[i].Frames = binaryReader.ReadInt32();
                    i++;
                }
            }
            fe = new FilesEditor(movies);
        }

        private void _SumTotalFrames()
        {
            foreach (Movie movie in movies)
            {
                maxFramesComplete += movie.Frames;
            }
        }

        private void _AddNumberOfMovie()
        {
            char[] number = new char[3];
            foreach (Movie movie in movies)
            {
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(movie.Name);
                int point = fileNameWithoutExt.Length - 1;
                for (int i = 2; i >= 0; i--)
                {
                    if (char.IsDigit(fileNameWithoutExt[point]))
                        number[i] = fileNameWithoutExt[point--];
                    else
                        number[i] = '0';
                }
                string s = String.Concat(number);
                movie.Number = int.Parse(s);
            }
        }

        private void _Sort()
        {
            //List<Movie> mov = new List<Movie>();
            //mov.Sort((n,b) => );
            int length = movies.Length;

            for (int i = 0; i < length; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (movies[i].Number > movies[j].Number)
                    {
                        Movie movie;
                        movie = movies[i];
                        movies[i] = movies[j];
                        movies[j] = movie;
                    }
                }
            }
        }

        private void _AddTotalFramesInNewFile()
        {
            fe.AddTotalFramesInNewFile(maxFramesComplete);
        }

        private void _ConcatFiles()
        {
            fe.concat_SNES();
        }
    }
}
