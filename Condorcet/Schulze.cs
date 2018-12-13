using System;
using System.Collections.Generic;
using System.Linq;

namespace Condorcet
{
    public class Schulze<T> : CondorcetBase<T> where T : IComparable
    {
        public Schulze(HashSet<T> candidates) : base(candidates) {}

        private Dictionary<T, Dictionary<T, uint>> CalcP(Dictionary<T, Dictionary<T, uint>> d)
        {
            Dictionary<T, Dictionary<T, uint>> p = new Dictionary<T, Dictionary<T, uint>>();
            foreach (var c1 in candidates)
            {
                foreach (var c2 in candidates)
                {
                    if (c1.CompareTo(c2) != 0)
                    {
                        if (! p.ContainsKey(c1))
                        {
                            p[c1] = new Dictionary<T, uint>();
                        }
                        p[c1].Add(c2,0);
                    }
                }
            }

            foreach (var ci in candidates)
            {
                foreach (var cj in candidates)
                {
                    if (ci.CompareTo(cj) != 0)
                    {
                        if (d[ci][cj] > d[cj][ci])
                        {
                            p[ci][cj] = d[ci][cj];
                        }
                        // else
                        // {
                        //     p[ci][cj] = 0;
                        // }
                    }
                }
            }

            foreach (var ci in candidates)
            {
                foreach (var cj in candidates)
                {
                    if (ci.CompareTo(cj) != 0)
                    {
                        foreach (var ck in candidates)
                        {
                            if ( (ci.CompareTo(ck) != 0) && (cj.CompareTo(ck) != 0) )
                            {
                                p[cj][ck] = Math.Max(p[cj][ck], Math.Min(p[cj][ci], p[ci][ck]));
                            }
                        }
                    }
                }
            }
            return p;
        }

        private Dictionary<T, uint> CountWins(Dictionary<T, Dictionary<T, uint>> p)
        {
            Dictionary<T, uint> wins = new Dictionary<T, uint>();
            foreach (var c1 in candidates)
            {
                uint numwins = 0;
                foreach (var c2 in candidates)
                {
                    if (c1.CompareTo(c2) != 0)
                    {
                        uint c1score = p[c1][c2];
                        uint c2score = p[c2][c1];
                        if (c1score > c2score)
                        {
                            numwins++;
                        }
                    }
                }
                wins[c1] = numwins;
            }
            return wins;
        }

        public override T[] Rank()
        {
            //Calculate d[V,W]
            Dictionary<T, Dictionary<T, uint>> d = this.CalcD();

            //Calculate p[V,W]
            Dictionary<T, Dictionary<T, uint>> p = this.CalcP(d);

            //Count wins
            Dictionary<T, uint> wins = this.CountWins(p);

            //sort `candidates` by wins and return
            T[] ranked = candidates.ToArray();
            Array.Sort<T>(ranked, (x, y) => wins[y].CompareTo(wins[x]));
            return ranked;
        }

        public override Dictionary<T, uint> RankWithValues()
        {
            //Calculate d[V,W]
            Dictionary<T, Dictionary<T, uint>> d = this.CalcD();

            //Calculate p[V,W]
            Dictionary<T, Dictionary<T, uint>> p = this.CalcP(d);

            //Count wins
            Dictionary<T, uint> wins = this.CountWins(p);
            return wins;
        }
    }
}
