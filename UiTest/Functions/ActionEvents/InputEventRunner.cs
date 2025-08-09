using System;
using System.Collections.Generic;
using System.Threading;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Model.Cell;
using UiTest.Service.Factory;

namespace UiTest.Functions.ActionEvents
{
    public class InputEventRunner : BaseRunner<ActionEventCoverManagent, object>
    {
        private readonly CellData cellData;

        public InputEventRunner(CellData cellData)
        {
            this.cellData = cellData;
            coverManagement = new ActionEventCoverManagent();
            actionEvents = new List<ActionEventSetting>();
        }
        private readonly List<ActionEventSetting> actionEvents;
        protected override bool IsRunnable()
        {
            return !IsRunning && ActionEvents?.Count > 0;
        }
        public List<ActionEventSetting> ActionEvents { get => actionEvents; set { actionEvents.Clear(); if (value != null) actionEvents.AddRange(value); } }

        protected override void RunAction()
        {
            foreach (var actionEvent in ActionEvents)
            {
                if (coverManagement.IsRunCancelled || !coverManagement.IsPass)
                {
                    break;
                }
                var functionBody = InputEventFactory.Instance.CreateInputActionWith(actionEvent, cellData);
                var functionCover = new EventCover(functionBody, coverManagement);
                functionCover.Run();
            }
            coverManagement.WaitForAllTaskDone();
        }
    }
}
