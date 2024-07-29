// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("wkPqVR6eSRf0a4t1jRVRwMF+OC5c9/8ilGLAstMAwYtUVeqTfihUZialq6SUJqWupialpaQV59LjqmxpILLFAgPcXjPpFl0WXZsNvF0wgE2ozYiPy6hKkKtD+SSiuQe0+ERsF+h+SaAL8mBdX78Cf9ZenutIIfArHRl5ZRisTBej85nY2m8646oRKeHBM8hqG2LKyFSi1MfxwtLqaykuArdXsAWnE5V+z7Jt5W+nwXXc5bMQlCalhpSpoq2OIuwiU6mlpaWhpKcQdoA2ADYfJgGH58+fsg3AlkVOS3H7UhcK5IsYCVVz/h5hGmZlS6yzrVfNsC4m6B2t/pbR4rXYGvfnu0rJX7rp28+oycqHQewTwxwpfzllRt4Mtxh73W4yZaanpaSl");
        private static int[] order = new int[] { 8,12,12,4,7,5,7,10,10,10,10,11,12,13,14 };
        private static int key = 164;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
