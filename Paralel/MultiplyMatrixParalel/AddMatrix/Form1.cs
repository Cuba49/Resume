﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AddMatrix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Value = numProcs;
            textBox1.Text = "Вас вітає програма для множення матриць. Виберіть необхідну кількість потоків і натисність \"ПУСК!\". Рекомендована кількість потоків для вашого комп'ютера 1-" + numProcs.ToString() + ".";
        }
        int numProcs = Environment.ProcessorCount;
        int countThreads;
        int coundEndThreads;
        List<int[,]> listAllMasToAdd = new List<int[,]>();
        int[,] result = new int[0, 0];
        string[] linesGlobal = new string[0];
        string[] columnsCountGlobal = new string[0];
        string[] strMasResult;
        //string[] dirs = Directory.GetFiles(@"..\Files\", "*");
        OpenFileDialog ofd = new OpenFileDialog();
        int[,] tmpMas1;
        int[,] tmpMas2;
        int[,] tmpResult;
        int thisFile=1;


        Stopwatch stopWatch = new Stopwatch();
        private void button1_Click(object sender, EventArgs e)
        {
            countThreads = Convert.ToInt32(numericUpDown1.Value);
            ofd.Filter =
                "Text files (*.TXT;)|*.TXT;|" +
                "All files (*.*)|*.*";
            ofd.Multiselect = true;
            ofd.Title = "Виберіть необхідні файли матриць для множення";
            if (ofd.ShowDialog(this) == DialogResult.OK)
            {
                linesGlobal = System.IO.File.ReadAllLines(ofd.FileNames[0]);
            }
            columnsCountGlobal = linesGlobal[0].Split(' ');
            result = new int[linesGlobal.Length, columnsCountGlobal.Length];
            tmpResult = new int[linesGlobal.Length, columnsCountGlobal.Length];
            int countFile = ofd.FileNames.Length;
            int portion = countFile / countThreads;

            List<Thread> taskMas = new List<Thread>();
            int endCopy = 0;
            stopWatch.Start();
            for (int i = 0; i < countThreads; i++)
            {
                int start = i == 0 ? 0 : endCopy + 1;
                int end = (i == countThreads - 1) ? countFile - 1 : start + portion - 1;
                endCopy = end;
                Thread thread = new Thread(new ThreadStart(() => Readmatrix(start, end)));
                thread.Name = i.ToString();
                thread.IsBackground = true;
                thread.Start();
            }

        }
        public void Readmatrix(int start, int end)
        {
            string[] linesInFile;
            string[] charMasFromOneLineInFile;
            for (int i = start; i <= end; i++)
            {
                var tmpResultToRead = new int[linesGlobal.Length, columnsCountGlobal.Length];
                linesInFile = System.IO.File.ReadAllLines(ofd.FileNames[i]);
                for (int j = 0; j < linesInFile.Length; j++)
                {
                    charMasFromOneLineInFile = linesInFile[j].Split(' ');
                    for (int q = 0; q < charMasFromOneLineInFile.Length; q++)
                    {
                        tmpResultToRead[j, q] = Convert.ToInt32(charMasFromOneLineInFile[q]);
                    }
                }
                listAllMasToAdd.Add(tmpResultToRead);
            }
            OneThreadEnd();
        }
        public void OneThreadEnd()
        {
            coundEndThreads++;
            if (coundEndThreads == countThreads)
            {
                coundEndThreads = 0;
                Multiply();
                
            }
        }
        public void Multiply()
        {
            tmpMas1 = listAllMasToAdd[0];
            tmpMas2 = listAllMasToAdd[thisFile];
            thisFile++;
            ThreadOfMultiply();


        }
        public void ThreadOfMultiply()
        {
            tmpResult = new int[linesGlobal.Length, columnsCountGlobal.Length];
            int endCopy = 0;
            int countFile = linesGlobal.Length;
            int portion = countFile / countThreads;
            for (int i = 0; i < countThreads; i++)
            {
                int start = i == 0 ? 0 : endCopy + 1;
                int end = (i == countThreads - 1) ? linesGlobal.Length - 1 : start + portion - 1;
                endCopy = end;
                Thread thread = new Thread(new ThreadStart(() => MultiplyString(start, end)));
                thread.Name = i.ToString();
                thread.IsBackground = true;
                thread.Start();
            }
        }
        public void MultiplyString(int start, int end)
        {
            end = start == end ? end+=1 : end;
            for (int j = start; j < end; j++)
            {
                for (int q = 0; q < columnsCountGlobal.Length; q++)
                {
                    for (int f = 0; f < columnsCountGlobal.Length; f++)
                    {
                        tmpResult[j, q] += tmpMas1[j, f] * tmpMas2[f, q];
                    }
                }

            }
            MyLineFinish();
        }
        public void MyLineFinish()
        {
            coundEndThreads++;
            if (coundEndThreads == countThreads)
            {
                coundEndThreads = 0;
                if (thisFile < 4)
                {
                    tmpMas1 = tmpResult;
                    tmpMas2 = listAllMasToAdd[thisFile];
                    thisFile++;
                    ThreadOfMultiply();
                }
                else
                {
                    result = tmpResult;
                    stopWatch.Stop();
                    strMasResult = new string[result.Length];
                    MessageBox.Show("Множення завершено! Було затрачено " + (stopWatch.ElapsedMilliseconds / 1000).ToString() + " секунд. Будь ласка, збережіть результат.");

                    ShowResult();
                }
            }
        }
        public void WriteInFile()
        {
            for (int i = 0; i < result.Length / columnsCountGlobal.Length; i++)
            {
                for (int j = 0; j < columnsCountGlobal.Length; j++)
                {
                    strMasResult[i] += result[i, j].ToString() + " ";
                }
            }
            Stream myStream;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "result.txt";
            sfd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            sfd.FilterIndex = 2;
            sfd.RestoreDirectory = true;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if ((myStream = sfd.OpenFile()) != null)
                {
                    myStream.Close();
                    StreamWriter sw = new StreamWriter(sfd.FileName);
                    foreach (var node in strMasResult)
                    {
                        sw.WriteLine(node);
                    }
                    sw.Close();
                }
            }
        }
        public void ShowResult()
        {
            Result result2 = new Result(result, columnsCountGlobal.Length, linesGlobal.Length);
            result2.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            WriteInFile();
        }
    }
}
