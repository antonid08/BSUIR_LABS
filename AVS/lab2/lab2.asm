model small
.stack 100h
.386
.data    

	a dd ?
	b dd ?
	h dd ?
    eps dd ?

    x dd ?

    n dd 1
    k dd 1

    s dd 0
    y dd 0

    cmp_result dd ?

    overflow db ?

	sqrtFromD dd ?
	
    two dd 2.0


    input_a_str db 'Input a: $'
    input_b_str db 'Input b: $'
    input_h_str db 'Input h: $'
    input_eps_str db 'Input eps: $'

    s_str db 'S(x) = $'
    y_str db 'Y(x) = $'
    n_str db 'n = $'
    x_str db 'x = $'
    
    tab_str db 9, '$'


    debug_str db 'debug $'


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


    mov dx, offset input_h_str
    mov ah, 09h
    int 21h

	call inputNumber
	fstp h ;сохранение вершины с выталкиванием из стека


    mov dx, offset input_eps_str
    mov ah, 09h
    int 21h

    call inputNumber
    fstp eps ;сохранение вершины с выталкиванием из стека


    fld a ;загружаем а в вершину стека
    fstp x; теперь x=a

 @new_x:   

    fld1 ; загружаем еденицу, чтобы можно было потом считать логарифм (там нужно, чтобы st(1) = 1)
@calculate_y:
    fld x ;считаем 2*sin(x/2)
    fdiv two

    fsin
;    call outputNumber

    fmul two
    fabs ;модуль

;call outputNumber
    

    fyl2x ;ST(1)*log2(ST(0)), результат в ST(0)
    fldln2;загружает константу натурального логарифма 2 в вершину стека сопроцессора.
    fmul;Процессор может вычислить только логарифм по основанию 2.
    ;поэтому полученное значение умножаем на натуральный логарифм от 2. 
    ;И получаем натуральный логарифм от исходного числа.
    fchs ; меняем знак
    ;тут st(0) = y
    fst y ; сохраняем в пямать

;call outputNumber
   

    fld1 ; грузим 1
    
    fldz ;грузим 0
    fst s ; s = 0
    fstp k ; k = 0 

@iteration_x:
    fld s

    fld1 ; грузим 1
    fadd k ; st(0) = 1 + k
    fstp k ; выгружаем в память

    fld x; грузим x
    fmul k; st(0) = x * k
    fcos ; st(0) = cos(x*k)
    fdiv k ; st(0) = cos(x*k)/k

    fld s; st(0) = s, предыдущее выражение смещается в st(1)
    fadd ; прибавляес к s результат новой итерации. st(0) = новое s
    fst s; загружаем обратно в s. st(0) до сих пор = s

    fsub y ; st(0) = s - y (будем сравниать разность)
    fabs ; возьмем модуль

    fld eps ; st(0) = eps, st(1) = s - y

    fcom ; сравниваем eps и (s-y)
    fstsw ax ; загружем регистр состояния swr в целочисленный региср ax 
    sahf ; загружаем ah во флаговый регистр



    jc @iteration_x ; если не достигли нужной точности, делаем еще итерацию

@output_results:

    lea dx, x_str                                                        
    mov ah,09h                                                          
    int 21h  

    fld x
    call outputNumber


    lea dx, tab_str                                                        
    mov ah,09h                                                          
    int 21h   


    lea dx, n_str                                                        
    mov ah,09h                                                          
    int 21h  

    fld k
    call outputNumber


    lea dx, tab_str                                                        
    mov ah,09h                                                          
    int 21h 


    lea dx, s_str                                                        
    mov ah,09h                                                          
    int 21h   

    fld s
    call outputNumber


    lea dx, tab_str                                                        
    mov ah,09h                                                          
    int 21h  


    lea dx, y_str                                                        
    mov ah,09h                                                          
    int 21h  

    fld y
    call outputNumber


    mov ah,02h
    mov dl,0ah
    int 21h  ;перенос строки
    

@check_count:

    fld b ;загружаем b
   
    fld x
    fadd h ; загружаем и инкрементируем x
    fst x; сохраняем x

    fcom ;сравниваем x и b
    fstsw ax
    sahf

    jc @new_x ;если x все еще меньше, идем на новую итерацию




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

                                        mov cx, 5 ;будет 3 цифры в дробной части, не больше

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