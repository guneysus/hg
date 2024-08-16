namespace Hg.Tests;

public class AttributeTests
{
    [Fact]
    public void AddAttribute()
    {
        Node node = new Node("div") / ("id", "100");
        node.Attributes["id"].ShouldBe("100");
    }

    [Fact]
    public void Attr_Compose_ul_A_Little_Bit_Complex()
    {
        var cursor = ul / ("id", "app") >
                (li / ("class", "odd item-first") / "Item #1" > p > strong / "Replaced Item #1")
                + li / ("class", "even") / "Item #2"
                + li / ("class", "odd") / ("data-id", 100) / "Item #3";

        cursor.Children.Count().ShouldBe(3);
        cursor.Children.First().Text.ShouldBe("Replaced Item #1");
        cursor.Children.Skip(1).First().Text.ShouldBe("Item #2");
        cursor.Children.Skip(2).First().Text.ShouldBe("Item #3");

        cursor.GetRoot().Attributes["id"].ShouldBe("app");
        cursor.GetRoot().Children.First().Attributes["class"].ShouldBe("odd item-first");

        Node root = cursor.GetRoot();
        Node lastChild = root.Children.Last();
        lastChild.Attributes["class"].ShouldBe("odd");
    }
}