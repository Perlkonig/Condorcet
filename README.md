# Condorcet Library

A C# library that implements various Condorcet voting algorithms.

[Hosted by GitHub](https://github.com/Perlkonig)

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

### Schulze

A description of the method can be found on [Wikipedia](https://en.wikipedia.org/wiki/Schulze_method). The constructor takes the list of candidates, after which you can use the `AddBallot` method. You can then call the `Rank` method to get a list of candidates in order, winner first. You can also call `RankWithValues` if you want to see each candidate with their ranking.

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

## TODO

* I'm a novice programmer. Any suggestions to improve efficiency or readability would be warmly welcomed.
* I'm always looking for more test cases. I need to add code for ties.
