Start CODE 0
; IntLiteral 255(0x000000FF) to 0x0021.
	MOVLW 0xFF
	MOVWF 0x0021
; Assign Byte at 0x0021 to 0x0020 (cnt)
	MOVF 0x0021, w
	MOVWF 0x0020
; IntLiteral 0(0x00000000) to 0x0022.
	MOVLW 0x00
	MOVWF 0x0022
; Assign Byte at 0x0022 to 0x0021 (value)
	MOVF 0x0022, w
	MOVWF 0x0021
; 0x0021 (value)
	MOVF 0x0021, w
	MOVWF 0x0023
; Assign Byte at 0x0023 to 0x0022 (data)
	MOVF 0x0023, w
	MOVWF 0x0022
; 0x0022 (data)
	MOVF 0x0022, w
	MOVWF 0x0023
; Assign Byte at 0x0023 to 0x0005 (GPIO)
	MOVF 0x0023, w
	MOVWF 0x0005
begin
	MOVF 0x0020, w
	MOVWF 0x0023
	DECF 0x0020,f
; 0x0020 (cnt)
	MOVF 0x0020, w
	MOVWF 0x0023
	MOVF 0x0023,f
	BTFSS 0x0003,2
	GOTO lbl00000001
; IntLiteral 255(0x000000FF) to 0x0028.
	MOVLW 0xFF
	MOVWF 0x0028
; Assign Byte at 0x0028 to 0x0020 (cnt)
	MOVF 0x0028, w
	MOVWF 0x0020
	MOVF 0x0021, w
	MOVWF 0x0028
	INCF 0x0021,f
; 0x0021 (value)
	MOVF 0x0021, w
	MOVWF 0x0029
; Assign Byte at 0x0029 to 0x0028 (data)
	MOVF 0x0029, w
	MOVWF 0x0028
; 0x0028 (data)
	MOVF 0x0028, w
	MOVWF 0x0029
; Assign Byte at 0x0029 to 0x0005 (GPIO)
	MOVF 0x0029, w
	MOVWF 0x0005
lbl00000001
	GOTO begin
	RETURN
; End of method Start.

_Interrupt CODE 4
; 0x002A (w)
	MOVF 0x002A, w
	MOVWF 0x002B
; Assign Byte at 0x002B to 0x0029 (wTemp)
	MOVF 0x002B, w
	MOVWF 0x0029
; 0x0003 (_STATUS)
	MOVF 0x0003, w
	MOVWF 0x002C
; Assign Byte at 0x002C to 0x002B (value)
	MOVF 0x002C, w
	MOVWF 0x002B
; 0x002D (value)
	MOVF 0x002D, w
	MOVWF 0x002E
	SWAPF 0x002E,f
; Assign Byte at 0x002E to 0x002C (SWAPFWResult)
	MOVF 0x002E, w
	MOVWF 0x002C
; 0x002C (SWAPFWResult)
	MOVF 0x002C, w
	MOVWF 0x002F
; Assign Byte at 0x002F to 0x002E (statusTemp)
	MOVF 0x002F, w
	MOVWF 0x002E
; 0x002E (statusTemp)
	MOVF 0x002E, w
	MOVWF 0x0030
; Assign Byte at 0x0030 to 0x002F (value)
	MOVF 0x0030, w
	MOVWF 0x002F
; 0x002D (value)
	MOVF 0x002D, w
	MOVWF 0x0031
	SWAPF 0x0031,f
; Assign Byte at 0x0031 to 0x0030 (SWAPFWResult)
	MOVF 0x0031, w
	MOVWF 0x0030
; 0x0030 (SWAPFWResult)
	MOVF 0x0030, w
	MOVWF 0x0031
; Assign Byte at 0x0031 to 0x0003 (_STATUS)
	MOVF 0x0031, w
	MOVWF 0x0003
; Assign &Byte at 0x0029 to 0x0031 (value)
	MOVF 0x0029, w
	MOVWF 0x0031
; 0x0031 (value)
	MOVF 0x0031, w
	MOVWF 0x0029
	SWAPF 0x0029,f
; Assign Byte at 0x0029 to 0x0031 (value)
	MOVF 0x0029, w
	MOVWF 0x0031
; 0x0029 (wTemp)
	MOVF 0x0029, w
	MOVWF 0x0032
; Assign Byte at 0x0032 to 0x0029 (value)
	MOVF 0x0032, w
	MOVWF 0x0029
; 0x002D (value)
	MOVF 0x002D, w
	MOVWF 0x0033
	SWAPF 0x0033,f
; Assign Byte at 0x0033 to 0x0032 (SWAPFWResult)
	MOVF 0x0033, w
	MOVWF 0x0032
; 0x0032 (SWAPFWResult)
	MOVF 0x0032, w
	MOVWF 0x0033
; Assign Byte at 0x0033 to 0x002A (w)
	MOVF 0x0033, w
	MOVWF 0x002A
	RETURN
; End of method _Interrupt.
