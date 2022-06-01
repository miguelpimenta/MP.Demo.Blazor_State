using BlazorState;
using Bunit;
using FluentAssertions;
using MediatR;
using MP.BlazorStateDemo.Components;
using MP.BlazorStateDemo.Tests.Extensions;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;

namespace MP.BlazorStateDemo.Tests.Components;

[Trait("Category", "Counter Increment/Decrement")]
public class CounterComponentTests : TestContext
{
    private ITestOutputHelper _testOutput;

    public CounterComponentTests(
        ITestOutputHelper testOutput)
    {
        _testOutput = testOutput;
    }

    private static TestContext GetTestContext()
    {
        var assembly = typeof(MP.BlazorStateDemo.Core.Application.Features.Counter.CounterState)
            .GetTypeInfo().Assembly;

        var ctx = new TestContext();

        ctx.Services
            .AddMediatR(assembly);

        ctx.Services
            .AddBlazorState
            (
                (opt) => opt.Assemblies = new Assembly[] { assembly }
            );

        return ctx;
    }

    [Fact(DisplayName = "Check HTML Markup"), Order(2),]
    public void CheckHtmlMarkup()
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        _testOutput
            .WriteLine(cut.Markup);

        cut.MarkupMatches("" +
            "<div>" +
            "<h1>Counter</h1>  " +
            "<p id =\"countresult\">Current count: 0</p>" +
            "<button class=\"btn btn-warning\" id=\"btnDecrementCount\" blazor:onclick=\"1\">Decrement</button>" +
            "<button class=\"btn btn-primary\" id=\"btnIncrementCount\" blazor:onclick=\"2\">Increment</button>" +
            "</div>");
    }

    [Fact(DisplayName = "Check Buttons (Increment/Decrement)"), Order(2),]
    public void CheckIfButtonsExist()
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        var buttons = cut
            .FindAll("button");

        buttons[0].TextContent
            .Should()
            .Contain("Decrement");

        buttons[1].TextContent
            .Should()
            .Contain("Increment");
    }

    [Fact(DisplayName = "Increment Count"), Order(3),]
    public void IncrementCountExpectedBehavior()
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        cut.Markup
            .Contains("Current count: 0");

        cut
           .Find("#countresult")
           .MarkupMatches("<p id=\"countresult\">Current count: 0</p>");

        var btnIncrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnIncrementCount"));

        btnIncrement.Click();

        cut
            .Find("#countresult")
            .TextContent
            .Should()
            .Be("Current count: 1");
    }

    [Fact(DisplayName = "Decrement Count"), Order(4)]
    public void DecrementCountExpectedBehavior()
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        cut.Markup
            .Contains("Current count: 0");

        cut
           .Find("#countresult")
           .MarkupMatches("<p id=\"countresult\">Current count: 0</p>");

        var btnDecrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnDecrementCount"));

        btnDecrement.Click();

        cut
            .Find("#countresult").TextContent
            .Should()
            .Be("Current count: -1");
    }

    [Theory(DisplayName = "Increment Count Multiple Times"), Order(5)]
    [InlineData(5, 5)]
    [InlineData(10, 10)]
    [InlineData(15, 15)]
    [InlineData(30, 30)]
    [InlineData(60, 60)]
    public void IncrementCountMultipleTimesExpectedBehavior(int clicks, int countResult)
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        cut.Markup
            .Contains("Current count: 0");

        var btnIncrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnIncrementCount"));

        1.UpTo(clicks, (_) => btnIncrement.Click());

        cut
            .Find("#countresult").TextContent
            .Should()
            .Be($"Current count: {countResult}");
    }

    [Theory(DisplayName = "Decrement Count Multiple Times"), Order(6)]
    [InlineData(5, -5)]
    [InlineData(10, -10)]
    [InlineData(15, -15)]
    [InlineData(30, -30)]
    [InlineData(60, -60)]
    public void DecrementCountMultipleTimesExpectedBehavior(int clicks, int countResult)
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        cut.Markup
            .Contains("Current count: 0");

        var btnDecrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnDecrementCount"));

        1.UpTo(clicks, (_) => btnDecrement.Click());

        cut
            .Find("#countresult").TextContent
            .Should()
            .Be($"Current count: {countResult}");
    }

    [Theory(DisplayName = "Increment/Decrement Count Multiple Times"), Order(7)]
    [InlineData(5, 15, -10)]
    [InlineData(10, 10, 0)]
    [InlineData(15, 5, 10)]
    [InlineData(0, 30, -30)]
    [InlineData(60, 30, 30)]
    public void IncrementDecrementCountMultipleTimesExpectedBehavior(
        int incrementClicks,
        int decrementClicks,
        int countResult)
    {
        using var ctx = GetTestContext();

        IRenderedFragment cut = ctx
            .RenderComponent<CounterComponent>();

        cut.Markup
            .Contains("Current count: 0");

        var btnIncrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnIncrementCount"));

        1.UpTo(incrementClicks, (_) => btnIncrement.Click());

        var btnDecrement = cut
            .FindAll("button")
            .SingleOrDefault(b => b.Id.Equals("btnDecrementCount"));

        1.UpTo(decrementClicks, (_) => btnDecrement.Click());

        cut
            .Find("#countresult").TextContent
            .Should()
            .Be($"Current count: {countResult}");
    }
}