﻿grammar GalliumScript ;

// Parser rules

program						: declaration* EOF ;


declaration					: classDeclaration
							| functionDeclaration
							| varDeclaration
							| statement
							;

classDeclaration            : 'class' IDENTIFIER '{' constructorDeclaration* classBodyDeclaration* '}' ;
constructorDeclaration      : '__' '(' functionParametersDecl? ')' block ;
classBodyDeclaration		: ( 'private' )? (functionDeclaration | varDeclaration) ;
functionDeclaration			: 'function' IDENTIFIER	'(' functionParametersDecl? ')' ':' type block ;
functionParametersDecl		: functionParameterDecl (',' functionParameterDecl)* ;
functionParameterDecl		: IDENTIFIER ':' type  ;

varDeclaration				: 'var' IDENTIFIER ':' type ('=' expression)? ';' ;

type						: 'int'
							| 'double'
							| 'bool'
							| 'string'
							| IDENTIFIER // for user-defined types e.g., classes
							;

block						: '{' declaration* '}' ;

statement					: exprStmt
							| printStmt
							| ifStmt
							| whileStmt
							| forStmt
							| switchStmt
							| block
							| returnStmt
							;

functionCall				: IDENTIFIER '(' argumentList? ')' ;

argumentList				: expression (',' expression)* ;

exprStmt					: expression ';' ;
printStmt					: 'print' expression ';' ;
ifStmt						: 'if' '(' expression ')' statement ('else' statement)? ;
whileStmt					: 'while' '(' expression ')' statement ;
forStmt						: 'for' '(' varDeclaration? ';' expression? ';' expression? ')' statement;

returnStmt : 'return' expression? ';' ;

expression					: expression BINARY_OPERATOR expression				# BinaryOperationExpression
							| expression CONDITIONAL_OPERATOR expression		# ConditionalOperationExpression
							| expression '.' IDENTIFIER '(' argumentList? ')'   # MethodInvocationExpression
							| 'new' '(' argumentList? ')'						# NewObjectExpression
							| expression LOGICAL_OPERATOR						# LogicalOperationExpression
							| expression '?' expression ':' expression			# TernaryExpression
							| expression BITWISE_OPERATOR expression			# BitwiseExpression
							| '!' expression									# LogicalNotExpression
							| '(' expression ')'								# ParenthesizedExpression
							| IDENTIFIER										# IdentifierExpression
							| functionCall										# FunctionCallExpression
							| literal											# LiteralExpression
							;


switchStmt					: 'switch' '(' expression ')' '{' switchBlockStatementGroups? switchLabel* '}' ;

switchBlockStatementGroups	: switchLabel+ block ;

switchLabel					: 'case' constantExpression ':'
							| 'default' ':'
							;

constantExpression			: literal													# LiteralConstantExpression
							| constantExpression BINARY_OPERATOR constantExpression		# BinaryConstantExpression
							| constantExpression BITWISE_OPERATOR constantExpression	# BitwiseConstantExpression
							| constantExpression LOGICAL_OPERATOR constantExpression	# LogicalConstantExpression
							| '(' constantExpression ')'								# ParenthesisConstantExpression
							| UNARY_OPERATOR constantExpression							# UnaryConstantExpression
							;


literal						: INT_LITERAL		# IntegerLiteral
							| DOUBLE_LITERAL	# DoubleLiteral
							| BOOL_LITERAL		# BooleanLiteral
							| STRING_LITERAL	# StringLiteral
							;
// Lexer rules
NEW                         : 'new' ;
BITWISE_OPERATOR			: '>>' | '<<' | '~';
BINARY_OPERATOR				: '*' | '/' | '+' | '-' ;
CONDITIONAL_OPERATOR		: '==' | '!=' | '<' | '<=' | '>' | '<=' ;
LOGICAL_OPERATOR			: '&&' | '||' ;

IDENTIFIER					: [a-zA-Z_][a-zA-Z_0-9]* ;
INT_LITERAL					: DIGIT+ ;
DOUBLE_LITERAL				: [0-9]+ '.' [0-9]* | '.' [0-9]+ ;
BOOL_LITERAL				: 'true' | 'false' ;
STRING_LITERAL				: '"' ( ~["\\] | '\\' . )* '"' ;  // Simplified string literals
UNARY_OPERATOR				: '-' | '+' ;


WS							: [ \t\r\n]+ -> skip ;
DIGIT						: [0-9] ;
ESC							: '\\' [btnr"\\] ;

// Single-line comments
LINE_COMMENT : '//' ~[\r\n]* -> skip;

// Multi-line comments
BLOCK_COMMENT : '/*' .*? '*/' -> skip;
