using System;
using System.Collections.Generic;
using System.Linq;

namespace Condorcet
{
    public class RankedPair<T> : CondorcetBase<T> where T : IComparable
    {
        public RankedPair(HashSet<T> candidates) : base(candidates) {}

        public override T[] Rank()
        {
            return new T[0];
        }

        public override Dictionary<T, uint> RankWithValues()
        {
            return new Dictionary<T, uint>();
        }
    }
}
