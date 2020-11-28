using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NoxFiretail.Scripts.Core.GameCommon;

namespace NoxFiretail.Scripts.Core
{
    /// <summary>
    /// Common API for general purpose.<br />
    /// Includes every existing function offered by NoxFiretail 
    /// </summary>
    public interface IGameCommon
    {
        /// <summary>
        /// Converts Meta String ID into MID.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        long FiveCC(string s);

        /// <summary>
        /// Call this to serialize all data
        /// </summary>
        /// <param name="versionID"></param>
        void SerializeLiveData(long versionID = 1, string path = "/test.sif");


    }
}
