Feature: Get Coordinates By City and State

Tests For Getting Coordinates By City and State

Scenario Outline: Positive City And State Test

	Given I am a user
	When I send city and state <cityState>
	Then I should get coordinates <lat> and <lon>

Examples:
	| cityState				| lat	| lon		|
	| Defiance,Ohio,USA		| 41.28 | -84.36	|
	| Defiance,Oh,USA		| 41.28 | -84.36	|

Scenario Outline: Negative City And State Test
	Given I am a user
	When I send city and state <cityState>
	Then I should get an error message <error>

Examples:
	|cityState							|error																|
	|TotallyNotARealPlaceVille,Ohio		|No information found for location: TotallyNotARealPlaceVille,Ohio	|
	|asdfasdf							|Invalid location													|