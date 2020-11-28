using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoxFiretail.Scripts.Core
{
    public partial class GameManager : IGameCommon
    {
        public long FiveCC(string s)
        {
            if (s.Length != 5)
                throw new Exception("String Meta ID request is 5 symbols");
            return (s[0]) | (s[1] << 8) | (s[2] << 16) | (s[3] << 24) | (s[4] << 32);
        }
    }
}
