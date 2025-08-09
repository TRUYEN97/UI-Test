using System;
using System.Collections.Generic;
using UiTest.Config.Events;
using UiTest.Service.Factory;

namespace UiTest.Functions.ActionEvents
{
    public class ActionEventRunner : BaseRunner<ActionEventCoverManagent, object>
    {
        private readonly List<ActionEventSetting> actionEvents;

        public ActionEventRunner()
        {
            coverManagement = new ActionEventCoverManagent();
            actionEvents = new List<ActionEventSetting>();
        }
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
                var functionBody = ActionEventFatory.Instance.CreateFunctionWith(actionEvent);
                var functionCover = new EventCover(functionBody, coverManagement);
                functionCover.Run();
            }
            coverManagement.WaitForAllTaskDone();
        }
    }
}
