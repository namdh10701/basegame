// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("+8LlYwMre1bpJHKhqq8l1yyO/4ao80cXfTw+i94HTvXNBcRWIebnOEZJasYIxrdNQUFBRUBDwkFPQHDCutcN8rnyuX/pWLnUZKkMmq1E7xa4ExvGcIYkVjfkJW+wsQ53msywgianDrH6eq3zEI9vkWnxtSQlmtzKU7NU4UP3cZorVokBi0MlkTgBV/SVH7bz7gBv/O2xlxr6hf6Cga9IV/n9nYH8SKjzRxd9PD6L3gdO9c0FDPlJGnI1BlE8/hMDX675/Z2B/EguLLBGMCMVJjYOj83K5pUftvPuAEwpbGsvTK50T6cdwEZd41AcoIjzLbteDT8rTC0uY6UI9yf4zZvdgaJMLS5jpQj3J/jNm92BojroU/yfOcRWIebnOLrXDfK58rl/6Vi51GSpJFY35CVvsLEOd5rMsIItu14NPytv/O2xlxr6hf6Cga9IV7gTG8ZwhkmzKVTKwgz5SRpyNQZRPP4TA1+uOuhT/J85itaBQkNBQEFwwkFicE1xmitWiQGLQyWROAFX9EwpbGsvTK3zEI9vkWnxtSQlmtzK9JJk0uTScMJBYnBNRklqxgjGt01BQUFFQENBSkLCQUFA8QM2B06IjUmzKVTKwq50T6cdwEZd41AcoIjzJqcOsfp6DJqtRO8WhLm7W+abMrp6D6zFFM/CQU9AcMJBSkLCQUFA8QM2B06IjSXXLI7/hi4ssEYwIxUmNg6Pzcrm9JJk0uTS+8LlYwMre1bpJHKhqq+Eubtb5psyunoPrMUUz1OzVOFD94rWgUJDQUBB");
        private static int[] order = new int[] { 24,18,15,19,12,8,6,11,19,17,25,11,13,28,28,27,26,27,28,21,23,24,26,26,24,28,28,27,28,29 };
        private static int key = 64;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
