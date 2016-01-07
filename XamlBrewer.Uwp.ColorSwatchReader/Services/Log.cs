using System.Diagnostics;

namespace Mvvm.Services
{
    public class Log
    {
        public static void Error(string message)
        {
            Debug.WriteLine("Error: " + message);
        }
        public static void Error(string message, string method)
        {
            Debug.WriteLine("Error in '" + method + "': " + message);
        }
    }
}
