using Shouldly;
using static Hg.HgExtensions;

namespace Hg.Tests;
public class DslTests
{
    [Fact]
    public void Compose_ul()
    {
        var cursor
            = ul >
                li / "Item #1"
                + li / "Item #2"
                + li / "Item #3";

        cursor.Children.Count().ShouldBe(3);
    }

    [Fact]
    public void Compose_ul_A_Little_Bit_Complex()
    {
        var cursor = ul >
                li / "Item #1" > p > strong / "Replaced Item #1"
                + li / "Item #2"
                + li / "Item #3";

        cursor.Children.Count().ShouldBe(3);
        cursor.Children.First().Text.ShouldBe("Replaced Item #1");
        cursor.Children.Skip(1).First().Text.ShouldBe("Item #2");
        cursor.Children.Skip(2).First().Text.ShouldBe("Item #3");
    }

    [Fact]
    public void Append_Later()
    {
        var cursor = ul >
                li / "Item #1" > p > strong / "Replaced Item #1"
                + li / "Item #2"
                + li / "Item #3";

        _ = cursor > li / "Item #4";

        cursor.Children.Count().ShouldBe(4);
        cursor.Children.Last().Text.ShouldBe("Item #4");

    }

    [Fact]
    public void Nested_Nodes()
    {
        var cursor = li / "Item #1" > p > strong / "Replaced Item #1";
        cursor.Children.Count().ShouldBe(0);
        cursor.Text.ShouldBe("Replaced Item #1");
    }

    [Fact]
    public void Root_Tests()
    {
        var cursor = li / "Item #1" > p > strong / "Replaced Item #1";
        cursor.Children.Count().ShouldBe(0);
        cursor.Text.ShouldBe("Replaced Item #1");


        cursor.GetRoot().TagName.ShouldBe("li");
        cursor.GetRoot().Children.Count().ShouldBe(1);
        cursor.GetRoot().Children.First().TagName.ShouldBe("p");
    }
}