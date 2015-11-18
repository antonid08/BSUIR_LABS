.model small
.stack 200h
.data
;данные программы
number dw 123
enteredNumber dw ?
buffer1 db 5 dup (0)
buffer2 db 5 dup (0)
divider dw 10

.code

main:
	;заполнение сегмента данных
	mov ax,@data
	mov ds,ax
   	
	call intInput
	push ax

	call intInput
	push ax

	pop si
	pop ax
	
	xor dx,dx
	div si
	
	call intOutput
exit:
	;передача управления dos
        mov ax,4c00h    
        int 21h

	intInput proc
		push bx
		push cx
		push dx
		push di
	
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

		cmp al,'9' ;проверка
		ja inputSymbol

		cmp al,'0'
		jb inputSymbol
	
		mov ah,02h ;вывод символа
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
		;сохранение значений регистров
		push ax
		push bx
		push cx
		push dx
	
		mov bx, divider
		
		xor cx,cx

		test ax,ax
	toChars:
		xor dx,dx
		div bx
		add dl, '0'
		push dx
		inc cx
	        cmp ax,0
		jnz toChars
	
		mov ah,02h
		mov dl,0ah 
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

end main



