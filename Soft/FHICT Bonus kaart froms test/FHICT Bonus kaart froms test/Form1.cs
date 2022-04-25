using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace FHICT_Bonus_kaart_froms_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        System.Threading.Thread t;
        private void Form1_Load(object sender, EventArgs e)
        {
            t = new System.Threading.Thread(InterpretSerialInput);
            t.Start();
        }
        static void LineChanger(string newText, string docPath, int index)
        {
            string[] arrLine = File.ReadAllLines(docPath);
            arrLine[index] = newText;
            File.WriteAllLines(docPath, arrLine);
        }
        
        static void ReadDataIn(string docPath, int index, ref int[] streak, ref int[] punten, ref bool[] checkedIn)
        {
            string readLine = File.ReadAllLines(docPath)[index - 1];
            string[] lineArr = readLine.Split(' ');
            streak[index] = Convert.ToInt32(lineArr[1]);
            punten[index] = Convert.ToInt32(lineArr[2]);
            checkedIn[index] = Convert.ToBoolean(Convert.ToInt32(lineArr[3]));
        }

        public void InterpretSerialInput()
        {
            SerialPort serialPort = new SerialPort
            {
                BaudRate = 9600,
                PortName = "COM11"
            };
            serialPort.Open();

            string[] naam = { "error", "Desmond", "Bart" };
            int[] streak = { 0, 0, 0 };
            int[] punten = { 0, 0, 0 };
            bool[] checkedIn = { false, false, false };
            // Set a variable to the data file path.
            string docPath = @"C:\Fontys retry\Proftaak Design challenge\FHICT Bonus Kaart\FHICTBonusKaart\Media\Data.txt";

            //Read data from txt file into local streak and point data
            for (int i = 1; i < checkedIn.Length; i++)
            {
                ReadDataIn(docPath, i, ref streak, ref punten, ref checkedIn);
            }

            
            while (true)
            {
                string serialInput = serialPort.ReadLine();
                Console.WriteLine(serialInput);
                if (serialInput.StartsWith("#StartOfDay%"))
                {
                    for (int i = 1; i < checkedIn.Length; i++)
                    {
                        if (!checkedIn[i]) { streak[i] = 0; }
                        checkedIn[i] = false;
                        LineChanger($"{naam[i]} {streak[i]} {punten[i]} 0", docPath, i - 1);
                    }
                }
                else if (serialInput.StartsWith("#CheckIn ") && serialInput.EndsWith("%\r"))
                {
                    string[] arrData = serialInput.Substring(9, serialInput.Length - 9 - 2).Split('!');
                    int cardIndex = 0;
                    if (arrData[0] == "09 B9 64 C2")
                    {
                        cardIndex = 1;
                    }
                    else if (arrData[0] == "CA 27 61 1F")
                    {
                        cardIndex = 2;
                    }
                    else
                    {
                        cardIndex = 0;
                        serialPort.Write("#Print error%");
                    }
                    bool opTijd = Convert.ToBoolean(Convert.ToInt32(arrData[1]));
                    if (!checkedIn[cardIndex] && cardIndex != 0)
                    {
                        //Bereken punten
                        //Reset streak if late
                        if (!opTijd)
                        {
                            streak[cardIndex] = 0;
                        }
                        else
                        {
                            streak[cardIndex]++;
                            //Per 10 dagen een extra punt per dag
                            punten[cardIndex] += streak[cardIndex] / 10 + 1;
                        }
                        //Register that the card has been checked in this day
                        checkedIn[cardIndex] = true;
                        //Stuur terug naar arduino
                        serialPort.Write($"#CheckIn {naam[cardIndex]} {streak[cardIndex]} {punten[cardIndex]} %");
                        LineChanger($"{naam[cardIndex]} {streak[cardIndex]} {punten[cardIndex]} {Convert.ToInt32(checkedIn[cardIndex])}", docPath, cardIndex - 1);
                    }
                    else if (cardIndex != 0)
                    {
                        serialPort.Write("#Print Checked In%");
                        serialPort.Write($"#CheckIn   {streak[cardIndex]} {punten[cardIndex]} %");
                    }
                }
            }
        }
    }
}
