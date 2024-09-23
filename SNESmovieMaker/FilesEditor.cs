using NES_SNES_Movie_Maker.Interfaces;
using NES_SNES_Movie_Maker.NES;
using NES_SNES_Movie_Maker.SNES;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Threading;

namespace NES_SNES_Movie_Maker
{
    public class FilesEditor
    {
        private BackgroundWorker worker;
        private IMovie[] movies;
        private string newPathAndNameOfFile;
        private ProgressBar pb;
        private int cur;                     //положение прогресс бара
        public bool isError;

        public FilesEditor(IMovie[] fileNames)
        {
            movies = fileNames;
            newPathAndNameOfFile = NewPath();

            cur = 0;
            worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += worker_ProgressChanged;
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                CreateCopyOfFirstFile();
                NesMovie[] movieS = movies as NesMovie[];
                int min = movieS[0].Number; //Берём номер самого первого файла в массиве

                foreach (NesMovie movie in movieS)
                {
                    concatFileNES(movie, min);
                    (sender as BackgroundWorker).ReportProgress(++cur);
                    Thread.Sleep(100);
                }
            }
            catch
            {
                isError = true;
            }

            isError = false;
            //pb.Close();
        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            changeProgressBarValue(e.ProgressPercentage);
        }

        private string NewPath()
        {
            //создаём новый Path и имя для нового Movie
            char[] nameOfFile = movies[0].Name.ToCharArray();

            int length = nameOfFile.Length - 5;
            int nullChar = 0;
            for (int i = 3; i > 0; i--)
            {
                if (char.IsDigit(nameOfFile[length]))
                {
                    nameOfFile[length--] = char.MinValue;
                    nullChar++;
                }
            }

            char[] nameOfFileNew = new char[nameOfFile.Length - nullChar];
            int j = 0;

            for (int i = 0; i < nameOfFile.Length; i++)
            {
                if (nameOfFile[i] != char.MinValue)
                    nameOfFileNew[j++] = nameOfFile[i];
            }

            string newPathAndNameOfFile = string.Concat(nameOfFileNew);
            return newPathAndNameOfFile;
        }

        private void CreateCopyOfFirstFile()
        {
            //дублирование первого файла
            byte[] bytes;
            int quantityOfBites;

            using (BinaryReader br = new BinaryReader(File.Open(movies[0].Name, FileMode.Open)))
            {
                quantityOfBites = (int)br.BaseStream.Length;
                bytes = new byte[quantityOfBites];
                br.Read(bytes, 0, quantityOfBites);
            }

            using (FileStream fs = File.Create(newPathAndNameOfFile, quantityOfBites))
            {
                fs.Write(bytes, 0, quantityOfBites);
            }
        }

        public void concat_SNES()
        {
            showProgressBarWindow();
            int min = movies[0].Number; //Берём номер самого первого файла в массиве

            foreach (IMovie movie in movies)
            {
                concatFileSnes(movie, min);
                changeProgressBarValue(++cur);
            }
            pb.Close();
        }

        public bool concat_NES()
        {
            showProgressBarWindow();
            worker.RunWorkerAsync();
            return isError; 
            
            //CreateCopyOfFirstFile();
            //NesMovie[] movieS = movies as NesMovie[];
            //int min = movieS[0].Number; //Берём номер самого первого файла в массиве

            //showProgressBarWindow();

            //foreach (NesMovie movie in movieS)
            //{
            //    concatFileNES(movie, min);
            //    changeProgressBarValue(++cur);
            //    Thread.Sleep(50);
            //}
            //pb.Close();
        }

        private void concatFileNES(NesMovie movie, int min)
        {
            using (FileStream fsMainMovie = File.OpenWrite(newPathAndNameOfFile))
            {
                if (movie.Number != min)
                {
                    using (BinaryReader fsNotMainMovies = new BinaryReader(File.OpenRead(movie.Name)))
                    {
                        int totalBytes = movie.TotalBytes - movie.Position;
                        byte[] bytesWrite = new byte[totalBytes];
                        fsNotMainMovies.BaseStream.Seek(movie.Position, SeekOrigin.Begin);
                        bytesWrite = fsNotMainMovies.ReadBytes(totalBytes);
                        fsMainMovie.Seek(0, SeekOrigin.End);
                        fsMainMovie.Write(bytesWrite, 0, totalBytes);
                    }
                }
            }
        }

        private void concatFileSnes (IMovie movie, int min)
        {
            using (FileStream fsMainMovie = File.OpenWrite(newPathAndNameOfFile))
            {
                if (movie.Number != min)
                {
                    using (BinaryReader fsNotMainMovies = new BinaryReader(File.OpenRead(movie.Name)))
                    {
                        int totalBytes = movie.Frames * 2;
                        byte[] bytesWrite = new byte[totalBytes];
                        fsNotMainMovies.BaseStream.Seek(movie.TotalBytes - totalBytes, SeekOrigin.Begin);
                        bytesWrite = fsNotMainMovies.ReadBytes(totalBytes);
                        fsMainMovie.Seek(0, SeekOrigin.End);
                        fsMainMovie.Write(bytesWrite, 0, bytesWrite.Length);
                    }
                }
            }
        }

        private void showProgressBarWindow()
        {
            pb = new ProgressBar();
            pb.MaxValue((double)movies.Length);
            pb.Show();
        }

        private void changeProgressBarValue(int cur)
        {
            pb.ChangeProgressBarValue(cur);
        }

        internal void AddTotalFramesInNewFile(int maxFramesComplete)
        {
            CreateCopyOfFirstFile();

            using (BinaryWriter binaryWriter = new BinaryWriter(File.OpenWrite(newPathAndNameOfFile)))
            {
                binaryWriter.BaseStream.Seek(16, SeekOrigin.Begin);
                binaryWriter.Write(maxFramesComplete);
                binaryWriter.BaseStream.Seek(32, SeekOrigin.Begin);
                binaryWriter.Write(maxFramesComplete);
            }
        }
    }
}
