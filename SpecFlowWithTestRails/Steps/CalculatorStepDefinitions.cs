using Xunit;

namespace SpecFlowWithTestRails.Steps;

[Binding]
public class CalculatorStepDefinitions
{
    private int _result;

    private readonly Calculator _calculator;

    public CalculatorStepDefinitions(Calculator calculator)
    {
        _calculator = calculator;
    }

    [Given("the first number is (.*)")]
    public void GivenTheFirstNumberIs(int number)
    {
        _calculator.FirstNumber = number;
    }

    [Given("the second number is (.*)")]
    public void GivenTheSecondNumberIs(int number)
    {
        _calculator.SecondNumber = number;
    }

    [When("the two numbers are added")]
    public void WhenTheTwoNumbersAreAdded()
    {
        _result = _calculator.Add();
    }

    [When("the two numbers are subtracted")]
    public void WhenTheTwoNumbersAreSubtracted()
    {
        _result = _calculator.Minus();
    }

    [Then("the result should be (.*)")]
    public void ThenTheResultShouldBe(int result)
    {
        Assert.Equal(_result, result);
    }

    [Then(@"Test Table:")]
    public void ThenTestTable(Table table)
    {

    }
}
