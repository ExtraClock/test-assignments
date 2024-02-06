
Task 2:
-------

Build a storage class capable of storing a predefined number of key-value pairs.
This storage class must have 2 functions: get(string $key) and put(string $key, string $value).

The methods must do the following.
- get method will return a value for given key, or null if the key was not found.
- put method will insert a new key value pair into the storage (or override existing key). 
  If the storage is at maximum capacity, the pair that was not used for the longest time, must be removed from 
  the storage before inserting the new one. 
  Pair considered to be used if the get or put methods are called with the pair's key.
   
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
!!! choose to use. Both get and put methods must be solved with complexity of O(1) - 
!!! meaning the most efficient solution you can find.

Notes:
1. Keys are unique. Putting the sam key again will result in overriding it's current value.  
2. Both put and get methods should reset the priority of the value. 
3. You can assume that the keys will always contain only 'a' to 'z' characters and be at least one character long.


Project Setup:
	> composer install

Running tests:
	> composer test
or
	> composer test-cover 
for coverage tests. 