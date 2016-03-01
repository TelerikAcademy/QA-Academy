Feature: Book Details
	In order to see more info for a book
	As a dummy QA
	I should have details page

@WebTest
Scenario: Book details page contains correct data

	Given book exists in IT eBooks
	Then book details page for this book is correct