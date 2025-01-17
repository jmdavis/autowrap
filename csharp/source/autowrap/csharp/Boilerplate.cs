namespace Autowrap {
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using Mono.Unix;

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    public class DLangException : Exception {
        public string DLangExceptionText { get; }
        public DLangException(string dlang) : base() {
            DLangExceptionText = dlang;
        }
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct slice {
        internal IntPtr length;
        internal IntPtr ptr;

        public slice(IntPtr ptr, IntPtr length)
        {
            this.ptr = ptr;
            this.length = length;
        }
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    internal enum DStringType : int {
        None = 0,
        _string = 1,
        _wstring = 2,
        _dstring = 4,
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    public abstract class DLangObject {
        private readonly IntPtr _ptr;
        internal protected IntPtr DLangPointer => _ptr;

        protected DLangObject(IntPtr ptr) {
            this._ptr = ptr;
        }

        ~DLangObject() {
            SharedFunctions.ReleaseMemory(_ptr);
        }
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_void_error {
        public void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr)) throw new DLangException(errStr);
        }
        private slice _error;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_bool_error {
        private void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr)) throw new DLangException(errStr);
        }
        public static implicit operator bool(return_bool_error ret) { ret.EnsureValid(); return ret._value != 0; }
        private byte _value;
        private slice _error;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_slice_error {
        private void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr)) throw new DLangException(errStr);
        }
        public static implicit operator slice(return_slice_error ret) { ret.EnsureValid(); return ret._value; }
        private slice _value;
        private slice _error;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct Marshalled_Duration {

        public Marshalled_Duration(long hnsecs)
        {
            _hnsecs = hnsecs;
        }

        public static implicit operator Marshalled_Duration(TimeSpan val)
        {
            return new Marshalled_Duration(val.Ticks);
        }

        public static implicit operator TimeSpan(Marshalled_Duration val)
        {
            return TimeSpan.FromTicks(val._hnsecs);
        }

        private long _hnsecs;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_Marshalled_Duration_error {

        private void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr))
                throw new DLangException(errStr);
        }

        public static implicit operator TimeSpan(return_Marshalled_Duration_error ret)
        {
            ret.EnsureValid(); return ret._value;
        }

        private Marshalled_Duration _value;
        private slice _error;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct Marshalled_std_datetime_date {

        public Marshalled_std_datetime_date(DateTime dt)
        {
            _year = (short)dt.Year;
            _month = (byte)dt.Month;
            _day = (byte)dt.Day;
            _hour = (byte)dt.Hour;
            _minute = (byte)dt.Minute;
            _second = (byte)dt.Second;
        }

        public static implicit operator Marshalled_std_datetime_date(DateTime val)
        {
            return new Marshalled_std_datetime_date(val);
        }

        public static implicit operator DateTime(Marshalled_std_datetime_date val)
        {
            return new DateTime(val._year, val._month, val._day,
                                val._hour, val._minute, val._second,
                                DateTimeKind.Utc);
        }

        short _year;
        byte _month;
        byte _day;
        byte _hour;
        byte _minute;
        byte _second;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_Marshalled_std_datetime_date_error {

        private void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr))
                throw new DLangException(errStr);
        }

        public static implicit operator DateTime(return_Marshalled_std_datetime_date_error ret)
        {
            ret.EnsureValid(); return ret._value;
        }

        private Marshalled_std_datetime_date _value;
        private slice _error;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct Marshalled_std_datetime_systime {

        public Marshalled_std_datetime_systime(DateTimeOffset dto)
        {
            _ticks = dto.Ticks;
            _utcOffset = dto.Offset.Ticks;
        }

        public static implicit operator Marshalled_std_datetime_systime(DateTimeOffset val)
        {
            return new Marshalled_std_datetime_systime(val);
        }

        public static implicit operator DateTimeOffset(Marshalled_std_datetime_systime val)
        {
            return new DateTimeOffset(val._ticks, TimeSpan.FromTicks(val._utcOffset));
        }

        private long _ticks;
        private long _utcOffset;
    }

    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    [StructLayout(LayoutKind.Sequential)]
    internal struct return_Marshalled_std_datetime_systime_error {

        private void EnsureValid() {
            var errStr = SharedFunctions.SliceToString(_error, DStringType._wstring);
            if (!string.IsNullOrEmpty(errStr))
                throw new DLangException(errStr);
        }

        public static implicit operator DateTimeOffset(return_Marshalled_std_datetime_systime_error ret)
        {
            ret.EnsureValid();
            return ret._value;
        }

        private Marshalled_std_datetime_systime _value;
        private slice _error;
    }

%3$s    [GeneratedCodeAttribute("Autowrap", "1.0.0.0")]
    public static class SharedFunctions {
        static SharedFunctions() {
            Stream stream = null;
            var outputName = Path.Combine(Environment.CurrentDirectory, RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "lib%1$s.dylib" : RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "lib%1$s.so" : "lib%1$s.dll");

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("%2$s.lib%1$s.dylib");
            }
            if (Environment.Is64BitProcess && RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("%2$s.lib%1$s.x64.so");
            }
            if (!Environment.Is64BitProcess && RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("%2$s.lib%1$s.x86.so");
            }
            if (Environment.Is64BitProcess && RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("%2$s.%1$s.x64.dll");
            }
            if (!Environment.Is64BitProcess && RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) {
                stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("%2$s.%1$s.x86.dll");
            }
            if (stream != null) {
                using(var file = new FileStream(outputName, FileMode.OpenOrCreate, FileAccess.Write)) {
                    stream.CopyTo(file);
                    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                        var ufi = new UnixFileInfo(outputName);
                        ufi.FileAccessPermissions |= FileAccessPermissions.UserExecute | FileAccessPermissions.GroupExecute | FileAccessPermissions.OtherExecute; 
                    }
                }
            } else {
                throw new DllNotFoundException($"The required native assembly is unavailable for the current operating system and process architecture.");
            }
        }

        internal static string SliceToString(slice str, DStringType type) {
            try {
                unsafe {
                    var bytes = (byte*)str.ptr.ToPointer();
                    if (bytes == null) {
                        return null;
                    }
                    if (type == DStringType._string) {
                        return String.Copy(System.Text.Encoding.UTF8.GetString(bytes, str.length.ToInt32()*(int)type));
                    } else if (type == DStringType._wstring) {
                        return String.Copy(System.Text.Encoding.Unicode.GetString(bytes, str.length.ToInt32()*(int)type));
                    } else if (type == DStringType._dstring) {
                        return String.Copy(System.Text.Encoding.UTF32.GetString(bytes, str.length.ToInt32()*(int)type));
                    } else {
                        throw new UnauthorizedAccessException("Unable to convert D string to C# string: Unrecognized string type.");
                    }
                }
            } finally {
                SharedFunctions.ReleaseMemory(str.ptr);
            }
        }

        // Support Functions
        [DllImport("%1$s", EntryPoint = "autowrap_csharp_createString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern slice CreateString([MarshalAs(UnmanagedType.LPWStr)]string str);

        [DllImport("%1$s", EntryPoint = "autowrap_csharp_createWString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern slice CreateWstring([MarshalAs(UnmanagedType.LPWStr)]string str);

        [DllImport("%1$s", EntryPoint = "autowrap_csharp_createDString", CallingConvention = CallingConvention.Cdecl)]
        internal static extern slice CreateDstring([MarshalAs(UnmanagedType.LPWStr)]string str);

        [DllImport("%1$s", EntryPoint = "autowrap_csharp_release", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ReleaseMemory(IntPtr ptr);

        [DllImport("%1$s", EntryPoint = "rt_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DRuntimeInitialize();

        [DllImport("%1$s", EntryPoint = "rt_term", CallingConvention = CallingConvention.Cdecl)]
        public static extern int DRuntimeTerminate();
    }

%4$s
}
