using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic.Basics
{
    public enum Operation
    {
        Sum,
        Subtract,
        Multiply
    }
    public class ExecutionManager
    {
        public Dictionary<Operation, Func<int>> FuncExecute { get; set; }
        private Func<int> _sum;
        private Func<int> _subtract;
        private Func<int> _multiply;

        public ExecutionManager()
        {
            FuncExecute = new Dictionary<Operation, Func<int>>(3);
        }

        public void PopulateFunctions(Func<int> Sum, Func<int> Subtract, Func<int> Multiply)
        {
            _sum = Sum;
            _subtract = Subtract;
            _multiply = Multiply;
        }

        public void PrepareExecution()
        {
            FuncExecute.Add(Operation.Sum, _sum);
            FuncExecute.Add(Operation.Subtract, _subtract);
            FuncExecute.Add(Operation.Multiply, _multiply);
        }
    }

    public class OperationManager
    {
        private int _first;
        private int _second;
        private readonly ExecutionManager _executionManager;

        public OperationManager(int first, int second, ExecutionManager executionManager)
        {
            _first = first;
            _second = second;
            _executionManager = executionManager;
            _executionManager.PopulateFunctions(Sum, Subtract, Multiply);
            _executionManager.PrepareExecution();
        }

        private int Sum()
        {
            return _first + _second;
        }

        private int Subtract()
        {
            return _first - _second;
        }

        private int Multiply()
        {
            return _first * _second;
        }

        public int Execute(Operation operation)
        {
            return _executionManager.FuncExecute.ContainsKey(operation) ?
                _executionManager.FuncExecute[operation]() :
                -1;
        }
    }
}
