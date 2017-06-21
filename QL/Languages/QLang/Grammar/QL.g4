grammar QL;
form
    : 'form' ID statement
;

statement
    : ifStatement
    | nonIfStatement
;


nonIfStatement
    : '{' statement* '}'                        #Block
    | ID ':' QUOTED_STRING type                 #Question
    | ID ':' QUOTED_STRING type '(' expr ')'    #ComputedQuestion
;

ifStatement
    : 'if' conditionBlock
        ('else if' conditionBlock)*
        ('else' statement)?
;

conditionBlock
    : expr statement
;

expr
    : '(' expr ')'                      #Parens
    | op=('!'|'-') expr                 #Unary
    | expr op=('*'|'/') expr            #Binary
    | expr op=('+'|'-') expr            #Binary
    | expr op=('>='|'>'|'<'|'<=') expr  #Binary
    | expr op=('=='|'!=') expr          #Binary
    | expr op='&&' expr                 #Binary
    | expr op='||' expr                 #Binary
    | ID                                #QuestionReference
    | QUOTED_STRING                     #StringConst
    | NUMBER                            #NumConst
    | (TRUE | FALSE)                    #BoolConst
;

type
    : ('bool' | 'boolean')              #BoolType
    | 'string'                          #StringType
    | ('int' | 'num' | 'float')         #NumType
;

fragment ESCAPED_QUOTE
    : '\\"'
;

DIGIT
    : [0-9]
;

NUMBER
    : DIGIT+
    | DIGIT+ '.' DIGIT+
;

TRUE: 'true';
FALSE: 'false';


ID
    : [a-zA-Z_][a-zA-Z_0-9]*
;
QUOTED_STRING
    : '"' ( ESCAPED_QUOTE | ~('\n'|'\r') )*? '"'
;


WS
    : [ \t\r\n]+ -> channel(HIDDEN)
    ;
COMMENT
    : '/*' .*? '*/' -> skip
    ;
LINE_COMMENT
    : '//' ~[\r\n]* -> skip
;