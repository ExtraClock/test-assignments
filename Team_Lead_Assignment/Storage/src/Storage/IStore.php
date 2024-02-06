<?php
namespace Storage;


interface IStore
{
	public function get(string $key): ?string;
	public function put(string $key, string $item): void;
}