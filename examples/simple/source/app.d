import std.typecons: Yes, No;
import autowrap.csharp;
import autowrap.reflection;

immutable Modules modules = Modules(Module("prefix"),
                                    Module("adder"),
                                    Module("structs"),
                                    Module("templates"),
                                    Module("api"),
                                    Module("wrap_all", Yes.alwaysExport));

version(EmitCSharp) {}
else
{
    import autowrap.python;
    mixin(
        wrapAll(
            autowrap.python.LibraryName("simple"),
            modules
        )
    );
}

mixin(
    wrapCSharp(
        modules,
        OutputFileName("Simple.cs"),
        autowrap.csharp.LibraryName("simple"),
        RootNamespace("Autowrap.CSharp.Examples.Simple")
    )
);
