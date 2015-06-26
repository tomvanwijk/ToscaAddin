using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.Execution.Results;

namespace CompareDB
{
    [SpecialExecutionTaskName("CompareDB")]
    public class CompareDB : SpecialExecutionTaskEnhanced
    {
        public CompareDB(Validator validator) : base(validator)  { }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            foreach (IParameter parameter in testAction.Parameters)
            {
                //ActionMode input means set the buffer
                if (parameter.ActionMode == ActionMode.Input)
                {
                    IInputValue inputValue = parameter.GetAsInputValue();
                    //Buffers.Instance.SetBuffer(parameter.Name, inputValue.Value);
                    testAction.SetResultForParameter(parameter, SpecialExecutionTaskResultState.Ok, string.Format("Buffer {0} set to value {1}.", parameter.Name, inputValue.Value));
                }
                //Otherwise we let TBox handle the verification. Other ActionModes like WaitOn will lead to an exception.
                else
                {
                    //Don't need the return value of HandleActualValue in this case.
                    //HandleActualValue(testAction, parameter, Buffers.Instance.GetBuffer(parameter.Name));
                }
            }
        }
    }
}
