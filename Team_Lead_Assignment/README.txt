Task 1: Storage
---------------
	
	Build a storage class capable of storing a predefined number of key-value pairs.
	This storage class must have 2 functions: get(string $key) and put(string $key, string $value).
	
	The methods must do the following.
	- get method will return a value for a given key, or null if the key was not found.
	- put method will insert a new key value pair into the storage (or override the existing key). 
	  If the storage is at maximum capacity, the pair that was not used for the longest time  must be removed from 
	  the storage before inserting the new one. 
	  Pair is considered to be used if the get or put methods are called with the pair's key.
	   
	For example in the following code:
	
	// 3 is the maximum number of items this storage item should hold. 
	$s = new Storage(3);
	
	$s->put('a', '1');
	$s->put('b', '2');
	$s->put('c', '3');
	$s->get('c');
	$s->get('b');
	$s->get('a');
	
	The key 'c' was not used for the longest time. Therefore any put operation at this point 
	will remove the 'c' => '3' key-value pair before inserting any new pair. 
	See StoreTest test class for more examples. 
	
	!!! Please note that the most important aspect of this task is the algorithm you 
	!!! choose to use. Both get and put methods must be solved with the complexity of O(1) - 
	!!! meaning the most efficient solution you can find.
	
	Notes:
	1. Keys are unique. Putting the same key again will result in overriding its current value.  
	2. Both put and get methods should reset the priority of the value. 
	3. You can assume that the keys will always contain only 'a' to 'z' characters and be at least one character long.
	4. Implementing unit tests is optional.
	
	
	Project Setup:
		> composer install
	
	Running tests:
		> composer test
	or
		> composer test-cover 
	for coverage tests. 



Task 2: Link Shortener
---------------
	For the requirements listed below, prepare a work plan.

	The plan should include:
	- Headcount. How many people will be involved and what is their role: back-end, full-stack, front-end, QA, etc.
	- Project timeline. It should include these phases: Development, QA, and Release.
	- High-level architecture and list of technologies going to be utilized
	- Breakdown to tasks. All tasks should have estimation (time or story points). You can use the tool of your choice (eg. Trello, Excel, Jira)

	Requirements:
	URL shortening services like bit.ly are very popular for generating shorter aliases for long URLs. You need to plan a project for this kind of web service. 
	A user inputs a long URL and the service returns a short URL and authentication token to check short-link statistics. When a user hits a short link, the service should count a click in the short-link statistics and redirect a user to the long url. When a user opens a statistics page with an authentication token, they're able to see how many clicks on the short link were made. 
	Initially, we expect 100 links generated per day and 1000 clicks per day made on generated short links. The system should be able to keep up with 10x load spikes on a daily scale. The maximum expected load growth over time that should be supported with the current solution is 100x (10,000 links per day and 100,000 clicks per day)
		

	Good luck.

