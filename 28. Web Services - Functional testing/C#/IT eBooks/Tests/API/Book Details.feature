Feature: Book Details API
	In order to develop IT eBooks web site
	As a web developer 
	I need Rest Api which give me book details

@ApiTest
Scenario: Book details

	When get book details for 2279690981 id 
	Then result is book object with folllowing details
	| Property  | Value                           |
	| ID        | 2279690981                      |
	| Title     | PHP & MySQL: The Missing Manual |
	| Author    | Brett McLaughlin                |
	| ISBN      | 9780596515867                   |
	| Year      | 2011                            |
	| Publisher | O'Reilly Media                  |

@ApiTest
Scenario: Book details for not existing book

	When get book details for 2279690982 id 
	Then status code is 200
	And response contains "Book not found!" error