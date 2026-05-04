namespace Spirograph_v1
{
    public sealed class Singleton
    {
        private static Singleton instance = null;
        private static readonly object padlock = new object();

        Singleton()
        {
        }

        public static Singleton Instance
        {
            get
            {
                lock (padlock)
                {
                    instance ??= new Singleton();
                    return instance;
                }
            }
        }

    }   // class Singleton

}   // namespame Spirograph_v1
