using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortTester
{
    public class Program
    {
        static void Main(string[] args)
        {
            CreateFolder();
            SerialPort _serialPort = new SerialPort();
            string[] portNames = SerialPort.GetPortNames();
            string currentProcess = Directory.GetCurrentDirectory() + "\\Logs";
            string path = currentProcess + "\\Logs.log";
            
            Console.WriteLine("Searching for COM ports...");
            Console.WriteLine("");
            foreach (string port in portNames)
            {
                try
                {
                    StringBuilder sb = new StringBuilder();
                    SerialPort cereal = new SerialPort(port);
                    sb.AppendLine("\n");
                    sb.AppendLine($"Port Name: {port}");
                    sb.AppendLine($"Baud Rate: {cereal.BaudRate}");
                    sb.AppendLine($"Data Bits: {cereal.DataBits}");
                    sb.AppendLine($"Parity: {cereal.Parity}");
                    sb.AppendLine($"Stop Bits: {cereal.StopBits}");
                    sb.AppendLine($"Handshake: {cereal.Handshake}");
                    sb.AppendLine($"Read Timeout: {cereal.ReadTimeout}");
                    sb.AppendLine($"Write Timeout: {cereal.WriteTimeout}");
                    sb.AppendLine("\n");
                    System.IO.File.AppendAllText(path, sb.ToString());

                    _serialPort.PortName = port;
                    _serialPort.BaudRate = cereal.BaudRate;
                    _serialPort.DataBits = cereal.DataBits;
                    _serialPort.Handshake = cereal.Handshake;
                    _serialPort.Parity = cereal.Parity;
                    _serialPort.StopBits = cereal.StopBits;

                    _serialPort.Open();
                    Console.WriteLine($"\nSerial port open {port}");
                    System.IO.File.AppendAllText(path, $"Serial port open {port}");
                    _serialPort.Close();
                    Console.WriteLine($"Serial port closed {port}");
                    System.IO.File.AppendAllText(path, $"\nSerial port closed {port}");
                }
                catch (ArgumentException ex)
                {
                    System.IO.File.AppendAllText(path, ex.ToString());
                }
                catch (Exception ex)
                {
                    System.IO.File.AppendAllText(path, ex.ToString());
                }
            }
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("Search done. Please see logs for details.");
            Console.WriteLine("Ang pogi ko");
            Console.WriteLine("Press enter to exit");
            Console.Read();

            Process.Start("explorer.exe", currentProcess);
        }

        public static void CreateFolder()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

        }
    }
}
