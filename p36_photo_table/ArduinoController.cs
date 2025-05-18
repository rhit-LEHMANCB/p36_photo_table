using System;
using System.IO.Ports;
using System.Threading;

namespace p36_photo_table
{
    public class ArduinoController
    {
        SerialPort serialPort;
        public ArduinoController()
        {
            string portName = AutodetectArduinoPort();
            Console.WriteLine("Arduino found on port: " + portName);
            if (portName == null)
            {
                throw new ArduinoNotFoundException();
            }
            serialPort = new SerialPort(portName, 9600);
            serialPort.Open();
        }

        private string AutodetectArduinoPort()
        {

            foreach (string portname in SerialPort.GetPortNames())
            {
                Console.WriteLine("Checking port: " + portname);
                var sp = new SerialPort(portname, 9600);
                sp.WriteTimeout = 1000;
                try
                {
                    sp.Open();
                    sp.Write("ping");
                    Thread.Sleep(500);
                    string received = sp.ReadLine();
                    Console.WriteLine("-" + received + "-");

                    if (received.Contains("pong"))
                    {
                        Console.WriteLine("device connected to: " + portname);
                        return portname;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("device NOT connected to: " + portname);
                }
                finally
                {
                    sp.Close();
                }
            }

            return null;
        }

        public void Home()
        {
            SendCommand("home");
            Console.WriteLine(WaitForResponse());
        }

        public void SendCommand(string command)
        {
            serialPort.Write(command);
        }

        internal string WaitForResponse()
        {
            while (true)
            {
                string response = serialPort.ReadLine();
                if (response != "")
                {
                    return response;
                }
            }
        }

        internal bool MoveMotors(int verticalSteps, int horizontalSteps, int tableSteps, int cameraSteps)
        {
            SendCommand($"{verticalSteps},{horizontalSteps},{tableSteps},{cameraSteps}");
            string response = WaitForResponse();
            if (response == "cancel")
            {
                return false;
            }

            Console.WriteLine(response);
            return true;
        }

        internal void CloseSession()
        {
            serialPort.Close();
        }
    }
}
