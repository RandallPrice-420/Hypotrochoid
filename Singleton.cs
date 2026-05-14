using System.Diagnostics;


namespace Spirograph_v1
{
    public sealed class Singleton
    {
        private static int Counter  = 0;     // Add 1 each time this class is created.

        private static readonly Singleton Instance = null;



        // Return the Instance (create it if necessary).  This version is not thread safe.
        public static Singleton GetInstance()
        {
            return Instance ?? new Singleton();
        }



        // Constructor is Private and can only be created in this class.
        private Singleton()
        {
            Counter++;

            Debug.Print($"Singleton - Counter {Counter.ToString()}");
        }


        // This is Public and accessed outside this class using GetInstance().
        public static void PrintDetails(string message)
        {
            Debug.Print(message);
        }


    }   // class Singleton

}   // namespame Spirograph_v1
