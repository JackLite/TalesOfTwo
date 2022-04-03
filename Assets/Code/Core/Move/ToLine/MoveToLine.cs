using UnityEngine;

namespace FlyAdventure.Core.Move.ToLine
{
    public struct MoveToLine
    {
        public int fromLine;
        public int toLine;
        public AnimationCurve ease;
        public float remain; // from 1 to 0
    }
}