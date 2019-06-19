version(Have_autowrap_pynih)
    import autowrap.pynih;
else version(Have_autowrap_csharp)
    import autowrap.csharp;
else
    import autowrap.python;


immutable Modules modules = Modules(Module("prefix"),
                                    Module("adder"),
                                    Module("structs"),
                                    Module("templates"),
                                    Module("api"),
                                    Module("wrap_all", Yes.alwaysExport));

version(Have_autowrap_pynih) {
    enum str = wrapDlang!(
        LibraryName("simple"),
        modules,
    );

    //pragma(msg,str);
    mixin(str);
}
else version(Have_autowrap_csharp) {
    mixin(
        wrapCSharp(
            modules,
            OutputFileName("Simple.cs"),
            autowrap.csharp.LibraryName("simple"),
            RootNamespace("Autowrap.CSharp.Examples.Simple")
        )
    );
}
else
    mixin(
        wrapAll(
            LibraryName("simple"),
            modules,
        )
    );
