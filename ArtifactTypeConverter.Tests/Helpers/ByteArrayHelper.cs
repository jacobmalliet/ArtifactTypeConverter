using System.Text;

namespace ArtifactTypeConverter.Tests.Helpers
{
    public static class ByteArrayHelper
    {
        public static byte[] GetBytes(string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static string GetString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
