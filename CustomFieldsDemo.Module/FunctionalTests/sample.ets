;#DropDB CustomFieldsDemoEasyTest

#Application CustomFieldsDemo.Win
#Application CustomFieldsDemo.Web
#IfDef CustomFieldsDemo.Win

*Action About...
 
*Action OK

#EndIf

#IfDef CustomFieldsDemo.Web
!Action Navigation(My Details)
#EndIf
