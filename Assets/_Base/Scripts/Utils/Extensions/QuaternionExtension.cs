using UnityEngine;

namespace _Base.Scripts.Utils.Extensions
{
    public static class QuaternionExtension
    {
        public static Quaternion Rotate(this Quaternion src, float angle) =>
            Quaternion.AngleAxis(-angle, Vector3.forward) * src;
    }
}