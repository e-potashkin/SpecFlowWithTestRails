Feature: Calculator

	@Issue_AC-2109
	Scenario: Add two numbers
		Given the first number is 50
		And the second number is 70
		When the two numbers are added
		Then the result should be 120
		And Test Table:
			| UserName                                 | ReportsTo | Roles    | Organization | ProfessionalRoles |
			| test.sensitive-content.hr@email.com      |           | HR       | All Company  |                   |
			| test.sensitive-content.config@email.com  |           | Employee | All Company  | Configuration     |
			| test.sensitive-content.reviews@email.com |           | Employee | All Company  | Reviews           |

	@Issue_AC-18272
	Scenario: Minus two numbers
		Given the first number is 70
		And the second number is 50
		When the two numbers are subtracted
		Then the result should be 20
