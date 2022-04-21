﻿using System;
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

        public void InterpretSerialInput()
        {
            SerialPort serialPort = new SerialPort
            {
                BaudRate = 9600,
                PortName = "COM11"
            };
            serialPort.Open();

            string serialInput = "";
            bool communacationStarted = false;
            bool checkInProtocol = false;
            int dataIndex = 0;

            int index = 0;
            string[] naam = { "Desmond", "Bart" };
            int[] streak = { 0, 0 };
            int[] punten = { 0, 0 };
            bool opTijd = false;

            while (true)
            {
                streak[0] = Convert.ToInt32(numUpDown_Streak0.Value);
                streak[1] = Convert.ToInt32(numUpDown_Streak1.Value);
                punten[0] = Convert.ToInt32(numUpDown_TotalPoints0.Value);
                punten[1] = Convert.ToInt32(numUpDown_TotalPoints1.Value);
                serialInput = serialPort.ReadLine();
                Console.WriteLine(serialInput);

                if (serialInput == "#") { communacationStarted = true; }

                if (communacationStarted)
                {
                    if (serialInput == "%")
                    {
                        communacationStarted = false;
                        checkInProtocol = false;
                        dataIndex = 0;
                        //Bereken punten
                        //Reset streak if late
                        if (!opTijd) { streak[index] = 0; }
                        else { streak[index]++; }
                        //Per 10 dagen een extra punt per dag
                        punten[index] += ((streak[index] - (streak[index] % 10)) / 10) + 1;
                        //Stuur terug naar arduino
                        serialPort.Write($"#CheckIn {naam[index]} {streak[index]} {punten[index]} %");
                        numUpDown_Streak0.Invoke((MethodInvoker)delegate { numUpDown_Streak0.Value = streak[0]; });
                        numUpDown_Streak1.Invoke((MethodInvoker)delegate { numUpDown_Streak1.Value = streak[1]; });
                        numUpDown_TotalPoints0.Invoke((MethodInvoker)delegate { numUpDown_TotalPoints0.Value = punten[0]; });
                        numUpDown_TotalPoints1.Invoke((MethodInvoker)delegate { numUpDown_TotalPoints1.Value = punten[1]; });
                    }
                    if (serialInput == "CheckIn")
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
                                    index = 1;
                                }
                                else if (serialInput.Substring(1) == "CA 27 61 1F")
                                {
                                    index = 0;
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