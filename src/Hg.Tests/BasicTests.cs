namespace Hg.Tests;

public class BasicTests
{
    [Fact]
    public void Addition_1()
    {
        var node1 = new Node("div");
        var node2 = new Node("div");
        var result = node1 + node2;
        result.ShouldContain(node1);
        result.ShouldContain(node2);
    }

    [Fact]
    public void Addition_2()
    {
        var result = new Node("div") + new Node("div") + new Node("div");
        result.Count.ShouldBe(3);

        result = new Node("div") + (new Node("div") + new Node("div"));
        result.Count.ShouldBe(3);

        result = (new Node("div") + new Node("div")) + (new Node("div") + new Node("div"));
        result.Count.ShouldBe(4);
    }

    [Fact]
    public void AddChildren()
    {
        var node = new Node("div");
        var child = new Node("div");
        var result = node > child;
        result.Parent.Children.Count.ShouldBe(1);

        result = new Node("div") > new Node("div") > new Node("div");
        result.Parent.Children.Count.ShouldBe(1);
        result.Parent.Parent.Children.Count.ShouldBe(1);
        result.Parent.Parent.Parent.ShouldBeNull();
    }

    [Fact]
    public void ComplexOperation_1()
    {
        var result = new Node("div") > new Node("div") + new Node("div");
        result.Children.Count().ShouldBe(2);
    }

    [Fact]
    public void ComplexOperation_2()
    {
        var result = new Node("div") { Text = "Root" } >
            new Node("div") { Text = "ul" } > new Node("div") { Text = "Li #1" }
            + new Node("div") { Text = "Li #2" }
            > new Node("div") { Text = "<div>" };
        result.Parent.Children.Count().ShouldBe(3);
        result.Parent.Children.Last().Text.ShouldBe("<div>");
    }

    [Fact]
    public void Compose_Ul_1()
    {
        var result = new Node("div") { Text = "ul" } >
            new Node("div") { Text = "<li>" } / "Item #1"
            + new Node("div") { Text = "<li>" } / "Item #2"
            + new Node("div") { Text = "<li>" } / "Item #3";
        result.Children.Count.ShouldBe(3);
    }

    [Fact]
    public void Compose_Ul_2()
    {
        var result = new Node("div") { Text = "ul" } >
            new Node("div") { Text = "<li>" } > new Node("div") { Text = "<em>Bold</em>" }
            + new Node("div") { Text = "<li>" } / "Item #2"
            + new Node("div") { Text = "<li>" } / "Item #3";

        result.Children.Count.ShouldBe(3);
        result.Children.First().Text.ShouldBe("<em>Bold</em>");
        result.Children.Skip(1).First().Text.ShouldBe("Item #2");
        result.Children.Skip(2).First().Text.ShouldBe("Item #3");
    }

    [Fact]
    public void Adding_A_New_Node_Should_Clear_Text()
    {
        var result = new Node("div") { Text = "<p>" } / "Hello"
            > new Node("div") { Text = "<pre>" } / "World";
        result.Parent.Text.ShouldBeNull();
        result.Parent.Children.First().Text.ShouldBe("World");
    }
}
