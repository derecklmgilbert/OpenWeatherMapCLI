Feature: Get Coordinates By Zip

Tests For Getting Coordinates By Zip

Scenario Outline: Positive Zip Code Test

	Given I am a user
	When I send zip code <zip>
	Then I should get coordinates <lat> and <lon>

Examples:
	| zip	| lat	 | lon		|
	| 90210 | 34.09 | -118.41	|

Scenario Outline: Negative Zip Code Test
	Given I am a user
	When I send zip code <zip>
	Then I should get an error message <error>

Examples:
	|zip		|error										|
	|99999		|No information found for zip code: 99999	|
	|43512-1234	|Invalid location							|
	|1234		|Invalid location							|