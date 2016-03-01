Feature: Navigation
	In order to see book details
	As a dummy QA
	I should be able navigate to book details

@WebTest
Scenario: Navigate from search results to book details 

	Given results page with some results
	When I click on some of the books on results page
	Then I see book details page for the same book

@WebTest
Scenario: Navigate from home page to book details 

	Given I'm on IT eBooks home page
	When I click on some of the books on home page
	Then I see book details page for the same book
