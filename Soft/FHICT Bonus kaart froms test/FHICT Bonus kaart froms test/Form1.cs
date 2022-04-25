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

        static void LineReader(string docPath, int index, ref int[] streak, ref int[] punten)
        {
            string readLine = File.ReadAllLines(docPath)[index];
            string[] lineArr = readLine.Split(' ');
            streak[index] = Convert.ToInt32(lineArr[1]);
            punten[index] = Convert.ToInt32(lineArr[2]);
        }

        public void InterpretSerialInput()
        {
            SerialPort serialPort = new SerialPort
            {
                BaudRate = 9600,
                PortName = "COM11"
            };
            serialPort.Open();

            string serialInput = "";
            bool communicationStarted = false;
            bool startOfDayProtocol = false;
            bool checkInProtocol = false;
            int dataIndex = 0;

            int cardIndex = 0;
            string[] naam = { "Desmond", "Bart" };
            int[] streak = { 0, 0 };
            int[] punten = { 0, 0 };
            bool[] checkedIn = { false, false };
            bool opTijd = false;
            // Set a variable to the Documents path.
            string docPath = @"C:\Fontys retry\Proftaak Design challenge\FHICT Bonus Kaart\FHICTBonusKaart\Media\Data.txt";

            NumericUpDown[] streakNumUpDowns = { numUpDown_Streak0, numUpDown_Streak1 };
            NumericUpDown[] puntenNumUpDowns = { numUpDown_TotalPoints0, numUpDown_TotalPoints1 };

            while (true)
            {
                //Read data form txt file into local streak and point data
                //for (int i = 0; i < streakNumUpDowns.Length; i++)
                //{
                //    streak[i] = Convert.ToInt32(streakNumUpDowns[i].Value);
                //    punten[i] = Convert.ToInt32(puntenNumUpDowns[i].Value);
                //}
                

                serialInput = serialPort.ReadLine();
                Console.WriteLine(serialInput);
                if (serialInput.StartsWith("#StartOfDay"))
                {
                    for (int i = 0; i < checkedIn.Length; i++)
                    {
                        if (!checkedIn[i]) { streak[i] = 0; }
                        checkedIn[i] = false;
                    }
                }
                else if (serialInput.StartsWith("#CheckIn ") && serialInput.EndsWith("%\r"))
                {
                    for (int i = 0; i < checkedIn.Length; i++)
                    {
                        LineReader(docPath, i, ref streak, ref punten);
                    }
                    string[] arrData = serialInput.Substring(9, serialInput.Length-11).Split('!');
                    if (arrData[0] == "09 B9 64 C2")
                    {
                        cardIndex = 0;
                    }
                    else if (arrData[0] == "CA 27 61 1F")
                    {
                        cardIndex = 1;
                    }
                    opTijd = Convert.ToBoolean(Convert.ToInt32(arrData[1]));
                    if (!checkedIn[cardIndex])
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
                        LineChanger($"{naam[cardIndex]} {streak[cardIndex]} {punten[cardIndex]}", docPath, cardIndex);
                    }
                }
            }
        }
    }
}
