model small
.stack 100h
.386
.data    

	a dd ?
	b dd ?
	c dd ?

	four dd 4.0
	two dd 2.0
    overflow db ?

	sqrtFromD dd ?
	
	divider dw 10

    input_a_str db 'Input a: $'
    input_b_str db 'Input b: $'
    input_c_str db 'Input c: $'

    no_solutions_str db 'No solutions! $'

    input_error_str db 'Error. Input again: $'

.code 
main:
	mov ax,@data
	mov ds,ax

	finit

    mov dx, offset input_a_str
    mov ah, 09h
    int 21h

	call inputNumber
	fstp a  ;сохранение вершины с выталкиванием из стека

    mov dx, offset input_b_str
    mov ah, 09h
    int 21h

	call inputNumber
	fstp b ;сохранение вершины с выталкиванием из стека


    mov dx, offset input_c_str
    mov ah, 09h
    int 21h

	call inputNumber
	fstp c ;сохранение вершины с выталкиванием из стека

    fld a
    
    ftst ;сраниваем с 0
    fstsw ax 
    sahf ;копируем флаги
    jnz @a_not_null

    fld c ; загрузка в вершину стека
    fchs
    fdiv b

    call outputNumber

    jmp exit


@a_not_null:
    ;fstp


	fld b  
	fmul b  ; ST(0) = b^2


	fld a ;загрузка в вершину стека
	fmul c
	fmul four ;ST(0) = 4*a*c, ST(1) = b^2

	fsub ; ST(0) = b^2 - 4*a*c
	
	ftst ;сраниваем с 0

	fstsw ax 
    sahf ;копируем флаги

    jc no_solutions ; если дискриминант отрицательный - выходим

    fsqrt ; считаем корень из дискриминанта
    fst sqrtFromD  ;сохраним его

    fld b ; ST(0) = b
    fchs ; ST(0) = -b

    fadd ; ST(0) = ST(0) + ST(1) = -b + sqrt(d)
    fdiv a; делим на a
    fdiv two; делим на 2

	call outputNumber

	fld sqrtFromD ;стек сбился, восстановим корень из дискр.

	fld b ; ST(0) = b
    fchs ; ST(0) = -b

    fsubr ; ST(0) = ST(0) - ST(1) = -b - sqrt(d)

    fdiv a; делим на a
    fdiv two; делим на 2

    mov ah,02h
	mov dl,0ah
	int 21h  ;перенос строки
    
    call outputNumber


    jmp exit

no_solutions:
    mov dx, offset no_solutions_str
    mov ah, 09h
    int 21h
    

exit:
        mov     ah, 4ch 
        int     21h  
                                inputNumber proc 
                                        push ax
                                        push bx
                                        push dx
                                        push si
       
                                        push bp
                                        mov bp, sp ;bp для доступа к стеку
                                        push 10
                                        push 0
                                        xor bx, bx
                             @preparing:
                                        xor si, si
                                        fldz ;сразу у нас 0

                                        mov ah, 01h  ;вводим символы и проверяем
                                        int 21h


                                        cmp al, 13 ;enter
                                        jz short @error

                                        cmp al, '.'  ; проверяем на точку
                                        je short @error

                                        cmp al, '-'
                                        jne short @positive_number

                                        inc si ; si - флаг минуса

                                @start_input:   
                                		mov ah, 01h ;вводим дальше
                                        int 21h

                                @positive_number:   
                                		cmp al, '.'  ; проверяем на точку
                                        je short @start_input_fractional_part

                                        cmp al, 13 ;enter
                                        jz short @finish_input

                                        ;проверка правильности ввода
                                        cmp al, '9'
                                        ja short @error
                                        sub al, '0'
                                        jb short @error

                                        mov [bp - 4], al
                                        fimul word ptr [bp - 2] ; сохраняем + умножаем на 10
                                                                    
                                		;и прибавим только что полученную цифру.
                                        fiadd word ptr [bp - 4]

                                        add bx, 1
                                        cmp bx, 10
                                        ja short @error
                                        

                                        jmp short @start_input

                                @start_input_fractional_part:   
                                		fld1  ;сохраним 1 (см. ниже)

                                @input_fract_symbol:
                                	    mov ah, 01h
                                        int 21h


                                        ; опять проверка на цифру
                                        cmp al, 13 ;enter
                                        jz short @drop_ten_power

                                        ;проверка правильности ввода
                                        cmp al, '9'
                                        ja short @error
                                        sub al, '0'
                                        jb short @error
                                
                                		;сохраняем
                                        mov [bp - 4], al

                                        fidiv word ptr [bp - 2] ;делим на 10


                                        fld st(0) ;создаем копию (см. ниже)

                                        fimul word ptr [bp - 4] ;умножаем на введенную цифру



                                        faddp st(2), st ;сложение с выталкиванием из стека
  
                                        jmp short @input_fract_symbol


                                @error: 
                                        xor bx, bx

                                        mov dx, 0AH 
                                        mov ah, 02h
                                        int 21h

                                        mov dx, offset input_error_str
                                        mov ah, 09h
                                        int 21h

                                        ;jmp exit
                                        jmp @preparing

                                @drop_ten_power:   
                                		fstp st(0) ;вытолкнем из стека
                                        jmp short @finish_input
                                       

                                @finish_input:
                                		mov ah, 02h
                                        mov dl, 0Dh
                                        int 21h
                                        mov dl, 0Ah
                                        int 21h

                                        test si, si
                                        jz short @return_from_func
                                        fchs
                                @return_from_func:
                                        leave  
                                        pop si
                                        pop dx
                                        pop bx
                                        pop ax


                                        ret
                        inputNumber endp




                        outputNumber proc
                                        push ax
                                        push cx
                                        push dx

                                        push bp
                                        mov bp, sp  ;bp для работы со стеком
                                        push 10
                                        push 0
 
                                        ftst  ; проверка знака
                                        fstsw ax ;загруска в пямять слова состояния
                                        sahf  ; значение регистра ah в младший байт флагового регистра
                                        jnc @positive

                                        mov ah, 02h
                                        mov dl, '-'
                                        int 21h

                                        fchs  ;модуль (x = -x)

                                @positive:   
                                        fld1                
                                        fld st(1)                

                                        fprem    ; остаток от деления
                                        fsub st(2), st   ;получаем целую часть
                                        fxch st(2)       ; обмен значениями

                                        ; вывод целой части
                                        xor cx, cx

                                ; Стандартный вывод. В цикле делим на 10 и т.д.
                                @int_part: fidiv   word ptr [bp - 2]    ; берем 10 из стека с помощью bp
                                        fxch st(1)    ; обмен значениями              
                                        fld st(1)     ;из памяти в вершину стека
                                        ;после деления останется число с точкой. нужно избавиться от дробной части

                                        fprem   ;найдем ее

                                        fsub st(2), st   ; и отнимем
 
                                        fimul word ptr [bp - 2]      
                                        fistp word ptr [bp - 4]  ;сохранение вершины стека с выталкиванием  
                                        inc cx

                                        push word ptr [bp - 4]   ;сохраняем число
                                        fxch st(1)               
                                                                                        
                                        ftst  ;сравниваем с нулем и повторяем, если не 0
                                        fstsw ax ;загруска в пямять слова состояния
                                        sahf  ; значение регистра ah в младший байт флагового регистра
                                        jnz short @int_part
                                
                                                ; вывод целой части
                                        mov ah, 02h
                                @output_int:   pop     dx
                                        add dl, 30h
                                        int 21h
                                        loop @output_int 

                                
                                                ;теперь дробная часть. проверяем, есть ли она
                                        fstp st(0)  ; выталкивание из стека            
                                        fxch st(1)                 
                                        ftst ;сравниваем с нулем
                                        fstsw ax ;загруска в пямять слова состояния
                                        sahf  ; значение регистра ah в младший байт флагового регистра
                                        jz short @without_fractional_part
                                
                                        mov ah, 02h
                                        mov dl, '.'
                                        int 21h

                                        mov cx, 3 ;будет 3 цифры в дробной части, не больше

                                @fractional_part:
                                            fimul word ptr [bp - 2] ; умножить др. часть на 10
                                        fxch st(1)                 
                                        fld  st(1)     ;добавим в стек еще раз то что получилось (см. дальше)      

                                        fprem ;из копии числа получаем дробную часть
     
                                        fsub st(2), st ;отнимаем от оригинала
                                        fxch st(2)      ; ставим в верхушку полученную цифру

                                        fistp word ptr [bp - 4]  ;сохраняем
   
                                        mov ah, 02h ; выводим
                                        mov dl, [bp - 4]
                                        add dl, 30h
                                        int 21h

                                        ;делаем проверку и повторяем, если нужно
                                        fxch st(1)    
                                        ftst
                                        fstsw ax
                                        sahf

                                        loopnz @fractional_part   ; + проверка на кол-во цифр

                                @without_fractional_part:   
                                                ;чистим стек
                                                fstp st(0)          
                                        fstp st(0)                

                                        leave
                                        pop dx
                                        pop cx
                                        pop ax

                                        ret
                        outputNumber endp


                  

end     main