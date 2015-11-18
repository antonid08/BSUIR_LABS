.model   small
.stack   
 stack 100h
.data         
	string db 100,100 dup ('$')
	finalString db 100,100 dup ('$')
	len dw 0
	countOfWords db 0
	endOfString db 0
	oneWordMsg db 'You entered 1 or 0 word. Program termenated.$'

	
	minLength dw 100
.code 
	assume ds:@data,es:@data


	inputString proc
		push ax

		mov ah,0ah
    	lea dx,string
    	int 21h

    	pop ax
    	ret
	inputString endp


	ignoreSpaces proc
		dec di
check_next_label:
				inc di
		cmp byte ptr [di+1], '$'
		jnz last_space_label
		mov endOfString, 1
		ret

last_space_label:
		cmp byte ptr [di], ' '
		jz check_next_label

		mov endOfString, 0
		ret
	ignoreSpaces endp


	findMinLength proc
		push ax
		push bx
		push cx
		push di

	    cld
    	lea di,string+2 ;задаем смещение	
    	mov bx, di ;чтобы считать символы в слове
	    mov cx, len ;todo lenth


findNextSpace_label:
	    call getWordLength

	    cmp ax, minLength
		jnc continue
		mov minLength, ax

continue: ;если минимальная длина не изменилась
		inc di; ищем еще с текущего смещения
		mov bx, di ;сохраняем точку отсчета для нового 

		call ignoreSpaces
		cmp endOfString, 1 ;еще проверка на конец строки (учитывая пробел)
		jz n_label
;		cmp bx, di
		

		mov bx, di ;еще раз
		cmp dx,1
		jnz findNextSpace_label
n_label:
		cmp countOfWords, 1  ;выдаем msg, если введено меньше двух слов
		jnc return_label
		mov dx,offset oneWordMsg               
		mov ah,09h
		int 21h
		mov ax, 4c00h
    	int 21h
		

return_label:
		pop di
		pop cx
		pop bx
		pop ax
		ret
	findMinLength endp


	getWordLength proc

	 	mov al, ' ' ;будем бужать до пробела
    	repne scas string
    	je space_founded_label

    	mov dx, 1 ;если уже конец строки, ставим dx в 1
    	xor ax, ax
		mov ax, di ;сохраняем смещение

		sub ax, bx ;в ax длина слова
		ret


space_founded_label:
		xor dx, dx ;если не конец строки, dx = 0
		dec di;получаем адрес пробела
		xor ax,ax
		mov ax, di ;сохраняем смещение
		sub ax, bx ;в ax длина слова

		inc countOfWords
		ret

	getWordLength endp


	removeShortWords proc
	;будем копировать исходную строку в новую
	;без коротких слов

		lea di, string+2 ;источник копирования
		lea si, finalString
		mov cx, len

check_next_word_label:
		mov bx, di ;запоминаем откуда идем, чтобы потом легко копировать
		call getWordLength
		cmp ax, minLength
		jnz copy_label

		;действия если слово минимальной длинны
		inc di
		jmp check_next_word_label 

copy_label:
		push cx

		mov di, si ;приемник для копирования
		mov si, bx
		mov cx, ax
		rep movs finalString, string
		
		cmp dx,1 ;проверяем конец строки. если не конец - добавим пробел
		jz end_of_string_label
		mov ax, ' '
		stos finalString

	
end_of_string_label:
		push si ;меняем местами значения регистров
		mov si, di
		pop di

		pop cx

		push ax
		inc di
		mov ax, [di]
		xor ah,ah
		cmp ax, '$'
		pop ax
		jnz check_next_word_label

		ret
	removeShortWords endp


	strlen proc
		push cx
		push di
		push ax

		lea di, string+2
		cld
		mov al, '$'
		xor ah,ah
		mov cx, 100
		repne scas string    

		xor ax, ax
		mov ax, 100
		sub ax, cx
		sub ax,2
		mov len, ax

		pop ax
		pop di
		pop cx
		ret
	strlen endp


main:
    mov ax, @data
    mov ds, ax
    mov es, ax
    
    xor dx,dx

    call inputString
    call strlen
    call findMinLength
    mov ax, minLength
    call removeShortWords

	mov ah,02h
	mov dl,0ah
	int 21h

    mov dx,offset finalString               
	mov ah,09h
	int 21h



exit: 	

    mov ax, 4c00h
    int 21h
end     main



