# HG: DSL for Html Generation


HG, abuses operator overloads in C# to implement DSL (Domain Specific Language) for HTML generation.


It is very early stage. It does not support 

- Self closing tags
- Attributes

yet.

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