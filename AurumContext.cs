using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum
{
    /// <summary>
    /// Represents all the variables in a scope at runtime, such as a block or a function
    /// </summary>
    public class AurumScope : Dictionary<string, AurumObject>
    {
        public int ID { get; }
        public AurumScope(int id) : base()
        {
            ID = id;
        }
        public AurumScope(int id, IDictionary<string, AurumObject> dictionary) : base(dictionary)
        {
            ID = id;
        }
    }

    /// <summary>
    /// Represents a context during runtime
    /// </summary>
    public interface IRTContext
    {
        AurumObject GetVar(string name);
        void SetVar(string name, AurumObject value);
        void DeclareVar(string name, AurumObject value = null);
        void PushScope(int id = 0);
        void PopScope();
        void Return(AurumObject value);
        AurumObject ReturnValue { get; }
        void Defer(IStmt stmt);
        void RunDeferred();
        void RunBlock(IStmt stmt);
        IRTContext NewFuncContext(int id);
        ISet<RTCFlags> Flags { get; }
    }

    public enum RTCFlags
    {
        Return,
        Break,
        Continue,
    }

    /// <summary>
    /// Represents a context during parsing, mainly used for type checking
    /// </summary>
    public interface IPTContext
    {
        void AddError(string message);
        void Finish();
        void DefineVar(string name, AurumClass type);
        bool TryGetVar(string name, out AurumClass type);
        void Push();
        void Pop();
        AurumClass GetReturnType();
        void SetReturnType(AurumClass type);
    }
}
