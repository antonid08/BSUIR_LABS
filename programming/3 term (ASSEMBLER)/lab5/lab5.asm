.model   small
.stack   
 stack 100h
.data    
	lines = 3
	colums = 3
	matrix1 dw 3 dup (3 dup (?))
	matrix2 dw 3 dup (3 dup (?))
	result dw 3 dup (3 dup (?))

.code
main:
	mov     ax,@data    
    mov     ds,ax
 	
 	lea BP, matrix1
	call inputMatrix
	call printMatrix

	xor bp, bp

	lea bp, matrix2
	call inputMatrix
	call printMatrix

	call multiplyMatrix
	lea bp, result
	call printMatrix

exit:
	mov ax,4c00h    
    int 21h

    printMatrix proc
    	push ax
    	push bx
    	push si
    	push bp

    	xor bx,bx ;счетчик по строкам
;    	add bx,bp ;ДОБАВЛЯЕМ АДРЕС АРГУМЕНТА, ЧТОБЫ ВЫВОДИТЬ ЕГО
print_cycle_lines:
    	xor si,si ;счетчик по столбцам

    	push ax
    	push dx
    	mov ah,02h ;перенесем строку
		mov dl,0ah 
		int 21h
		pop dx
		pop ax

		add bx,bp ;ДОБАВЛЯЕМ АДРЕС АРГУМЕНТА, ЧТОБЫ ВЫВОДИТЬ ЕГО
print_cycle_colums:

 		xor ax,ax
    	mov ax, [bx][si] ;двигаем тек. элемент в ax
    	call intOutput ;выводим

    	add si, 2 ;добавляем к счетчику размер слова
    	cmp si, colums*2
    	jnz print_cycle_colums ;если вывели не все столбцы, прыгаем на метку

    	add bx, colums*2 ;переводим базовый индекс на след. строку
    	sub bx, bp
    	cmp bx, lines*colums*2
    	jnz print_cycle_lines

    	pop bp
    	pop si
    	pop bx
    	pop ax
    	ret
    printMatrix endp


    multiplyMatrix proc
    	push si
    	push bx


    	xor si,si ;счетчик по строкам
cycle_lines:
    	xor bx,bx ; счетчик по столбцам
cycle_colums:
		call calculateCurrentMember

		add bx, 2 ;переход на след элемент
		cmp bx, colums*2
		jnz cycle_colums			
		
		add si, colums*2
		cmp si, lines*colums*2
		jnz cycle_lines

		pop bx
		pop si
    	ret
    multiplyMatrix endp


    calculateCurrentMember proc
    	push ax
    	push bx
    	push cx
    	push dx
    	push si
    	
    	mov result[si][bx], 0 ;сюда запишем результат
    	mov cx, 0 ;счетчик от 0 до кол-ва строк(столбцов)
next_step:
		xor ax, ax ;обнуляем axdx для корректной работы умножения
		xor dx, dx

		push bx
		push cx
		mov bx,cx	 ;сохраняем cx и bx, используем регистр bx для правильной индексации
 		mov ax, matrix1[si][bx] ;кидаем элемент первой матрицы в ax, чтобы на него домножить
 		pop cx
 		pop bx

 		push si
 		push cx
 		mov si,cx ;используем si для правильной индексации

 		push ax ;сохраняем элемент для умножения в ax
 		mov ax, si
 		push dx
 		mov dx, colums
 		mul dx ;умножаем наш счетчик на кол-во столбцов в строке, чтобы получить базовый регистр
 		pop dx
 		mov si, ax
 		pop ax

 		mul matrix2[si][bx] ;умножаем на переопределенный регистр
 		pop cx
 		pop si ;возвращаем счетчик и индекс текущего элемента из стека

 		add result[si][bx], ax ;добавляем к результату полученное произведение 

 		add cx, 2 ;увеличиваем счетчик на размер слова
 		cmp cx, colums*2
 		jnz next_step ;сравниваем счетчик с кол-вом столбцов*слово и делаем проверку

 		pop si
 		pop dx
 		pop cx
 		pop bx
 		pop ax
    	ret
    calculateCurrentMember endp


    inputMatrix proc ;ввод матрицы (вводим построчно). Адрес матрицы будет в BP
    	push ax
    	push bx
    	push cx
    	push dx
    	push si
    	push bp


    	mov cx, lines  ; счетчик по строкам
    	mov si, 0 ;текущая строка
    	add si,bp ;ДОБАВЛЯЕМ АДРЕС АРГУМЕНТА
inputNextLine:
		push cx  ;сохраняем его
		mov cx, colums ;теперь в cx кол-во столбцов
		mov bx, 0 ;текущий столбец
inputNextNumber: ;ввод числа в строке
		call intInput
		mov [si][bx], ax ;записываем очередное число

		add bx, 2 ;сдвигаем столбец в строке на слово
		
		loop inputNextNumber ;если ввели меньше чем нужно, вводим еще

		pop cx ;достаем созраненное значение строк
		add si, colums*2 ;сдвигаем на одну строку

		push ax
		push dx
		mov ah,02h ;перенесем каретку
		mov dl, 0ah
		int 21h
		pop dx
		pop ax
		
		loop inputNextLine

		pop bp
		pop si
		pop dx
		pop cx
		pop bx
		pop ax
		ret
    inputMatrix endp


	intInput proc
		push bx
		push cx
		push dx
		push di
		
		xor ax,ax
		xor dx,dx 

		mov ah,02h
		mov dl,'>'
		int 21h
		xor di,di
inputSymbol:
		mov ah,08h
		int 21h

		cmp al,13 ;enter
		jz done
		
		cmp al,8 ;backspace
		jz backspace

		cmp al,27 ;escape
		jz escape;

		cmp al,'9' ;ïðîâåðêà
		ja inputSymbol

		cmp al,'0'
		jb inputSymbol
	
		mov ah,02h ;âûâîä ñèìâîëà
		mov dl,al
		int 21h
		
		sub ax,'0'
		xor ah,ah
		mov cx,ax
		mov ax,di
		mov bx,10
		mul bx
		jc escape
		add ax,cx
		jc escape		
		mov di,ax
		jmp inputSymbol

backspace:	
		mov ax,di
		mov bx,10
		div bx
		call intOutput
		xor dx,dx
		mov di,ax 
		jmp inputSymbol

escape:		
		xor ax,ax
		xor dx,dx
		xor di,di
		call intOutput
		jmp inputSymbol		

done:		
		mov ax,di
					
		pop di
		pop dx
		pop cx
		pop bx
		ret
	intInput endp	
	

	intOutput proc
		;ñîõðàíåíèå çíà÷åíèé ðåãèñòðîâ
		push ax
		push bx
		push cx
		push dx
	
		mov bx, 10

		xor cx,cx
	toChars:
		xor dx,dx
		div bx
		add dl, '0'
		push dx
		inc cx
	        cmp ax,0
		jnz toChars
	
		mov ah,02h
		mov dl, ' ' 
		int 21h
	output:	
		pop dx	
		xor dh,dh
		int 21h
		loop output		
		
		pop dx
		pop cx
		pop bx
		pop ax		
		ret
	intOutput endp

end		main

