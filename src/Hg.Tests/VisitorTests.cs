namespace Hg.Tests;

public class VisitorTests
{
    private readonly ITestOutputHelper Out;

    public VisitorTests(ITestOutputHelper @out)
    {
        this.Out = @out;
    }

    [Fact]
    public void Simple_Debug()
    {
        var cursor = ul >
                li / "Item #1"
                + li / "Item #2"
                + li / "Item #3";

        var sb = new StringBuilder();
        cursor.VisitTree(onEnter: node =>
        {
            sb.Append($"<{node.TagName}>");
        }, onExit: node =>
        {
            if (!string.IsNullOrEmpty(node.Text))
                sb.Append(node.Text);
            sb.Append($"</{node.TagName}>");
        });

        string html = sb.ToString();
        Out.WriteLine(html);
        html.ShouldBe("<ul><li>Item #1</li><li>Item #2</li><li>Item #3</li></ul>");
    }

    [Fact]
    public void Get_Html_Output()
    {
        var cursor = ul >
                li / "Item #1"
                + li / "Item #2"
                + li / "Item #3";
        string html = cursor.Html();
        html.ShouldBe("<ul><li>Item #1</li><li>Item #2</li><li>Item #3</li></ul>");
    }

    [Fact]
    public void Get_Html_Output_With_Attributes()
    {
        var cursor = ul / ("id", "app") >
                li / ("class", "item-1st") / "Item #1"
                + li / ("class", "item-2nd") / "Item #2"
                + li / ("class", "item-3rd") / "Item #3";

        //cursor.Attributes.Clear();

        string html = cursor.Html();
        html.ShouldBe("""<ul id="app"><li class="item-1st">Item #1</li><li class="item-2nd">Item #2</li><li class="item-3rd">Item #3</li></ul>""");
    }
}
