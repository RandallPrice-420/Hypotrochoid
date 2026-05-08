using System.Diagnostics;


namespace Spirograph_v1
{
    public sealed class Singleton
    {
        //This variable value will be increment by 1 each time the object of the class is created
        private static int Counter = 0;

        //This variable is going to store the Singleton Instance
        private static Singleton Instance = null;



        //The following Static Method is going to return the Singleton Instance
        public static Singleton GetInstance()
        {
            // If the instance is null, create the Singleton instance else return
            // the already created instance.  This version is not thread safe.
            if (Instance == null)
            {
                Instance = new Singleton();
            }

            //Return the Singleton Instance
            return Instance;
        }



        //Constructor is Private means, from outside the class we cannot create an instance of this class
        private Singleton()
        {
            //Each Time the Constructor called, increment the Counter value by 1
            Counter++;
            Debug.Print("Counter Value " + Counter.ToString());
        }


        //The following can be accesed from outside of the class by using the Singleton Instance
        public void PrintDetails(string message)
        {
            Debug.Print(message);
        }


    }   // class Singleton

}   // namespame Spirograph_v1
