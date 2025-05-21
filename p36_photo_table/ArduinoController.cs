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
            serialPort.Parity = Parity.None;
            serialPort.DataBits = 8;
            serialPort.StopBits = StopBits.One;
            serialPort.Handshake = Handshake.None;
            serialPort.DtrEnable = true;
            serialPort.Open();

            Thread.Sleep(1000);
        }

        private string AutodetectArduinoPort()
        {

            // Replace the foreach loop in AutodetectArduinoPort with a version that uses timeouts and ReadExisting to avoid hanging
            foreach (string portname in SerialPort.GetPortNames())
            {
                Console.WriteLine("Checking port: " + portname);
                var sp = new SerialPort(portname, 9600);
                sp.Parity = Parity.None;
                sp.DataBits = 8;
                sp.StopBits = StopBits.One;
                sp.Handshake = Handshake.None;
                sp.DtrEnable = true;
                sp.WriteTimeout = 1000;
                sp.ReadTimeout = 1000; // Set a read timeout
                try
                {
                    sp.Open();
                    Thread.Sleep(1000);
                    sp.DiscardInBuffer();
                    sp.Write("ping");
                    string received = string.Empty;
                    var start = DateTime.Now;
                    while ((DateTime.Now - start).TotalMilliseconds < 1500)
                    {
                        try
                        {
                            received = sp.ReadLine();
                            break;
                        }
                        catch (TimeoutException)
                        {
                            // Ignore and retry until timeout
                        }
                        catch (InvalidOperationException)
                        {
                            // Port closed unexpectedly
                            break;
                        }
                    }
                    Console.WriteLine("-" + received + "-");

                    if (!string.IsNullOrEmpty(received) && received.Contains("pong"))
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
                    if (sp.IsOpen)
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
