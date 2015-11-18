.model   small
.stack   
 stack 100h
.data         
;данные программы
a dw 3
b dw 1
c dw 1
d dw 2

.code 
main:
        mov     ax,@data    
        mov     ds,ax
  	
	;a^3
	mov ax,a	
	mul a
	mul a
	mov si, ax ;si = a^3
	
	;b^2
	mov ax,b
	mul b
	mov di,ax ;di = b^2
	
	cmp di,si
	jc siMore ;if di<si	
	
	mov ax,c
	mov bx,d
	mov cx,b
	mul d
	add ax,cx ;ax - result
	jmp exit   	

siMore:
	mov ax,c
	mul d
	mov bx,ax ;bx = c * d
	
	mov ax,a
	div b ;ax = a / b

	cmp ax,bx
	jz equally
		
	mov ax,c ;ax - result
	jmp exit
	
equally:
	mov ax,a
	mov bx,b
	and ax,bx ;ax - result
exit:
        mov     ax,4c00h    
        int     21h         
end     main