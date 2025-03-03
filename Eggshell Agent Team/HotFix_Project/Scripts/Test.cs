using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotFix_Project.Scripts
{
    internal class Test
    {
        public static void TestA()
        {
            UnityEngine.Debug.Log("TestA");
        }
        public void TestB(int a)
        {
            UnityEngine.Debug.Log("TestB a=" + a);
        }
    }
}
