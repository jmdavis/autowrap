import autowrap.csharp;
import autowrap.python;

immutable Modules modules = Modules(Module("prefix"),
                                    Module("adder"),
                                    Module("structs"),
                                    Module("templates"),
                                    Module("api"),
                                    Module("wrap_all", Yes.alwaysExport));

mixin(
    wrapAll(
        autowrap.python.LibraryName("simple"),
        modules
    )
);

mixin(
    wrapCSharp(
        modules,
        OutputFileName("simple.cs"),
        autowrap.csharp.LibraryName("simple"),
        RootNamespace("Autowrap.CSharp.Tests.simple")
    )
);
