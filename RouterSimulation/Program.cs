using System;
using System.Collections.Generic;

namespace RouterSimulation
{
    class Program
    {
        // Define a simple routing table
        static Dictionary<string, string> routingTable = new Dictionary<string, string>
        {
            { "192.168.1.0/24", "Interface1" },
            { "192.168.2.0/24", "Interface2" },
            { "10.0.0.0/8", "Interface3" }
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Simple Router Simulation");
            Console.WriteLine("=========================");

            while (true)
            {
                // Simulate a packet
                Console.WriteLine("\nEnter a destination IP address (or type 'exit' to quit):");
                string destinationIP = Console.ReadLine();
                if (destinationIP?.ToLower() == "exit") break;

                // Process the packet
                string result = RoutePacket(destinationIP);
                Console.WriteLine(result);
            }
        }

        static string RoutePacket(string destinationIP)
        {
            Console.WriteLine($"\nProcessing packet for destination: {destinationIP}");

            // Find the matching route in the routing table
            foreach (var route in routingTable)
            {
                if (IsIPInSubnet(destinationIP, route.Key))
                {
                    return $"Packet routed to {route.Value} for subnet {route.Key}";
                }
            }

            // If no route is found
            return "No route to host. Packet dropped.";
        }

        static bool IsIPInSubnet(string ipAddress, string subnet)
        {
            // Split the subnet into address and prefix length (e.g., 192.168.1.0/24)
            var parts = subnet.Split('/');
            string subnetAddress = parts[0];
            int prefixLength = int.Parse(parts[1]);

            // Convert IP addresses to binary format
            var ipBinary = ConvertIPToBinary(ipAddress);
            var subnetBinary = ConvertIPToBinary(subnetAddress);

            // Compare the relevant prefix bits
            return ipBinary.Substring(0, prefixLength) == subnetBinary.Substring(0, prefixLength);
        }

        static string ConvertIPToBinary(string ipAddress)
        {
            var octets = ipAddress.Split('.');
            var binary = string.Empty;
            foreach (var octet in octets)
            {
                binary += Convert.ToString(int.Parse(octet), 2).PadLeft(8, '0');
            }
            return binary;
        }
    }
}
