using Shouldly;
using System.Text;
using Xunit.Abstractions;
using static Hg.HgExtensions;

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
}
