using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace Hg;

public class Node
{
    public Node(string tagName)
    {
        this.TagName = tagName;
    }

    protected Node() { }

    public string Text { get; set; }

    public NodeList Children { get; set; } = new NodeList();

    public Dictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    public Node Parent { get; set; }
    public string TagName { get; }

    public Node GetRoot()
    {
        var current = this;

        while (current.Parent != null)
            current = current.Parent;

        return current;
    }

    public static NodeList operator +(Node left, Node right) => new NodeList() { left, right };
    public static NodeList operator +(NodeList left, Node right)
    {
        left.Add(right);
        return left;
    }

    public static NodeList operator +(Node left, NodeList right)
    {
        right.Add(left);
        return right;
    }

    public static Node operator /(Node left, string text)
    {
        left.Text = text;
        return left;
    }

    public static Node operator /(Node left, (string, object) attr)
    {
        var (key, value) = attr;

        left.Attributes.Add(key, value);
        return left;
    }

    public static Node operator <(Node left, Node right) => throw new NotImplementedException();

    public static Node operator >(Node left, Node right)
    {
        left.ClearText();
        left.Children.Add(right);
        right.Parent = left;
        return right;
    }

    public static Node operator <(Node left, NodeList right) => throw new NotImplementedException();

    public static Node operator >(Node left, NodeList right)
    {
        left.ClearText();
        left.Children.AddRange(right);
        return left;
    }

    public void ClearText() => Text = null;

    public void VisitTree(Action<Node> onEnter, Action<Node> onExit, Node tree = default)
    {
        var current = tree ??= this.GetRoot();
        onEnter(current);

        if (current.Children.Any())
        {
            foreach (Node child in current.Children)
            {
                VisitTree(onEnter, onExit, child);
                current = child;
            }
        }

        onExit(tree);
    }

    public string Html()
    {
        var sb = new StringBuilder();
        this.GetRoot().VisitTree(onEnter: node =>
        {
            sb.Append($"<{node.TagName}");

            if (node.Attributes.Any())
            {
                foreach (var (k, v) in node.Attributes)
                {
                    sb.Append($" {k}=\"{v}\"");
                }
            }

            sb.Append(">");

        }, onExit: node =>
        {
            if (!string.IsNullOrEmpty(node.Text))
                sb.Append(node.Text);
            sb.Append($"</{node.TagName}>");
        });

        string html = sb.ToString();
        return html;
    }
}
