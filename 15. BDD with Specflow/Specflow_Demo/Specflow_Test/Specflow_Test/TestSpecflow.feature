Feature: TestSpecflow
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Scenario Outline: Search
	Given I am on telerik academy home page
	When I search for <searchString>
	Then I see <coursesCoutn> courses and <tracksCount> tracks
Examples: 
| searchString | coursesCoutn | tracksCount |
| XAML         | 4            | 4           |
| WPF          | 2            | 2           |

Scenario: ComboTest
	Given I am on "http://demos.telerik.com/aspnet-mvc/combobox/index" page

	When Select "Cotton" from the first comboBox
	Then The first comboBox text is "Cotton"
