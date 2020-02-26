using System;
using System.Text;

namespace SSC.Shared.Util
{
    public static class StringUtil
    {
        private static readonly Random rng = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
        private static readonly string PROJECT_NAME = "ssc-race";

        public static string GetRandomHexString(int length)
        {
            int max = (int)Math.Pow(16, length);
            int random = rng.Next(0, max);
            return random.ToString("X").ToLower();
        }

        public static string GetEventName<T>(string response = "") where T : Delegate
        {
            Type t = typeof(T);
            StringBuilder eventNameBuilder = new StringBuilder();
            eventNameBuilder.Append(PROJECT_NAME);
            eventNameBuilder.Append("::");
            eventNameBuilder.Append(t.Name);

            if (!string.IsNullOrEmpty(response))
            {
                eventNameBuilder.Append("#");
                eventNameBuilder.Append(response);
            }

            return eventNameBuilder.ToString();
        }
    }
}
