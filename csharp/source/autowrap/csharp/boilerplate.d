/**
   Necessary boilerplate for C#/.NET.

   To wrap all functions/return/parameter types and struct/class definitions from
   a list of modules, write this in a "main" module and generate mylib.{so,dll}:

   ------
   mixin wrapAll(LibraryName("mylib"), Modules("module1", "module2", ...));
   ------
 */
module autowrap.csharp.boilerplate;

import autowrap.csharp.dlang;
import autowrap.reflection;

//public:

/**
struct LibraryName {
    string value;
}

   The list of modules to automatically wrap for .NET consumption

struct Modules {
    import autowrap.reflection: Module;
    import std.traits: Unqual;
    import std.meta: allSatisfy;

    Module[] value;

    this(A...)(auto ref A modules) {

        foreach(module_; modules) {
            static if(is(Unqual!(typeof(module_)) == Module))
                value ~= module_;
            else static if(is(Unqual!(typeof(module_)) == string))
                value ~= Module(module_);
            else
                static assert(false, "Modules must either be `string` or `Module`");
        }
    }
}
 */
public string wrapCSharp(in string libraryName, in Modules modules) @safe pure {
    import std.format : format;
    import std.algorithm: map;
    import std.array: join;

    if(!__ctfe) return null;

    const modulesList = modules.value.map!(a => a.toString).join(", ");

    return q{
        //Insert shared boilerplate code
        %1$s

        import std.typecons;
        import autowrap.csharp.boilerplate;
        import autowrap.csharp.dlang : wrapDLang;

        immutable string t = wrapDLang!(%2$s);
        mixin(t);
        pragma(msg, t);

        //Insert DllMain for Windows only.
        version(Windows) {
            %3$s
        }
    }.format(commonBoilerplate(), modulesList, dllMainMixinStr());
}

/**
   Returns a string to be mixed in that defines the boilerplate code needed by all C# interfaces.
 */
private string commonBoilerplate() @safe pure {
    return q{
        import core.memory;
        import std.conv;
        import std.string;
        import std.traits;
        import std.utf;

        extern(C) export struct returnValue(T) {
            T value;
            wstring error;

            this(T value) nothrow {
                this.value = value;
                static if (isArray!(T) || is(T == class) || is(T == interface)) {
                    if (this.value !is null) {
                        pinPointer(cast(void*)this.error.ptr);
                    }
                }
                this.error = null;
            }

            this(Exception error) nothrow {
                this.value = T.init;
                try {
                    this.error = to!wstring(error.toString());
                } catch(Exception ex) {
                    this.error = "Unhandled Exception while marshalling exception data. You should never see this error.";
                }
                pinPointer(cast(void*)this.error.ptr);
            }
        }

        extern(C) export struct returnVoid {
            wstring error = null;

            this(Exception error) nothrow {
                try {
                    this.error = to!wstring(error.toString());
                } catch(Exception ex) {
                    this.error = "Unhandled Exception while marshalling exception data. You should never see this error.";
                }
                pinPointer(cast(void*)this.error.ptr);
            }
        }

        package void pinPointer(void* ptr) nothrow {
            GC.setAttr(ptr, GC.BlkAttr.NO_MOVE);
            GC.addRoot(ptr);
        }

        extern(C) export void autowrap_csharp_release(void* ptr) nothrow {
            GC.clrAttr(ptr, GC.BlkAttr.NO_MOVE);
            GC.removeRoot(ptr);
        }

        extern(C) export string autowrap_csharp_createString(wchar* str) nothrow {
            string temp = toUTF8(to!wstring(str.fromStringz()));
            pinPointer(cast(void*)temp.ptr);
            return temp;
        }

        extern(C) export wstring autowrap_csharp_createWString(wchar* str) nothrow {
            wstring temp = toUTF16(to!wstring(str.fromStringz()));
            pinPointer(cast(void*)temp.ptr);
            return temp;
        }

        extern(C) export dstring autowrap_csharp_createDString(wchar* str) nothrow {
            dstring temp = toUTF32(to!wstring(str.fromStringz()));
            pinPointer(cast(void*)temp.ptr);
            return temp;
        }
    };
}

/**
   Returns a string to be mixed in that defines DllMain, needed on Windows.
 */
private string dllMainMixinStr() @safe pure {
    return q{
        import core.sys.windows.windows: HINSTANCE, BOOL, ULONG, LPVOID;

        __gshared HINSTANCE g_hInst;

        extern (Windows) BOOL DllMain(HINSTANCE hInstance, ULONG ulReason, LPVOID pvReserved) {

            import core.sys.windows.windows;
            import core.sys.windows.dll;

            switch (ulReason)  {

                case DLL_PROCESS_ATTACH:
                    g_hInst = hInstance;
                    dll_process_attach(hInstance, true);
                break;

                case DLL_PROCESS_DETACH:
                    dll_process_detach(hInstance, true);
                    break;

                case DLL_THREAD_ATTACH:
                    dll_thread_attach(true, true);
                    break;

                case DLL_THREAD_DETACH:
                    dll_thread_detach(true, true);
                    break;

                default:
            }

            return true;
        }
    };
}

