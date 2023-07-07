Feature: Calculator2

	@Issue_BQM-18272
	Scenario: Add two numbers 2
		Given the first number is 50
		And the second number is 70
		When the two numbers are added
		Then the result should be 120

	@Issue_BQM-18272
	Scenario: Minus two numbers 2
		Given the first number is 70
		And the second number is 50
		When the two numbers are subtracted
		Then the result should be 20
