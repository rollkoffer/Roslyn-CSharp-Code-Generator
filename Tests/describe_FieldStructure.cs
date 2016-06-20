﻿
using System.Linq;
using CSharpCodeGenerator.DataStructures;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NSpec;

namespace CSharpCodeGenerator.Tests.DataStructures
{
    class describe_FieldStructure : nspec
    {
        void when_wrapping()
        {
            context["get modifier"] = () =>
            {
                it["returns none when no modifiers"] = () =>
                {
                    var tree = CSharpSyntaxTree.ParseText("class Testclass { string test = 1; }");
                    var field = new FieldStructure(tree.GetRoot().DescendantNodes().Single(n => n.IsKind(SyntaxKind.FieldDeclaration)) as FieldDeclarationSyntax);

                    field.ModifierFlags.should_be(ModifierFlags.None);
                };

                it["returns static modifier"] = () =>
                {
                    var tree = CSharpSyntaxTree.ParseText("class Testclass { static string test = 1; }");
                    var field = new FieldStructure(tree.GetRoot().DescendantNodes().Single(n => n.IsKind(SyntaxKind.FieldDeclaration)) as FieldDeclarationSyntax);

                    field.ModifierFlags.should_be(ModifierFlags.Static);
                };
            };

            context["get access modifier"] = () =>
            {
                it["returns none when no modifiers"] = () =>
                {
                    var tree = CSharpSyntaxTree.ParseText("class Testclass { string test = 1; }");
                    var field = new FieldStructure(tree.GetRoot().DescendantNodes().Single(n => n.IsKind(SyntaxKind.FieldDeclaration)) as FieldDeclarationSyntax);

                    field.AccessModifier.should_be(AccessModifier.None);
                };

                it["returns static modifier"] = () =>
                {
                    var tree = CSharpSyntaxTree.ParseText("class Testclass { public string test = 1; }");
                    var field = new FieldStructure(tree.GetRoot().DescendantNodes().Single(n => n.IsKind(SyntaxKind.FieldDeclaration)) as FieldDeclarationSyntax);

                    field.AccessModifier.should_be(AccessModifier.Public);
                };
            };

            it["returns field identifier"] = () =>
            {
                var identifier = "testIdentifier";
                var tree = CSharpSyntaxTree.ParseText($"class Testclass {{ public string {identifier} = 1; }}");
                var field = new FieldStructure(tree.GetRoot().DescendantNodes().Single(n => n.IsKind(SyntaxKind.FieldDeclaration)) as FieldDeclarationSyntax);

                field.Identifier.should_be(identifier);
            };
        }
    }
}
