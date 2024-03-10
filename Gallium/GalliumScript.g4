grammar GalliumScript ;

program             : declaration* EOF ;

declaration         : classDecl
                    | funDecl
                    | varDecl
                    | statement 
                    ;

classDecl           : 'class' IDENTIFIER ('<' IDENTIFIER)? '{' function* '}' ;
funDecl             : 'fun' function ;
varDecl             : 'var' IDENTIFIER ('=' expression)? ';' ;

statement           : exprStmt
                    | forStmt
                    | ifStmt
                    | printStmt
                    | returnStmt
                    | whileStmt
                    | block 
                    ;

exprStmt            : expression ';' ;
forStmt             : 'for' '(' (varDecl | exprStmt | ';') expression? ';' expression? ')' statement ;
ifStmt              : 'if' '(' expression ')' statement ('else' statement)? ;
printStmt           : 'print' expression ';' ;
returnStmt          : 'return' expression? ';' ;
whileStmt           : 'while' '(' expression ')' statement ;
block               : '{' declaration* '}' 
                    ;

expression          : assignment ;
assignment          : (call '.')? IDENTIFIER '=' assignment
                    | logic_or 
                    ;

logic_or            : logic_and ('or' logic_and)* ;
logic_and           : equality ('and' equality)* ;
equality            : comparison (('!=' | '==') comparison)* ;
comparison          : term (('>' | '>=' | '<' | '<=') term)* ;
term                : factor (term_op factor)* ;
factor              : unary (('/' | '*') unary)* ;

term_op             : (MINUS | PLUS) ;
unary               : ('!' | '-') unary | call ;

call                : primary ('(' arguments? ')' 
                    | '.' IDENTIFIER)* ;

primary             : 'true'        # TrueConstantExpression
                    | 'false'       # FalseConstantExpression
                    | 'nil'         # NilConstantExression
                    | 'this'        # ThisExpression
                    | NUMBER        # NumericConstantExpression
                    | STRING        # StringConstantExpression
                    | IDENTIFIER    # IdentifierExpression
                    | '(' expression ')'    # GroupExpression
                    | 'super' '.' IDENTIFIER  # SuperExpression
                    ;

function            : IDENTIFIER '(' parameters? ')' block ;
parameters          : IDENTIFIER (',' IDENTIFIER)* ;
arguments           : expression (',' expression)* ;

// Lexer Rules

MINUS               : '-' ;
PLUS                : '+' ;
IDENTIFIER          : [a-zA-Z_][a-zA-Z_0-9]* ;
NUMBER              : DIGIT+ ('.' DIGIT+)? ;
STRING: '"' (ESC | ~["\\])* '"';

DIGIT               : [0-9] ;

// Skip spaces, tabs, and newlines
WS                  : [ \t\r\n]+ -> skip ;

// Single-line comments
LINE_COMMENT : '//' ~[\r\n]* -> skip;

// Multi-line comments
BLOCK_COMMENT : '/*' .*? '*/' -> skip;

fragment ESC: '\\' [btnr"\\];
