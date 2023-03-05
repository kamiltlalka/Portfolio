<x-app-layout>
    <x-slot name="header">
        <h2 class="font-semibold text-xl text-gray-800 dark:text-gray-200 leading-tight">
            Lista adresów
        </h2>
    </x-slot>
     <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                    <h3>Wyszukaj</h3>
                   <form action="{{url('wyszukaj')}}" method="post">
                        @csrf

                        <input type="text" placeholder="wyszukaj" name="nazwa" />

                        <input type="submit">
                    </form>
                   
                </div>
            </div>
        </div>
    </div>
    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                   

                            {!! $list = App\Http\Controllers\DashboardController::show($phrase); !!}

                   
                </div>
            </div>
        </div>
    </div>
    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                            Dodaj adres
                   <form action="{{url('dodajadres')}}" method="post">
                        @csrf

                        <input type="text" placeholder="miasto" name="city" />
                        <input type="text" placeholder="ulica" name="street" />
                        <input type="text" placeholder="numer"name="number" />
                        <input type="text" placeholder="opis" name="desc" />
                        <input type="submit">
                    </form>
                   
                </div>
            </div>
        </div>
    </div>

    @if(Auth::user()->hasRole('admin'))
    {

    <div class="py-12">
        <div class="max-w-7xl mx-auto sm:px-6 lg:px-8">
            <div class="bg-white dark:bg-gray-800 overflow-hidden shadow-sm sm:rounded-lg">
                <div class="p-6 text-gray-900 dark:text-gray-100">
                Usuń adres
                   <form action="{{url('usunadres')}}" method="post">
                        @csrf

                        <input type="text" placeholder="id" name="id" />

                        <input type="submit">
                    </form>
                   
                </div>
            </div>
        </div>
    </div>

    }
    @endif


</x-app-layout>
