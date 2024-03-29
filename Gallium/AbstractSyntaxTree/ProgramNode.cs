﻿namespace Gallium.AbstractSyntaxTree;

public class ProgramNode : ASTNode
{
    public List<ASTNode> Declarations { get; }

    public ProgramNode(List<ASTNode> declarations)
    {
        Declarations = declarations;
    }
}