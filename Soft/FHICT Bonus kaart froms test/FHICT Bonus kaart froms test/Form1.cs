using System;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace FHICT_Bonus_kaart_froms_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Thread t;
        private void Form1_Load(object sender, EventArgs e)
        {
            t = new Thread(InterpretSerialInput);
            t.Start();
        }
        static void LineChanger(string newText, string docPath, int index)
        {
            string[] arrLine = File.ReadAllLines(docPath);
            arrLine[index] = newText;
            File.WriteAllLines(docPath, arrLine);
        }

        static string LineReader (string docPath, int lineToRead)
        {
            string[] arrLine = File.ReadAllLines(docPath);
            return arrLine[lineToRead];
        }
        
        static void ReadDataIn(string docPath, int index, ref int[] streak, ref int[] punten, ref bool[] checkedIn)
        {
            string readLine = LineReader(docPath, index - 1);
            string[] lineArr = readLine.Split(' ');
            streak[index] = Convert.ToInt32(lineArr[1]);
            punten[index] = Convert.ToInt32(lineArr[2]);
            checkedIn[index] = Convert.ToBoolean(Convert.ToInt32(lineArr[3]));
        }

        string[] naam = { "error", "Desmond", "Bart" };
        int[] streak = { 0, 0, 0 };
        int[] punten = { 0, 0, 0 };
        bool[] checkedIn = { false, false, false };
        // Set a variable to the data file path.
        string docPath = @"C:\Fontys retry\Proftaak Design challenge\FHICT Bonus Kaart\FHICTBonusKaart\Media\Data.txt";

        public void InterpretSerialInput()
        {
            SerialPort serialPort = new SerialPort
            {
                BaudRate = 9600,
                PortName = "COM11"
            };
            serialPort.Open();

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

        private void bttn_WriteToDB_Click(object sender, EventArgs e)
        {
            NumericUpDown[] streakNumUpDowns = { null, numUpDown_Streak0, numUpDown_Streak1 };
            NumericUpDown[] puntenNumUpDowns = { null, numUpDown_TotalPoints0, numUpDown_TotalPoints1 };
            RadioButton[] checkedInRbs = { null, rb_CheckedIn0, rb_CheckedIn1 };
            Button bttn = (Button)sender;
            int index = bttn.TabIndex;
            if (bttn.Name.Contains("Read"))
            {
                string readLine = LineReader(docPath, index-1);
                string[] lineArr = readLine.Split(' ');
                streakNumUpDowns[index].Value = Convert.ToInt32(lineArr[1]);
                puntenNumUpDowns[index].Value = Convert.ToInt32(lineArr[2]);
                checkedInRbs[index].Checked = Convert.ToBoolean(Convert.ToInt32(lineArr[3]));
            }
            else
            {
                streak[index] = Convert.ToInt32(streakNumUpDowns[index].Value);
                punten[index] = Convert.ToInt32(puntenNumUpDowns[index].Value);
                checkedIn[index] = checkedInRbs[index].Checked;
                LineChanger($"{naam[index]} {streak[index]} {punten[index]} {Convert.ToInt32(checkedIn[index])}", docPath, index - 1);
            }
        }
    }
}
