using System;

namespace SSRC
{
    public static class StringHelper
    {
        private static Random rng = new Random();

        public static string GetRandomHexString(int length)
        {
            int max = (int)Math.Pow(16, length);
            int random = rng.Next(0, max);
            return random.ToString("X").ToLower();
        }

    }
}
