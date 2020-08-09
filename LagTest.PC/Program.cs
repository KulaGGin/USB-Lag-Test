using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LagTest.PC {
    class Program {
        static void Main(string[] args) {
            string testText = "Test Lag.";

            Stopwatch watch;
            List<double> results = new List<double>();
            List<string> portNames = SerialPort.GetPortNames().ToList();
            Console.WriteLine("Available COM Ports:");
            string portsString = portNames.Aggregate((concat, port) => $"{concat}, {port}");
            Console.WriteLine(portsString);
            Console.WriteLine("Write name of the COM Port to use(leave empty to use first available): ");
            string usedPort = Console.ReadLine();
            if(usedPort == "")
                usedPort = portNames[0];

            SerialPort serialPort = new SerialPort { PortName = usedPort, BaudRate = 115200, RtsEnable = true };

            try {
                serialPort.Open();
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }

            for(int testNumber = 0; testNumber < 1000; ++testNumber) {
                serialPort.DiscardInBuffer();

                watch = Stopwatch.StartNew();
                serialPort.WriteLine(testText);

                Console.ReadKey();

                watch.Stop();

                double millisecondsPassed = Math.Round(((double)watch.ElapsedTicks / (double)TimeSpan.TicksPerMillisecond), 3);
                results.Add(millisecondsPassed);
                Console.WriteLine($"{millisecondsPassed}");
            }

            Console.Clear();
            results.ForEach(result => Console.WriteLine(result));
            serialPort.Close();
            Console.ReadKey();
        }
    }
}
