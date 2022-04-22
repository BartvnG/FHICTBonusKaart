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
        static void LineChanger(string newText, string docPath, int lineToWrite)
        {
            string[] arrLine = File.ReadAllLines(docPath);
            arrLine[lineToWrite] = newText;
            File.WriteAllLines(docPath, arrLine);
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
                for (int i = 0; i < streakNumUpDowns.Length; i++)
                {
                    streak[i] = Convert.ToInt32(streakNumUpDowns[i].Value);
                    punten[i] = Convert.ToInt32(puntenNumUpDowns[i].Value);
                }

                serialInput = serialPort.ReadLine();
                Console.WriteLine(serialInput);

                if (serialInput == "#") 
                { 
                    communicationStarted = true; 
                }

                if (communicationStarted)
                {
                    if (serialInput == "%")
                    {
                        communicationStarted = false;
                        checkInProtocol = false;
                        dataIndex = 0;
                        //Straf als je de vorige dag niet hebt ingecheckt
                        if (startOfDayProtocol)
                        {
                            for (int i = 0; i < checkedIn.Length; i++)
                            {
                                if (!checkedIn[i]) { streak[i] = 0; }
                                checkedIn[i] = false;
                            }
                            startOfDayProtocol = false;
                        }
                        else if (!checkedIn[cardIndex])
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
                                punten[cardIndex] = streak[cardIndex] / 10 + 1;
                            }
                            //Register that the card has been checked in this day
                            checkedIn[cardIndex] = true;
                            //Stuur terug naar arduino
                            serialPort.Write($"#CheckIn {naam[cardIndex]} {streak[cardIndex]} {punten[cardIndex]} %");
                            LineChanger($"{naam[cardIndex]} {streak[cardIndex]} {punten[cardIndex]}", docPath, cardIndex);
                        }
                        //for (int i = 0; i < streakNumUpDowns.Length; i++)
                        //{
                        //    streakNumUpDowns[i].Invoke((MethodInvoker)delegate { streakNumUpDowns[i].Value = streak[i]; });
                        //    puntenNumUpDowns[i].Invoke((MethodInvoker)delegate { puntenNumUpDowns[i].Value = punten[i]; });
                        //}
                    }

                    if (serialInput == "StartOfDay")
                    {
                        startOfDayProtocol = true;
                    }
                    else if (serialInput == "CheckIn")
                    {
                        checkInProtocol = true;
                    }
                    else if (checkInProtocol)
                    {
                        switch (dataIndex)
                        {
                            case 0:
                                if (serialInput.Substring(1) == "09 B9 64 C2")
                                {
                                    cardIndex = 1;
                                }
                                else if (serialInput.Substring(1) == "CA 27 61 1F")
                                {
                                    cardIndex = 0;
                                }
                                //To implement? non Fontys cards not recognised, show error on lcd
                                break;
                            case 1:
                                opTijd = Convert.ToBoolean(Convert.ToInt32(serialInput));
                                break;
                            default:
                                break;
                        }
                        dataIndex++;
                    }
                }
            }
        }
    }
}
