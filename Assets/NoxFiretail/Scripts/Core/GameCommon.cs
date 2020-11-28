using NoxFiretail.Scripts.Serializer;
using System;
using System.IO;
using System.Numerics;
using UnityEngine;

namespace NoxFiretail.Scripts.Core
{
    /// <summary>
    /// Global common data wrappers offered by NoxFiretail.<para></para>
    /// Offers:
    /// <list type="bullet">
    /// <item>timers</item>
    /// <item>primitives</item>
    /// <item>special fields</item>
    /// </list>
    /// </summary>
    public partial class GameCommon
    {
        /// <summary>
        /// Normally false, but once in the entire lifetime of the produc this will be true.
        /// </summary>
        public static bool FirestEverRun = false;
        public static bool InstanceDeserialized = false;

        /// <summary>
        /// Entire NoxFiretail library API
        /// </summary>
        public static IGameCommon IGC;
        /// <summary>
        /// Entire NoxFiretail library API
        /// </summary>
        public static IGameCommon API { get => IGC; }

    }
}