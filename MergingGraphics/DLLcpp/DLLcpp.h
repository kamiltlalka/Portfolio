#pragma once
#ifndef DLLCPP_H
#define DLLCPP_H




extern "C" __declspec(dllexport) int* MyFunctionFromDll(int* FirstPic, int* SecondPic, int size, float procentage)
{
	int* newarray = new int[size];
	newarray = FirstPic;
	for (int i = 0; i < (size); i++)
	{
		newarray[i] = ((FirstPic[i]*procentage)+(SecondPic[i]*(1-procentage)));

	}
	return newarray;

}



#endif 