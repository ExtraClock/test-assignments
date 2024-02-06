<?php
namespace Storage;


use PHPUnit\Framework\TestCase;


class StoreTest extends TestCase
{
	public function test_get_ItemNotFound_ReturnNull()
	{
		$subject = new Store(10);
		
		self::assertNull($subject->get('a'));
		
	}
	
	public function test_put_NoStorageSpaceLeft_ItemUntouchedForTheMostTimeIsRemoved()
	{
		$subject = new Store(2);
		
		$subject->put('a', 'val_1');
		$subject->put('b', 'val_2');
		
		$subject->get('a');
		$subject->put('c', 'val_3');
		
		self::assertNull($subject->get('a'));
		self::assertNotNull($subject->get('b'));
		self::assertNotNull($subject->get('c'));
	}
	
	public function test_put_PriorityOfAnItemReset()
	{
		$subject = new Store(2);
		
		$subject->put('a', 'val_1');
		$subject->put('b', 'val_2');
		
		$subject->put('a', 'val_1_2');
		$subject->put('c', 'val_3');
		
		self::assertNotNull($subject->get('a'));
		self::assertNull($subject->get('b'));
		self::assertNotNull($subject->get('c'));
	}
}