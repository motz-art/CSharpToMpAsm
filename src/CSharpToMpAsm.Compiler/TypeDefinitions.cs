namespace CSharpToMpAsm.Compiler
{
    public static class TypeDefinitions
    {
        public static bool IsNumeric(this TypeDefinition type)
        {
            return type == Int32 || type == Byte || type == SByte;
        }

        public static TypeDefinition Void = new TypeDefinition
                                                {
                                                    Name = "void",
                                                    IsAbstract = true,
                                                    Size = 0,
                                                };

        public static TypeDefinition Int32 = new TypeDefinition
                                                 {
                                                     Size = 4,
                                                     IsAbstract = false,
                                                     Name = "Int32",
                                                     NameSpace = "System"
                                                 };

        public static TypeDefinition Byte = new TypeDefinition
                                                {
                                                    Size = 1,
                                                    IsAbstract = false,
                                                    Name = "Byte",
                                                    NameSpace = "System"
                                                };

        public static TypeDefinition Char = new TypeDefinition
                                                 {
                                                     Size = 1,
                                                     IsAbstract = false,
                                                     Name = "Char",
                                                     NameSpace = "System"
                                                 };

        public static TypeDefinition SByte = new TypeDefinition
                                                 {
                                                     Size = 1,
                                                     IsAbstract = false,
                                                     Name = "SByte",
                                                     NameSpace = "System"
                                                 };
    }
}