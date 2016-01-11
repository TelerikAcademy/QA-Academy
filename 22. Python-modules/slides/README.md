# Python Modules


## File Operations

- Reading files
- Writing to files
- Open-close

## Others

- os
	- `os.tmpfile()`
	- `os.
	 

- httplib

  ```
  params = urllib.urlencode({'spam': 1, 'eggs': 2, 'bacon': 0})
  headers = {"Content-type": 'application/x-www-form-urlencoded',
             "Accept": "text/plain"
            }

  connection = httplib.HTTPConnection('telerikacademy.com')
  connection.request('POST', '/', params, headers)

  request = connection.getresponse()
  print request.status
  print request.reason
  print request.read()
```

- time
	-	`time.ctime()`
	-	`time.sleep(secs)`
- urllip
	-	`urllib.urlencode(obj)`
	-	`urllib.urlopen(url)`

- json
	-	serializing:

	```python
	usr = User('Doncho', 'Minkov')
	usrJson = json.dumps(usr, sort_keys = True, indent = 4)
	```

	-	deserializing:

	```python
	import json

	class User:
		def __init__(self, name, age):
			self.name = name
			self.age = age

		def as_user(dict):
			return User(dict['name'], dict['age'])

	usrJson = '{"age": 18, "name": "Doncho"}'
	usr = json.loads(usrJson, object_hook=as_user)
	```
