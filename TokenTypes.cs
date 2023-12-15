public enum TokenType
{
    // Identifier
    VAR,
    FUNCTIONS,
    
    // Types of Datas

    NUMBER,
    BOOLEAN,
    STRING, 
    PI,

    // Binarys Operators

    PLUS,
    MINUS,
    MULT,
    DIV,
    MOD,
    OR,
    AND,
    EQUALS,
    DIFFERENT,
    LESS,
    GREATER,
    LESS_EQUAL,
    GREATER_EQUAL,
    CONCAT,
    POW,
    ASIGNATION,
    //Operador unario
    NOT,

    // Puntuations Marks
    L_PARENT,
    R_PARENT,
    SEMICOLON,
    D_APOSTROPHE,
    COMMA,

    // Reserved Keywords
    IF,
    ELSE,
    TRUE,
    FALSE,
    LET,
    IN,
    PRINT,
    THEN,
    SEN,
    COS,
    LOG,
    RETURN,

    EOF,
    CallFunction
}