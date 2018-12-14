# Condorcet Library

A C# library that implements various Condorcet voting algorithms.

[Hosted by GitHub](https://github.com/Perlkonig/Condorcet)

## Ballots

All the algorithms use the same ballot: an dictionary whose key is the candidate name (of type `T`, which must implement `IComparable`) and whose value is the rank (which must be an unsigned integer). Typically, the absolute values of the ranks is irrelevant. All that matters is their sequence. For example, you could rank your first choice as `100` as long as your next choice was something like `110` and so on down the line. But this might differ by algorithm. Traditionally you indicate your first choice with a `1` and go from there. You do *not* need to vote for all candidates. Skipping a candidate simply means they're at the very bottom of your list.

Here's an example of a ballot ranking five candidates in the order ACBED:

```csharp
Dictionary<char, uint> b1 = new Dictionary<char, uint>
{
    {'A', 1},
    {'C', 2},
    {'B', 3},
    {'E', 4},
    {'D', 5}
};
```

## Available Algorithms

For all algorithms, the constructor takes the list of candidates, after which you can use the `AddBallot` method. You can then call the `Rank` method to get a list of candidates in order, winner first. 

### Schulze

A description of the method can be found on [Wikipedia](https://en.wikipedia.org/wiki/Schulze_method).

```csharp
using System.Collections.Generic;
using Condorcet;

HashSet<char> candidates = new HashSet<char> {'A', 'B', 'C', 'D', 'E'};

//Build the ballot
Dictionary<char, uint> ballot = new Dictionary<char, uint>
{
    {'A', 1},
    {'C', 2},
    {'B', 3},
    {'E', 4},
    {'D', 5}
};

//Construct the Schulze object with the list of candidates
Schulze<char> s = new Schulze<char>(candidates);

//Assume 50 people all voted the same way
s.AddBallot(ballot, 50);

//Get the rankings
char[] ranked = s.Rank();

//Because everybody voted unanimously, you should get the array ['A', 'C', 'B', 'E', 'D'].
//See the tests for more examples.
```

### Ranked Pairs

A description of the method can be found on [Wikipedia](https://en.wikipedia.org/wiki/Ranked_pairs).

```csharp
using System.Collections.Generic;
using Condorcet;

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

//Because everybody voted unanimously, you should get the array ["Chattanooga", "Knoxville", "Nashville", "Memphis"].
//See the tests for more examples.
```

## TODO

* I'm a novice programmer. Any suggestions to improve efficiency or readability would be warmly welcomed.
* I'm always looking for more test cases. I need to add code for ties.
