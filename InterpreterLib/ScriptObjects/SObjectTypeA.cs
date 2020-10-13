using System;
using System.Collections.Generic;

namespace InterpreterLib.ScriptObjects
{
    /// <summary>
    /// Экспериментальный тип
    /// </summary>
    public struct SObjectTypeA
    {
        private SObjectTypeInt typeInt;

        private Func<object, string> convertToStringFunc;
        private Func<object, decimal> convertToDecimalFunc;
        private Func<object, bool> convertToBoolFunc;

        private SObjectTypeA(SObjectTypeInt typeInt,
            Func<object, string> convertToStringFunc, 
            Func<object, decimal> convertToDecimalFunc, 
            Func<object, bool> convertToBoolFunc)
        {
            this.typeInt = typeInt;
            this.convertToStringFunc = convertToStringFunc;
            this.convertToDecimalFunc = convertToDecimalFunc;
            this.convertToBoolFunc = convertToBoolFunc;
        }


        public override bool Equals(object obj)
        {
            if(obj is SObjectTypeA type)
                return type.typeInt == typeInt;
            else
                return false;
        }

        public override int GetHashCode() => (int)typeInt;
        public override string ToString() => typeInt.ToString();


        private static Dictionary<SObjectTypeInt, SObjectTypeA> types = new Dictionary<SObjectTypeInt, SObjectTypeA>();

        static SObjectTypeA()
        {
            AddType(new SObjectTypeA(SObjectTypeInt.NoValue,
                    (o) => string.Empty,
                    (o) => throw new NotImplementedException("Variable has no value"),
                    (o) => throw new NotImplementedException("Variable has no value")
                ));
            AddType(new SObjectTypeA(SObjectTypeInt.Numeric,
                    (o) => o.ToString(),
                    (o) => (decimal)o,
                    (o) => (decimal)o==0 ? false : true
                ));
            AddType(new SObjectTypeA(SObjectTypeInt.String,
                    (o) => o.ToString(),
                    (o) => (decimal)o,
                    (o) =>
                    {
                        string val = o?.ToString().ToLower();
                        if (val == "true")
                            return true;
                        else if (val == "false")
                            return false;
                        else
                            throw new NotImplementedException($"Can't convert '{o}' to boolean value");
                    }
                ));
            //AddType(new SObjectTypeA(SObjectTypeInt.Boolean,
            //        (o) => o.ToString(),
            //        (o) => (bool)o,
            //        (o) => (decimal)o == 0 ? false : true
            //    ));
            //AddType(new SObjectTypeA(SObjectTypeInt.Array));
        }

        public static void AddType(SObjectTypeA type)
        {
            types.Add(type.typeInt, type);
        }

        public static bool operator == (SObjectTypeA type1, SObjectTypeA type2) => type1.Equals(type2);
        public static bool operator != (SObjectTypeA type1, SObjectTypeA type2) => !type1.Equals(type2);

        public static SObjectTypeA NoValue => types[SObjectTypeInt.NoValue];
        public static SObjectTypeA Numeric => types[SObjectTypeInt.Numeric];
        public static SObjectTypeA String => types[SObjectTypeInt.String];
        public static SObjectTypeA Boolean => types[SObjectTypeInt.Boolean];
        public static SObjectTypeA Array => types[SObjectTypeInt.Array];

        enum SObjectTypeInt
        {
            NoValue,
            Numeric,
            String,
            Boolean,
            Array
        }

    }

}
