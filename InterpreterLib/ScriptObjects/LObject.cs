using InterpreterLib.InterpreterModules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace InterpreterLib.ScriptObjects
{
    public class SObject
    {
        public VariablesHeap Vars { get; private set; } = new VariablesHeap();
        public SObjectType Type { get; private set; } = SObjectType.NoValue;

        private bool boolValue;
        public bool BoolValue
        {
            get => boolValue;
            set
            {
                boolValue = value;
                Type = SObjectType.Boolean;
            }
        }

        private decimal numValue;
        public decimal NumValue 
        {
            get => Type switch
            {
                SObjectType.NoValue => throw new NotImplementedException(),
                SObjectType.Numeric => numValue,
                SObjectType.String => GetDecimalValue(stringValue),
                SObjectType.Boolean => boolValue ? 1 : 0,
                _ => throw new NotImplementedException(),
            };
            set
            {
                numValue = value;
                Type = SObjectType.Numeric;
            }
        }

        private string stringValue;
        public string StringValue 
        {
            get => Type switch
            {
                SObjectType.NoValue => "",
                SObjectType.Numeric => numValue.ToString(),
                SObjectType.String => stringValue,
                SObjectType.Boolean => boolValue.ToString(),
                _ => ""
            };
            set
            {
                stringValue = value;
                Type = SObjectType.String;
            }
        }

        public SObject()
        {
        }

        public SObject(SObject obj)
        {
            SetValue(obj);
        }

        public SObject(decimal numVal)
        {
            NumValue = numVal;
        }

        public SObject(string stringVal)
        {
            StringValue = stringVal;
        }

        public SObject(bool boolVal)
        {
            BoolValue = boolVal;
        }

        public void SetValue(SObject obj)
        {
            Type = obj.Type;

            switch (obj.Type)
            {
                case SObjectType.NoValue:                    
                    break;
                case SObjectType.Numeric:
                    NumValue = obj.NumValue;
                    break;
                case SObjectType.String:
                    StringValue = obj.StringValue;
                    break;
                case SObjectType.Boolean:
                    BoolValue = obj.BoolValue;
                    break;
                default:                    
                    break;
            }
        }

        public override bool Equals(object obj)
        {
            SObject lObject = obj as SObject;

            if (lObject == null)
                return false;

            if (lObject.Type != Type)
                return false;

            return StringValue == lObject.StringValue;
        }

        public override string ToString()
        {
            return StringValue;

        }

        private decimal GetDecimalValue(string text)
        {
            string tokenString = text.Replace(',', '.');
            decimal num;
            if (!decimal.TryParse(tokenString, NumberStyles.Any, CultureInfo.InvariantCulture, out num))
                throw new ArgumentException($"Wrong numeric value '{text}'");
            return num;
        }

    }
}
