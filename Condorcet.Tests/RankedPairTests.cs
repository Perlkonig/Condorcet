using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

using Condorcet;

namespace Condorcet.Tests
{
    public class RankedPairTests
    {
        [Fact]
        public void RankedPair_Wikipedia()
        {
            HashSet<string> candidates = new HashSet<string> {"Memphis", "Nashville", "Chattanooga", "Knoxville"};

            Dictionary<string, uint> b1 = new Dictionary<string, uint>
            {
                {"Memphis", 1},
                {"Nashville", 2},
                {"Chattanooga", 3},
                {"Knoxville", 4}
            };
            Dictionary<string, uint> b2 = new Dictionary<string, uint>
            {
                {"Nashville", 1},
                {"Chattanooga", 2},
                {"Knoxville", 3},
                {"Memphis", 4}
            };
            Dictionary<string, uint> b3 = new Dictionary<string, uint>
            {
                {"Chattanooga", 1},
                {"Knoxville", 2},
                {"Nashville", 3},
                {"Memphis", 4}
            };
            Dictionary<string, uint> b4 = new Dictionary<string, uint>
            {
                {"Knoxville", 1},
                {"Chattanooga", 2},
                {"Nashville", 3},
                {"Memphis", 4}
            };

            RankedPair<string> s = new RankedPair<string>(candidates);
            s.AddBallot(b1, 42);
            s.AddBallot(b2, 26);
            s.AddBallot(b3, 15);
            s.AddBallot(b4, 17);
            string[] expected = new string[] {"Nashville", "Chattanooga", "Knoxville", "Memphis"};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }

        [Fact]
        public void RankedPair_Wikipedia_DroppingLast()
        {
            HashSet<string> candidates = new HashSet<string> {"Memphis", "Nashville", "Chattanooga", "Knoxville"};

            Dictionary<string, uint> b1 = new Dictionary<string, uint>
            {
                {"Memphis", 1},
                {"Nashville", 2},
                {"Chattanooga", 3}
            };
            Dictionary<string, uint> b2 = new Dictionary<string, uint>
            {
                {"Nashville", 1},
                {"Chattanooga", 2},
                {"Knoxville", 3}
            };
            Dictionary<string, uint> b3 = new Dictionary<string, uint>
            {
                {"Chattanooga", 1},
                {"Knoxville", 2},
                {"Nashville", 3}
            };
            Dictionary<string, uint> b4 = new Dictionary<string, uint>
            {
                {"Knoxville", 1},
                {"Chattanooga", 2},
                {"Nashville", 3}
            };

            RankedPair<string> s = new RankedPair<string>(candidates);
            s.AddBallot(b1, 42);
            s.AddBallot(b2, 26);
            s.AddBallot(b3, 15);
            s.AddBallot(b4, 17);
            string[] expected = new string[] {"Nashville", "Chattanooga", "Knoxville", "Memphis"};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }

        [Fact]
        public void RankedPair_Unanimous()
        {
            HashSet<string> candidates = new HashSet<string> {"Memphis", "Nashville", "Chattanooga", "Knoxville"};

            Dictionary<string, uint> b1 = new Dictionary<string, uint>
            {
                {"Chattanooga", 1},
                {"Knoxville", 2},
                {"Nashville", 3},
                {"Memphis", 4}
            };

            RankedPair<string> s = new RankedPair<string>(candidates);
            s.AddBallot(b1, 42);
            string[] expected = new string[] {"Chattanooga", "Knoxville", "Nashville", "Memphis"};
            Assert.True(expected.SequenceEqual(s.Rank()));
        }    
    }
}
