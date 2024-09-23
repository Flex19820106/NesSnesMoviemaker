using System;
using System.IO;
using System.Windows;

namespace NES_SNES_Movie_Maker.NES
{
    class NesMovieMaker
    {
        private string[] files;
        public NesMovie[] movies;
        private FilesEditor fe;
        private int maxFramesComplete;

        public NesMovieMaker(string[] fileNames)
        {
            files = fileNames;
            movies = new NesMovie[fileNames.Length];
        }

        public void InitializeMovieFiles() => _InitializeMovieFiles();
        public void SumTotalFrames() => _SumTotalFrames();
        public void AddNumberOfMovie() => _AddNumberOfMovie();
        public void Sort() => _Sort();
        public bool ConcatFiles() => _ConcatFiles();

        private void _InitializeMovieFiles()
        {
            int i = 0;
            foreach (string fileName in files)
            {
                using (BinaryReader binaryReader = new BinaryReader(File.Open(fileName, FileMode.Open)))
                {
                    movies[i] = new NesMovie();
                    movies[i].Name = fileName;
                    movies[i].TotalBytes = (int)binaryReader.BaseStream.Length;
                    int frames = 0;
                    int position = movies[i].TotalBytes - 15;
                    binaryReader.BaseStream.Seek(position, SeekOrigin.Begin);

                    for (int j = 0; j < movies[i].TotalBytes; j++)
                    {
                        char a = binaryReader.ReadChar();
                        if (a == '|')
                            frames++;
                        else
                        {
                            position += 15;
                            break;
                        }
                        position -= 15;
                        binaryReader.BaseStream.Seek(position, SeekOrigin.Begin);
                    }
                    movies[i].Position = position;
                    movies[i].Frames = frames;
                    i++;
                }
            }
            fe = new FilesEditor(movies);
        }

        private void _SumTotalFrames()
        {
            foreach (NesMovie movie in movies)
            {
                maxFramesComplete += movie.Frames;
            }
        }

        private void _AddNumberOfMovie()
        {
            char[] number = new char[3];
            foreach (NesMovie movie in movies)
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
                        NesMovie movie;
                        movie = movies[i];
                        movies[i] = movies[j];
                        movies[j] = movie;
                    }
                }
            }
        }

        private bool _ConcatFiles()
        {
            return fe.concat_NES();
        }
    }
}
