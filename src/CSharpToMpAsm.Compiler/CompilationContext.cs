using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ICSharpCode.NRefactory.CSharp;
using ICSharpCode.NRefactory.TypeSystem;
using CSharpToMpAsm.Compiler.Codes;

namespace CSharpToMpAsm.Compiler
{
    public class CompilationContext
    {
        class Pair
        {
            public TypeDefinition Definition { get; set; }
            public TypeDeclaration Node { get; set; }
        }

        private List<Pair> _types;

        public TypeDefinition ResolveType(AstType type)
        {
            var primitiveType = type as PrimitiveType;
            if (primitiveType != null)
            {
                switch (primitiveType.KnownTypeCode)
                {
                    case KnownTypeCode.Void:
                        return TypeDefinitions.Void;
                    case KnownTypeCode.Int32:
                        return TypeDefinitions.Int32;
                    case KnownTypeCode.Byte:
                        return TypeDefinitions.Byte;
                    case KnownTypeCode.SByte:
                        return TypeDefinitions.SByte;
                    default:
                        throw new NotSupportedException(string.Format("Type {0} is not supported.",
                                                                      primitiveType.KnownTypeCode));
                }
            }
            var name = type.ResolveTypeName();
            return _types.Select(x => x.Definition).Single(x => x.Name == name);
        }

        public void Compile(SyntaxTree[] parseResults)
        {
            _types = parseResults.SelectMany(FindTypeDefinitions).ToList();
            foreach (var type in _types)
            {
                RegisterBase(type);
            }
            foreach (var type in _types)
            {
                FindMembers(type);
            }
            foreach (var type in _types)
            {
                Compile(type);
            }
        }

        private IEnumerable<Pair> FindTypeDefinitions(SyntaxTree syntaxTree)
        {
            var results = new List<Pair>();

            var finder = new TypeDeclarationFinder(
                (d, n, u) => results.Add(new Pair { Definition = new TypeDefinition(d, n, u.ToList()), Node = d }));

            syntaxTree.AcceptVisitor(finder);

            return results;
        }

        private void RegisterBase(Pair pair)
        {
            var types = pair.Node.BaseTypes.Select(x => _types.Single(y => y.Definition.Name == x.ResolveTypeName()));
            foreach (var typeDefinition in types)
            {
                pair.Definition.AddBase(typeDefinition.Definition);
            }
        }

        private void FindMembers(Pair type)
        {
            foreach (var entityDeclaration in type.Node.Members)
            {
                var methodDeclaration = entityDeclaration as MethodDeclaration;
                if (methodDeclaration != null)
                {
                    RegisterMethod(type, methodDeclaration);
                }
                var propertyDeclaration = entityDeclaration as PropertyDeclaration;
                if (propertyDeclaration != null)
                {
                    RegisterProperty(type, propertyDeclaration);
                }
            }
        }

        private void RegisterMethod(Pair pair, MethodDeclaration methodDeclaration)
        {
            pair.Definition.AddMethod(methodDeclaration.Name, CreateDefinition(methodDeclaration));
        }

        private MethodDefinition CreateDefinition(MethodDeclaration methodDeclaration)
        {
            var definition = new MethodDefinition(methodDeclaration.Name)
                                       {
                                           IsAbstract = (methodDeclaration.Modifiers & Modifiers.Abstract) != 0,
                                           IsVirtual = (methodDeclaration.Modifiers & (Modifiers.Abstract | Modifiers.Virtual)) != 0,
                                       };

            definition.ReturnType = ResolveType(methodDeclaration.ReturnType);

            definition.Parameters = methodDeclaration.Parameters.Select(x => CreateParameter(x, definition)).ToArray();

            var attributes = methodDeclaration.Attributes.SelectMany(x => x.AcceptVisitor(new AttributeFinder())).ToList();

            var addressAttribute = attributes.FirstOrDefault(x => "Address".Equals(x.Name, StringComparison.InvariantCulture));

            definition.CodeAddress = addressAttribute != null ? (int)addressAttribute.Arguments[0].Value : MethodDefinition.CodeAddressNotSet;

            return definition;
        }
        
        private ParameterDestination CreateParameter(ParameterDeclaration declaration, MethodDefinition definition)
        {
            var type = ResolveType(declaration.Type);
            switch (declaration.ParameterModifier)
            {
                case ParameterModifier.None: break;
                case ParameterModifier.Out:
                case ParameterModifier.Ref:
                    type = TypeDefinitions.Reference(type);
                    break;
                default: throw new NotImplementedException();
            }
            return new ParameterDestination(declaration.Name, type);
        }

        private void RegisterProperty(Pair pair, PropertyDeclaration propertyDeclaration)
        {
            var attributes = propertyDeclaration.Attributes.SelectMany(x => x.AcceptVisitor(new AttributeFinder())).ToList();
            var addressAttr = attributes.FirstOrDefault(x => x.Name == "Address");
            ResultLocation address = null;

            var type = ResolveType(propertyDeclaration.ReturnType);
            if (addressAttr != null)
            {
                address = new ResultLocation((int) addressAttr.Arguments[0].Value);
            }

            var propertyDestination = new PropertyDestination(propertyDeclaration.Name, type, address);
            pair.Definition.AddDestination(propertyDestination);
        }

        private void Compile(Pair pair)
        {
            foreach (var member in pair.Node.Members)
            {
                if (member is TypeDeclaration) continue;

                var methodDeclaration = member as MethodDeclaration;
                if (methodDeclaration != null)
                {
                    CompileMethod(pair, methodDeclaration);
                    continue;
                }

                var propertyDeclaration = member as PropertyDeclaration;
                if (propertyDeclaration != null)
                {
                    CompileProperty(propertyDeclaration);
                    continue;
                }

                throw new NotImplementedException();
            }
        }

        private void CompileProperty(PropertyDeclaration propertyDeclaration)
        {
            if (propertyDeclaration.Getter.Body.IsNull && propertyDeclaration.Setter.Body.IsNull) return;

            throw new NotSupportedException("Non auto properties are not supported.");
        }

        private void CompileMethod(Pair pair, MethodDeclaration methodDeclaration)
        {
            var method = pair.Definition.ResolveMethod(methodDeclaration.Name);

            if ((methodDeclaration.Modifiers & Modifiers.Override) != 0)
            {
                var overrides = pair.Definition.BaseTypes.Select(x => x.TryResolveMethod(methodDeclaration.Name))
                    .Where(x => x != null).Single();
                method.Overrides = overrides;
            }

            if (method.IsAbstract) return;

            var context = new BodyContext(pair.Definition, this, method);

            context.AddParameters(method.Parameters);

            var bodyGenerator = new BodyGenerator(this);

            var bodyIl = methodDeclaration.Body.AcceptVisitor(bodyGenerator, context);
            if (bodyIl == null) throw new InvalidOperationException("Method body wasn't generated.");
            method.Body = bodyIl;
        }


        public TypeDefinition FindEntry()
        {
            return _types.Select(x => x.Definition).Where(x => !x.IsAbstract).Single();
        }

        public string CompileEntry(TypeDefinition entry)
        {
            var methods = GetAllMethods(entry);

            var stringWriter = new StringWriter();

            //ToDo: Check if writer could only be created when actually writing ASM.
            IMpAsmWriter writer = new MpAsmTextWriter(stringWriter);

            foreach (var method in methods)
            {
                ILabel label;
                label = method.CodeAddress != MethodDefinition.CodeAddressNotSet
                            ? writer.CreateLabel(method.Name, method.CodeAddress)
                            : writer.CreateLabel(method.Name);

                method.Label = label;

                var overridesMethod = method.Overrides;
                while (overridesMethod != null)
                {
                    overridesMethod.Label = label;
                    overridesMethod = overridesMethod.Overrides;
                }
            }

            var changes = true;
            while (changes)
            {
                changes = false;
                foreach (var method in methods)
                {
                    var newBody = method.Body.Optimize();
                    newBody = new InlineMethodVisitor(method).Visit(newBody);
                    if (!ReferenceEquals(newBody, method.Body))
                    {
                        changes = true;
                        method.Body = newBody;
                    }

                    if (!method.ShouldInline && method.CodeAddress == MethodDefinition.CodeAddressNotSet)
                    {
                        var operationsCountVisitor = new OperationsCountVisitor();
                        operationsCountVisitor.Visit(method.Body);
                        if (operationsCountVisitor.Count < 3 && !operationsCountVisitor.HasBranhes)
                        {
                            changes = true;
                            method.ShouldInline = true;
                        }
                    }
                }
            }

            methods = methods.Where(x => !x.ShouldInline).ToList();

            var memManager = new MemoryManager(256);

            memManager.SetNotImplemented(0, 256);
            memManager.SetReserved(0, 0x1f);
            memManager.SetFree(0x20, 64);
            memManager.SetReserved(0x80, 0x1f);

            var memoryVisitor = new MemoryPlannerVisitor(memManager);
            foreach (var method in methods)
            {
                memoryVisitor.SetupMethodDataLocations(method);
                method.Body = memoryVisitor.Visit(method.Body);
            }

            foreach (var method in methods)
            {
                writer.WriteLabel(method.Label);

                method.Body.WriteMpAsm(writer);

                if (method.ReturnType == TypeDefinitions.Void)
                writer.Return();

                writer.Comment("; End of method {0}.", method.Name);

                writer.Comment("");
            }

            Console.WriteLine("Available: {0} bytes.",memManager.CalcAvailableBytes());

            return stringWriter.ToString();
        }

        private static List<MethodDefinition> GetAllMethods(TypeDefinition entry)
        {
            var methods = entry.Methods.ToList();

            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                if (method.Overrides != null)
                {
                    if (methods.Contains(method.Overrides))
                    {
                        RemapVirtualMethodCallsMethods(method, methods);
                        methods.Remove(method.Overrides);
                        i = -1;
                    }
                }
            }
            return methods;
        }

        private static void RemapVirtualMethodCallsMethods(MethodDefinition method, List<MethodDefinition> methods)
        {
            var visitor = new RemapMethodCallVisitor(method.Overrides, method);
            foreach (var methodDefinition in methods)
            {
                if (methodDefinition.Body!=null)
                methodDefinition.Body = visitor.Visit(methodDefinition.Body);
            }
        }
    }
}