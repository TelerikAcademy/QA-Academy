Feature: Search
	In order to find some book
	As a dummy QA
	I should be able to search by title and author

Background: 

	Given I'm on IT eBooks home page

@WebTest
Scenario: Search by Title

	When I seach for Selenium by Title
	Then result contains Selenium 1.0 Testing Tools book

@WebTest
Scenario: Search by Author

	When I seach for David Burns by Author
	Then result contains Selenium 2 Testing Tools book
