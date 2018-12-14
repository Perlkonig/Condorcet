using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Condorcet
{
    public class RankedPair<T> : CondorcetBase<T> where T : IComparable
    {
        public RankedPair(HashSet<T> candidates) : base(candidates) {}

        private struct Result
        {
            public uint majority;
            public uint opposition;
        }

        private Dictionary<Tuple<T,T>, Result> calcPairs(Dictionary<T, Dictionary<T, uint>> d)
        {
            Dictionary<Tuple<T,T>, Result> pairs = new Dictionary<Tuple<T, T>, Result>();
            foreach (var c1 in candidates)
            {
                foreach (var c2 in candidates)
                {
                    if (c1.CompareTo(c2) != 0)
                    {
                        if (d[c1][c2] > d[c2][c1])
                        {
                            Tuple<T,T> pair = new Tuple<T, T>(c1, c2);
                            Result r = new Result
                            {
                                majority = d[c1][c2],
                                opposition = d[c2][c1]
                            };
                            pairs[pair] = r;
                        }
                    }
                }
            }
            return pairs;
        }

        public override T[] Rank()
        {
            //Tally
            var d = CalcD();
            
            //Sort
            var pairs = calcPairs(d);
            Tuple<T,T>[] keys = pairs.Keys.ToArray();
            var sorted = keys.OrderByDescending(x => pairs[x].majority).ThenBy(x => pairs[x].opposition).ToArray();

            //Lock
            List<Tuple<T,T>> locked = new List<Tuple<T, T>>();
            foreach (var pair in sorted)
            {
                //Check for circularity
                //This is done by seeing if the loser is already locked in as a winner.
                //If so, skip this record if this pair's majority is greater than the loser's already locked in.
                bool circularity = false;
                foreach (var l in locked)
                {
                    if (pair.Item2.CompareTo(l.Item1) == 0)
                    {
                        if (pairs[pair].majority > pairs[l].majority)
                        {
                            circularity = true;
                            break;
                        }
                    }
                }
                if (!circularity)
                {
                    locked.Add(pair);
                }
            }

            List<T> ranked = new List<T>();
            //Do the following until you've exhausted the list
            while (locked.Count > 0)
            {
                //Find source of graph
                //The source node has no entry points (find the winner who never lost)
                HashSet<T> winners = new HashSet<T>(locked.Select(x => x.Item1));
                HashSet<T> losers = new HashSet<T>(locked.Select(x => x.Item2));
                T[] sources = winners.Except(losers).ToArray();
                if (sources.Length != 1)
                {
                    throw new InvalidOperationException("There was more than one source of the graph. This should never happen.");
                }
                T source = sources[0];

                //Add that winner to the rankings
                ranked.Add(source);

                //Now remove all entries from `locked` where `source` won and find the next source
                locked.RemoveAll(x => x.Item1.Equals(source));
            }

            //Check for missing candidates.
            //If found, add them to the bottom of the ranking in the order provided to the constructor.
            //Until I can expand the test cases, I'm restricting this to only missing one candidate.
            //This happens in unanimous cases, at least.
            Trace.Assert(candidates.Count - ranked.Count <= 1);
            if (ranked.Count < candidates.Count)
            {
                foreach (var c in candidates)
                {
                    if (! ranked.Contains(c))
                    {
                        ranked.Add(c);
                    }
                }
            }

            return ranked.ToArray();
        }

    }
}
