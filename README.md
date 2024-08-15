# HG: DSL for Html Generation

![Example](./img/img/hg_1)

HG, abuses operator overloads in C# to implement DSL (Domain Specific Language) for HTML generation.


It is very early stage. 

It does not support:
- Self closing tags
- Attributes

yet.

It has no nuget package (yet).

Just a fun project that might give some ideas how to ab(use) .NET's features.

## Usage

```csharp
using static Hg.HgExtensions;

var cursor = ul > 
  li / "Item #1"
  + li / "Item #2"
  + li / "Item #3";

var html = cursor.Html();


// <ul><li>Item #1</li><li>Item #2</li><li>Item #3</li></ul>

```

