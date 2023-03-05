<?php

namespace Database\Seeders;

// use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    /**
     * Seed the application's database.
     */
    public function run(): void
    {
    $this->call(LaratrustSeeder::class);

        for($i = 0; $i<10; $i++)
        DB::table('users')->insert([
            'miasto' => Str::random(10),
            'ulica' => Str::random(13),
            'numer' => Int::random(10),
            'opis' => Str::random(15),
            'user_id' => Int::random(4),
        ]);



    }
}
