# Selenium WebDriver
## Homework
1. Go to [http://stage.telerikacademy.com/](http://stage.telerikacademy.com/), and click on "Регистрация"

* Fill the form as shown below, leave "Потребителско име" blank. Verify that a message about missing username appears. 

</br>
<img src="images/registrationForm.png" />

* Now fill in "Потребителско име" using C# function, use unique name every time. Verify the message isn’t present anymore.
* Test if the backend validation works properly. For that you’ll have to disable JavaScript in your test and then start editing. Run the test a couple of times to verify that it works correctly. You can try to delete a login cookie.
</br>
Think of an appropriate way to organize your tests and use 
	* Base test class with methods missing in WebDriver
	* Page Object Model

2.Using Selenium WebDriver create a report for valid IP addresses for each country in the world. The test should navigate to [http://services.ce3c.be/ciprg/](http://services.ce3c.be/ciprg/) and extract all IP ranges for each listed country in one place. The complete list should then be parsed as C# objects and printed to the Console in the following format: `“{CountryName} - {IPAddress}”`. The IP Address should be the first IP address from the first IP Range for each country
</br>
Hint: You can format the IP Ranges result as JSON (formatting by input): 

```cs
{
  "Country" : "{country}",
  "StartIPRange" : "{startip}",
  "EndIPRange" : "{endip}"
},
  ```
Make sure that your JSON format is valid and arrays are encapsulated within opening and closing square brackets.
*	Implement at least two different approaches for collecting the complete list of IP Ranges
	*	By filling the Country name in the Country(s) input
	*	By navigating to each countrie`s URL (Hint: you can apply filtering in the URL)
*	Use RegEx expression to parse the default List Formatting: http://services.ce3c.be/ciprg/?countrys=AFGHANISTAN
*	Create a report for the count of all valid IPs for each country.
Example: 
```cs
{
  "Country" : "Afghanistan",
  "StartIPRange" : "27.116.56.0",
  "EndIPRange" : "27.116.59.255"
},
  ```

In this IP range the possible IP addresses are (59 – 56 + 1) * 256 = 1024


