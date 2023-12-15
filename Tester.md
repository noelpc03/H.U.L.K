
print("hEllO WORLD");              OK
print(122+23-21*2^12+23/(12+11));  OK  -85870
print(print(12)+23);               OK   35
print(23+print(21));                  OK  44 
let a=12 in a=a+a;                  OK    24
print(7+(let a =12 in a+a));        OK   31
let number = 42 in (let text = "The meaning of life is" in (print(text @ number)));   OK
let a = 42 in if (a % 2 == 0) print("Even") else print("odd"); OK
let a = 42 in print(if (a % 2 == 0) "even" else "odd");   OK

function sum(a,b)=> a+b;   OK
sum(12,23);  OK   35

let x= (let y=12 in y*y) in print(x+12); OK 156

let x=12 in let y=13 in let z=15 in print(x+y+z); OK  40 

function sum2(a)=> if(a==1) return 1 else return a*sum2(a-1);  OK
function fib(a)=> if(a==1) return 1 else return (fib(a-1)*a); OK 

print(2^2^3);


function conc(a,b)=> a@b@12;
conc("Hola mundo"," Bienvenido");
print(sen(2*PI) ^ 2 + cos(3*PI / log(100)));  OK
let a=6, b= a*7 in print(b);
let a=6 in let b=a*7 in print(b);
let a=20 in let a= 42 in print(a);
let a=7 ,a=7*6 in print(a);

function a(x)=> 12;

## Errores

### Lexicos
let 14a = 5 in print(14a);

### Sintactico
 let a = 5 in print(a;
let a = 5 inn print(a);
let a = in print(a);

### Semanticos

let a = "hello world" in print(a + 5);

