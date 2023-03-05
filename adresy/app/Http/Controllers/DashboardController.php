<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use DB;
class DashboardController extends Controller
{
    public function index()
    {
        if(Auth::user()->hasRole('user')){
                return view('dashboard',['phrase' => '']);
        }else if(Auth::user()->hasRole('admin')){
                return view('dashboard',['phrase' => '']);
        }
    }

    public static function show($search = '')
    {

        $body = '<table cellspacing="1" cellpadding="6" >';
        if(Auth::user()->hasRole('admin'))
        $result = DB::select('select * from adress where  miasto like "%'.$search.'%" or ulica like "%'.$search.'%" or opis like "%'.$search.'%"');
        else
        $result = DB::select('select * from adress where user_id = ?',array(Auth::user()->id));

        foreach ($result as &$value) {
        $body = $body .'<tr><td>';
        if(Auth::user()->hasRole('admin'))
             $body = $body . $value->id . '</td><td>';
            

         $body = $body . $value->miasto . '</td><td>' . $value->ulica . '</td><td>' . $value->numer . '</td><td>' . $value->opis . '</td></tr>';
        }
        $body = $body . '</table>';

        return $body;
    }

    //adds record to database(without checking for proper data)
    public static function add(Request $request)
    {

        DB::insert('insert into adress (miasto,ulica,numer,opis,user_id) values (?, ?, ?, ?, ?)', array($request->city,$request->street,$request->number,$request->desc,Auth::user()->id));
        return back();
    }

    //search by value
    public static function search(Request $request)
    {

        return view('dashboard',['phrase' => $request->nazwa]);
    }

    // removes record (only for admin)
    public static function remove(Request $request)
    {

        
        if(is_numeric($request->id) &&  Auth::user()->hasRole('admin'))
        DB::table('adress')->delete($request->id);
        return back();
    }

}
