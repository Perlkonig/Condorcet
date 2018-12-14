using System;
using System.Collections.Generic;
using System.Linq;

namespace Condorcet
{
    public abstract class CondorcetBase<T> where T : IComparable
    {
        protected List<Dictionary<T, uint>> ballots;
        protected HashSet<T> candidates;

        public CondorcetBase(HashSet<T> candidates)
        {
            ballots = new List<Dictionary<T, uint>>();
            this.candidates = candidates;
        }

        public virtual void AddBallot(Dictionary<T, uint> ballot)
        {
            ballots.Add(ballot);
        }

        public virtual void AddBallot(Dictionary<T, uint> ballot, uint weight)
        {
            for (var i=0; i<weight; i++)
            {
                this.AddBallot(ballot);
            }
        }

        protected virtual Dictionary<T, Dictionary<T, uint>> CalcD()
        {
            Dictionary<T, Dictionary<T, uint>> d = new Dictionary<T, Dictionary<T, uint>>();
            foreach (var c1 in candidates)
            {
                foreach (var c2 in candidates)
                {
                    if (c1.CompareTo(c2) != 0)
                    {
                        if (! d.ContainsKey(c1))
                        {
                            d[c1] = new Dictionary<T, uint>();
                        }
                        d[c1].Add(c2,0);
                    }
                }
            }

            foreach (var ballot in ballots)
            {
                foreach (var c1 in candidates)
                {
                    foreach (var c2 in candidates)
                    {
                        if (c1.CompareTo(c2) != 0)
                        {
                            if (ballot.ContainsKey(c1))
                            {
                                if ( (! ballot.ContainsKey(c2)) || (ballot[c1] < ballot[c2]) )
                                {
                                    d[c1][c2]++;
                                }
                            }
                        }
                    }
                }
            }

            return d;
        }

        public abstract T[] Rank();
    }
}
