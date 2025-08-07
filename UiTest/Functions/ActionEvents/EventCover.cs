using System;
using System.Threading.Tasks;
using UiTest.Config.Events;
using UiTest.Functions.Interface;
using UiTest.Service.Logger;

namespace UiTest.Functions.ActionEvents
{
    public class EventCover : BaseCover<object>
    {
        public EventCover(IActionEvent actionEvent, CoverManagement<object> coverManagement) : base(actionEvent, coverManagement)
        {
        }

        public override void Run()
        {
            try
            {
                if (IsRunning || !coverManagement.TryAdd(this)) return;
                isRunning = true;
                functionBody.Run();
            }
            catch (Exception ex)
            {
                ProgramLogger.AddError(nameof(this.GetType), ex.Message);
            }
            finally
            {
                isRunning = false;
                IsAcceptable= functionBody.IsAcceptable;
                coverManagement.SetTestDone(this);
            }
        }
    }
}
