Feature: Search API
	In order to develop IT eBooks web site
	As a web developer 
	I need Rest Api which can search for books

@ApiTest
Scenario: Default search

	When Search with selenium webdriver query
	Then result contains following books
	| ID         | Title                                  |
	| 2115300285 | Instant Selenium Testing Tools Starter |
	| 2205205534 | Selenium 2 Testing Tools               |
	| 583859216  | Selenium 1.0 Testing Tools             |

@ApiTest
Scenario: Search with page number parameter

	When Search with mysql query and page number 3
	Then result contains following books
	| ID         | Title                         |
	| 1470937288 | MySQL for Python              |
	| 573588234  | MySQL 5.1 Plugin Development  |
	| 2144150505 | Understanding MySQL Internals |

@ApiTest
Scenario: Search with too long query string

	When Search with qwertyuiopasdfghjklzxcvbnmqwertyuiopasdfghjklzxcvbnm query
	Then no results are found

@ApiTest
Scenario: Search with invalid page number parameter

	When Search with mysql query and page number -1
	Then result contains following books
	| ID         | Title                              |
	| 3398759608 | MySQL Stored Procedure Programming |
	| 924371675  | High Availability MySQL Cookbook   |
	| 1470937288 | MySQL for Python                   |


