using System;
using UiTest.Common;
using UiTest.Model.Function;
using UiTest.Config.Items;
using UiTest.Service.Communicate.Interface;
using UiTest.Service.Communicate.Implement.Serial;
using UiTest.Service.Communicate;
using UiTest.Functions.TestFunctions.Config.Sfis;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace UiTest.Functions.TestFunctions.Body.Sfis
{
    public class SendSfis : BaseFunctionBody<SfisConfig>
    {
        public SendSfis(FunctionData functionData) : this(null, functionData, new ItemSetting()) { }
        public SendSfis(SfisConfig config, FunctionData functionData, ItemSetting itemSetting) : base(config, functionData, itemSetting) { }
        protected override (TestStatus status, string value) Test()
        {
            using (BaseCommunicate serial = new MySerialPort("com8", 9600))
            {
                if (serial.Connect())
                {
                    string line;
                    serial.WriteLine("Hi!");
                    while ((line = serial.ReadLine()) != null)
                    {
                        NewMethod(serial, line);
                        if (line == "q")
                        {
                            break;
                        }
                    }
                    Logger.AddInfoText("out");
                    serial.WriteLine("out");
                }
                else
                {
                    Logger.AddInfoText("f");
                }
            }
            return (TestStatus.PASSED, RetryTimes.ToString());
        }

        private void NewMethod(IWriter serial, string line)
        {
            Logger.AddInfoText(line);
            serial.WriteLine("ok");
        }
    }
}
