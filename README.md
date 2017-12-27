# example-lync
#### Examples using Lync
https://teamtreehouse.com/library/querying-with-linq/

#### What You'll Learn ####
- Lync Queries
- Delegates (aka Anonymous Functions in PHP) Types (part of functional programming), turning functionals into variables to be passed around.
- Lync Quantifiers Operators

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
- See example in *Program.cs*.
- `Delegates` = Anonymous Funcitons in PHP
- `Func` allows anonymous function with input parameters
- `Lambda` expression (x) `=>` x +2,`=>` is the lamba operator

```csharp
// lambda version
// public Func<int, int> Square = (number) => 
public Func<int, int> Square = delegate (int number)
{
    return number * number;
};

//  public Action<int, Func<int, int>> DisplayResult =  (result, function) =>
public Action<int, Func<int, int>> DisplayResult = delegate (int result, Func<int, int> function)
{
    Console.WriteLine(function(result));
};
```

### Quantifiers ###
- Lync has 3 types of quantifer operators
	- Any
	- All
	- Contains

```csharp
LoadAssembly("Birdwatcher.dll");

/*
var birds = new List<Bird>
{
    new Bird { Name = "Cardinal", Color = "Red", Sightings = 3 },
    new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
    new Bird { Name =  "Robin", Color = "Red", Sightings = 5 },
    new Bird { Name =  "Canary", Color = "Yellow", Sightings = 0 },
    new Bird { Name =  "Blue Jay", Color = "Blue", Sightings = 1 },
    new Bird { Name =  "Crow", Color = "Black", Sightings = 11 },
    new Bird { Name =  "Pidgeon", Color = "White", Sightings = 10 }
};
*/

using BirdWatcher;
var birds = BirdRepository.LoadBirds();
birds.Any(b => b.Name == "Crow"); //true - has bird with name = Crow
birds.Any(); //true - has birds
```

### Element Operators ###
- Single
- SingleOrDefault
- First
- FirstOrDefault
- Last
- LastOrDefault
- ElementAt
- ElementAtOrDefault

```csharp
birds.Where(b => b.Name == "Crow").Single();
birds.Single(b => b.Name == "Crow");
birds.SingleOfDefault(b => b.Name == "Chickadee");
birds.First();
birds.Last();
birds.First(b => b.Color == "Red");
```
- `SingleorDefault` if no match, provide default

### Partioning ###
- Take
- Skip
- TakeWhile
- SkipWhile
```csharp
birds.OrderBy(b =>b.Name.Length).Skip(6).Take(3);
birds.OrderBy(b =>b.Name.Length).TakeWhile(b => b.Name.Length < 6);
```

### Querying ###
```csharp
var colors = new List<string> { "Red", "Blue", "Purple" }
var favoriteBirds = from b in birds 
join c in colors or b.Color equals c 
select b;

//joins
var FavoriteBirds = birds.Join(colors,
b => b.color,
c => c,
(birds, color) => new { Color = color, Bird = bird });
)

var endangeredSightings = birds.Join(statuses, 
	b => b.ConservationStatus,
	s => s,
	(b,s) => new {
			Status = s,
			Sightings = b.Sightings 
		}.GroupBy(b => b.Status).Select(b => new {
				Status = b.Key,
				Sightings = b.Sum( s=> s.Sightings.Count()) 
			}
		);

// complex example
source.Where(s => search.CommonName == null || s.CommonName.Contains(search.CommonName))
.Where(s=> search.Country == null || s.Habitats.Any(h => h.Country.Contains(search.Country)))
.Where(s => search.Colors.Any(c => c == s.PrimaryColor) || search.Colors.Join(s.TertiaryColors,
	sc => sc,
	tc => tc,
	(sc, tv) => (sc)).Any()
	)
.Skip page * pageSize
.Take(20);

var myBirds = new List<Bird> 
{ 
    new Bird { Name = "Cardinal", Color = "Red", Sightings = 3 },
    new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
    new Bird { Name =  "Robin", Color = "Red", Sightings = 5 }
};

var yourBirds = new List<Bird> 
{ 
    new Bird { Name =  "Dove", Color = "White", Sightings = 2 },
    new Bird { Name =  "Robin", Color = "Red", Sightings = 5 },
    new Bird { Name =  "Canary", Color = "Yellow", Sightings = 0 }
};

// Create a variable named ourBirds and assign to it a LINQ query that is the result of a join from myBirds onto yourBirds using the Name property as the key. Make sure to return the birds that are the same between the two lists.
var ourBirds = myBirds.Join(yourBirds, b=> b.Name , n=> n.Name,  (bird, name) => bird);
```

### Aggregates ###
- Sum
- Count
- Min
- Max
- Average
```csharp
birds.Sum(b => b.Sightings);
```

### Set Operators ###
- except
- union
- intersect
- concat
```csharp
var colors = new List<string> { "Pink", "Blue", "Teal" };
colors.Except(birds.Select(b = > b.Color).Distinct());
```

### Generation Operators ###
- only works with int
- Enumerable
	- Empty
	- Range
	- Repeat
	- DefaultIfEmpty
```csharp
// instead of this
var numbers = new List<int>();
for( int i = 0; i < 10; i++) {
	numbers.Add(i);
}

// its this
var numbers = Enumerable.Range(10,10);
```

### Conversion Operator ###
- AsEnumerable
- Cast
- OfType
- ToArray
- ToDictionary
- ToList
- ToLookup

### Summary ###
- Quantifiers see if sequence fits a condition
- Elements - to pick
- Partiioning - to get subset
- Joins - joining
- Aggregate - to analyize
- Set Operator - remove duplicates and merging
- Generation - to generate
- Conversion - to convert to different sequencing