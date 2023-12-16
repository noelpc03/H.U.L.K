# HULK: Havana University Language for Kompilers
HULK es un lenguaje de programación imperativo, funcional, estática y fuertemente tipado. Casi todas las instrucciones en HULK son expresiones. En particular, este subconjunto de HULK se compone solamente de expresiones que pueden escribirse en una línea y permite el calculo de expresiones matemáticas, la declaración de variables y la declaración de funciones para su posterior uso.
Para realizar la evaluacion de la instrucción debe pasar por varios pasos: Lexer, Parser y Evaluador

# Importante
Hulk cuenta con una aplicación de consola, la cual se inicia al escribiir en consola "dotnet run".

# Lexer (Análisis lexicográfico)
Este proceso recibe la entrada en un string y su labor es desglozarla y crear los tokens según su tipo y valor. Los tokens validos se encuentran almacenados en un ENUM, por lo que todos los que no pertenezcan a esa estructura serán detectados como invalidos y el programa mostrará un error lexico. 

# Parser (Análisis sintactico)
Aqui se reciben los tokens en una lista y su objetivo es la creacion de un árbol para su posterior evaluacion. En este punto las expresiones mal formadas como paréntesis no balanceados o expresiones incompletas son detectadas y esto es mostrado como un error sintáctico. Para realizar esta labor se utiliza el parsing descendente recursivo en el cual cada token ocupa un lugar en una jerarquía.

# Evaluador (Análisis semántico)
En este punto de evalua el árbol creado anteriormente, primero evaluando el nodo de izquierdo y después el derecho hasta llegar a un nodo sin ramificaciones. Al llegar a este punto se tendrá un valor el cual es devuelto. Cada árbol tiene un tipo específico por lo que a cada uno le corresponde un método particular para su evaluación.
En este proceso son detectados los errores semánticos, los cuales son los que se producen por el uso incorrecto de los tipos y argumentos, como al sumar un double y un string. 

# Palabras reservadas
- **let**
- **in**
- **True**
- **False**
- **if**
- **print**
- **return**
- **else**
- **function**
- **sen**
- **cos**
- **log**
- **PI**

# Operadores
- **PLUS (+)**
- **MINUS (-)**
- **MULT (*)**
- **MOD (%)**
- **FLOAT_DIV (/)**
- **ASSIGN (=)**
- **SAME (==)**
- **DIFFERENT (!=)**
- **LESS (<)**
- **GREATER (>)**
- **LESS_EQUAL (<=)**
- **GREATER_EQUAL (>=)**
- **NOT (!)**
- **AND (&)**
- **OR (|)**

# Declaracion de variables
Ejemplo:
```js
let x = PI/2 in print(tan(x));
```
# Funciones
Declaración de funciones:
```js
function tan(x) => sin(x) / cos(x);
```
Uso de la funcion después de su declaración:
```js
print(tan(PI/2));
```





