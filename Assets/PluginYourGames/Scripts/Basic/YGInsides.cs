namespace YG.Insides
{
    public static partial class YGInsides
    {
        public static InfoYG infoYG => YG2.infoYG;
        public static IPlatformsYG2 iPlatform => YG2.iPlatform;

        public static void Message(string message)
        {
            YG2.Message(message);
        }

        public static void GetDataInvoke()
        {
            YG2.GetDataInvoke();
        }
    }
}