using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using Condorcet;

namespace Condorcet.Tests
{
    public class SchulzeTests
    {
        [Fact]
        public void Schulze_Wikipedia()
        {
            HashSet<char> candidates = new HashSet<char> {'A', 'B', 'C', 'D', 'E'};

            Dictionary<char, uint> b1 = new Dictionary<char, uint>
            {
                {'A', 1},
                {'C', 2},
                {'B', 3},
                {'E', 4},
                {'D', 5}
            };
            Dictionary<char, uint> b2 = new Dictionary<char, uint>
            {
                {'A', 1},
                {'D', 2},
                {'E', 3},
                {'C', 4},
                {'B', 5}
            };
            Dictionary<char, uint> b3 = new Dictionary<char, uint>
            {
                {'B', 1},
                {'E', 2},
                {'D', 3},
                {'A', 4},
                {'C', 5}
            };
            Dictionary<char, uint> b4 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'A', 2},
                {'B', 3},
                {'E', 4},
                {'D', 5}
            };
            Dictionary<char, uint> b5 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'A', 2},
                {'E', 3},
                {'B', 4},
                {'D', 5}
            };
            Dictionary<char, uint> b6 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'B', 2},
                {'A', 3},
                {'D', 4},
                {'E', 5}
            };
            Dictionary<char, uint> b7 = new Dictionary<char, uint>
            {
                {'D', 1},
                {'E', 2},
                {'C', 3},
                {'B', 4},
                {'A', 5}
            };
            Dictionary<char, uint> b8 = new Dictionary<char, uint>
            {
                {'E', 1},
                {'B', 2},
                {'A', 3},
                {'D', 4},
                {'C', 5}
            };

            Schulze<char> s = new Schulze<char>(candidates);
            s.AddBallot(b1, 5);
            s.AddBallot(b2, 5);
            s.AddBallot(b3, 8);
            s.AddBallot(b4, 3);
            s.AddBallot(b5, 7);
            s.AddBallot(b6, 2);
            s.AddBallot(b7, 7);
            s.AddBallot(b8, 8);
            char[] expected = new char[] {'E', 'A', 'C', 'B', 'D'};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }

        [Fact]
        public void Schulze_Wikipedia_DroppingLast()
        {
            HashSet<char> candidates = new HashSet<char> {'A', 'B', 'C', 'D', 'E'};

            Dictionary<char, uint> b1 = new Dictionary<char, uint>
            {
                {'A', 1},
                {'C', 2},
                {'B', 3},
                {'E', 4}
            };
            Dictionary<char, uint> b2 = new Dictionary<char, uint>
            {
                {'A', 1},
                {'D', 2},
                {'E', 3},
                {'C', 4}
            };
            Dictionary<char, uint> b3 = new Dictionary<char, uint>
            {
                {'B', 1},
                {'E', 2},
                {'D', 3},
                {'A', 4}
            };
            Dictionary<char, uint> b4 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'A', 2},
                {'B', 3},
                {'E', 4}
            };
            Dictionary<char, uint> b5 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'A', 2},
                {'E', 3},
                {'B', 4}
            };
            Dictionary<char, uint> b6 = new Dictionary<char, uint>
            {
                {'C', 1},
                {'B', 2},
                {'A', 3},
                {'D', 4}
            };
            Dictionary<char, uint> b7 = new Dictionary<char, uint>
            {
                {'D', 1},
                {'E', 2},
                {'C', 3},
                {'B', 4}
            };
            Dictionary<char, uint> b8 = new Dictionary<char, uint>
            {
                {'E', 1},
                {'B', 2},
                {'A', 3},
                {'D', 4}
            };

            Schulze<char> s = new Schulze<char>(candidates);
            s.AddBallot(b1, 5);
            s.AddBallot(b2, 5);
            s.AddBallot(b3, 8);
            s.AddBallot(b4, 3);
            s.AddBallot(b5, 7);
            s.AddBallot(b6, 2);
            s.AddBallot(b7, 7);
            s.AddBallot(b8, 8);
            char[] expected = new char[] {'E', 'A', 'C', 'B', 'D'};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }

        [Fact]
        public void Schulze_Unanimous()
        {
            HashSet<char> candidates = new HashSet<char> {'A', 'B', 'C', 'D', 'E'};

            Dictionary<char, uint> b1 = new Dictionary<char, uint>
            {
                {'A', 1},
                {'C', 2},
                {'B', 3},
                {'E', 4},
                {'D', 5}
            };

            Schulze<char> s = new Schulze<char>(candidates);
            s.AddBallot(b1, 50);
            char[] expected = new char[] {'A', 'C', 'B', 'E', 'D'};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }    
    }
}
