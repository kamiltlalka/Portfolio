.code
__real@3f800000 DD 03f800000r             

zmiennaint = 32
newarray = 40
zmiennaint2 = 48
zmiennaint3 = 56
FirstPic$ = 80
SecondPic$ = 88
size$ = 96
procentage$ = 104
MyFunctionFromASM PROC                
        movss   DWORD PTR [rsp+32], xmm3 ;zapisanie zmniennych do rejestru
        mov     DWORD PTR [rsp+24], r8d
        mov     QWORD PTR [rsp+16], rdx
        mov     QWORD PTR [rsp+8], rcx
        sub     rsp, 72                       
        
        movsxd  rax, DWORD PTR size$[rsp]               ; tworzenie nowej tablicy
        mov     QWORD PTR zmiennaint2[rsp], rax
        mov     eax, 4
        mov     rcx, QWORD PTR zmiennaint2[rsp]
        mul     rcx
        mov     rcx, -1
        cmovo   rax, rcx
        mov     rcx, rax
        mov     QWORD PTR zmiennaint3[rsp], rax
        mov     rax, QWORD PTR zmiennaint3[rsp]
        mov     QWORD PTR newarray[rsp], rax
        mov     rax, QWORD PTR FirstPic$[rsp]
        mov     QWORD PTR newarray[rsp], rax    ;zapisanie pierwszego zdjêcia do tablicy
        mov     DWORD PTR zmiennaint[rsp], 0
        jmp     SHORT $Skok2                        ;warunki skoków dla pêtli
$Skok1:
        mov     eax, DWORD PTR zmiennaint[rsp]
        inc     eax
        mov     DWORD PTR zmiennaint[rsp], eax
$Skok2:
        mov     eax, DWORD PTR size$[rsp]
        cmp     DWORD PTR zmiennaint[rsp], eax
        jge     SHORT $Skok3
        movsxd  rax, DWORD PTR zmiennaint[rsp]          ;mno¿enie danych i wstawianie do nowej tablicy
        mov     rcx, QWORD PTR FirstPic$[rsp]
        cvtsi2ss xmm0, DWORD PTR [rcx+rax*4]            ;instrukcja vektorowa do konwersji xmm0 z double na Single precision floating point
        mulss   xmm0, DWORD PTR procentage$[rsp]    
        movsxd  rax, DWORD PTR zmiennaint[rsp]
        mov     rcx, QWORD PTR SecondPic$[rsp]
        cvtsi2ss xmm1, DWORD PTR [rcx+rax*4]
        movss   xmm2, DWORD PTR __real@3f800000
        subss   xmm2, DWORD PTR procentage$[rsp]
        mulss   xmm1, xmm2
        addss   xmm0, xmm1
        cvttss2si eax, xmm0
        movsxd  rcx, DWORD PTR zmiennaint[rsp]
        mov     rdx, QWORD PTR newarray[rsp]
        mov     DWORD PTR [rdx+rcx*4], eax
        jmp     SHORT $Skok1
$Skok3:
        mov     rax, QWORD PTR newarray[rsp]    ;zwracanie nowej tablicy
        add     rsp, 72                            
        ret     0
MyFunctionFromASM ENDP               
end