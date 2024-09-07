using System;
using System.Numerics;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using OpenTabletDriver.Plugin.Output;

namespace ANTICHATTER;
//ignore most of the stuff, its for compatibility lol
namespace ANTICHATTER
{
    [PluginName("ANTICHATTER")]
    public class Antichatter : IPositionedPipelineElement<IDeviceReport>
    {
        //example property
        /*
        [Property("<name>"), DefaultPropertyValue(6f), ToolTip
            ("<description>")]
        public float variable { set; get; }
        */

        private Vector2 LastPos;
        //variables

        public Vector2 Filter(Vector2 input)
        {

            //example function (stores input as lastPos for next Filter call)


            
            LastPos = Vector2.Multiply(Vector2.Add(input, LastPos), 0.5f);
            return LastPos;
        }



        public event Action<IDeviceReport> Emit;

        public void Consume(IDeviceReport value)
        {
            if (value is ITabletReport report)
            {
                report.Position = Filter(report.Position);
                value = report;
            }

            Emit?.Invoke(value);
        }
        
        public PipelinePosition Position => PipelinePosition.PreTransform;

        
    }
}
