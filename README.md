# example-lync
#### Examples using Lync
https://teamtreehouse.com/library/querying-with-linq/

#### What You'll Learn ####
- Lync Queries
- Delegates Types (part of functional programming), turning functionals into variables to be passed around.

### Lync ###

```csharp
List<int> numbers = new List<int> { 2, 4, 8, 16, 32, 64 };
List<int> numbersGreaterThanTen = new List<int>();

// traditional way
foreach (int number in numbers) {
	numbersGreaterThanTen.Add(number); // 16, 32, 64
}

// lync way
// a range variable - a source -  a query - a result
from number in numbers where number > 10 select number;

from n in numbers where n > 10 select n;

from n in numbers
where n > 10
select n;

// Link queries always return an enumerable of the type that was in the original collection.  They are not evaluated until it is enumerated over. (Deffered execution)

IEnumerable<int> numbersGreaterThanTen = from n in numbers
where n > 10
select n;
numbersGreaterThanTen.Count(); //3
```

```csharp
using System.Collections.Generic;
using System.Linq;


namespace Treehouse.CodeChallenges
{
    public class NumberAnalysis
    {
        private List<int> _numbers;
        public NumberAnalysis()
        {
            _numbers = new List<int> { 2, 4, 6, 8, 10 };
        }
        
        public IEnumerable<int> NumbersGreaterThanFive() {
             return from n in _numbers where n > 5 select n;
             // short form
             //return _numbers.Where(n => n > 5);
        }
    }
}
```

```csharp
public class Bird {
  public string Name { get; set; }
  public string Color { get; set; }
  public string Sightings { get; set; }
}

List<Bird> birds = new List<Bird> {
	new Bird { Name = "Cardinal", Color = "Red", Sightings = 3 },
    new Bird { Name = "Dove", "Color" = "White", Sightings = 2 }
};

birds.Add(new Bird { Name = "Robin", Color = "Red", Sightings = 5 });

Bird blueJay = new Bird();
blueJay.Name = "Blue Jay";
blueJay.Color = "Blue";
blueJay.Sightings = 1;
birds.Add(blueJay);

from b in birds where b.Color == "Red" select b.Name; //Cardinal, Robin

from b in birds where b.Color == "Red" select new { b.Name, b.Color }; // Anonymous type same as below where creatig an anonymous type
var anonymousCrow = new { Name = "Crow", Color = "Black", Sightings = 11 };

// to add anonymous type to bird list
birds.Add(new Bird { Name = anonymousCrow.Name, Color = anonymousCrow.Color, Sightings = anonymousCrow.Sightings });

// ordering
from b in birds
orderby b.Name descending
select b.Name

// grouping
var results = from b in birds
group b by b.Color into g
select new { Name = g.Name };

```

```csharp
var birds = new[] 
{ 
    new { Name = "Pelican", Color = "White" }, 
    new { Name = "Swan", Color = "White" }, 
    new { Name = "Crow", Color = "Black" } 
};

var mysteryBird = new { Color = "White", Sightings = 3};

//match birds in the list to the mystery bird
var matchingBirds = from s in birds where s.Color == mysteryBird.Color select new { BirdName=s.Name };
```

### Delegates ###
