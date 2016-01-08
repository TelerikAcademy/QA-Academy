<!-- section start -->
<!-- attr: { class:'slide-title', hasScriptWrapper:true } -->
# Python Overview
<div class="signature">
    <p class="signature-course">QA Academy</p>
    <p class="signature-initiative">Telerik Software Academy</p>
    <a href="http://academy.telerik.com" class="signature-link">http://academy.telerik.com</a>
</div>

<!-- section start -->

# Table of Contents

- Python Overview
- Installing Python
- Python IDEs
- Python Fundamentals
  - Data types
  - Control flow structures
  - Loops
  - Functions
  - Lambda functions
- Data Structures and comprehensions
  - Lists, sets, dictionaries and tuples
- Destructuring assignments
- Modules
- Object-oriented programming
  - Creating classes
  - Properties, instance and class methods
  - Inheritance
Best Practices
The Zen of Python

<!-- section start -->

# Python Overview
##  Usage, Versions, Installation

<!-- attr: {style: 'font-size: 0.95em'} -->
# Python Overview

- Python is a widely used general-purpose, high-level programming language
  - Design philosophy that emphasizes code readability
  - Syntax allows programmers to express concepts in fewer lines of code
  - Provides constructs intended to enable clear programs on both a small and large scale
- Python supports multiple programming paradigms, including
  - Object-oriented
  - Imperative and functional programming

<!-- attr: {style: 'font-size: 0.9em'} -->
# Python Overview

- It features a **dynamic type system** and **automatic memory management**
- Python is widely used for:
  - Automations scripts, Sikuli
  - Web applications development: Django, Pyramid
  - Games with PyGame
  - Working with Big Data
  - Science
  - And more
- Lately Python is considered the best language for beginner programmers
  - Since Python has a fluent and expressional syntax

# Installing Python

- On Linux:
  - Comes out-of-the-box
    - Part of the Linux Core is built with Python
- On Mac:
  - Easiest with Homebrew:

  ```bash
  brew install python
  ```

- On Windows:
  - Download the installer from http://python.org
  - Add the install path to System Variables' `$PATH`

# Python Versions
##  TODO

- Python 2
- Python 3

<!-- attr: {'data-transition': 'fade-in'} -->
#  Running Python in the REPL

- REPL or Read-Eval-Print-Loop
  - Run from a Terminal/CMD
  - Used to test python code
  - Start with `$ python`

- _Example:_ Тhe **numbers from 0 to 4**

```python
for number in range(5):
  print(number)
```

<!-- attr: {'data-transition': 'fade-in'} -->
#  Running Python in the REPL

- REPL or Read-Eval-Print-Loop
  - Run from a Terminal/CMD
  - Used to test python code
  - Start with `$ python`

- _Example:_ **the sum** of the numbers **from 5 to 10**

```python
sum = 0

for number in range(5, 10):
  sum += number

print(sum)
```

<!-- attr: {style: 'font-size:0.95em'} -->
#  Running Python in the REPL

- Significant whitespace is a very important part of Python

- **Significant whitespace** marks the scopes in Python
  - This is actually the indentation
    - It is the equivalent of curly brackets (`{}`) in other languages
  - Good practices say **Use four spaces for indent**

<!-- section start -->

<!-- attr: {class: 'slide-section'} -->
# Data Types in Python
##  int, float, etc..

# Data Types in Python

- Python supports all the primary data types:
  - `int` - integer numbers
    - int(intAsString) parses a string to int
  - `float` - floating-point numbers
    - float(floatAsString) - parses a string to float
  - `None` - empty or uninitialized object
    - like null, nil
  - `bool` - Boolean values
    - `True` or `False`
  - `str` - string values
    - sequence of characters

<!-- attr: {class: 'slide-section'} -->
# Data Types
##  [Demo](http://)

<!-- section start -->

<!-- attr: {class: 'slide-section'} -->
# Control-flow Structures
##  if-elif-else

<!-- attr: {hasScriptWrapper: true} -->
# Control-flow Structures

- Python has conditionals:

```python
  # run code if conditionOne is True
elif conditionTwo:
  # run code if conditionOne is False
  # and conditionTwo is True
else
  # run if both conditionOne and conditionTwo are False
```

- The conditions are True-like and False-like
  - `""` (empty string), `0`, `None` are evaluated to `False`
  - **Non-empty strings**, **any number** or **object** are evaluated to `True`

# Control-flow Structures
##  [Demo](http://)

<!-- section start -->

# Loops
##  for and while

# Loops

- Python supports two types of loops:

  - `for` loop:

  ```python
  for i in range(5):
    # run code with values of i: 0, 1, 2, 3, and 5
  ```

  ```python
  names = ['Doncho', 'Asya', 'Sneji', 'Dimo']

  for name in names:
    print('Hello, {0}'.format(name))
  ```

  - `while` loop:

  ```python
  number = 1024
  binNumber = ''
  while number >= 1:
    binNumber += '%i'%(number & 1)
    number >>= 1
  binNumber = binNumber[::-1]
  print(binNumber)
  ```

# Loops
##  [Demo](http://)

# Data Structures in Python
##  Lists, Dictionaries and Sets

# Data Structures in Python

- Python has three primary data structures
  - **List**
    - Keeps a collection of objects
    - Objects can be accessed and changed by index
    - Objects are ordered by index
    - Objects can be appended/removed dynamically
  - **Dictionary**
    - Keeps a collection of key-value pairs
    - Values are accessed by key
    - The key can be of any type
  - **Sets**
    - Keep a collection of unique objects
    - Otherwise, same as List

# Lists in Python
##  Collections of Objects

# Lists in Python

- Lists are created using the square brackets literal
  - List of integers:

  ```python
  numbers = [1, 2, 3, 4, 5, 6, 7]
  ```

  - List of strings

  ```python
  names = ['Doncho', 'Asya', 'Sneji', 'Dimo']
  ```

- Printing lists
  - _Example:_ object by object:

  ```python
  for name in names:
    print('Hi! I am %s'%name)
  ```

  - _Example:_ by index:

  ```python
  for index in len(names):
    print('Hi! I am %s'%name[index])
  ```

- Appending new objects:

  ```python
  names.append('John');
  ```

- Removing objects:
  - _Example:_ remove the right-most object

  ```python
  names.pop();
  ```

  - _Example:_ remove object at position:

  ```python
  names.pop(1);
  names.pop(-1);
  ```

# Sets in Python
## Collections of Unique Values

# Sets in Python

- Sets are created like lists, but using curly brackets instead of square
  - They contain unique values
    - i.e. each value can be contained only once
    - __eq__(other) method is called for each object
- _Example:_

```python
names = {"Doncho", "Nikolay"}
names.add("Ivaylo")
names.add("Evlogi")
names.add("Doncho")
print(names)
# the result is {'Nikolay', 'Ivaylo', 'Evlogi', 'Doncho'}
# 'Doncho' is contained only once
```

# Dictionaries in Python
##  Collections of key-value pairs

# Dictionaries in Python

- Dictionary is a collection of key-value pairs
  - The key can be of any type
    - For custom types, the methods __hash__() and __eq__(other) must be overloaded
  - Values can be of any type
    - Even other dictionaries or lists

- _Example:_ dictionary of string-list pairs

```python
musicPreferences = {
  "Rock": ["Peter", "Georgi"],
  "Pop Folk": ["Maria", "Teodor"],
  "Blues and Jazz": ["Alex", "Todor"],
  "Pop": ["Nikolay", "Ivan", "Elena"]
}
```

# Dictionaries in Python
##  [Demo](http://)

# Tuples
## List of immutable objects

# Tuples

- A tuple is a sequence of immutable objects
  - Much like lists, but their values cannot be changed
  - Use parenthesis instead of square brackets
- _Example:_

```python
languages = ("Python", "JavaScript", "Swift")
for lang in languages:
  print(lang)
print("Number of languages: {0}".format(len(languages))
```

# Tuples
##  [Demo](http://)

# Comprehensions
##  TODO

<!-- section start -->

<!-- attr: { id:'questio  ns', class:'slide-section' } -->
# Questions
## Python
[link to Telerik Academy Forum](http://telerikacademy.com/Forum/Category/60/end-to-end-javascript-applications)
