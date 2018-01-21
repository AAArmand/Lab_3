using System.Collections.Generic;

namespace GraphicsEditor.Commands.Data
{
    public class IndexesComparer : IEqualityComparer<uint[]>
    {
        public bool Equals(uint[] x, uint[] y)
        {
            if (x.Length == y.Length)
            {
                for (int count = 0; count < x.Length; count++)
                {
                    if (x[count] != y[count])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        public int GetHashCode(uint[] obj)
        {
            uint hCode = 0;
            
            //зачем исключающее ИЛИ
            foreach (uint variable in obj)
            {
                hCode ^= variable;
            }
            
            return hCode.GetHashCode();
        }
    }
}