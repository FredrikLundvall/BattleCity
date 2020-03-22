using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCity
{
    public static class Similar
    {
        public static bool AreSimiliar(float a, float b, float delta = 0.0001f)
        {
            return Math.Abs(a - b) < delta;
        }
    }
}
