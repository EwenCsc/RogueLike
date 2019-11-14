using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapRogueLike.V2
{
    class MathHelper
    {
        public static float Lerp(float _start, float _end, float _porcent)
        {
            return _start * (1 - _porcent) + _end * _porcent;
        }
    }
}
