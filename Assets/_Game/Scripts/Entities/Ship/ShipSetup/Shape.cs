using System.Collections.Generic;

namespace _Game.Scripts
{
    public static class Shape
    {
        public static Dictionary<int, int[,]> ShapeDic = new Dictionary<int, int[,]>()
        {
            {
                0,
                    new int[,]
                    {
                        { 1 }
                    }
            },
            {
                1,
                    new int[,]
                    {
                        { 1 },
                        { 1 }
                    }
            },
            {
                2,
                    new int[,]
                    {
                        { 1 },
                        { 1 },
                        { 1 }
                    }
            },
            {
                3,
                    new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 }
                    }
            },
            {
                4,
                    new int[,]
                    {
                        { 1, 1 },
                        { 1 ,1 },
                        { 1 ,1 }
                    }
            }
        };
    }
}