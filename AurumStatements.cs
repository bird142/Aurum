using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurum
{
    /// <summary>
    /// Represents a statement node in the AST.
    /// </summary>
    public interface IStmt
    {
        /// <summary>
        /// Executes the statement and any children.
        /// </summary>
        /// <param name="context">The runtime context in which to execute the statement.</param>
        void Exec(IRTContext context);

        /// <summary>
        /// Checks the statement and any children for type errors.
        /// </summary>
        /// <param name="context">The parse-time context in which to type-check the statement.</param>
        void Check(IPTContext context);
    }

    /// <summary>
    /// Base class for all statements.
    /// </summary>
    internal abstract class BaseStmt : IStmt
    {
        public abstract void Exec(IRTContext context);
        public abstract void Check(IPTContext context);
    }

    /// <summary>
    /// Represents an expression node in the AST.
    /// </summary>
    public interface IExpr : IStmt
    {
        /// <summary>
        /// Evaluates the expression's value.
        /// </summary>
        /// <param name="context">The runtime context to use for evaluation.</param>
        /// <returns>The evaluated value of the expression.</returns>
        AurumObject Eval(IRTContext context);

        /// <summary>
        /// Evaluates the type of the expression.
        /// </summary>
        /// <param name="context">The parse-time context to use for evaluation.</param>
        /// <returns>The evaluated type of the expression.</returns>
        AurumClass EvalType(IPTContext context);
    }
    internal abstract class BaseExpr : BaseStmt, IExpr
    {
        public abstract AurumObject Eval(IRTContext context);
        public abstract AurumClass EvalType(IPTContext context);
        public override void Exec(IRTContext context)
        {
            Eval(context);
        }
        public override void Check(IPTContext context)
        {
            EvalType(context);
        }
    }
}
